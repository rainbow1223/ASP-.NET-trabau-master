<%@ Page Title="Project Explorer - Trabau" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="projects_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href='<%= Page.ResolveUrl("~/assets/css/project-list.css?version=1.4") %>' rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="myCard-heading">
        <h4 id="h4_heading" runat="server">Project Explorer  <a id="btn_newproject" class="btn-rounded-white" href="new-project.aspx" runat="server">Create Project</a></h4>
    </div>
    <div class="projects-content">
        <div class="projects-result no-background">
            <div id="div_projects_result">
            </div>
        </div>
    </div>

    <script>
        var pathconfig = '<%= Page.ResolveUrl("index.aspx") %>';
    </script>
    <script src='<%= Page.ResolveUrl("~/assets/js/project-list.js?version=1.4") %>'></script>
</asp:Content>

