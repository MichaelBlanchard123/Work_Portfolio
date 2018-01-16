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
		
		<style>
		.form-inline > * 
		{
			margin:10px 7px;
		}
		
		.page-link 
		{
			position: relative;
			display: block;
			padding: 0.5rem 0.75rem;
			margin-left: -1px;
			line-height: 1.25;
			color: #fff;
			background-color: #355B7F;
		}
 
		.page-item.disabled .page-link 
		{
			color: #868e96;
			pointer-events: none;
			cursor: auto;
			background-color: #CEFFCE;
			border-color: #718393;
		}
 
		.page-item.active .page-link 
		{
			z-index: 1;
			color: #fff;
			background-color: #26415E;
			border-color: #AEFF5E;
		}
		 
		.page-link:focus, .page-link:hover 
		{
			color: #fff;
			text-decoration: none;
			background-color: #26415E;
			border-color: #AEFF5E;
		}
		#addtransaction
		{
			position: absolute;
			right: 0;
			bottom: 2px;
		}
		.modal-dialog 
		{
			min-height: calc(100vh - 80px);
			display: flex;
			flex-direction: column;
			justify-content: center;
			overflow: auto;
		}
		@media(max-width: 768px) 
		{
			.modal-dialog 
			{
				min-height: calc(100vh - 20px);
			}
		}
		</style>
		<?php 
			error_reporting(E_ALL); // Show all errors
			
			require_once('mysqlconnection.php'); //database connection
			$filter="All";
			$displayuser = "All";
			
			/* If transaction_type dropdown not set use default */
			if (!isset($_POST['transaction_type']))
			{
				$_POST['transaction_type'] = "Payable";
			}
			
			/* Transaction Description Validation */
			if (isset($_POST['transaction_description']))
			{
				if(strlen($_POST['transaction_description']) == 0)
				{ 
					$transaction_description_error_message = 'Transaction description is required!';
				}
				else
				{
					$transaction_description = $_POST['transaction_description'];
				}
			}
			
			/* Transaction Amount Validation */
			if (isset($_POST['transaction_amount']))
			{
				if(strlen($_POST['transaction_amount']) == 0)
				{ 
					$transaction_amount_error_message = 'Transaction amount is required!';
				}
				else if(!is_numeric($_POST['transaction_amount']))
				{
					$transaction_amount_error_message = 'Transaction amount must be numeric!';
				}
				else if(!preg_match("/^-?[0-9]+(?:\.[0-9]{1,2})?$/", $_POST['transaction_amount']))
				{
					$transaction_amount_error_message = 'Must follow currency format!';
				}
				else
				{
					$transaction_amount = $_POST['transaction_amount'];
				}
			}
			
			if ( $_SERVER['REQUEST_METHOD'] == 'POST' )
			{
				if (!isset($transaction_description_error_message) && !isset($transaction_amount_error_message))
				{
					$transaction_type = $_POST['transaction_type'];
					$salt = 'SALTING&)*$IS(#@$(AWESOME&';
					
					$sql3="INSERT INTO transactions
					(transaction_desc,
					transaction_amount,
					transaction_type,
					user_user_id)
					VALUES
					('$transaction_description',
					'$transaction_amount',
					'$transaction_type',
					'".$_SESSION["user_id"]."')";
					
					//mail($email, "Welcome to money owing app ".$firstname."!", wordwrap('Welcome aboard our money owing app! You can now enjoy accurately tracking your payments and payees. \n
					//If you have any questions regarding our service please contact us at DontContactUs@gmail.com', 70), 'From: <donnotreply@gmail.com>' . "\r\n");
					
					if(mysqli_query($dbc, $sql3))
					{
						echo "Records added successfully.";
					} 
					else
					{
						echo "ERROR: Could not able to execute $sql. " . mysqli_error($dbc);
					}
				}
			}
			
			if($_SESSION["user_isadmin"] == 1) 
			{
				$current_user = "";
				
				if (isset($_GET['displayuser']))
				{
					$displayuser = $_GET['displayuser'];
					if($displayuser != "All")
					{
						$current_user = " user_user_id=$displayuser ";
					}
				}
			}
			else
			{
				$current_user = " user_user_id=".$_SESSION['user_id']." ";
			}
			
			if ( isset($_GET['display']) && is_numeric($_GET['display']) ){
				$display = $_GET['display'];
			} else {
				$display = 10;
			}
			
			// FILTER LOGIC
			if ( isset($_GET['filter']) ){
				$filter = $_GET['filter'];
				switch ($filter){
					case "Payable":
						$where = " transaction_type LIKE 'Payable'";
						break;
					case "Receivable":
						$where = " transaction_type LIKE 'Receivable'";
						break;
					case "Resolved":
						$where = " transaction_type LIKE 'Resolved'";
						break;
					default:
						$where = "";
						break;
				}
			} else {
				$where = "";
			}
			
			if ( isset($_GET['p']) && is_numeric($_GET['p']) ){
				$pages = $_GET['p'];
			} else {
				//SELECT ALL ROWS
				$sql = "SELECT * FROM transactions ";
				if(strlen($current_user) != 0 || strlen($where) != 0) { $sql .= "WHERE"; }
				$sql .= $current_user;
				if(strlen($current_user) != 0 && strlen($where) != 0) { $sql .= "AND"; }
				$sql .= $where;
				$result = mysqli_query($dbc,$sql);
				$records = mysqli_num_rows($result);
				
				if ( $records > $display ){
					$pages = ceil($records/$display);
				} else {
					$pages = 1;
				}
			}
			
			if ( isset($_GET['s']) && is_numeric($_GET['s']) ){
				$start = $_GET['s'];
			} else {
				$start = 0;
			}
			
			// SORTING LOGIC
			if ( isset($_GET['sort']) ){
				$sort = $_GET['sort'];
				switch ($sort){
					case 'transaction_desc':
						$order_by = 'transaction_desc ASC';
						break;
					case 'transaction_amount':
						$order_by = 'transaction_amount ASC';
						break;
					case 'transaction_type':
						$order_by = 'transaction_type ASC';
						break;
					default:
						$order_by = 'user_user_id ASC';
						break;
				}
			} else {
				$sort="";
				$order_by = "transaction_id ASC";
			}
			
			$sql2 = "SELECT * FROM transactions ";
			if(strlen($current_user) != 0 || strlen($where) != 0) { $sql2 .= "WHERE"; }
			$sql2 .= $current_user;
			if(strlen($current_user) != 0 && strlen($where) != 0) { $sql2 .= "AND"; }
			$sql2 .= $where;
			$sql2 .=  " ORDER BY $order_by LIMIT $start,$display";
			$result2 = mysqli_query($dbc,$sql2);
			$num = mysqli_num_rows($result2);
			
			$sql3 = "SELECT * FROM user";
			$result3 = mysqli_query($dbc,$sql3);
			$num3 = mysqli_num_rows($result3);
		?>
	</head>
	<body id="home">
	
	<?php
	require('navbar.php');
	?>
	
			<div class="container">
				<div class="row">
					<div class="col-lg-12">
						<div class="jumbotron">
							<h1 class="text-center">Transactions</h1>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-lg-12">
						<div class="form-inline">
							<label>Number of rows:</label>
							<div class="dropdown show">
								<a class="btn btn-secondary dropdown-toggle" href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
									<?php if(isset($display)) echo $display; ?>
								</a>

								<div class="dropdown-menu" aria-labelledby="dropdownMenuLink">
									<a class="dropdown-item" href="dashboard.php?&sort=<?=$sort?>&s=<?=$start?>&p<?=$pages?>&filter=<?=$filter?>&display=10&displayuser=<?=$displayuser?>">10</a>
									<a class="dropdown-item" href="dashboard.php?&sort=<?=$sort?>&s=<?=$start?>&p<?=$pages?>&filter=<?=$filter?>&display=20&displayuser=<?=$displayuser?>">20</a>
									<a class="dropdown-item" href="dashboard.php?&sort=<?=$sort?>&s=<?=$start?>&p<?=$pages?>&filter=<?=$filter?>&display=50&displayuser=<?=$displayuser?>">50</a>
								</div>
							</div>
							<label>Filter by:</label>
							<div class="dropdown show">
								<a class="btn btn-secondary dropdown-toggle" href="#" role="button" id="dropdownMenuLink1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
									<?php if(isset($filter)) echo $filter; ?>
								</a>

								<div class="dropdown-menu" aria-labelledby="dropdownMenuLink1">
									<a class="dropdown-item" href="dashboard.php?&sort=<?=$sort?>&s=<?=$start?>&p<?=$pages?>&filter=All&display=<?=$display?>&displayuser=<?=$displayuser?>">All</a>
									<a class="dropdown-item" href="dashboard.php?&sort=<?=$sort?>&s=<?=$start?>&p<?=$pages?>&filter=Payable&display=<?=$display?>&displayuser=<?=$displayuser?>">Payable</a>
									<a class="dropdown-item" href="dashboard.php?&sort=<?=$sort?>&s=<?=$start?>&p<?=$pages?>&filter=Receivable&display=<?=$display?>&displayuser=<?=$displayuser?>">Receivable</a>
									<a class="dropdown-item" href="dashboard.php?&sort=<?=$sort?>&s=<?=$start?>&p<?=$pages?>&filter=Resolved&display=<?=$display?>&displayuser=<?=$displayuser?>">Resolved</a>
								</div>
							</div>
							<?php
								if($_SESSION["user_isadmin"] == 1) 
								{
							?>
							<label>User:</label>
							<div class="dropdown show">
								<a class="btn btn-secondary dropdown-toggle" href="#" role="button" id="dropdownMenuLink2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
									<?php if(isset($displayuser)) echo $displayuser; ?>
								</a>

								<div class="dropdown-menu" aria-labelledby="dropdownMenuLink2">
									<?php
										if ($num3 > 0)
										{
											echo '<a class="dropdown-item" href="dashboard.php?&sort='.$sort.'&s='.$start.'&p='.$pages.'&filter='.$filter.'&display='.$display.'&displayuser=All">All</a>';
											while ($row = mysqli_fetch_array($result3))
											{
												echo '<a class="dropdown-item" href="dashboard.php?&sort='.$sort.'&s='.$start.'&p='.$pages.'&filter='.$filter.'&display='.$display.'&displayuser='.$row['user_id'].'">'.$row['user_first_name'].'</a>';
											}
										}
									?>
								</div>
							</div>
							<?php
								}
							?>
							<?=(!empty($sort) ? '<p>Sorting by: '.$sort.' - <a href="dashboard.php?&s='.$start.'&p'.$pages.'&filter='.$filter.'">Remove Sorting</a></p>' : '');?>
							<button type="submit" class="btn btn-primary float-right" id="addtransaction" data-toggle="modal" data-target="#myModal">Add Transaction</button>
						</div>
					</div>
				</div>
				 <div class="row">
					<div class="col-lg-12">
						<table class="table">
							<thead>
								<tr>
									<th><a href="dashboard.php?sort=transaction_desc&s=<?=$start?>&p<?=$pages?>&filter=<?=$filter?>&display=<?=$display?>&displayuser=<?=$displayuser?>">Transaction Info</a></th>
									<th><a href="dashboard.php?sort=transaction_amount&s=<?=$start?>&p<?=$pages?>&filter=<?=$filter?>&display=<?=$display?>&displayuser=<?=$displayuser?>">Amount</a></th>
									<th><a href="dashboard.php?sort=transaction_type&s=<?=$start?>&p<?=$pages?>&filter=<?=$filter?>&display=<?=$display?>&displayuser=<?=$displayuser?>">Type</a></th>
									<?php if($_SESSION["user_isadmin"] == 1) { ?>
									<th><a href="dashboard.php?sort=user_user_id&s=<?=$start?>&p<?=$pages?>&filter=<?=$filter?>&display=<?=$display?>&displayuser=<?=$displayuser?>">User ID</a></th>
									<?php } ?>
									<th></th>
								</tr>
							</thead>
							<tbody>
							<?php
								if ($num > 0)
								{
									while ($row = mysqli_fetch_array($result2))
									{
										if($row['transaction_type'] == 'Payable') 
										{
											$string = '<tr class="table-danger"';
										}
										elseif($row['transaction_type'] == 'Receivable') 
										{
											$string = '<tr class="table-success"';
										}
										else
										{
											$string = '<tr';
										}
										
										$string .= '><td>'.$row['transaction_desc'].
										'</td><td>'.$row['transaction_amount'].
										'</td><td>'.$row['transaction_type'];
										
										if($_SESSION['user_isadmin'] == 1) 
										{
											$string .= '</td><td>'.$row['user_user_id'];
										}
										$string .= '</td><td><a class="text-white" href="edit.php?user_id='.$row['user_user_id'].'">Edit</a> <a onclick="openModal('.$row['transaction_id'].')">Delete</a></td></tr>';
										
										echo $string;
									}
								} 
								else 
								{
									echo '<tr><td colspan="6" style="text-align:center">No transactions listed! Click "Add Transaction" button to start creating entries.</td></tr>';
								}
								
								if ( $pages > 1 ) 
								{
									$current_page = ($start/$display)+1;
								
							?>
							<script type="text/javascript">

							function openModal(x)
							{
								$('#myModal2').modal();
								document.getElementById('myField').value = x;
								document.getElementById('delete').submit();
							}       
							</script>
							<tr>
								<td colspan=5>
									<nav aria-label="Page navigation">
										<ul class="pagination justify-content-center">
										<?php if ( $current_page != 1 ) { ?>
										<li class="page-item">
											<a class="page-link" href="dashboard.php?s=<?=($start-$display)?>&p=<?=$pages?>&sort=<?=$sort?>&filter=<?=$filter?>&display=<?=$display?>&displayuser=<?=$displayuser?>" aria-label="Previous">
												<span aria-hidden="true">&laquo; Previous</span>
											</a>
										</li>
										<?php }
											for ( $i=1;$i<=$pages;$i++ ){
										?>
										<li<?=($i==$current_page ? ' class="page-item active"':'')?>><a class="page-link" href="dashboard.php?s=<?=($display*($i-1))?>&p=<?=$pages?>&sort=<?=$sort?>&filter=<?=$filter?>&display=<?=$display?>&displayuser=<?=$displayuser?>"><?=$i?></a></li>
										<?php } ?>
										<?php if ( $current_page != $pages ) { ?>
										<li class="page-item">
											<a class="page-link" href="dashboard.php?s=<?=($start+$display)?>&p=<?=$pages?>&sort=<?=$sort?>&filter=<?=$filter?>&display=<?=$display?>&displayuser=<?=$displayuser?>" aria-label="Next">
												<span aria-hidden="true">Next &raquo;</span>
											</a>
										</li>
										<?php } ?>
										</ul>
									</nav>
								</td>
							</tr>
							<?php
								}
							?>
							</tbody>
						</table>
					</div>
				</div>
			</div>
			<!-- The Add Transaction Modal -->
			<div class="modal <?php if (isset($transaction_description_error_message) || isset($transaction_amount_error_message)){} else { echo" fade"; } ?>" id="myModal">
				<div class="modal-dialog">
					<div class="modal-content">
						<form action="<?php echo $_SERVER['PHP_SELF']; ?>" method="post">
							<!-- Modal Header -->
							<div class="modal-header">
								<h2 class="modal-title">Add Transaction</h2>
								<button type="button" class="close" data-dismiss="modal">&times;</button>
							</div>
							<!-- Modal body -->
							<div class="modal-body">
								<div class="form-group">
									<label for="exampleInputEmail1">Transaction Description</label>
									<input type="text" name="transaction_description" class="form-control" id="transactiondescriptioninput" value="<?php if (isset($_POST['transaction_description'])) echo $_POST['transaction_description']; ?>" placeholder="Enter Description"/>
									<small id="transactiondescriptionHelp" class="text-danger"><?php if(isset($transaction_description_error_message)) echo $transaction_description_error_message; ?></small>
								</div>
								<div class="form-group">
									<label for="exampleInputPassword1">Transaction Amount</label>
									<div class="input-group">
										<input type="text" name="transaction_amount" class="form-control" id="transactionamountinput" value="<?php if (isset($_POST['transaction_amount'])) echo $_POST['transaction_amount']; ?>" placeholder="99.99"/>
										<div class="input-group-append">
											<span class="input-group-text text-white">$</span>
										</div>
									</div>
									<small id="transactionamountHelp" class="text-danger"><?php if(isset($transaction_amount_error_message)) echo $transaction_amount_error_message; ?></small>
								</div>
								<div class="form-group">
									<label for="transactiontypeselectbox">Transaction Type</label>
									<select class="form-control col-sm-4" id="transactiontypeselectbox" name="transaction_type">
										<option <?php if($_POST['transaction_type']=="Payable") echo 'selected="selected"'; ?> value="Payable">Payable</option>
										<option <?php if($_POST['transaction_type']=="Receivable") echo 'selected="selected"'; ?> value="Receivable">Receivable</option>
										<option <?php if($_POST['transaction_type']=="Resolved") echo 'selected="selected"'; ?> value="Resolved">Resolved</option>
									</select>
								</div>
							</div>
							<!-- Modal footer -->
							<div class="modal-footer">
								<button type="submit" class="btn btn-primary">Add</button>
								<button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="document.getElementById('transactiondescriptioninput').value = ''; 
								document.getElementById('transactionamountinput').value = '';
								document.getElementById('transactiondescriptionHelp').innerHTML = '';
								document.getElementById('transactionamountHelp').innerHTML = '';">Cancel</button>
							</div>
						</form>
					</div>
				</div>
			</div>
			<!-- The Add Transaction Modal -->
			 <div id="myModal2" class="modal fade" role="dialog">
				<div class="modal-dialog">
					<!-- Modal content-->
					<div class="modal-content">
						<div class="modal-header">       
							<h4 class="modal-title">Delete Record</h4>
						</div>
						<div class="modal-body">
						<form action="<?php echo $_SERVER['PHP_SELF']; ?>" method="post" id="delete">
							<input type="hidden" id="myField" value="" />
						</form>
							<p>Are you sure you want to delete this record?</p>
						</div>
						<div class="modal-footer">
							<button onclick="delete()" class="btn btn-danger">Delete</button>
							<button type="submit" name="delete" class="btn btn-secondary" data-dismiss="modal">Close</button>
						</div>
					</div>
				</div>
			</div>
			<?php
			if ( $_SERVER['REQUEST_METHOD'] == 'POST' )
			{
				if (isset($transaction_description_error_message) || isset($transaction_amount_error_message))
				{
					?>
					<script>
					$(document).ready(function(){
					$("#myModal").modal();
					});
					</script>
					<?php
				}
			}
			?>
		</div>
	</section>
	<?php
	include('footer.php');
	?>