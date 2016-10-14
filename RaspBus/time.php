<html>
<head>
<script type="text/javascript" src="scripts/jquery-2.2.4.min.js"></script>
<script src="scripts/code.js"></script>

<link rel="stylesheet" href="style/styles.css">
<link rel="stylesheet" type="text/css" href="style/styleTable.css">
</head>
<body >

<?php


require_once 'login.php';
if(isset($_POST['station']) && isset($_POST['nameStop']) && isset($_POST['route']))
{
	 $tname=$_POST['station'];
	 $tnameStop=$_POST['nameStop'];
	 $troute=$_POST['route'];

	 $queryStation="SELECT  `fend`, `lend` FROM `LASTSTATION` WHERE `ID_STOP_BUS`='".$tname."'";
	 $dataStation=db_connection($queryStation);
		$rowS =  mysqli_fetch_array($dataStation);
		$resRoute=$rowS['fend'] ."-". $rowS['lend'];
	if($troute=="прямо"){
		$resRoute=$rowS['lend'] ."-". $rowS['fend'];
	}
	 echo('<header >
			<div> <article id="box">  
			<img src="https://cdn1.iconfinder.com/data/icons/flat-ui-free/128/time.png" />
			<article id="clock"></article>
			</article> </div>
			<h1>Рассписание троллейбуса №'.$tname. ' города Гродно</h1>
			<h3>Остановка: '.$tnameStop. '</h3>
			<h3>Направление: '.$resRoute. ' </h3>
		</header>
		
		<div class="centr"><section class="statiAll">');




	 echo ("$tname   $ttime  $troute"); 
		$dbc = mysqli_connect($db_hostname,$db_username,$db_password,$db_database);
		$query = "SELECT  `WEEKDAYS`, `WEEKEND` FROM `MAINROUTE` inner join STOPBUS USING(`ID_STOP_BUS`) WHERE `TNUMBER`='$tname' AND `NAME_STOP`='$tnameStop' and  ROUTE='$troute';";
		$data = db_connection($query);
		$row = mysqli_fetch_array($data);
		$day = $row[0];
		$wday = $row[1];
	
	
	
		$query = "SELECT * FROM `TIMETRANSPORT` WHERE `ID_TIME`='$day' or `ID_TIME`='$wday' ORDER BY `ID_TIME`";
	
		$data = db_connection($query);
		$table1='<table class="table1"><thead><tr>
			<td>  </td><td>5</td><td>6</td><td>7</td><td>8</td><td>9</td><td>10</td><td>11</td><td>12</td> 
			<td>13</td><td>14</td><td>15</td><td>16</td><td>17</td><td>18</td><td>19</td><td>20</td><td>21</td><td>22</td><td>23</td><td>0</td><td>1</td> 	
			</tr></thead><tbody >';
		$result = mysqli_query($dbc,"SELECT * FROM `TIMETRANSPORT` WHERE `ID_TIME`='$day'");
		$result1 = mysqli_query($dbc,"SELECT * FROM `TIMETRANSPORT` WHERE `ID_TIME`='$wday'");
		$row1 = mysqli_fetch_row($result);
		$row2 = mysqli_fetch_row($result1);
			$coun = count($row1);
			$table1 .='<tr><td>буд.</td>'; 
			
			for ($j=2;$j<$coun;$j++)
					{
						$table1.="<td>$row1[$j]</td>"; 
					}
			$table1.='</tr><tr class="wens"><td>вых.</td>'; 
			for ($j=2;$j<$coun;$j++)
					{
						$table1.="<td>$row2[$j]</td>"; 
					}		
				$table1.= "</tr></tbody></table>";	
		echo ($table1);


	
		$table2='<table class="table2"><thead><tr>
			<td>  </td><td>буд.</td><td class="wens">вых.</td> 
			
		</tr></thead><tbody >';
		
		
		for ($j=5,$k=2;$j!="2";$j++,$k++)
		{
			if($row1[$k]!="" || $row2[$k]){
			$table2.='<tr><td>'.$j.'</td><td>'.$row1[$k].'</td><td class="wens">'.$row2[$k].'</td></tr>'; 
			}
			if($j=="23"){
				$j=-1;
			}
			
		}
			echo($table2);
			
		
				

}
 


 


?>
</section></div>
 </body>
 </html>