$OutputDirectory = $PSScriptRoot + "\buildsrc\"
$Skin = $args[0]
$SkinPath = $PSScriptRoot + "\Skins\" + $Skin + "\src\"
$SourcePath = $PSScriptRoot + "\src\"
$BuildManifest = $OutputDirectory + "buildmanifest.txt"
$CoreFileCount = 0
$OverrideFileCount = 0

echo "Using skin $Skin"

if (!(Test-Path -Path $OutputDirectory))
{
    mkdir $OutputDirectory
}

# Check if there was a previous successful build time
$PreviousBuildTime = [DateTime]::MinValue
if (Test-Path -Path $BuildManifest) {
    $BuildTime = (Get-Content -Path $BuildManifest)
    $PreviousBuildTime = [DateTime]::Parse($BuildTime)

    $msg = "Last successful build was $PreviousBuildTime"
    echo $msg
}

echo "Copying modified core files to intermediate directory..."

# Copy modified core files to build root
Get-ChildItem $SourcePath -File -Recurse |
        Where-Object { $_.LastWriteTime -gt $PreviousBuildTime } |
        ForEach-Object -Process {
    Push-Location $SourcePath
    $RelPath = Resolve-Path -Relative $_.FullName
    $Folder = [System.IO.Path]::GetDirectoryName($RelPath)
    Pop-Location
    if (!(Test-Path -Path ($OutputDirectory + $Folder)))
    {
        mkdir ($OutputDirectory + $Folder)
    }
    Copy-Item $_.FullName ($OutputDirectory + $RelPath)
    $CoreFileCount += 1
}

$msg = "Copied $CoreFileCount modified core files."
echo $msg
echo "Copying skin override files to intermediate directory..."

# Copy modified skin files and overwrite core files
Get-ChildItem $SkinPath -File -Recurse |
        Where-Object { $_.LastWriteTime -gt $PreviousBuildTime } |
        ForEach-Object -Process {
    Push-Location $SkinPath
    $RelPath = Resolve-Path -Relative $_.FullName
    $Folder = [System.IO.Path]::GetDirectoryName($RelPath)
    Pop-Location
    # TODO: Check if path matches an existing file in buildsrc?
    if (!(Test-Path -Path ($OutputDirectory + $Folder)))
    {
        mkdir ($OutputDirectory + $Folder)
    }
    Copy-Item $_.FullName ($OutputDirectory + $RelPath)
    $OverrideFileCount += 1
}

$msg = "Copied $OverrideFileCount modified override files."
echo $msg

echo "Building files..."
npm run buildWithOverrides

# Save successful build date
$PreviousBuildTime = Get-Date
Out-File -FilePath $BuildManifest -InputObject $PreviousBuildTime