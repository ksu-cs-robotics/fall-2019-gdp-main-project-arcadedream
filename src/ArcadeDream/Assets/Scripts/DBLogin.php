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

$sql = "SELECT UserPassword FROM playerlist WHERE Username='" . $loginUser . "'";
$result = $conn->query($sql);

if (!$result) {
	trigger_error("Invalid query: " . $conn->error);
}
if ($result->num_rows > 0) {
	while($row = $result->fetch_assoc()) {
		if (password_verify(@loginPassword, $row["UserPassword"])) echo "Login Successful";
		else echo "Wrong Password";
	}
}
else echo "Username does not exist";
$conn->close();
?>