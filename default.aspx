<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" %>

<!DOCTYPE html>
<html lang="en">
    <head runat="server">
        <meta charset="utf-8" />
	    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
	    <meta name="viewport" content="width=device-width, initial-scale=1" />
	    <meta name="description" content="Shipment Inspection App" />
		<meta name="author" content="Barian Systems" />
		<meta name="keyword" content="shipping, web app" />
        <link rel="manifest" href="ico/manifest.json" />
        <meta name="msapplication-TileColor" content="#ffffff" />
        <meta name="msapplication-TileImage" content="ico/ms-icon-144x144.png" />
        <meta name="theme-color" content="#ffffff" />

        <!--<link rel="shortcut icon" href="favicon.ico" type="image/x-icon">
        <link rel="icon" href="favicon.ico" type="image/x-icon">-->

	    <title>PIDS</title>

	    <!-- Bootstrap core CSS -->
        <link href="css/bootstrap.min.css" rel="stylesheet" />

        <!-- Remove following comment to add Right to Left Support or add class rtl to body -->
        <!-- <link href="css/bootstrap-rtl.min.css" rel="stylesheet"> -->

        <link href="css/jquery.mmenu.css" rel="stylesheet" />
        <link href="css/simple-line-icons.css" rel="stylesheet" />
        <link href="css/font-awesome.min.css" rel="stylesheet" />

        <!-- page css files -->
        <link href="plugins/jquery-ui/jquery-ui-1.10.4.min.css" rel="stylesheet" />
        <style>
            footer, #usage {
                display: none;
            }
        </style>
        <!-- Custom styles for this template -->
        <link href="css/style.min.css" rel="stylesheet" />
        <link href="css/add-ons.min.css" rel="stylesheet" />
    </head>
    <body class="login" style="background-image:url(img/background.jpg)">
        <div class="container">
            <div class="row">
                <div class="login-box col-lg-4 col-lg-offset-4 col-sm-12 ">
                    <form id="form1" runat="server">
                        <div class="header">
                            <asp:Image ID="Image1" runat="server" ImageUrl="img/pids.png" />
						</div>
                        <fieldset style="width:100%;">
                            <div class="text-center text-danger" style="margin-top:-25px; margin-bottom:5px;">
                                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                                <asp:ValidationSummary ID="LoginUserValidationSummary" CssClass="fa fa-exclamation" runat="server" ValidationGroup="LoginUserValidationGroup" DisplayMode="List"/>
                            </div>
                                    
                            <div class="form-group first">
                                <div class="input-group col-sm-12 has-feedback">
                                    <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                    <asp:TextBox ID="txtUsername" CssClass="form-control input-lg" placeholder="Username" runat="server"></asp:TextBox>
                                    <span class="form-control-feedback">
                                        <asp:RequiredFieldValidator ID="UsnRequired" runat="server" ControlToValidate="txtUsername" ErrorMessage="Username is required" ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                    </span>
                                </div>
                            </div>

                            <div class="form-group last">
                                <div class="input-group col-sm-12 has-feedback">
                                    <span class="input-group-addon"><i class="fa fa-key"></i></span>
                                    <asp:TextBox ID="txtPassword" CssClass="form-control input-lg" placeholder="Password" TextMode="Password" runat="server"></asp:TextBox>
                                    <span class="form-control-feedback">
                                        <asp:RequiredFieldValidator ID="PwdRequired" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required" ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                    </span>
                                </div>
                            </div>
                            <asp:Button ID="lbLogin" runat="server" CommandName="Login" OnClick="lbLogin_Click" CssClass="btn btn-primary col-sm-12" Text="Login" ValidationGroup="LoginUserValidationGroup" />
                            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
                        </fieldset>                        
                    </form>
                </div>
            </div>
        </div>

        <footer>
            <div class="row">
                <div class="col-sm-12 text-center">
                    &copy; Client Name
                </div>
                <div class="col-sm-12 text-center">
                    Powered by: 
                </div>
            </div>
        </footer>
		<!-- start: JavaScript-->
		<!--[if !IE]>-->
            <script src="js/jquery-2.1.1.min.js"></script>
        <!--<![endif]-->

		<!--[if IE]>
			<script src="js/jquery-1.11.1.min.js"></script>
		<![endif]-->

		<!--[if !IE]>-->
			<script type="text/javascript">
				window.jQuery || document.write("<script src='js/jquery-2.1.1.min.js'>"+"<"+"/script>");
			</script>
		<!--<![endif]-->

		<!--[if IE]>
			<script type="text/javascript">
		 	window.jQuery || document.write("<script src='js/jquery-1.11.1.min.js'>"+"<"+"/script>");
			</script>
		<![endif]-->
		<script src="js/jquery-migrate-1.2.1.min.js"></script>
		<script src="js/bootstrap.min.js"></script>
        
        <!-- theme scripts -->
		<script src="plugins/pace/pace.min.js"></script>
		<script src="js/jquery.mmenu.min.js"></script>
		<script src="js/core.min.js"></script>
        
        <script src="plugins/jquery-cookie/jquery.cookie.min.js"></script>

    </body>
</html>
