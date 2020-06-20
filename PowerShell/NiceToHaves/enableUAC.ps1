$path = 'HKLM:\Software\Microsoft\Windows\CurrentVersion\Policies\System'
$name = 'EnableLUA'
$value = '1'

Set-Itemproperty -Path $path -Name $name -Value $value