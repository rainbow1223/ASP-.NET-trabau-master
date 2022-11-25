function Paging(PageNumber, PageSize, TotalRecords, ClassName, DisableClassName) {
    var ReturnValue = "";

    var TotalPages = Math.ceil(TotalRecords / PageSize);
    if (+PageNumber > 1) {
        if (+PageNumber == 2)
            ReturnValue = ReturnValue + "<a pn='" + (+PageNumber - 1) + "' class='" + ClassName + "'>Previous</a>&nbsp;&nbsp;&nbsp;";
        else {
            ReturnValue = ReturnValue + "<a pn='";
            ReturnValue = ReturnValue + (+PageNumber - 1) + "' class='" + ClassName + "'>Previous</a>&nbsp;&nbsp;&nbsp;";
        }
    }
    else
        ReturnValue = ReturnValue + "<span class='" + DisableClassName + "'>Previous</span>&nbsp;&nbsp;&nbsp;";
    if ((+PageNumber - 3) > 1)
        ReturnValue = ReturnValue + "<a pn='1' class='" + ClassName + "'>1</a>&nbsp;.....&nbsp;|&nbsp;";
    for (var i = +PageNumber - 3; i <= +PageNumber; i++)
        if (i >= 1) {
            if (+PageNumber != i) {
                ReturnValue = ReturnValue + "<a pn='";
                ReturnValue = ReturnValue + i + "' class='" + ClassName + "'>" + i + "</a>&nbsp;|&nbsp;";
            }
            else {
                ReturnValue = ReturnValue + "<span style='font-weight:bold;'>" + i + "</span>&nbsp;|&nbsp;";
            }
        }
    for (var i = +PageNumber + 1; i <= +PageNumber + 3; i++)
        if (i <= TotalPages) {
            if (+PageNumber != i) {
                ReturnValue = ReturnValue + "<a pn='";
                ReturnValue = ReturnValue + i + "' class='" + ClassName + "'>" + i + "</a>&nbsp;|&nbsp;";
            }
            else {
                ReturnValue = ReturnValue + "<span style='font-weight:bold;'>" + i + "</span>&nbsp;|&nbsp;";
            }
        }
    if ((+PageNumber + 3) < TotalPages) {
        ReturnValue = ReturnValue + ".....&nbsp;<a pn='";
        ReturnValue = ReturnValue + TotalPages + "' class='" + ClassName + "'>" + TotalPages + "</a>";
    }
    if (+PageNumber < TotalPages) {
        ReturnValue = ReturnValue + "&nbsp;&nbsp;&nbsp;<a pn='";
        ReturnValue = ReturnValue + (+PageNumber + 1) + "' class='" + ClassName + "'>Next</a>";
    }
    else
        ReturnValue = ReturnValue + "&nbsp;&nbsp;&nbsp;<span class='" + DisableClassName + "'>Next</span>";

    var num = (PageNumber * PageSize);
    var records = '<span>Showing ' + ((num - PageSize) + 1).toString() + ' to ' + (parseInt(num) > parseInt(TotalRecords) ? TotalRecords : num).toString() + ' of ' + TotalRecords.toString() + ' entries</span>';
    return (records + ReturnValue);
}