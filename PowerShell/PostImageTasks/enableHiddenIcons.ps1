# Enable hidden icons in system tray
$path = 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\'
$regEntry = 'EnableAutoTray'
$value = 0

Set-Itemproperty -Path $path -Name $regEntry -Value $value

# Restart explorer to see changes
Stop-Process explorer.exe