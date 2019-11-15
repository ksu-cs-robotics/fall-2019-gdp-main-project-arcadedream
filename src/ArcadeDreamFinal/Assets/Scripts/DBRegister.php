<?php
$server = "localhost";
$username = "root";
$password = "";
$database = "arcadedream";

$loginUser = $_POST["loginUser"];
$loginPassword = $_POST["loginPassword"];

$conn = new mysqli($server, $username, $password, $database);

if ($conn->connect_error) {
	die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT Username FROM playerlist WHERE Username='" . $loginUser . "'";
$result = $conn->query($sql);

if (!$result) {
	trigger_error("Invalid query: " . $conn->error);
}
if ($result->num_rows > 0) {
	echo "Username already in use";
}
else {
	$uuid = abs(crc32(uniqid()));
	$hash = password_hash($loginPassword, PASSWORD_DEFAULT);
	$sql2 = "INSERT INTO playerlist (ID, Username, UserPassword, Coins, Total_Points) VALUES ('" . $uuid . "','" . $loginUser . "','" . $hash . "',100,0)";
	
	if ($conn->query($sql2) == TRUE) echo "New record created";
	else echo "Error: " . $conn->err;
}
$conn->close();
?>