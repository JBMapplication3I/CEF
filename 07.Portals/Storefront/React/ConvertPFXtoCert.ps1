Write-Host "Creating https certificate"

$certificate = New-SelfSignedCertificate -certstorelocation cert:\localmachine\my -dnsname *.clarityclient.com
$password = "cl41TyCl13nt!"
$securePassword = ConvertTo-SecureString -String $password -Force -AsPlainText

$pfxPath = "./clarityclient.com.pfx"
$outPath = "./node_modules/webpack-dev-server/ssl/server.pem"
Export-PfxCertificate -Cert $certificate -FilePath $pfxPath -Password $securePassword | Out-Null
Import-PfxCertificate -Password $securePassword -FilePath $pfxPath -CertStoreLocation Cert:\LocalMachine\Root | Out-Null

$keyPath = "./clarityclient.com-key.pem"
$certPath = "./clarityclient.com.pem"

openssl pkcs12 -in $pfxPath -nocerts -out $keyPath -nodes -passin pass:$password
openssl pkcs12 -in $pfxPath -nokeys -out $certPath -nodes -passin pass:$password

$key = Get-Content ./clarityclient.com-key.pem
$cert = Get-Content ./clarityclient.com.pem
$key + $cert | Out-File $outPath -Encoding ASCII

Write-Host "Https certificate written to $outPath"