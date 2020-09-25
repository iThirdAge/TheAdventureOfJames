$Outlook = New-Object -ComObject Outlook.Application
$outlookNameSpace = $Outlook.getNamespace("MAPI")
$newPSTLocation = 'C:\Outlook_PSTs'
$outlookProcessID = 0

# Verify if the path exists for the new PST folder. If not, create it
if (!(Test-Path $newPSTLocation))
{
    New-Item -Path $newPSTLocation -ItemType "Directory"
}

# Unhook all PSTs in Documents open in Outlook
$all_psts = $outlookNameSpace.Stores | Where-Object {($_.ExchangeStoreType -eq '3') -and ($_.FilePath -like '*.pst') -and ($_.IsDataFileStore -eq $true)}

ForEach ($pst in $all_psts)
{
    $Outlook.Session.RemoveStore($pst.GetRootFolder())
}

# Close Outlook
$outlookProcessID = (Get-Process 'Outlook').id
Get-Process Outlook | Foreach-Object { $_.CloseMainWindow() | Out-Null } | Stop-Process -force
Wait-Process -Id $outlookProcessID

# Comb through system for any PST file and move it to the desired location
Get-ChildItem -Path C:\*.pst -Recurse | Move-Item -Destination C:\Outlook_PSTs\

# Open Outlook
Start-Process Outlook
Start-Sleep -Seconds 10

# Add the PSTs back to Outlook
$Outlook = New-Object -ComObject Outlook.Application #need to reinstantiate the Outlook instance
$myPSTs = Get-ChildItem $newPSTLocation -Filter *.pst 
ForEach ($pst in $myPSTs)
{
    $Outlook.Session.AddStore($pst.FullName)
}
