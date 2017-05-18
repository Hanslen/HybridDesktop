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
		$text = $_GET["text"];
		$cate = $_GET["cate"];
		$query = "UPDATE onlineTexting SET text='".$text."' WHERE subject='".$cate."'";
		// $query = "select * from onlineTexting";
		// $final = array();
		if($text != "undefined"){
					if($result = mysqli_query($db, $query)){
						echo $result;
						echo json_encode($final, JSON_UNESCAPED_UNICODE);
					}
					else{
						print "Error - the query could not be executed" . mysqli_error($db) . " <br/>";
					}
		}

		mysqli_close($db);
		?>