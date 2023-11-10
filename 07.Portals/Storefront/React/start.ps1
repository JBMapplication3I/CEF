$OverrideFileNames = @()

$OutputDirectory = $PSScriptRoot + "\buildsrc\"
$Skin = $args[0]
$SkinPath = $PSScriptRoot + "\Skins\" + $Skin + "\src\"
$SourcePath = $PSScriptRoot + "\src\"

# find current client override files
Get-ChildItem $SkinPath -File -Recurse |
        ForEach-Object -Process {
    $OverrideFileNames += $_.FullName
}
$WatcherJob = Start-Job -ScriptBlock {
    # begin listening for source changes
    $WatchFilter = "*.*" # all files for now, but we could limit to *.ts/*.js/etc
    $SkinWatcher = New-Object IO.FileSystemWatcher $using:SkinPath, $WatchFilter -Property @{
        IncludeSubdirectories = $true
    }
    $SourceWatcher = New-Object IO.FileSystemWatcher $using:SourcePath, $WatchFilter -Property @{
        IncludeSubdirectories = $true
    }
    
    $OnOverrideCreated = Register-ObjectEvent $SkinWatcher -EventName "Created" -Action {
        $details = $Event.SourceEventArgs
        $fullPath = $details.fullPath
        $relPath = $details.Name
        # Ensure change event is for a file, not a directory
        if (Test-Path -Path $fullPath -PathType Leaf)
        {
            # Copy the new file to buildsrc
            $newPath = $using:OutputDirectory + $relPath
            Write-Host "Copying $relPath to buildsrc"
            Copy-Item $fullPath $newPath
        }
    }
    
    $OnOverrideModified = Register-ObjectEvent $SkinWatcher -EventName "Changed" -Action {
        $details = $Event.SourceEventArgs
        $fullPath = $details.fullPath
        $relPath = $details.Name
        # Ensure change event is for a file, not a directory
        if (Test-Path -Path $fullPath -PathType Leaf)
        {
            # Copy the modified file to buildsrc
            $newPath = $using:OutputDirectory + $relPath
            Write-Host "Copying $relPath to buildsrc"
            Copy-Item $fullPath $newPath
        }
    }

    $OnOverrideDeleted = Register-ObjectEvent $SkinWatcher -EventName "Deleted" -Action {
        $details = $Event.SourceEventArgs
        $fullPath = $details.fullPath
        $relPath = $details.Name
        # Ensure change event is for a file, not a directory
        if (Test-Path -Path $fullPath -PathType Leaf)
        {
            # Determine the path to the equivalent core file
            $pathToCoreFile = $using:SourcePath + $relPath
            # Check if said core file exists
            if (Test-Path -Path $pathToCoreFile -PathType Leaf)
            {
                # Copy the core file over to buildsrc, overwriting the old override
                Write-Host "Restoring $relPath from core file"
                $newPath = $using:OutputDirectory + $relPath
                Copy-Item $pathToCoreFile $newPath
            }
        }
    }

    $OnOverrideRenamed = Register-ObjectEvent $SkinWatcher -EventName "Renamed" -Action {
        $details = $Event.SourceEventArgs
        $fullPath = $details.fullPath
        $relPath = $details.Name
        $oldRelPath = $details.OldName
        $oldFullPath = $details.OldFullPath

        # Ensure change event is for a file, not a directory
        if (Test-Path -Path $fullPath -PathType Leaf)
        {
            $pathToCoreFile = $using:SourcePath + $oldRelPath
            if (Test-Path -Path $pathToCoreFile -PathType Leaf)
            {
                # If old path corresponds to a core file, copy the core file
                $pathToOverwrite = $using:OutputDirectory + $oldRelPath
                Copy-Item $pathToCoreFile $pathToOverwrite
            }
            else
            {
                # If old path does not correspond to a core file, delete old file from buildsrc
                $pathToRemove = $using:OutputDirectory + $oldRelPath
                Remove-Item $pathToRemove
            }
            # Copy new file to buildsrc
            $newPath = $using:OutputDirectory + $relPath
            Copy-Item $fullPath $newPath
        }
    }

    $OnCoreFileCreated = Register-ObjectEvent $SourceWatcher -EventName "Created" -Action {
        $details = $Event.SourceEventArgs
        $fullPath = $details.fullPath
        $relPath = $details.Name
        # Ensure change event is for a file, not a directory
        if (Test-Path -Path $fullPath -PathType Leaf)
        {
            # Check if an override exists for the file
            $pathToOverride = $using:SkinPath + $relPath
            if (!(Test-Path -Path $pathToOverride -PathType Leaf))
            {
                # If no override exists, copy new file to buildsrc
                $newPath = $using:OutputDirectory + $relPath
                Copy-Item $fullPath $newPath
            }
        }
    }

    $OnCoreFileModified = Register-ObjectEvent $SourceWatcher -EventName "Changed" -Action {
        $details = $Event.SourceEventArgs
        $fullPath = $details.fullPath
        $relPath = $details.Name
        # Ensure change event is for a file, not a directory
        if (Test-Path -Path $fullPath -PathType Leaf)
        {
            # Check if an override exists for the file
            Write-Host "Checking if $relPath is in an override"
            $pathToOverride = $using:SkinPath + $relPath
            if (!(Test-Path -Path $pathToOverride -PathType Leaf))
            {
                # If no override exists, copy modified file to buildsrc
                $newPath = $using:OutputDirectory + $relPath
                Write-Host "Copying $relPath to buildsrc"
                Copy-Item $fullPath $newPath
            }
        }
    }

    $OnCoreFileDeleted = Register-ObjectEvent $SourceWatcher -EventName "Deleted" -Action {
        $details = $Event.SourceEventArgs
        $fullPath = $details.fullPath
        $relPath = $details.Name
        # Ensure change event is for a file, not a directory
        if (Test-Path -Path $fullPath -PathType Leaf)
        {
            # Check if an override exists for the file
            $pathToOverride = $using:SkinPath + $relPath
            if (!(Test-Path -Path $pathToOverride -PathType Leaf))
            {
                # If no override exists, delete the file from buildsrc
                $toDelete = $using:OutputDirectory + $relPath
                Remove-Item $toDelete
            }
        }
    }

    $OnCoreFileRenamed = Register-ObjectEvent $SourceWatcher -EventName "Renamed" -Action {
        $details = $Event.SourceEventArgs
        $fullPath = $details.fullPath
        $relPath = $details.Name
        $oldRelPath = $details.OldName
        $oldFullPath = $details.OldFullPath

        # Ensure change event is for a file, not a directory
        if (Test-Path -Path $fullPath -PathType Leaf)
        {
            $pathToClientOverrideForOld = $using:SkinPath + $oldRelPath
            $pathToClientOverrideForNew = $using:SkinPath + $relPath
            $pathToOldInBuildSrc = $using:OutputDirectory + $oldRelPath
            $pathToNewInBuildSrc = $using:OutputDirectory + $relPath
            if (Test-Path -Path $pathToClientOverrideForOld -PathType Leaf)
            {
                # If old path is client override, leave buildsrc as-is
                # Do Nothing
            }
            else
            {
                # If old path is not client override, delete from buildsrc
                Remove-Item $pathToOldInBuildSrc
            }
            if (Test-Path -Path $pathToClientOverrideForNew -PathType Leaf)
            {
                # If new path is client override, leave buildsrc as-is
                # Do Nothing
            }
            else
            {
                # If new path is not client override, copy to buildsrc
                Copy-Item $fullPath $pathToNewInBuildSrc
            }
        }
    }

    $SkinWatcher.EnableRaisingEvents = $true
    $SourceWatcher.EnableRaisingEvents = $true

    do
    {
        Wait-Event -Timeout 1
    } while ($true)
}

npm run startWithOverrides

Stop-Job $WatcherJob
Receive-Job $WatcherJob