var table;
$(document).ready(function () {
    GetGenericCatgeoryDataForDataTable();


    $('#btnSaveCategoryDetails').click(function () {
        var CategoryId = $('#hfCategoryId').val();
        var CategoryName = $('#txtCategoryName').val();
        var CategoryType = $('#ddlCategoryType').val();
        var IsActive = $('#chkCategoryStatus').prop('checked');
        if (CategoryName != '') {
            if (CategoryType != '') {
                SaveCategoryDetails(CategoryId, CategoryName, CategoryType, IsActive);
            }
            else {
                toastr["error"]("Select Category Type");
            }
        }
        else {
            toastr["error"]("Enter Category Name");
        }
        return false;
    });
});


function EditCategory(id) {
    $('#btnSaveCategoryDetails').hide();
    HandlePopUp('1', 'divTrabau_Popup_EditCatgeory');
    var data = $(id).attr('id');
    GetCategoryDetails(data);
}

function OpenAddCatgeoryPopup() {
    HandlePopUp('1', 'divTrabau_Popup_EditCatgeory');
    Clear();
    GetCategoryTypes('');
}

function Clear() {
    $('#hfCategoryId').val('');
    $('#txtCategoryName').val('');
    $('#ddlCategoryType').html('');
    $('#btnSaveCategoryDetails').hide();
    $('#chkCategoryStatus').prop('checked', true);
}

function GetGenericCatgeoryDataForDataTable() {
    table = $('#tblDataTable').DataTable({
        "processing": true,
        "serverSide": true,
        "iDisplayLength": 25,
        "ajax": {
            url: services_pathconfig + "/GetGenericCatgeoryDataForDataTable", type: "post",
            ajaxGridOptions: { contentType: 'application/json; charset=utf-8' },
            datatype: "json"
        },
        columnDefs: [
            { orderable: false, targets: -1 }
        ],
        "columns": [
            { "data": "CategoryName" },
            { "data": "CategoryType" },
            { "data": "IsActive" },
            {
                "data": "CategoryId_Enc", render: function (data, type, row, meta) {
                    return type === 'display' ? '<a id="' + data + '" onclick="EditCategory(this)"><i class="fa fa-edit"></i> Edit</a>' : data;
                }
            }
        ],
        destroy: true
    });
}


function GetCategoryDetails(_data) {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetCategoryDetails',
        data: "{data:'" + _data + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data.response == 'ok') {
                var category = json_data.data;
                $('#txtCategoryName').val(category[0].CategoryName);
                $('#chkCategoryStatus').prop('checked', category[0].IsActive);
                $('#hfCategoryId').val(_data);
                GetCategoryTypes(category[0].CategoryType);

            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}



function GetCategoryTypes(CategoryType) {
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
                var cat_types = json_data.data;

                var cat_types_html = '';
                for (var i = 0; i < cat_types.length; i++) {
                    cat_types_html = cat_types_html + '<option value="' + cat_types[i].CategoryId + '">' + cat_types[i].CategoryType + '</option>';
                }
                $('#ddlCategoryType').html(cat_types_html);
                if (CategoryType != '') {
                    $('#ddlCategoryType').val(CategoryType);
                }
                $('#btnSaveCategoryDetails').show();
            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}




function SaveCategoryDetails(CategoryId, CategoryName, CategoryType, IsActive) {
    $('#btnSaveCategoryDetails').html('<img src="../../assets/uploads/loading-green-back.svg' + '" class="loading-request"/> Saving Category Details');
    var cat_details = {
        "CategoryId": CategoryId,
        "CategoryName": CategoryName,
        "CategoryType": CategoryType,
        "IsActive": IsActive
    };
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/SaveCategoryDetails',
        data: JSON.stringify(cat_details),
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data.response == 'success') {
                HandlePopUp('0', 'divTrabau_Popup_EditCatgeory');
                Clear();
                table.ajax.reload();
            }
            else {

            }
            $('#btnSaveCategoryDetails').html('Save Category Details');
            toastr[json_data.response](json_data.message);
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
    return false;
}
