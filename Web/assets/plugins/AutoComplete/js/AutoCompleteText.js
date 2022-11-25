
function AutoCompleteTextBox(source, target, targetfull, servicepath, splitter, ref_fn) {
    $("#" + source).keyup(function (e) {
        var code = e.keyCode || e.which;
        if (code != 13) {
            $('#' + target).val('');
        }
    });

    $("#" + source).keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code != 13) {
            $('#' + target).val('');
        }
    });

    RegisterAutoComplete_Loading(source);

    $("#" + source).autocomplete({
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
                },
                error: function (response) {
                    //       alert(response.responseText);
                },
                failure: function (response) {
                    //         alert(response.responseText);
                }
            });
        },
        //open: function (e, i) {
        //    alert("open: The suggestion list is opened!!!");
        //},
        select: function (e, i) {

            $("#" + target).val(i.item.val);
            if (targetfull != '') {
                $("#" + targetfull).val(i.item.label);
            }

            if (ref_fn != '') {
                window[ref_fn]();
            }

            try {

            } catch (e) {
                $('.autocomplete_loading').remove();
            }
        },

        minLength: 3
    });

    function RegisterAutoComplete_Loading(source) {
        var timer;
        var delay = 400; // 0.4 seconds delay after last input
        $("#" + source).on('inputchange', function () {
            try {
                if ($(this).parent('[class*="autocomplete_loading_main"]').html().indexOf('autocomplete_loading') == -1) {
                    var elem_loading = document.createElement('div');
                    elem_loading.className = "autocomplete_loading";
                    elem_loading.innerHTML = '<img src="/assets/images/loading_main.svg">';
                    $(this).parent('[class*="autocomplete_loading_main"]').append(elem_loading);
                }
                window.clearTimeout(timer);
                timer = window.setTimeout(function () {
                    //insert delayed input change action/event here
                    $('.autocomplete_loading').remove();

                }, delay);
            } catch (e) {

            }

        });
    }
}


