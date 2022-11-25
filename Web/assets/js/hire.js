$(document).ready(function () {
    LoadMyHires();
});


var loading_data = '<div class="loading-linear-background"><div class="loading-top-1" ></div><div class="loading-top-2"></div></div>';

function LoadMyHires() {
    $('#div_hires').html(loading_data);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetMyHires',
        data: "{}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var Hires_html = json_data[0].myhires_html;
                Hires_html = Hires_html.substring(Hires_html.indexOf('<div class="hire-result-data">'), Hires_html.indexOf('</form>'));
                $('#div_hires').html(Hires_html);

                $('[data-toggle="popover"]').popover({
                    placement: 'top',
                    trigger: 'hover'
                });

                Register_PreferEvent();
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