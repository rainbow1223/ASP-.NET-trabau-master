<%@ Page Title="My Jobs - Trabau" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="myjobs.aspx.cs" Inherits="Jobs_myjobs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        var pathconfig = '<%= Page.ResolveUrl("myjobs.aspx") %>';
    </script>
    <%--<script src='<%= Page.ResolveUrl("~/assets/js/hire.js?version=1.2") %>'></script>--%>
    <div class="myCard-heading">
        <h4>My Jobs</h4>
    </div>
    <div class="profile-card">
        <div class="airCardBody padding-20">
            <div class="card-body-content">
                <div class="row">
                    <div class="col-sm-12">
                        <asp:Repeater ID="rMyJobs" runat="server">
                            <ItemTemplate>
                                <asp:Label ID="lblJobId" runat="server" Text='<%#Eval("JobId") %>' Visible="false"></asp:Label>
                                <div class="myjobs-row">
                                    <div class="myjobs-result row">
                                        <div class="col-md-5">
                                            <p class="myjobs-result-top">
                                                <b><%#Eval("JobTitle") %></b>
                                            </p>
                                            <p class="myjobs-result-bottom">
                                                <b>Hired by <%#Eval("Client_Name") %></b>
                                            </p>
                                        </div>
                                        <div class="col-md-7">
                                            <p><b>$<%#Eval("Budget") %><%#Eval("BudgetType") %></b> budget</p>
                                            <p>Started on <%#Eval("StartDate") %></p>
                                            <p>Project length: <%#Eval("ProjectLength") %></p>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

