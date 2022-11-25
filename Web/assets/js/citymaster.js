var table;
$(document).ready(function () {
    GetCityDataForDataTable();

    $('#ddlCountry').change(function () {
        var CountryId = $(this).val();
        GetStates(CountryId, '');
    });

    $('#btnSaveCityDetails').click(function () {
        var CityID = $('#hfCityId').val();
        var CityName = $('#txtCityName').val();
        var StateID = $('#ddlState').val();
        var IsActive = $('#chkCityStatus').prop('checked');
        SaveCityDetails(CityID, CityName, StateID, IsActive);
        return false;
    });
});

function OpenAddCityPopup() {
    $('#btnSaveCityDetails').hide();
    HandlePopUp('1', 'divTrabau_Popup_EditCity');
    Clear();
    GetCountries('ddlCountry');
}

function SaveCityDetails(CityID, CityName, StateID, IsActive) {
    $('#btnSaveCityDetails').html('<img src="../../assets/uploads/loading-green-back.svg' + '" class="loading-request"/> Saving City Details');
    var city_details = {
        "CityID": CityID,
        "CityName": CityName,
        "StateID": StateID,
        "IsActive": IsActive
    };
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/SaveCityDetails',
        data: JSON.stringify(city_details),
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data.response == 'success') {


                HandlePopUp('0', 'divTrabau_Popup_EditCity');
                Clear();
                table.ajax.reload();
            }
            else {

            }
            $('#btnSaveCityDetails').html('Save City Details');
            toastr[json_data.response](json_data.message);
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
    return false;
}

function Clear() {
    $('#hfCityId').val('');
    $('#txtCityName').val('');
    $('#ddlState').html('');
    $('#ddlCountry').html('');
    $('#btnSaveCityDetails').hide();
}

function EditCity(id) {
    $('#btnSaveCityDetails').hide();
    HandlePopUp('1', 'divTrabau_Popup_EditCity');
    var data = $(id).attr('id');
    GetCityDetails(data);
}

function GetCityDetails(_data) {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetCityDetails',
        data: "{data:'" + _data + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data.response == 'ok') {
                var city = json_data.data;
                $('#txtCityName').val(city[0].CityName);

                var countries = json_data.countries;
                var country_html = '';
                for (var i = 0; i < countries.length; i++) {
                    country_html = country_html + '<option value="' + countries[i].CountryId + '">' + countries[i].CountryName + '</option>';
                }
                $('#ddlCountry').html(country_html);
                $('#ddlCountry').val(city[0].CountryId);
                $('#chkCityStatus').prop('checked', city[0].IsActive);
                GetStates(city[0].CountryId, city[0].StateID);

                $('#hfCityId').val(_data);
            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

function GetStates(_data, StateID) {
    $('#ddlState').html('<option>Loading States</option>');
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetStates',
        data: "{data:'" + _data + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data.response == 'ok') {
                var states = json_data.data;
                var state_html = '';
                for (var i = 0; i < states.length; i++) {
                    state_html = state_html + '<option value="' + states[i].StateId + '">' + states[i].StateName + '</option>';
                }
                $('#ddlState').html(state_html);
                if (StateID != '') {
                    $('#ddlState').val(StateID);
                }

                $('#btnSaveCityDetails').show();
            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}


function GetCountries(_target) {
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
                $('#' + _target).change();
            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}


function GetCityDataForDataTable() {
    table = $('#tblDataTable').DataTable({
        "processing": true,
        "serverSide": true,
        "iDisplayLength": 25,
        "ajax": {
            url: services_pathconfig + "/GetCityDataForDataTable", type: "post",
            ajaxGridOptions: { contentType: 'application/json; charset=utf-8' },
            datatype: "json"
        },
        columnDefs: [
            { orderable: false, targets: -1 }
        ],
        "columns": [
            { "data": "CityName" },
            { "data": "StateName" },
            { "data": "CountryName" },
            { "data": "IsActive" },
            {
                "data": "Id_Enc", render: function (data, type, row, meta) {
                    return type === 'display' ? '<a id="' + data + '" onclick="EditCity(this)"><i class="fa fa-edit"></i> Edit</a>' : data;
                }
            }
        ],
        destroy: true
    });

    //$('#tblDataTable_filter').find('input[type="search"]').attr('placeholder', 'Enter text to search');
    //$('#tblDataTable_filter').find('input[type="search"]').addClass('form-control');

}



function GetCityData(text, PageNumber, PageSize) {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetCities',
        data: "{text:'" + text + "',PageNumber:'" + PageNumber + "',PageSize:'" + PageSize + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'Ok') {
                var table_html = json_data[0].table_html;
                $('#div_cities').html(table_html);

                var total = json_data[0].total_records;

                var result = Paging(PageNumber, PageSize, total, 'myClass', 'myDisableClass');
                $(".pagingDiv").html(result);

                $(".pagingDiv").unbind('click');
                $(".pagingDiv").on("click", "a", function () {
                    var text = $('#txtSearch').val();
                    GetCityData(text, $(this).attr("pn"), PageSize);
                });
            }
            else {
                toastr["error"]("Error while getting city details, please refresh and try again");
            }
            $('#data-loading').hide();
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}