import-module webadministration

$WebSiteName = "GriDev.us (local.) (Scheduler)" # your web site name
$WebSiteFullName = "IIS:\Sites\" + $WebSiteName
$ApplicationPool = Get-Item $WebSiteFullName | Select-Object applicationPool
$ApplicationPoolFullName = "IIS:\AppPools\" + $ApplicationPool.applicationPool

Set-WebConfiguration -filter '/system.applicationHost/serviceAutoStartProviders' -value (@{name="ApplicationPreload";type="Clarity.Ecommerce.Scheduler.ApplicationPreload, Clarity.Ecommerce.Scheduler"})
set-itemproperty $WebSiteFullName -name applicationDefaults.serviceAutoStartEnabled -value True
set-itemproperty $WebSiteFullName -name applicationDefaults.serviceAutoStartProvider -value 'ApplicationPreload'
set-itemproperty $ApplicationPoolFullName -name autoStart -value True
set-itemproperty $ApplicationPoolFullName -name startMode -value 1 #1 = AlwaysRunning, 0 = OnDemand
