function ClearMigrations([string[]]$paths) {
    $paths.ForEach({
            Remove-Item -Path $_ -Recurse -Force -ErrorAction SilentlyContinue
        })
}
function ClearDb([string]$path, [string]$project, [string]$startup_project, [string]$context) {
     Set-Location $path
    dotnet ef database drop --project $project --startup-project $startup_project --context $context --force
}
function UpdateDb([string]$pathMigration,[string]$pathDbUpdate, [string]$migrationName, [string]$project, [string]$startup_project, [string]$context) {
    Set-Location $pathMigration
    dotnet ef migrations add migrationName --project $project --startup-project $startup_project --context $context
    Set-Location $pathDbUpdate
    dotnet ef database update --context $context
}

ClearMigrations -paths @("D:\Profiles\Marcus\Desktop\Add-Nsti\back\Authentication\Migrations", "D:\Profiles\Marcus\Desktop\Add-Nsti\back\Repository\Migrations")
ClearDb -path "D:\Profiles\Marcus\Desktop\Add-Nsti\back" -project "Authentication" -startup_project "Api" -context "IdImDbContext"
ClearDb -path "D:\Profiles\Marcus\Desktop\Add-Nsti\back"  -project "Repository" -startup_project "Api" -context "ImsystemDbContext"
UpdateDb -pathMigration "D:\Profiles\Marcus\Desktop\Add-Nsti\back" -pathDbUpdate "D:\Profiles\Marcus\Desktop\Add-Nsti\back\Api" -migrationName "AAAA" -project "Authentication" -startup_project "Api" -context "IdImDbContext"
UpdateDb -pathMigration "D:\Profiles\Marcus\Desktop\Add-Nsti\back" -pathDbUpdate "D:\Profiles\Marcus\Desktop\Add-Nsti\back\Api" -migrationName "BBBB" -project "Repository" -startup_project "Api" -context "ImsystemDbContext"

Set-Location "D:\Profiles\Marcus\Desktop\Add-Nsti\back\api"
dotnet watch run