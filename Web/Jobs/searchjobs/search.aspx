<%@ Page Title="Advance Search - Trabau" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="search.aspx.cs" Inherits="Jobs_searchjobs_search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href='<%= Page.ResolveUrl("~/assets/css/search-jobs.css?version=1.1") %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tabs-content">
        <ul class="search-tabs">
            <li class="active" target-content-id="search-jobs-content"><a>SEARCH</a></li>
            <li target-content-id="saved-jobs-content" id="li_savedtab" runat="server" visible="false"><a>Saved Jobs</a></li>
        </ul>
    </div>
    <div class="search-jobs-content">
        <div class="search-top-content">
            <div class="t-search-jobs">
                <asp:TextBox ID="txtSearchJobs" runat="server" ClientIDMode="Static" placeholder="Search" CssClass="advance-search-input"></asp:TextBox>
                <a id="lbtnSearch" class="btn-job-search" href="javascript:void(0);"><i class='fa fa-search' aria-hidden='true'></i></a>
            </div>
            <div class="advance-filters">
                <a class="btn-advance-filters" id="btn-advance-filters"><i class="fa fa-filter" aria-hidden="true"></i>Filters</a>
            </div>
        </div>
        <div class="search-bottom-content">
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
        </div>
        <div class="jobs-search-result no-background ad-search">
            <div class="search-result-header hidden">
                <h3>
                    <asp:Label ID="lblSearchHeader" runat="server" ClientIDMode="Static"></asp:Label></h3>
            </div>

            <asp:HiddenField ID="hfjobs_found" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hfjobs_result_pageno" runat="server" ClientIDMode="Static" />
            <div id="div_search_result" class="advance-search-result">
            </div>
        </div>
    </div>
    <div class="saved-jobs-content" id="div_saved_content" runat="server" visible="false">
        <div class="jobs-search-result no-background ad-search">
            <asp:HiddenField ID="hfsaved_jobs_found" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hfsaved_jobs_result_pageno" runat="server" ClientIDMode="Static" />
            <div id="div_saved_result" class="advance-search-result">
            </div>
        </div>
    </div>

    <div id="divTrabau_JobDetails" class="modal fade" role="dialog">

        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <button class="close" onclick="HandlePopUp('0', 'divTrabau_JobDetails');ClearPopupData('jobdetails-content');return false;">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body jobdetails-content">
                </div>
            </div>
        </div>
    </div>
    <script>
        var pathconfig = '<%= Page.ResolveUrl("index.aspx") %>';
        var current_page = 'advance_search';
    </script>
    <script src='<%= Page.ResolveUrl("~/assets/js/search-job.js?version=1.6") %>' type="text/javascript">
    </script>
</asp:Content>

