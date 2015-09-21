$username = "yllemo2"
$password = "CHANGE_PASSWORD"

$basicAuth = ("{0}:{1}" -f $username,$password)
$basicAuth = [System.Text.Encoding]::UTF8.GetBytes($basicAuth)
$basicAuth = [System.Convert]::ToBase64String($basicAuth)
$headers = @{Authorization=("Basic {0}" -f $basicAuth)}

Invoke-RestMethod -Uri https://yllemo.visualstudio.com/defaultcollection/_apis/wit/workitems/1 -headers $headers -Method Get