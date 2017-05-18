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
	 //    //Check connection
		if(!$db){
			die("Connection failed: " . mysqli_connect_error($db));
		}
		$query = "DELETE FROM tasks WHERE id=".$id;
		// echo $query;
		// $query = "select * from onlineTexting";
		// $final = array();
		if($result = mysqli_query($db, $query)){
			$query = "select * from tasks";
			$final = array();
			if($result = mysqli_query($db, $query)){
				while($row = mysqli_fetch_array($result, MYSQL_NUM)){
						$result2["id"] = $row[0];
						$result2["username"] = $row[1];
						$result2["task"] = $row[2];
						$result2["category"] = $row[3];
						$result2["time"] = $row[4];
						if($row[5] == "1"){
							$result2["status"] = true;
						}
						else{
							$result2["status"] = false;
						}
						// $result2["status"] = $row[5];
						// $tempjson = json_encode($result2, JSON_UNESCAPED_UNICODE);
						array_push($final, $result2);
						// echo $final;
				}
				echo json_encode($final, JSON_UNESCAPED_UNICODE);
						// echo print_r($final);
			}
			// echo $result;
			// echo json_encode($final, JSON_UNESCAPED_UNICODE);
		}
		else{
			print "Error - the query could not be executed" . mysqli_error($db) . " <br/>";
		}
		

		mysqli_close($db);
		?>
