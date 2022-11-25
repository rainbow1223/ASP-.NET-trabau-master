Sys.Application.add_load(LoadDocsEvents);
function GetFileInfo() {
    var filename = $('#fu_profile_document').val().split('\\').pop();
    $('#sfileinfo').text(filename);
}