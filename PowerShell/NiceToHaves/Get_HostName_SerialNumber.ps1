$session = New-CimSession
$serialNumber = (Get-CimInstance -CimSession $session -Class Win32_BIOS).SerialNumber

Write-Host $env:computername
Write-Host $serialNumber
pause
