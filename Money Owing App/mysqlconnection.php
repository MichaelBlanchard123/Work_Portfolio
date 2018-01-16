<?php
	define("DB_HOST","localhost");
	define("DB_USER","root");
	define("DB_PASSWORD","itstudies12345");
	define("DB_NAME","money_owing_database");
	
	$dbc = @mysqli_connect(DB_HOST,DB_USER,DB_PASSWORD,DB_NAME) OR die("Could not connect to MySQL database: ".mysqli_connect_error());
?>