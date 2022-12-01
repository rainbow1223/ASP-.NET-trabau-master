<%@ Page Language="C#" AutoEventWireup="true" CodeFile="forgotpassword.aspx.cs" Inherits="forgotpassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Forgot / Reset Password</title>
    <link rel="stylesheet" href="assets/css/bootstrap.min.css">
    <link rel="stylesheet" href="assets/css/flaticon.css">
    <link rel="stylesheet" href="assets/css/font-awesome.min.css">
    <link rel="stylesheet" href="assets/css/style-1.1.css">
    <link rel="stylesheet" href="assets/css/responsive-1.1.css">
    <script src="../assets/js/jquery-3.4.1.min.js"></script>
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

        #imgcaptcha {
            margin-bottom: 15px;
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
                <h4>Reset your password</h4>
                <form id="form1" runat="server">
                    <div id="div_reset_request" runat="server">
                        <div class="Validator-Container">
                            <asp:TextBox ID="UserName" runat="server" CssClass="form-control" placeholder="Enter User Id or Email Address" autocomplete="Off"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" CssClass="text-danger custom-validator" ControlToValidate="UserName" SetFocusOnError="true"
                                ErrorMessage="Enter User Id or Email Address" ValidationGroup="Trabau_ForgotPassword" Display="Dynamic" />
                        </div>
                        <div>
                            <asp:Image ID="imgcaptcha" runat="server" />
                        </div>
                        <div class="Validator-Container">
                            <asp:TextBox ID="txtCaptcha" runat="server" CssClass="form-control" placeholder="Enter captcha" autocomplete="Off"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" CssClass="text-danger custom-validator" ControlToValidate="txtCaptcha" SetFocusOnError="true"
                                ErrorMessage="Enter captcha" ValidationGroup="Trabau_ForgotPassword" Display="Dynamic" />
                        </div>
                        <asp:Label ID="FailureText" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
                        <asp:Button ID="btnSendResetEmail" runat="server" Text="Request Reset Email" ValidationGroup="Trabau_ForgotPassword" CssClass="form-control"
                            OnClick="btnSendResetEmail_Click" />
                    </div>
                    <div id="div_reset_request_message" runat="server" visible="false">
                       <%-- <asp:Label ID="lblMessage" runat="server"></asp:Label>--%>
                        <div id="div_message" runat="server"></div>
                    </div>
                </form>
            </div>
        </main>
    </div>
</body>
</html>
