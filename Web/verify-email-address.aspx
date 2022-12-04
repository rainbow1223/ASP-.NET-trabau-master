<%@ Page Title="" Language="C#" MasterPageFile="~/gen.master" AutoEventWireup="true" CodeFile="verify-email-address.aspx.cs" Inherits="verify_email_address" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="off-canvas-container">
        <main class="container">
            <div class="air-card">
                <div class="logo">
                    <a href="#">
                        <img src="assets/uploads/logo.png" alt="logo" /></a>
                </div>
                <div id="div_thanks" runat="server" visible="false">
                    <div>Thanks for Email address verification for Trabau account.</div>
                    <div class="otherFrmDet"><a class="cta-btn-sm" href="Login.aspx">Continue to Login</a></div>
                </div>
                <div id="div_message" runat="server" visible="false">
                    <div>
                        <asp:Literal ID="ltrlMessage" runat="server"></asp:Literal></div>
                </div>
            </div>
        </main>
    </div>
</asp:Content>

