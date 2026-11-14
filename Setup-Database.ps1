$ErrorActionPreference = 'Stop'
Push-Location $PSScriptRoot
try {
    dotnet restore GoSound.sln
    if ($LASTEXITCODE -ne 0) { throw 'Restore failed.' }
    dotnet tool restore
    if ($LASTEXITCODE -ne 0) { throw 'Tool restore failed.' }
    if (-not (Test-Path 'GoSound/Migrations/*_InitialCreate.cs')) {
        dotnet ef migrations add InitialCreate --project GoSound/GoSound.csproj --output-dir Migrations
        if ($LASTEXITCODE -ne 0) { throw 'Migration generation failed.' }
    }
    dotnet ef database update --project GoSound/GoSound.csproj
    if ($LASTEXITCODE -ne 0) { throw 'Database update failed.' }
    Write-Host 'Database ready. Review migration and commit it to GitHub.'
} finally { Pop-Location }
