<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resetpassword.aspx.cs" Inherits="resetpassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Reset Password</title>
    <link rel="stylesheet" href="assets/css/bootstrap.min.css">
    <link rel="stylesheet" href="assets/css/flaticon.css">
    <link rel="stylesheet" href="assets/css/font-awesome.min.css">
    <link rel="stylesheet" href="assets/css/style-1.1.css">
    <link rel="stylesheet" href="assets/css/responsive-1.1.css">
    <link href="assets/plugins/sweet-alert2/sweetalert2.min.css" rel="stylesheet" />
    <script src="../assets/js/jquery-3.4.1.min.js"></script>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/sweet-alert2/sweetalert2.min.js") %>'></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <style>
        html {
            position: relative;
            min-height: 100%;
        }

        body {
            margin-bottom: 100px;
            background: #f1f1f1;
        }
    </style>
</head>
<body style="background-image: url('assets/uploads/login-bg.jpg'); background-repeat: no-repeat; background-size: cover; background-position: center; background-color: #0bbc56;">
    <div class="off-canvas-container">
        <main class="container">
            <div class="air-card">
                <div class="logo">
                    <a href="#">
                        <img src="assets/uploads/logo.png" alt="logo" /></a>
                </div>

                <form id="form1" runat="server">
                    <div id="div_reset_request" runat="server">
                        <h4>Reset your password</h4>
                        <div class="Validator-Container">
                            <asp:TextBox ID="txtNewPassword" TextMode="Password" runat="server" CssClass="form-control" placeholder="Enter new password" autocomplete="Off"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" CssClass="text-danger custom-validator" ControlToValidate="txtNewPassword" SetFocusOnError="true"
                                ErrorMessage="Enter new password" ValidationGroup="Trabau_ForgotPassword" Display="Dynamic" />
                        </div>
                        <div class="Validator-Container">
                            <asp:TextBox ID="txtConfirmNewPassword" TextMode="Password" runat="server" CssClass="form-control" placeholder="Confirm new password" autocomplete="Off"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" CssClass="text-danger custom-validator" ControlToValidate="txtConfirmNewPassword" SetFocusOnError="true"
                                ErrorMessage="Confirm new password" ValidationGroup="Trabau_ForgotPassword" Display="Dynamic" />
                            <asp:CompareValidator ID="cvReNewPassword" runat="server" ControlToCompare="txtNewPassword" ValidationGroup="Trabau_ForgotPassword"
                                ControlToValidate="txtConfirmNewPassword" CssClass="text-danger custom-validator" Type="String" ErrorMessage="Confirm password match failed" Operator="Equal">
                            </asp:CompareValidator>
                        </div>
                        <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" ValidationGroup="Trabau_ForgotPassword" CssClass="form-control"
                            OnClick="btnResetPassword_Click" />
                    </div>
                    <div id="div_reset_password_message" runat="server" visible="false">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </form>
            </div>
        </main>
    </div>
</body>
</html>
