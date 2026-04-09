$plansDir = Join-Path $env:TENDRIL_HOME "Plans"
if (-not (Test-Path $plansDir)) {
    Write-Host "Plans directory not found: $plansDir" -ForegroundColor Red
    exit 1
}

$states = @{}
$total = 0
$parseErrors = 0

Get-ChildItem -Path $plansDir -Directory | Where-Object { $_.Name -match '^\d{5}-' } | ForEach-Object {
    $yamlPath = Join-Path $_.FullName "plan.yaml"
    if (-not (Test-Path $yamlPath)) { return }

    $total++
    $yaml = Get-Content $yamlPath -Raw
    if ($yaml -match '(?m)^state:\s*(.+)$') {
        $state = $Matches[1].Trim()
        if (-not $states.ContainsKey($state)) { $states[$state] = 0 }
        $states[$state] = $states[$state] + 1
    } else {
        $parseErrors++
    }
}

Write-Host "`nPlan state counts ($total total):" -ForegroundColor Cyan
$states.GetEnumerator() | Sort-Object Value -Descending | ForEach-Object {
    Write-Host ("  {0,-20} {1}" -f $_.Key, $_.Value)
}
if ($parseErrors -gt 0) {
    Write-Host "`n  (no state found)    $parseErrors" -ForegroundColor Yellow
}
