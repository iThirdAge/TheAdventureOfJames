[CmdletBinding()]
param (
    [Parameter(Mandatory = $true, HelpMessage = "Run script with -Mode Win32 or -Mode MSI")]
    [ValidateNotNullOrEmpty()]
    [ValidateSet("Win32", "MSI")]
    [string]$Mode
)

Switch($Mode.toLower())
{
    "win32"
    {
        # Return Win32 Apps
        Get-WmiObject Win32_Product | Select-Object Name
    }

    "msi"
    {
        # Return installed MSI codes
        Get-WmiObject Win32_Product | Sort-Object -Property Name | Format-Table IdentifyingNumber, Name, LocalPackage -AutoSize
    }    
}
