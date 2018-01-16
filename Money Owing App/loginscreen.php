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
		
		<!-- bootswatch theme -->
		<link rel="stylesheet" href="https://bootswatch.com/4/darkly/bootstrap.css" media="screen">
		
		<?php 
			error_reporting(E_ALL); // Show all errors
			
			require_once('mysqlconnection.php'); //database connection
			
			if (isset($_POST['email']) and isset($_POST['password']))
			{
				$email = filter_var($_POST['email'], FILTER_SANITIZE_EMAIL);
				$password = filter_var($_POST['password'], FILTER_SANITIZE_STRING);
				$rememberme = (isset($_POST['rememberme']) ? 1 : 0);
				
				$salt = 'SALTING&)*$IS(#@$(AWESOME&';
				$sql_query="SELECT * FROM user WHERE user_email='$email' AND user_password='".md5($password.$salt)."' LIMIT 1";
				
				$result = mysqli_query($dbc,$sql_query);
				$num = mysqli_num_rows($result);
				$row = mysqli_fetch_array($result);
				
				if ($num > 0) 
				{
					$_SESSION["user_id"] = $row["user_id"];
					$_SESSION["user_first_name"] = $row["user_first_name"];
					$_SESSION['user_isadmin'] = $row["user_isadmin"];
					$time = time() + (86400 * 30);
					
					if($rememberme == 1) //add cookie for Remember Me radio button.
					{
						setcookie('cookie_email', $email, $time);
                        setcookie('cookie_password', $password, $time);
					}
					else
					{
						setcookie('cookie_email', "", $time);
                        setcookie('cookie_password', "", $time);
					}
					
					header('Location: dashboard.php');
				}
				else
				{
					echo "Invalid email and password combination!";
				}
			}
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
		<section id="main">
			<div class="jumbotron vertical-center">
				<div class="container">
					<form action="<?php echo $_SERVER['PHP_SELF']; ?>" method="post">
						<!-- <div class = "panel panel-default">
							<div class = "panel-body bg-info"> -->
								<div class="row">
									<div class="col-md"></div>
										<div class="col-md-5"><img src="money_app_logo.png" class="img-responsive" alt="Responsive image" width="350px;"></div>
									<div class="col-md"></div>
								</div>
								<div class="row">
									<div class="col-md"></div>
										<div class="col-md-5">
											<h1 class="display-2">Login<h1>
										</div>
									<div class="col-md"></div>
								</div>
								<div class="row">
									<div class="col-md"></div>
									<div class="col-md-5">
											<div class="form-group">
												<label for="emailinput">Enter Email:</label>
												<input name="email" type="text" class="form-control" id="emailinput" value="<?php if(isset($_COOKIE['cookie_email'])) { echo $_COOKIE['cookie_email']; } elseif(isset($_POST['email'])) { echo $_POST['email']; } ?>" placeholder="example@email.com">
											</div>
									</div>
									<div class="col-md"></div>
								</div>
								<div class="row">
									<div class="col-md"></div>
									<div class="col-md-5">
											<div class="form-group">
												<label for="passwordinput">Enter Password:</label>
												<input name="password" type="password" class="form-control" id="passwordinput" value="<?php if(isset($_COOKIE['cookie_password'])) { echo $_COOKIE['cookie_password']; } elseif(isset($_POST['password'])) { echo $_POST['password']; } ?>" placeholder="password">
											</div>
									</div>
									<div class="col-md"></div>
								</div>
								<div class="row">
									<div class="col-md"></div>
										<div class="col-md-5">
											<div class="checkbox">
												<label><input name="rememberme" type="checkbox" <?php if (isset($_POST['rememberme'])) echo'checked="checked"'?>>Remember Me</label>
												<a class="nav-link float-right" href="registeraccount.php">Register Profile</a>
											</div>
										</div>
									<div class="col-md"></div>
								</div>
								<div class="row">
									<div class="col-md"></div>
										<div class="col-md-5"><button type="submit" class="btn btn-primary">Login</button></div>
									<div class="col-md"></div>
								</div>
							<!-- </div>
						</div> -->
					</form>
				</div>
			</div>
		</section>
		
	<?php
	include('footer.php');
	?>