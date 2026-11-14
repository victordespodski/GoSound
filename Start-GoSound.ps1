$ErrorActionPreference = 'Stop'
Push-Location $PSScriptRoot
try {
    if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) { throw 'Install .NET 10 SDK from https://dotnet.microsoft.com/download/dotnet/10.0' }
    if (-not ((dotnet --list-sdks) -match '^10\.')) { throw '.NET 10 SDK is required. An older Visual Studio can be bypassed by running this script in PowerShell.' }
    Write-Host 'Database connection: 1 = installed LocalDB, 2 = custom SQL Server connection'
    $choice = Read-Host 'Choose 1 or 2'
    if ($choice -eq '1') { $connection = 'Server=(localdb)\MSSQLLocalDB;Database=GoSound;Trusted_Connection=True;TrustServerCertificate=True;' }
    elseif ($choice -eq '2') {
        $secure = Read-Host 'Enter your local connection string (hidden input)' -AsSecureString
        $connection = [System.Net.NetworkCredential]::new('', $secure).Password
    } else { throw 'Invalid choice.' }
    $env:ConnectionStrings__DefaultConnection = $connection
    $env:ASPNETCORE_ENVIRONMENT = 'Development'
    dotnet restore GoSound.slnx
    if ($LASTEXITCODE -ne 0) { throw 'Restore failed.' }
    dotnet tool restore
    if ($LASTEXITCODE -ne 0) { throw 'Tool restore failed.' }
    dotnet ef database update --project GoSound/GoSound.csproj
    if ($LASTEXITCODE -ne 0) { throw 'Database setup failed. Check whether SQL Server is running and the connection is correct.' }
    dotnet build GoSound.slnx
    if ($LASTEXITCODE -ne 0) { throw 'Build failed.' }
    Write-Host 'Open http://localhost:5180 after the app starts. Ctrl+C stops the app.'
    Start-Process 'http://localhost:5180'
    dotnet run --project GoSound/GoSound.csproj
} finally {
    Remove-Item Env:ConnectionStrings__DefaultConnection -ErrorAction SilentlyContinue
    Pop-Location
}
