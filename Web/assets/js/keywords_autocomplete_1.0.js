
function AutoCompleteTextBox_Keywords(source, target, targetfull, servicepath, splitter, ref_fn) {
    source = $('input[id*="' + source + '"]').attr('id');
    target = $('input[id*="' + target + '"]').attr('id');
    $("#" + source).autocomplete({
        // appendTo: $("#" + source).parent(),

        source: function (request, response) {
            $.ajax({
                url: servicepath,
                data: "{ 'Prefix': '" + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",

                success: function (data) {
                    if (data.d.length > 0) {

                        response($.map(data.d, function (item) {
                            return {
                                label: item.split(splitter)[0],
                                val: item.split(splitter)[1]
                            }
                        }))
                    }
                    //else {
                    //    response([{ label: 'No results found.', val: -1 }]);
                    //}
                },
                error: function (response) {
                    // alert(response.responseText);
                },
                failure: function (response) {
                    // alert(response.responseText);
                }
            });
        },
        //open: function (e, i) {
        //    alert("open: The suggestion list is opened!!!");
        //},
        select: function (e, i) {

            // $("#" + target).val(i.item.val);
            if (targetfull != '') {
                $("#" + targetfull).val(i.item.label);
            }
            keyword_selected_index_changed(source, target, targetfull, servicepath, splitter, ref_fn, i.item.label, i.item.val);

            if (ref_fn != '') {
                window[ref_fn]();
            }
        },

        minLength: 3
    });
}

function LoadKeywords(_kinput, _khiddenfield) {
    _kinput = $('input[id*="' + _kinput + '"]').attr('id');
    _khiddenfield = $('input[id*="' + _khiddenfield + '"]').attr('id');

    var _placeholder = $('#' + _kinput).attr('placeholder');
    var text = $('#' + _khiddenfield).val();
    if (text != '') {
        $list = $('<ul />');
        var $container = $('<div class="multipleInput-container" />');
        for (var i = 0; i < text.split(',').length; i++) {
            if (text.split(',')[i].trim() != '') {
                $list.append($('<li class="multipleInput-email"><span data="' + text.split(',')[i] + '"> ' + text.split(',')[i] + '</span></li>')
                                 .append($('<a class="multipleInput-close" title="Remove" onclick="RemoveKeyword(this)">x</a>')
                                 )
                            );
            }
        }

        var $input = $('<input type="text" class="' + _kinput + '" id="' + _kinput + '" placeholder="' + _placeholder + '" />');
        $container.append($list).append($input).insertAfter($('#' + _kinput));
        $('#' + _kinput).remove();
        $('#' + _kinput).focus();
    }
}

function keyword_selected_index_changed(_kinput, _khiddenfield, targetfull, servicepath, splitter, ref_fn, input_text, input_value) {

    var val = '';
    val = $('#' + _kinput).val();
    if (!CheckExists(_khiddenfield, input_value)) {
        if (!$('input.' + _kinput).length) {
            $list = $('<ul />');
            var $container = $('<div class="multipleInput-container" />');
            $list.append($('<li class="multipleInput-email"><span data="' + input_value + '"> ' + input_text + '</span></li>')
                             .append($('<a class="multipleInput-close" title="Remove" onclick="RemoveKeyword(this)">x</a>')
                             )
                        );
            var _placeholder = $('#' + _kinput).attr('placeholder');
            var $input = $('<input type="text" class="' + _kinput + '" id="' + _kinput + '" placeholder="' + _placeholder + '" />');
            $container.append($list).append($input).insertAfter($('#' + _kinput));
            $('#' + _kinput).remove();
            $('.' + _kinput).focus();
        }
        else {
            $('#' + _kinput).parent('div[class="multipleInput-container"]').find('ul').append($('<li class="multipleInput-email"><span data="' + input_value + '"> ' + input_text + '</span></li>')
                     .append($('<a class="multipleInput-close" title="Remove" onclick="RemoveKeyword(this)">x</a>')
                     ));

        }


        if ($('#' + _khiddenfield).val() == '') {
            $('#' + _khiddenfield).val(input_value);
        }
        else {
            $('#' + _khiddenfield).val($('#' + _khiddenfield).val() + ',' + input_value);
        }
        setTimeout(function () {
            AutoCompleteTextBox_Keywords(_kinput, _khiddenfield, targetfull, servicepath, splitter, ref_fn);
            $('.' + _kinput).val('');
            $('.' + _kinput).focus();
        }, 100);


    }
    else {
        Swal.fire({ type: 'error', title: 'Keyword ' + val + ' already exists', showConfirmButton: true, timer: 1500 })
    }
}

function CheckExists(_khiddenfield, text) {
    var exists = false;
    var keywords = $('#' + _khiddenfield).val();
    for (var i = 0; i < keywords.split(',').length; i++) {
        if (keywords.split(',')[i].toString().trim() == text.toString().trim()) {
            exists = true;
            break;
        }
    }
    return exists;
}

function RemoveKeyword(ele) {
    var hf = $(ele).closest('div').parent('div').find('input[type="hidden"]').attr('id');
    $(ele).parent('li').remove();

    var emails = new Array();
    $('#' + hf).parent('div').find('.multipleInput-email span').each(function () {
        emails.push($(this).attr('data').toString().trim());
    });

    $('#' + hf).val(emails.join().trim());
}


