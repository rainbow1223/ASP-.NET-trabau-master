var loading_data = '<div class="loading-linear-background"><div class="loading-top-1" ></div><div class="loading-top-2"></div></div>';
var loading_data_small = '<div class="loading-linear-back-small"><div class="loading-top-1"></div></div></div>';
var modal_popup = '<div id="@id" class="modal fade" role="dialog" style="z-index: 99999;"><div class="modal-dialog modal-lg" role="document"> <div class="modal-content"> @header<div class="modal-body"> </div> </div> </div> </div>';

var $carousel = null;
var isFlickity = false;
var uniquePropertyKey;
var parentUniquePropertyKey;

$(document).ready(function () {

    jQuery.fn.ForceNumericOnly =
        function () {
            return this.each(function () {
                $(this).keydown(function (e) {
                    var key = e.charCode || e.keyCode || 0;
                    // allow backspace, tab, delete, enter, arrows, numbers and keypad numbers ONLY
                    // home, end, period, and numpad decimal
                    return (
                        key == 8 ||
                        key == 9 ||
                        key == 13 ||
                        key == 46 ||
                        key == 110 ||
                        key == 190 ||
                        (key >= 35 && key <= 40) ||
                        (key >= 48 && key <= 57) ||
                        (key >= 96 && key <= 105));
                });
            });
        };

    //Initialize Toolbar component
    try {
        ej.base.enableRipple(true);

        var toolbar = new ej.navigations.Toolbar({ width: 'auto', overflowMode: 'Popup' });

        //Render initialized Toolbar component
        toolbar.appendTo('#template_toolbar');
    } catch (e) {

    }

    ActivateTouchSlider();

    //window.addEventListener("orientationchange", function () {
    //    // Announce the new orientation number
    //    setTimeout(function () {
    //        if ($carousel != null) {
    //            $carousel.flickity('destroy');
    //        }
    //        ActivateTouchSlider();
    //    }, 200);
    //}, false);

    $(window).resize(function () {
        // Announce the new orientation number
        setTimeout(function () {
            if ($(window).width() >= 992) {
                if ($carousel != null && isFlickity) {
                    console.log('touch slider destroyed');
                    $carousel.flickity('destroy');
                    isFlickity = !isFlickity;
                }
            }
            else {
                ActivateTouchSlider();
            }
        }, 500);
    });

    // Listen for orientation changes      
    //$.get("../assets/static_html/home_menu.html?i=1233", function (data) {
    //    $('#content649').html(data);
    //    $('#content674').html(data);
    //});

    // this is for adding controls in file menu
    //$.get("../assets/static_html/home_menu.html?i=1233", function (data) {
    //    $('.file-nav-content').html(data);
    //});

    // Prevent closing from click inside dropdown
    $(document).on('click', '.dropdown-menu', function (e) {
        e.stopPropagation();
    });

    // make it as accordion for smaller screens
    if ($(window).width() < 992) {
        $('.nav-item a').click(function (e) {
            //MainNavClick(this);

            e.preventDefault();
            //if ($(this).next('.dropdown-menu').length) {
            //    $(this).next('.dropdown-menu').toggle();
            //}
            $('.dropdown').on('hide.bs.dropdown', function () {
                // $(this).find('.dropdown-menu').hide();
            })
        });
    }

    //$('.navbar-toggler').click(function () {
    //    var expanded = $(this).attr('aria-expanded');
    //    if (expanded == 'false' || expanded == undefined) {
    //        $(this).find('span').attr('class', 'flaticon-close');
    //    }
    //    else {
    //        $(this).find('span').attr('class', 'navbar-toggler-icon');
    //    }
    //});




    $('#filetoggle').click(function () {
        if ($('.dropdownfile').css('display') === 'none') {
            var elem = document.createElement('div');
            elem.className = "modal-backdrop show";
            elem.id = 'dropdown-overlay';
            elem.style.cssText = 'opacity: 0.2;';
            document.body.appendChild(elem);

            $('#dropdown-overlay').click(function () {
                $('.dropdownfile').fadeOut(200);
                $('#dropdown-overlay').remove();
            });
        }


        $('.dropdownfile').fadeToggle(200);
    });

    $('.project-maximize-arrow').click(function () {
        $(this).toggleClass('up-arrow');

        if ($(this).hasClass('up-arrow')) {
            $('.project-maximize-arrow').css('top', '11px');
            $('header').slideUp(500);
            $('footer').slideUp(500);
            $('body').animate({ 'padding-top': "0" }, 250);
        }
        else {
            $('.project-maximize-arrow').css('top', '16px');
            $('header').slideDown(500);
            $('footer').slideDown(500);
            $('body').animate({ 'padding-top': "70px" }, 250);
        }
    });

    $('a[data-toggle="tab"]').on('shown.bs.tab', function (event) {
        var target_tab = event.target;
        // console.log(target_tab.hash);

        var totalitems = $(target_tab.hash + ' .tab-content-inner').length;

        if (totalitems > 2) {
            if (!$('.project-loading').hasClass('project-loading-full')) {
                $('.project-loading').addClass('project-loading-full');
                $('.project-loading').html('<p>Loading content</p>');
            }
            $('.project-loading').show();
            setTimeout(function () {
                $(target_tab.hash).flickity({
                    freeScroll: true,
                    pageDots: false,
                    contain: true
                });
            }, 100);


            setTimeout(function () {
                $(".flickity-slider").unbind('DOMSubtreeModified');
                $(".flickity-slider").bind('DOMSubtreeModified', function (e) {
                    setTimeout(function () {
                        var style_value = $(".flickity-slider").attr('style');
                        //console.log('style_value: ' + style_value);
                        style_value = style_value.substr(style_value.indexOf('('), style_value.indexOf(')'));
                        style_value = style_value.replace('(', '');
                        style_value = style_value.replace(')', '');
                        style_value = style_value.replace(';', '');
                        //console.log('style_value: ' + style_value);
                        var data = style_value.split(',')[0].replace('%', '');

                        if (parseFloat(data) < -35) {
                            $('.flickity-slider').css('margin-left', '0');
                        }
                        else {
                            $('.flickity-slider').css('margin-left', '');
                        }
                    }, 1000);
                });
            }, 200);
        }

        setTimeout(function () {
            $('.project-loading').hide();
        }, 500);

    });


    setTimeout(function () {
        $('.navigation-content').show();
        $('.myCard-heading').show();
        // $('#tab649').click();
        // $('#tab674').click();
    }, 200);

    $('.nav-item .nav-link').click(function () {
        $('.dropdown-menu').removeClass('show');

        var currentAnchor = this;
        var navigationID = $(this).parent('li').attr('navigation-id');
        var anchorID = $(this).attr('id');
        anchorID = anchorID.replace('tab', '');

        if ($('ul[id="' + anchorID + '"]').length == 0) {
            var navigationLoading = '<ul class="navigation-loading dropdown-menu">' + loading_data + '</ul>';
            $(currentAnchor).parent('li').append(navigationLoading);
            var _data = {
                navigationID: navigationID,
                domain: getUrlParameter('domain')
            };

            $.ajax({
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                url: pathconfig + '/GetChildNavigationItems',
                data: JSON.stringify(_data),
                success: function (msg) {
                    var data = msg.d;
                    var json_data = JSON.parse(data);
                    if (json_data[0].response == 'ok') {
                        var html = json_data[0].NavigationHTML;
                        $('.navigation-loading').remove();
                        $(currentAnchor).parent('li').append(html);

                        $('ul[id="' + anchorID + '"]').addClass('show');

                        if ($(window).width() < 992) {
                            AdjustDropdownLocation();
                        }
                        $(document).click(function (e) {
                            e.stopPropagation();
                            var container = $(".dropdown");

                            //check if the clicked area is dropdown or not
                            if (container.has(e.target).length === 0) {
                                $('.dropdown-menu').removeClass('show');
                            }
                        });

                        RegisterDisabledNavigationEvents();
                    }
                }
                ,
                error: function (xhr, ajaxOptions, thrownError) {

                }
            });
        }
        else {
            $('ul[id="' + anchorID + '"]').toggleClass('show');
        }
    });
    //$('.project-dropdown-toggle').click(function () {
    //    $(this).parent('div').find('ul').slideToggle(200);
    //});
});

var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
    return false;
};

function ActivateTouchSlider() {
    setTimeout(function () {
        if ($(window).width() < 992) {
            if (!isFlickity) {
                console.log('touch slider activated');
                $('.navbar').addClass('nav-hidden');
                $carousel = $('.navbar-nav').flickity({
                    freeScroll: true,
                    pageDots: false,
                    contain: true
                });

                isFlickity = !isFlickity;

                $('.flickity-slider').css('margin-left', '0');

                AdjustDropdownLocation();
            }
        }
    }, 500);
}

function AdjustDropdownLocation() {
    var total = $('.flickity-slider .nav-item').length;
    $('.flickity-slider .nav-item').each(function (index) {
        if (total - index <= 3) {
            $(this).find('ul[childlevel="0"]').attr('style', 'right:0; left:auto;');
        }
    });

    $('.navbar').removeClass('nav-hidden');
}

const UpdateProject = () => {
    var additionalInformation = $('#txtadditional_information').val();
    var applicationName = $('#txtapplication_name').val();
    var budgetType = $('#txtbudget_type').val();
    var companyName = $('#txtcompany_name').val();
    var projectDescription = $('#hfProjectDescription').val();
    var endDate = $('#txtend_date').val();
    var endTime = $('#txtend_time').val();
    var projectName = $('#txtproject_name').val();
    var startDate = $('#txtstart_date').val();
    var startTime = $('#txtstart_time').val();


    var _projectname = $('#txt0000').val();
    var _companyname = $('#txt0001').val();
    var _applicationname = $('#txt0002').val();
    var _description = $('#txt0004').val();
    var _enddate = $('#txt0006').val();
    var _endtime = $('#txt0008').val();
    var _budgettype = $('#txt0009').val();
    var _totalhours = $('#txt0007').val();

    var _data = {
        project_name: '',
        end_date: '',
        company_name: '',
        application_name: '',
        description: '',
        end_time: '',
        budget_type: '',
        total_hours: ''
    };

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/UpdateProject',
        data: JSON.stringify(_data),
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            var project_html = json_data[0].project_html;
            $('#div_project_content').html(project_html);
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });

    return false;
}

const OpenNavigation = (id) => {
    var element = $("<div></div>");
    element.attr("nav_id", $(id).attr('target-nav-id'));

    $('div[class*="modal-backdrop"]').remove();
    NavClick(element);
}

const OpenEditDialog = (id) => {
    var element = $("<div></div>");
    element.attr("nav_id", $(id).attr('nav-id'));
    element.attr("data_id", $(id).attr('data-index'));
    element.attr("currentnav_id", $(id).attr('currentnav-id'));
    element.attr("unique_property_key", $(id).attr('unique-property-key'));
    element.attr("parent_unique_property_key", $(id).attr('parent-unique-property-key'));

    $('div[class*="modal-backdrop"]').remove();
    NavClick(element);
}

const NavClick = (id) => {
    if (!$(id).hasClass('navigationDisabled')) {
        // clearing the unique proptery key as navigation tabs are going to be loaded
        uniquePropertyKey = '';
        parentUniquePropertyKey = '';

        $('.nav-modal-title').html('');
        $('#div_project_tabs_content').html(loading_data);
        HandlePopUp('1', 'divTrabau_project_tabs');
        var navID = $(id).attr('nav_id');
        var dataID = $(id).attr('data_id');

        if ($(id).attr('parent_unique_property_key') != undefined) {
            parentUniquePropertyKey = $(id).attr('parent_unique_property_key');
        }
        //console.log('Nav Click>>>Pup' + parentUniquePropertyKey);

        if ($(id).attr('unique_property_key') != undefined) {
            uniquePropertyKey = $(id).attr('unique_property_key');
        }
        //console.log('Nav Click>>>up' + uniquePropertyKey);

        var currentNavID = $(id).attr('currentnav_id');
        $.ajax({
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: pathconfig + '/GetNavigationTabs',
            data: "{NavId:'" + navID + "',DataId:'" + dataID + "',currentNavID:'" + currentNavID + "'}",
            success: function (msg) {
                var data = msg.d;
                var json_data = JSON.parse(data);

                if (json_data[0].response == 'ok') {
                    var _data = JSON.parse(json_data[0].response_data);

                    var nav_parent = '<ul class="nav nav-tabs" id="myTab1" role="tablist">';
                    var li = '<li class="@tabClass" id="@id" nav_id="@navID" tab-short-name="@tabshortname" onclick="TabClick(this)"><a class="nav-link">@TabName</a></li>';

                    var uniqueProperty = _data[0].UniqueProperty;

                    if (uniquePropertyKey == undefined || uniquePropertyKey == '') {
                        if (uniqueProperty != undefined && uniqueProperty != '') {
                            uniquePropertyKey = uniqueProperty;
                        }
                    }

                    var navigation_name = '';
                    var visibility = false;
                    for (var i = 0; i < _data.length; i++) {
                        if (_data[i].Visibility && !visibility) {
                            visibility = true;
                        }
                        var tabname = _data[i].TabName;
                        var tab_shortname = _data[i].TabShortName;
                        var tabClass = _data[i].TabClass;

                        var tabid = _data[i].TabId;
                        var navID = _data[i].OrgNavigationId;
                        if (navigation_name == '') {
                            navigation_name = _data[i].NavigationName;
                        }

                        var li_temp = li.replace('@TabName', tabname);
                        li_temp = li_temp.replace('@id', tabid);
                        li_temp = li_temp.replace('@navID', navID);
                        li_temp = li_temp.replace('@tabshortname', tab_shortname);
                        li_temp = li_temp.replace("@tabClass", (tabClass != '' ? tabClass + ' ' : '') + 'nav-item');

                        nav_parent = nav_parent + li_temp;
                    }

                    //  $('.nav-modal-title').text(navigation_name);

                    nav_parent = nav_parent + "</ul>";
                    nav_parent = nav_parent + '<div class="tab-content" id="myTabContent1111"><div class="tab-pane fade show active" id="tab_content"></div></div>';

                    $('#div_project_tabs_content').html(nav_parent);

                    if (!visibility) {
                        $('#myTab1').hide();
                    }
                    TabClick($('#myTab1 li:first'));
                }
            }
            ,
            error: function (xhr, ajaxOptions, thrownError) {
                HandlePopUp('0', 'divTrabau_project_tabs');
            }
        });
    }
    return false;
}

const ProjectTabClick = (id) => {
    $(id).parent('ul').find('a').removeClass('active');
    $(id).find('a').addClass('active');
    var target = $(id).attr('target');
    $('div[id*="project_content_"]').hide();
    $('#' + target).show();
}


var inlineErrorsRequired = true;

function AddInlineErrorsCheckbox() {
    $('.inline-errors').remove();
    $('.inline-errors-main').remove();

    let inlineErrorsCheckbox = '<div class="inline-errors"><label>Inline Warning</label><input type="checkbox" checked="true" /></div>';
    $('#divTrabau_project_tabs').find('.modal-header').append(inlineErrorsCheckbox);

    $('.inline-errors').find('input[type="checkbox"]').unbind('click');
    $('.inline-errors').find('input[type="checkbox"]').click(function () {
        inlineErrorsRequired = $(this).prop('checked');
    });
    inlineErrorsRequired = true;
}


const PreviousTab = () => {
    PreserveData();

    var li = $('#myTab1').find('a[class*="active"]').closest('li').prev('li');
    $(li).click();
}

const PreserveData = () => {
    var datatoSave = GetFormData();
    var XMLData = {
        datatoSave: JSON.stringify(datatoSave)
    };
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/PreserveTabContentData',
        data: JSON.stringify(XMLData),
        success: function (msg) {
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}


const TabClick = (id) => {
    //console.log('Tab Click up>>>' + uniquePropertyKey);
    //console.log('Tab Click Pup>>>' + parentUniquePropertyKey);

    $('.inline-errors').remove();
    $(id).parent('ul').find('a').removeClass('active');
    $(id).find('a').addClass('active');
    var tabid = $(id).attr('id');
    var navID = $(id).attr('nav_id');
    $('#tab_content').html(loading_data);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetTabContent',
        data: "{TabId:'" + tabid + "',navID:'" + navID + "',uniquePropertyKey:'" + (uniquePropertyKey == undefined ? '' : uniquePropertyKey)
            + "',parentUniquePropertyKey:'" + (parentUniquePropertyKey == undefined ? '' : parentUniquePropertyKey) + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var tabs_html = json_data[0].response_data;

                $('#tab_content').html(tabs_html);

                RegisterValidationEvent();

                var ref_fn = json_data[0].function_name;
                if (ref_fn != '' && ref_fn != undefined) {
                    for (var j = 0; j < ref_fn.split(',').length; j++) {
                        var fn_name = ref_fn.split(',')[j];
                        window[fn_name]();
                    }
                }

                $('.tab-field[multidropdown="true"]').find('select').each(function () {
                    var hfid = $(this).closest('.tab-field').find('input[type="hidden"]').attr('id');
                    RegisterSelect2($(this).attr('id'), 'Select', hfid, '');
                });


                //$('.file-upload-control').click(function () {
                //    $(this).find('input[type="file"]').focus().trigger('click');
                //});


                var total_tabs = $('#myTab1 li').length;

                var last_tab = $('#myTab1').find('a[class*="active"]').parent("li").is(":last-child")
                var li_index = $('#myTab1').find('a[class*="active"]').parent('li').index();

                if (total_tabs <= 1 || last_tab) {
                    $('#btnSaveTabData').html('Apply');
                }
                else {
                    $('#btnSaveTabData').html('Next');
                }

                if (li_index > 0) {
                    var btn_next = '<input id="btnPrevTabData" type="button" value="Previous" class="cta-btn-md" onclick="PreviousTab()" />&nbsp;';
                    $('.tab-footer').prepend(btn_next);
                }

                var tabname = $('#myTab1').find('a[class*="active"]').parent('li').text();
                var tab_shortname = $('#myTab1').find('a[class*="active"]').parent('li').attr('tab-short-name');
                $('.nav-modal-title').html(tabname + '<p style="margin:0;color:#fff">' + tab_shortname + '</p>');

                $('.file-upload-control').append('<label id="fileLabel">Choose or drag and drop file</label>');


                $(".file-upload-control").on('dragover', function (e) {
                    e.stopPropagation();
                    e.preventDefault();
                    $('.file-upload-control').find('label').text('Drag here');
                    setTimeout(function () {
                        $('.file-upload-control').find('label').text('Choose or drag and drop file');
                    }, 5000);
                }).on('dragleave dragend', function (e) {
                    e.stopPropagation();
                    e.preventDefault();
                    $('.file-upload-control').find('label').text('Choose or drag and drop file');
                }).on('drop', function (e) {
                    // e.preventDefault();     // Stops some browsers from redirecting.

                });


                $('.view-more-desc').click(function () {
                    var fileKey = $(this).parent('p').attr('file_key');

                    var div_content = CreateNew_Modal('0', loading_data);

                    var datatoUpload = {
                        fileKey: fileKey
                    };

                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        url: pathconfig + '/GetMoreFileDescription',
                        data: JSON.stringify(datatoUpload),
                        success: function (msg) {
                            var data = msg.d;
                            var json_data = JSON.parse(data);

                            if (json_data[0].response == 'ok') {
                                $('#' + div_content).html(json_data[0].FileDescription);
                            }
                        }
                        ,
                        error: function (xhr, ajaxOptions, thrownError) {

                        }
                    });

                    return false;
                });

                $('.accordiion-field').click(function () {
                    if ($(this).next('table').css('display') == 'none') {
                        $(this).find('.opj-arrow').removeClass('fa-angle-down');
                        $(this).find('.opj-arrow').addClass('fa-angle-up');
                    }
                    else {
                        $(this).find('.opj-arrow').removeClass('fa-angle-up');
                        $(this).find('.opj-arrow').addClass('fa-angle-down');
                    }


                    $(this).next('table').slideToggle(200);
                });

                $('.tab-field[changeeventrequired=true]').find('select').change();

                $('.custom-radio label').click(function () {
                    $(this).parent('li').find('input[type=radio]').click();
                });

                $('.btn-context-menu').click(function () {
                    $('.context-menu').not($(this).parent('div').find('.context-menu')).hide();

                    if ($(this).parent('div').find('.context-menu').css('display') == 'none') {
                        $(this).parent('div').find('.context-menu').show();

                        var elem = document.createElement('div');
                        elem.className = "modal-backdrop fade in context-menu-shadow";
                        elem.style.cssText = "z-index:1041;";
                        document.body.appendChild(elem);

                        $('.context-menu-shadow').click(function () {
                            $('.context-menu').hide();
                            $('.context-menu-shadow').remove();
                        });
                    }
                    else {
                        $('.context-menu-shadow').remove();
                        $(this).parent('div').find('.context-menu').hide();
                    }
                });

                $(document).on('keyup', function (evt) {
                    if (evt.keyCode == 27) {
                        $('.context-menu').hide();
                        $('.context-menu-shadow').remove();
                    }
                });

                // RegisterFieldChangeEvent();

            }
            else {
                $('#tab_content').html('<div class="alert-danger text-center p-2">Something went wrong</div>');
            }
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });

    return false;
}


var size = 2;
var timeout_id = 0;
function progress(id) {
    size = size + 1;
    if (size > 299) {
        clearTimeout(timeout_id);
    }
    if (parseInt(size / 3) <= 100) {
        var percentage = parseInt(size / 3).toString() + '%';
        $(id).closest('.file-upload-main').find('.progress-bar-percentage').text(percentage);
        $(id).closest('.file-upload-main').find('.progress-bar-override').css('width', percentage);
    }
}

function StartUpload(id) {
    $(id).closest('.file-upload-main').find('.progress-bar-percentage').text('0');
    $(id).closest('.file-upload-main').find('.progress-bar-override').css('width', '0');

    $(id).closest('.file-upload-main').find('.progress-bar-percentage').attr('style', 'display:block !important');
    size = 2;
    timeout_id = 0;
    timeout_id = setInterval(function () {
        progress(id);
    }, 20);
}

const CreateNew_Modal = (headerRequired, modalContent) => {
    var div_content = "<div class='col-sm-12' id='@id'>" + modalContent + "</div>";

    var modal = modal_popup;
    var id = 'div_trabau_filedesc_popup';
    var header = '<div class="modal-header myCard-heading">@headerTitle<input type="button" value="&times;" class="close white" onclick="CloseNew_Modal(\'0\',\'@id\');" /> </div>';
    modal = modal.replace('@id', id);
    modal = modal.replace('@id', id);
    div_content = div_content.replace('@id', id + '_content');
    header = header.replace('@headerTitle', (headerRequired ? '<h4 class="modal-title">@modalTitle</h4>' : ''));
    header = header.replace('@id', id);
    modal = modal.replace('@header', header);
    modal = modal.replace('@modalTitle', 'File Description');
    $('body').append(modal);
    HandlePopUp_HigherIndex('1', id);

    $('#' + id).find('.modal-body').html(div_content);

    return id + '_content';
}


const CloseNew_Modal = (val, id) => {
    HandlePopUp('0', id);

    var elem = document.createElement('div');
    elem.className = "modal-backdrop show";
    document.body.appendChild(elem);
}


const CreateFileDescription_Modal = (filename) => {
    var modal = modal_popup;
    var id = 'div_trabau_filedesc__popup';
    var header = '<div class="modal-header myCard-heading"> <h4 class="modal-title">@modalTitle</h4> <input type="button" value="&times;" class="close white" onclick="HandlePopUp(\'0\',\'@id\');" /> </div>';
    modal = modal.replace('@id', id);
    modal = modal.replace('@id', id);
    modal = modal.replace('@header', header);
    modal = modal.replace('@modalTitle', 'Description for "' + filename + '"');


    $('body').append(modal);
    HandlePopUp_HigherIndex('1', id);
    var div_desc = "<div class='col-sm-12' id='@id'><h6 class='pb-1'>Description</h6><textarea id='ta_file_desc' placeholder='Enter Description' class='form-control'></textarea></div>";
    var button = "<div class='col-sm-12 tab-footer'><a id='btnSaveFileDesc' class='cta-btn-md pointer' onclick='SaveTabFile();'>Save</a></div>";
    $('#' + id).find('.modal-body').html(div_desc + button);
}

var ele_fileupload;
const TabFileChanged = (id) => {
    ele_fileupload = id;
    var filename = ele_fileupload.files[0].name;

    CreateFileDescription_Modal(filename);
}


const SaveTabFile = () => {
    StartUpload(ele_fileupload);
    var filename = ele_fileupload.files[0].name;
    var tabdetails_id = $(ele_fileupload).closest('.tab-field').attr('id');
    var file_desc = $('#ta_file_desc').val();

    var reader = new FileReader();
    reader.readAsDataURL(ele_fileupload.files[0]);

    reader.onload = function () {
        var base64data = reader.result;//base64encoded string

        var datatoUpload = {
            base64data: base64data,
            filename: filename,
            tabdetails_id: tabdetails_id,
            file_desc: file_desc
        };

        $.ajax({
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: pathconfig + '/SaveTabFile',
            data: JSON.stringify(datatoUpload),
            success: function (msg) {
                var data = msg.d;
                var json_data = JSON.parse(data);

                DisplayTabFiles(json_data, ele_fileupload);

                clearTimeout(timeout_id);
                var percentage = '100%';
                $(ele_fileupload).closest('.file-upload-main').find('.progress-bar-percentage').text(percentage);
                $(ele_fileupload).closest('.file-upload-main').find('.progress-bar-override').css('width', percentage);

                setTimeout(function () {
                    $(ele_fileupload).closest('.file-upload-main').find('.progress-bar-override').hide();
                    $(ele_fileupload).closest('.file-upload-main').find('.progress-bar-percentage').attr('style', 'display:none !important');
                }, 500);

                setTimeout(function () {
                    $(ele_fileupload).closest('.file-upload-main').find('.progress-bar-override').css('width', 0);
                    $(ele_fileupload).closest('.file-upload-main').find('.progress-bar-override').show();
                }, 1000);

                HandlePopUp('0', 'div_trabau_filedesc__popup');

                var elem = document.createElement('div');
                elem.className = "modal-backdrop show";
                document.body.appendChild(elem);
            }
            ,
            error: function (xhr, ajaxOptions, thrownError) {

            }
        });
    };
}



const DisplayTabFiles = (json_data, id) => {

    if (json_data.length > 0) {
        var files_html = '<table class="table table-bordered">';
        files_html = files_html + '<tr>';
        files_html = files_html + '<th>File Name</th>';
        files_html = files_html + '<th>File Size</th>';
        files_html = files_html + '<th>File Date</th>';
        files_html = files_html + '<th>File Description</th>';
        files_html = files_html + '<th>File Type</th>';
        files_html = files_html + '<th>Download</th>';
        files_html = files_html + '<th>Remove</th>';
        files_html = files_html + '</tr>';

        for (var i = 0; i < json_data.length; i++) {
            var tr = '<tr>';
            tr = tr + '<td><p class="file-name">' + json_data[i].file_name + '<p></td>';
            tr = tr + '<td>' + json_data[i].file_size + '</td>';
            tr = tr + '<td>' + json_data[i].file_date + '</td>';
            tr = tr + '<td><p id="' + json_data[i].file_key + '" class="file-description">' + json_data[i].file_description + '</p></td>';
            tr = tr + '<td>' + json_data[i].file_type + '</td>';
            tr = tr + '<td><a href="../download.ashx?key=' + json_data[i].file_key + '">Download</a></td>';
            tr = tr + '<td id="' + json_data[i].file_key + '"><a href="javascript: void (0);" onclick="RemoveTabFile(this)">Remove</a></td>';

            tr = tr + '</tr>';

            files_html = files_html + tr;
        }

        files_html = files_html + '</table>';

        $(id).closest('.tab-field').find('.tab-files-output').html(files_html);
    }
    else {
        $(id).closest('.tab-field').find('.tab-files-output').html('');
    }
}

const RemoveTabFile = (id) => {
    var tabdetails_id = $(id).closest('.tab-field').attr('id');

    var filekey = $(id).parent('td').attr('id');
    var datatoUpload = {
        filekey: filekey,
        tabdetails_id: tabdetails_id
    };
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/RemoveTabFile',
        data: JSON.stringify(datatoUpload),
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            DisplayTabFiles(json_data, id);
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

const ViewFullHelpText = (id) => {
    var tabdetails_id = $(id).attr('class').replace('view-more-help ', '');
    var datatoUpload = {
        tabdetails_id: tabdetails_id
    };

    HandlePopUp('0', 'divTrabau_project_tabs');

    HandlePopUp('1', 'divTrabau_morehelp_text');
    $('#divTrabau_morehelp_text').find('.modal-body').html(loading_data);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetFullHelpText',
        data: JSON.stringify(datatoUpload),
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            $('#divTrabau_morehelp_text').find('.modal-body').html(json_data[0].fullhelptext);
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

function RegisterValidationEvent() {
    $('input[type=text]').unbind('blur');
    $('input[type=text]').blur(function () {
        if ($(this).val() != '') {
            $(this).css('border', '');
            $(this).parent('.tab-field').find('.alert-validation').remove();
        }
    });

    $('input[type=number]').unbind('blur');
    $('input[type=number]').blur(function () {
        if ($(this).val() != '') {
            $(this).css('border', '');
            $(this).parent('.tab-field').find('.alert-validation').remove();
        }
    });

    $('textarea').unbind('blur');
    $('textarea').blur(function () {
        if ($(this).val() != '') {
            $(this).css('border', '');
            $(this).parent('.tab-field').find('.alert-validation').remove();
        }
    });

    $('select').unbind('change');
    $('select').change(function () {
        if ($(this).val() != '') {
            $(this).css('border', '');
            $(this).parent('.tab-field').find('.alert-validation').remove();
        }
    });

    //$('.dx-htmleditor-content').blur(function () {
    //    if ($(this).closest('.tab-field').find('.input-field-data').val() != '') {
    //        //$(this).css('border', '');
    //        console.log($(this).parent('.tab-field').find('.alert-validation'));
    //        $(this).parent('.tab-field').find('.alert-validation').remove();
    //    }
    //});
}

function AddTableRow(id) {
    var tr = $(id).closest('table').find('tr:last').clone();
    var duplicatefound = false;
    var avoidaddempty = false;
    var avoidmessage = '';
    $(tr).find("input[type='text']").each(function () {
        this.value = '';
    });

    $(tr).find("select").each(function () {
        $(this)[0].selectedIndex = 0;
    });

    var prevtr = $(id).closest('table').find('tr:last');

    $(tr).find("select[skippreviousvalues='true']").each(function () {
        var select = $(this);

        var tdindex = $(select).closest('td').index();

        var prevvalue = $(prevtr).find('td').eq(tdindex).find('select').val();

        select.find('option').each(function () {
            var value = $(this).attr('value');
            if (parseInt(value) <= parseInt(prevvalue)) {
                $(this).remove();
            }
        });

        if ($(select).find('option').length === 0) {
            if ($(select).attr('avoidaddblank') === 'true') {
                avoidaddempty = true;
                if ($(select).attr('avoidaddblankmessage') !== '') {
                    avoidmessage = $(select).attr('avoidaddblankmessage');
                }
            }
        }
        //prevtr.find('select')

    });

    $(tr).find("input[type='text']").each(function () {
        $(this).removeAttr('id');
        $(this).removeClass('hasDatepicker');
    });

    $(tr).find("label[ClientSideDefaultValue!='']").each(function () {
        var funcClient = $(this).attr("ClientSideDefaultValue");
        if (funcClient !== '' && funcClient !== undefined) {
            var data = window[funcClient]();
            $(this).text(data);
        }
    });


    $(tr).find("label[clientsideavoidduplicate=true]").each(function () {
        if (!duplicatefound) {
            var value = $(this).text();
            var tdindex = $(this).closest('td').index();

            $('#dynamictable').find('tr').each(function () {
                if (!duplicatefound) {
                    var data = $(this).find('td').eq(tdindex).find('label[clientsideavoidduplicate=true]').text();
                    if (data === value) {
                        duplicatefound = true;
                    }
                }
            });
        }
    });

    if (!duplicatefound && !avoidaddempty) {
        $(id).closest('tbody').append(tr);
        $(id).remove();
        $(tr).find('td').each(function (j, el) {
            if ($(el).find('select[ClientSideOverrideReadOnly=true]').length > 0) {
                $(el).find('select[ClientSideOverrideReadOnly=true]').removeAttr('disabled');
            }
            else if ($(el).find('input[type="checkbox"][ClientSideOverrideReadOnly=true]').length > 0) {
                $(el).find('input[type=checkbox][ClientSideOverrideReadOnly=true]').removeAttr('disabled');
            }

            if ($(el).find('input[type="checkbox"][ClientSideOverrideValue!=""]').length > 0) {
                var overridevalue = $(el).find('input[type=checkbox][ClientSideOverrideValue!=""]').attr('ClientSideOverrideValue');
                $(el).find('input[type=checkbox][ClientSideOverrideValue!=""]').attr('checked', overridevalue);
            }
        });
        RegisterValidationEvent();
        ActivateDatePicker();
    }
    else {
        if (duplicatefound) {
            if ($(id).attr('addonerecordcurrentdayerrormessage') !== '') {
                toastr['error']($(id).attr('addonerecordcurrentdayerrormessage'));
            }
        }
        else if (avoidaddempty) {
            toastr['error'](avoidmessage);
        }
    }
}

function RemoveTableRow(id) {
    var tr = $(id).closest('tr');
    var CanRemove = false;
    var tdindex = $(id).closest("tr").index();
    $(tr).find("label[clientsideremovesamedate=true]").each(function () {
        if (!CanRemove) {
            var data = $(this).text();
            if (data === GetCurrentDate()) {
                CanRemove = true;
            }
        }
    });

    if ($(tr).find("label[clientsideremovesamedate=true]").length === 0) {
        CanRemove = true;
    }

    if (CanRemove) {
        var rowCount = $('#dynamictable tr').length;
        if (rowCount > 2) {
            var buttons = '';
            if ($(id).closest("tr").is(":last-child")) {
                buttons = $(id).closest("td").html();
            }
            $(id).closest('tr').remove();
            if ($(id).closest("tr").is(":last-child")) {
                addButtonOnLastRow(buttons);
            }
        }
    }
    else {
        if ($(id).attr('removeonlycurrentdayerrormessage') !== '') {
            if (tdindex === 0) {
                if ($(id).attr('removeonlymessageforinitialrow') !== '') {
                    toastr['error']($(id).attr('removeonlymessageforinitialrow'));
                }
                else {
                    toastr['error']($(id).attr('removeonlycurrentdayerrormessage'));
                }
            }
            else {
                toastr['error']($(id).attr('removeonlycurrentdayerrormessage'));
            }
        }
    }
}

function GetTableData() {
    var xml_data = '<tabdata>';
    var xml_element = '<data><index>@index</index><key>@key</key><value>@value</value></data>';

    if ($('#dynamictable').attr('read-only') == 'false') {
        var header = $('#dynamictable thead tr th').map(function () {
            return $(this).attr('id');
        });

        $('#dynamictable tbody tr').each(function (i, tr) {
            $(tr).find('td').each(function (j, el) {
                var _xml_element = xml_element;
                _xml_element = _xml_element.replace('@index', (i + 1).toString());

                var value = '';
                if ($(el).find('select').length > 0) {
                    value = $(this).find('select').val();
                }
                else if ($(el).find('input[type="text"]').length > 0) {
                    value = $(this).find('input[type=text]').val();
                }
                else if ($(el).find('input[type="checkbox"]').length > 0) {
                    value = $(this).find('input[type=checkbox]').prop('checked');
                }
                else if ($(el).find('label[collectdata="true"]').length > 0) {
                    value = $(this).find('label[collectdata="true"]').text();
                }

                if ($(el).find('select').length > 0 || $(el).find('input[type="text"]').length > 0 || $(el).find('input[type="checkbox"]').length > 0 || $(el).find('label[collectdata="true"]').length > 0) {
                    _xml_element = _xml_element.replace('@value', value);
                    var key = header[j];
                    _xml_element = _xml_element.replace('@key', key);

                    xml_data = xml_data + _xml_element;
                }
            });
        });

        xml_data = xml_data + '</tabdata>';

        return xml_data;
    }
    else {
        return '';
    }
}

function addButtonOnLastRow(buttons) {
    var tr = $('#dynamictable').find('tr:last').find('td:last');
    $(tr).html(buttons);
}

const GetFormData = () => {
    var jsonObj = [];
    var xml_data = '<tabdata>';
    var xml_element = '<data><key>@key</key><value>@value</value></data>';
    $('#tab_content').find(".tab-field").each(function () {
        var _xml_element = xml_element;
        var type = $(this).attr('type');
        var key = $(this).attr('id');
        var value = '';
        var item = {}
        if (type == 'textbox') {
            value = $(this).find('input[type="text"]').val();
        }
        else if (type == 'number') {
            value = $(this).find('input[type="number"]').val();
        }
        else if (type == 'textarea') {
            value = $(this).find('textarea').val();
        }
        else if (type == 'dropdown') {
            var isMultiDropdown = $(this).attr('multidropdown');
            if (isMultiDropdown == 'true') {
                value = $(this).find('input[type="hidden"]').val();
            }
            else {
                value = $(this).find('select').val();
            }
        }
        else if (type == 'editor') {
            value = $(this).find('.dx-htmleditor-content').html();
        }
        _xml_element = _xml_element.replace('@key', key);
        _xml_element = _xml_element.replace('@value', value);
        xml_data = xml_data + _xml_element;

        item["key"] = key;
        item["value"] = value;
        jsonObj.push(item);
    });
    xml_data = xml_data + '</tabdata>';
    //return xml_data;
    return jsonObj;
}

function sanitize(string) {
    const map = {
        '&': '&amp;',
        '<': '&lt;',
        '>': '&gt;',
        '"': '&quot;',
        "'": '&#x27;',
        "/": '&#x2F;',
    };
    const reg = /[&<>"'/]/ig;
    return string.replace(reg, (match) => (map[match]));
}

const SaveTabData = (preserveDataRequired) => {
    var button_text = $('#btnSaveTabData').html();

    var datatoSave = '';
    var datatoSave_table = '';
    var itemtype = '';

    if ($('#dynamictable').length > 0) {
        itemtype = 'table';
    }
    else {
        itemtype = 'form';
    }

    var validation_response = CheckValidation('table');
    if (validation_response == '') {

        validation_response = CheckValidation('form');
        if (validation_response == '') {
            if ($('#dynamictable').length > 0) {
                datatoSave_table = GetTableData();
            }

            datatoSave = GetFormData();


            var Files_TabDetailsId = '';
            $('#tab_content').find('.file-upload-main').each(function () {
                if (Files_TabDetailsId == '') {
                    Files_TabDetailsId = $(this).closest('.tab-field').attr('id');
                }
                else {
                    Files_TabDetailsId = Files_TabDetailsId + ',' + $(this).closest('.tab-field').attr('id');
                }
            });

            var XMLData = {
                itemtype: itemtype,
                datatoSave_table: datatoSave_table,
                datatoSave: JSON.stringify(datatoSave),
                Files_TabDetailsId: Files_TabDetailsId,
                preserveDataRequired: preserveDataRequired,
                uniquePropertyKey: (uniquePropertyKey == undefined ? '' : uniquePropertyKey),
                parentUniquePropertyKey: (parentUniquePropertyKey == undefined ? '' : parentUniquePropertyKey)
            };

            if (button_text == 'Next' || button_text == 'Apply') {
                $('#btnSaveTabData').html('Saving Data <img src="../assets/uploads/loading.svg" style="width:20px;" />');
                $('#btnSaveTabData').addClass('disabled');

                $.ajax({
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    url: pathconfig + '/SaveTabContent',
                    data: JSON.stringify(XMLData),
                    success: function (msg) {
                        var data = msg.d;
                        var json_data = JSON.parse(data);

                        $('#btnSaveTabData').html(button_text);
                        $('#btnSaveTabData').removeClass('disabled');

                        if (json_data[0].response == 'ok') {
                            var status = '';
                            if (!preserveDataRequired) {
                                var message = json_data[0].res_message;
                                status = json_data[0].res_response;

                                if (status == 'success') {
                                    var currentNavID = json_data[0].CurrentNavID;
                                    if (currentNavID != undefined && currentNavID != '') {
                                        var element = $("<div></div>");
                                        element.attr("nav_id", currentNavID);
                                        NavClick(element);
                                    }
                                }

                                var ClosePopupRequired = json_data[0].ClosePopupRequired;
                                if (ClosePopupRequired == '1') {
                                    CloseProjectTabs();
                                }
                                toastr[status](message);
                            }
                            else {
                                status = 'success';
                            }

                            if (status == 'success' && $('#btnSaveTabData').html() == 'Next') {
                                var li = $('#myTab1').find('a[class*="active"]').closest('li').next('li');
                                $(li).click();
                            }
                        }
                        else {
                            toastr['error']('Something went wrong, please refresh and try again');
                        }
                    }
                    ,
                    error: function (xhr, ajaxOptions, thrownError) {
                        $('#btnSaveTabData').html(button_text);
                        $('#btnSaveTabData').removeClass('disabled');
                    }
                });
            }
        }
        else {
            if (!inlineErrorsRequired) {
                toastr['error'](validation_response);
            }
        }
    }
    else {
        toastr['error'](validation_response);
    }

    return false;
}

var recent_target_fieldid;
function RegisterFieldChangeEvent() {
    $('.tab-field[changeeventrequired="true"]').find('select').change(function () {
        var tabDetailsId = $(this).closest('.tab-field').attr('id');
        var eventValue = $(this).val();

        var params = {
            tabDetailsId: tabDetailsId,
            param1: eventValue
        };

        $.ajax({
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: pathconfig + '/CallFieldChangeEvent',
            data: JSON.stringify(params),
            success: function (msg) {
                var data = msg.d;
                var json_data = JSON.parse(data.Result);

                for (var i = 0; i < json_data.length; i++) {
                    var targetFieldId = json_data[i].targetFieldId;
                    var targetField_DataSource = json_data[i].targetField_DataSource;;

                    ExecuteChangeEvent(targetFieldId, targetField_DataSource, eventValue);
                }
            }
            ,
            error: function (xhr, ajaxOptions, thrownError) {

            }
        });

        return false;
    });
}

const ExecuteChangeEvent = (targetFieldId, targetField_DataSource, eventValue) => {
    var target_type = $('#' + targetFieldId).attr('type');

    var params = {
        targetField_DataSource: targetField_DataSource,
        eventValue: eventValue,
        targetFieldId: targetFieldId,
        targetType: target_type
    };
    var multidropdown = '';

    if (eventValue != '' && eventValue != undefined) {
        if (target_type == 'label') {
            $('#' + targetFieldId).find('label').text('Loading');
        }
        else if (target_type == 'textbox') {
            $('#' + targetFieldId).find('input[type="text"]').val('Loading');
        }
        else if (target_type == "dropdown") {
            $('#' + targetFieldId).find('select').html('Loading');
            multidropdown = $('#' + targetFieldId).attr('multidropdown');
            if (multidropdown == 'true') {
                $('#' + targetFieldId).find('input[type="hidden"]').val('');
            }
        }

        $.ajax({
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: pathconfig + '/ExecuteFieldChangeEvent',
            data: JSON.stringify(params),
            success: function (msg) {
                var data = msg.d;
                var json_data = JSON.parse(data);
                if (json_data[0].response == 'ok') {
                    var result = json_data[0].result;

                    if (target_type == 'label') {
                        $('#' + targetFieldId).find('label').text(result);
                    }
                    else if (target_type == 'textbox') {
                        $('#' + targetFieldId).find('input').val(result);
                    }
                    else if (target_type == "dropdown") {
                        $('#' + targetFieldId).find('select').html(result);

                        if (multidropdown == 'true') {
                            var optionsChecked = json_data[0].optionsChecked;
                            $('#' + targetFieldId).find('input[type="hidden"]').val(optionsChecked);

                            var hfid = $('#' + targetFieldId).find('input[type="hidden"]').attr('id');
                            var ddl = $('#' + targetFieldId).find('select').attr('id');
                            SetSelect2Value(ddl, hfid, '0');
                        }

                    }
                }
            }
            ,
            error: function (xhr, ajaxOptions, thrownError) {

            }
        });
    }
    else {
        if (target_type == 'label') {
            $('#' + targetFieldId).find('label').text('');
        }
        else if (target_type == 'textbox') {
            $('#' + targetFieldId).find('input[type="text"]').val('');
        }
        else if (target_type == "dropdown") {
            $('#' + targetFieldId).find('select').html('');
            multidropdown = $('#' + targetFieldId).attr('multidropdown');
            if (multidropdown == 'true') {
                $('#' + targetFieldId).find('input[type="hidden"]').val('');
            }
        }
    }
}

const ChangeControlEvent = (preserveDataRequired) => {
    var current_navid = $('.tab-field').attr('current_navid');
    var eventValue = $('input[type=radio]:checked').val();

    var element = $("<div></div>");
    element.attr("currentnav_id", current_navid);
    element.attr("nav_id", eventValue);

    $('div[class*="modal-backdrop"]').remove();
    NavClick(element);
}



const CheckValidation = (type) => {
    var response = '';
    if (type == 'table') {
        $('#dynamictable tbody tr').each(function (i, tr) {
            $(tr).find('td').each(function (j, el) {
                var value = '';
                var required = '';
                var element;
                if ($(el).find('select').length > 0) {
                    value = $(this).find('select').val();
                    required = $(this).find('select').attr('required');
                    element = $(this).find('select');
                }
                else if ($(el).find('input[type="text"]').length > 0) {
                    value = $(this).find('input[type=text]').val();
                    required = $(this).find('input[type=text]').attr('required');
                    element = $(this).find('input[type="text"]');
                }
                else if ($(el).find('input[type="number"]').length > 0) {
                    value = $(this).find('input[type=number]').val();
                    required = $(this).find('input[type=number]').attr('required');
                    element = $(this).find('input[type="number"]');
                }

                if (required == 'required' && value == '') {
                    $(element).css('border', '1px solid red');
                    response = 'Please fill all required fields';
                }
            });

        });
    }
    else {
        $('.alert-validation').remove();
        $('#tab_content').find(".tab-field").each(function () {
            var type = $(this).attr('type');
            var value = '';
            var required = '';
            var error_message = '';
            var non_inline_error_message = '';
            var element;
            if (type == 'textbox') {
                value = $(this).find('input[type="text"]').val();
                required = $(this).find('input[type="text"]').attr('required');
                element = $(this).find('input[type="text"]');
                error_message = $(this).find('input[type="text"]').attr('error-message');
                non_inline_error_message = $(this).find('input[type="text"]').attr('non-inline-error-message');

                if ((value == '' || value == undefined)) {
                    if (inlineErrorsRequired) {
                        $(this).find('h6').append('<span class="alert-validation">&nbsp;' + error_message + '</span>');
                    }
                    else {
                        response = non_inline_error_message;
                    }
                }
            }
            else if (type == 'number') {
                value = $(this).find('input[type="number"]').val();
                required = $(this).find('input[type="number"]').attr('required');
                element = $(this).find('input[type="number"]');
                error_message = $(this).find('input[type="number"]').attr('error-message');
                non_inline_error_message = $(this).find('input[type="number"]').attr('non-inline-error-message');

                if ((value == '' || value == undefined)) {
                    if (inlineErrorsRequired) {
                        $(this).find('h6').append('<span class="alert-validation">&nbsp;' + error_message + '</span>');
                    }
                    else {
                        response = non_inline_error_message;
                    }
                }
            }
            else if (type == 'textarea') {
                value = $(this).find('textarea').val();
                required = $(this).find('textarea').attr('required');
                element = $(this).find('textarea');
                error_message = $(this).find('textarea').attr('error-message');
                non_inline_error_message = $(this).find('textarea').attr('non-inline-error-message');

                if ((value == '' || value == undefined)) {
                    if (inlineErrorsRequired) {
                        $(this).find('h6').append('<span class="alert-validation">&nbsp;' + error_message + '</span>');
                    }
                    else {
                        response = non_inline_error_message;
                    }
                }
            }
            else if (type == 'dropdown') {
                value = $(this).find('select').val();
                required = $(this).find('select').attr('required');
                element = $(this).find('select');
                error_message = $(this).find('select').attr('error-message');
                non_inline_error_message = $(this).find('select').attr('non-inline-error-message');

                if ((value == '' || value == undefined)) {
                    if (inlineErrorsRequired) {
                        $(this).find('h6').append('<span class="alert-validation">&nbsp;' + error_message + '</span>');
                    }
                    else {
                        response = non_inline_error_message;
                    }
                }
            }
            else if (type == 'editor') {
                value = $(this).find('.hidden-field-data').val();
                required = $(this).find('.hidden-field-data').attr('required');
                element = $(this).find('.dx-htmleditor-content');
                error_message = $(this).find('.hidden-field-data').attr('error-message');
                non_inline_error_message = $(this).find('.hidden-field-data').attr('non-inline-error-message');

                if ((value == '' || value == undefined)) {
                    if (inlineErrorsRequired) {
                        $(this).find('.field-name').append('<span class="alert-validation">&nbsp;' + error_message + '</span>');
                    }
                    else {
                        response = non_inline_error_message;
                    }
                }
            }

            if (required == 'required' && value == '') {
                $(element).css('border', '1px solid red');

                if (response == '') {
                    response = error_message;
                }
            }
        });
    }

    return response;
}


function ActivateHelpText() {
    $('[data-toggle="popover"]').popover({
        placement: 'top',
        trigger: 'manual',
        html: true
    }).on("mouseenter", function () {
        var _this = this;
        $(this).popover("show");
        $(".popover").on("mouseleave", function () {
            $(_this).popover('hide');
        });

        $('.view-more-help').click(function () {
            ViewFullHelpText(this);
        });
    }).on("mouseleave", function () {
        var _this = this;
        setTimeout(function () {
            if (!$(".popover:hover").length) {
                $(_this).popover("hide");
            }
        }, 300);
    });
}

const CloseNav_Popup = () => {
    HandlePopUp('0', 'divTrabau_project_tabs');
}

const CloseInfo_Popup = () => {
    HandlePopUp('0', 'divTrabau_project_info');
}

const CloseProjectTabs = () => {
    var navID = $('#aCloseProjectTabs').attr('nav-id');
    HandlePopUp('0', 'divTrabau_project_tabs');
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/RemovePreservedTabContentData',
        data: "{}",
        success: function (msg) {
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });

    if (navID != '' && navID != undefined) {
        var element = $("<div></div>");
        element.attr("nav_id", navID);
        NavClick(element);
    }
}

const CloseProjectTabsChild = () => {
    HandlePopUp('0', 'divTrabau_project_tabs_child');
    HandlePopUp('1', 'divTrabau_project_tabs');
}

const LoadActionDetails = (action_name) => {
    $('#div_project_content').html(loading_data);
    HandlePopUp('1', 'divTrabau_project_info');
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetProjectActionDetails',
        data: "{action:'" + action_name + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            var project_html = json_data[0].project_html;
            $('#div_project_content').html(project_html);

            ProjectTabClick($('#myTab1 li:first'));

            var ref_fn = json_data[0].function_name;
            if (ref_fn != '' && ref_fn != undefined) {
                window[ref_fn]();
            }
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });

    return false;
}

const ViewRowDetails = (id) => {
    var expandRowButton = false;
    if ($(id).attr('class').indexOf('fa-angle') > -1) {
        expandRowButton = true;
    }
    var dataSource = $(id).attr('data-source');
    var dataParameter = $(id).attr('primary-data');
    var gridTitle = $(id).attr('grid-title');
    var currentNavigationid = $(id).attr('current-navigationid');

    var target = '';
    if (!expandRowButton) {
        target = 'divTrabau_project_tabs_child_content';
        $('.navchild-modal-title').html(gridTitle);
        HandlePopUp('0', 'divTrabau_project_tabs');
        HandlePopUp('1', 'divTrabau_project_tabs_child');
        $('#divTrabau_project_tabs_child_content').html(loading_data);
    }
    else {
        target = 'td_expanded';
        var expandRowButtonDown = false;
        if ($(id).attr('class').indexOf('fa-angle-down') > -1) {
            expandRowButtonDown = true;
        }
        if (expandRowButtonDown) {
            $('.tr_expanded').remove();
            $('#dynamictable').find('i[class*="fa-angle-up"]').attr('class', 'fa fa-angle-down');

            $(id).attr('class', 'fa fa-angle-up');
            var additionalExpandHTML = '<tr class="tr_expanded"><td colspan="5" id="td_expanded">' + loading_data + '</td></tr>';
            $(id).parent('td').parent('tr').after(additionalExpandHTML);
        }
        else {
            $(id).attr('class', 'fa fa-angle-down');
            $('.tr_expanded').remove();
        }
    }
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetRowDetails',
        data: "{dataSource:'" + dataSource + "',dataParameter:'" + dataParameter + "',currentNavigationid:'" + currentNavigationid + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var tabs_html = json_data[0].response_data;

                $('#' + target).html(tabs_html);
            }
            else {
                $('#' + target).html('<div class="alert-danger text-center p-2">Something went wrong</div>');
            }
        }
    });
}


function LoadEditor() {
    $(function () {
        var editor = $(".html-editor").dxHtmlEditor({
            height: 400,
            toolbar: {
                items: [
                    "undo", "redo", "separator",
                    {
                        formatName: "size",
                        formatValues: ["8pt", "10pt", "12pt", "14pt", "18pt", "24pt", "36pt"]
                    },
                    {
                        formatName: "font",
                        formatValues: ["Arial", "Courier New", "Georgia", "Impact", "Lucida Console", "Tahoma", "Times New Roman", "Verdana"]
                    },
                    "separator", "bold", "italic", "strike", "underline", "separator",
                    "alignLeft", "alignCenter", "alignRight", "alignJustify", "separator",
                    "orderedList", "bulletList", "separator",
                    {
                        formatName: "header",
                        formatValues: [false, 1, 2, 3, 4, 5]
                    }, "separator",
                    "color", "background", "separator",
                    "link", "image", "separator",
                    "clear", "codeBlock", "blockquote", "separator",
                    "insertTable", "deleteTable",
                    "insertRowAbove", "insertRowBelow", "deleteRow",
                    "insertColumnLeft", "insertColumnRight", "deleteColumn"
                ]
            },
            mediaResizing: {
                enabled: true
            },
            onValueChanged: function (e) {
                $('input[id*="hfProjectDescription"]').val(editor.option("value"));
                $('input[id*="txtDescription"]').val(editor.option("value"));
            }
        }).dxHtmlEditor("instance");

        setTimeout(function () {
            var project_desc = $('input[id*="hfProjectDescription"]').val();
            if (project_desc != '' && project_desc != undefined) {
                //$('.dx-htmleditor-content').html(project_desc);
                editor.option("value", project_desc);
                $('input[id*="txtDescription"]').val(project_desc);
            }
        }, 50);
    });
}


function LoadField_Editor() {
    $(function () {
        $(".html-editor").dxHtmlEditor({
            height: 400,
            toolbar: {
                items: [
                    "undo", "redo", "separator",
                    {
                        formatName: "size",
                        formatValues: ["8pt", "10pt", "12pt", "14pt", "18pt", "24pt", "36pt"]
                    },
                    {
                        formatName: "font",
                        formatValues: ["Arial", "Courier New", "Georgia", "Impact", "Lucida Console", "Tahoma", "Times New Roman", "Verdana"]
                    },
                    "separator", "bold", "italic", "strike", "underline", "separator",
                    "alignLeft", "alignCenter", "alignRight", "alignJustify", "separator",
                    "orderedList", "bulletList", "separator",
                    {
                        formatName: "header",
                        formatValues: [false, 1, 2, 3, 4, 5]
                    }, "separator",
                    "color", "background", "separator",
                    "link", "image", "separator",
                    "clear", "codeBlock", "blockquote", "separator",
                    "insertTable", "deleteTable",
                    "insertRowAbove", "insertRowBelow", "deleteRow",
                    "insertColumnLeft", "insertColumnRight", "deleteColumn"
                ]
            },
            mediaResizing: {
                enabled: true
            },
            onValueChanged: function (e) {
                var hf_data = $(e.element).closest('.col-sm-12').find('.hidden-field-data');
                var input_data = $(e.element).closest('.col-sm-12').find('.input-field-data');

                $(hf_data).val(e.component.option("value"));
                $(input_data).val(e.component.option("value"));

                if (e.component.option("value") != '') {
                    $(e.element).find('.dx-htmleditor-content').css('border', '');
                    $(e.element).closest('.tab-field').find('.alert-validation').remove();
                }
            },
            onInitialized: function (e) {
                setTimeout(function () {
                    var hf_value = $(e.element).closest('.col-sm-12').find('.hidden-field-data').val();
                    var input_data = $(e.element).closest('.col-sm-12').find('.input-field-data');

                    if (hf_value != '' && hf_value != undefined) {
                        e.component.option("value", hf_value);
                        $(input_data).val(hf_value);
                    }
                }, 50);
            }
        }).dxHtmlEditor("instance");
    });
}

function ActivateDatePicker() {
    $('input[date-picker-required="true"]').each(function () {
        $(this).datepicker({
            changeMonth: true,
            changeYear: true,
            minDate: 0
        });
    });
}

function DisableEditor() {
    setTimeout(function () {
        $('.dx-viewport[disabled="disabled"]').find('.dx-htmleditor-content').attr('contenteditable', false);
    }, 100);
}

function GetCurrentDate() {
    const date = new Date();
    const formattedDate = date.toLocaleDateString('en-GB', {
        day: 'numeric', month: 'short', year: 'numeric'
    }).replace(/ /g, ' ');
    return formattedDate;
}
function GetCurrentTime() {
    var d = new Date();
    return d.toLocaleTimeString();
}

var backDropRequired = false;
function MainNavClick(anchor) {
    $(anchor).next('ul').hide();
    // console.log(li);
    //var itemIndex = $(li).index();
    //$(li).find('.dropdown-menu').css('left', '-' + (itemIndex * 50).toString() + 'px');

    HandlePopUp('1', 'divTrabau_NavigationContent');
    var htmlContent = $(anchor).next('ul').html();
    htmlContent = '<ul>' + htmlContent + '</ul>'
    $('#divTrabau_NavigationContent').find('.modal-body').html(htmlContent);
    $('#divTrabau_NavigationContent').find('.modal-content').css('width', ($(window).width() - 15).toString() + 'px');
    $('#divTrabau_NavigationContent').find('.dropdown-item ul').css('margin-top', '10px');
    $('#divTrabau_NavigationContent').find('.dropdown-item ul').css('width', '100%');
    $('#divTrabau_NavigationContent .modal-body li:not(:has(ul)').each(function () {
        $(this).attr('style', 'padding: .25rem 0 !important');
    });
    $('#divTrabau_NavigationContent .modal-body li').each(function () {
        $(this).find('.dropdown-item').attr('style', 'padding-left: 10px !important');
    });

    $('#divTrabau_NavigationContent .modal-dialog').animate({ left: "0" });

    backDropRequired = true;
    setTimeout(function () {
        CheckMobileNavigationBackground();
    }, 500);

    RegisterNavClickEvents();
}

function CloseMobileNavigation() {
    backDropRequired = false;
    $('#divTrabau_NavigationContent .modal-dialog').animate({ left: "-1000px" });
}

function CheckMobileNavigationBackground() {
    if (backDropRequired) {
        if ($('.modal-backdrop').length == 0) {
            // adding modal backdrop
            var elem = document.createElement('div');
            elem.className = "modal-backdrop show";
            document.body.appendChild(elem);

            console.log('backdrop added');
        }

        setTimeout(function () {
            CheckMobileNavigationBackground();
        }, 1000);
    }
}

var clicked = false;
function RegisterNavClickEvents() {
    $('#divTrabau_NavigationContent').find('.dropdown-item').click(function () {
        if (!clicked) {
            var childlevel = $(this).attr('childlevel');
            console.log(childlevel);
            if (childlevel != undefined) {
                clicked = true;
                if (parseInt(childlevel) <= 1) {
                    $('#divTrabau_NavigationContent').find('.dropdown-item ul').not($(this).find('ul[childlevel="' + childlevel + '"]')).slideUp(300);
                }
                $(this).find('ul[childlevel="' + childlevel + '"]').slideToggle(300);
                $(this).find('ul[childlevel="' + childlevel + '"]').css('position', 'relative');
                $(this).find('ul[childlevel="' + childlevel + '"]').css('top', '0');

                setTimeout(function () {
                    clicked = false;
                }, 500);
            }
        }
    });
}

function ActivateNumberOnly() {
    $('#tab_content input[type="number"]').each(function () {
        $(this).ForceNumericOnly();
    });
}

function RegisterDisabledNavigationEvents() {
    $('.navigationDisabled').unbind('click');

    $('.navigationDisabled').click(function () {
        var currentNavElement = $(this);
        var navID = currentNavElement.attr('nav_id');
        $.ajax({
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: pathconfig + '/ValidateNavigation',
            data: "{NavigationID:'" + navID + "'}",
            success: function (msg) {
                var message = msg.d;
                if (message != '' && message != undefined) {
                    toastr['error'](message);
                }
                else {
                    currentNavElement.removeClass('navigationDisabled');
                    currentNavElement.attr('onclick', 'NavClick(this)');
                    NavClick(currentNavElement);
                }
            }
        });
    });
}

