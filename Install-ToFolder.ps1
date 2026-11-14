# Run from the extracted package. Makes a backup rather than merging old and new code.
$ErrorActionPreference = 'Stop'
$source = [System.IO.Path]::GetFullPath($PSScriptRoot).TrimEnd('\','/')
$targetInput = Read-Host 'Full path of the existing GoSound folder (containing the solution)'
if ([string]::IsNullOrWhiteSpace($targetInput)) { throw 'A target path is required.' }
$target = [System.IO.Path]::GetFullPath($targetInput).TrimEnd('\','/')
if ($target -eq $source -or $target.StartsWith($source + [System.IO.Path]::DirectorySeparatorChar, [System.StringComparison]::OrdinalIgnoreCase) -or $source.StartsWith($target + [System.IO.Path]::DirectorySeparatorChar, [System.StringComparison]::OrdinalIgnoreCase)) { throw 'Source and target must be separate folders, not nested.' }
if ([string]::IsNullOrWhiteSpace([System.IO.Path]::GetFileName($target))) { throw 'Choose a project folder, not a drive root.' }
Write-Host 'Close Visual Studio before continuing.'
if (Test-Path -LiteralPath $target) {
    $backup = $target + '_Backup_' + (Get-Date -Format 'yyyyMMdd_HHmmss')
    if (Test-Path -LiteralPath $backup) { throw 'Backup folder already exists.' }
    Move-Item -LiteralPath $target -Destination $backup
    Write-Host "Old project preserved at $backup"
}
New-Item -ItemType Directory -Path $target -Force | Out-Null
Get-ChildItem -LiteralPath $source -Force | Where-Object { $_.Name -notin @('.git','.vs','bin','obj') } | Copy-Item -Destination $target -Recurse -Force
Write-Host "Project copied to $target. Run Start-GoSound.ps1 there. Your old .git and local settings remain in the backup."
