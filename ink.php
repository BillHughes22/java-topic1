<?php
$DBName = "mysql_28354_ppcs";
$DBUser = "pinuser468";
//$DBUser = "root";
$DBPass = "ppcs0987";
$DBHost = "my03.winhost.com";
//$DBHost = "localhost";

$dbcnx = mysql_connect ( $DBHost, $DBUser, $DBPass )
or die ( "<p>Unable to connect to the database server at this time.</p>" );

$gotDB = mysql_select_db ( $DBName )
or die ( "<p>Unable to locate the PINPOINT database at this time.</p >" );

?>
