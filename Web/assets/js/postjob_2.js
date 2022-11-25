Sys.Application.add_load(LoadPostJobEvents);

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
                $('input[id*="hfJobDescription"]').val(editor.option("value"));
            }
        }).dxHtmlEditor("instance");

        setTimeout(function () {
            var job_desc = $('input[id*="hfJobDescription"]').val();
            if (job_desc != '' && job_desc != undefined) {
                //$('.dx-htmleditor-content').html(job_desc);
                editor.option("value", job_desc);
            }
        }, 50);
    });




    //setTimeout(function () {
    //    $('.dx-htmleditor-content').unbind('DOMSubtreeModified');
    //    $('.dx-htmleditor-content').bind('DOMSubtreeModified', function () {
    //        $('input[id*="hfJobDescription"]').val($('.dx-htmleditor-content').html());
    //    });
    //}, 500);
}

function ActivateToolTip() {
    // ActivateTooltipText('rbtnlTypeOfWork');
    var tow_toolip = $('input[id*="rbtnlTypeOfWork"]:checked').parent('span').attr('custom-tooltip');
    $('.tow-tooltip').attr('data-content', tow_toolip);


    $('input[id*="rbtnlTypeOfWork"]').change(function () {
        var tow_toolip = $('input[id*="rbtnlTypeOfWork"]:checked').parent('span').attr('custom-tooltip');
        $('.tow-tooltip').attr('data-content', tow_toolip);
    });




    var hp_toolip = $('input[id*="rbtnlPaymentType"]:checked').parent('span').attr('custom-tooltip');
    $('.hp-tooltip').attr('data-content', hp_toolip);


    $('input[id*="rbtnlPaymentType"]').change(function () {
        var hp_toolip = $('input[id*="rbtnlPaymentType"]:checked').parent('span').attr('custom-tooltip');
        $('.hp-tooltip').attr('data-content', hp_toolip);
    });



    $('[data-toggle="popover"]').popover({
        placement: 'top',
        trigger: 'hover'
    });
}

function ActivateToolTip_AdditionalSkills() {
    $('[data-toggle="popover"]').popover({
        placement: 'top',
        trigger: 'hover'
    });
}

function ActivateTooltipText(table) {

    $("table[id*='" + table + "'] td").each(function () {
        var tooltip_text = $(this).find('span').attr('custom-tooltip');
        //$(this).attr('data-toggle', 'Popover title');
        //$(this).attr('title', 'bottom');
        $(this).attr('aria-describedby', tooltip_text);
    });
}
function openURL(myurl) {
    window.open(myurl, '_blank');
}

function CheckRadioButton() {
    $('input[type="radio"]:checked').closest('td').addClass('special-radio-btn-selected');

}

function CheckCheckboxButton() {
    $('input[type="checkbox"]:checked').closest('td').addClass('btn-selected');
}

function CheckBSE_RadioButton() {
    $('input[type="radio"]:checked').closest('td').addClass('selected');
}

function CountCharacters() {
    //  $('#s_characters_count').text($('textarea[id*="txtJobDescription"]').val().length);
}
var size = 2;
var id = 0;
function progress() {
    size = size + 1;
    if (size > 299) {
        clearTimeout(id);
    }
    var percentage = parseInt(size / 3).toString() + '%';
    $('.progress-bar-percentage').text(percentage);
    $('.progress-bar-override').css('width', percentage);
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
        $('input[id*="btnAddProfileFiles"]').click();
    }, 1500);
}

function RadioButtonSelection(rbtnl) {
    $('table[id*="' + rbtnl + '"]').find('td').addClass('col-lg-4 col-md-6 col-sm-6');
}

function RadioButtonSelection_Override() {
    $('.special-radio-btn-override').find('td').addClass('col-lg-6 col-md-6 col-sm-6');
}

function getB64Str(buffer) {
    var binary = '';
    var bytes = new Uint8Array(buffer);
    var len = bytes.byteLength;
    for (var i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
}


