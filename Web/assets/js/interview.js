$(document).ready(function () {
    LoadMyInterviews('All');
});


var loading_data = '<div class="loading-linear-background"><div class="loading-top-1" ></div><div class="loading-top-2"></div></div>';

function LoadMyInterviews(InterviewFilter) {
    $('#div_interviews').html(loading_data);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetMyInterviews',
        data: "{InterviewFilter:'" + InterviewFilter + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var interviews_html = json_data[0].interviews_html;
                interviews_html = interviews_html.substring(interviews_html.indexOf('<div class="interview-data">'), interviews_html.indexOf('</form>'));
                $('#div_interviews').html(interviews_html);

                $('[data-toggle="popover"]').popover({
                    placement: 'top',
                    trigger: 'hover'
                });

                $('.btn-remove-interview').click(function () {
                    var data = $(this).attr('data');
                    Swal.fire({
                        title: 'Are you sure to remove from the Interview list?',
                        text: "Freelancer will be removed form the Interview list",
                        icon: 'warning',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'Yes, remove it!',
                    }).then((result) => {
                        if (result.value) {
                            RemoveFromInterview(data, this);
                            return true;
                        }
                    });

                });

                $('.btn-select-interview').click(function () {
                    var _data = $(this).attr('data');
                    GetInterview_Content(_data);
                });

                $('.interview-sch-item').click(function () {
                    var _data = $(this).attr('data');
                    GetInterview_Content(_data);
                });

                $('.btn-interview-report').click(function () {
                    var data = $(this).parent('div').attr('data');
                    GetInterview_Report(data);
                });

                $('.btn-int-request-reject').click(function () {
                    var data = $(this).parent('div').attr('data');
                    Swal.fire({
                        title: 'Are you sure to reject Interview Request?',
                        text: "Freelancer will be rejected for the Interview",
                        icon: 'warning',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'Yes, reject it!',
                    }).then((result) => {
                        if (result.value) {
                            Interview_RequestAction(data, this, 'R');
                            return true;
                        }
                    });

                });

                $('.btn-int-request-accept').click(function () {
                    var data = $(this).parent('div').attr('data');
                    Swal.fire({
                        title: 'Are you sure to reject Interview Request?',
                        text: "Freelancer will be rejected for the Interview",
                        icon: 'warning',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'Yes, reject it!',
                    }).then((result) => {
                        if (result.value) {
                            Interview_RequestAction(data, this, 'A');
                            return true;
                        }
                    });
                });

                $('select[id*="ddlInterviewFilter"]').change(function () {
                    var InterviewFilter = $(this).val();
                    LoadMyInterviews(InterviewFilter);
                });

                GetTrabau_PicInfo('1000', $('.hire-result-main').length + 1000);
            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}


function Interview_RequestAction(InterviewData, id, ActionType) {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/Interview_RequestAction',
        data: "{InterviewData:'" + InterviewData + "',ActionType:'" + ActionType + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var action_message = json_data[0].action_message;
                var action_response = json_data[0].action_response;
                if (action_response == 'success') {
                    var InterviewFilter = $('select[id*="ddlInterviewFilter"]').val();
                    LoadMyInterviews(InterviewFilter);
                }
                toastr[action_response](action_message);
            }
            else {
                toastr["error"]("Error while taking action on interview list, please refresh and try again");
            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}


function GetInterview_Report(data) {
    $('#divTrabau_Interview_details h4').text('Interview report');
    HandlePopUp('1', 'divTrabau_Interview_details');
    $('.interviewdetails-content').html(loading_data);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetInterview_Report',
        data: "{data:'" + data + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var Interview_html = json_data[0].Interview_html;
                Interview_html = Interview_html.substring(Interview_html.indexOf('<div class="interview-details-data">'), Interview_html.indexOf('</form>'));
                $('.interviewdetails-content').html(Interview_html);


            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

function RemoveFromInterview(proposalId, id) {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/RemoveFromInterview',
        data: "{ProposalId:'" + proposalId + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var action_message = json_data[0].action_message;
                var action_response = json_data[0].action_response;
                if (action_response == 'success') {
                    $('a[data="' + proposalId + '"]').parent('div').parent('div').remove();
                }
                toastr[action_response](action_message);
            }
            else {
                toastr["error"]("Error while taking action on interview list, please refresh and try again");
            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}



function GetInterview_Content(data) {
    HandlePopUp('1', 'divTrabau_Interview_details');
    $('.interviewdetails-content').html(loading_data);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetInterviewContentDetails',
        data: "{data:'" + data + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var Interview_html = json_data[0].Interview_html;
                Interview_html = Interview_html.substring(Interview_html.indexOf('<div class="interview-details-data">'), Interview_html.indexOf('</form>'));
                $('.interviewdetails-content').html(Interview_html);

                setTimeout(function () {
                    $('input[id*="txtInterviewDate"]').datepicker({
                        changeMonth: true,
                        changeYear: true,
                        minDate: 0
                    });

                    $('input[id*="txtInterviewFromTime"]').timepicker();
                    $('input[id*="txtInterviewToTime"]').timepicker();
                }, 200);





                $('.btn-schedule-interview').click(function () {
                    var InterviewType = $('select[id*="ddlInterviewType"]').val();
                    var InterviewDate = $('input[id*="txtInterviewDate"]').val();
                    var InterviewFromTime = $('input[id*="txtInterviewFromTime"]').val();
                    var InterviewToTime = $('input[id*="txtInterviewToTime"]').val();
                    var Interview_data = $('input[id*="hfInterview_data"]').val();

                    if (InterviewType != '0') {
                        if (InterviewDate != '' && InterviewDate != undefined) {
                            if (InterviewFromTime != '' && InterviewFromTime != undefined) {
                                if (InterviewToTime != '' && InterviewToTime != undefined) {
                                    ScheduleInterview(Interview_data, InterviewType, InterviewDate, InterviewFromTime, InterviewToTime);
                                }
                                else {
                                    toastr["error"]("Select Interview To Timing");
                                }
                            }
                            else {
                                toastr["error"]("Select Interview From Timing");
                            }
                        }
                        else {
                            toastr["error"]("Select Interview Date");
                        }
                    }
                    else {
                        toastr["error"]("Select Interview Type");
                    }
                });



                $('.btn-cancel-interview').click(function () {
                    var Interview_data = $('input[id*="hfInterview_data"]').val();

                    Swal.fire({
                        title: 'Are you sure to cancel this scheduled Interview?',
                        text: 'Interview schedule will be cancelled',
                        icon: 'warning',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'Yes, cancel it!',
                    }).then((result) => {
                        if (result.value) {
                            Cancel_ScheduledInterview(Interview_data);
                        }
                    });

                });

                $('select[id*="ddlInterviewType"]').change(function () {
                    var InterviewType = $('select[id*="ddlInterviewType"]').val();
                    if (InterviewType != '' && InterviewType != undefined) {
                        CheckInterviewType(InterviewType);
                    }
                });

                var InterviewType = $('select[id*="ddlInterviewType"]').val();
                if (InterviewType != '' && InterviewType != undefined) {
                    CheckInterviewType(InterviewType);
                }

                $('#btnUpdateResponse').click(function () {
                    var ResponseId = $('select[id*="ddlInterviewResponse"]').val();
                    if (ResponseId != '' && ResponseId != '0') {
                        var data = $('div[id*="div_Interview_Response"]').attr('data');
                        Update_InterviewResponse(data, ResponseId);
                    }
                    else {
                        toastr['error']('Select Response for Interview');
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


function Update_InterviewResponse(Interview_data, ResponseId) {

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/Update_InterviewResponse',
        data: "{Interview_data:'" + Interview_data + "',ResponseId:'" + ResponseId + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data[0].response == 'ok') {
                var action_message = json_data[0].action_message;
                var action_response = json_data[0].action_response;
                if (action_response == 'success') {
                    HandlePopUp('0', 'divTrabau_Interview_details');
                    var InterviewFilter = $('select[id*="ddlInterviewFilter"]').val();
                    LoadMyInterviews(InterviewFilter);
                }
                toastr[action_response](action_message);
            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

function CheckInterviewType(InterviewType) {
    $('.add-question').html(loading_data);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/CheckInterviewType',
        data: "{InterviewType:'" + InterviewType + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data[0].response == 'ok') {
                var questions_html = json_data[0].questions_html;
                setTimeout(function () {
                    $('.add-question').html(questions_html);
                    $('.textEditor').jqte();

                    $('.btn-remove-question').click(function () {
                        var data = $(this).parent('li').attr('data');
                        RemoveInterview_Question(data, this);
                    });

                    $('.btn-add-question').click(function () {
                        var questiontext = $('.jqte_editor').html();
                        if (questiontext != '') {
                            AddInterview_Question(questiontext);
                        }
                        else {
                            toastr["error"]("Enter question text");
                        }
                    });

                }, 500);
                //var action_message = json_data[0].action_message;
                //var action_response = json_data[0].action_response;
                //if (action_response == 'success') {
                //    HandlePopUp('0', 'divTrabau_Interview_details');
                //    LoadMyInterviews();
                //}
                //toastr[action_response](action_message);
            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}


function Cancel_ScheduledInterview(Interview_data) {

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/Cancel_ScheduledInterview',
        data: "{Interview_data:'" + Interview_data + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data[0].response == 'ok') {
                var action_message = json_data[0].action_message;
                var action_response = json_data[0].action_response;
                if (action_response == 'success') {
                    HandlePopUp('0', 'divTrabau_Interview_details');
                    var InterviewFilter = $('select[id*="ddlInterviewFilter"]').val();
                    LoadMyInterviews(InterviewFilter);
                }
                toastr[action_response](action_message);
            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

function ScheduleInterview(Interview_data, InterviewType, InterviewDate, InterviewFromTime, InterviewToTime) {

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/ScheduleInterview',
        data: "{Interview_data:'" + Interview_data + "',InterviewType:'" + InterviewType + "',InterviewDate:'" + InterviewDate + "',InterviewFromTime:'" + InterviewFromTime + "',InterviewToTime:'" + InterviewToTime + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data[0].response == 'ok') {
                var action_message = json_data[0].action_message;
                var action_response = json_data[0].action_response;
                if (action_response == 'success') {
                    HandlePopUp('0', 'divTrabau_Interview_details');
                    var InterviewFilter = $('select[id*="ddlInterviewFilter"]').val();
                    LoadMyInterviews(InterviewFilter);
                }
                toastr[action_response](action_message);
            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

function AddInterview_Question(question) {
    //    $('.interviewdetails-content').html(loading_data);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/AddInterviewQuestion',
        data: "{Question:'" + question + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            var response = json_data[0][0].response;
            var action_message = json_data[0][0].action_message;
            var action_response = json_data[0][0].action_response;

            if (response == 'ok') {
                if (action_response == 'success') {
                    var ul = '';
                    for (var i = 0; i < json_data[1].length; i++) {
                        var li = '<li class="interview-ques" data="' + json_data[1][i].Ques_Key + '">' + json_data[1][i].Question + '</li>';
                        ul = ul + li;
                    }
                    $('#ol_questions').html(ul);

                    $('.jqte_editor').html('');

                    $('.btn-remove-question').click(function () {
                        var data = $(this).parent('li').attr('data');
                        RemoveInterview_Question(data, this);
                    });

                    $('#h4_ques_header').show();
                }
            }

            toastr[action_response](action_message);

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}


function RemoveInterview_Question(question, id) {
    $(id).val('Removing Question');
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/RemoveInterviewQuestion',
        data: "{question:'" + question + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data[0].response == 'ok') {
                var action_message = json_data[0].action_message;
                var action_response = json_data[0].action_response;
                if (action_response == 'success') {
                    //$('li[data="' + data + '"]').slideUp(200);
                    //setTimeout(function () {
                    $('li[data="' + question + '"]').remove();
                    //}, 200);

                    if ($('#ol_questions li').length == 0) {
                        $('#h4_ques_header').hide();
                    }
                }
                toastr[action_response](action_message);
            }
            else {
                $(id).val('Remove Question');
            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}