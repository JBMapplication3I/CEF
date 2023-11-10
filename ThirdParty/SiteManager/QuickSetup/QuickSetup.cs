namespace SiteManager.QuickSetup
{
    using System;
    using System.ComponentModel;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using Events;
    using SiteManager.ViewModels;

    internal static class QuickSetup
    {
        #region Constants
        private const string Bar = "===============================================================================";
        #endregion

        #region Private Fields
        private static int progress;

        private static string? output;

        private static StringBuilder? outputB;

        private static RichTextBox? richTextBox;
        #endregion

        #region Private Properties
        private static int Progress
        {
            get => progress;
            set
            {
                if (progress == value)
                {
                    return;
                }
                progress = Math.Min(100, Math.Max(0, value));
                ProgressChanged?.Invoke(null, new(progress, null));
            }
        }

        private static string? Output
        {
            get => output;
            set
            {
                if (output == value)
                {
                    return;
                }
                output = value;
                OutputChanged?.Invoke(null, new(output));
            }
        }

        private static StringBuilder? OutputB
        {
            get => outputB;
            set
            {
                // ReSharper disable once RedundantCheckBeforeAssignment
                if (outputB == value)
                {
                    return;
                }
                outputB = value;
                // OutputChanged?.Invoke(null, new(output));
            }
        }

        private static RichTextBox RichTextBox
        {
            get => richTextBox!;
            set
            {
                // ReSharper disable once RedundantCheckBeforeAssignment
                if (richTextBox == value)
                {
                    return;
                }
                richTextBox = value;
                // DocumentChanged?.Invoke(null, new(document));
            }
        }
        #endregion

        #region Event Handlers
        public static event ProgressChangedEventHandler? ProgressChanged;

        public static event OutputChangedEventHandler? OutputChanged;

#pragma warning disable 67
        public static event DocumentChangedEventHandler? DocumentChanged;
#pragma warning restore 67
        #endregion

        private static void PostOutput()
        {
            if (OutputB == null)
            {
                return;
            }
            Output += OutputB.ToString();
            OutputB.Clear();
        }

        public static Task RunAsync(RunSettingsViewModel runSettings, CancellationToken cancellationToken)
        {
            // Beginning
            Progress = 0;
            OutputB = new StringBuilder();
            Output = string.Empty;
            {
                // Disclaimer
                OutputB.AppendLine(Bar);
                OutputB.AppendLine("DISCLAIMER: This script is for internal Clarity Ventures, Inc. use only.");
                OutputB.AppendLine("It should not be given to clients or used in production servers.");
                OutputB.AppendLine();
                Progress++;
                PostOutput();
                cancellationToken.ThrowIfCancellationRequested();
            }
            {
                // Show TODOs
                OutputB.AppendLine(Bar);
                OutputB.AppendLine("// TODO: Make the values for names and such enterable to the script");
                OutputB.AppendLine("// TODO: Add step: Add entry to hosts file for the binding");
                OutputB.AppendLine("// TODO: Add step: Install and set up ssl cert for https binding");
                OutputB.AppendLine("// TODO: Make the step numbering automatic");
                OutputB.AppendLine("// TODO: Create command line argument for the batch to resume at specific point");
                OutputB.AppendLine("// TODO: Output values to a file for resumes so you don't have to enter multiple times");
                OutputB.AppendLine("// TODO: Detect Enterprise vs Professional automatically");
                OutputB.AppendLine("// TODO: Detect VS Version (2019+) automatically");
                OutputB.AppendLine("// TODO: Add setting to choose to include Brand, Store and Vendor Admin standups");
                OutputB.AppendLine("// TODO: Add command line arguments that will assign values to appSettings keys and propogate to file");
                OutputB.AppendLine("// TODO: Add command line arguments that will assign values to cef_gulp_config keys and propogate to file");
                OutputB.AppendLine("// TODO: Convert this batch file to an executable");
                OutputB.AppendLine("// TODO: Add command line argument to make this batch file do IIS only");
                OutputB.AppendLine("// TODO: Make the batch file idempotent to upgrade across all versions to latest, like EF does");
                OutputB.AppendLine("// TODO: Add domain to portal aliases table");
                OutputB.AppendLine("// TODO: Change skin folder name in DNN");
                OutputB.AppendLine();
                Progress++;
                PostOutput();
                cancellationToken.ThrowIfCancellationRequested();
            }
            {
                // Show Variables
                OutputB.AppendLine(Bar);
                OutputB.AppendLine("Starting Standup Script");
                // SET VERSION=2021.1
                // SET CEFDIR=%~dp0
                // SET WEBDIR=%CEFDIR%..\WEB9
                // SET PORTALSDIR=%WEBDIR%\Portals\_default\Skins
                // SET IMGDIR=%CEFDIR%..\Images
                // SET SOLITMDIR=%CEFDIR%Solution Items
                OutputB.AppendLine();
                OutputB.Append("CEF Version:            ").AppendLine(runSettings.Version);
                OutputB.Append("CEF Dir:                ").AppendLine(runSettings.CEFDirectory);
                OutputB.Append("WEB9 Dir:               ").AppendLine(runSettings.WEBDirectory);
                OutputB.Append("Portals Dir:            ").AppendLine(runSettings.PortalsRelativeToWEBDirectory);
                OutputB.Append("Images Dir:             ").AppendLine(runSettings.ImagesDirectory);
                OutputB.Append("Solution Items Dir:     ").AppendLine(runSettings.SolutionItemsRelativeToCEFDirectory);
                OutputB.AppendLine("HINT: \"..\\\" means go up one directory, relative to the current. This can be repeated.");
                // SET SLNNAME=Clarity.Ecommerce.All.sln
                // SET DATAMODELPATH=%CEFDIR%01.DataAccessLayer\01.Clarity.Ecommerce.DataModel
                // SET PROJ_CAPS=CLARITY
                // SET SkinName=%PROJ_CAPS%
                // SET proj_lower=clarity
                // SET Proj_Title=Clarity
                // SET env_lower=local
                // SET Comp_Name_Title=Clarity Ventures, Inc.
                // SET MainDomainTitle=ClarityClient.com
                // SET MainDomain=clarityclient.com
                // SET SubDomain=%proj_lower%-%env_lower%.
                // SET FullDomain=%SubDomain%%MainDomain%
                // SET SiteName=%MainDomainTitle% (%SubDomain%)
                // SET SubRoot=/DesktopModules/ClarityEcommerce
                // SET AppPoolNameDNN=%SiteName% (DNN)
                // SET NamePartAdmin=API-Admin
                // SET NamePartBrandAdmin=API-BrandAdmin
                // SET NamePartStoreAdmin=API-StoreAdmin
                // SET NamePartVendorAdmin=API-VendorAdmin
                // SET NamePartStorefront=API-Storefront
                // SET NamePartScheduler=Scheduler
                // SET NamePartAPIReference=API-Reference
                // SET DbSource=.\SQL2019
                // SET CEFDbName=DEMO_CEF_2021_1
                // SET DNNDbName=DEMO_DNN9_2021_1
                // SET AuthKind=User=SQLLogin;Password=p4ssw0rd;
                // SET CEFConnString=Data Source=%DbSource%;Initial Catalog=%CEFDbName%;%AuthKind%
                // SET DNNConnString=Data Source=%DbSource%;Initial Catalog=%DNNDbName%;%AuthKind%
                // REM setx PYTHON "%UserProfile%\.windows-build-tools\python27\python.exe" /M 2> nul
                OutputB.AppendLine();
                Progress++;
                PostOutput();
                cancellationToken.ThrowIfCancellationRequested();
            }
            {
                // Ask about VS Ent vs Pro
                OutputB.AppendLine(Bar);
                OutputB.AppendLine("Ask about VS Ent vs Pro");
                OutputB.AppendLine();
                // SET /p whichInstalled=Do you have Visual Studio Enterpise or Professional installed? (e/[p]):
                // IF /I "%whichInstalled%" NEQ "e" GOTO Pro
                // :Ent
                // SET WHICH=Enterprise
                // GOTO :Cont
                // :Pro
                // SET WHICH=Professional
                // GOTO :Cont
                // :Cont
                // SET MsBuildPath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\%WHICH%\MSBuild\Current\Bin
                OutputB.AppendLine();
                Progress++;
                PostOutput();
                cancellationToken.ThrowIfCancellationRequested();
            }
            // ECHO ==========================================================================
            // ECHO ==========================================================================
            // ECHO [1mSTEP 001.[0m Git checkout the [1;93mCEF-Configs[0m repo to [1;93mC:\CEF-Configs[0m.
            // ECHO     [36mHINT: You may need to checkout to a folder like [33mC:\Data[0m[36m where you have[0m
            // ECHO     [36m      permissions first and then move it to [33mC:\[0m[36m.[0m
            // ECHO     [36mHINT: If you already have it, just pull latest in it[0m
            // PAUSE
            // ECHO ==========================================================================
            // ECHO [1mSTEP 002.[0m Since you only need one checkout of [1;93mCEF-Configs[0m repo for the box
            // ECHO           it is in, remove other copies from the box.
            // ECHO [101;93m     WARNING: NEVER LEAVE [4mCEF-Configs[0m[101;93m ON A CLIENT OR PRODUCTION BOX!      [0m
            // PAUSE
            // ECHO ==========================================================================
            // ECHO [1mSTEP 003.[0m Git checkout either the core [93mDNN9-CEF[0m repo or the [93mClient DNN[0m
            // ECHO           repo you want to use to the [93mWEB9 Dir[0m shown above.
            // ECHO     [36mHINT: If you already have it, just pull latest in it.[0m
            // PAUSE
            // ECHO ==========================================================================
            // ECHO [1mSTEP 004.[0m If you have a website in [1mIIS[0m named
            // ECHO           [93m%SiteName%[0m
            // ECHO           and/or any accompanying [1mApp Pools[0m, remove/delete them.
            // PAUSE
            // ECHO ==========================================================================
            // ECHO [1mSTEP 005.[0m Create a new/empty/blank database at:
            // ECHO           [1;93m%CEFConnString%[0m
            // ECHO     [91mWARNING: If you already have one and it is already populated with[0m
            // ECHO              [91mschema from master, it should be fine. But if it has[0m
            // ECHO              [91mconflicting migrations from a client project, this setup will[0m
            // ECHO              [91mfail.[0m
            // PAUSE
            // ECHO ==========================================================================
            // ECHO [1mSTEP 006.[0m Restore a backup of a standard [1mDNN9[0m for [1mCEF %VERSION%[0m database or
            // ECHO           the [1mDNN9[0m for the [1mClient[0m at:
            // ECHO           [1;93m%DNNConnString%[0m
            // PAUSE
            // REM ECHO ==========================================================================
            // REM ECHO STEP 00X. Verify the following questions:
            // REM SET /p haveCEFDb=Do you have a blank CEF Db ready at "%CEFConnString%" (you should to run this): (y/[n]) :
            // REM IF /I "%haveCEFDb%" NEQ "y" GOTO END
            // REM SET /p haveDNNDb=Do you have a restored DNN Db ready at "%DNNConnString%" (you should to run this): (y/[n]) :
            // REM IF /I "%haveDNNDb%" NEQ "y" GOTO END
            // ECHO ==========================================================================
            // ECHO [96mSTEP 007 (AUTO): Create the [93mImages[96m directory...[0m
            // mkdir "%IMGDIR%" 2> nul
            // ECHO [96mSTEP 007 (AUTO): ...Done[0m
            // ECHO ==========================================================================
            // ECHO [96mSTEP 008 (AUTO): Refresh the CEF folder with git ensuring its totally[0m
            // ECHO [96m                 reset for the script.[0m
            // ECHO [32m-----008a.[0m "cd "%CEFDIR%""
            // cd "%CEFDIR%"
            // ECHO [32m-----008b. (SKIPPED)[0m "git.exe reset --hard"
            // REM git.exe reset --hard 2> nul
            // ECHO [32m-----008c. (SKIPPED)[0m "git.exe clean -fdx"
            // REM git.exe clean -fdx 2> nul
            // ECHO [32m-----008d.[0m "git.exe fetch -v --progress --all"
            // git.exe fetch -v --progress --all 2> nul
            // ECHO [32m-----008c.[0m "git.exe remote update --prune"
            // git.exe remote update --prune 2> nul
            // ECHO [32m-----008d.[0m "git.exe pull --progress -v --no-rebase "origin" master"
            // git.exe pull --progress -v --no-rebase "origin" master 2> nul
            // ECHO [96mSTEP 008 (AUTO): ...Done[0m
            // ECHO ==========================================================================
            // ECHO [96mSTEP 009 (AUTO): Refresh the WEB9 folder with git ensuring its totally[0m
            // ECHO [96m                 reset for the script.[0m
            // ECHO [32m-----009a.[0m "cd "%WEBDIR%""
            // cd "%WEBDIR%"
            // ECHO [32m-----009b. (SKIPPED)[0m "git.exe reset --hard"
            // REM git.exe reset --hard 2> nul
            // ECHO [32m-----009c. (SKIPPED)[0m "git.exe clean -fdx"
            // REM git.exe clean -fdx 2> nul
            // ECHO [32m-----009d.[0m "git.exe fetch -v --progress --all"
            // git.exe fetch -v --progress --all 2> nul
            // ECHO [32m-----009c.[0m "git.exe remote update --prune"
            // git.exe remote update --prune 2> nul
            // ECHO [32m-----009d.[0m "git.exe pull --progress -v --no-rebase "origin" master"
            // git.exe pull --progress -v --no-rebase "origin" master 2> nul
            // ECHO [96mSTEP 009 (AUTO): ...Done[0m
            // ECHO ==========================================================================
            // ECHO [96mSTEP 010 (AUTO): Read in [93mCEF Configs[0m[96m for [93mv%VERSION%[0m[96m...[0m
            // ECHO [32m-----010a.[0m "cd "%CEFDIR%""
            // cd "%CEFDIR%"
            // ECHO -----
            // ECHO [32m-----010b.[0m "xcopy "C:\CEF-Configs\CEF-%VERSION%-QI" "%CEFDIR%" /Y /C /R /I /S"
            // xcopy "C:\CEF-Configs\CEF-%VERSION%-QI" "%CEFDIR%" /Y /C /R /I /S 2> nul
            // ECHO [96mSTEP 010 (AUTO): ...Done[0m
            // ECHO ==========================================================================
            // ECHO [96mSTEP 011 (AUTO): Read in [93mWEB9 Configs[0m[96m for [93mv%VERSION%[0m[96m...[0m
            // ECHO [32m-----011a.[0m "cd "%WEBDIR%""
            // cd "%WEBDIR%"
            // ECHO [32m-----011b.[0m "xcopy "C:\CEF-Configs\WEB9-%VERSION%-QI" "%WEBDIR%" /Y /C /R /I /S"
            // xcopy "C:\CEF-Configs\WEB9-%VERSION%-QI" "%WEBDIR%" /Y /C /R /I /S 2> nul
            // ECHO [96mSTEP 011 (AUTO): ...Done[0m
            // ECHO ==========================================================================
            // ECHO [96mSTEP 012 (AUTO): Replace CEF Connection Strings and other app settings in[0m
            // ECHO [96m                 config files...[0m
            // ECHO     [36mHINT: This may take a few seconds.[0m
            // ECHO [32m-----012a.[0m "cd "%SOLITMDIR%""
            // cd "%SOLITMDIR%"
            // ECHO [32m-----012b.[0m "replace strings"
            // powershell -Command "(Get-Content connectionStrings.config) | ForEach-Object { $_ -replace '{{DBSOURCE}}', '%DbSource%' } | Set-Content connectionStrings.config"
            // powershell -Command "(Get-Content connectionStrings.config) | ForEach-Object { $_ -replace '{{DBNAME}}', '%CEFDbName%' } | Set-Content connectionStrings.config"
            // powershell -Command "(Get-Content connectionStrings.config) | ForEach-Object { $_ -replace '{{AUTHKIND}}', '%AuthKind%' } | Set-Content connectionStrings.config"
            // powershell -Command "(Get-Content appSettings.config) | ForEach-Object { $_ -replace '{{PROJ_CAPS}}', '%PROJ_CAPS%' } | Set-Content appSettings.config"
            // powershell -Command "(Get-Content appSettings.config) | ForEach-Object { $_ -replace '{{proj_lower}}', '%proj_lower%' } | Set-Content appSettings.config"
            // powershell -Command "(Get-Content appSettings.config) | ForEach-Object { $_ -replace '{{Proj_Title}}', '%Proj_Title%' } | Set-Content appSettings.config"
            // powershell -Command "(Get-Content appSettings.config) | ForEach-Object { $_ -replace '{{env_lower}}', '%env_lower%' } | Set-Content appSettings.config"
            // powershell -Command "(Get-Content appSettings.config) | ForEach-Object { $_ -replace '{{Comp_Name_Title}}', '%Comp_Name_Title%' } | Set-Content appSettings.config"
            // powershell -Command "(Get-Content appSettings.config) | ForEach-Object { $_ -replace '{{Directory}}', '%CEFDIR%..\' } | Set-Content appSettings.config"
            // powershell -Command "(Get-Content cef_gulp_config.json) | ForEach-Object { $_ -replace '{{SKIN}}', '%SkinName%' } | Set-Content cef_gulp_config.json"
            // powershell -Command "(Get-Content rewrite.rewriteMaps.config) | ForEach-Object { $_ -replace '{{proj}}', '%proj_lower%' } | Set-Content rewrite.rewriteMaps.config"
            // ECHO [32m-----012c.[0m "cd "%CEFDIR%07.Portals\Storefront\Skins\Clarity""
            // cd "%CEFDIR%07.Portals\Storefront\Skins\Clarity"
            // ECHO [32m-----012d.[0m "replace strings"
            // powershell -Command "(Get-Content rewrite.rewriteMaps.config) | ForEach-Object { $_ -replace '{{proj}}', '%proj_lower%' } | Set-Content rewrite.rewriteMaps.config"
            // IF /I "%SkinName%" NEQ "Clarity" (
            // ECHO [32m-----012d1.[0m "copy files from Clarity to %SkinName%"
            // xcopy "%CEFDIR%07.Portals\Storefront\Skins\Clarity" "%CEFDIR%07.Portals\Storefront\Skins\%SkinName%" /Y /C /R /I /S 2> nul
            // )
            // ECHO [96mSTEP 012 (AUTO): ...Done[0m
            // ECHO ==========================================================================
            // ECHO [96mSTEP 013 (AUTO): Replace CEF Connection Strings and other app settings in[0m
            // ECHO [96m                 config files...[0m
            // ECHO     [36mHINT: This may take a few seconds.[0m
            // ECHO [32m-----013a.[0m "cd "%WEBDIR%""
            // cd "%WEBDIR%"
            // ECHO [32m-----013b.[0m "replace strings"
            // powershell -Command "(Get-Content web.connectionStrings.config) | ForEach-Object { $_ -replace '{{DBSOURCE}}', '%DbSource%' } | Set-Content web.connectionStrings.config"
            // powershell -Command "(Get-Content web.connectionStrings.config) | ForEach-Object { $_ -replace '{{DBNAME}}', '%DNNDbName%' } | Set-Content web.connectionStrings.config"
            // powershell -Command "(Get-Content web.connectionStrings.config) | ForEach-Object { $_ -replace '{{AUTHKIND}}', '%AuthKind%' } | Set-Content web.connectionStrings.config"
            // ECHO [96mSTEP 013 (AUTO): ...Done[0m
            // ECHO ==========================================================================
            // ECHO [96mSTEP 014 (AUTO): Create the skin symbolic links from CEF to WEB9...[0m
            // ECHO [32m-----014a.[0m "cd "%PORTALSDIR%""
            // cd "%PORTALSDIR%"
            // ECHO [32m-----014b.[0m "mklink /D "%SkinName%" "%CEFDIR%07.Portals\Storefront\Skins\%SkinName%""
            // mklink /D "%SkinName%" "%CEFDIR%07.Portals\Storefront\Skins\%SkinName%" 2> nul
            // ECHO [32m-----014c.[0m "mklink /D "Clarity-Admin" "%CEFDIR%07.Portals\Storefront\Skins\Clarity-Admin""
            // mklink /D "Clarity-Admin" "%CEFDIR%07.Portals\Storefront\Skins\Clarity-Admin" 2> nul
            // ECHO [96mSTEP 014 (AUTO): ...Done[0m
            // ECHO ==========================================================================
            // ECHO [96mSTEP 015 (AUTO): Do NPM Install for Storefront...[0m
            // ECHO     [36mHINT: This may take a minute or two.[0m
            // ECHO [32m-----015a.[0m "cd "%CEFDIR%07.Portals\Storefront\AngJS""
            // cd "%CEFDIR%07.Portals\Storefront\AngJS"
            // ECHO [32m-----015b.[0m "npm i -g --production windows-build-tools && npm i -g gulp && npm install && npm rebuild node-sass"
            // CMD /C "npm i -g --production windows-build-tools && npm i -g gulp && npm install && npm rebuild node-sass"
            // ECHO [32m-----015c.[0m "gulp build"
            // CMD /C "gulp build"
            // ECHO [32m-----015d.[0m "gulp build:minify"
            // CMD /C "gulp build:minify"
            // ECHO [96mSTEP 015 (AUTO): ...Done[0m
            // ECHO ==========================================================================
            // ECHO [96mSTEP 016 (AUTO): Do NPM Install for Admin...[0m
            // ECHO     [36mHINT: This may take a minute or two.[0m
            // ECHO [32m-----016a.[0m "cd "%CEFDIR%07.Portals\Admin\AngJS""
            // cd "%CEFDIR%07.Portals\Admin\AngJS"
            // ECHO [32m-----016b.[0m "npm i -g gulp && npm install"
            // CMD /C "npm i -g gulp && npm install"
            // ECHO [32m-----016c.[0m "gulp build"
            // CMD /C "gulp build"
            // ECHO [32m-----016d.[0m "gulp build:minify"
            // CMD /C "gulp build:minify"
            // ECHO [96mSTEP 016 (AUTO): ...Done[0m
            // ECHO ==========================================================================
            // ECHO [96mSTEP 017 (AUTO): Pull NuGet 5.8.0 locally...[0m
            // ECHO [32m-----017a.[0m "cd "%CEFDIR%""
            // cd "%CEFDIR%"
            // ECHO [32m-----017b.[0m "mkdir ".nuget""
            // mkdir ".nuget" 2> nul
            // ECHO [32m-----017c.[0m "curl "https://dist.nuget.org/win-x86-commandline/v5.8.0/nuget.exe" --output ".nuget/nuget.exe""
            // curl "https://dist.nuget.org/win-x86-commandline/v5.8.0/nuget.exe" --output ".nuget/nuget.exe"
            // ECHO [96mSTEP 017 (AUTO): ...Done[0m
            // ECHO ==========================================================================
            // ECHO [96mSTEP 018 (AUTO): Use NuGet to Restore Packages...[0m
            // ECHO     [36mHINT: This may take a minute or two.[0m
            // ECHO [32m-----018a.[0m "cd "%CEFDIR%""
            // cd "%CEFDIR%"
            // ECHO [32m-----018b.[0m "".nuget/nuget.exe" restore %SLNNAME%"
            // ".nuget/nuget.exe" restore %SLNNAME% > restore1.log
            // IF %ERRORLEVEL% NEQ 0 (
            // ECHO. [101;93mUh oh, something bad happened. Check restore1.log for errors[0m
            // GOTO END
            // )
            // ECHO [32m-----018c.[0m ""%MsBuildPath%\MSBuild.exe" %SLNNAME% /p:Configuration="Debug" /p:Platform="Any CPU" /t:restore"
            // "%MsBuildPath%\MSBuild.exe" %SLNNAME% /p:Configuration="Debug" /p:Platform="Any CPU" /t:restore > restore2.log
            // IF %ERRORLEVEL% NEQ 0 (
            // ECHO. [101;93mUh oh, something bad happened. Check restore2.log for errors[0m
            // GOTO END
            // )
            // ECHO [96mSTEP 018 (AUTO): ...Done[0m
            // ECHO ==========================================================================
            // ECHO [96mSTEP 019 (AUTO): Use MSBuild to compile the solution...[0m
            // ECHO     [36mHINT: This may take a few minutes.[0m
            // ECHO [32m-----019a.[0m "cd "%CEFDIR%""
            // cd "%CEFDIR%"
            // ECHO [32m-----019b.[0m ""%MsBuildPath%\MSBuild.exe" %SLNNAME% /p:Configuration="Debug" /p:Platform="Any CPU""
            // "%MsBuildPath%\MSBuild.exe" %SLNNAME% /p:Configuration="Debug" /p:Platform="Any CPU" > build.log
            // IF %ERRORLEVEL% NEQ 0 (
            // ECHO. [101;93mUh oh, something bad happened. Check build.log for errors[0m
            // GOTO END
            // )
            // ECHO [96mSTEP 019 (AUTO): ...Done[0m
            // ECHO ==========================================================================
            // ECHO [96mSTEP 020 (AUTO): Use EF6 to set up the CEF DB schema...[0m
            // ECHO     [36mHINT: This may take a few minutes.[0m
            // "%CEFDIR%packages\EntityFramework.6.4.4\tools\net45\win-x86\ef6.exe" database update -v ^
            //  --project-dir "%DATAMODELPATH%" -a "%DATAMODELPATH%\bin\Debug\Clarity.Ecommerce.DataModel.dll" ^
            //  --config "%DATAMODELPATH%\app.config" > schema.log
            // ECHO [96mSTEP 020 (AUTO): ...Done[0m
            // IF %ERRORLEVEL% NEQ 0 (
            // ECHO. [101;93mUh oh, something bad happened. Check schema.log for errors[0m
            // GOTO END
            // )
            // ECHO [101;93m  WARNING: This does not inject [4mSeed Data[0m[101;93m, use the Unit Test to do that!  [0m
            // ECHO ==========================================================================
            // ECHO [96mSTEP 021 (AUTO): Propogate IIS...[0m
            // ECHO [32m-----021a.[0m "cd C:\Windows\System32\inetsrv"
            // cd C:\Windows\System32\inetsrv
            // ECHO [32m-----021b.[0m Create DNN pool, then DNN Site, then assign DNN Pool to DNN Site
            // appcmd add apppool /name:"%AppPoolNameDNN%" ^
            //  && appcmd add site /name:"%SiteName%" /bindings:http://%FullDomain%:80 /physicalPath:"%WEBDIR%" ^
            //  && appcmd set site /site.name:"%SiteName%" /[path='/'].applicationPool:"%AppPoolNameDNN%"
            // ECHO [32m-----021c.[0m Create Admin pool, then Admin App, then assign Admin pool to Admin App, then Create Admin UI Virtual Directory
            // appcmd add apppool /name:"%SiteName% (%NamePartAdmin%)" ^
            //  && appcmd add app /site.name:"%SiteName%" /path:"%SubRoot%/%NamePartAdmin%" /physicalPath:"%CEFDIR%07.Portals\Admin\Service" ^
            //  && appcmd set site /site.name:"%SiteName%" /[path='%SubRoot%/%NamePartAdmin%'].applicationPool:"%SiteName% (%NamePartAdmin%)" ^
            //  && appcmd add vdir /app.name:"%SiteName%/" /path:"%SubRoot%/UI-Admin" /physicalPath:"%CEFDIR%07.Portals\Admin\AngJS"
            // ECHO [32m-----021d.[0m Create Storefront pool, then Storefront App, then assign Storefront pool to Storefront App, then Create Storefront UI Virtual Directory
            // appcmd add apppool /name:"%SiteName% (%NamePartStorefront%)" ^
            //  && appcmd add app /site.name:"%SiteName%" /path:"%SubRoot%/%NamePartStorefront%" /physicalPath:"%CEFDIR%07.Portals\Storefront\Service" ^
            //  && appcmd set site /site.name:"%SiteName%" /[path='%SubRoot%/%NamePartStorefront%'].applicationPool:"%SiteName% (%NamePartStorefront%)" ^
            //  && appcmd add vdir /app.name:"%SiteName%/" /path:"%SubRoot%/UI-Storefront" /physicalPath:"%CEFDIR%07.Portals\Storefront\AngJS"
            // ECHO [32m-----021e.[0m Create Scheduler pool, then Scheduler App, then assign Scheduler pool to Scheduler App
            // appcmd add apppool /name:"%SiteName% (%NamePartScheduler%)" ^
            //  && appcmd add app /site.name:"%SiteName%" /path:"%SubRoot%/%NamePartScheduler%" /physicalPath:"%CEFDIR%07.Portals\Scheduler\Scheduler" ^
            //  && appcmd set site /site.name:"%SiteName%" /[path='%SubRoot%/%NamePartScheduler%'].applicationPool:"%SiteName% (%NamePartScheduler%)"
            // ECHO [32m-----021f.[0m Create API-Reference Virtual Directory
            // appcmd add vdir /app.name:"%SiteName%/" /path:"%SubRoot%/%NamePartAPIReference%" /physicalPath:"%CEFDIR%07.Portals\APIReference\APIReference"
            // ECHO [32m-----021g.[0m Create Images Virtual Directory (images/ecommerce)
            // appcmd add vdir /app.name:"%SiteName%/" /path:"/Images/ecommerce" /physicalPath:"%IMGDIR%"
            // ECHO [32m-----021h. (SKIPPED)[0m Create StoreAdmin pool, then StoreAdmin App, then assign StoreAdmin pool to StoreAdmin App
            // REM appcmd add apppool /name:"%SiteName% (%NamePartStoreAdmin%)" ^
            // REM  && appcmd add app /site.name:"%SiteName%" /path:"%SubRoot%/%NamePartStoreAdmin%" /physicalPath:"%CEFDIR%07.Portals\StoreAdmin\Service" ^
            // REM  && appcmd set site /site.name:"%SiteName%" /[path='%SubRoot%/%NamePartStoreAdmin%'].applicationPool:"%SiteName% (%NamePartStoreAdmin%)"
            // ECHO [32m-----021i. (SKIPPED)[0m Create BrandAdmin pool, then BrandAdmin App, then assign BrandAdmin pool to BrandAdmin App
            // REM appcmd add apppool /name:"%SiteName% (%NamePartBrandAdmin%)" ^
            // REM  && appcmd add app /site.name:"%SiteName%" /path:"%SubRoot%/%NamePartBrandAdmin%" /physicalPath:"%CEFDIR%07.Portals\BrandAdmin\Service" ^
            // REM  && appcmd set site /site.name:"%SiteName%" /[path='%SubRoot%/%NamePartBrandAdmin%'].applicationPool:"%SiteName% (%NamePartBrandAdmin%)"
            // ECHO [32m-----021j. (SKIPPED)[0m Create VendorAdmin pool, then VendorAdmin App, then assign VendorAdmin pool to VendorAdmin App
            // REM appcmd add apppool /name:"%SiteName% (%NamePartVendorAdmin%)" ^
            // REM  && appcmd add app /site.name:"%SiteName%" /path:"%SubRoot%/%NamePartVendorAdmin%" /physicalPath:"%CEFDIR%07.Portals\VendorAdmin\Service" ^
            // REM  && appcmd set site /site.name:"%SiteName%" /[path='%SubRoot%/%NamePartVendorAdmin%'].applicationPool:"%SiteName% (%NamePartVendorAdmin%)"
            // ECHO [96mSTEP 021 (AUTO): ...Done[0m
            //
            // PAUSE
            // :END
            // cd %CEFDIR%
            return Task.CompletedTask;
        }
    }
}
