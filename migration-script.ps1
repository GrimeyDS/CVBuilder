$startUpProjectName = "CVBuilder"

$projectFiles = Get-ChildItem -Path "." -Recurse -Filter "*.csproj" | Select-Object Name, FullName
$projects = @()
foreach ($projectFile in $projectFiles) {
    $dbContextNames = Get-ChildItem -Path ("$(Split-Path -Path $projectFile.FullName -Parent)") -Recurse -Filter "*DbContext.cs" | Select-Object -ExpandProperty Name
    
    if ($dbContextNames.Count -eq 0) {
        continue;
    }

    $projects += [PSCustomObject]@{
        Name = $projectFile.Name -replace '.csproj$', ''
        DbContextNames  = $dbContextNames -replace '.cs$', ''
    }
}

foreach ($project in $projects) {
    $projectName = $project.Name
    foreach ($dbContextName in $project.DbContextNames) {
        ("Updating '$($projectName) - $dbContextName'")
        dotnet ef database update -p $projectName -s $startUpProjectName -c $dbContextName
    }
}
