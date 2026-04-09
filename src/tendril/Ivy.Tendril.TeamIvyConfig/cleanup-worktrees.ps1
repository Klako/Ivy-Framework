# Cleanup worktrees from terminal-state plans (Completed, Skipped, Failed)
# Preserves worktrees for plans in active states (ReadyForReview, Draft, InProgress, etc.)
#
# By-the-book cleanup:
#   1. git worktree remove  - for worktrees still registered with a parent repo
#   2. Remove-Item          - for leftover directories already unlinked from git
#   3. git worktree prune   - on each parent repo to clean stale metadata

param(
    [switch]$DryRun
)

$PlansDir = "D:\Tendril\Plans"
$ReposDir = "D:\Repos\_Ivy"
$TerminalStates = @("Completed", "Skipped", "Failed")

$cleaned = 0
$skipped = 0
$errors = 0
$totalFreed = 0

# Step 1: Build a lookup of registered worktrees per parent repo
Write-Host "Scanning parent repos for registered worktrees..." -ForegroundColor Cyan
$repos = @("Ivy", "Ivy-Framework", "Ivy-Tendril", "Ivy-Agent", "Ivy.Dbml.Parser", "Ivy.Agent.Test.Manager")
$registeredWorktrees = @{}

foreach ($repo in $repos) {
    $repoPath = Join-Path $ReposDir $repo
    if (-not (Test-Path (Join-Path $repoPath ".git"))) { continue }

    $wtList = git -C $repoPath worktree list --porcelain 2>$null
    foreach ($line in $wtList) {
        if ($line -match "^worktree\s+(.+)$") {
            $wtPath = $Matches[1].Trim() -replace '/', '\'
            $registeredWorktrees[$wtPath] = $repoPath
        }
    }
}

Write-Host "  Found $($registeredWorktrees.Count) registered worktrees across all repos" -ForegroundColor Cyan
Write-Host ""

# Step 2: Process plans
$plans = Get-ChildItem -Path $PlansDir -Directory | Where-Object {
    Test-Path (Join-Path $_.FullName "worktrees")
}

Write-Host "Found $($plans.Count) plans with worktree directories" -ForegroundColor Cyan
Write-Host ""

foreach ($plan in $plans) {
    $yamlPath = Join-Path $plan.FullName "plan.yaml"
    $worktreesDir = Join-Path $plan.FullName "worktrees"

    # Read state from plan.yaml
    $state = $null
    if (Test-Path $yamlPath) {
        $match = Select-String -Path $yamlPath -Pattern "^state:\s*(\S+)" | Select-Object -First 1
        if ($match) {
            $state = $match.Matches[0].Groups[1].Value
        }
    }

    if ($state -notin $TerminalStates) {
        Write-Host "  SKIP $($plan.Name) [state: $state]" -ForegroundColor Yellow
        $skipped++
        continue
    }

    # Calculate size before deleting
    $size = (Get-ChildItem -Path $worktreesDir -Recurse -Force -ErrorAction SilentlyContinue |
             Measure-Object -Property Length -Sum).Sum
    $sizeLabel = "{0:N1} MB" -f ($size / 1MB)

    # Check each repo subdirectory for registered worktrees
    $repoSubDirs = Get-ChildItem -Path $worktreesDir -Directory -ErrorAction SilentlyContinue
    foreach ($repoSub in $repoSubDirs) {
        $normalizedPath = $repoSub.FullName -replace '/', '\'
        if ($registeredWorktrees.ContainsKey($normalizedPath)) {
            $parentRepo = $registeredWorktrees[$normalizedPath]
            $repoName = [System.IO.Path]::GetFileName($parentRepo)
            if ($DryRun) {
                Write-Host "  [DRY] git worktree remove --force $($repoSub.FullName)" -ForegroundColor Magenta
            } else {
                try {
                    git -C $parentRepo worktree remove --force $repoSub.FullName 2>$null
                    Write-Host "  GIT  $($plan.Name)/$($repoSub.Name) - worktree removed from $repoName" -ForegroundColor DarkGreen
                } catch {
                    Write-Host "  WARN git worktree remove failed for $($repoSub.FullName), will force-delete" -ForegroundColor DarkYellow
                }
            }
        }
    }

    # Delete the worktrees directory (catches both unregistered leftovers and any git-remove remnants)
    if ($DryRun) {
        Write-Host "  [DRY] DEL $($plan.Name) [$sizeLabel]" -ForegroundColor Magenta
    } else {
        try {
            Remove-Item -Path $worktreesDir -Recurse -Force -ErrorAction Stop
            Write-Host "  DEL  $($plan.Name) [$sizeLabel]" -ForegroundColor Green
            $totalFreed += $size
            $cleaned++
        }
        catch {
            Write-Host "  ERR  $($plan.Name): $($_.Exception.Message)" -ForegroundColor Red
            $errors++
        }
    }
}

# Step 3: Prune stale worktree metadata in all parent repos
Write-Host ""
Write-Host "Pruning stale worktree metadata..." -ForegroundColor Cyan
foreach ($repo in $repos) {
    $repoPath = Join-Path $ReposDir $repo
    if (-not (Test-Path (Join-Path $repoPath ".git"))) { continue }

    if ($DryRun) {
        Write-Host "  [DRY] git -C $repoPath worktree prune" -ForegroundColor Magenta
    } else {
        git -C $repoPath worktree prune 2>$null
        Write-Host "  PRUNE $repo" -ForegroundColor DarkGreen
    }
}

$freedLabel = "{0:N2} GB" -f ($totalFreed / 1GB)
Write-Host ""
Write-Host "Done." -ForegroundColor Cyan
Write-Host "  Cleaned: $cleaned"
Write-Host "  Skipped: $skipped (active state)"
Write-Host "  Errors:  $errors"
if (-not $DryRun) {
    Write-Host "  Freed:   $freedLabel"
}
