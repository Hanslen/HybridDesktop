<?php 
		$servername = "localhost";
        $username = "root";
        $password = "password";//It's not the correct password.
	    //Create Connection
	    $db = mysqli_connect($servername, $username, $password);
	    $db_selected = mysqli_select_db($db, 'Hybrid_Desktop');
	    mysqli_query($db,"set names utf8");

	 //    //Check connection
		if(!$db){
			die("Connection failed: " . mysqli_connect_error($db));
		}
		// echo $categoryId;
		//check is correct or not
		$cate=$_GET["cate"];
		$query = "select * from onlineTexting WHERE subject='".$cate."'";
		$final = array();
		if($result = mysqli_query($db, $query)){
			while($row = mysqli_fetch_array($result, MYSQL_NUM)){
					$result2["id"] = $row[0];
					$result2["username"] = $row[1];
					$result2["text"] = $row[2];
					$result2["subject"] = $row[3];
					$result2["show"] = $row[4];
					if($row[4] == 1){
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
		else{
			print "Error - the query could not be executed" . mysqli_error($db) . " <br/>";
		}
		mysqli_close($db);
		?>