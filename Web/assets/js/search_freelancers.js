
var loading_data = '<div class="loading-linear-background"><div class="loading-top-1" ></div><div class="loading-top-2"></div></div>';
var loading_data_small = '<div class="loading-linear-back-small"><div class="loading-top-1" ></div><div class="loading-top-2"></div></div>';

var obj_filters = new Object();
obj_filters.Category = '';
obj_filters.HourlyRate = '';
obj_filters.JobSuccess = '';
obj_filters.EarnedAmount = '';
obj_filters.Language = '';
obj_filters.ProfileType = '';

$(document).ready(function () {

    Get_SearchFilters();

    var page_number = '1';
    $('#div_search_result').html(loading_data);
    Search_Freelancers(page_number);

    $('#lbtnSearch').click(function () {
        $('#div_search_result').html(loading_data);
        $('#hfprofiles_result_pageno').val('1');
        page_number = '1';
        Search_Freelancers(page_number);
    });

    $('#txtSearch').keypress(function (e) {
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            $('#div_search_result').html(loading_data);
            $('#hfprofiles_result_pageno').val('1');
            page_number = '1';
            Search_Freelancers(page_number);
        }
    });


    $(window).scroll(function () {
        var scrollTop = $(window).scrollTop();
        var maxscroll = document.body.offsetHeight - window.innerHeight;
        if (parseInt(scrollTop) >= maxscroll - 50) {
            if ($('#hfprofiles_found').val() == '1') {
                if ($('#div_search_result').html().indexOf('loading-linear-background') == -1) {
                    var page_number = $('#hfprofiles_result_pageno').val();
                    if (page_number == '') {
                        page_number = '1';
                    }
                    else {
                        page_number = (parseInt(page_number) + 1).toString();
                        $('#div_search_result').append(loading_data);
                    }
                    Search_Freelancers(page_number);
                }
            }
        }
    });


});


function Search_Freelancers(PageNumber) {
    var SearchText = $('#txtSearch').val();
    GetFilters_Data();

    var filters = JSON.stringify(obj_filters);
    filters = filters.replace('{', '');
    filters = filters.replace('}', '');
    filters = filters.replace(/"/g, "'");

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/Search_Freelancers',
        data: "{PageNumber:'" + PageNumber + "',SearchText:'" + SearchText + "'," + filters + "}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                $('#hfprofiles_result_pageno').val(PageNumber);

                $('#hfprofiles_found').val(json_data[0].profiles_found);


                var profiles_html = json_data[0].profiles_html;
                profiles_html = profiles_html.substring(profiles_html.indexOf('<div class="freelancers-result-data">'), profiles_html.indexOf('</form>'));

                var items_count = 0;
                if (PageNumber == '1') {
                    $('#div_search_result').html(profiles_html);
                }
                else {
                    items_count = $('.freelancers-result-data .profile-row').length;
                    $('.loading-linear-background').remove();
                    $('.freelancers-result-data').append(profiles_html);
                }


                $('[data-toggle="popover"]').popover({
                    placement: 'top',
                    trigger: 'hover'
                });

                try {
                    Register_PreferEvent();
                } catch (e) {

                }
               

                GetTrabau_PicInfo(1000 + items_count, $('.freelancers-result-data .profile-row').length + 1000);
            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}


function Get_SearchFilters() {
    $('.profile-filters').html(loading_data_small);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/Get_SearchFilters',
        data: "{}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {

                var filters_html = json_data[0].filters_html;
                filters_html = filters_html.substring(filters_html.indexOf('<div class="filters-result-data">'), filters_html.indexOf('</form>'));

                $('.profile-filters').html(filters_html);


                $('.profile-filter-main').click(function () {
                    if ($(this).html().indexOf('flaticon-down-chevron') > -1) {
                        $(this).find('i').attr('class', 'flaticon-up-chevron');
                        $(this).parent('div').find('div[class="profile-filter-child"]').slideDown(300);
                    }
                    else {
                        $(this).find('i').attr('class', 'flaticon-down-chevron');
                        $(this).parent('div').find('div[class="profile-filter-child"]').slideUp(300);
                    }

                });

                $('.profile-filter-child a').click(function () {
                    $(this).parent('div').find('a').removeClass('active');
                    var mainfilter = $(this).parent('div').parent('div').find('span[id*="lblType"]').text();
                    $(this).addClass('active');

                    $('#div_search_result').html(loading_data);
                    $('#hfprofiles_result_pageno').val('1');
                    page_number = '1';
                    Search_Freelancers(page_number);
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

function GetFilters_Data() {
    var Category_Filter = $('#Category').find('a[class*="active"]').attr('id');
    var HourlyRate_Filter = $('#HourlyRate').find('a[class*="active"]').attr('id');
    var JobSuccess_Filter = $('#JobSuccess').find('a[class*="active"]').attr('id');
    var Earnedamount_Filter = $('#Earnedamount').find('a[class*="active"]').attr('id');
    var Language_Filter = $('#Language').find('a[class*="active"]').attr('id');
    var ProfileType_Filter = $('#ProfileType').find('a[class*="active"]').attr('id');

    obj_filters.Category = (Category_Filter == undefined ? '' : Category_Filter);
    obj_filters.HourlyRate = (HourlyRate_Filter == undefined ? '' : HourlyRate_Filter);
    obj_filters.JobSuccess = (JobSuccess_Filter == undefined ? '' : JobSuccess_Filter);
    obj_filters.EarnedAmount = (Earnedamount_Filter == undefined ? '' : Earnedamount_Filter);
    obj_filters.Language = (Language_Filter == undefined ? '' : Language_Filter);
    obj_filters.ProfileType = (ProfileType_Filter == undefined ? '' : ProfileType_Filter);
}