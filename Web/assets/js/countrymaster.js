var table;
$(document).ready(function () {
    GetCountryDataForDataTable();

    //$('#ddlCountry').change(function () {
    //    var CountryId = $(this).val();
    //    GetStates(CountryId, '');
    //});

    $('#btnSaveCountryDetails').click(function () {
        var CountryID = $('#hfCountryId').val();
        var CountryName = $('#txtCountryName').val();
        var CountryCode = $('#txtCountryCode').val();
        var CountryPrefix = $('#txtCountryPrefix').val();
        var TimeZone = $('#txtTimeZone').val();
        var TimeZoneDetails = $('#txtTimeZoneDetails').val();
        var IsActive = $('#chkCountryStatus').prop('checked');
        if (CountryName != '') {
            if (CountryCode != '') {
                if (CountryPrefix != '') {
                    if (TimeZone != '') {
                        if (TimeZoneDetails != '') {
                            SaveCountryDetails(CountryID, CountryName, CountryCode, CountryPrefix, TimeZone, TimeZoneDetails, IsActive);
                        }
                        else {
                            toastr["error"]("Enter Time Zone Details");
                        }
                    }
                    else {
                        toastr["error"]("Enter Time Zone");
                    }
                }
                else {
                    toastr["error"]("Enter Country Prefix");
                }
            }
            else {
                toastr["error"]("Enter Country Code");
            }
        }
        else {
            toastr["error"]("Enter Country Name");
        }
        return false;
    });
});

function EditCountry(id) {
    $('#btnSaveCountryDetails').hide();
    HandlePopUp('1', 'divTrabau_Popup_EditCountry');
    var data = $(id).attr('id');
    GetCountryDetails(data);
}

function OpenAddCountryPopup() {
    HandlePopUp('1', 'divTrabau_Popup_EditCountry');
    Clear();
    $('#btnSaveCountryDetails').show();
}

function Clear() {
    $('#hfCountryId').val('');
    $('#txtCountryName').val('');
    $('#txtCountryCode').val('');
    $('#txtCountryPrefix').val('');
    $('#txtTimeZone').val('');
    $('#txtTimeZoneDetails').val('');
    $('#btnSaveCountryDetails').hide();
}

function GetCountryDataForDataTable() {
    table = $('#tblDataTable').DataTable({
        "processing": true,
        "serverSide": true,
        "iDisplayLength": 25,
        "ajax": {
            url: services_pathconfig + "/GetCountryDataForDataTable", type: "post",
            ajaxGridOptions: { contentType: 'application/json; charset=utf-8' },
            datatype: "json"
        },
        columnDefs: [
            { orderable: false, targets: -1 }
        ],
        "columns": [
            { "data": "CountryName" },
            { "data": "CountryCode" },
            { "data": "CountryPrefix" },
            { "data": "TimeZone" },
            { "data": "TimeZoneDetails" },
            { "data": "IsActive" },
            {
                "data": "CountryId_Enc", render: function (data, type, row, meta) {
                    return type === 'display' ? '<a id="' + data + '" onclick="EditCountry(this)"><i class="fa fa-edit"></i> Edit</a>' : data;
                }
            }
        ],
        destroy: true
    });
}


function GetCountryDetails(_data) {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetCountryDetails',
        data: "{data:'" + _data + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data.response == 'ok') {
                var country = json_data.data;
                $('#txtCountryName').val(country[0].CountryName);
                $('#txtCountryCode').val(country[0].CountryCode);
                $('#txtCountryPrefix').val(country[0].CountryPrefix);
                $('#txtTimeZone').val(country[0].TimeZone);
                $('#txtTimeZoneDetails').val(country[0].TimeZoneDetails);

                $('#chkCountryStatus').prop('checked', country[0].IsActive);

                $('#hfCountryId').val(_data);

                $('#btnSaveCountryDetails').show();
            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}



function SaveCountryDetails(CountryID, CountryName, CountryCode, CountryPrefix, TimeZone, TimeZoneDetails, IsActive) {
    $('#btnSaveCountryDetails').html('<img src="../../assets/uploads/loading-green-back.svg' + '" class="loading-request"/> Saving Country Details');
    var country_details = {
        "CountryID": CountryID,
        "CountryName": CountryName,
        "CountryCode": CountryCode,
        "CountryPrefix": CountryPrefix,
        "TimeZone": TimeZone,
        "TimeZoneDetails": TimeZoneDetails,
        "IsActive": IsActive
    };
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/SaveCountryDetails',
        data: JSON.stringify(country_details),
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data.response == 'success') {
                HandlePopUp('0', 'divTrabau_Popup_EditCountry');
                Clear();
                table.ajax.reload();
            }
            else {

            }
            $('#btnSaveCountryDetails').html('Save Country Details');
            toastr[json_data.response](json_data.message);
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
    return false;
}