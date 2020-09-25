# Turn off Intranet Compatibility View in IE
$path = 'HKCU:\SOFTWARE\Microsoft\Internet Explorer\BrowserEmulation'
$name = 'IntranetCompatibilityMode'
$value = 0

Set-Itemproperty -Path $path -Name $name -Value $value