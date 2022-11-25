//home 
$(document).ready(function () {
    window.history.pushState(null, "", window.location.href);
    window.onpopstate = function () {
        window.history.pushState(null, "", window.location.href);
    };


    $(window).scroll(function () {
        if ($(this).scrollTop() > 1) {
            $('header').addClass("sticky");
        }
        else {
            $('header').removeClass("sticky");
        }
    });

    $("#toggle").click(function () {
        $(".main-nav").addClass("swap");
    });

    $("#search-nav").click(function () {
        $(".search-wrapper").addClass("swap");
    });

    $("#closeToggle").click(function () {
        $(".main-nav").removeClass("swap");
    });

    $(".search_close").click(function () {
        $(".search-wrapper").removeClass("swap");
    });
});


function HandlePopUp(val, id) {
    if (val == '1') {
        $('#' + id).show();
        setTimeout(function () {

            var scrollBarWidth = window.innerWidth - document.body.offsetWidth;
            $('body').css('margin-right', scrollBarWidth).addClass('showing-modal');

            $('#' + id).show();
            $('#' + id).addClass('show');
            //  $('body').css('overflow', 'hidden');
            var elem = document.createElement('div');
            elem.className = "modal-backdrop show";
            //elem.style.cssText = "z-index:9999;";
            document.body.appendChild(elem);
        }, 300);
    }
    else {
        $('body').css('margin-right', '').removeClass('showing-modal');
        $('#' + id).removeClass('show');
        $('div[class*="modal-backdrop"]').remove();
        setTimeout(function () {
            $('#' + id).hide();
            //  $('body').css('overflow', 'auto');
        }, 100);
    }
}

