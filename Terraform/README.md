Install Terraform at
https://learn.hashicorp.com/tutorials/terraform/install-cli

This will be mainly from a Windows perspective as most of my work as of recent (4/2022) is on Mac and Linx. I wish to preserve my knowledge of Windows administration

For Windows, the setting of environment variables can be done as follows

`$Env:<variable-name> = "<new-value>"`

This will preserve the variable for just the instance of the Powershell window open, similar to Mac/Linux console command `export`

When you install Terraform on Windows, you will need to add the executable to your PATH variable. You can do so through editing directly in the Registry if you wish for Systemwide. For User you need HKCU but beware, this is the current user using Powershell.

`$PATH = "HKLM:\SYSTEM\CurrentControlSet\Control\Session Manager\Environment"`
`Set-ItemProperty -Path $RegPath -Name PATH -Value "$Env:PATH += '<Path_To_TerraformExe>'"`

Alternatively, edit through Control Panel -> System Properties -> Environment Variables 
Append under System or User variables depending on what kind of installation you want. This will prevent the `terraform` command from being lost