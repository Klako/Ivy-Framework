# Retry cleanup for worktree directories that failed due to:
#   - Windows long paths (node_modules/.pnpm)
#   - Locked files (Access denied)
#
# Uses robocopy /MIR against an empty dir to handle long paths,
# then rd /s /q with \\?\ prefix as fallback.

$PlansDir = "D:\Tendril\Plans"
$EmptyDir = "$env:TEMP\__empty_cleanup_dir"

# Create an empty directory for robocopy mirror trick
if (-not (Test-Path $EmptyDir)) {
    New-Item -ItemType Directory -Path $EmptyDir | Out-Null
}

# Read the list of failed plans
$failedPlans = Get-Content "C:\Users\niels\AppData\Local\Temp\2\failed_plans.txt" | Where-Object { $_.Trim() -ne "" }

# Helper: kill processes locking files under a directory
function Kill-LockingProcesses($dir) {
    # Use handle64 if available, otherwise try Get-Process with modules
    $procs = Get-Process | Where-Object {
        try {
            $_.Modules | Where-Object { $_.FileName -like "$dir*" }
        } catch { $false }
    }
    foreach ($p in $procs) {
        Write-Host "  KILL PID $($p.Id) ($($p.ProcessName)) - locking files in $([System.IO.Path]::GetFileName($dir))" -ForegroundColor DarkYellow
        Stop-Process -Id $p.Id -Force -ErrorAction SilentlyContinue
    }
}

Write-Host "Retrying cleanup for $($failedPlans.Count) failed plans" -ForegroundColor Cyan
Write-Host ""

$cleaned = 0
$errors = 0
$totalFreed = 0

foreach ($planName in $failedPlans) {
    $worktreesDir = Join-Path $PlansDir "$planName\worktrees"

    if (-not (Test-Path $worktreesDir)) {
        Write-Host "  GONE $planName (already cleaned)" -ForegroundColor DarkGray
        $cleaned++
        continue
    }

    # Estimate size (may undercount due to long paths, but best effort)
    $size = 0
    try {
        $size = (cmd /c "dir /s /a `"$worktreesDir`"" 2>$null | Select-String "(\d[\d,\.]+) bytes" | Select-Object -Last 1)
        if ($size -match '([\d,\.]+)\s+bytes') {
            $size = [int64]($Matches[1] -replace '[,\.]', '')
        } else {
            $size = 0
        }
    } catch { $size = 0 }

    $sizeLabel = "{0:N1} MB" -f ($size / 1MB)

    # Step 0: Kill any processes locking files in this worktree
    Kill-LockingProcesses $worktreesDir

    # Step 1: Use robocopy /MIR to empty the directory (handles long paths)
    $repoSubDirs = Get-ChildItem -Path $worktreesDir -Directory -ErrorAction SilentlyContinue
    foreach ($sub in $repoSubDirs) {
        $result = robocopy $EmptyDir $sub.FullName /MIR /R:1 /W:1 /NFL /NDL /NJH /NJS /NP 2>&1
        if (Test-Path $sub.FullName) {
            # Robocopy mirror leaves the empty root dir, remove it
            try {
                Remove-Item -Path $sub.FullName -Recurse -Force -ErrorAction Stop 2>$null
            } catch {
                # Fallback: rd with long path prefix
                $longPath = "\\?\$($sub.FullName)"
                cmd /c "rd /s /q `"$longPath`"" 2>$null
            }
        }
    }

    # Step 2: Remove the now-empty worktrees dir itself
    if (Test-Path $worktreesDir) {
        try {
            Remove-Item -Path $worktreesDir -Recurse -Force -ErrorAction Stop
        } catch {
            $longPath = "\\?\$worktreesDir"
            cmd /c "rd /s /q `"$longPath`"" 2>$null
        }
    }

    # Verify
    if (-not (Test-Path $worktreesDir)) {
        Write-Host "  DEL  $planName [$sizeLabel]" -ForegroundColor Green
        $totalFreed += $size
        $cleaned++
    } else {
        # Last resort: check what's still holding files
        $remaining = (Get-ChildItem -Path $worktreesDir -Recurse -Force -ErrorAction SilentlyContinue | Measure-Object).Count
        Write-Host "  ERR  $planName ($remaining items remaining)" -ForegroundColor Red
        $errors++
    }
}

# Cleanup temp empty dir
Remove-Item -Path $EmptyDir -Force -ErrorAction SilentlyContinue

$freedLabel = "{0:N2} GB" -f ($totalFreed / 1GB)
Write-Host ""
Write-Host "Done." -ForegroundColor Cyan
Write-Host "  Cleaned: $cleaned"
Write-Host "  Errors:  $errors"
Write-Host "  Freed:   ~$freedLabel"
