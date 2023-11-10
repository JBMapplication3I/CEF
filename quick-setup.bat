@echo off
SETLOCAL
TITLE CEF Standup Script
SET STEPNUM=0
ECHO.
ECHO [32m==========================================================================[0m
ECHO [32m[1mDISCLAIMER:[0m[32m This script is for internal [1mClarity Ventures, Inc.[0m[32m use only.[0m
ECHO [32mIt should not be given to clients or used in production servers.          [0m
ECHO [32m==========================================================================[0m
ECHO [32m// TODO: Detect VS Version (2019+) automatically[0m
ECHO [32m// TODO: Add setting to choose to include Brand, Store and Vendor Admin standups[0m
ECHO [32m// TODO: Convert this batch file to an executable[0m
ECHO [32m// TODO: Make the batch file idempotent to upgrade across all versions to latest, like EF does[0m
ECHO [32m// TODO: DNN DB: Add domain to portal aliases table[0m
ECHO [32m// TODO: DNN DB: Change skin folder name in Tabs table[0m
ECHO ==========================================================================
ECHO [1mBefore Starting, make sure to get the [1;93mquick-install-settings.bat[0m[1m file from[0m
ECHO [1mthe [1;93mCEF-Configs[0m[1m repo and populate it with settings values. You can do this[0m
ECHO [1mnow and afterward still continue this script.[0m
ECHO     [36mHINT: It goes in the [33mSolution Items[0m[36m folder like other settings.[0m
PAUSE
ECHO ==========================================================================
ECHO [1mStarting standup script...[0m
ECHO.
SET "CEFDIR=%~dp0"
SET "dir=%cd%"
:dropChar
SET "dir=%dir:~0,-1%"
IF "%dir:~-1%"=="\" (
  GOTO charDropped
) ELSE (
  GOTO dropChar
)
:charDropped
FOR /f %%a IN ("%cd%") DO SET "dir=%%~dpa"
SET "PARENTDIR=%dir%"
SET "DATAMODELPATH=%CEFDIR%01.DataAccessLayer\01.Clarity.Ecommerce.DataModel"
SET "SOLITMDIR=%CEFDIR%Solution Items\"
SET "WEBDIR=%PARENTDIR%WEB9"
SET "PORTALSDIR=%WEBDIR%\Portals\_default\Skins"
SET "IMGDIR=%PARENTDIR%Images"
SET "appDir=%ProgramFiles%\Microsoft Visual Studio\2022\"
IF EXIST "%appDir%Enterprise\" (
  SET "MsBuildPath=%appDir%Enterprise\MSBuild\Current\Bin\"
) ELSE (
  SET "MsBuildPath=%appDir%Professional\MSBuild\Current\Bin\"
)
ECHO [1mMSBuild Path          [0m %MsBuildPath%
CALL ".\Solution Items\quick-install-settings.bat"
IF "%DOCORS%" NEQ "0" (
SET "MVCDirAPI=%PARENTDIR%API"
SET "MVCDirAdmin=%PARENTDIR%%NamePartMVCAdmin%"
SET "MVCDirBrandAdmin=%PARENTDIR%%NamePartMVCBrandAdmin%"
SET "MVCDirStoreAdmin=%PARENTDIR%%NamePartMVCStoreAdmin%"
SET "MVCDirVendorAdmin=%PARENTDIR%%NamePartMVCVendorAdmin%"
)
ECHO [1mCEF Version:          [0m %VERSION%
ECHO [1mCEF Dir:              [0m %CEFDIR%
ECHO [1mParent Dir:           [0m %PARENTDIR%
ECHO [1mSolution Items Dir:   [0m %SOLITMDIR%
ECHO [1mWEB9 Dir:             [0m %WEBDIR%
IF "%DOCORS%" NEQ "0" (
ECHO [1mMVC API Dir:          [0m %MVCDirAPI%
ECHO [1mMVC Admin Dir:        [0m %MVCDirAdmin%
ECHO [1mMVC Brand Admin Dir:  [0m %MVCDirBrandAdmin%
ECHO [1mMVC Store Admin Dir:  [0m %MVCDirStoreAdmin%
ECHO [1mMVC Vendor Admin Dir: [0m %MVCDirVendorAdmin%
)
ECHO [1mPortals Dir:          [0m %PORTALSDIR%
ECHO [1mImages Dir:           [0m %IMGDIR%
ECHO     [36mHINT: "..\" means go up one directory, relative to the current. This[0m
ECHO           [36mcan be repeated.[0m
REM setx PYTHON "%UserProfile%\.windows-build-tools\python27\python.exe" /M 2> nul
IF "%RESUME%" NEQ "0" (
  ECHO ==========================================================================
  ECHO Resuming at step %RESUME%
  ECHO ==========================================================================
  GOTO %RESUME%
)
:0
ECHO ==========================================================================
:1
SET STEPNUM=1
ECHO [1mSTEP %STEPNUM%.[0m Git checkout the [1;93mCEF-Configs[0m repo to [1;93mC:\CEF-Configs[0m.
ECHO     [36mHINT: You may need to checkout to a folder like [33mC:\Data[0m[36m where you have[0m
ECHO     [36m      permissions first and then move it to [33mC:\[0m[36m.[0m
ECHO     [36mHINT: If you already have it, just pull latest in it[0m
PAUSE
ECHO ==========================================================================
:2
SET STEPNUM=2
ECHO [1mSTEP %STEPNUM%.[0m Since you only need one checkout of [1;93mCEF-Configs[0m repo for the box
ECHO         it is in, remove other copies from the box.
ECHO [101;93m     WARNING: NEVER LEAVE [4mCEF-Configs[0m[101;93m ON A CLIENT OR PRODUCTION BOX!      [0m
PAUSE
ECHO ==========================================================================
:3
SET STEPNUM=3
ECHO [1mSTEP %STEPNUM%.[0m Git checkout either the core [93mDNN9-CEF[0m repo or the [93mClient DNN[0m
ECHO         repo you want to use to the [93mWEB9 Dir[0m shown above.
ECHO     [36mHINT: If you already have it, just pull latest in it.[0m
PAUSE
ECHO ==========================================================================
:4
SET STEPNUM=4
ECHO [1mSTEP %STEPNUM%.[0m If you have a website in [1mIIS[0m named
ECHO         [93m%SiteName%[0m
ECHO         and/or any accompanying [1mApp Pools[0m, remove/delete them.
PAUSE
ECHO ==========================================================================
:5
SET STEPNUM=5
ECHO [1mSTEP %STEPNUM%.[0m Create a new/empty/blank database at:
ECHO   [1;93m%CEFConnString%[0m
ECHO     [91mWARNING: If you already have one and it is already populated with[0m
ECHO              [91mschema from master, it should be fine. But if it has[0m
ECHO              [91mconflicting migrations from a client project, this setup will[0m
ECHO              [91mfail.[0m
PAUSE
ECHO ==========================================================================
:6
SET STEPNUM=6
ECHO [1mSTEP %STEPNUM%.[0m Restore a backup of a standard [1mDNN9[0m for [1mCEF %VERSION%[0m database or
ECHO         the [1mDNN9[0m for the [1mClient[0m at:
ECHO   [1;93m%DNNConnString%[0m
PAUSE
ECHO ==========================================================================
:7
SET STEPNUM=7
ECHO [96mSTEP %STEPNUM% (AUTO): Create the [93mImages[96m directory...[0m
mkdir "%IMGDIR%" 2> nul
ECHO [96mSTEP %STEPNUM% (AUTO): ...Done[0m
ECHO ==========================================================================
:8
SET STEPNUM=8
ECHO [96mSTEP %STEPNUM% (AUTO): Refresh the CEF folder with git ensuring its totally[0m
ECHO [96m                 reset for the script.[0m
ECHO [32m-----%STEPNUM%a.[0m "cd "%CEFDIR%""
cd "%CEFDIR%"
ECHO [32m-----%STEPNUM%b. (SKIPPED)[0m "git.exe reset --hard"
REM git.exe reset --hard 2> nul
ECHO [32m-----%STEPNUM%c. (SKIPPED)[0m "git.exe clean -fdx"
REM git.exe clean -fdx 2> nul
ECHO [32m-----%STEPNUM%d.[0m "git.exe fetch -v --progress --all"
git.exe fetch -v --progress --all 2> nul
ECHO [32m-----%STEPNUM%c.[0m "git.exe remote update --prune"
git.exe remote update --prune 2> nul
REM ECHO [32m-----%STEPNUM%d.[0m "git.exe pull --progress -v --no-rebase "origin" master"
REM IF /I "%SKIP_RELEASEPULL%" EQU "1" (
REM ECHO [32m-----%STEPNUM%d.[0m Skipping this per setting value
REM ) else (
REM git.exe pull --progress -v --no-rebase "origin" master 2> nul
REM )
ECHO [96mSTEP %STEPNUM% (AUTO): ...Done[0m
ECHO ==========================================================================
:9
SET STEPNUM=9
ECHO [96mSTEP %STEPNUM% (AUTO): Refresh the WEB9 folder with git ensuring its totally[0m
ECHO [96m                 reset for the script.[0m
ECHO [32m-----%STEPNUM%a.[0m "cd "%WEBDIR%""
cd "%WEBDIR%"
ECHO [32m-----%STEPNUM%b. (SKIPPED)[0m "git.exe reset --hard"
REM git.exe reset --hard 2> nul
ECHO [32m-----%STEPNUM%c. (SKIPPED)[0m "git.exe clean -fdx"
REM git.exe clean -fdx 2> nul
ECHO [32m-----%STEPNUM%d.[0m "git.exe fetch -v --progress --all"
git.exe fetch -v --progress --all 2> nul
ECHO [32m-----%STEPNUM%c.[0m "git.exe remote update --prune"
git.exe remote update --prune 2> nul
REM IF /I "%SKIP_RELEASEPULL%" EQU "1" (
REM ECHO [32m-----%STEPNUM%d.[0m Skipping this per setting value
REM ) else (
REM ECHO [32m-----%STEPNUM%d.[0m "git.exe pull --progress -v --no-rebase "origin" master"
REM git.exe pull --progress -v --no-rebase "origin" master 2> nul
REM )
ECHO [96mSTEP %STEPNUM% (AUTO): ...Done[0m
ECHO ==========================================================================
:10
SET STEPNUM=10
ECHO [96mSTEP %STEPNUM% (AUTO): Read in [93mCEF Configs[0m[96m for [93mv%VERSION%[0m[96m...[0m
ECHO [32m-----%STEPNUM%a.[0m "cd "%CEFDIR%""
cd "%CEFDIR%"
ECHO -----
ECHO [32m-----%STEPNUM%b.[0m "xcopy "C:\CEF-Configs\%VERSION%\QI\CEF" "%CEFDIR%" /Y /C /R /I /S"
xcopy "C:\CEF-Configs\%VERSION%\QI\CEF" "%CEFDIR%" /Y /C /R /I /S 2> nul
ECHO [96mSTEP %STEPNUM% (AUTO): ...Done[0m
ECHO ==========================================================================
:11
SET STEPNUM=11
ECHO [96mSTEP %STEPNUM% (AUTO): Read in [93mWEB9 Configs[0m[96m for [93mv%VERSION%[0m[96m...[0m
ECHO [32m-----%STEPNUM%a.[0m "cd "%WEBDIR%""
cd "%WEBDIR%"
ECHO [32m-----%STEPNUM%b.[0m "xcopy "C:\CEF-Configs\%VERSION%\QI\WEB9" "%WEBDIR%" /Y /C /R /I /S"
xcopy "C:\CEF-Configs\%VERSION%\QI\WEB9" "%WEBDIR%" /Y /C /R /I /S 2> nul
ECHO [96mSTEP %STEPNUM% (AUTO): ...Done[0m
ECHO ==========================================================================
:12
SET STEPNUM=12
ECHO [96mSTEP %STEPNUM% (AUTO): Replace CEF Connection Strings and other app settings in[0m
ECHO [96m                 config files...[0m
ECHO     [36mHINT: This may take a few seconds.[0m
ECHO [32m-----%STEPNUM%a.[0m "cd "%SOLITMDIR%""
cd "%SOLITMDIR%"
ECHO [32m-----%STEPNUM%b.[0m "replace strings"
powershell -Command "(Get-Content connectionStrings.config) | ForEach-Object { $_ -replace '{{DBSOURCE}}', '%DbSource%' } | Set-Content connectionStrings.config"
powershell -Command "(Get-Content connectionStrings.config) | ForEach-Object { $_ -replace '{{DBNAME}}', '%CEFDbName%' } | Set-Content connectionStrings.config"
powershell -Command "(Get-Content connectionStrings.config) | ForEach-Object { $_ -replace '{{AUTHKIND}}', '%AuthKind%' } | Set-Content connectionStrings.config"
powershell -Command "(Get-Content appSettings.config) | ForEach-Object { $_ -replace '{{PROJ_CAPS}}', '%PROJ_CAPS%' } | Set-Content appSettings.config"
powershell -Command "(Get-Content appSettings.config) | ForEach-Object { $_ -replace '{{proj_lower}}', '%proj_lower%' } | Set-Content appSettings.config"
powershell -Command "(Get-Content appSettings.config) | ForEach-Object { $_ -replace '{{Proj_Title}}', '%Proj_Title%' } | Set-Content appSettings.config"
powershell -Command "(Get-Content appSettings.config) | ForEach-Object { $_ -replace '{{env_lower}}', '%env_lower%' } | Set-Content appSettings.config"
powershell -Command "(Get-Content appSettings.config) | ForEach-Object { $_ -replace '{{Comp_Name_Title}}', '%Comp_Name_Title%' } | Set-Content appSettings.config"
powershell -Command "(Get-Content appSettings.config) | ForEach-Object { $_ -replace '{{Directory}}', '%PARENTDIR%' } | Set-Content appSettings.config"
powershell -Command "(Get-Content appSettings.config) | ForEach-Object { $_ -replace '{{MainDomain}}', '%MainDomain%' } | Set-Content appSettings.config"
powershell -Command "(Get-Content cef_gulp_config.json) | ForEach-Object { $_ -replace '{{SKIN}}', '%SkinName%' } | Set-Content cef_gulp_config.json"
powershell -Command "(Get-Content rewrite.rewriteMaps.config) | ForEach-Object { $_ -replace '{{proj_lower}}', '%proj_lower%' } | Set-Content rewrite.rewriteMaps.config"
powershell -Command "(Get-Content rewrite.rewriteMaps.config) | ForEach-Object { $_ -replace '{{env_lower}}', '%env_lower%' } | Set-Content rewrite.rewriteMaps.config"
powershell -Command "(Get-Content rewrite.rewriteMaps.config) | ForEach-Object { $_ -replace '{{MainDomain}}', '%MainDomain%' } | Set-Content rewrite.rewriteMaps.config"
ECHO [32m-----%STEPNUM%c.[0m "cd "%CEFDIR%07.Portals\Storefront\Skins\Clarity""
cd "%CEFDIR%07.Portals\Storefront\Skins\Clarity"
ECHO [32m-----%STEPNUM%d.[0m "replace strings"
powershell -Command "(Get-Content rewrite.rewriteMaps.config) | ForEach-Object { $_ -replace '{{proj_lower}}', '%proj_lower%' } | Set-Content rewrite.rewriteMaps.config"
powershell -Command "(Get-Content rewrite.rewriteMaps.config) | ForEach-Object { $_ -replace '{{env_lower}}', '%env_lower%' } | Set-Content rewrite.rewriteMaps.config"
powershell -Command "(Get-Content rewrite.rewriteMaps.config) | ForEach-Object { $_ -replace '{{MainDomain}}', '%MainDomain%' } | Set-Content rewrite.rewriteMaps.config"
IF /I "%SkinName%" NEQ "Clarity" (
ECHO [32m-----%STEPNUM%d1.[0m "copy files from Clarity to %SkinName%"
xcopy "%CEFDIR%07.Portals\Storefront\Skins\Clarity" "%CEFDIR%07.Portals\Storefront\Skins\%SkinName%" /Y /C /R /I /S 2> nul
)
ECHO [96mSTEP %STEPNUM% (AUTO): ...Done[0m
ECHO ==========================================================================
:13
SET STEPNUM=13
ECHO [96mSTEP %STEPNUM% (AUTO): Replace CEF Connection Strings and other app settings in[0m
ECHO [96m                 config files...[0m
ECHO     [36mHINT: This may take a few seconds.[0m
ECHO [32m-----%STEPNUM%a.[0m "cd "%WEBDIR%""
cd "%WEBDIR%"
ECHO [32m-----%STEPNUM%b.[0m "replace strings"
powershell -Command "(Get-Content web.connectionStrings.config) | ForEach-Object { $_ -replace '{{DBSOURCE}}', '%DbSource%' } | Set-Content web.connectionStrings.config"
powershell -Command "(Get-Content web.connectionStrings.config) | ForEach-Object { $_ -replace '{{DBNAME}}', '%DNNDbName%' } | Set-Content web.connectionStrings.config"
powershell -Command "(Get-Content web.connectionStrings.config) | ForEach-Object { $_ -replace '{{AUTHKIND}}', '%AuthKind%' } | Set-Content web.connectionStrings.config"
ECHO [96mSTEP %STEPNUM% (AUTO): ...Done[0m
ECHO ==========================================================================
:14
SET STEPNUM=14
ECHO [96mSTEP %STEPNUM% (AUTO): Create the skin symbolic links from CEF to WEB9...[0m
ECHO [32m-----%STEPNUM%a.[0m "cd "%PORTALSDIR%""
cd "%PORTALSDIR%"
ECHO [32m-----%STEPNUM%b.[0m "mklink /D "%SkinName%" "%CEFDIR%07.Portals\Storefront\Skins\%SkinName%""
mklink /D "%SkinName%" "%CEFDIR%07.Portals\Storefront\Skins\%SkinName%" 2> nul
ECHO [32m-----%STEPNUM%c.[0m "mklink /D "Clarity-Admin" "%CEFDIR%07.Portals\Storefront\Skins\Clarity-Admin""
mklink /D "Clarity-Admin" "%CEFDIR%07.Portals\Storefront\Skins\Clarity-Admin" 2> nul
ECHO [96mSTEP %STEPNUM% (AUTO): ...Done[0m
ECHO ==========================================================================
:15
SET STEPNUM=15
ECHO [96mSTEP %STEPNUM% (AUTO): Do NPM Install for Admin...[0m
ECHO     [36mHINT: This may take a minute or two.[0m
ECHO [32m-----%STEPNUM%a.[0m "cd "%CEFDIR%07.Portals\Admin\AngJS""
cd "%CEFDIR%07.Portals\Admin\AngJS"
ECHO [32m-----%STEPNUM%b.[0m "npm i -g gulp && npm install"
IF /I "%SKIP_NPM_INSTALLS%" EQU "1" (
  ECHO [32m-----%STEPNUM%b.[0m Skipping npm installs
) else (
  CMD /C "npm i -g gulp && npm install"
)
ECHO [32m-----%STEPNUM%c.[0m "gulp build"
CMD /C "gulp build"
ECHO [32m-----%STEPNUM%d.[0m "gulp build:minify"
CMD /C "gulp build:minify"
ECHO [96mSTEP %STEPNUM% (AUTO): ...Done[0m
ECHO ==========================================================================
:16
SET STEPNUM=16
ECHO [96mSTEP %STEPNUM% (AUTO): Do NPM Install for Storefront AngJS...[0m
ECHO     [36mHINT: This may take a minute or two.[0m
ECHO [32m-----%STEPNUM%a.[0m "cd "%CEFDIR%07.Portals\Storefront\AngJS""
cd "%CEFDIR%07.Portals\Storefront\AngJS"
IF /I "%SKIP_NPM_INSTALLS%" EQU "1" (
  ECHO [32m-----%STEPNUM%b.[0m Skipping npm installs
) else (
  IF /I "%SKIP_NPMBUILDTOOLS%" EQU "1" (
    ECHO [32m-----%STEPNUM%b.[0m "npm i -g gulp && npm install && npm rebuild node-sass"
    ECHO [32mSkipping Windows Build Tools[0m
    CMD /C "npm i -g gulp && npm install && npm rebuild node-sass"
  ) ELSE (
    ECHO [32m-----%STEPNUM%b.[0m "npm i -g --production windows-build-tools && npm i -g gulp && npm install && npm rebuild node-sass"
    CMD /C "npm i -g --production windows-build-tools && npm i -g gulp && npm install && npm rebuild node-sass"
  )
)
ECHO [32m-----%STEPNUM%c.[0m "gulp build"
CMD /C "gulp build"
ECHO [32m-----%STEPNUM%d.[0m "gulp build:minify"
CMD /C "gulp build:minify"
ECHO [96mSTEP %STEPNUM% (AUTO): ...Done[0m
ECHO ==========================================================================
:187
SET STEPNUM=17
ECHO [96mSTEP %STEPNUM% (AUTO): Do NPM Install for Storefront React...[0m
ECHO     [36mHINT: This may take a minute or two.[0m
ECHO [32m-----%STEPNUM%a.[0m "cd "%CEFDIR%07.Portals\Storefront\React""
cd "%CEFDIR%07.Portals\Storefront\React"
ECHO [32m-----%STEPNUM%b.[0m "npm install"
IF /I "%SKIP_NPM_INSTALLS%" EQU "1" (
  ECHO [32m-----%STEPNUM%b.[0m Skipping npm installs
) else (
  CMD /C "npm install"
)
ECHO [32m-----%STEPNUM%c.[0m "npm run build"
CMD /C "npm run build"
ECHO [96mSTEP %STEPNUM% (AUTO): ...Done[0m
ECHO ==========================================================================
:18
SET STEPNUM=18
ECHO [96mSTEP %STEPNUM% (AUTO): Pull NuGet 5.8.0 locally...[0m
ECHO [32m-----%STEPNUM%a.[0m "cd "%CEFDIR%""
cd "%CEFDIR%"
ECHO [32m-----%STEPNUM%b.[0m "mkdir ".nuget""
mkdir ".nuget" 2> nul
ECHO [32m-----%STEPNUM%c.[0m "curl "https://dist.nuget.org/win-x86-commandline/v5.8.0/nuget.exe" --output ".nuget/nuget.exe""
curl "https://dist.nuget.org/win-x86-commandline/v5.8.0/nuget.exe" --output ".nuget/nuget.exe"
ECHO [96mSTEP %STEPNUM% (AUTO): ...Done[0m
ECHO ==========================================================================
:19
SET STEPNUM=19
ECHO [96mSTEP %STEPNUM% (AUTO): Use NuGet to Restore Packages...[0m
ECHO     [36mHINT: This may take a minute or two.[0m
ECHO [32m-----%STEPNUM%a.[0m "cd "%CEFDIR%""
cd "%CEFDIR%"
ECHO [32m-----%STEPNUM%b.[0m "".nuget/nuget.exe" restore %SLNNAME%"
".nuget/nuget.exe" restore %SLNNAME% > restore1.log
IF %ERRORLEVEL% NEQ 0 (
ECHO. [101;93mUh oh, something bad happened. Check restore1.log for errors[0m
GOTO END
)
ECHO [32m-----%STEPNUM%c.[0m ""%MsBuildPath%MSBuild.exe" %SLNNAME% /p:Configuration="Debug" /p:Platform="Any CPU" /t:restore"
"%MsBuildPath%MSBuild.exe" %SLNNAME% /p:Configuration="Debug" /p:Platform="Any CPU" /t:restore > restore2.log
IF %ERRORLEVEL% NEQ 0 (
ECHO. [101;93mUh oh, something bad happened. Check restore2.log for errors[0m
GOTO END
)
ECHO [96mSTEP %STEPNUM% (AUTO): ...Done[0m
ECHO ==========================================================================
:20
SET STEPNUM=20
ECHO [96mSTEP %STEPNUM% (AUTO): Use MSBuild to compile the solution...[0m
ECHO     [36mHINT: This may take a few minutes, a long pause at [33m19b[36m is normal.[0m
ECHO     [36mHINT: A [33mbuild.log[36m file is being written to with the live progress.[0m
ECHO [32m-----%STEPNUM%a.[0m "cd "%CEFDIR%""
cd "%CEFDIR%"
ECHO [32m-----%STEPNUM%b.[0m ""%MsBuildPath%MSBuild.exe" %SLNNAME% /p:Configuration="Debug" /p:Platform="Any CPU""
"%MsBuildPath%MSBuild.exe" %SLNNAME% /p:Configuration="Debug" /p:Platform="Any CPU" > build.log
IF %ERRORLEVEL% NEQ 0 (
ECHO. [101;93mUh oh, something bad happened. Check build.log for errors[0m
GOTO END
)
ECHO [96mSTEP %STEPNUM% (AUTO): ...Done[0m
ECHO ==========================================================================
:21
SET STEPNUM=21
ECHO [96mSTEP %STEPNUM% (AUTO): Use EF6 to set up the CEF DB schema...[0m
ECHO     [36mHINT: This may take a few minutes, a long pause is normal.[0m
ECHO     [36mHINT: A [33mschema.log[36m file is being written to with the live progress.[0m
"%CEFDIR%packages\EntityFramework.6.4.4\tools\net45\win-x86\ef6.exe" database update -v ^
 --project-dir "%DATAMODELPATH%" -a "%DATAMODELPATH%\bin\Debug\net472\Clarity.Ecommerce.DataModel.dll" ^
 --config "%DATAMODELPATH%\app.config" > schema.log
ECHO [96mSTEP %STEPNUM% (AUTO): ...Done[0m
IF %ERRORLEVEL% NEQ 0 (
ECHO [101;93m        Uh oh, something bad happened. Check schema.log for errors        [0m
GOTO END
)
ECHO [103;91m  WARNING: This does not inject [4mSeed Data[0m[103;91m, use the Unit Test to do that!  [0m
ECHO ==========================================================================
:22
SET STEPNUM=22
ECHO [96mSTEP %STEPNUM% (AUTO): Propogate IIS...[0m
ECHO [32m-----%STEPNUM%a.[0m "cd C:\Windows\System32\inetsrv"
cd C:\Windows\System32\inetsrv
IF "%DOCORS%" NEQ "0" (
ECHO [32m-----%STEPNUM%b.[0m Create each app pool for all portals
appcmd add apppool /name:"%AppPoolNameDNN%"
appcmd add apppool /name:"%SiteName% (%NamePartAPIMVC%)"
appcmd add apppool /name:"%SiteName% (%NamePartAPIAdmin%)"
appcmd add apppool /name:"%SiteName% (%NamePartMVCAdmin%)"
appcmd add apppool /name:"%SiteName% (%NamePartAPIBrandAdmin%)"
appcmd add apppool /name:"%SiteName% (%NamePartMVCBrandAdmin%)"
appcmd add apppool /name:"%SiteName% (%NamePartAPIStoreAdmin%)"
appcmd add apppool /name:"%SiteName% (%NamePartMVCStoreAdmin%)"
appcmd add apppool /name:"%SiteName% (%NamePartAPIVendorAdmin%)"
appcmd add apppool /name:"%SiteName% (%NamePartMVCVendorAdmin%)"
appcmd add apppool /name:"%SiteName% (%NamePartAPIStorefront%)"
appcmd add apppool /name:"%SiteName% (%NamePartScheduler%)"
ECHO [32m-----%STEPNUM%c.[0m Add the root sites to IIS
appcmd add site /name:"%SiteNameAPI%" /bindings:http://%DomainPartAPI%%FullDomain%:80 /physicalPath:"%MVCDirAPI%"
appcmd add site /name:"%SiteNameMain%" /bindings:http://%DomainPartMain%%FullDomain%:80 /physicalPath:"%WEBDIR%"
appcmd add site /name:"%SiteNameAdmin%" /bindings:http://%DomainPartAdmin%%FullDomain%:80 /physicalPath:"%MVCDirAdmin%"
appcmd add site /name:"%SiteNameBrandAdmin%" /bindings:http://%DomainPartBrandAdmin%%FullDomain%:80 /physicalPath:"%MVCDirBrandAdmin%"
appcmd add site /name:"%SiteNameStoreAdmin%" /bindings:http://%DomainPartStoreAdmin%%FullDomain%:80 /physicalPath:"%MVCDirStoreAdmin%"
appcmd add site /name:"%SiteNameVendorAdmin%" /bindings:http://%DomainPartVendorAdmin%%FullDomain%:80 /physicalPath:"%MVCDirVendorAdmin%"
ECHO [32m-----%STEPNUM%d.[0m Set the App Pools on the root sites in IIS
appcmd set site /site.name:"%SiteNameAPI%" /[path='/'].applicationPool:"%SiteName% (%NamePartAPIMVC%)"
appcmd set site /site.name:"%SiteNameMain%" /[path='/'].applicationPool:"%AppPoolNameDNN%"
appcmd set site /site.name:"%SiteNameAdmin%" /[path='/'].applicationPool:"%SiteName% (%NamePartMVCAdmin%)"
appcmd set site /site.name:"%SiteNameBrandAdmin%" /[path='/'].applicationPool:"%SiteName% (%NamePartMVCBrandAdmin%)"
appcmd set site /site.name:"%SiteNameStoreAdmin%" /[path='/'].applicationPool:"%SiteName% (%NamePartMVCStoreAdmin%)"
appcmd set site /site.name:"%SiteNameVendorAdmin%" /[path='/'].applicationPool:"%SiteName% (%NamePartMVCVendorAdmin%)"
ECHO [32m-----%STEPNUM%e.[0m Add the Virtual Applications to the shared API site
appcmd add app /site.name:"%SiteNameAPI%" /path:"/%NamePartAPIStorefront%" /physicalPath:"%CEFDIR%07.Portals\Storefront\Service"
appcmd add app /site.name:"%SiteNameAPI%" /path:"/%NamePartAPIAdmin%" /physicalPath:"%CEFDIR%07.Portals\Admin\Service"
appcmd add app /site.name:"%SiteNameAPI%" /path:"/%NamePartAPIBrandAdmin%" /physicalPath:"%CEFDIR%07.Portals\BrandAdmin\Service"
appcmd add app /site.name:"%SiteNameAPI%" /path:"/%NamePartAPIStoreAdmin%" /physicalPath:"%CEFDIR%07.Portals\StoreAdmin\Service"
appcmd add app /site.name:"%SiteNameAPI%" /path:"/%NamePartAPIVendorAdmin%" /physicalPath:"%CEFDIR%07.Portals\VendorAdmin\Service"
appcmd add app /site.name:"%SiteNameAPI%" /path:"/%NamePartScheduler%" /physicalPath:"%CEFDIR%07.Portals\Scheduler\Scheduler"
ECHO [32m-----%STEPNUM%f.[0m Set the App Pools on these virtual applications in IIS
appcmd set site /site.name:"%SiteNameAPI%" /[path='/%NamePartAPIStorefront%'].applicationPool:"%SiteName% (%NamePartAPIStorefront%)"
appcmd set site /site.name:"%SiteNameAPI%" /[path='/%NamePartAPIAdmin%'].applicationPool:"%SiteName% (%NamePartAPIAdmin%)"
appcmd set site /site.name:"%SiteNameAPI%" /[path='/%NamePartAPIBrandAdmin%'].applicationPool:"%SiteName% (%NamePartAPIBrandAdmin%)"
appcmd set site /site.name:"%SiteNameAPI%" /[path='/%NamePartAPIStoreAdmin%'].applicationPool:"%SiteName% (%NamePartAPIStoreAdmin%)"
appcmd set site /site.name:"%SiteNameAPI%" /[path='/%NamePartAPIVendorAdmin%'].applicationPool:"%SiteName% (%NamePartAPIVendorAdmin%)"
appcmd set site /site.name:"%SiteNameAPI%" /[path='/%NamePartScheduler%'].applicationPool:"%SiteName% (%NamePartScheduler%)"
ECHO [32m-----%STEPNUM%g.[0m Add the Virtual Directories to the shared API site
appcmd add vdir /app.name:"%SiteNameAPI%/" /path:"/%NamePartUIStorefront%" /physicalPath:"%CEFDIR%07.Portals\Storefront\AngJS"
appcmd add vdir /app.name:"%SiteNameAPI%/" /path:"/%NamePartUIAdmin%" /physicalPath:"%CEFDIR%07.Portals\Admin\AngJS"
appcmd add vdir /app.name:"%SiteNameAPI%/" /path:"/Images/ecommerce" /physicalPath:"%IMGDIR%"
appcmd add vdir /app.name:"%SiteNameAPI%/" /path:"/%NamePartAPIReference%" /physicalPath:"%CEFDIR%07.Portals\APIReference\APIReference"
appcmd add vdir /app.name:"%SiteNameAPI%/" /path:"/Skins" /physicalPath:"%CEFDIR%07.Portals\Storefront\Skins"
ECHO [32m-----%STEPNUM%h.[0m Append the root sites to the hosts file
(ECHO. && ECHO 127.0.0.1			%DomainPartAPI%%FullDomain% %DomainPartMain%%FullDomain% %DomainPartAdmin%%FullDomain% %DomainPartBrandAdmin%%FullDomain% %DomainPartStoreAdmin%%FullDomain% %DomainPartVendorAdmin%%FullDomain%) >> %WINDIR%\System32\drivers\etc\hosts
) ELSE (
ECHO [32m-----%STEPNUM%b.[0m Create each app pool for all portals
appcmd add apppool /name:"%AppPoolNameDNN%"
appcmd add apppool /name:"%SiteName% (%NamePartAPIAdmin%)"
appcmd add apppool /name:"%SiteName% (%NamePartAPIStorefront%)"
appcmd add apppool /name:"%SiteName% (%NamePartScheduler%)"
ECHO [32m-----%STEPNUM%c.[0m Add the root sites to IIS
appcmd add site /name:"%SiteName%" /bindings:http://%FullDomain%:80 /physicalPath:"%WEBDIR%"
ECHO [32m-----%STEPNUM%d.[0m Set the App Pools on the root sites in IIS
appcmd set site /site.name:"%SiteName%" /[path='/'].applicationPool:"%AppPoolNameDNN%"
ECHO [32m-----%STEPNUM%e.[0m Add the Virtual Applications to the site
appcmd add app /site.name:"%SiteName%" /path:"%SubRoot%/%NamePartAPIStorefront%" /physicalPath:"%CEFDIR%07.Portals\Storefront\Service"
appcmd add app /site.name:"%SiteName%" /path:"%SubRoot%/%NamePartAPIAdmin%" /physicalPath:"%CEFDIR%07.Portals\Admin\Service"
appcmd add app /site.name:"%SiteName%" /path:"%SubRoot%/%NamePartScheduler%" /physicalPath:"%CEFDIR%07.Portals\Scheduler\Scheduler"
ECHO [32m-----%STEPNUM%f.[0m Set the App Pools on these virtual applications in IIS
appcmd set site /site.name:"%SiteName%" /[path='%SubRoot%/%NamePartAPIStorefront%'].applicationPool:"%SiteName% (%NamePartAPIStorefront%)"
appcmd set site /site.name:"%SiteName%" /[path='%SubRoot%/%NamePartAPIAdmin%'].applicationPool:"%SiteName% (%NamePartAPIAdmin%)"
appcmd set site /site.name:"%SiteName%" /[path='%SubRoot%/%NamePartScheduler%'].applicationPool:"%SiteName% (%NamePartScheduler%)"
ECHO [32m-----%STEPNUM%g.[0m Add the Virtual Directories to the shared API site
appcmd add vdir /app.name:"%SiteName%/" /path:"%SubRoot%/%NamePartUIStorefront%" /physicalPath:"%CEFDIR%07.Portals\Storefront\AngJS"
appcmd add vdir /app.name:"%SiteName%/" /path:"%SubRoot%/%NamePartUIAdmin%" /physicalPath:"%CEFDIR%07.Portals\Admin\AngJS"
appcmd add vdir /app.name:"%SiteName%/" /path:"/Images/ecommerce" /physicalPath:"%IMGDIR%"
appcmd add vdir /app.name:"%SiteName%/" /path:"%SubRoot%/%NamePartAPIReference%" /physicalPath:"%CEFDIR%07.Portals\APIReference\APIReference"
ECHO [32m-----%STEPNUM%h.[0m Append the root sites to the hosts file
(ECHO. && ECHO 127.0.0.1			%FullDomain%) >> %WINDIR%\System32\drivers\etc\hosts
)
ECHO [96mSTEP %STEPNUM% (AUTO): ...Done[0m
ECHO ==========================================================================
:23
ECHO [96mSTEP %STEPNUM% (AUTO): Install SSL Cert if available...[0m
SET STEPNUM=23
IF EXIST "%SSLCERTFILELOC%" (
  ECHO [32m-----%STEPNUM%a.[0m "cd C:\Windows\System32\inetsrv"
  cd C:\Windows\System32\inetsrv
  ECHO [32m-----%STEPNUM%b.[0m Installing SSL Cert from %SSLCERTFILELOC%
  certutil -p %SSLCERTFILEPW% -importPFX "%SSLCERTFILELOC%"
  IF "%DOCORS%" NEQ "0" (
    powershell -executionpolicy remotesigned -File "%CEFDIR%installcert.ps1" "%SiteNameAPI%" "%DomainPartAPI%%FullDomain%" "%SSLCERTNAME%"
    powershell -executionpolicy remotesigned -File "%CEFDIR%installcert.ps1" "%SiteNameMain%" "%DomainPartMain%%FullDomain%" "%SSLCERTNAME%"
    powershell -executionpolicy remotesigned -File "%CEFDIR%installcert.ps1" "%SiteNameAdmin%" "%DomainPartAdmin%%FullDomain%" "%SSLCERTNAME%"
    powershell -executionpolicy remotesigned -File "%CEFDIR%installcert.ps1" "%SiteNameBrandAdmin%" "%DomainPartBrandAdmin%%FullDomain%" "%SSLCERTNAME%"
    powershell -executionpolicy remotesigned -File "%CEFDIR%installcert.ps1" "%SiteNameStoreAdmin%" "%DomainPartStoreAdmin%%FullDomain%" "%SSLCERTNAME%"
    powershell -executionpolicy remotesigned -File "%CEFDIR%installcert.ps1" "%SiteNameVendorAdmin%" "%DomainPartVendorAdmin%%FullDomain%" "%SSLCERTNAME%"
  ) ELSE (
    powershell -executionpolicy remotesigned -File "%CEFDIR%installcert.ps1" "%SiteName%" "%FullDomain%" "%SSLCERTNAME%"
  )
  ECHO     [36mHINT: If the above generated error [33m183[36m or [33mCRYPT_E_EXISTS[36m, it can be ignored[0m
) ELSE (
  ECHO [32m-----%STEPNUM%a.[0m Not available
)
ECHO [96mSTEP %STEPNUM% (AUTO): ...Done[0m

PAUSE
:END
cd %CEFDIR%
ENDLOCAL
