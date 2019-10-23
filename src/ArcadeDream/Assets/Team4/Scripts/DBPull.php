<?php
$server = "localhost";
$username = "root";
$password = "";
$database = "arcadedream";

$userID = $_POST["userID"];

$conn = new mysqli($server, $username, $password, $database);

if ($conn->connect_error) {
	die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT ID, Username, Coins, Total_points FROM playerlist WHERE ID='" . $userID . "'";
$result = $conn->query($sql);

if (!$result) {
	trigger_error("Invalid query: " . $conn->error);
}
if ($result->num_rows > 0) {
	$rows = array();
	while($row = $result->fetch_assoc()) {
		$rows = $row;
	}
	print json_encode($rows);
}
else echo "ID does not exist";
$conn->close();
?>