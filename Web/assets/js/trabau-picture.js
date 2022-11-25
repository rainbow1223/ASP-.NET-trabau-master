function GetTrabau_PicInfo(id, max) {
    //console.log('Getting Pic:' + id.toString() + '/' + max.toString());
    var data = $('div[data-key*="userpic_' + id + '"]').attr('data');
    var userid = $('div[data-key*="userpic_' + id + '"]').attr('data-key');
    //console.log('data>>>' + data);
    //console.log('userid>>>' + userid);
    // console.log('data:' + data + '/userid: ' + userid);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig_hire + '/GetUserPicture',
        data: "{data:'" + data + "'}",
        success: function (msg) {
            var found = false;
            $('body img').each(function () {
                if ($(this).attr('src').indexOf('loading_pic.gif') > -1) {
                    found = true;
                }
            });
            // console.log('found>>>' + found);

            if (found == true) {
                if (msg.d != '') {
                    $('div[data-key="' + userid + '"]').find('img').attr('src', msg.d);

                    $('div[data-key="' + userid + '"]').removeAttr('data');
                    $('div[data-key="' + userid + '"]').removeAttr('data-key');

                    if (parseInt(max) - 1 > parseInt(id)) {
                        // console.log('max>>>' + max+' & id>>>'+id);
                        setTimeout(function () {
                            GetTrabau_PicInfo((parseInt(id) + 1).toString(), max);
                        }, 100);
                    }
                }
                else {
                    GetTrabau_PicInfo(id, max);
                }
            }
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {
            // window.location = "Login.aspx";
        }
    });
}