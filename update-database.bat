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
IF EXIST "%ProgramFiles(x86)%" (
  SET "appDir=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\"
) ELSE (
  SET "appDir=%ProgramFiles%\Microsoft Visual Studio\2019\"
)
IF EXIST "%appDir%Enterprise\" (
  SET "MsBuildPath=%appDir%Enterprise\MSBuild\Current\Bin\"
) ELSE (
  SET "MsBuildPath=%appDir%Professional\MSBuild\Current\Bin\"
)
ECHO [1mMSBuild Path          [0m %MsBuildPath%
CALL ".\Solution Items\quick-install-settings.bat"
ECHO [1mCEF Version:          [0m %VERSION%
ECHO [1mCEF Dir:              [0m %CEFDIR%
ECHO [1mParent Dir:           [0m %PARENTDIR%
ECHO [1mSolution Items Dir:   [0m %SOLITMDIR%
ECHO [1mWEB9 Dir:             [0m %WEBDIR%
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
ECHO [96mSTEP %STEPNUM% (AUTO): Use EF6 to set up the CEF DB schema...[0m
ECHO     [36mHINT: This may take a few minutes, a long pause is normal.[0m
ECHO     [36mHINT: A [33mschema.log[36m file is being written to with the live progress.[0m
"%CEFDIR%packages\EntityFramework.6.4.4\tools\net45\win-x86\ef6.exe" database update -v ^
 --project-dir "%DATAMODELPATH%" -a "%DATAMODELPATH%\bin\Debug\Clarity.Ecommerce.DataModel.dll" ^
 --config "%DATAMODELPATH%\app.config" > schema.log
ECHO [96mSTEP %STEPNUM% (AUTO): ...Done[0m
IF %ERRORLEVEL% NEQ 0 (
ECHO [101;93m        Uh oh, something bad happened. Check schema.log for errors        [0m
GOTO END
)
ECHO [103;91m  WARNING: This does not inject [4mSeed Data[0m[103;91m, use the Unit Test to do that!  [0m

PAUSE
:END
cd %CEFDIR%
ENDLOCAL
