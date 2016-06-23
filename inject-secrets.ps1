$azureConn = get-content secrets
$filesToInject = gci -Exclude secrets -Recurse -Filter *.config | where-object { -not ($_.FullName -like "*\bin\*") } | select-string -pattern "Endpoint=" | group path | select name

Write-Information "$($filesToInject.count) to be injected with secret"
$filesToInject | % { Write-Information $_.Name }
	
foreach ($file in (get-item $filesToInject.Name))
{
    (Get-Content $file.PSPath) |
    Foreach-Object { $_ -replace 'value="Endpoint=sb.*"', "value=""$azureConn""" } |
    Set-Content $file.PSPath
}