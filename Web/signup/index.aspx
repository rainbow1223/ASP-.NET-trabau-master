<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Signup_index" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Sign Up - Trabau</title>
    <link rel="stylesheet" href="../assets/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/css/flaticon.css">
    <link rel="stylesheet" href="../assets/css/font-awesome.min.css">
    <link rel="stylesheet" href="../assets/css/style-1.1.css?version=1.1" />
    <link rel="stylesheet" href="../assets/css/login.css?version=1.0" />
    <link rel="stylesheet" href="../assets/css/responsive-1.1.css">
    <link href='../assets/plugins/sweet-alert2/sweetalert2.min.css' rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="../assets/js/bootstrap.min.js"></script>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/sweet-alert2/sweetalert2.min.js") %>'></script>
    <script>
        function HandlePopUp(val, id) {
            if (val == '1') {
                $('#' + id).show();
                setTimeout(function () {

                    var scrollBarWidth = window.innerWidth - document.body.offsetWidth;
                    $('body').css('margin-right', scrollBarWidth).addClass('showing-modal');

                    $('#' + id).show();
                    $('#' + id).addClass('show');
                    //  $('body').css('overflow', 'hidden');
                    var elem = document.createElement('div');
                    elem.className = "modal-backdrop show";
                    //elem.style.cssText = "z-index:9999;";
                    document.body.appendChild(elem);
                }, 300);
            }
            else {
                $('body').css('margin-right', '').removeClass('showing-modal');
                $('#' + id).removeClass('show');
                $('div[class*="modal-backdrop"]').remove();
                setTimeout(function () {
                    $('#' + id).hide();
                    //  $('body').css('overflow', 'auto');
                }, 100);
            }
        }
    </script>
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
<body style="background-image: url('../assets/uploads/login-bg.jpg'); background-repeat: no-repeat; background-size: cover; background-position: center; background-color: #0bbc56;">
    <form id="form1" runat="server">
        <div class="off-canvas-container">
            <main class="container">
                <div class="air-card">
                    <div class="logo">
                        <a href="#">
                            <img src="../assets/uploads/logo.png" alt="logo"></a>
                    </div>
                    <h4>Get your free Account</h4>

                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <div class="SignUp" id="div_signup" runat="server" visible="false">
                        <asp:UpdatePanel ID="upSignUp_Step1" runat="server">
                            <ContentTemplate>
                                <script src="../assets/js/trabau_script.js"></script>
                                <script>
                                    //  Sys.Application.add_load(Validator_Events);
                                </script>
                                <div id="div_signup_step1" runat="server" visible="false">
                                    <div class="Validator-Container">
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="Enter First Name" autocomplete="Off"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" CssClass="text-danger custom-validator" ControlToValidate="txtFirstName" SetFocusOnError="true" ErrorMessage="Enter First Name" ValidationGroup="SignUp" Display="Dynamic" />
                                    </div>

                                    <div class="Validator-Container">
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Enter Last Name" autocomplete="Off"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" CssClass="text-danger custom-validator" ControlToValidate="txtLastName" SetFocusOnError="true" ErrorMessage="Enter Last Name" ValidationGroup="SignUp" Display="Dynamic" />
                                    </div>
                                    <div class="Validator-Container">
                                        <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control" placeholder="Enter Email Address" autocomplete="Off"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" CssClass="text-danger custom-validator" ControlToValidate="txtEmailId" SetFocusOnError="true" ErrorMessage="Enter Email Address" ValidationGroup="SignUp" Display="Dynamic" />
                                        <asp:RegularExpressionValidator runat="server" CssClass="text-danger custom-validator" ControlToValidate="txtEmailId"
                                            SetFocusOnError="true" ErrorMessage="Enter valid email address" ValidationGroup="SignUp" Display="Dynamic"
                                            ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$">
                                        </asp:RegularExpressionValidator>
                                    </div>

                                </div>

                                <div id="div_signup_step2" runat="server" visible="false">
                                    <div>
                                        <p id="div_step2_header" runat="server"></p>
                                    </div>
                                    <div class="Validator-Container">
                                        <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control" placeholder="Enter User Id (Lower characters or numeric)" autocomplete="Off"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="fteUserId" runat="server" TargetControlID="txtUserId" FilterMode="ValidChars" ValidChars="1234567890.qwertyuiopasdfghjklzxcvbnm" />
                                        <asp:RequiredFieldValidator runat="server" CssClass="text-danger custom-validator" ControlToValidate="txtUserId" SetFocusOnError="true" ErrorMessage="Enter User Id" ValidationGroup="SignUp" Display="Dynamic" />
                                    </div>
                                    <div class="Validator-Container">
                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Enter Password" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" CssClass="text-danger custom-validator" ControlToValidate="txtPassword" SetFocusOnError="true" ErrorMessage="Enter Password" ValidationGroup="SignUp" Display="Dynamic" />
                                    </div>
                                    <div class="Validator-Container">
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" placeholder="Re Enter Password" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" CssClass="text-danger custom-validator" ControlToValidate="txtConfirmPassword" SetFocusOnError="true" ErrorMessage="Re Enter Password" ValidationGroup="SignUp" Display="Dynamic" />
                                        <asp:CompareValidator ID="cvReNewPassword" runat="server" CssClass="text-danger custom-validator" ControlToCompare="txtPassword" ValidationGroup="SignUp"
                                            ControlToValidate="txtConfirmPassword" Type="String" ErrorMessage="Confirm password match failed" Operator="Equal">
                                        </asp:CompareValidator>
                                    </div>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="Validator-Container">
                                        <asp:HiddenField ID="hfRemarks" runat="server" />
                                        <asp:DropDownList ID="ddlRegistrationType" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Work as Freelancer" Value="W"></asp:ListItem>
                                            <asp:ListItem Text="Hire for project" Value="H"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" CssClass="text-danger custom-validator" ControlToValidate="ddlRegistrationType" SetFocusOnError="true" ErrorMessage="Select Registration Type" InitialValue="0" ValidationGroup="SignUp" Display="Dynamic" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Button ID="btnSignUp" runat="server" Text="Sign Up" CssClass="form-control" ValidationGroup="SignUp" OnClick="btnSignUp_Click" />
                                </div>
                                <div class="googlebutton" id="div_btn_google" runat="server" visible="false">
                                    <asp:LinkButton ID="lbtnContinueWithGoogle" runat="server" OnClick="lbtnContinueWithGoogle_Click">
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
                                    <span>Continue with Google</span>
                                    </asp:LinkButton>
                                </div>
                                <div class="fb-button" id="div_btn_fb" runat="server" visible="false">
                                    <asp:LinkButton ID="lbtnSignInWithFacebook" runat="server" OnClick="lbtnSignInWithFacebook_Click">
                                    <i class="fa fa-facebook fa-fw"></i> Continue with Facebook
                                    </asp:LinkButton>
                                </div>
                                <div class="linkedin-button" id="div_btn_linkedin" runat="server" visible="false">
                                    <asp:LinkButton ID="lbtnLinkedIn" runat="server" OnClick="lbtnLinkedIn_Click">
                                   <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 21 21" width="21" height="21">
                                        <g transform="scale(.4375)" fill="none" fill-rule="evenodd">
                                            <rect class="bug-text-color" fill="#0077B5" x="1" y="1" width="46" height="46" rx="4"></rect>
                                            <path d="M0 4.01A4.01 4.01 0 014.01 0h39.98A4.01 4.01 0 0148 4.01v39.98A4.01 4.01 0 0143.99 48H4.01A4.01 4.01 0 010 43.99V4.01zM19 18.3h6.5v3.266C26.437 19.688 28.838 18 32.445 18 39.359 18 41 21.738 41 28.597V41.3h-7V30.159c0-3.906-.937-6.109-3.32-6.109-3.305 0-4.68 2.375-4.68 6.109V41.3h-7v-23zM7 41h7V18H7v23zm8-30.5a4.5 4.5 0 11-9 0 4.5 4.5 0 019 0z" class="background" fill="#fff"></path>
                                        </g>
                                   </svg> Continue with LinkedIn
                                    </asp:LinkButton>

                                    <%-- <img src="assets/uploads/LinkedIn-Sign-In.png" />--%>
                                </div>
                                <div id="div_response" runat="server"></div>

                                <div class="otherFrmDet">
                                    <span>Or</span>
                                    <p>Already have an account? <a href="../login.aspx">Log In</a></p>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                </div>
            </main>
        </div>


        <div id="divTrabau_Protection" class="modal fade" role="dialog">

            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">

                    <!-- Modal body -->

                    <div class="modal-body" id="div_protection_content">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label for="txtProtection_Password">Enter Password</label>
                                    <asp:TextBox ID="txtProtection_Password" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="txtProtection_Password" SetFocusOnError="true" ErrorMessage="Enter Password" ValidationGroup="Protection" Display="Dynamic" />
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <asp:LinkButton ID="btnValidateProtection" runat="server" Text="Submit" OnClick="btnValidateProtection_Click" ValidationGroup="Protection" CssClass="btn btn-primary" />
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" style="z-index: 9999">
            <ProgressTemplate>
                <div id="loader-wrapper">
                    <div id="loader">
                        <img src='<%= Page.ResolveUrl("~/assets/images/loading.svg") %>'>
                        <p>Loading...</p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <cc1:AlwaysVisibleControlExtender ID="AlwaysVisibleControlExtender5" runat="server"
            HorizontalSide="Center" TargetControlID="UpdateProgress1" VerticalSide="Middle"></cc1:AlwaysVisibleControlExtender>

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
    </form>
</body>
</html>
