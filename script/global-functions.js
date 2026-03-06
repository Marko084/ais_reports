function GetAdminTableColumnNames(table) {
    var headerNames = [];
    var idx = 0;
    $(table).find("thead >tr >th").each(function () {
        headerNames.push({ "name": $(this).text(), "idx": idx });
        idx++;
    });

    return headerNames;
}