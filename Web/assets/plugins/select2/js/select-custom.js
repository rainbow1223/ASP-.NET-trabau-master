function RegisterSelect2(ddl, _text, _hf, _load) {
    $('[Id*="' + ddl + '"]').select2({
        multiple: true,
        placeholder: {
            id: '0', // the value of the option
            text: _text
        }
    })

    if (_load == '1') {
        $('[Id*="' + _hf + '"]').val('');
    }

    $('[Id*="' + ddl + '"]').change(function () {
        var ids = $('[Id*="' + ddl + '"]').val(); // works
        $('[Id*="' + _hf + '"]').val(ids);
    });

    SetSelect2Value(ddl, _hf, '0');
}

function SetSelect2Value(ddl, _hf, _postback) {
    var values = $('[Id*="' + _hf + '"]').val();
    if (typeof (values) == "undefined" || values == "0" || values == "") {
        values = "0";
    }

    var selectedValues = values.split(',');
    $('[Id*="' + ddl + '"]').val(selectedValues).trigger("change");
}