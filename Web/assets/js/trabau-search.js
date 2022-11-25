$(document).ready(function () {
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

    $('#txtHeader_Search').unbind('inputchange');
    $('#txtHeader_Search').bind('input', function () {
        $('#search-loading').show();
        window.clearTimeout(timer);
        timer = window.setTimeout(function () {
            //insert delayed input change action/event here
            SearchMenu();

        }, search_delay);
    });

    $('#txtHeader_Search').unbind('keypress');
    $('#txtHeader_Search').keypress(function (e) {
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            var text = $('#txtHeader_Search').val();
            if (text.length > 0) {
                var search_type = $('.search-drp').find('a[class="search-selected"]').attr('search-type');
                if (search_type == 'FL') {
                    window.location.href = '../../profile/search.aspx?query=' + text;
                }
                else {
                    window.location.href = 'search-jobs.aspx?query=' + text;
                }
            }
            e.preventDefault();
        }

    });


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

    $('#txtHeader_Search').unbind('click');
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