<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Jobs_searchjobs_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href='<%= Page.ResolveUrl("~/assets/css/search-jobs.css?version=1.8") %>' rel="stylesheet" type="text/css" />
    <style>
        ul[class*="ui-autocomplete"] {
            z-index: 9999;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row search-jobs-top">
        <div class="col-sm-12 col-lg-2">
            <h5>Find Jobs</h5>
        </div>
        <div class="col-sm-12 col-lg-10 t-search-jobs">
            <asp:TextBox ID="txtSearchJobs" runat="server" placeholder="Search for jobs" ClientIDMode="Static"></asp:TextBox>
            <a id="lbtnSearch" class="btn-job-search" href="javascript:void(0);"><i class='fa fa-search' aria-hidden='true'></i></a>
            <div class="advance-search-init">
                <a href="search.aspx">Advance Search</a>
                <div class="filters"></div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-2 leftSideBar">
            <div>
                <ul class="left-search-filters">
                    <li class="active" onclick="FilterFeed(this)">
                        <a id="lbtnMyFeed" href="javascript:void(0);">My Feed</a></li>
                    <li onclick="FilterFeed(this)">
                        <a id="lbtnRecommended" href="javascript:void(0);">Recommended</a></li>
                </ul>
            </div>
            <div>
                <h5 class="jobs-sub-heading">My Categories</h5>
                <ul class="list-without-styled search-job-categories" id="user_categories">
                </ul>
            </div>
        </div>
        <div class="col-sm-12 col-lg-8 jobs-search-content">
            <div class="tabs-content jobs-search-tabs">
                <ul class="job-search-tabs">
                    <li class="active" id="li_myfeed"><a>MY FEED</a></li>
                    <li id="li_recommended"><a>RECOMMENDED</a></li>
                </ul>
            </div>
            <div class="jobs-search-result">
                <div class="search-result-header">
                    <h3>
                        <asp:Label ID="lblSearchHeader" runat="server" ClientIDMode="Static"></asp:Label></h3>
                </div>

                <asp:HiddenField ID="hfjobs_found" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfjobs_result_pageno" runat="server" ClientIDMode="Static" />
                <div id="div_search_result">
                </div>
            </div>
        </div>
        <div class="col-sm-2 job-search-right-main">
            <div class="job-search-right"></div>
        </div>
    </div>

    <div id="divTrabau_JobDetails" class="modal fade" role="dialog">

        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <button class="close" onclick="HandlePopUp('0', 'divTrabau_JobDetails');return false;">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body jobdetails-content">
                </div>
            </div>
        </div>
    </div>

    <div id="divTrabau_SendJobToFriend" class="modal fade" role="dialog">

        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Send Job To Friend</h4>
                    <input id="btnClose" type="button" value="&times;" class="close" onclick="HandlePopUp('0','divTrabau_SendJobToFriend');HandlePopUp('1', 'divTrabau_JobDetails');" />
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                    <div class="row">
                        <asp:HiddenField ID="hfFriendUserId" runat="server" ClientIDMode="Static" />
                        <div class="col-sm-12">
                            <label>Enter Friend Name</label>
                            <asp:TextBox ID="txtFriendName" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div class="col-sm-12">
                            <label>Enter Friend Email Address</label>
                            <asp:TextBox ID="txtFriendEmailAddress" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div class="col-sm-12">
                            <asp:Button ID="btnSendJobToFriend" runat="server" CssClass="cta-btn-sm" Text="Send Job to Friend" ClientIDMode="Static" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        var pathconfig = '<%= Page.ResolveUrl("index.aspx") %>';
        var current_page = 'index';
    </script>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/AutoComplete/js/AutoCompleteText.js") %>' type="text/javascript"></script>
    <script src='<%= Page.ResolveUrl("~/assets/js/search-job.js?version=2.7") %>' type="text/javascript"></script>
</asp:Content>

