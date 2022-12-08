<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" EnableEventValidation="false" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Login - Trabau</title>
    <link rel="stylesheet" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/css/flaticon.css" />
    <link rel="stylesheet" href="assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="assets/css/style-1.1.css?version=1.4" />
    <link rel="stylesheet" href="assets/css/login.css?version=1.0" />
    <link rel="stylesheet" href="assets/css/responsive-1.1.css" />
    <script src="../assets/js/jquery-3.4.1.min.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <style>
        html {
            position: relative;
            min-height: 100%;
        }

        body {
            margin-bottom: 100px;
        }
    </style>
</head>
<body style="background-image: url('assets/uploads/login-bg.jpg'); background-repeat: no-repeat; background-size: cover; background-position: center; background-color: #0bbc56;">
    <div class="off-canvas-container">
        <main class="container">
            <div class="air-card">
                <div class="logo">
                    <a href="#">
                        <img src="assets/uploads/logo.png" alt="logo"></a>
                </div>
                <h4>Log in</h4>
                <form id="form1" runat="server">
                    <asp:Login ID="UserAuthLogin" runat="server" OnAuthenticate="UserAuthLogin_Authenticate" FailureText="<div style='color:red'>Invalid UserName or Password</div>" Width="100%">
                        <LayoutTemplate>
                            <div class="Validator-Container">
                                <asp:TextBox ID="UserName" runat="server" CssClass="form-control" placeholder="Enter UserId" autocomplete="Off"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" CssClass="text-danger custom-validator" ControlToValidate="UserName" SetFocusOnError="true"
                                    ErrorMessage="Enter UserId" ValidationGroup="Trabau_Login" Display="Dynamic" />
                            </div>
                            <div class="Validator-Container">
                                <asp:TextBox ID="Password" runat="server" CssClass="form-control" placeholder="Enter Password" TextMode="Password" autocomplete="Off"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" CssClass="text-danger custom-validator" ControlToValidate="Password" SetFocusOnError="true"
                                    ErrorMessage="Enter Password" ValidationGroup="Trabau_Login" Display="Dynamic" />
                            </div>
                            <asp:Button ID="btnLogin" runat="server" Text="Login" ValidationGroup="Trabau_Login" CommandName="Login" CssClass="form-control" Style="margin-bottom: 10px;" />
                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                            <span class="seperator text-center" id="div_Or" runat="server" visible="false">Or</span>

                        </LayoutTemplate>
                    </asp:Login>
                    <div class="googlebutton" id="div_btn_google" runat="server" visible="false">
                        <asp:LinkButton ID="lbtnSignInWithGoogle" runat="server" OnClick="lbtnSignInWithGoogle_Click">
                                    <icon>
                                        <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 24 24" width="24px" height="24px" x="0" y="0" preserveAspectRatio="xMinYMin meet">
                                          <g>
                                            <path style="fill:#E94435" d="M12.1,5.8c1.6-0.1,3.1,0.5,4.3,1.6l2.6-2.7c-1.9-1.8-4.4-2.7-6.9-2.7c-3.8,0-7.2,2-9,5.3l3,2.4C7.1,7.2,9.5,5.7,12.1,5.8z"></path>
                                            <path style="fill:#F8BB15" d="M5.8,12c0-0.8,0.1-1.6,0.4-2.3l-3-2.4C2.4,8.7,2,10.4,2,12c0,1.6,0.4,3.3,1.1,4.7l3.1-2.4C5.9,13.6,5.8,12.8,5.8,12z"></path>
                                            <path style="fill:#34A751" d="M15.8,17.3c-1.2,0.6-2.5,1-3.8,0.9c-2.6,0-4.9-1.5-5.8-3.9l-3.1,2.4C4.9,20,8.3,22.1,12,22c2.5,0.1,4.9-0.8,6.8-2.3L15.8,17.3z"></path>
                                            <path style="fill:#547DBE" d="M22,12c0-0.7-0.1-1.3-0.2-2H12v4h6.1v0.2c-0.3,1.3-1.1,2.4-2.2,3.1l3,2.4C21,17.7,22.1,14.9,22,12z"></path>
                                          </g>
                                        </svg>
                                    </icon>
                                    <span>Sign In with Google</span>
                        </asp:LinkButton>
                    </div>
                    <div class="fb-button" id="div_btn_fb" runat="server" visible="false">
                        <asp:LinkButton ID="lbtnSignInWithFacebook" runat="server" OnClick="lbtnSignInWithFacebook_Click">
                                    <i class="fa fa-facebook fa-fw"></i> Sign In with Facebook
                        </asp:LinkButton>
                    </div>
                    <div class="linkedin-button" id="div_btn_linkedin" runat="server" visible="false">
                        <asp:LinkButton ID="lbtnLinkedIn" runat="server" OnClick="lbtnLinkedIn_Click">
                                   <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 21 21" width="21" height="21">
                                        <g transform="scale(.4375)" fill="none" fill-rule="evenodd">
                                            <rect class="bug-text-color" fill="#0077B5" x="1" y="1" width="46" height="46" rx="4"></rect>
                                            <path d="M0 4.01A4.01 4.01 0 014.01 0h39.98A4.01 4.01 0 0148 4.01v39.98A4.01 4.01 0 0143.99 48H4.01A4.01 4.01 0 010 43.99V4.01zM19 18.3h6.5v3.266C26.437 19.688 28.838 18 32.445 18 39.359 18 41 21.738 41 28.597V41.3h-7V30.159c0-3.906-.937-6.109-3.32-6.109-3.305 0-4.68 2.375-4.68 6.109V41.3h-7v-23zM7 41h7V18H7v23zm8-30.5a4.5 4.5 0 11-9 0 4.5 4.5 0 019 0z" class="background" fill="#fff"></path>
                                        </g>
                                   </svg> Sign In with LinkedIn
                        </asp:LinkButton>

                        <%-- <img src="assets/uploads/LinkedIn-Sign-In.png" />--%>
                    </div>
                    <div class="otherFrmDet">
                        <p><a href="forgotpassword.aspx">Forgot Password?</a></p>
                        <span>Or</span>
                        <p>Don't have an Account? <a href="signup/index.aspx">Sign Up</a></p>
                    </div>
                </form>
            </div>
        </main>
    </div>

    <footer class="fix-footer text-center">
        <div class="container">
            <ul>
                <li><a href="#">Terms of Service</a></li>
                <li><a href="#">Privacy Policy</a></li>
                <li><a href="#">Accessibility</a></li>
            </ul>
            <p>© All Rights Reserved 2020 | Trabau</p>
        </div>
    </footer>

</body>
</html>
