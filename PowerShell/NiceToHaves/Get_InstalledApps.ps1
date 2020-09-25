set-executionpolicy bypass

Get-WmiObject Win32_Product | where { $_.name -like "*" }

pause