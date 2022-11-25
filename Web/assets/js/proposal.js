$(document).ready(function () {
    $('.btn-interview-request').click(function () {
        var data = $(this).attr('data');
        InterviewRequest(data, this);
    });
});

function InterviewRequest(InterviewData, id) {
    $(id).html('<img src="../../assets/uploads/loading-green-back.svg' + '" class="loading-request"/> Processing Request');
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/InterviewRequest',
        data: "{InterviewData:'" + InterviewData + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var action_message = json_data[0].action_message;
                var action_response = json_data[0].action_response;
                if (action_response == 'success') {
                    $(id).parent('div').html('<b>Request sent for Interview</b>');
                }
                toastr[action_response](action_message);
            }
            else {
                toastr["error"]("Error while generating interview request, please refresh and try again");
            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}