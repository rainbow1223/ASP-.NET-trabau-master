<%@ Page Title="My Interviews - Trabau" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="myinterviews.aspx.cs" Inherits="Jobs_interview_myinterviews" 
    ValidateRequest="false" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href='<%= Page.ResolveUrl("~/assets/plugins/jquery-timepicker/css/jquery.timepicker.css") %>' rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        var pathconfig = '<%= Page.ResolveUrl("myinterviews.aspx") %>';
    </script>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/jquery-timepicker/js/jquery.timepicker.js") %>'></script>
    <script src='<%= Page.ResolveUrl("~/assets/js/myinterview.js?version=1.6") %>'></script>
    <div class="myCard-heading">
        <h4>My Interviews</h4>
    </div>
    <div class="profile-card">
        <div class="airCardBody padding-20">
            <div class="card-body-content">
                <div class="row">
                    <div class="col-sm-12" id="div_interviews">
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

