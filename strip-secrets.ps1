$azureConn = get-content secrets
$connParts = $azureConn.split(";").split("=") | Where-Object { $_.Trim() -ne '' }
$azureKey = $connParts | select -last 1
$azureNamespace =  $connParts | select -index 1

$filesTostrip = gci -Exclude secrets -Recurse | where-object { -not ($_.FullName -like "*\bin\*") } | select-string -pattern $azureKey | group path | select name

Write-Information "$($filesToInject.count) files to be stripped from secret"
    
if($filesTostrip.count -eq 0)
{
    return;
}
$filesTostrip | % { Write-Information $_.Name }
foreach ($file in (get-item $filesTostrip.Name))
{
    (Get-Content $file.PSPath) | 
    % { $_ -replace "$azureKey", "[secret]" } |
    % { $_ -replace "$azureNamespace", "sb://[namespace].servicebus.windows.net/" } |
    Set-Content $file.PSPath
}