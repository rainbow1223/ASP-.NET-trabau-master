var table;
$(document).ready(function () {
    GetSkillsDataForDataTable();


    $('#btnSaveSkillDetails').click(function () {
        var SkillId = $('#hfSkillId').val();
        var SkillName = $('#txtSkillName').val();
        var SkillType = $('#ddlSkillType').val();
        var IsActive = $('#chkSkillStatus').prop('checked');
        if (SkillName != '') {
            if (SkillType != '') {
                SaveSkillDetails(SkillId, SkillName, SkillType, IsActive);
            }
            else {
                toastr["error"]("Select Skill Type");
            }
        }
        else {
            toastr["error"]("Enter Skill Name");
        }
        return false;
    });
});

function EditSkill(id) {
    $('#btnSaveSkillDetails').hide();
    HandlePopUp('1', 'divTrabau_Popup_EditSkill');
    var data = $(id).attr('id');
    GetSkillDetails(data);
}

function Clear() {
    $('#hfSkillId').val('');
    $('#txtSkillName').val('');
    $('#ddlSkillType').val('None');
    $('#btnSaveSkillDetails').hide();
    $('#chkSkillStatus').prop('checked', true);
}

function OpenAddSkillPopup() {
    HandlePopUp('1', 'divTrabau_Popup_EditSkill');
    Clear();
    $('#btnSaveSkillDetails').show();
}


function GetSkillsDataForDataTable() {
    table = $('#tblDataTable').DataTable({
        "processing": true,
        "serverSide": true,
        "iDisplayLength": 25,
        "ajax": {
            url: services_pathconfig + "/GetSkillsDataForDataTable", type: "post",
            ajaxGridOptions: { contentType: 'application/json; charset=utf-8' },
            datatype: "json"
        },
        columnDefs: [
            { orderable: false, targets: -1 }
        ],
        "columns": [
            { "data": "SkillName" },
            { "data": "SkillType" },
            { "data": "IsActive" },
            {
                "data": "SkillId_Enc", render: function (data, type, row, meta) {
                    return type === 'display' ? '<a id="' + data + '" onclick="EditSkill(this)"><i class="fa fa-edit"></i> Edit</a>' : data;
                }
            }
        ],
        destroy: true
    });
}

function GetSkillDetails(_data) {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetSkillDetails',
        data: "{data:'" + _data + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data.response == 'ok') {
                var skill = json_data.data;
                $('#txtSkillName').val(skill[0].SkillName);
                $('#chkSkillStatus').prop('checked', skill[0].IsActive);
                $('#ddlSkillType').val(skill[0].SkillType);
                $('#hfSkillId').val(_data);

                $('#btnSaveSkillDetails').show();
            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}



function SaveSkillDetails(SkillId, SkillName, SkillType, IsActive) {
    $('#btnSaveSkillDetails').html('<img src="../../assets/uploads/loading-green-back.svg' + '" class="loading-request"/> Saving Skill Details');
    var skill_details = {
        "SkillId": SkillId,
        "SkillName": SkillName,
        "SkillType": SkillType,
        "IsActive": IsActive
    };
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/SaveSkillDetails',
        data: JSON.stringify(skill_details),
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data.response == 'success') {
                HandlePopUp('0', 'divTrabau_Popup_EditSkill');
                Clear();
                table.ajax.reload();
            }
            else {

            }
            $('#btnSaveSkillDetails').html('Save Skill Details');
            toastr[json_data.response](json_data.message);
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
    return false;
}