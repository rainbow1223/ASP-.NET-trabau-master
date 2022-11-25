Sys.Application.add_load(LoadNewProjectEvents);

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


var size = 2;
var id = 0;
function progress() {
    size = size + 1;
    if (size > 299) {
        clearTimeout(id);
    }
    if (parseInt(size / 3) <= 100) {
        var percentage = parseInt(size / 3).toString() + '%';
        $('.progress-bar-percentage').text(percentage);
        $('.progress-bar-override').css('width', percentage);
    }
}

function StartUpload(sender, args) {
    $('.progress-bar-percentage').text('0');
    $('.progress-bar-override').css('width', '0');

    $('.progress-bar-percentage').attr('style', 'display:block !important');
    size = 2;
    id = 0;
    id = setInterval("progress()", 20);
}

function UploadComplete(sender, args) {
    clearTimeout(id);
    var percentage = '100%';
    $('.progress-bar-percentage').text(percentage);
    $('.progress-bar-override').css('width', percentage);

    setTimeout(function () {
        $('.progress-bar-percentage').attr('style', 'display:none !important');
        $('input[id*="btnAddProjectFiles"]').click();
    }, 1500);

    setTimeout(function () {
        $('html, body').animate({ scrollTop: $('html, body').scrollTop() + 500 }, 800);
    }, 2000);
}

function openURL(myurl) {
    window.open(myurl, '_blank');
}

function ExpandProjectType_RadioButton() {
    var id = $('input[type="radio"][id*="rbtnlProjectType"]:checked');
    var selected = $(id).val();

    if (selected != undefined && selected != '') {
        var html_post = '<div class="panel-collapse in collapse" style=""><div class="panel-body"><p>Create a project to hire someone to work (one or more people) to do a job for you. You can add that person to the project later</p></div></div>';
        var html_pre = '<div class="panel-collapse in collapse" style=""><div class="panel-body"><p>Create a project for someone (one or more people) you already hired to work on</p></div></div>';
        var html_other = '<div class="panel-collapse in collapse" style=""><div class="panel-body"><p>Other type of projects</p></div></div>';

        if ($(id).parent('td').html().indexOf('panel-collapse') == -1) {
            $(id).parent('td').append((selected == "Post" ? html_post : (selected == 'Pre' ? html_pre : html_other)));
        }

        $(id).closest('tbody').find('div[class*="panel-collapse"]').slideUp(300);

        $(id).parent('td').find('div[class*="panel-collapse"]').slideDown(300);
    }
}

function ExpandProjectTypeStep2_RadioButton() {
    var id = $('input[type="radio"][id*="rbtnlOtherProject_Step2"]:checked');
    var selected = $(id).parent('td').find('label').text().toLowerCase();
    if (selected != undefined && selected != '') {
        var html = '<div class="panel-collapse in collapse" style=""><div class="panel-body"><p>Create a project to @content</p></div></div>';

        if ($(id).parent('td').html().indexOf('panel-collapse') == -1) {
            html = html.replace('@content', selected);

            $(id).parent('td').append(html);
        }

        $(id).closest('tbody').find('div[class*="panel-collapse"]').slideUp(300);

        $(id).parent('td').find('div[class*="panel-collapse"]').slideDown(300);
    }
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

        $('#vm_desc').click(function () {
            $('.popover').remove();
            HandlePopUp('1', 'divTrabau_moreproject_description');
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

function CheckOtherCommFunction() {
    try {
        var selected = $('select[id*="ddlCommunicationFunction"] option:selected').text();

        if (selected.toLowerCase().indexOf('other') > -1) {
            $('#div_other_comm_func').show();
            ValidatorEnable($('span[id*="rfvOtherCommFunction"]')[0], true);
            $('input[id*="txtManagerName"]').parent('div').removeClass('mt-3');
            $('span[id*=other_comm_func_help]').attr('data-content', 'Other Communication Function');
            $('span[id*="rfvOtherCommFunction"]').text('Enter Other Communication Function');
            $('#spn_comm_function').text('Other Communication Function');
            $('input[id*="txtOtherCommFunction"]').attr('placeholder', 'Enter Other Communication Function');
            $('span[id*="rfvOtherCommFunction"]').css('visibility', 'hidden');
        }
        else {
            $('#div_other_comm_func').hide();
            ValidatorEnable($('span[id*="rfvOtherCommFunction"]')[0], false);
            $('input[id*="txtManagerName"]').parent('div').addClass('mt-3');
            $('span[id*=other_comm_func_help]').attr('data-content', 'Communication Function');
            $('span[id*="rfvOtherCommFunction"]').text('Enter Communication Function');
        }
    } catch (e) {

    }
}

function ActivateOtherCommFunction_Default() { /* For non other projects */
    $('div[id*=div_other_comm_func]').show();
    $('span[id*=other_comm_func_help]').attr('data-content', 'This is simply the function of the project or the main function of the project: for example, Buy House, Design Logo, Replace Water Heater, Replace Radiator, Design Website, etc.  This is simply a verb.');
    ValidatorEnable($('span[id*="rfvOtherCommFunction"]')[0], true);
    $('span[id*="rfvOtherCommFunction"]').text('Enter Communication Function');
    $('span[id*="rfvOtherCommFunction"]').css('visibility', 'hidden');
    $('div[id*="div_other_comm_func"]').removeClass('mt-3');
    $('input[id*="txtManagerName"]').parent('div').removeClass('mt-3');
    $('#spn_comm_function').text('Communication Function');
    $('input[id*="txtOtherCommFunction"]').attr('placeholder', 'Enter Communication Function');
}

function FixTextboxClasses() {
    $("input[type='text']").each(function () {
        if (!$(this).hasClass('form-control')) {
            $(this).addClass('form-control')
        }
    });

    $("textarea").each(function () {
        if (!$(this).hasClass('form-control')) {
            $(this).addClass('form-control')
        }
    });

    $("select").each(function () {
        if (!$(this).hasClass('form-control')) {
            $(this).addClass('form-control')
        }
    });
}

function setTime(input, value) {
    $('input[id*="' + input + '"]').timepicker('setTime', value);
}