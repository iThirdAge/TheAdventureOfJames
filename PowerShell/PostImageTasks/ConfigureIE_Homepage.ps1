# Set Homepage
$path = 'HKCU:\Software\Microsoft\Internet Explorer\Main\'
$startPage = 'Start Page'
$value = 'https://www.gwinnettcounty.com/web/gwinnett/Home'

Set-Itemproperty -Path $path -Name $startPage -Value $value