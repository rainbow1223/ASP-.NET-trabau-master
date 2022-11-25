var loading_data = '<div class="loading-linear-background"><div class="loading-top-1" ></div><div class="loading-top-2"></div></div>';

$(document).ready(function () {
    SearchJobs('job-search-result');
});
function SearchJobs(search_target) {
    $('#' + search_target).html(loading_data);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/SearchJobs',
        data: "{}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data[0].response == 'ok') {
                var jobs_details = json_data[0].jobs_html;
                jobs_details = jobs_details.substring(jobs_details.indexOf('<div class="jobs-result-data">'), jobs_details.indexOf('</form>'));
                $('#' + search_target).html(jobs_details);

                $('.job-result').unbind('click');
                $('.job-result').click(function () {
                    window.location.href = 'login.aspx';
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