<html>
<head>
<script type="text/javascript" src="scripts/jquery-2.2.4.min.js"></script>
<script src="scripts/code.js"></script>

<link rel="stylesheet" href="style/styles.css">
<link rel="stylesheet" href="style/stylesStation.css">

</style>
</head>
<body onload="just1()">


<?php
require_once 'login.php';




if(isset($_POST['station']))
 {
	 $tname=$_POST['station'];
	$query = "SELECT NAME_STOP FROM  `MAINROUTE` inner join `STOPBUS` USING(ID_STOP_BUS) where TNUMBER = '".$tname."' and  ROUTE='прямо'";
	$data = db_connection($query);

	$queryStation="SELECT  `fend`, `lend` FROM `LASTSTATION` WHERE `ID_STOP_BUS`='".$tname."'";
	$dataStation=db_connection($queryStation);
	$rowS =  mysqli_fetch_array($dataStation);
	 echo '<header >
			<div> <article id="box">  
			<img src="https://cdn1.iconfinder.com/data/icons/flat-ui-free/128/time.png" />
			<article id="clock"></article>
			</article> </div>
			<h1>Рассписание троллейбуса №'.$tname. ' города Гродно</h1>

		</header>
		<section>
		<form action="time.php" method="post">
		<input type="hidden" name="route" value="" id="route"/>
		<input type="hidden" name="station" value='.$tname.' />
		<button type="submit" name="nameStop" value="" id="central"> set </button>

		</form>';
	
	 echo('<div class="centr"><section class="statiAll"><section class="stat1" id="st1">
	 	<header>
		<h2>'.$rowS['fend'].' - '.$rowS['lend'].'	</h2>
	 	</header>');
	 while($row = mysqli_fetch_array($data)){
	
echo <<<END
		
		<button  name="nameStop" value="$row[0]" > $row[0] </button></br>

		
END;
}
	echo('</section>');
	
	$query = "SELECT NAME_STOP FROM  `MAINROUTE` inner join `STOPBUS` USING(ID_STOP_BUS) where TNUMBER = '".$tname."' and  ROUTE='обратно'";
	$data= db_connection($query);
	
	 echo('<section class="stat2" id="st2"><header>
		<h2>'.$rowS['lend'].' - '.$rowS['fend'].'</h2>	
	 	</header>');
	 while($row = mysqli_fetch_array($data)){
	
echo <<<END
		
		<button name="nameStop" value="$row[0]" > $row[0] </button></br>

END;
}
	echo('</section></section></div>');

 }


 


?>
</section>
 </body>
 </html>