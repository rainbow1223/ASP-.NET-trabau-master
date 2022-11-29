<%@ Page Title="All Proposals - Trabau" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="all-proposals.aspx.cs" Inherits="Jobs_proposals_all_proposals" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .proposal-row {
            cursor: default !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        var pathconfig = '<%= Page.ResolveUrl("all-proposals.aspx") %>';
    </script>
    <script src='<%= Page.ResolveUrl("~/assets/js/proposal-all.js?version=1.0") %>'></script>
    <div class="myCard-heading">
        <h4>All Proposals</h4>
    </div>
    <div class="proposals-content">
        <div class="proposals-result no-background ad-search">
            <div id="div_proposals_result">
            </div>
        </div>
    </div>
</asp:Content>

