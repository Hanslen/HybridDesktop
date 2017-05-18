<?php
		$servername = "localhost";
        $username = "root";
        $password = "password";//It's not the correct password.
	    //Create Connection
	    $db = mysqli_connect($servername, $username, $password);
	    $db_selected = mysqli_select_db($db, 'Hybrid_Desktop');
	    mysqli_query($db,"set names utf8");
		$email = $_GET["email"];
		$feedback = $_GET["feedback"];
	 //    //Check connection
		if(!$db){
			die("Connection failed: " . mysqli_connect_error($db));
		}
		$text = $_GET["text"];
		$query = "INSERT INTO feedback (email,feedback) VALUES ('".$email."','".$feedback."')";
		// echo $query;
		// $query = "select * from onlineTexting";
		// $final = array();
		if($result = mysqli_query($db, $query)){
			echo "Success";
		}
		else{
			print "Error - the query could not be executed" . mysqli_error($db) . " <br/>";
		}
		

		mysqli_close($db);
		?>
