# Configure Internet Explorer
# Based on - https://support.microsoft.com/en-us/help/182569/internet-explorer-security-zones-registry-entries-for-advanced-users

# Homepage
$path = 'HKCU:\Software\Microsoft\Internet Explorer\Main\'
$startPage = 'Start Page'
$value = '' #Enter desired homepage here

Set-Itemproperty -Path $path -Name $startPage -Value $value

# Enable Auto Detect for Local Intranet
set-location 'HKCU:\Software\Policies\Microsoft\Windows\CurrentVersion\Internet Settings\'
new-item ZoneMap -Force

$path = 'HKCU:\Software\Policies\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\'
$name = 'AutoDetect'
$value = 1

Set-Itemproperty -Path $path -Name $name -Value $value

# Configure Websites for Local Intranet and Trusted Sites
$localIntranetWebsites = @("") #Add websites here
$trustedWebsites = @("") #Add websites here

# Swap into registry location for our websites
set-location 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\'

#create the Domains folder for the websites
new-item Domains
set-location 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\'

# Add websites under registry hive for Domains
foreach($intranetSite in $localIntranetWebsites)
{
	new-item $intranetSite -Force

	# Add sites to Local Intranet for both HTTP and HTTPS
	new-itemproperty "HKCU:\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\$intranetSite" -Name * -Value 1 -Type DWORD -Force
}

foreach($trustedWebsite in $trustedWebsites)
{
	new-item $trustedWebsite -Force

	# Add sites to Trusted Sites for both HTTP and HTTPS
	new-itemproperty "HKCU:\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\$trustedWebsite" -Name * -Value 2 -Type DWORD -Force
}

# Define our Function to call when we need to set our ActiveX values
function Set_ActiveX($sitePath)
{
	# values to Enable for Active X
	$arrayToEnable = @("1200","1400","2401","2702","1208","1209","2201","2000","120A","270C","1405","1803","1604","2600","1608","1802","2100","1601","1606","2101","1409","1402")
	foreach ($element in $arrayToEnable)
	{
		Set-Itemproperty -Path $sitePath -Name $element -Value 0
	}

	# values to Prompt
	$arrayToPrompt = @("1001","1004","1201","2300","1609","1806","1804","1407")
	foreach ($element in $arrayToPrompt)
	{
		Set-Itemproperty -Path $sitePath -Name $element -Value 1
	}

	# values to Deny
	$arrayToDeny = @("2402","2400","2004","2001","120B","1406","2709","2708","1206","2102","120C","2104","1A04","160A","1607","270B","1809","2301","2103","2105")
	foreach ($element in $arrayToDeny)
	{
		Set-Itemproperty -Path $sitePath -Name $element -Value 3
	}
} # End of Set_ActiveX

# Define our Function to call when we need to set the user to automatically login when in certain websites
function Set_AutomaticLoginForIE($sitePath)
{
	Set-Itemproperty -Path $sitePath -Name "2007" -Value 65536 #Enable .NET Framework permissions
	Set-Itemproperty -Path $sitePath -Name "1A00" -Value 0 #Automatic Login
} # End of Set_AutomaticLoginForIE

# Set Active X for Internet by swapping to the Zone first then set the Automatic Login
$internetPath = 'HKCU:\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\3'
Set_ActiveX($internetPath)
Set_AutomaticLoginForIE($internetPath)

# Set ActiveX for Intranet by swapping to the Zone first then set the Automatic Login
$intranetPath = 'HKCU:\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\1'
Set_ActiveX($intranetPath)
Set_AutomaticLoginForIE($intranetPath)

# Set ActiveX for Trusted Sites by swapping to the Zone first then set the Automatic Login
$trustedSitePath = 'HKCU:\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\2'
Set_ActiveX($trustedSitePath)
Set_AutomaticLoginForIE($trustedSitePath)

# tell power button to shutdown
powercfg /setacvalueindex 381b4222-f694-41f0-9685-ff5bb260df2e 4f971e89-eebd-4455-a8de-9e59040e7347 7648efa3-dd9c-4e3e-b566-50f929386280 3
powercfg /setdcvalueindex 381b4222-f694-41f0-9685-ff5bb260df2e 4f971e89-eebd-4455-a8de-9e59040e7347 7648efa3-dd9c-4e3e-b566-50f929386280 3

# tell sleep button to do nothing
powercfg /setacvalueindex 381b4222-f694-41f0-9685-ff5bb260df2e 4f971e89-eebd-4455-a8de-9e59040e7347 96996bc0-ad50-47ec-923b-6f41874dd9eb 0
powercfg /setdcvalueindex 381b4222-f694-41f0-9685-ff5bb260df2e 4f971e89-eebd-4455-a8de-9e59040e7347 96996bc0-ad50-47ec-923b-6f41874dd9eb 0

# tell lid close to do nothing
powercfg /setacvalueindex 381b4222-f694-41f0-9685-ff5bb260df2e 4f971e89-eebd-4455-a8de-9e59040e7347 5ca83367-6e45-459f-a27b-476b1d01c936 0
powercfg /setdcvalueindex 381b4222-f694-41f0-9685-ff5bb260df2e 4f971e89-eebd-4455-a8de-9e59040e7347 5ca83367-6e45-459f-a27b-476b1d01c936 0

# Display Desktop Icons
$ErrorActionPreference = "SilentlyContinue"
If ($Error) {$Error.Clear()}
$RegistryPath = "HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced"

If (Test-Path $RegistryPath) {
	$Res = Get-ItemProperty -Path $RegistryPath -Name "HideIcons"
	If (-Not($Res)) {
		New-ItemProperty -Path $RegistryPath -Name "HideIcons" -Value "0" -PropertyType DWORD -Force | Out-Null
	}
	$Check = (Get-ItemProperty -Path $RegistryPath -Name "HideIcons").HideIcons
	If ($Check -NE 0) {
		New-ItemProperty -Path $RegistryPath -Name "HideIcons" -Value "0" -PropertyType DWORD -Force | Out-Null
	}
}

$RegistryPath = "HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons"
If (-Not(Test-Path $RegistryPath)) {
	New-Item -Path "HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer" -Name "HideDesktopIcons" -Force | Out-Null
	New-Item -Path "HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons" -Name "NewStartPanel" -Force | Out-Null
}

$RegistryPath = "HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel"
If (-Not(Test-Path $RegistryPath)) {
	New-Item -Path "HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons" -Name "NewStartPanel" -Force | Out-Null
}

If (Test-Path $RegistryPath) {

	## -- My Computer
	$Res = Get-ItemProperty -Path $RegistryPath -Name "{20D04FE0-3AEA-1069-A2D8-08002B30309D}"
	If (-Not($Res)) {
		New-ItemProperty -Path $RegistryPath -Name "{20D04FE0-3AEA-1069-A2D8-08002B30309D}" -Value "0" -PropertyType DWORD -Force | Out-Null
	}
	$Check = (Get-ItemProperty -Path $RegistryPath -Name "{20D04FE0-3AEA-1069-A2D8-08002B30309D}")."{20D04FE0-3AEA-1069-A2D8-08002B30309D}"
	If ($Check -NE 0) {
		New-ItemProperty -Path $RegistryPath -Name "{20D04FE0-3AEA-1069-A2D8-08002B30309D}" -Value "0" -PropertyType DWORD -Force | Out-Null
	}

	## -- User's Files
	$Res = Get-ItemProperty -Path $RegistryPath -Name "{59031a47-3f72-44a7-89c5-5595fe6b30ee}"
	If (-Not($Res)) {
		New-ItemProperty -Path $RegistryPath -Name "{59031a47-3f72-44a7-89c5-5595fe6b30ee}" -Value "0" -PropertyType DWORD -Force | Out-Null
	}
	$Check = (Get-ItemProperty -Path $RegistryPath -Name "{59031a47-3f72-44a7-89c5-5595fe6b30ee}")."{59031a47-3f72-44a7-89c5-5595fe6b30ee}"
	If ($Check -NE 0) {
		New-ItemProperty -Path $RegistryPath -Name "{59031a47-3f72-44a7-89c5-5595fe6b30ee}" -Value "0" -PropertyType DWORD -Force | Out-Null
	}
}
If ($Error) {$Error.Clear()}

# Enable hidden icons in system tray
$path = 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\'
$regEntry = 'EnableAutoTray'
$value = 0

Set-Itemproperty -Path $path -Name $regEntry -Value $value

# Configure Screensaver
Set-ItemProperty -Path "HKCU:\Control Panel\Desktop" -Name ScreenSaveActive -Value 1
Set-ItemProperty -Path "HKCU:\Control Panel\Desktop" -Name ScreenSaveTimeOut -Value 900
Set-ItemProperty -Path "HKCU:\Control Panel\Desktop" -Name scrnsave.exe -Value "c:\windows\system32\Ribbons.scr"

# Restart explorer to see changes
Stop-Process explorer.exe -Wait
