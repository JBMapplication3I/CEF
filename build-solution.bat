@echo off
SETLOCAL
TITLE CEF Standup Script
ECHO.
ECHO [32m==========================================================================[0m
ECHO [32m[1mDISCLAIMER:[0m[32m This script is for internal [1mClarity Ventures, Inc.[0m[32m use only.[0m
ECHO [32mIt should not be given to clients or used in production servers.          [0m
ECHO [32m==========================================================================[0m
ECHO [32m// TODO: Detect VS Version (2019+) automatically[0m
ECHO [32m// TODO: Convert this batch file to an executable[0m
ECHO.
ECHO [1mStarting build script[0m
ECHO.
SET CEFDIR=%~dp0
SET dir=%cd%
:dropChar
SET dir=%dir:~0,-1%
IF "%dir:~-1%"=="\" (
  GOTO charDropped
) ELSE (
  GOTO dropChar
)
:charDropped
FOR /f %%a IN ("%cd%") DO SET dir=%%~dpa
SET PARENTDIR=%dir:~0,-1%
CALL ".\Solution Items\quick-install-settings.bat"
ECHO [1mCEF Version:       [0m %VERSION%
ECHO [1mCEF Dir:           [0m %CEFDIR%
ECHO [1mParent Dir:        [0m %PARENTDIR%
REM setx PYTHON "%UserProfile%\.windows-build-tools\python27\python.exe" /M 2> nul
IF EXIST "%ProgramFiles%" (
  SET "appDir=%ProgramFiles%\Microsoft Visual Studio\2022\"
) else (
  SET "appDir=%ProgramFiles%\Microsoft Visual Studio\2022\"
)
IF EXIST "%appDir%Enterprise\" (
  SET "MsBuildPath=%appDir%Enterprise\MSBuild\Current\Bin\"
) else (
  SET "MsBuildPath=%appDir%Professional\MSBuild\Current\Bin\"
)
ECHO [1mMSBuild Path       [0m %MsBuildPath%
ECHO ==========================================================================
ECHO [96mSTEP 001 (AUTO): Do NPM Install for Storefront...[0m
ECHO     [36mHINT: This may take a minute or two.[0m
ECHO [32m-----001a.[0m "CD "%CEFDIR%07.Portals\Storefront\AngJS""
CD "%CEFDIR%07.Portals\Storefront\AngJS"
ECHO [32m-----001b.[0m "gulp build"
CMD /C "gulp build"
ECHO [32m-----001c.[0m "gulp build:minify"
CMD /C "gulp build:minify"
ECHO [96mSTEP 001 (AUTO): ...Done[0m
ECHO ==========================================================================
ECHO [96mSTEP 002 (AUTO): Do NPM Install for Admin...[0m
ECHO     [36mHINT: This may take a minute or two.[0m
ECHO [32m-----002a.[0m "CD "%CEFDIR%07.Portals\Admin\AngJS""
CD "%CEFDIR%07.Portals\Admin\AngJS"
ECHO [32m-----002b.[0m "gulp build"
CMD /C "gulp build"
ECHO [32m-----002c.[0m "gulp build:minify"
CMD /C "gulp build:minify"
ECHO [96mSTEP 002 (AUTO): ...Done[0m
ECHO ==========================================================================
ECHO [96mSTEP 003 (AUTO): Pull NuGet 5.8.0 locally...[0m
ECHO [32m-----003a.[0m "CD "%CEFDIR%""
CD "%CEFDIR%"
ECHO [32m-----003b.[0m "mkdir ".nuget""
mkdir ".nuget" 2> nul
ECHO [32m-----003c.[0m "curl "https://dist.nuget.org/win-x86-commandline/v5.8.0/nuget.exe" --output ".nuget/nuget.exe""
curl "https://dist.nuget.org/win-x86-commandline/v5.8.0/nuget.exe" --output ".nuget/nuget.exe"
ECHO [96mSTEP 003 (AUTO): ...Done[0m
ECHO ==========================================================================
ECHO [96mSTEP 004 (AUTO): Use NuGet to Restore Packages...[0m
ECHO     [36mHINT: This may take a minute or two.[0m
ECHO [32m-----004a.[0m "CD "%CEFDIR%""
CD "%CEFDIR%"
ECHO [32m-----004b.[0m "".nuget/nuget.exe" restore %SLNNAME%"
".nuget/nuget.exe" restore %SLNNAME% > restore1.log
IF %ERRORLEVEL% NEQ 0 (
ECHO. [101;93mUh oh, something bad happened. Check restore1.log for errors[0m
GOTO END
)
ECHO [32m-----004c.[0m ""%MsBuildPath%MSBuild.exe" %SLNNAME% /p:Configuration="Debug" /p:Platform="Any CPU" /t:restore"
"%MsBuildPath%MSBuild.exe" %SLNNAME% /p:Configuration="Debug" /p:Platform="Any CPU" /t:restore > restore2.log
IF %ERRORLEVEL% NEQ 0 (
ECHO. [101;93mUh oh, something bad happened. Check restore2.log for errors[0m
GOTO END
)
ECHO [96mSTEP 004 (AUTO): ...Done[0m
ECHO ==========================================================================
ECHO [96mSTEP 005 (AUTO): Use MSBuild to compile the solution...[0m
ECHO     [36mHINT: This may take a few minutes.[0m
ECHO [32m-----005a.[0m "CD "%CEFDIR%""
CD "%CEFDIR%"
ECHO [32m-----005b.[0m ""%MsBuildPath%MSBuild.exe" %SLNNAME% /p:Configuration="Debug" /p:Platform="Any CPU""
"%MsBuildPath%MSBuild.exe" %SLNNAME% /p:Configuration="Debug" /p:Platform="Any CPU" > build.log
IF %ERRORLEVEL% NEQ 0 (
ECHO. [101;93mUh oh, something bad happened. Check build.log for errors[0m
GOTO END
)
ECHO [96mSTEP 005 (AUTO): ...Done[0m
PAUSE
:END
CD %CEFDIR%
ENDLOCAL
