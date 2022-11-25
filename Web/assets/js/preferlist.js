$(document).ready(function () {
    $('[data-toggle="popover"]').popover({
        placement: 'top',
        trigger: 'hover'
    });

    $('.btn-remove-prefer').click(function () {
        var userid = $(this).attr('data');
        var Type = 'D';
        RemovePreferList(userid, Type);
    });
});

function RemovePreferList(UserId, Type) {
    $('a[data="' + UserId + '"]').html('<img src="../../assets/uploads/loading-red-back.svg" class="loading-request"/> Removing');
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
                    $('a[data="' + UserId + '"]').parent('div').parent('div').remove();
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