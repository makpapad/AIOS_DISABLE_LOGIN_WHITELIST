This project is a Windows application built with VB.NET that connects to a Linux server via SSH to modify the wp-config.php file of a WordPress installation. The primary functionality is to toggle the setting AIOS_DISABLE_LOGIN_WHITELIST between true and false. This setting controls the login whitelisting feature in the "All In One Security" (AIOS) plugin for WordPress.

Features:
Connects to a Linux server using SSH with private key authentication.
Reads and modifies the wp-config.php file to update the AIOS_DISABLE_LOGIN_WHITELIST value.
Displays the current state of the setting (true or false) to the user.
Allows users to edit the server details and save them in an encoded file for future use.
User-friendly interface with a toggle function for easily changing the setting.
This tool is useful for WordPress administrators who need to manage security settings without manually editing server files.
