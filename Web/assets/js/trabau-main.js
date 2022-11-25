//window.onload = function () {
//    noBack();
//}

$(document).ready(function () {
    window.history.pushState(null, "", window.location.href);
    window.onpopstate = function () {
        window.history.pushState(null, "", window.location.href);
    };

    var current_search_type = $.cookie('trabau_search_type');
    if (current_search_type == undefined) {
        current_search_type = 'JOBS';
    }
    else if (current_search_type == '') {
        current_search_type = 'JOBS';
    }
    $('a[class*="search-selected"]').removeClass('search-selected');
    $('a[search-type="' + current_search_type + '"]').addClass('search-selected');
    $('#txtHeader_Search').attr('placeholder', $('a[search-type="' + current_search_type + '"]').text());


    Get_RecentSearchHistory();
    $('.org-type').parent('div').parent('div').parent('div').parent('a').addClass('account-active');

    $.event.special.inputchange = {
        setup: function () {
            var self = this, val;
            $.data(this, 'timer', window.setInterval(function () {
                val = self.value;
                if ($.data(self, 'cache') != val) {
                    $.data(self, 'cache', val);
                    $(self).trigger('inputchange');
                }
            }, 20));
        },
        teardown: function () {
            window.clearInterval($.data(this, 'timer'));
        },
        add: function () {
            $.data(this, 'cache', this.value);
        }
    };


    var timer;
    var search_delay = 400; // 0.4 seconds delay after last input

    $('#txtHeader_Search').bind('input', function () {
        $('#search-loading').show();
        window.clearTimeout(timer);
        timer = window.setTimeout(function () {
            //insert delayed input change action/event here
            SearchMenu();

        }, search_delay);
    });

    $('#txtHeader_Search').keypress(function (e) {
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            var text = $('#txtHeader_Search').val();
            if (text.length > 0) {
                var search_type = $('.search-drp').find('a[class="search-selected"]').attr('search-type');
                alert(search_type);
                if (search_type == 'FL') {
                    window.location.href = '../../profile/search.aspx?query=' + text;
                }
                else {
                    window.location.href = '../../jobs/searchjobs/index.aspx?query=' + text;
                }
            }
        }
    });


    $("#toggle").click(function () {
        $(".main-nav").addClass("swap");
    });

    $("#closeToggle").click(function () {
        $(".main-nav").removeClass("swap");
    });

    if ($(window).width() > 991) {
        $('.main-menu ul li a').hover(
            function () {
                $(this).find('span').css({ "white-space": "break-spaces" });
                //var _src = $(this).find('img').attr('src');
                //$(this).find('img').attr('src', _src.replace('.svg', '-color.svg'));
            },

            function () {
                $(this).find('span').css({ "white-space": "nowrap" });
                //var _src = $(this).find('img').attr('src');
                //$(this).find('img').attr('src', _src.replace('-color.svg', '.svg'));
            }
        );
    }

    $('.body-overlay').click(function () {
        // $(".div_search").html('');
        $('.div_search').hide();
        $('.body-overlay').hide();
        $('#txtHeader_Search').val('');

        $('.search-wrapper').css('width', '300px');
        $('.container').css('position', 'unset');
        $('.search-wrapper').css('position', 'relative');
        $('.search-wrapper').css('left', '0');

        $('#search-loading').hide();
    });

    $('#txtHeader_Search').click(function () {
        $('.div_search').slideDown(200);
        $('.body-overlay').show();

        $('.search-wrapper').css('width', '500px');
        $('.container').css('position', 'relative');
        $('.search-wrapper').css('position', 'absolute');
        $('.search-wrapper').css('left', '200px');

        $('#search-loading').show();


        setTimeout(function () {
            $('#search-loading').hide();
        }, 1000);
    });

    $('.change-search-type').click(function () {
        $('.div_search').hide();
        $('.body-overlay').hide();
        $('#txtHeader_Search').val('');

        $('.search-wrapper').css('width', '300px');
        $('.container').css('position', 'unset');
        $('.search-wrapper').css('position', 'relative');
        $('.search-wrapper').css('left', '0');

        $('#search-loading').hide();
    });
});

function noBack() {
    window.history.forward(0);
}


function Get_RecentSearchHistory() {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig_main + '/Get_RecentSearchHistory',
        data: "{}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var history_html = json_data[0].history_html;
                history_html = history_html.substring(history_html.indexOf('<div class="search-history-data">'), history_html.indexOf('</form>'));

                var search_type = $('.search-drp').find('a[class="search-selected"]').attr('search-type');
                if (search_type == 'FL') {
                    for (var i = 0; i < 5; i++) {
                        history_html = history_html.replace('@href', '../../profile/search.aspx');
                    }

                }
                else {
                    for (var i = 0; i < 5; i++) {
                        history_html = history_html.replace('@href', '../../jobs/searchjobs/index.aspx');
                    }
                }

                $('.div_search').html(history_html);

            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}


function SearchMenu() {
    var text = $('#txtHeader_Search').val();
    if (text.length > 0) {

        var search_html = '<div class="search-header">Search</div><div class="search-item"><a href="../../profile/search.aspx?query=' + text + '">' + text + '</a></div>';
        $('.div_search').html(search_html);

    }

    $('#search-loading').hide();
}


function ChangeDropDown(id) {
    var search_type = $(id).attr('search-type');
    $.cookie('trabau_search_type', search_type, { expires: 7, path: '/' });

    $('a[class*="search-selected"]').removeClass('search-selected');
    $(id).addClass('search-selected');
    $('#txtHeader_Search').attr('placeholder', $(id).text());
}

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


function HandlePopUp_HigherIndex(val, id) {
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
            elem.style.cssText = 'z-index: 9999;';
            //elem.style.cssText = "z-index:9999;";
            document.body.append(elem);
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