'CEFDotNetNukeExtensions DevDeploy Starting...'
$buildScriptsDirectory = $PSScriptRoot
"BuildScript Folder = " + $buildScriptsDirectory
$projectDirectory = Split-Path $PSScriptRoot -Parent
"Project Folder = " + $projectDirectory
$config = Get-Content -Raw -Path "$buildScriptsDirectory\DevDeploy.json" | ConvertFrom-Json
'DevDeploy Enabled = ' + $config.DoDnnDevDeploy
$dnnFolder = $config.DnnFolder;
if($config.DoDnnDevDeploy){
	"DNN Folder = " + $dnnFolder
	if(Test-Path $dnnFolder){
		$binFolder = $dnnFolder + '\bin'
		"DNN Bin Folder = " + $binFolder
		if(Test-Path $binFolder){
			$moduleFolder = $config.DnnFolder + '\DesktopModules\ClarityEcommerceDnn'
			"ClarityEcommerceDnn Module Folder = " + $moduleFolder
			if(Test-Path $moduleFolder){
				'Copying Files...'
				robocopy "$projectDirectory\bin" $binFolder ClarityEcommerceDNN.* /s
				robocopy $projectDirectory $moduleFolder *.ascx *.asmx *.css *.html *.htm *.resx *.aspx *.js *.txt /s /XD "$projectDirectory\packages" "$projectDirectory\CEFComponents" "$projectDirectory\PersonaBar" "$projectDirectory\obj" "$projectDirectory\_ReSharper" "$projectDirectory\.git"
				robocopy "$projectDirectory\images" "$moduleFolder\images" /s
				robocopy "$projectDirectory\CEFComponents" "$moduleFolder\CEFComponents" /s
				robocopy "$projectDirectory\PersonaBar" "$dnnFolder\DesktopModules\Admin\Dnn.PersonaBar\Modules\ClarityEcommerceDNN" /s
			} else{
				'DNN ClarityEcommerceDnn Module Folder (' + $moduleFolder + ') was not found. You must install the module using DNN Extension Install first.'
			}
		} else {
			'DNN Bin Folder (' + $binFolder + ') was not found. Please make sure that you are pointing to a proper installation of DNN.'
		}
	} else{
		'DNN Folder (' + $config.DnnFolder + ') was not found. Please specify the DNN Folder in the DevDeploy.json file in the BuildScripts folder.'
	}
}
'DONE'
