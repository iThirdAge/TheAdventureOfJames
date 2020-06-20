# Harrison Vu

# Input vars
$myUser = $args[0]

# Add user to local admin group
Add-LocalGroupMember -Group "Administrators" -Member $myUser
