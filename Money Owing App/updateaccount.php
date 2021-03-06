<?php
   /*
      Michael Blanchard
      Final Assignment - CSD320
      Description: 
   */
   session_start();
?>

<DOCTYPE html>
<html lang="en">
	<head>
		<meta http-equiv="Contant-Type" content="text/html; charset=utf-8"/>
		<meta name="viewport" content="width=device-width, initial-scale=1">
		<meta http-equiv="X-UA-Compatible" content="IE=edge" />
		<link rel="stylesheet" href="https://bootswatch.com/4/darkly/bootstrap.css" media="screen">
		
		<!--  jQuery -->
		<script type="text/javascript" src="https://code.jquery.com/jquery-1.11.3.min.js"></script>
		
		<!-- Bootstrap Date-Picker Plugin -->
		<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
		<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>
		
		<?php
			error_reporting(E_ALL); // Show all errors
			
			require_once('mysqlconnection.php'); //database connection
			
			$sql = "SELECT * FROM user WHERE user_id = ".$_SESSION["user_id"]." LIMIT 1";
			$result = mysqli_query($dbc, $sql);
			$user = mysqli_fetch_array($result);
			
			/* First Name Validation */
			if (isset($_POST['firstname']))
			{
				if(strlen($_POST['firstname']) == 0)
				{ 
					$firstname_error_message = 'Firstname is required!'; 
				}
				else
				{
					$firstname = filter_var($_POST['firstname'], FILTER_SANITIZE_STRING);
				}
			}
			
			/* Last Name Validation */
			if (isset($_POST['lastname']))
			{
				if(strlen($_POST['lastname']) == 0)
				{ 
					$lastname_error_message = 'Lastname is required!'; 
				}
				else
				{
					$lastname = filter_var($_POST['lastname'], FILTER_SANITIZE_STRING);
				}
			}
		
			/* Email Validation */
			if (isset($_POST['email']))
			{
				if($_POST['email'] != $user['user_email'])
				{
					if(strlen($_POST['email']) == 0)
					{ 
						$email_error_message = 'Email is required!'; 
					}
					elseif (!filter_var($_POST["email"], FILTER_VALIDATE_EMAIL)) 
					{
						$email_error_message = 'Invalid Email Address!';
					}
					else
					{
						$result = mysqli_query($dbc, "SELECT * FROM user WHERE user_email='".$_POST['email']."'");
						$num_rows = mysqli_num_rows($result);
						
						if($num_rows >= 1)
						{
							$email_error_message = 'This email has already been taken!';
						}
						else
						{
							$email = filter_var($_POST['email'], FILTER_SANITIZE_EMAIL);
						}
					}
				}
			}
			
			/* Password Validation */
			if (isset($_POST['password']))
			{
				if(strlen($_POST['password']) > 0)
				{
					if(strlen($_POST['password']) > 20 or strlen($_POST['password']) < 8)
					{ 
						$password_error_message = 'Must be 8-20 characters long!'; 
					}
					else
					{
						$password = filter_var($_POST['password'], FILTER_SANITIZE_STRING);
					}
				}
			}
			
			/* Password Verify Validation */
			if (isset($_POST['verifypassword']))
			{
				if($_POST['password'] != $_POST['verifypassword'])
				{ 
					$verifypassword_error_message = 'Passwords should be same!'; 
				}
				else
				{
					$verifypassword = filter_var($_POST['verifypassword'], FILTER_SANITIZE_STRING);
				}
			}
			
			if ( $_SERVER['REQUEST_METHOD'] == 'POST' )
			{
				if (!isset($firstname_error_message) && !isset($lastname_error_message) && !isset($email_error_message) //if no errors are found
				&& !isset($password_error_message) && !isset($verifypassword_error_message))
				{
					$salt = 'SALTING&)*$IS(#@$(AWESOME&';
					
					$firstname = $_POST['firstname'];
					$lastname = $_POST['lastname'];
					$email = $_POST['email'];
					$password = $_POST['password'];
					$dateofbirth = $_POST['dateofbirth'];
					
					if ( isset($password) && $password != "")
					{
						$sql="UPDATE user SET
						user_first_name='$firstname',
						user_last_name='$lastname',
						user_email='$email',
						password='".md5($password.$salt)."',
						user_date_of_birth='$dateofbirth'
						WHERE user_id = ".$_SESSION["user_id"];
						echo $sql;
					}
					else
					{
						$sql="UPDATE user SET
						user_first_name='$firstname',
						user_last_name='$lastname',
						user_email='$email',
						user_date_of_birth='$dateofbirth'
						WHERE user_id = ".$_SESSION["user_id"];
						echo $sql;
					}
				}
			}
			
			$firstname = $user['user_first_name'];
			$lastname = $user['user_last_name'];
			$email = $user['user_email'];
			$dateofbirth = $user['user_date_of_birth'];
		?>
		
		<style>
		.vertical-center {
		  min-height: 100%;
		  min-height: 100vh;

		  display: flex;
		  align-items: center;
		}
		</style>
	</head>
	<body id="home">
	
	<?php
	require('navbar.php');
	?>
		<section id="main">
			<div class="container">
				<form action="<?php echo $_SERVER['PHP_SELF']; ?>" method="post">
					<!-- <div class = "panel panel-default">
						<div class = "panel-body bg-info"> -->
							<div class="row">
								<div class="col-lg-12">
									<div class="jumbotron">
										<h1 class="text-center">Profile</h1>
									</div>
								</div>
							</div>
							<div class="row">
								<div class="col-md"></div>
									<div class="col-md-5 text-center">
										<img class="img-fluid" id="" src="img_chania.jpg" alt="Thumbnail" width="380" height="380" style="margin-bottom: 15px">
									</div>
								<div class="col-md"></div>
							</div>
							<div class="row">
								<div class="col-md"></div>
								<div class="col-md-5">
									<div class="form-group">
										<label for="firstnameinput">First Name:</label>
										<input name="firstname" type="text" class="form-control" id="firstnameinput" value="<?php if (isset($firstname)) echo $firstname; ?>" placeholder="firstname">
										<small id="firstnameHelp" class="text-danger"><?php if(isset($firstname_error_message)) echo $firstname_error_message; ?></small>
									</div>
								</div>
								<div class="col-md"></div>
							</div>
							<div class="row">
								<div class="col-md"></div>
								<div class="col-md-5">
									<div class="form-group">
										<label for="lastnameinput">Last Name:</label>
										<input name="lastname" type="text" class="form-control" id="lastnameinput" value="<?php if (isset($lastname)) echo $lastname; ?>" placeholder="lastname">
										<small id="lastnameHelp" class="text-danger"><?php if(isset($lastname_error_message)) echo $lastname_error_message; ?></small>
									</div>
								</div>
								<div class="col-md"></div>
							</div>
							<div class="row">
								<div class="col-md"></div>
								<div class="col-md-5">
									<div class="form-group">
										<label for="emailinput">Email:</label>
										<input name="email" type="text" class="form-control" id="emailinput" value="<?php if (isset($email)) echo $email; ?>" placeholder="example@email.com">
										<small id="emailHelp" class="text-danger"><?php if(isset($email_error_message)) echo $email_error_message; ?></small>
									</div>
								</div>
								<div class="col-md"></div>
							</div>
							<div class="row">
								<div class="col-md"></div>
								<div class="col-md-5">
									<div class="form-group">
										<label for="passwordinput">Password:</label>
										<button type="button" class="btn btn-link float-right" id="showhidelabel" onclick="ShowAndHidePassword()">Show</button>
										<input name="password" type="password" class="form-control" id="passwordinput" value="<?php if (isset($_POST['password'])) echo $_POST['password']; ?>" placeholder="password">
										<small id="passwordHelp" class="text-danger"><?php if(isset($password_error_message)) echo $password_error_message; ?></small>
									</div>
								</div>
								<div class="col-md"></div>
							</div>
							<div class="row">
								<div class="col-md"></div>
								<div class="col-md-5">
									<div class="form-group">
										<label for="verifypasswordinput">Verify Password:</label>
										<input name="verifypassword" type="password" class="form-control" id="verifypasswordinput" value="<?php if (isset($_POST['verifypassword'])) echo $_POST['verifypassword']; ?>" placeholder="verify password">
										<small id="verifypasswordHelp" class="text-danger"><?php if(isset($verifypassword_error_message)) echo $verifypassword_error_message; ?></small> <!-- First Name Error Message ERROR: Must be 8-20 characters long.-->
									</div>
								</div>
								<div class="col-md"></div>
							</div>
							<div class="row">
								<div class="col-md"></div>
								<div class="col-md-5">
									<div class="form-group">
										<label for="dateofbirthinput">Date of Birth:</label>
										<input name="dateofbirth" type="text" class="form-control" id="dateofbirthinput" value="<?php if (isset($dateofbirth)) echo $dateofbirth; ?>" placeholder="dd/mm/yyyy">
										<small id="dataofbirthHelp" class="text-danger"></small> <!-- First Name Error Message -->
									</div>
								</div>
								<div class="col-md"></div>
							</div>
							<div class="row">
								<div class="col-md"></div>
								<div class="col-md-5">
									<small id="dataofbirthHelp" class="text-muted">*All fields are required.</small>
								</div>
								<div class="col-md"></div>
							</div>
							<div class="row">
								<div class="col-md"></div>
									<div class="col-md-5">
										<button type="submit" class="btn btn-primary">Update Profile</button>
										<button type="button" class="btn btn-primary pull-right" onClick="document.location.href='loginscreen.php'">Back</button>
									</div>
								<div class="col-md"></div>
							</div>
						<!-- </div>
					</div> -->
				</form>
			</div>
		</section>
		<script>
			$(document).ready(function(){
			  var date_input=$('input[name="dateofbirth"]');
			  var container=$('.bootstrap-iso form').length>0 ? $('.bootstrap-iso form').parent() : "body";
			  var options={
				format: 'dd/mm/yyyy',
				startView: 2,
				endDate: '-13y',
				container: container,
				autoclose: true,
			  };
			  date_input.datepicker(options);
			})

			function ShowAndHidePassword()
			{
				var password = document.getElementById("passwordinput");
				var verifypassword = document.getElementById("verifypasswordinput");
				var showhide = document.getElementById("showhidelabel");
				
				if (password.type == "password") 
				{
					password.type = "text";
					verifypassword.type = "text";
					showhide.innerHTML  = "Hide";
				} 
				else 
				{
					password.type = "password";
					verifypassword.type = "password";
					showhide.innerHTML  = "Show";
				}
			}
		</script>
	<?php
	include('footer.php');
	?>