<?php
$server = "localhost";
$username = "root";
$password = "";
$database = "arcadedream";

$userID = $_POST["userID"];
$updateType = $_POST["updateType"];

$conn = new mysqli($server, $username, $password, $database);

if ($conn->connect_error) {
	die("Connection failed: " . $conn->connect_error);
}
$sql;
$sql2;
if ($updateType == "coins") {
	$coinAmount = $_POST["amount"];
	$sql2 = "UPDATE playerlist SET Coins=" . $coinAmount . " WHERE ID=" . $userID;
}
if ($updateType == "points") {
	$pointAmount = $_POST["amount"];
	$sql2 = "UPDATE playerlist SET Total_Points=" . $pointAmount . " WHERE ID=" . $userID;
}
if ($updateType == "game1") {
	$gameTime = $_POST["gameTime"];
	$highscore = $_POST["highscore"];
	$username = $_POST["username"];
	
	$sql = "SELECT ID FROM subgame1 WHERE ID='" . $userID . "'";
	$result = $conn->query($sql);
	
	if ($result->num_rows > 0) {
		$sql2 = "UPDATE subgame1 SET GameTime=" . $gameTime . ", Highscore=" . $highscore ." WHERE ID=" . $userID;
	}
	else $sql2 = "INSERT INTO subgame1 (GameTime, Highscore, Username, ID) VALUES (" . $gameTime ."," . $highscore .",'" . $username ."'," . $userID . ")";
}
if ($updateType == "game2") {
	$gameTime = $_POST["gameTime"];
	$highscore = $_POST["highscore"];
	$username = $_POST["username"];
	
	$sql = "SELECT ID FROM subgame2 WHERE ID='" . $userID . "'";
	$result = $conn->query($sql);
	
	if ($result->num_rows > 0) {
		$sql2 = "UPDATE subgame2 SET GameTime=" . $gameTime . ", Highscore=" . $highscore ." WHERE ID=" . $userID;
	}
	else $sql2 = "INSERT INTO subgame2 (GameTime, Highscore, Username, ID) VALUES (" . $gameTime ."," . $highscore .",'" . $username ."'," . $userID . ")";
}
if ($updateType == "game3") {
	$gameTime = $_POST["gameTime"];
	$highscore = $_POST["highscore"];
	$username = $_POST["username"];
	
	$sql = "SELECT ID FROM subgame3 WHERE ID='" . $userID . "'";
	$result = $conn->query($sql);
	
	if ($result->num_rows > 0) {
		$sql2 = "UPDATE subgame3 SET GameTime=" . $gameTime . ", Highscore=" . $highscore ." WHERE ID=" . $userID;
	}
	else $sql2 = "INSERT INTO subgame3 (GameTime, Highscore, Username, ID) VALUES (" . $gameTime ."," . $highscore .",'" . $username ."'," . $userID . ")";
}
if ($updateType == "game4") {
	$gameTime = $_POST["gameTime"];
	$highscore = $_POST["highscore"];
	$username = $_POST["username"];
	
	$sql = "SELECT ID FROM subgame4 WHERE ID='" . $userID . "'";
	$result = $conn->query($sql);
	
	if ($result->num_rows > 0) {
		$sql2 = "UPDATE subgame4 SET GameTime=" . $gameTime . ", Highscore=" . $highscore ." WHERE ID=" . $userID;
	}
	else $sql2 = "INSERT INTO subgame4 (GameTime, Highscore, Username, ID) VALUES (" . $gameTime ."," . $highscore .",'" . $username ."'," . $userID . ")";
}

$result = $conn->query($sql2);

if ($conn->query($sql2) == TRUE) echo "Operation successful";
else echo "Error: " . $conn->err;

$conn->close();
?>