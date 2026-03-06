function MultiControlPickList(obj, val) {
    var claimsGrid = $("div[grid-type='claims']");
    var plMap = $(claimsGrid).attr("multipicklistmap");
    var tableName = plMap.split("~")[0];
    var sourceKeyFieldName = plMap.split("|")[0].split("~")[1];
    var destKeyFieldName = plMap.split("|")[0].split("~")[2];
    var sourceFieldList = "";
    var destFieldList = "";

    if (destKeyFieldName == "") {
        destKeyFieldName = sourceKeyFieldName;
    }

    $.each(plMap.split("|"), function (idx, val) {

        try
        {
            var sFieldName = val.split("~")[1];
            var dFieldName = val.split("~")[2];


            if (idx > 0) {
                if (sourceFieldList.length == 0) {
                    sourceFieldList = sFieldName;
                    destFieldList = (dFieldName == "" ? sFieldName : dFieldName);
                }
                else {
                    sourceFieldList += "|" + sFieldName;
                    destFieldList += "|" + (dFieldName == "" ? sFieldName : dFieldName);
                }
            }
        }
        catch (e) {
            alert(e.Message);
        }

    });

    var NewJSONDBQuery = {};

    NewJSONDBQuery.FieldList = sourceFieldList;
    NewJSONDBQuery.TableName = tableName;
    NewJSONDBQuery.KeyFieldName = sourceKeyFieldName;
    NewJSONDBQuery.KeyFieldValue = val;
    NewJSONDBQuery.QueryType = "get";

    var DTO = { 'NewJSONDBQuery': NewJSONDBQuery };

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../AISWS.asmx/JSONDBQuery",
        data: JSON.stringify(DTO),
        dataType: "json",
        success: function (data) {
            $.each(data.d.FieldList.split("|"), function (idx, val) {
                var fName = val.split("~")[0];
                var fValue = val.split("~")[1];

                if (fName == "DriverName") {
                    fName = "PVODriver";
                }
                else if (fName == "TransfereeName")
                {
                    var n=fValue.indexOf(" ");
                    var firstN = fValue.substring(n+1, fValue.length);
                    var lastN = fValue.substring(0, n);

                    $("#edit-dialog > div > .edit-field >input[data-field-name='FirstName'] ").val(firstN);
                    $("#edit-dialog > div > .edit-field >input[data-field-name='LastName'] ").val(lastN);
                }

                $("#edit-dialog > div > .edit-field >input[data-field-name='" + fName + "'] ").val(fValue);
            });
        },
        error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
    });
}
