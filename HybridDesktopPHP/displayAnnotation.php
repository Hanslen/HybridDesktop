<?php
		$servername = "localhost";
        $username = "root";
        $password = "password";//It's not the correct password.
	    //Create Connection
	    $db = mysqli_connect($servername, $username, $password);
	    $db_selected = mysqli_select_db($db, 'Hybrid_Desktop');
	    mysqli_query($db,"set names utf8");
		$username="Hanslen";
		$id = $_GET["id"];
		$status = $_GET["status"];
	 //    //Check connection
		if(!$db){
			die("Connection failed: " . mysqli_connect_error($db));
		}
		$query = "UPDATE onlineTexting SET show=".$status." WHERE id=".$id;
		// echo $query;
		// $query = "select * from onlineTexting";
		// $final = array();
		if($result = mysqli_query($db, $query)){
			$query = "select * from onlineTexting";
			$final = array();
			if($result = mysqli_query($db, $query)){
				while($row = mysqli_fetch_array($result, MYSQL_NUM)){
					if($row[4] == 1){
						$result2["id"] = $row[0];
						$result2["username"] = $row[1];
						$result2["text"] = $row[2];
						$result2["subject"] = $row[3];
						$result2["show"] = $row[4];
						array_push($final, $result2);
					}
				}
				echo json_encode($final[0], JSON_UNESCAPED_UNICODE);
			}
			else{
				print "Error - the query could not be executed" . mysqli_error($db) . " <br/>";
			}
		}
		else{
			print "Error - the query could not be executed" . mysqli_error($db) . " <br/>";
		}
		

		mysqli_close($db);
		?>
