Set-ItemProperty -Path "HKCU:\Control Panel\Desktop" -Name ScreenSaveActive -Value 1
Set-ItemProperty -Path "HKCU:\Control Panel\Desktop" -Name ScreenSaveTimeOut -Value 900
Set-ItemProperty -Path "HKCU:\Control Panel\Desktop" -Name scrnsave.exe -Value "c:\windows\system32\Ribbons.scr"