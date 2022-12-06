<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="verification.aspx.cs" Inherits="verification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .email-verification img {
            width: 150px;
        }

        .email-verification h4 {
            color: #6c6969;
            margin-top: 20px;
        }

        .email-verification .verification-text {
            margin-top: 20px;
        }

            .email-verification .verification-text a {
                color: #0bbc56;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="email-verification text-center">
            <img src="assets/images/email_mail_verify.png" />
            <h4>Verify your email to proceed
            </h4>
            <p class="verification-text">
                We just sent an email to the address: <b><asp:Literal ID="ltrlEmailAddress" runat="server"></asp:Literal></b>
                <br />
                Please check your email and click on the link provided to verify your email id
            </p>
            <p class="verification-text" runat="server" visible="false">
                <asp:LinkButton ID="lbtnChangeEmailAddress" runat="server" Text="Change Email Id"></asp:LinkButton>
            </p>
            <asp:Button ID="btnResendVerificationLink" runat="server" Text="Resend Verification Email Id" CssClass="cta-btn-sm" OnClick="btnResendVerificationLink_Click" />
        </div>
    </div>
</asp:Content>

