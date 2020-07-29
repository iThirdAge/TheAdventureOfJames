# Harrison Vu

# Variables
$RemotePath = "HKLM:\System\CurrentControlSet\Control\Terminal Server"

# Join computer to domain and rename it
#Add-Computer -Domain "gc" -NewName #serverName

Set-TimeZone -Name "Eastern Standard Time"
Write-Host "EST Timezone Set" -ForegroundColor Green

# Allow Remote Desktop and uncheck the "Allow connections only from computers running Remote Desktop with Network Level Auth"
# This setting is verified through System Properties -> Remote
Set-ItemProperty -Path $RemotePath -Name "fDenyTSConnections" -Value 0
Write-Host "RDP enabled" -ForegroundColor Green

# Single Session disable(?)
Set-ItemProperty -Path $RemotePath -Name "fSingleSessionPerUser" -Value 0
Write-Host "RDP SingleSession disabled" -ForegroundColor Green

# Turn off Windows Firewall for all profiles
Set-NetFirewallProfile -Profile Domain,Public,Private -Enabled False
Write-Host "Firewall disabled" -ForegroundColor Green

Install-WindowsFeature SNMP-Service -IncludeManagementTools
Write-Host "SNMP Installed" -ForegroundColor Green

# Set power plan to High Performance
powercfg /s 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c
Write-Host "High Performance power plan set" -ForegroundColor Green

# Turn off IE Enhanced Security
$AdminKey = "HKLM:\SOFTWARE\Microsoft\Active Setup\Installed Components\{A509B1A7-37EF-4b3f-8CFC-4F3A74704073}"
$UserKey = "HKLM:\SOFTWARE\Microsoft\Active Setup\Installed Components\{A509B1A8-37EF-4b3f-8CFC-4F3A74704073}"
Set-ItemProperty -Path $AdminKey -Name "IsInstalled" -Value 0
Set-ItemProperty -Path $UserKey -Name "IsInstalled" -Value 0
Write-Host "IE Enhanced Security Configuration (ESC) has been disabled." -ForegroundColor Green
