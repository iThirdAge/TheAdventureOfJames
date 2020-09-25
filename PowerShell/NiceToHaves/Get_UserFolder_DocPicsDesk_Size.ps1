$currentUser = $env:USERNAME
$userFolder = "C:\users\" + $currentUser

$allFolders = Get-ChildItem $userFolder -Directory -Force | Where-Object {$_.BaseName -eq "Documents"} | Where-Object {$_.BaseName -eq "Pictures"} | Where-Object {$_.BaseName -eq "Desktop"}

#Create array to store folder objects found with size info.
[System.Collections.ArrayList]$folderList = @()

#Go through each folder in the base path.
ForEach ($folder in $allFolders) {

    #Clear out the variables used in the loop.
    $fullPath = $null        
    $folderObject = $null
    $folderSize = $null
    $folderSizeInMB = $null
    $folderSizeInGB = $null
    $folderBaseName = $null

    #Store the full path to the folder and its name in separate variables
    $fullPath = $folder.FullName
    $folderBaseName = $folder.BaseName     

    Write-Verbose "Working with [$fullPath]..."            

    #Get folder info / sizes
    $folderSize = Get-Childitem -Path $fullPath -Recurse -Force -ErrorAction SilentlyContinue | Measure-Object -Property Length -Sum -ErrorAction SilentlyContinue       
        
    #We use the string format operator here to show only 2 decimals, and do some PS Math.
    $folderSizeInMB = "{0} MB" -f ($folderSize.Sum / 1MB)
    $folderSizeInGB = "{0} GB" -f ($folderSize.Sum / 1GB)

    #Here we create a custom object that we'll add to the array
    $folderObject = [PSCustomObject]@{

        FolderName    = $folderBaseName
        'Size(MB)'    = $folderSizeInMB
        'Size(GB)'    = $folderSizeInGB

    }                        

    #Add the object to the array
    $folderList.Add($folderObject) | Out-Null

}

if ($AddTotal) {

    $grandTotal = $null

    if ($folderList.Count -gt 1) {
    
        $folderList | ForEach-Object {

            $grandTotal += $_.'Size(Bytes)'    

        }

        $totalFolderSizeInMB = " MB" -f ($grandTotal / 1MB)
        $totalFolderSizeInGB = " GB" -f ($grandTotal / 1GB)

        $folderObject = [PSCustomObject]@{

            FolderName    = 'GrandTotal'
            'Size(MB)'    = $totalFolderSizeInMB
            'Size(GB)'    = $totalFolderSizeInGB
        }

        #Add the object to the array
        $folderList.Add($folderObject) | Out-Null
    }   

}
#Return the object array with the objects selected in the order specified.
Return $folderList