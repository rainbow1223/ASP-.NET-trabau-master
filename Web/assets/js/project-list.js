$(document).ready(function () {
    LoadProjectCount();
});


var loading_data = '<div class="loading-linear-background"><div class="loading-top-1" ></div><div class="loading-top-2"></div></div>';

function LoadProjectCount() {
    $('#div_projects_result').html(loading_data);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetProjectCount',
        data: "{}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var project_html = json_data[0].project_html;
                project_html = project_html.substring(project_html.indexOf('<div class="project-count-details">'), project_html.indexOf('</form>'));
                $('#div_projects_result').html(project_html);

                //$('[data-toggle="popover"]').popover({
                //    placement: 'top',
                //    trigger: 'hover'
                //});


                $('.projects-result .project-result-top').click(function () {
                    if ($(this).parent('.project-result-overview').find('.project-content-details').css('display') == 'none') {
                        $(this).parent('.project-result-overview').find('.opj-arrow').removeClass('fa-angle-down');
                        $(this).parent('.project-result-overview').find('.opj-arrow').addClass('fa-angle-up');

                        var Status = $(this).find('[id*="lblStatus"]').text();
                        LoadProjectDetails(Status, $(this).parent('.project-result-overview'));
                    }
                    else {
                        $(this).parent('.project-result-overview').find('.opj-arrow').removeClass('fa-angle-up');
                        $(this).parent('.project-result-overview').find('.opj-arrow').addClass('fa-angle-down');
                    }

                    if ($(this).parent('.project-result-overview').find('.project-content-details').css('display') == 'none') {
                        $('.project-count-details').find('.project-content-details').slideUp(100);
                    }
                    $(this).parent('.project-result-overview').find('.project-content-details').slideToggle(200);
                });
            }
            else {

            }

            $('[data-toggle="popover"]').popover({
                placement: 'top',
                trigger: 'hover'
            });
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}



function LoadProjectDetails(Status, target) {
    $(target).find('.project-content-details').html(loading_data);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetProjects',
        data: "{Status:'" + Status + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var project_html = json_data[0].project_html;
                project_html = project_html.substring(project_html.indexOf('<div class="projects-control">'), project_html.indexOf('</form>'));
                $(target).find('.project-content-details').html(project_html);



                $('.aproject_menu').unbind('click');
                $('.aproject_menu').click(function () {
                    $('.project-menu').not($(this).parent('div').parent('.profile-card').find('.project-menu')).hide();

                    if ($(this).parent('div').parent('.profile-card').find('.project-menu').css('display') == 'none') {
                        $(this).parent('div').parent('.profile-card').find('.project-menu').show();

                        var elem = document.createElement('div');
                        elem.className = "modal-backdrop fade in project-menu-shadow";
                        elem.style.cssText = "z-index:100;";
                        document.body.appendChild(elem);

                        $('.project-menu-shadow').click(function () {
                            $('.project-menu').hide();
                            $('.project-menu-shadow').remove();
                        });
                    }
                    else {
                        $('.project-menu-shadow').remove();
                        $(this).parent('div').parent('.profile-card').find('.project-menu').hide();
                    }
                });

                $(document).on('keyup', function (evt) {
                    if (evt.keyCode == 27) {
                        $('.project-menu').hide();
                        $('.project-menu-shadow').remove();
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