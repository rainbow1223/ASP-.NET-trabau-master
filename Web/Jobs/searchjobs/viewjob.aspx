<%@ Page Title="Job details - Trabau" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="viewjob.aspx.cs" Inherits="Jobs_searchjobs_viewjob" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href='<%= Page.ResolveUrl("~/assets/css/search-jobs.css") %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="jobdetails-content border">
    </div>
    <script>
        var loading_data = '<div class="loading-linear-background"><div class="loading-top-1" ></div><div class="loading-top-2"></div></div>';
        var loading_data_small = '<div class="loading-linear-back-small"><div class="loading-top-1" ></div><div class="loading-top-2"></div></div>';

        function GetJobDetails(JobId) {
            $('.jobdetails-content').html(loading_data);
            $.ajax({
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                url: '<%= Page.ResolveUrl("index.aspx/GetJobDetails") %>',
                data: "{JobId:'" + JobId + "'}",
                success: function (msg) {
                    var data = msg.d;
                    var json_data = JSON.parse(data);

                    if (json_data[0].response == 'ok') {
                        var jobs_details = json_data[0].jobdetails_html;
                        jobs_details = jobs_details.substring(jobs_details.indexOf('<div class="job-details">'), jobs_details.indexOf('</form>'));
                        $('.jobdetails-content').html(jobs_details);

                        GetJobAdditionalFileDetails(JobId);
                    }
                    else {

                    }

                }
                ,
                error: function (xhr, ajaxOptions, thrownError) {

                }
            });
        }

        function GetJobAdditionalFileDetails(JobId) {
            $('.additional-files').html(loading_data_small);
            $.ajax({
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                url: '<%= Page.ResolveUrl("index.aspx/GetJobAdditionalFiles") %>',
                data: "{JobId:'" + JobId + "'}",
                success: function (msg) {
                    var data = msg.d;
                    var json_data = JSON.parse(data);

                    if (json_data[0].response == 'ok') {
                        var additional_files = json_data[0].jobAF_html;
                        additional_files = additional_files.substring(additional_files.indexOf('<div class="job-additional-files">'), additional_files.indexOf('</form>'));
                        $('.additional-files').html(additional_files);
                    }
                    else {

                    }

                }
                ,
                error: function (xhr, ajaxOptions, thrownError) {

                }
            });
        }

        function DownloadAdditionalFile(id) {
            var URL = $(id).attr('download-url');
            $.ajax({
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                url: '<%= Page.ResolveUrl("index.aspx/DownloadAdditionalFile") %>',
                data: "{URL:'" + URL + "'}",
                success: function (msg) {
                    var data = msg.d;
                    var json_data = JSON.parse(data);

                    if (json_data[0].response == 'ok') {
                        var download_url = json_data[0].DownloadURL;
                        window.open(download_url, '_blank');
                    }
                    //else {

                    //}

                }
                ,
                error: function (xhr, ajaxOptions, thrownError) {

                }
            });
        }

        function SaveJob(JobId) {
            var _disabled = $('a[id*="aSaveJob"]').prop('disabled');
            if (_disabled == undefined || _disabled == false) {
                $('a[id*="aSaveJob"]').prop('disabled', true);
                $('a[id*="aSaveJob"]').addClass('btn-disabled');
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    url: '<%= Page.ResolveUrl("index.aspx/SaveJob") %>',
                    data: "{JobId:'" + JobId + "'}",
                    success: function (msg) {
                        var data = msg.d;
                        var json_data = JSON.parse(data);

                        if (json_data[0].response == 'ok') {
                            var action_response = json_data[0].action_response;
                            var action_message = json_data[0].action_message;

                            if (action_response == 'success') {
                                if (action_message.indexOf('removed') == -1) {
                                    $('a[id*="aSaveJob"]').html('<i class="fa fa-heart" aria-hidden="true"></i> Saved');
                                }
                                else {
                                    $('a[id*="aSaveJob"]').html('<i class="fa fa-heart-o" aria-hidden="true"></i> Save a Job');
                                }
                            }
                            toastr[action_response](action_message);

                        }
                        //else {

                        //}
                        $('a[id*="aSaveJob"]').prop('disabled', false);
                        $('a[id*="aSaveJob"]').removeClass('btn-disabled');
                    }
                    ,
                    error: function (xhr, ajaxOptions, thrownError) {
                        $('a[id*="aSaveJob"]').prop('disabled', false);
                        $('a[id*="aSaveJob"]').removeClass('btn-disabled');
                    }
                });
            }
        }
    </script>
</asp:Content>

