$(document).ready(function () {
    LoadAllProposals();
});


var loading_data = '<div class="loading-linear-background"><div class="loading-top-1" ></div><div class="loading-top-2"></div></div>';

function LoadAllProposals() {
    $('#div_proposals_result').html(loading_data);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetAllProposals',
        data: "{}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var proposals_html = json_data[0].proposals_html;
                proposals_html = proposals_html.substring(proposals_html.indexOf('<div class="proposals-result-data">'), proposals_html.indexOf('</form>'));
                $('#div_proposals_result').html(proposals_html);

                $('.btn-view-jobposting').click(function () {
                    var data = $(this).attr('id');
                    OpenJobPosting(data);
                });
                GetTrabau_PicInfo('1000', $('.proposals-result-data .proposal-row').length + 1000);

            }
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

function OpenJobPosting(_data) {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/OpenJobPostingLink',
        data: "{id:'" + _data + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var redirecturl = json_data[0].redirecturl
                if (redirecturl != '') {
                    window.location.href = redirecturl;
                }
            }
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}