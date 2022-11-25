var table;
$(document).ready(function () {
    GetStateDataForDataTable();


    $('#btnSaveStateDetails').click(function () {
        var StateId = $('#hfStateId').val();
        var StateName = $('#txtStateName').val();
        var CountryID = $('#ddlCountry').val();
        var IsActive = $('#chkStateStatus').prop('checked');
        if (StateName != '') {
            if (CountryID != '') {
                SaveStateDetails(StateId, StateName, CountryID, IsActive);
            }
            else {
                toastr["error"]("Select Country Name");
            }
        }
        else {
            toastr["error"]("Enter State Name");
        }
        return false;
    });
});

function EditState(id) {
    $('#btnSaveStateDetails').hide();
    HandlePopUp('1', 'divTrabau_Popup_EditState');
    var data = $(id).attr('id');
    GetStateDetails(data);
}

function OpenAddStatePopup() {
    HandlePopUp('1', 'divTrabau_Popup_EditState');
    Clear();
    GetCountries('ddlCountry', '');
}

function Clear() {
    $('#hfStateId').val('');
    $('#txtStateName').val('');
    $('#ddlCountry').html('');
    $('#btnSaveStateDetails').hide();
    $('#chkStateStatus').prop('checked', true);
}

function GetStateDataForDataTable() {
    table = $('#tblDataTable').DataTable({
        "processing": true,
        "serverSide": true,
        "iDisplayLength": 25,
        "ajax": {
            url: services_pathconfig + "/GetStateDataForDataTable", type: "post",
            ajaxGridOptions: { contentType: 'application/json; charset=utf-8' },
            datatype: "json"
        },
        columnDefs: [
            { orderable: false, targets: -1 }
        ],
        "columns": [
            { "data": "StateName" },
            { "data": "CountryName" },
            { "data": "IsActive" },
            {
                "data": "StateId_Enc", render: function (data, type, row, meta) {
                    return type === 'display' ? '<a id="' + data + '" onclick="EditState(this)"><i class="fa fa-edit"></i> Edit</a>' : data;
                }
            }
        ],
        destroy: true
    });
}


function GetStateDetails(_data) {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetStateDetails',
        data: "{data:'" + _data + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data.response == 'ok') {
                var state = json_data.data;
                $('#txtStateName').val(state[0].StateName);
                $('#chkStateStatus').prop('checked', state[0].IsActive);
                $('#hfStateId').val(_data);
                GetCountries('ddlCountry', state[0].CountryId);

            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}


function GetCountries(_target, CountryId) {
    $('#' + _target).html('<option>Loading Countries</option>');
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetCountries',
        data: "{}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data.response == 'ok') {
                var countries = json_data.data;

                var countries_html = '';
                for (var i = 0; i < countries.length; i++) {
                    countries_html = countries_html + '<option value="' + countries[i].CountryId + '">' + countries[i].CountryName + '</option>';
                }
                $('#' + _target).html(countries_html);
                if (CountryId != '') {
                    $('#' + _target).val(CountryId);
                }
                $('#btnSaveStateDetails').show();
            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}



function SaveStateDetails(StateId, StateName, CountryId, IsActive) {
    $('#btnSaveStateDetails').html('<img src="../../assets/uploads/loading-green-back.svg' + '" class="loading-request"/> Saving State Details');
    var state_details = {
        "StateID": StateId,
        "StateName": StateName,
        "CountryId": CountryId,
        "IsActive": IsActive
    };
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/SaveStateDetails',
        data: JSON.stringify(state_details),
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data.response == 'success') {
                HandlePopUp('0', 'divTrabau_Popup_EditState');
                Clear();
                table.ajax.reload();
            }
            else {

            }
            $('#btnSaveStateDetails').html('Save State Details');
            toastr[json_data.response](json_data.message);
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
    return false;
}
