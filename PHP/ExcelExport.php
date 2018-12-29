<?php
require "config.php";
function xlsBOF()
{
	echo pack("ssssss", 0x809, 0x8, 0x0, 0x10, 0x0, 0x0);
	return;
}
function xlsEOF()
{
	echo pack("ss", 0x0A, 0x00);
	return;
}
function xlsWriteNumber($Row, $Col, $Value)
{
	echo pack("sssss", 0x203, 14, $Row, $Col, 0x0);
	echo pack("d", $Value);
	return;
}
function xlsWriteLabel($Row, $Col, $Value )
{
	$L = strlen($Value);
	echo pack("ssssss", 0x204, 8 + $L, $Row, $Col, 0x0, $L);
	echo $Value;
	return;
}

function generateExcel1()
{
	$query = $_POST["query"];
	header("Pragma: public");
	header("Expires: 0");
	header("Cache-Control: must-revalidate, post-check=0, pre-check=0");
	header("Content-Type: application/force-download");
	header("Content-Type: application/octet-stream");
	header("Content-Type: application/download");;
	header("Content-Disposition: attachment;filename=list.xls");
	header("Content-Transfer-Encoding: binary");
	$link = create_link();
	$result=mysqli_query($link, $query);

	$xlsCol = 1;
	while($finfo = mysqli_fetch_field($result))
	{
		xlsWriteLabel(1,$xlsCol,$finfo->name);
		$xlsCol++;
	}
	xlsBOF();
	$xlsRow = 2;
	while($row = mysqli_fetch_row($result))
	{
		$xlsCol = 1;
		while($finfo = mysqli_fetch_field($result))
		{
			switch($finfo->name)
			{
				case "CURRENCY":
				case "DATE":
				case "CONCEPT":
					xlsWriteLabel($xlsRow,$xlsCol,$row[$finfo->name]);
					break;
				default:
					xlsWriteNumber($xlsRow,$xlsCol,$row[$finfo->name]);
					break;
			}
			$xlsCol++;
		}
		$xlsRow++;
	}
	xlsEOF();
	mysqli_free_result($result);
	mysqli_close($link);
}

function generateExcel2()
{
	try
	{
		$query = $_POST["query"];

		header("Content-Type: application/xls");
		header("Content-Disposition: attachment; filename=list2.xls");
		header("Pragma: no-cache");
		header("Expires: 0");

		$sep = "\t"; //tabbed character
		$link = create_link();
		$result = mysqli_query($link, $query);

		while($finfo = mysqli_fetch_field($result))
		{
			echo $finfo->name . $sep;
		}
		echo "\n";

		while($row = mysqli_fetch_row($result))
		{
			$schema_insert = "";
			for($j=0; $j<mysqli_num_fields($result);$j++)
			{
				if(!isset($row[$j]))
					$schema_insert .= "NULL".$sep;
				elseif ($row[$j] != "")
					$schema_insert .= $row[$j].$sep;
				else
					$schema_insert .= "".$sep;
			}
			$schema_insert = str_replace($sep."$", "", $schema_insert);
			$schema_insert = preg_replace("/\r\n|\n\r|\n|\r/", " ", $schema_insert);
			$schema_insert .= "\t";
			echo trim($schema_insert);
			echo "\n";
		}
		mysqli_free_result($result);
		mysqli_close($link);
	}
	catch(Exception $e)
	{
		echo $e->getMessage();
	}
}


function generateExcel3()
{
	try
	{
		$query = $_POST["query"];
		header('Content-type: application/ms-excel');
		header('Content-Disposition: attachment; filename=list3.xls');
		$sep = "\t"; //tabbed character
		$link = create_link();
		$result = mysqli_query($link, $query);
		$content = "";
		while($finfo = mysqli_fetch_field($result))
		{
			$content .= (($finfo->name) . $sep);
		}
		$content .= "\n";
		while($row = mysqli_fetch_row($result))
		{
			$schema_insert = "";
			for($j=0; $j<mysqli_num_fields($result);$j++)
			{
				if(!isset($row[$j]))
					$schema_insert .= "NULL".$sep;
				elseif ($row[$j] != "")
					$schema_insert .= $row[$j].$sep;
				else
					$schema_insert .= "".$sep;
			}
			//$schema_insert = str_replace($sep."$", "", $schema_insert);
			//$schema_insert = preg_replace("/\r\n|\n\r|\n|\r/", " ", $schema_insert);
			//$schema_insert .= "\t";
			//echo trim($schema_insert);
			//echo "\n";

			$content .= $schema_insert;
			$contnet .= "\n";
		}
		echo $content;
		mysqli_free_result($result);
		mysqli_close($link);
	}
	catch(Exception $e)
	{
		echo $e->getMessage();
	}
}

generateExcel3();
?>