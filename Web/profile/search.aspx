<%@ Page Title="Freelancers Search Result - Trabau" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="search.aspx.cs" Inherits="profile_search"
    EnableEventValidation="false" ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href='<%= Page.ResolveUrl("~/assets/css/search-profile.css?version=1.2") %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        var pathconfig = '<%= Page.ResolveUrl("search.aspx") %>';
    </script>
    
    <script src='<%= Page.ResolveUrl("~/assets/js/search_freelancers.js?version=2.0") %>'></script>
    <section class="inner-page-banner hire-banner" id="div_search_header" runat="server" visible="false">
        <div class="container">
            <div class="row d-flex align-items-center">
                <div class="col-lg-7">
                    <div class="bannerContent">
                        <h2>Search Freelancers, Contractors, & Agencies</h2>
                    </div>
                </div>
                <div class="col-lg-5">
                    <div class="bannerGraphic">
                        <img src='<%= Page.ResolveUrl("~/assets/uploads/banner-graphic-7.png") %>' alt="bannerGraphic" />
                    </div>
                </div>
            </div>
        </div>
    </section>
    <section class="category-sec p-80" id="div_cont_parent" runat="server">
        <div id="div_container" runat="server">
            <div class="row">
                <div class="col-sm-3">
                    <div class="search-profile-filters">
                        <div class="filter-header">Filter By</div>
                        <div class="profile-filters">
                        </div>
                    </div>
                </div>
                <div class="col-sm-9">
                    <div class="search-profile-content">
                        <div class="search-top-content">
                            <div class="t-search-jobs">
                                <asp:TextBox ID="txtSearch" runat="server" ClientIDMode="Static" placeholder="Search" CssClass="advance-search-input"></asp:TextBox>
                                <a id="lbtnSearch" class="btn-job-search" href="javascript:void(0);"><i class='fa fa-search' aria-hidden='true'></i></a>
                            </div>
                        </div>
                        <%-- <div class="search-bottom-content">
                <div class="row">
                    <div class="col-sm-7">
                        <i class="fa fa-bullhorn" aria-hidden="true"></i><span id="sTotalCount"></span>
                    </div>
                    <div class="col-sm-5">
                        <label><b>Sort:</b></label><asp:DropDownList ID="ddlSortBy" runat="server" CssClass="searching-sort form-control">
                            <asp:ListItem Text="Newset" Value="Newest"></asp:ListItem>
                            <asp:ListItem Text="Relevance" Value="Relevance"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="advance-search-controls" style="display: none;">
                    <div class="col-sm-3">
                        <label class="advance-filter-type">Job Type</label>
                        <table id="chkJobType" class="advance-filters-controls"></table>
                        <asp:HiddenField ID="hfJobType" runat="server" ClientIDMode="Static" />
                    </div>
                    <div class="col-sm-3">
                        <label class="advance-filter-type">Experience Level</label>
                        <table id="chkExpLevel" class="advance-filters-controls"></table>
                        <asp:HiddenField ID="hfExpLevel" runat="server" ClientIDMode="Static" />
                    </div>
                    <div class="col-sm-3">
                        <label class="advance-filter-type">Client History</label>
                        <table id="chkClientHistory" class="advance-filters-controls"></table>
                        <asp:HiddenField ID="hfClientHistory" runat="server" ClientIDMode="Static" />
                    </div>
                    <div class="col-sm-3">
                        <label class="advance-filter-type">Number of Proposals</label>
                        <table id="chkNoOfProposals" class="advance-filters-controls"></table>
                        <asp:HiddenField ID="hfNoOfProposals" runat="server" ClientIDMode="Static" />
                    </div>
                    <div class="col-sm-3">
                        <label class="advance-filter-type">Budget</label>
                        <table id="chkBudget" class="advance-filters-controls"></table>
                        <asp:HiddenField ID="hfBudget" runat="server" ClientIDMode="Static" />
                    </div>
                    <div class="col-sm-3 hidden">
                        <label class="advance-filter-type">Hours Per Week</label>
                        <table id="chkHoursPerWeek" class="advance-filters-controls"></table>
                        <asp:HiddenField ID="hfHoursPerWeek" runat="server" ClientIDMode="Static" />
                    </div>
                    <div class="col-sm-3 hidden">
                        <label class="advance-filter-type">Project Length </label>
                        <table id="chkProjectLength" class="advance-filters-controls"></table>
                        <asp:HiddenField ID="hfProjectLength" runat="server" ClientIDMode="Static" />
                    </div>
                    <div class="col-sm-12 margin-top-20 margin-bottom-10">
                        <input type="button" value="Close Filters" class="cta-btn-sm" id="btnCloseFilters" />
                    </div>
                </div>
            </div>--%>
                        <div class="profiles-search-result no-background freelancer-search-result">
                            <div class="search-result-header hidden">
                                <h3>
                                    <asp:Label ID="lblSearchHeader" runat="server" ClientIDMode="Static"></asp:Label></h3>
                            </div>

                            <asp:HiddenField ID="hfprofiles_found" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hfprofiles_result_pageno" runat="server" ClientIDMode="Static" />
                            <div id="div_search_result" class="advance-search-result">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

