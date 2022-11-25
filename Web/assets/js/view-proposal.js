$(document).ready(function () {
    $('a[id*="aAddToPreferList"]').click(function () {
        var userid = $(this).attr('data');
        var Type = 'A';
        if ($(this).attr('class').indexOf('disabled') > -1) {
            Type = 'D';
        }
        AddToPreferList(userid, Type);
    });
});