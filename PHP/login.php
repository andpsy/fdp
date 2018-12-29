<?php
	require "config.php";
	$link = create_link();
	$user = $_POST["user"];
	$pass = $_POST["pass"];
	$login_ok = true;
	$new_access = true;
	if((isset($user) && !empty($user) && $user!="" && $user!=null) && (isset($pass) && !empty($pass) && $pass!="" && $pass!=null))
	{
		$new_access = false;
		// test user and pass ....
		$q = "call OWNERSsp_LOGIN('".strval($user)."', '".strval($pass)."')";
		$result = mysqli_query($link, $q) or die(mysqli_error()) ;
		$row = mysqli_fetch_assoc($result);
		$owner_id = $row["ID"];
		mysqli_free_result($result);
		mysqli_close($link);
		if(isset($owner_id) && !empty($owner_id) && $owner_id!="" && $owner_id!=null && $owner_id > 0)
		{
			$login_ok = true;
			$request_uri = str_replace("login.php", "IEReport.php?id=".strval($owner_id), $_SERVER['REQUEST_URI']);
			header( "location: ".$request_uri ) ;
		}
		else
		{
			$login_ok = false;
		}
	}
	else
	{
		$new_access = true;
	}
	if($new_access || !$login_ok)
	{
		if(!$login_ok )
		{
			echo "<html><body>Invalild login!</body></html>";
		}
?>

	<html dir="ltr">
	<head>
		<link href="css.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<img class="background-image" src="login-whisp.png">

		<div id="content-container">
			<div id="login-container">
				<div id="login-sub-container">
					<div id="login-sub-header">
						<img src="logo.gif" alt="logo" />
					</div>
					<div id="login-sub">
						<div id="forms">
							<form id="login_form" action="<?php echo $_SERVER['PHP_SELF']; ?>" method="post">
								<div class="input-req-login"><label for="user">Username</label></div>
								<div class="input-field-login icon username-container">
									<input name="user" id="user" autofocus="autofocus" value="" placeholder="Enter your username." class="std_textbox" type="text"  tabindex="1" required>
								</div>
								<div style="margin-top:30px;" class="input-req-login"><label for="pass">Password</label></div>
								<div class="input-field-login icon password-container">
									<input name="pass" id="pass" placeholder="Enter your account password." class="std_textbox" type="password" tabindex="2"  required>
								</div>
								<div style="width: 285px;">
									<div class="login-btn">
										<button name="login" type="submit" id="login_submit" tabindex="3">Log in</button>
									</div>
								</div>
								<div class="clear" id="push"></div>
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
	</body>
	</html>
<?php
	}
?>
