	<section id="main">
		<div class="container">
			<div class="row">
				<div class="col-lg-12">
					<nav class="navbar navbar-expand-lg navbar-dark bg-dark">
						<img src="money_app_logo.png" class="img-responsive" alt="Responsive image" width="110px;">
						<button class="navbar-toggler collapsed" type="button" data-toggle="collapse" data-target="#navbarColor02" aria-controls="navbarColor02" aria-expanded="false" aria-label="Toggle navigation" style="">
							<span class="navbar-toggler-icon"></span>
						 </button>
						<div class="navbar-collapse collapse" id="navbarColor02" style="">
							<ul class="navbar-nav mx-auto">
								<?php
								if(!isset($_SESSION["user_isadmin"])) {
								?>
								<li class="nav-item">
									<a class="nav-link" href="#"><h5>Home</h5></a>
								</li>
								<li class="nav-item">
									<a class="nav-link" href="#"><h5>Features</h5></a>
								</li>
								<li class="nav-item">
									<a class="nav-link" href="#"><h5>Pricing</h5></a>
								</li>
								<?php 
								} else {
								?>
								<li class="nav-item">
									<a class="nav-link" href="dashboard.php"><h5>Transactions</h5></a>
								</li>
								<li class="nav-item">
									<a class="nav-link" href="updateaccount.php"><h5>Profile</h5></a>
								</li>
								<?php 
								}
								?>
								<li class="nav-item">
									<a class="nav-link" href="#"><h5>Contact Us</h5></a>
								</li>
							</ul>
							
							<?php
							if(!isset($_SESSION["user_isadmin"])) {
							?>
							<button type="button" class="btn btn-primary pull-right" onClick="document.location.href='loginscreen.php'">Log In</button>
							<button type="button" class="btn btn-primary pull-right" onClick="document.location.href='registeraccount.php'">Register</button>
							<?php 
							} else {
							?>
							<a class="nav-link float-right text-white" href="updateaccount.php">Welcome Back, <?php echo $_SESSION["user_first_name"] ?></a>
							<button type="button" class="btn btn-primary pull-right" onClick="document.location.href='loginscreen.php'">Log Out</button>
							<?php 
							}
							?>
						</div>
					</nav>
				</div>
			</div>
	
	<script src="https://bootswatch.com/_vendor/jquery/dist/jquery.min.js"></script>
    <script src="https://bootswatch.com/_vendor/popper.js/dist/umd/popper.min.js"></script>
    <script src="https://bootswatch.com/_vendor/bootstrap/dist/js/bootstrap.min.js"></script>