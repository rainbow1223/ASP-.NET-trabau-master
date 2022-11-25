var loading_data = '<div class="loading-linear-background"><div class="loading-top-1" ></div><div class="loading-top-2"></div></div>';
var loading_data_small = '<div class="loading-linear-back-small"><div class="loading-top-1" ></div><div class="loading-top-2"></div></div>';

var current_tab = 'search';
var obj_filters = new Object();
obj_filters.JobType = '';
obj_filters.ExpLevel = '';
obj_filters.ClientHistory = '';
obj_filters.NoOfProposals = '';
obj_filters.Budget = '';
obj_filters.HoursPerWeek = '';
obj_filters.ProjectLength = '';


$(document).ready(function () {

    var page_number = '1';


    if (current_page == 'index') {
        GetUserCategories();
        LoadRight_Navigation();
    }
    else {
        GetAdvanceControlFilters();
    }


    $('#btnSendJobToFriend').click(function () {
        var data = $('a[id*="aSendJob"]').attr('data');
        var Name = $('#txtFriendName').val();
        var EmailAddress = $('#txtFriendEmailAddress').val();
        if (Name != '') {
            if (EmailAddress != '') {
                var userid = $('#hfFriendUserId').val();
                SendJobToFriend(Name, EmailAddress, data, userid);
            }
            else {
                toastr['error']('Enter Email Address for sending job to friend');
            }
        }
        else {
            toastr['error']('Enter Name for sending job to friend');
        }
        return false;
    });



    $(window).scroll(function () {
        if (current_tab == 'search') {
            var scrollTop = $(window).scrollTop();
            var maxscroll = document.body.offsetHeight - window.innerHeight;
            if (parseInt(scrollTop) >= maxscroll - 10) {
                if ($('#hfjobs_found').val() == '1') {
                    if ($('#div_search_result').html().indexOf('loading-linear-background') == -1) {
                        var page_number = $('#hfjobs_result_pageno').val();
                        if (page_number == '') {
                            page_number = '1';
                        }
                        else {
                            page_number = (parseInt(page_number) + 1).toString();
                            $('#div_search_result').append(loading_data);
                        }
                        var text = $('#txtSearchJobs').val();
                        var type = $('.left-search-filters li.active').find('a').text();
                        SearchJobs(page_number, '', text, type, '0', 'div_search_result', 'hfjobs_result_pageno', 'hfjobs_found');
                    }
                }
            }
        }
    });


    $('#lbtnSearch').click(function () {
        var type = $('.left-search-filters li.active').find('a').text();
        $('#hfjobs_result_pageno').val('1');
        $('#div_search_result').html(loading_data + loading_data);
        var text = $('#txtSearchJobs').val();

        ///////////////////////////////////CONTROLS FILTERS//////////////////////////////////////////////
        var JobType = $('#hfJobType').val();
        var ExpLevel = $('#hfExpLevel').val();
        var ClientHistory = $('#hfClientHistory').val();
        var NoOfProposals = $('#hfNoOfProposals').val();
        var Budget = $('#hfBudget').val();
        var HoursPerWeek = $('#hfHoursPerWeek').val();
        var ProjectLength = $('#hfProjectLength').val();

        obj_filters.JobType = (JobType == undefined ? '' : JobType);
        obj_filters.ExpLevel = (ExpLevel == undefined ? '' : ExpLevel);
        obj_filters.ClientHistory = (ClientHistory == undefined ? '' : ClientHistory);
        obj_filters.NoOfProposals = (NoOfProposals == undefined ? '' : NoOfProposals);
        obj_filters.Budget = (Budget == undefined ? '' : Budget);
        obj_filters.HoursPerWeek = (HoursPerWeek == undefined ? '' : HoursPerWeek);
        obj_filters.ProjectLength = (ProjectLength == undefined ? '' : ProjectLength);
        ///////////////////////////////////CONTROLS FILTERS//////////////////////////////////////////////

        SearchJobs('1', '', text, type, '0', 'div_search_result', 'hfjobs_result_pageno', 'hfjobs_found');
    });


    $('.search-tabs li').click(function () {
        $('.search-tabs li').removeClass('active');
        $('.search-jobs-content').hide();
        $('.saved-jobs-content').hide();

        $('.' + $(this).attr('target-content-id')).show();
        $(this).addClass('active');


        if ($('.saved-jobs-content').attr('style').indexOf('none') == -1) {
            current_tab = 'saved';
            $('#div_saved_result').html(loading_data + loading_data);
            SearchJobs('1', '', '', '', '1', 'div_saved_result', 'hfsaved_jobs_result_pageno', 'hfsaved_jobs_found');
        }
        else {
            current_tab = 'search';
            $('#div_search_result').html(loading_data + loading_data);
            SearchJobs('1', '', '', 'My Feed', '0', 'div_search_result', 'hfjobs_result_pageno', 'hfjobs_found');
        }
    });

    var current_url = window.location.href;
    if (current_url.indexOf('savedjobs') > -1) {
        $('li[target-content-id="saved-jobs-content"]').click();
    }
    else {
        $('#div_search_result').html(loading_data + loading_data);
        var search_text = $('#txtSearchJobs').val();
        SearchJobs(page_number, '', search_text, 'My Feed', '0', 'div_search_result', 'hfjobs_result_pageno', 'hfjobs_found');
    }

    if (current_page == 'advance_search') {
        $('#btn-advance-filters').click(function () {
            $('.advance-search-controls').slideToggle(200);
        });

        $('#btnCloseFilters').click(function () {
            $('.advance-search-controls').slideToggle(200);
        });

    }
    else {
        $('.job-search-tabs li').click(function () {
            $('.job-search-tabs li').removeClass('active');
            $(this).addClass('active');
            var _currenttab = $(this).attr('id');
            var type = '';
            if (_currenttab == 'li_myfeed') {
                type = 'My Feed';
            }
            else {
                type = 'Recommended';
            }
            $('#div_search_result').html(loading_data + loading_data);
            SearchJobs('1', '', '', type, '0', 'div_search_result', 'hfjobs_result_pageno', 'hfjobs_found');

        });
    }

});

function SendJobToFriend(Name, EmailAddress, data, userid) {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/SendJobToFriend',
        data: "{Name:'" + Name + "',EmailAddress:'" + EmailAddress + "',data:'" + data + "',userid:'" + userid + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            var response = json_data[0].response;
            if (response == 'ok') {
                var action_response = json_data[0].action_response;
                var action_message = json_data[0].action_message;
                if (action_response == 'success') {
                    $('#hfFriendUserId').val('');
                    $('#txtFriendName').val('');
                    $('#txtFriendEmailAddress').val('');

                    HandlePopUp('0', 'divTrabau_SendJobToFriend');
                    HandlePopUp('1', 'divTrabau_JobDetails');

                }
                toastr[action_response](action_message);
            }
            else {
                toastr['error']('Error while sending job to ' + EmailAddress + ', please refresh and try again');
            }
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}



function LoadRight_Navigation() {
    $('.job-search-right').html(loading_data);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetRightNavDetails',
        data: "{}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var navigation_html = json_data[0].navigation_html;
                navigation_html = navigation_html.substring(navigation_html.indexOf('<div class="nav-details">'), navigation_html.indexOf('</form>'));
                $('.job-search-right').html(navigation_html);

            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

function ClearPopupData(id) {
    $('.' + id).html('');
}



function GetUserCategories(JobId) {

    $('#user_categories').html(loading_data);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetUserCategories',
        data: "{}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var categories = json_data[0].categories;

                $('#user_categories').html(categories);

            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

function FilterFeed(id) {
    $('.left-search-filters li').removeClass('active');
    $(id).addClass('active');
    var type = $(id).find('a').text();

    $('#hfjobs_result_pageno').val('1');
    $('#div_search_result').html(loading_data + loading_data);
    var page_number = '1';
    var _text = '';
    SearchJobs(page_number, '', _text, type, '0', 'div_search_result', 'hfjobs_result_pageno', 'hfjobs_found');
}

function FilterJobs(id) {
    var type = $('.left-search-filters li.active').find('a').text();
    $('#hfjobs_result_pageno').val('1');
    $('#div_search_result').html(loading_data + loading_data);
    var page_number = '1';
    var _text = '';
    var category_id = $(id).attr('id');

    var cat_name = $(id).find('a').text();
    var filter_html = '<div class="filter-text">' + cat_name + '<span title="' + cat_name + '" class="remove-filter" onclick="Remove_SearchFilter()">×</span></div>';
    $('.filters').html(filter_html);

    SearchJobs(page_number, category_id, _text, type, '0', 'div_search_result', 'hfjobs_result_pageno', 'hfjobs_found');
}

function Remove_SearchFilter() {
    $('.filters').html('');
    $('#lbtnSearch').click();
}


function SearchJobs(page_number, category, _text, type, savedjobs, search_target, hdn_pageno, hdn_jobs) {
    var filters = JSON.stringify(obj_filters);
    filters = filters.replace('{', '');
    filters = filters.replace('}', '');
    filters = filters.replace(/"/g, "'");

    $('#lblSearchHeader').text(type);
    if (current_page == 'index') {
        $(document).prop('title', type);
    }
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/SearchJobs',
        data: "{'page_number':'" + page_number + "','category':'" + category + "','text':'" + _text + "','type':'" + type + "','savedjobs':'" + savedjobs + "'," + filters + "}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data[0].response == 'ok') {
                $('#' + hdn_pageno).val(page_number);
                $('#' + hdn_jobs).val(json_data[0].jobs_found);
                var jobs_details = json_data[0].jobs_html;
                jobs_details = jobs_details.substring(jobs_details.indexOf('<div class="jobs-result-data">'), jobs_details.indexOf('</form>'));
                if (page_number == '1') {
                    $('#' + search_target).html(jobs_details);
                }
                else {
                    $('.loading-linear-background').remove();
                    $('#' + search_target).append(jobs_details);
                }

                if (current_tab == 'search') {
                    var numItems = $('.job-result-main').length;
                    $('#sTotalCount').text(numItems.toString() + ' jobs found');
                }
                $('.job-result').unbind('click');
                $('.job-result').click(function () {
                    var JobId = $(this).find('span[id*="lbl_JobId"]').text();
                    GetJobDetails(JobId);
                });


                $('.btn-view-search-job').unbind('click');
                $('.btn-view-search-job').click(function () {
                    var JobId = $(this).parent('div').parent('div').find('span[id*="lbl_JobId"]').text();
                    GetJobDetails(JobId);
                });
                //$('.div_stud_attendance_loading').hide();

                //$('#div-subject-details').scrollbar();
                $('[data-toggle="popover"]').popover({
                    placement: 'top',
                    trigger: 'hover'
                });
            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

function ActivateTooltip() {
    $('[data-toggle="popover"]').popover({
        placement: 'top',
        trigger: 'hover'
    });
}

function GetJobDetails(JobId) {
    $('.jobdetails-content').html(loading_data);
    HandlePopUp('1', 'divTrabau_JobDetails');
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetJobDetails',
        data: "{JobId:'" + JobId + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var jobs_details = json_data[0].jobdetails_html;
                jobs_details = jobs_details.substring(jobs_details.indexOf('<div class="job-details">'), jobs_details.indexOf('</form>'));
                $('.jobdetails-content').html(jobs_details);

                $('a[id*="aAddToPreferList"]').click(function () {
                    var userid = $(this).attr('data');
                    var Type = 'A';
                    if ($(this).attr('class').indexOf('disabled') > -1) {
                        Type = 'D';
                    }
                    AddToPreferList(userid, Type);
                });


                $('a[id*="aSendJob"]').click(function () {
                    HandlePopUp('0', 'divTrabau_JobDetails');
                    HandlePopUp('1', 'divTrabau_SendJobToFriend');
                    $('#hfFriendUserId').val('');
                    $('#txtFriendName').val('');
                    $('#txtFriendEmailAddress').val('');

                    AutoCompleteTextBox('txtFriendName', 'hfFriendUserId', '', pathconfig + '/GetUsers', '::', 'SearchUserForSendToFriend');
                });

                $('.btn-view-search-job').unbind('click');
                $('.btn-view-search-job').click(function () {
                    var JobId = $(this).parent('div').parent('div').find('span[id*="lbl_JobId"]').text();
                    GetJobDetails(JobId);
                });

                $('.other-posted-jobs .job-result').click(function () {
                    if ($(this).find('.job-result-full-details').css('display') == 'none') {
                        $(this).find('.opj-arrow').removeClass('fa-angle-down');
                        $(this).find('.opj-arrow').addClass('fa-angle-up');
                        $(this).parent('div[class*="other-posted-jobs"]').find('.job-actions').show();
                    }
                    else {
                        $(this).find('.opj-arrow').removeClass('fa-angle-up');
                        $(this).find('.opj-arrow').addClass('fa-angle-down');
                        $(this).parent('div[class*="other-posted-jobs"]').find('.job-actions').hide();
                    }


                    $(this).find('.job-result-full-details').slideToggle(200);
                });

                ActivateTooltip();

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

function SearchUserForSendToFriend(id) {
    $('#txtFriendEmailAddress').val('***********');
}

function GetJobAdditionalFileDetails(JobId) {
    $('.additional-files').html(loading_data_small);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetJobAdditionalFiles',
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
        url: pathconfig + '/DownloadAdditionalFile',
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
            url: pathconfig + '/SaveJob',
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


function SaveJob_Main(JobId, id) {
    var _disabled = $(id).prop('disabled');
    if (_disabled == undefined || _disabled == false) {
        $(id).prop('disabled', true);
        $.ajax({
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: pathconfig + '/SaveJob',
            data: "{JobId:'" + JobId + "'}",
            success: function (msg) {
                var data = msg.d;
                var json_data = JSON.parse(data);

                if (json_data[0].response == 'ok') {
                    var action_response = json_data[0].action_response;
                    var action_message = json_data[0].action_message;

                    if (action_response == 'success') {
                        if (action_message.indexOf('removed') == -1) {
                            $(id).html('<i class="fa fa-heart" aria-hidden="true"></i> Saved');
                            $(id).addClass('saved-job');
                        }
                        else {
                            $(id).html('<i class="fa fa-heart-o" aria-hidden="true"></i> Save Job');
                            $(id).removeClass('saved-job');
                        }
                    }
                    toastr[action_response](action_message);

                }
                //else {

                //}
                $(id).prop('disabled', false);
            }
            ,
            error: function (xhr, ajaxOptions, thrownError) {
                $(id).prop('disabled', false);
            }
        });
    }
}


function GetAdvanceControlFilters() {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetAdvanceControlFilters',
        data: "{}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                $('#chkJobType').html(json_data[0].JobType);
                $('#chkExpLevel').html(json_data[0].ExperienceLevel);
                $('#chkClientHistory').html(json_data[0].ClientHistory);
                $('#chkNoOfProposals').html(json_data[0].NumberofProposals);
                $('#chkBudget').html(json_data[0].Budget);
                $('#chkHoursPerWeek').html(json_data[0].HoursPerWeek);
                $('#chkProjectLength').html(json_data[0].ProjectLength);


                $('.advance-filters-controls').find('input[type="checkbox"]').click(function () {
                    try {
                        var new_filter = $.map($(this).closest('table').find('input[type="checkbox"]:checked'), function (n, i) {
                            return n.value;
                        }).join(',');

                        $(this).closest('table').parent('div').find('input[type="hidden"]').val(new_filter);

                        $('#lbtnSearch').click();
                    } catch (e) {

                    }
                });


                $('.advance-filters-controls td').click(function (event) {
                    if (event.target.type !== 'checkbox') {
                        $(this).find('input[type="checkbox"]').trigger('click');

                    }

                });
            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}