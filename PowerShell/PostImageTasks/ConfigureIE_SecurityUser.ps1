# Harrison Vu

# Enable Auto Detect for Local Intranet
set-location 'HKCU:\Software\Policies\Microsoft\Windows\CurrentVersion\Internet Settings\'
new-item ZoneMap -Force

$path = 'HKCU:\Software\Policies\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\'
$name = 'AutoDetect'
$value = 1

Set-Itemproperty -Path $path -Name $name -Value $value

# Configure Trusted Sites under Local Intranet
$gcGov = '*.gc.gov'
$gwinnett = '*.gwinnettcounty.com'
set-location 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\'
new-item Domains
set-location 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\'
new-item $gcGov -Force
new-item $gwinnett -Force
new-itemproperty 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\*.gc.gov' -Name https -Value 1 -Type DWORD -Force
new-itemproperty 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\*.gc.gov' -Name http -Value 1 -Type DWORD -Force
new-itemproperty 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\*.gwinnettcounty.com' -Name https -Value 1 -Type DWORD -Force
new-itemproperty 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains\*.gwinnettcounty.com' -Name http -Value 1 -Type DWORD -Force

# Set Active X for Internet
$internetPath = 'HKCU:\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\3'

# values to Enable for Active X
$arrayToEnable = @("1200","1400","2401","2702","1208","1209","2201","2000","120A","270C","1405","1803","1604","2600","1608","1802","2100","1601","1606","2101","1409","1402")
foreach ($element in $arrayToEnable)
{
    Set-Itemproperty -Path $internetPath -Name $element -Value 0
}

# values to Prompt
$arrayToPrompt = @("1001","1004","1201","2300","1609","1806","1804","1407")
foreach ($element in $arrayToPrompt)
{
    Set-Itemproperty -Path $internetPath -Name $element -Value 1
}

# values to Deny
$arrayToDeny = @("2402","2400","2004","2001","120B","1406","2709","2708","1206","2102","120C","2104","1A04","160A","1607","270B","1809","2301","2103","2105")
foreach ($element in $arrayToPrompt)
{
    Set-Itemproperty -Path $internetPath -Name $element -Value 3
}

# random manual sets
Set-Itemproperty -Path $internetPath -Name "2007" -Value 65536
Set-Itemproperty -Path $internetPath -Name "1A00" -Value 0

# Set ActiveX for Intranet
$intranetPath = 'HKCU:\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\1'

# values to Enable for Active X
$arrayToEnable = @("1200","1400","2402","2400","2401","2702","1208","1209","2201","2000","120A","1001","1004","1201","270C","1405","1803","1604","2600","1608","1206","2102","120C","2104","1A04","1802","2100","160A","1806","1607","270B","1601","1606","2101","1407","2103","2105","1402","2707")
foreach ($element in $arrayToEnable)
{
    Set-Itemproperty -Path $intranetPath -Name $element -Value 0
}

# values to Prompt
$arrayToPrompt = @("1406","2300","1609","1804")
foreach ($element in $arrayToPrompt)
{
    Set-Itemproperty -Path $intranetPath -Name $element -Value 1
}

# values to Deny
$arrayToDeny = @("2500","2004","2001","120B","2709","2708","1809","2301","1409")
foreach ($element in $arrayToPrompt)
{
    Set-Itemproperty -Path $intranetPath -Name $element -Value 3
}

# random manual sets for Intranet
Set-Itemproperty -Path $intranetPath -Name "2007" -Value 65536
Set-Itemproperty -Path $intranetPath -Name "1A00" -Value 0
