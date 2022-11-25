$(document).ready(function () {
    $('.layout-page-content .container').attr('class', 'container-fluid');
    Waves.init();
    if ($(window).width() <= 768) {
        $("body").addClass("enlarged");
        $('.side-menu').addClass('small-menu');
    }

    if ($(window).width() <= 991) {
        $('.side-menu').addClass('small-menu');
    }

    if ($(window).width() >= 480) {
        $('.scroll-bar').scrollbar();
    }

    $('.side-menu').show();

    $('#sidebar-menu li[class*="collapsed"]').click(function () {
        $('.side-menu').removeClass('small-menu');

        if ($(window).width() <= 991) {
            if ($('body').html().indexOf('small-menu-shadow') == -1) {
                var elem = document.createElement('div');
                elem.className = "modal-backdrop show small-menu-shadow";
                elem.style = "z-index:9";
                document.body.appendChild(elem);
            }

            $('.small-menu-shadow').unbind('click');
            $('.small-menu-shadow').click(function () {
                $('.small-menu-shadow').remove();

                $('.side-menu').addClass('small-menu');

                $('.sub-menu').hide(200);
                $('.close-nav-menu').hide();

                $('.side-menu').find('i[class*="fa-chevron"]').attr('class', 'fa fa-chevron-right');
            });

            $('.close-nav-menu').unbind('click');
            $('.close-nav-menu').click(function () {
                $('.small-menu-shadow').remove();

                $('.side-menu').addClass('small-menu');

                $('.sub-menu').hide(200);
                $('.close-nav-menu').hide();

                $('.side-menu').find('i[class*="fa-chevron"]').attr('class', 'fa fa-chevron-right');
            });

            $('.close-nav-menu').show();
        }

        var target = $(this).attr('data-target');
        $(this).toggleClass('mm-active');

        //alert($(this).find('i[class="fa fa-chevron-right"]').attr('class'));
        if ($(this).find('i[class*="fa-chevron"]').attr('class').indexOf('right') > -1) {
            $(this).find('i[class*="fa-chevron"]').attr('class', 'fa fa-chevron-down');
        }
        else {
            $(this).find('i[class*="fa-chevron"]').attr('class', 'fa fa-chevron-right');
        }
        
        //$(this).find('i[class="fa fa-chevron-down"]').attr('class', 'fa fa-chevron-right');
        $(target).slideToggle(200);
        if ($("body").hasClass("enlarged")) {
            $("body").toggleClass("enlarged");
        }
    });
});