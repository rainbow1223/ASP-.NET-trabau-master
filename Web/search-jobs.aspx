<%@ Page Title="Search Jobs - Trabau" Language="C#" MasterPageFile="~/index.master" AutoEventWireup="true" CodeFile="search-jobs.aspx.cs" Inherits="search_jobs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href='<%= Page.ResolveUrl("~/assets/css/search-jobs.css?version=1.1") %>' rel="stylesheet" type="text/css" />
    <style>
        .search-jobs-content {
            border: none;
        }

        .search-job-header {
            background: #0c9647;
            color: #fff;
            padding: 10px 30px 10px 30px;
            margin: 0 -32px;
            font-size: 26px;
        }

        .search-jobs-content {
            padding-top: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="inner-page-banner hire-banner">
        <div class="container">
            <div class="row d-flex align-items-center">
                <div class="col-lg-7">
                    <div class="bannerContent">
                        <h2>Search jobs</h2>
                        <p>Get top notch quality of service from highly qualified professionals who are ready to deliver timely projects.
                        </p>
                    </div>
                </div>
                <div class="col-lg-5">
                    <div class="bannerGraphic">
                        <img src="assets/uploads/banner-graphic-7.png" alt="bannerGraphic" />
                    </div>
                </div>
            </div>
        </div>
    </section>
    <section class="category-sec p-80">
        <div class="container">
            <div class="search-job-header">
                Recent jobs posted by Client
            </div>
            <div class="search-jobs-content">
                <div class="jobs-search-result no-background ad-search" id="job-search-result">
                </div>
            </div>
        </div>
    </section>
    <script>
        var pathconfig = '<%= Page.ResolveUrl("search-jobs.aspx") %>';
    </script>
    <script src='<%= Page.ResolveUrl("~/assets/js/search-job-op.js?version=1.0") %>' type="text/javascript">
        
    </script>
</asp:Content>

