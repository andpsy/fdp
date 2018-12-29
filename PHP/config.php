<?php
function create_link(){
	$dbhost = 'localhost';
	//$dbuser = 'rtermo93';
	$dbuser = 'root';
	//$dbpass = '4aaa75e6f23ad';
	$dbpass = 'sca';
	//$dbname = 'rtermo93_termodinamic';
	$dbname = 'fdp_0002';
	$link = mysqli_connect ($dbhost, $dbuser, $dbpass, $dbname);
	if (!$link) {
		die('Could not connect: ' . mysql_error());
	}
	return $link;
}
?>
