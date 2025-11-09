clear
$appPath = "\desktop\Add-Nsti"
$backApiPath = "back\api"
$backEndPath = "back"


$app = Join-Path -Path $env:USERPROFILE -ChildPath $appPath
$api = Join-Path -Path $app -ChildPath $backApiPath
$back  = Join-Path -Path $app -ChildPath $backEndPath

$systemMigration = Join-Path $back -ChildPath  "Repository\Migrations"
$idMigration = Join-Path $back -ChildPath "Authentication\Migrations"


function ClearMigrations([string[]]$paths) {
    $paths.ForEach({
    write-host($_)
            Remove-Item -Path $_ -Recurse -Force
            #Remove-Item -Path $_ -Recurse -Force -ErrorAction SilentlyContinue
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

ClearMigrations -paths @($systemMigration, $idMigration)

ClearDb -path $back -project "Authentication" -startup_project "Api" -context "IdImDbContext"
ClearDb -path $back -project "Repository" -startup_project "Api" -context "ImsystemDbContext"

UpdateDb -pathMigration $back -pathDbUpdate $api -migrationName "AAAA" -project "Authentication" -startup_project "Api" -context "IdImDbContext"
UpdateDb -pathMigration $back -pathDbUpdate $api -migrationName "BBBB" -project "Repository" -startup_project "Api" -context "ImsystemDbContext"

Set-Location $api

dotnet watch run