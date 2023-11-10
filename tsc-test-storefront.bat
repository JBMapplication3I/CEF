@echo off
ECHO Make sure your command prompt buffer is set to at least 3,000 before continuing
IF EXIST "%ProgramFiles(x86)%" (
  SET "appDir=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\"
) ELSE (
  SET "appDir=%ProgramFiles%\Microsoft Visual Studio\2019\"
)
IF EXIST "%appDir%Enterprise\" (
  SET "NodePath=%appDir%Enterprise\MSBuild\Microsoft\VisualStudio\NodeJs\node.exe"
) ELSE (
  SET "NodePath=%appDir%Professional\MSBuild\Microsoft\VisualStudio\NodeJs\node.exe"
)
PAUSE
"%NodePath%" "%~dp0\packages\Microsoft.TypeScript.MSBuild.4.4.4\tools\tsc\tsc.js" ^
  --project "%~dp0\07.Portals\Storefront\AngJS\tsconfig.json" ^
  --listEmittedFiles --locale en-US --listFiles --noEmit
ECHO Scroll up through the list of files to locate any errors
PAUSE
