<?php
require "config.php";
require "maketable.php";
echo "<html dir=\"ltr\">";
echo "<head>";
echo "<script type=\"text/javascript\" src=\"common.js\"></script>";
echo "<script type=\"text/javascript\" src=\"jquery.js\"></script>";
echo "<script type=\"text/javascript\" src=\"table.js\"></script>";
echo "<script type=\"text/javascript\" src=\"datepicker.js\"></script>";
echo "<link href=\"report.css\" rel=\"stylesheet\" type=\"text/css\" />";
echo "<link href=\"datepicker.css\" rel=\"stylesheet\" type=\"text/css\" />";
echo "</head>";
echo "<body>";
echo "<img class=\"background-image\" src=\"login-whisp.png\">";
$owner_id = $_REQUEST["id"];
if(!isset($owner_id) || empty($owner_id) || $owner_id == "" || $owner_id ==null)
	$owner_id = $_POST["owner_id"];

$property = $_POST["property"];
$source = strval($_POST["source"]);
$type = $_POST["type"];
$currency = $_POST["currency"];
$status = $_POST["status"];
$start_date = $_POST["start_date"];
$end_date = $_POST["end_date"];
?>

    <div id="content-container" style="width:1024px;">
        <div id="login-container">
        	<!--
 			style="height:260px;"
        	-->
            <div id="login-sub-container">
                <div id="login-sub-header">
                	<?php
						$link3 = create_link();
						$result = mysqli_query($link3, "call OWNERSsp_get_by_id(".$owner_id.")") or die("Error: ".mysqli_error()) ;
						$row = mysqli_fetch_assoc($result);
						echo $row["FULL_NAME"];
						mysqli_free_result($result);
						mysqli_close($link3);
                	?>
                	<img id="img1" src="08_descending.gif" onclick="switchDiv('login-sub', 'img1', 'report_content')">
                </div>
                <div id="login-sub" style="height:200px;display:block;">
                    <div id="forms">
                        <form id="login_form" action="<?php echo $_SERVER['PHP_SELF']; ?>" method="post">
                        	<table style="width:100%;">
                        		<tr>
                        			<td style="border: 0px solid #ffffff;">
										<div class="input-req-login">
											<label for="property">Property:</label>
										</div>
									</td>
                        			<td style="border: 0px solid #ffffff;">
										<div class="input-req-login">
											<select name="property" id="property" class="combobox" tabindex="1">
												<option value="">-- ALL --</option>
												<?php
													$link = create_link();
													$result = mysqli_query($link, "call PROPERTIESsp_list_by_owner_id(".strval($owner_id).")") or die(mysqli_error()) ;
													while($items = mysqli_fetch_assoc($result))
													{
														if($items["ID"] != -1)
														{
															echo "<option value=\"".$items["ID"]."\"";
															echo ((isset($property) && !empty($property) && strval($property)!="" && $property!=null && strval($property)!="-1" && strval($property)!="-- ALL --") && $property==$items["ID"])?" selected=\"selected\"":"";
															echo ">".$items["NAME"]."</option>";
														}
													}
													mysqli_free_result($result);
													mysqli_close($link);
												?>
											</select>
										</div>
									</td>

                        			<td style="border: 0px solid #ffffff;">
										<div class="input-req-login">
											<label for="currency">Currency:</label>
										</div>
									</td>
                        			<td style="border: 0px solid #ffffff;">
										<div class="input-req-login">
											<select name="currency" id="currency" class="combobox" tabindex="4">
												<?php
													$link = create_link();
													$result = mysqli_query($link, "call OWNERSsp_GET_CURRENCIES(".strval($owner_id).")") or die(mysqli_error()) ;
													while($items = mysqli_fetch_assoc($result))
													{
														echo "<option value=\"".$items["CURRENCY"]."\"";
														echo ((isset($currency) && !empty($currency) && strval($currency)!="" && $currency!=null && strval($currency)!="-1" && strval($currency)!="-- ALL --") && $currency==$items["CURRENCY"])?" selected=\"selected\"":"";
														echo ">".$items["CURRENCY"]."</option>";
													}
													mysqli_free_result($result);
													mysqli_close($link);
												?>
											</select>
										</div>
									</td>
								</tr>
								<tr>
									<td style="border: 0px solid #ffffff;">
										<div class="input-req-login">
											<label for="source">Source:</label>
										</div>
									</td>
									<td style="border: 0px solid #ffffff;">
										<div class="input-req-login">
											<select name="source" id="source" class="combobox" tabindex="2">
												<?php
												echo "<option value=\"-1\">-- ALL --</option>";
												echo "<option value=\"FALSE\"";
												echo ((isset($source) && !empty($source) && strval($source)!="" && $source!=null && strval($source)!="-1" && strval($source)!="-- ALL --") && $source=="FALSE")?" selected=\"selected\"":"";
												echo ">CASH</option>";
												echo "<option value=\"TRUE\"";
												echo ((isset($source) && !empty($source) && strval($source)!="" && $source!=null && strval($source)!="-1" && strval($source)!="-- ALL --") && $source=="TRUE")?" selected=\"selected\"":"";
												echo ">BANK</option>";
												?>
											</select>
										</div>
									</td>

                        			<td style="border: 0px solid #ffffff;">
										<div class="input-req-login">
											<label for="status">Status:</label>
										</div>
									</td>
                        			<td style="border: 0px solid #ffffff;">
										<div class="input-req-login">
											<select name="status" id="status" class="combobox" tabindex="5">
												<option value="">-- ALL --</option>
												<?php
													$link = create_link();
													$result = mysqli_query($link, "call LISTSsp_select_by_list_type_name('ie_status')") or die(mysqli_error()) ;
													while($items = mysqli_fetch_assoc($result))
													{
														if($items["ID"] != -1)
														{
															echo "<option value=\"".$items["ID"]."\"";
															echo ((isset($status) && !empty($status) && strval($status)!="" && $status!=null && strval($status)!="-1" && strval($status)!="-- ALL --") && $status==$items["ID"])?" selected=\"selected\"":"";
															echo ">".$items["NAME"]."</option>";
														}
													}
													mysqli_free_result($result);
													mysqli_close($link);
												?>
											</select>
										</div>
									</td>

								</tr>
								<tr>
									<td style="border: 0px solid #ffffff;">
										<div class="input-req-login">
											<label for="type">Type:</label>
										</div>
									</td>
									<td style="border: 0px solid #ffffff;">
										<div class="input-req-login">
											<select name="type" id="type" class="combobox" tabindex="3">
												<?php
												echo "<option value=\"-1\">-- ALL --</option>";
												echo "<option value=\"FALSE\"";
												echo ((isset($type) && !empty($type) && strval($type)!="" && $type!=null && strval($type)!="-1" && strval($type)!="-- ALL --") && $type=="FALSE")?" selected=\"selected\"":"";
												echo ">EXPENSE</option>";
												echo "<option value=\"TRUE\"";
												echo ((isset($type) && !empty($type) && strval($type)!="" && $type!=null && strval($type)!="-1" && strval($type)!="-- ALL --") && $type=="TRUE")?" selected=\"selected\"":"";
												echo ">INCOME</option>";
												?>
											</select>
										</div>
									</td>

									<td style="border: 0px solid #ffffff;">
										<div class="input-req-login">
											<label for="start_date">Start date:</label>
										</div>
									</td>
									<td style="border: 0px solid #ffffff;" nowrap>
										<div class="input-req-login">
											<?php
											echo "<input class=\"date_textbox\" name=\"start_date\" value=\"";
											echo (isset($start_date) && !empty($start_date) && strval($start_date)!="" && $start_date!=null && strval($start_date)!="-1" && strval($start_date)!="-- ALL --")?$start_date:"";
											echo "\" required />";
											?>
											<input type="button" value="..." onclick="displayDatePicker('start_date',false,'ymd','-');">
										</div>
									</td>

								</tr>
								<tr>
									<td colspan="2" style="border: 0px solid #ffffff;">
										<div style="width: 285px;">
											<div class="login-btn">
												<button name="report" type="submit" id="login_submit" tabindex="3">Generate report</button>
											</div>
										</div>
										<input type="hidden" name="owner_id" id="owner_id" value="<?php echo $owner_id; ?>" />
										<div class="clear" id="push"></div>
									</td>

									<td style="border: 0px solid #ffffff;">
										<div class="input-req-login">
											<label for="end_date">End date:</label>
										</div>
									</td>
									<td style="border: 0px solid #ffffff;" nowrap>
										<div class="input-req-login">
											<?php
											echo "<input class=\"date_textbox\" name=\"end_date\" value=\"";
											echo (isset($end_date) && !empty($end_date) && strval($end_date)!="" && $end_date!=null && strval($end_date)!="-1" && strval($end_date)!="-- ALL --")?$end_date:"";
											echo "\" required />";
											?>
											<input type="button" value="..." onclick="displayDatePicker('end_date',false,'ymd','-');">
										</div>
									</td>

								</tr>
							</table>
                        </form>
                    <!--CLOSE forms -->
                    </div>
                <!--CLOSE login-sub -->
                </div>
            <!--CLOSE login-sub-container -->
            </div>
        <!--CLOSE login-container -->
        </div>
	</div>
	<div id="report_content" style="position:absolute;top:260px;width:1024px;text-align:center;">
<?php
try
{
	if(!isset($_POST["owner_id"]) || empty($_POST["owner_id"])) return;

	$fieldarray = array("CURRENCY","DATE","CONCEPT","INCOME BANK","INCOME CASH","EXPENSE BANK","EXPENSE CASH","BALLANCE BANK","BALLANCE CASH");
	if($currency == "-- ALL --")
	{
		$link = create_link();
		$result = mysqli_query($link, "call OWNERSsp_GET_CURRENCIES(".strval($owner_id).")") or die(mysqli_error()) ;
		while($items = mysqli_fetch_assoc($result))
		{
			if($items["CURRENCY"] != "-- ALL --")
			{
				if($status == "")  //all
				{
					$link1 = create_link();
					$result2 = mysqli_query($link1, "call LISTSsp_select_by_list_type_name('ie_status')") or die(mysqli_error()) ;
					while($items2 = mysqli_fetch_assoc($result2))
					{
						if($items2["ID"] != -1)
						{
							$query = "call OWNERSsp_IE_DETAILS_REPORT(".strval($owner_id);
							$query .= ((isset($property) && !empty($property) && strval($property)!="" && $property!=null && strval($property)!="-1" && strval($property)!="-- ALL --")?", ".$property : ", NULL");
							$query .= (isset($source) && !empty($source) && $source!=null && strval($source)!="-1")?", ".$source : ", NULL";
							$query .= (isset($type) && !empty($type) && strval($type)!="" && $type!=null && strval($type)!="-1" && strval($type)!="-- ALL --")?", ".$type : ", NULL";
							$query .= ", '".$items["CURRENCY"]."'";
							$query .= ", ".$items2["ID"];
							$query .= (isset($start_date) && !empty($start_date) && strval($start_date)!="" && $start_date!=null)?", '".$start_date."'" : ", NULL";
							$query .= (isset($end_date) && !empty($end_date) && strval($end_date)!="" && $end_date!=null)?", '".$end_date."'" : ", CURDATE()";
							$query .= ")";
							maketable($query, $fieldarray, true, $items["CURRENCY"]."_".$items2["ID"], "Currency: ".$items["CURRENCY"]." --- I/E Status: ".$items2["NAME"]);
						}
					}
					mysqli_free_result($result2);
					mysqli_close($link1);
				}
				else
				{
					$query = "call OWNERSsp_IE_DETAILS_REPORT(".strval($owner_id);
					$query .= ((isset($property) && !empty($property) && strval($property)!="" && $property!=null && strval($property)!="-1" && strval($property)!="-- ALL --")?", ".$property : ", NULL");
					$query .= (isset($source) && !empty($source) && $source!=null && strval($source)!="-1")?", ".$source : ", NULL";
					$query .= (isset($type) && !empty($type) && strval($type)!="" && $type!=null && strval($type)!="-1" && strval($type)!="-- ALL --")?", ".$type : ", NULL";
					$query .= ", '".$items["CURRENCY"]."'";
					$query .= (isset($status) && !empty($status) && strval($status)!="" && $status!=null && strval($status)!="-1" && strval($status)!="-- ALL --")?", ".$status : ", NULL";
					$query .= (isset($start_date) && !empty($start_date) && strval($start_date)!="" && $start_date!=null)?", '".$start_date."'" : ", NULL";
					$query .= (isset($end_date) && !empty($end_date) && strval($end_date)!="" && $end_date!=null)?", '".$end_date."'" : ", CURDATE()";
					$query .= ")";
					maketable($query, $fieldarray, true, $items["CURRENCY"]."_".$status, "Currency: ".$items["CURRENCY"]." --- I/E Status: ".$status);
				}
			}
		}
		mysqli_free_result($result);
		mysqli_close($link);
	}
	else
	{
		if($status == "")  //all
		{
				$link1 = create_link();
				$result2 = mysqli_query($link1, "call LISTSsp_select_by_list_type_name('ie_status')") or die(mysqli_error()) ;
				while($items2 = mysqli_fetch_assoc($result2))
				{
					if($items2["ID"] != -1)
					{
						$query = "call OWNERSsp_IE_DETAILS_REPORT(".strval($owner_id);
						$query .= ((isset($property) && !empty($property) && strval($property)!="" && $property!=null && strval($property)!="-1" && strval($property)!="-- ALL --")?", ".$property : ", NULL");
						$query .= (isset($source) && !empty($source) && $source!=null && strval($source)!="-1")?", ".$source : ", NULL";
						$query .= (isset($type) && !empty($type) && strval($type)!="" && $type!=null && strval($type)!="-1" && strval($type)!="-- ALL --")?", ".$type : ", NULL";
						$query .= (isset($currency) && !empty($currency) && strval($currency)!="" && $currency!=null && strval($currency)!="-1" && strval($currency)!="-- ALL --")?", '".$currency."'" : ", NULL";
						$query .= ", ".$items2["ID"];
						$query .= (isset($start_date) && !empty($start_date) && strval($start_date)!="" && $start_date!=null)?", '".$start_date."'" : ", NULL";
						$query .= (isset($end_date) && !empty($end_date) && strval($end_date)!="" && $end_date!=null)?", '".$end_date."'" : ", CURDATE()";
						$query .= ")";
						maketable($query, $fieldarray, true, $currency."_".$items2["ID"], "Currency: ".$currency." --- I/E Status: ".$items2["NAME"]);
					}
				}
				mysqli_free_result($result2);
				mysqli_close($link1);
		}
		else
		{
			$query = "call OWNERSsp_IE_DETAILS_REPORT(".strval($owner_id);
			$query .= ((isset($property) && !empty($property) && strval($property)!="" && $property!=null && strval($property)!="-1" && strval($property)!="-- ALL --")?", ".$property : ", NULL");
			$query .= (isset($source) && !empty($source) && $source!=null && strval($source)!="-1")?", ".$source : ", NULL";
			$query .= (isset($type) && !empty($type) && strval($type)!="" && $type!=null && strval($type)!="-1" && strval($type)!="-- ALL --")?", ".$type : ", NULL";
			$query .= (isset($currency) && !empty($currency) && strval($currency)!="" && $currency!=null && strval($currency)!="-1" && strval($currency)!="-- ALL --")?", '".$currency."'" : ", NULL";
			$query .= (isset($status) && !empty($status) && strval($status)!="" && $status!=null && strval($status)!="-1" && strval($status)!="-- ALL --")?", ".$status : ", NULL";
			$query .= (isset($start_date) && !empty($start_date) && strval($start_date)!="" && $start_date!=null)?", '".$start_date."'" : ", NULL";
			$query .= (isset($end_date) && !empty($end_date) && strval($end_date)!="" && $end_date!=null)?", '".$end_date."'" : ", CURDATE()";
			$query .= ")";
			maketable($query, $fieldarray, true, $currency."_".$status, "Currency: ".$currency." --- I/E Status: ".$status);
		}
	}
}
catch(Exception $e)
{
	echo $e->getMessage();
}
?>
	</div>
</body>
</html>
