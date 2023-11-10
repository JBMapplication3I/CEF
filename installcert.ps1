param
(
  [string]$iisSite = "ClarityClient.com (clarity-local.)",
  [string]$hostName = "clarity-local.clarityclient.com",
  [string]$certificateName = "*.clarityclient.com"
)
$thumbprint = Get-ChildItem -path cert:\LocalMachine\My | where { $_.Subject.StartsWith("CN=$certificateName") } | Select-Object -Expand Thumbprint
$guid = [guid]::NewGuid().ToString('B')
netsh http add sslcert hostnameport="${hostname}:443" certhash=$thumbprint certstorename=MY appid="$guid"
New-WebBinding -name $iisSite -Protocol https -HostHeader $hostName -Port 443 -SslFlags 1
