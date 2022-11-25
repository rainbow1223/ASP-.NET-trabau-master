var table;
$(document).ready(function () {
    GetRoleDataForDataTable();


    $('#btnSaveRole').click(function () {
        var RoleId = $('#hfRoleId').val();
        var RoleName = $('#txtRoleName').val();
        var IsActive = $('#chkRoleStatus').prop('checked');
        if (RoleName != '') {
            SaveRoleDetails(RoleId, RoleName, IsActive);
        }
        else {
            toastr["error"]("Enter Role Name");
        }
        return false;
    });
});

function OpenAddRolePopup() {
    HandlePopUp('1', 'divTrabau_Popup_EditRole');
    Clear();
    $('#btnSaveRole').show();
}

function EditRole(id) {
    $('#btnSaveRole').hide();
    HandlePopUp('1', 'divTrabau_Popup_EditRole');
    var data = $(id).attr('id');
    GetRoleDetails(data);
}

function Clear() {
    $('#hfRoleId').val('');
    $('#txtRoleName').val('');
    $('#btnSaveRole').hide();
    $('#chkRoleStatus').prop('checked', true);
}


function GetRoleDataForDataTable() {
    table = $('#tblDataTable').DataTable({
        "processing": true,
        "serverSide": true,
        "iDisplayLength": 25,
        "ajax": {
            url: services_pathconfig + "/GetRoleDataForDataTable", type: "post",
            ajaxGridOptions: { contentType: 'application/json; charset=utf-8' },
            datatype: "json"
        },
        columnDefs: [
            { orderable: false, targets: -1 }
        ],
        "columns": [
            { "data": "RoleName" },
            { "data": "IsActive" },
            {
                "data": "RoleId_Enc", render: function (data, type, row, meta) {
                    return type === 'display' ? '<a id="' + data + '" onclick="EditRole(this)"><i class="fa fa-edit"></i> Edit</a>' : data;
                }
            }
        ],
        destroy: true
    });
}


function GetRoleDetails(_data) {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetRoleDetails',
        data: "{data:'" + _data + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data.response == 'ok') {
                var role = json_data.data;
                $('#txtRoleName').val(role[0].RoleName);
                $('#chkRoleStatus').prop('checked', role[0].IsActive);
                $('#hfRoleId').val(_data);

                $('#btnSaveRole').show();
            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}



function SaveRoleDetails(RoleId, RoleName, IsActive) {
    $('#btnSaveRole').html('<img src="../../assets/uploads/loading-green-back.svg' + '" class="loading-request"/> Saving Role');
    var role_details = {
        "RoleId": RoleId,
        "RoleName": RoleName,
        "IsActive": IsActive
    };
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/SaveRoleDetails',
        data: JSON.stringify(role_details),
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data.response == 'success') {
                HandlePopUp('0', 'divTrabau_Popup_EditRole');
                Clear();
                table.ajax.reload();
            }
            else {

            }
            $('#btnSaveRole').html('Save Role');
            toastr[json_data.response](json_data.message);
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
    return false;
}
