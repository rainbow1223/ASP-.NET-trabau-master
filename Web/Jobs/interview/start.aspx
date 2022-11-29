<%@ Page Title="Interview - Trabau" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="start.aspx.cs" Inherits="Jobs_interview_start" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href='<%= Page.ResolveUrl("~/assets/plugins/Editor/jquery-te-1.4.0.css") %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= Page.ResolveUrl("~/assets/plugins/Editor/jquery-1.10.2.min.js") %>'></script>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/Editor/jquery-te-1.4.0.min.js") %>'></script>
    <script src='<%= Page.ResolveUrl("~/assets/js/start-interview.js?version=1.0") %>'></script>
    <div class="myCard-heading" id="div_interview_header" runat="server">
        <h4>Answer Interview Questions</h4>
    </div>
    <div class="profile-card">
        <div class="airCardBody padding-20">
            <div class="card-body-content" id="div_ques" runat="server">
                <div class="row pad-10">
                    <div class="col-sm-12">
                        <h4>
                            <asp:Label ID="lblInterviewHeader" runat="server"></asp:Label></h4>
                        <a class="view-job-posting" target="_blank" id="aViewJobPosting" runat="server">View job posting </a>
                        <asp:Label ID="lblTotalQuestions" runat="server" Font-Bold="true" Style="float: right; color: #0bbc56"></asp:Label>
                    </div>
                </div>
                <%--<asp:Repeater ID="rInterviewQuestions" runat="server">
                    <ItemTemplate>--%>
                <asp:Label ID="lblQuestionId" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblScheduleId" runat="server" Visible="false"></asp:Label>
                <div class="row interview-ques-main">
                    <div class="interview-ques col-sm-12">
                        <b>
                            <asp:Label ID="lblQuestionNo" runat="server" CssClass="interview-ques-no"></asp:Label>
                            <asp:Label ID="lblQuestion" runat="server"></asp:Label>
                        </b>
                    </div>
                    <div class="interview-ques col-sm-12">
                        <asp:TextBox ID="txtAnswer" runat="server" CssClass="form-control textEditor"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvAnswer" runat="server" ControlToValidate="txtAnswer" ErrorMessage="Answer question" Text="Answer question" CssClass="text-danger"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <%--</ItemTemplate>
                </asp:Repeater>--%>
                <div class="row pad-10">
                    <div class="col-sm-12">
                        <asp:Button ID="btnNextQuestion" runat="server" CssClass="cta-btn-sm" Text="Save & Next Question" OnClick="btnNextQuestion_Click" />
                        <asp:ValidationSummary ID="vsNextQuestion" runat="server" ShowMessageBox="true" ShowSummary="false" />
                        <asp:Button ID="btnSendtoClient" runat="server" CssClass="cta-btn-sm" Text="Save & Send to Client" OnClick="btnSendtoClient_Click" Visible="false" />
                    </div>
                </div>
            </div>
            <div class="card-body-content interview-finished" id="div_ques_finished" runat="server" visible="false">
                Interview questions already submitted or doesn't belongs to your account
            </div>
        </div>
    </div>
</asp:Content>

