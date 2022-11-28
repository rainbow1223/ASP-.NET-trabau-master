<%@ Page Title="Edit Project - Trabau" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="edit-project.aspx.cs" Inherits="projects_edit_project" ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Src="~/projects/UserControls/wucNewProject.ascx" TagPrefix="uc1" TagName="wucNewProject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href='<%= Page.ResolveUrl("~/assets/css/new-project.css?version=2.0") %>' rel="stylesheet" type="text/css" />
    <%--<link href='<%= Page.ResolveUrl("~/assets/plugins/jquery-timepicker/css/jquery.timepicker.css") %>' rel="stylesheet" />--%>

    <link href='<%= Page.ResolveUrl("~/assets/plugins/jquery.timepicker/css/jquery.timepicker.min.css") %>' rel="stylesheet" type="text/css" />

    <link href='<%= Page.ResolveUrl("~/assets/plugins/HTMLEditor/dx.common.css") %>' rel="stylesheet" type="text/css" />
    <link href='<%= Page.ResolveUrl("~/assets/plugins/HTMLEditor/dx.light.css") %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src='<%= Page.ResolveUrl("~/assets/plugins/jquery-timepicker/js/jquery.timepicker.js") %>'></script>--%>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/jquery.timepicker/js/jquery.timepicker.min.js") %>'></script>
    <script src='<%= Page.ResolveUrl("~/assets/js/new_project_1.js?version=2.7") %>'></script>

    <script src='<%= Page.ResolveUrl("~/assets/plugins/HTMLEditor/dx-quill.min.js") %>'></script>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/HTMLEditor/dx.all.js") %>'></script>
    <section class="ProjectSteps-section">
        <asp:UpdatePanel ID="upNewProject" runat="server">
            <ContentTemplate>
                <script src='<%= Page.ResolveUrl("~/assets/js/new_project_2.js?version=2.7") %>'></script>
                <uc1:wucNewProject runat="server" ID="wucNewProject" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </section>
</asp:Content>
