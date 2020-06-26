# Harrison Vu

# Set Homepage
$path = 'HKCU:\Software\Microsoft\Internet Explorer\Main\'
$startPage = 'Start Page'
$value = #homePageValue

Set-Itemproperty -Path $path -Name $startPage -Value $value
