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
		$query = "SELECT distinct category from tasks;";
		$final = array();
		if($result = mysqli_query($db, $query)){
			while($row = mysqli_fetch_array($result, MYSQL_NUM)){
					$result2["category"] = $row[0];
					array_push($final, $result2);
					// echo $final;
			}
			echo json_encode($final, JSON_UNESCAPED_UNICODE);
		}
		else{
			print "Error - the query could not be executed" . mysqli_error($db) . " <br/>";
		}
		mysqli_close($db);
		?>
