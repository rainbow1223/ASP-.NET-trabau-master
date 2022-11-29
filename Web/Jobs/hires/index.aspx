<%@ Page Title="My Hires - Trabau" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Jobs_hires_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        var pathconfig = '<%= Page.ResolveUrl("index.aspx") %>';
    </script>
    <script src='<%= Page.ResolveUrl("~/assets/js/hire.js?version=1.3") %>'></script>
    <div class="myCard-heading">
        <h4>My Hires</h4>
    </div>
    <div class="profile-card">
        <div class="airCardBody padding-20">
            <div class="card-body-content">
                <div class="row">
                    <div class="col-sm-12" id="div_hires">

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

