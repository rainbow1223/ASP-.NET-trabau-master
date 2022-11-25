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
                var interviews_html = json_data[0].Interview_html;
                interviews_html = interviews_html.substring(interviews_html.indexOf('<div class="interview-data">'), interviews_html.indexOf('</form>'));
                $('#div_interviews').html(interviews_html);

                $('[data-toggle="popover"]').popover({
                    placement: 'top',
                    trigger: 'hover'
                });

                $('.btn-int-sch-accept').click(function () {
                    var id = this;
                    HandlePopUp('1', 'divTrabau_Interview_Action');

                    $('.btnaccept-schedule').unbind('click');
                    $('.btnaccept-schedule').click(function () {
                        var data = $(this).attr('data');

                        Swal.fire({
                            title: 'Accept Interview',
                            text: 'Are you sure to accept this Interview?',
                            icon: 'warning',
                            showCancelButton: true,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Yes, accept it!',
                        }).then((result) => {
                            if (result.value) {
                                var contact_number = $('#txtContactNumber').val();
                                InterviewSchedule_Action(data, 'A', id, contact_number);
                                return true;
                            }
                        });

                    });


                });

                $('.btn-int-sch-reject').click(function () {
                    var id = this;
                    Swal.fire({
                        title: 'Reject Interview',
                        text: 'Are you sure to reject this Interview?',
                        icon: 'warning',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'Yes, reject it!',
                    }).then((result) => {
                        if (result.value) {
                            var data = $(this).parent('div').attr('data');
                            InterviewSchedule_Action(data, 'R', id, '');
                            return true;
                        }
                    });

                });

                $('.btn-int-sch-change').click(function () {
                    var data = $(this).parent('div').attr('data');
                    GetInterview_Content(data);
                });


                $('.btn-interview-report').click(function () {
                    var data = $(this).attr('data');
                    GetInterview_Report(data);
                });

                $('select[id*="ddlInterviewFilter"]').change(function () {
                    var InterviewFilter = $(this).val();
                    LoadMyInterviews(InterviewFilter);
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

function InterviewSchedule_Action(Interviewdata, action_type, id, contact_number) {
    $(id).html('<img src="../../assets/uploads/' + (action_type == 'A' ? 'loading-green-back.svg' : 'loading-gray-back.svg') + '" class="loading-request"/>' + (action_type == 'A' ? 'Accepting' : 'Rejecting'));
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/InterviewSchedule_Action',
        data: "{Interviewdata:'" + Interviewdata + "',action_type:'" + action_type + "',contact_number:'" + contact_number + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var action_message = json_data[0].action_message;
                var action_response = json_data[0].action_response;
                if (action_response == 'success') {
                    HandlePopUp('0', 'divTrabau_Interview_Action');
                    LoadMyInterviews();
                }
                else {
                    $(id).html((action_type == 'A' ? 'Accept' : 'Reject'));
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

                $('.btn-request-update').click(function () {
                    var InterviewDate = $('input[id*="txtInterviewDate"]').val();
                    var InterviewFromTime = $('input[id*="txtInterviewFromTime"]').val();
                    var InterviewToTime = $('input[id*="txtInterviewToTime"]').val();
                    var Interview_data = $('input[id*="hfInterview_data"]').val();

                    if (InterviewDate != '' && InterviewDate != undefined) {
                        if (InterviewFromTime != '' && InterviewFromTime != undefined) {
                            if (InterviewToTime != '' && InterviewToTime != undefined) {
                                UpdateInterviewSchedule(Interview_data, InterviewDate, InterviewFromTime, InterviewToTime);
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


function UpdateInterviewSchedule(Interviewdata, InterviewDate, InterviewFromTime, InterviewToTime) {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/UpdateInterviewSchedule',
        data: "{Interviewdata:'" + Interviewdata + "',InterviewDate:'" + InterviewDate + "',InterviewFromTime:'" + InterviewFromTime + "',InterviewToTime:'" + InterviewToTime + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var action_message = json_data[0].action_message;
                var action_response = json_data[0].action_response;
                if (action_response == 'success') {
                    HandlePopUp('0', 'divTrabau_Interview_details');
                    $('.interviewdetails-content').html('');

                    LoadMyInterviews();
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



function StartInterview(data) {
    HandlePopUp('1', 'divTrabau_Interview_details');
    $('.interviewdetails-content').html(loading_data);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/StartInterview',
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
