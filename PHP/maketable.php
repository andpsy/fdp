<?php
function maketable($query, $fieldarray, $show_column_header, $div_id, $div_label)
{
	$link2 = create_link();
	echo "<div id='".$div_id."' class='login-sub-container'>";
	echo "<div id=\"login-sub-header\">";
		echo "<table style=\"width:100%;border-collapse:collapse;padding: 0px 0px 0px 0px; margin: 0px 0px 0px 0px;\">";
			echo "<tr>";
				echo "<td style=\"width:20px;border-width:0px;padding: 0px 0px 0px 0px; margin: 0px 0px 0px 0px;\">";
					echo "<form style=\"padding: 0px 0px 0px 0px; margin: 0px 0px 0px 0px;\" method=\"post\" id=\"print\" action=\"ExcelExport.php\" target=\"_blank\">";
						echo "<img src=\"16.png\" title=\"Print\" style=\"padding: 0px 10px 0px 0px;\" />";
					echo "</form>";
				echo "</td>";
				echo "<td style=\"width:20px;border-width:0px;padding: 0px 0px 0px 0px; margin: 0px 0px 0px 0px;\">";
					echo "<form style=\"padding: 0px 0px 0px 0px; margin: 0px 0px 0px 0px;\" method=\"post\" id=\"export_excel\" action=\"ExcelExport.php\" target=\"_blank\">";
						echo "<input type=\"hidden\" name=\"query\" id=\"query\" value=\"".$query."\" />";
						echo "<input type=\"image\" src=\"excel.png\" title=\"Export to Excel\" style=\"padding: 0px 10px 0px 0px;\" alt=\"Submit Form\" />";
					echo "</form>";
				echo "</td>";
				echo "<td style=\"text-align:center;border-width:0px;padding: 0px 0px 0px 0px; margin: 0px 0px 0px 0px;\">";
					echo "<label for=\"".$div_id."\">".$div_label."</label>";
				echo "</td>";
				echo "<td style=\"width:30px;border-width:0px;padding: 0px 0px 0px 0px; margin: 0px 0px 0px 0px;\">";
					echo "<img id=\"img2_".$div_id."\" src=\"08_descending.gif\" onclick=\"switchDiv('table_".$div_id."', 'img2_".$div_id."','null')\">";
				echo "</td>";
			echo "</tr>";
		echo "</table>";
	echo "</div>";

	echo "<table id=\"table_".$div_id."\" class=\"table-autosort:0 table-stripeclass:alternate\" style=\"display:block;width:1022px;\">";
	//count number of columns
	$columns = count($fieldarray);
	if($show_column_header)
	{
		echo "<thead>";
		echo "<tr>";
		for($x = 0; $x < $columns; $x++)
		{
			echo "<th ";
			switch($fieldarray[$x])
			{
				case "CURRENCY":
					echo "class=\"header table-sortable:default\" align=\"center\" style=\"width:65px;\"";
					break;
				case "DATE":
					echo "class=\"header table-sortable:date\" align=\"center\" style=\"width:75px;\"";
					break;
				case "CONCEPT":
					echo "class=\"header table-sortable:default\" align=\"left\" style=\"width:402px;\"";
					break;
				default:
					echo "class=\"header table-sortable:numeric\" align=\"right\" style=\"width:80px;\"";
					break;
			}
			echo ">";
			echo $fieldarray[$x]."</th>" ;
		}
		echo "</tr>";
		echo "</thead>";
	}

	//run the query
	$result = mysqli_query($link2, $query) or die("Error: ".mysqli_error()) ;
	$itemnum = mysqli_num_rows($result);
	if($itemnum > 0)
	{
		echo "<tbody>";
		while($items = mysqli_fetch_assoc($result))
		{
			echo "<tr>";
			for($x = 0; $x < $columns; $x++)
			{
				echo "<td";
				switch($fieldarray[$x])
				{
					case "CURRENCY":
					case "DATE":
						echo " align=\"center\"";
						break;
					case "CONCEPT":
						echo " align=\"left\"";
						break;
					default:
						echo " align=\"right\"";
						break;
				}
				echo ">";
				echo ((strval($items[$fieldarray[$x]])=="0" && strpos($fieldarray[$x], "BALLANCE") === false)?"":strval($items[$fieldarray[$x]]))."</td>" ;
			}
			echo "</tr>" ;
		}
		echo "</tbody>";
	}
	echo "</table>";
	echo "</div>";
	mysqli_free_result($result);
	mysqli_close($link2);
}
?>