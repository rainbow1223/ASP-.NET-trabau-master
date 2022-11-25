var table;
$(document).ready(function () {
    GetDocsDataForDataTable();


    $('#btnSaveDocumentCategory').click(function () {
        var DocId = $('#hfDocumentId').val();
        var DocName = $('#txtDocumentCategoryName').val();
        var IsActive = $('#chkDocumentStatus').prop('checked');
        if (DocName != '') {
            SaveDocumentDetails(DocId, DocName, IsActive);
        }
        else {
            toastr["error"]("Enter Document Category Name");
        }
        return false;
    });
});

function EditDocumentCategory(id) {
    $('#btnSaveDocumentCategory').hide();
    HandlePopUp('1', 'divTrabau_Popup_EditDocument');
    var data = $(id).attr('id');
    GetDocumentCategoryDetails(data);
}

function Clear() {
    $('#hfDocumentId').val('');
    $('#txtDocumentCategoryName').val('');
    $('#btnSaveDocumentCategory').hide();
    $('#chkDocumentStatus').prop('checked', true);
}

function OpenAddDocumentPopup() {
    HandlePopUp('1', 'divTrabau_Popup_EditDocument');
    Clear();
    $('#btnSaveDocumentCategory').show();
}


function GetDocsDataForDataTable() {
    table = $('#tblDataTable').DataTable({
        "processing": true,
        "serverSide": true,
        "iDisplayLength": 25,
        "ajax": {
            url: services_pathconfig + "/GetDocumentDataForDataTable", type: "post",
            ajaxGridOptions: { contentType: 'application/json; charset=utf-8' },
            datatype: "json"
        },
        columnDefs: [
            { orderable: false, targets: -1 }
        ],
        "columns": [
            { "data": "DocumentName" },
            { "data": "IsActive" },
            {
                "data": "DocumentId_Enc", render: function (data, type, row, meta) {
                    return type === 'display' ? '<a id="' + data + '" onclick="EditDocumentCategory(this)"><i class="fa fa-edit"></i> Edit</a>' : data;
                }
            }
        ],
        destroy: true
    });
}

function GetDocumentCategoryDetails(_data) {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetDocumentCategoryDetails',
        data: "{data:'" + _data + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data.response == 'ok') {
                var doc = json_data.data;
                $('#txtDocumentCategoryName').val(doc[0].DocumentName);
                $('#chkDocumentStatus').prop('checked', doc[0].IsActive);
                $('#hfDocumentId').val(_data);

                $('#btnSaveDocumentCategory').show();
            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}


function SaveDocumentDetails(DocId, DocName, IsActive) {
    $('#btnSaveDocumentCategory').html('<img src="../../assets/uploads/loading-green-back.svg' + '" class="loading-request"/> Saving Document');
    var doc_details = {
        "DocId": DocId,
        "DocName": DocName,
        "IsActive": IsActive
    };
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/SaveDocumentDetails',
        data: JSON.stringify(doc_details),
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);
            if (json_data.response == 'success') {
                HandlePopUp('0', 'divTrabau_Popup_EditDocument');
                Clear();
                table.ajax.reload();
            }
            $('#btnSaveDocumentCategory').html('Save Role');
            toastr[json_data.response](json_data.message);
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
    return false;
}
