<html>
<head>
<script type="text/javascript" src="scripts/jquery-2.2.4.min.js"></script>
<script src="scripts/code.js"></script>
 <link rel="stylesheet" href="style/styles.css">
</head>
<body onload="just()">
<header >
<div> <article id="box">  
  
  <img src="https://cdn1.iconfinder.com/data/icons/flat-ui-free/128/time.png" />
  
  <article id="clock"></article>
  
</article> </div>
<h1>Рассписание троллейбусов города Гродно</h1>
</header>
<section>

<form  action="stations.php" method="POST">
		<button type="submit" name="station" value='' id="central"> set </button>
</form>
<div id="mainSect">
<?php


require_once 'login.php';

$query = "SELECT DISTINCT TNUMBER FROM MAINROUTE;";
$data = db_connection($query);
while($row = mysqli_fetch_array($data)){	
	echo '<button  name="station"  value='.$row[0].'>'. $row[0] .'</button>	';	
}

echo ('</section>');
 ?>
 </div>
 <footer>
 </footer>
 </body>
 </html>