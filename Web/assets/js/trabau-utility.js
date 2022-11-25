$(document).ready(function () {
    Register_PreferEvent();
});

function Activate_TooltipNow() {
    $('[data-toggle="popover"]').popover({
        placement: 'top',
        trigger: 'hover'
    });
}


function Register_PreferEvent() {
    $('.btn-prefer-list').unbind('click');
    $('.btn-prefer-list').click(function () {
        var userid = $(this).attr('data');
        var Type = 'A';
        if ($(this).attr('class').indexOf('disabled') > -1) {
            Type = 'D';
        }
        AddToPreferList(userid, Type);
    });
}

function AddToPreferList(UserId, Type) {
    var current_page = window.location.href;;


    $('a[data="' + UserId + '"]').html('<img src="../../assets/uploads/' + (Type == 'A' ? 'loading-green-back.svg' : 'loading-gray-back.svg') + '" class="loading-request"/> ' + (Type == 'A' ? 'Adding' : 'Removing'));
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig_hire + '/AddtoPreferList',
        data: "{UserId:'" + UserId + "',Type:'" + Type + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            var message = json_data[0].message;
            var message_response = json_data[0].message_response;
            var redirect_url = json_data[0].redirect_url;
            toastr[message_response](message);

            if (json_data[0].response == 'ok') {
                if (message_response == 'success') {
                    var target_user = ((current_page.indexOf('jobs/searchjobs/index.aspx') > -1 || current_page.indexOf('jobs/proposals/viewproposal.aspx') > -1) ? 'Client' : 'Freelancer');
                    if (Type == 'A') {
                        $('a[data="' + UserId + '"]').html('<i class="fa fa-check" aria-hidden="true"></i> Preferred List');
                        $('a[data="' + UserId + '"]').addClass('disabled');
                        $('a[data="' + UserId + '"]').attr('data-content', 'Remove ' + target_user + ' from preferred list');
                    }
                    else {
                        $('a[data="' + UserId + '"]').html('Add ' + target_user + ' to Preferred List');
                        $('a[data="' + UserId + '"]').removeClass('disabled');
                        $('a[data="' + UserId + '"]').attr('data-content', 'Add ' + target_user + ' to preferred list');

                    }
                }
            }
            else {
                if (redirect_url != undefined && redirect_url != '') {
                    setTimeout(function () { window.location.href = redirect_url; }, 1000);
                }
            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}