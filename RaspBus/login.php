<?php // login.php
$db_hostname = '127.0.0.1';
$db_database = 'TIMEBUS';
$db_username = 'USERS';
$db_password = 'USERS';


function db_connection($query) {
		$mysqli = mysqli_connect('127.0.0.1','USERS','USERS','TIMEBUS');
		$ret =mysqli_query($mysqli,$query);
		mysqli_close($mysqli);
		return $ret;
	}
?>