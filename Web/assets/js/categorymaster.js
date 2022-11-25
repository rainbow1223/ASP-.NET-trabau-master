var table;
$(document).ready(function () {
    GetServiceCategoryDataForDataTable();


    $('#btnSaveServiceCategory').click(function () {
        var ServiceCategoryId = $('#hfServiceCategoryId').val();
        var ServiceCategoryName = $('#txtServiceCategoryName').val();
        var CategoryTypeId = $('#ddlCategoryType').val();
        var IsActive = $('#chkServiceCategoryStatus').prop('checked');
        if (ServiceCategoryName != '') {
            if (CategoryTypeId != '') {
                SaveServiceCategory(ServiceCategoryId, ServiceCategoryName, CategoryTypeId, IsActive);
            }
            else {
                toastr["error"]("Select Category Type");
            }
        }
        else {
            toastr["error"]("Enter Service Category Name");
        }
        return false;
    });
});

function EditServiceCategory(id) {
    $('#btnSaveServiceCategory').hide();
    HandlePopUp('1', 'divTrabau_Popup_EditServiceCategory');
    var data = $(id).attr('id');
    GetServiceCategoryDetails(data);
}

function OpenAddServiceCategoryPopup() {
    HandlePopUp('1', 'divTrabau_Popup_EditServiceCategory');
    Clear();
    GetCategoryTypes('');
}

function Clear() {
    $('#hfServiceCategoryId').val('');
    $('#txtServiceCategoryName').val('');
    $('#ddlCategoryType').html('');
    $('#btnSaveServiceCategory').hide();
    $('#chkServiceCategoryStatus').prop('checked', true);
}


function GetServiceCategoryDataForDataTable() {
    table = $('#tblDataTable').DataTable({
        "processing": true,
        "serverSide": true,
        "iDisplayLength": 25,
        "ajax": {
            url: services_pathconfig + "/GetServicesCategoryDataForDataTable", type: "post",
            ajaxGridOptions: { contentType: 'application/json; charset=utf-8' },
            datatype: "json"
        },
        columnDefs: [
            { orderable: false, targets: -1 }
        ],
        "columns": [
            { "data": "ServiceCategoryName" },
            { "data": "CategoryType" },
            { "data": "IsActive" },
            {
                "data": "ServiceCategoryId_Enc", render: function (data, type, row, meta) {
                    return type === 'display' ? '<a id="' + data + '" onclick="EditServiceCategory(this)"><i class="fa fa-edit"></i> Edit</a>' : data;
                }
            }
        ],
        destroy: true
    });
}


function GetServiceCategoryDetails(_data) {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetServiceCategoryDetails',
        data: "{data:'" + _data + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data.response == 'ok') {
                var service = json_data.data;
                $('#txtServiceCategoryName').val(service[0].ServiceCategoryName);
                $('#chkServiceCategoryStatus').prop('checked', service[0].IsActive);
                $('#hfServiceCategoryId').val(_data);
                GetCategoryTypes(service[0].CategoryParentId);

            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}



function GetCategoryTypes(CategoryTypeId) {
    $('#ddlCategoryType').html('<option>Loading Category Types</option>');
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetCategoryTypes',
        data: "{}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data.response == 'ok') {
                var categories = json_data.data;

                var categories_html = '';
                for (var i = 0; i < categories.length; i++) {
                    categories_html = categories_html + '<option value="' + categories[i].CategoryTypeId + '">' + categories[i].ServiceCategoryName + '</option>';
                }
                $('#ddlCategoryType').html(categories_html);
                if (CategoryTypeId != '') {
                    $('#ddlCategoryType').val(CategoryTypeId);
                }
                $('#btnSaveServiceCategory').show();
            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}



function SaveServiceCategory(ServiceCategoryId, ServiceCategoryName, CategoryTypeId, IsActive) {
    $('#btnSaveServiceCategory').html('<img src="../../assets/uploads/loading-green-back.svg' + '" class="loading-request"/> Saving Service Category');
    var service_details = {
        "ServiceCategoryId": ServiceCategoryId,
        "ServiceCategoryName": ServiceCategoryName,
        "CategoryTypeId": CategoryTypeId,
        "IsActive": IsActive
    };
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/SaveServiceCategory',
        data: JSON.stringify(service_details),
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data.response == 'success') {
                HandlePopUp('0', 'divTrabau_Popup_EditServiceCategory');
                Clear();
                table.ajax.reload();
            }
            else {

            }
            $('#btnSaveServiceCategory').html('Save Service Category');
            toastr[json_data.response](json_data.message);
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
    return false;
}
