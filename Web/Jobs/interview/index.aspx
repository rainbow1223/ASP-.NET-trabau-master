<%@ Page Title="Interviews - Trabau" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Jobs_interview_index" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href='<%= Page.ResolveUrl("~/assets/plugins/jquery-timepicker/css/jquery.timepicker.css") %>' rel="stylesheet" />
    <link href='<%= Page.ResolveUrl("~/assets/plugins/Editor/jquery-te-1.4.0.css") %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        var pathconfig = '<%= Page.ResolveUrl("index.aspx") %>';
    </script>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/Editor/jquery-1.10.2.min.js") %>'></script>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/Editor/jquery-te-1.4.0.min.js") %>'></script>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/jquery-timepicker/js/jquery.timepicker.js") %>'></script>
    <script src='<%= Page.ResolveUrl("~/assets/js/interview.js?version=2.0") %>'></script>
    <div class="myCard-heading">
        <h4>Interviews</h4>
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

