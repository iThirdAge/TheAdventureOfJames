# Harrison Vu

$path = 'HKLM:\Software\Microsoft\WindowsUpdate\UX\Settings'
$element = 'TrayIconVisibility'
$value = 0
Set-Itemproperty -Path $path -Name $element -Value $value
