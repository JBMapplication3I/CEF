@echo off
REM "%~dp0.\packages\Microsoft.TypeScript.MSBuild.2.6.1\tools\tsc\tsc.exe" --project "%~dp0.\08.Clarity.Ecommerce.UI\tsconfig.json" --listEmittedFiles > tsc.log
REM PAUSE
ECHO Make sure your command prompt buffer is set to at least 3,000 before continuing
PAUSE
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Microsoft\VisualStudio\NodeJs\node.exe" "%~dp0\..\..\..\packages\Microsoft.TypeScript.MSBuild.4.1.2\tools\tsc\tsc.js"  --project "%~dp0\tsconfig.json" --listEmittedFiles --locale en-US --listFiles --noEmit
ECHO Scroll up through the list of files to locate any errors
PAUSE
