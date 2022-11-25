Sys.Application.add_load(LoadApplyJobEvents);


function LoadEditor() {
    var job_desc = $('input[id*="hfCoverLetter"]').val();
    $(function () {
        try {
           
            if (job_desc != '' && job_desc != undefined) {
                $(".html-editor").reload();
            }
        } catch (e) {

        }
        
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
                $('input[id*="hfCoverLetter"]').val(editor.option("value"));
            }  
        }).dxHtmlEditor("instance");

        
        setTimeout(function () {
            if (job_desc != '' && job_desc != undefined) {
                //$('.dx-htmleditor-content').html(job_desc);
                editor.option("value", job_desc);
            }
        }, 50);

        //setTimeout(function () {
        //    $('.dx-htmleditor-content').unbind('DOMSubtreeModified');
        //    $('.dx-htmleditor-content').bind('DOMSubtreeModified', function () {
        //        $('input[id*="hfCoverLetter"]').val(editor.option("value"));
        //    });
        //}, 500);
    });


    
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
        $('input[id*="btnAddProfileFiles"]').click();
    }, 1500);


}

function RegisterFP_BidChangeEvent() {
    $('input[id*="txtBid_FixedPrice"]').on('inputchange', function () {
        $('input[id*="txtReceive_FixedPrice"]').unbind('inputchange');

        var Bid_FixedPrice = $(this).val();
        if (Bid_FixedPrice == '' || Bid_FixedPrice == '0') {
            $('span[id*="lblTrabauServiceFee_FixedPrice"]').text('0');
            $('input[id*="txtReceive_FixedPrice"]').val('0');
        }
        else {
            var Fee_FixedPrice = (parseFloat(Bid_FixedPrice) * 0.1).toFixed(2);
            $('span[id*="lblTrabauServiceFee_FixedPrice"]').text(Fee_FixedPrice.toString());
            $('input[id*="txtReceive_FixedPrice"]').val((parseFloat(Bid_FixedPrice) - parseFloat(Fee_FixedPrice)).toFixed(2).toString());


        }
    });
}

function RegisterFP_ReceiveChangeEvent() {
    $('input[id*="txtReceive_FixedPrice"]').on('inputchange', function () {
        $('input[id*="txtBid_FixedPrice"]').unbind('inputchange');

        var Receive_FixedPrice = $(this).val();
        if (Receive_FixedPrice == '' || Receive_FixedPrice == '0') {
            $('span[id*="lblTrabauServiceFee_FixedPrice"]').text('0');
            $('input[id*="txtBid_FixedPrice"]').val('0');
        }
        else {
            var Fee_FixedPrice = (parseFloat(Receive_FixedPrice) * 0.1).toFixed(2);
            $('span[id*="lblTrabauServiceFee_FixedPrice"]').text(Fee_FixedPrice.toString());
            $('input[id*="txtBid_FixedPrice"]').val((parseFloat(Receive_FixedPrice) + parseFloat(Fee_FixedPrice)).toFixed(2).toString());
        }
    });
}



function RegisterHR_BidChangeEvent() {
    $('input[id*="txtBid_HourlyRate"]').on('inputchange', function () {
        $('input[id*="txtReceive_HourlyRate"]').unbind('inputchange');

        var Bid_FixedPrice = $(this).val();
        if (Bid_FixedPrice == '' || Bid_FixedPrice == '0') {
            $('span[id*="lblTrabauServiceFee_HourlyRate"]').text('0');
            $('input[id*="txtReceive_HourlyRate"]').val('0');
        }
        else {
            var Fee_FixedPrice = (parseFloat(Bid_FixedPrice) * 0.1).toFixed(2);
            $('span[id*="lblTrabauServiceFee_HourlyRate"]').text(Fee_FixedPrice.toString());
            $('input[id*="txtReceive_HourlyRate"]').val((parseFloat(Bid_FixedPrice) - parseFloat(Fee_FixedPrice)).toFixed(2).toString());


        }
    });
}

function RegisterHR_ReceiveChangeEvent() {
    $('input[id*="txtReceive_HourlyRate"]').on('inputchange', function () {
        $('input[id*="txtBid_HourlyRate"]').unbind('inputchange');

        var Receive_FixedPrice = $(this).val();
        if (Receive_FixedPrice == '' || Receive_FixedPrice == '0') {
            $('span[id*="lblTrabauServiceFee_HourlyRate"]').text('0');
            $('input[id*="txtBid_HourlyRate"]').val('0');
        }
        else {
            var Fee_FixedPrice = (parseFloat(Receive_FixedPrice) * 0.1).toFixed(2);
            $('span[id*="lblTrabauServiceFee_HourlyRate"]').text(Fee_FixedPrice.toString());
            $('input[id*="txtBid_HourlyRate"]').val((parseFloat(Receive_FixedPrice) + parseFloat(Fee_FixedPrice)).toFixed(2).toString());
        }
    });
}

function openURL(myurl) {
    window.open(myurl, '_blank');
}