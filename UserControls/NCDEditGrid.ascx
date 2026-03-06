<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDEditGrid.ascx.cs" Inherits="AISReports.UserControls.NCDEditGrid" %>
<style type="text/css">
    .edit-field span {display:inline-block; width:300px;height:21px;float: left; vertical-align: top;}
    .question-label {display:block !important;width:100% !important;}
</style>
<asp:Literal runat="server" ID="litScript" />
<script type="text/javascript">
    var editMode;
    var identityField = "";
    var sTable;

    function setAutoComplete(obj, responseName) {
        $(obj).autocomplete({
            source: getResponseList(responseName),
            minLength: 0,
            select: function () {
                $(this).autocomplete("close");
            }
        }).click(function () {
            $(this).autocomplete("search");
        });
    }

    function getSearchGridFilterResults(select) {
        $(".slide-out-div input").each(function () {
            if ($(this).attr("data-table-type").toLowerCase() == "databaseeditobjects") {
                $(this).val($(select).val());
                getResultByFilter();
            }
        });

    }

    function CheckForIdentityField() {
        $(".response-field").each(function () {

            if ($(this).attr("aria-identity-column") == "true") {
                identityField = $(this).attr("data-field-name");
            }
        });
    }

    function GetFieldList(dMode) {
        var fieldlist = "";
        var fieldindex = 0;

        $(".response-field").each(function () {

            if ($(this).attr("aria-identity-column") == "true") {
                identityField = $(this).attr("data-field-name");
            }

            if ($(this).attr("aria-identity-column") == "false" && $(this).attr("disabled") !=="undefined")  {
                var responseListName = $(this).attr("response-list-type");

                if (responseListName != 'undefined') {
                    setAutoComplete($(this), responseListName);
                }

                if ($(this).attr("aria-data-type").indexOf('date') != -1) {
                    $(this).datepicker({
                        changeMonth: true,
                        changeYear: true
                    });
                }

                if (($(this).attr("aria-data-type") == "numeric" ||
                     $(this).attr("aria-data-type") == "decimal" ||
                     $(this).attr("aria-data-type") == "int") && $(this).val() == "") {

                    if ($(this).attr("id").toLowerCase().indexOf("companyid") != -1) {
                        if (cid == "10025") {
                            $(this).val("1");
                        }
                        else if(cid=="9999") {
                            //don't set the company id.
                        }
                        else {
                            $(this).val(cid);
                        }
                    }
                    else {
                        $(this).val("0");
                    }

                    $(this).ForceNumericOnly();

                }

                if ($(this).attr("aria-computed-column") == "true") {
                    $(this).attr("disabled", true)
                }

                if ($(this).attr("data-field-name") != "undefined") {

                    var fieldName = $(this).attr("data-field-name");
                    var tableType = $(this).attr("data-table-type");

                    if (typeof $(this).attr("picklist-field-name") != "undefined") {
                        fieldName = $(this).attr("picklist-field-name");
                        tableType =  $(this).attr("picklist-table-type");
                        //fieldName = fieldName + "|" + $(this).attr("picklist-field-name");
                        //tableType = tableType + "|" + $(this).attr("picklist-table-type");
                    }

                    $(this).autocomplete({
                        source: "../ListHandler.ashx?cid=" + cid + "&uid=" + uid + "&fn=" + fieldName + "&tn=" + tableType + "&qt=lookup&qid=" + S4(),
                        minLength: 1
                    });

                   // alert("../ListHandler.ashx?cid=" + cid + "&uid=" + uid + "&fn=" + fieldName + "&tn=" + tableType + "&qt=lookup&qid=" + S4());
                }

                if (typeof $(this).attr("aria-default-column") != 'undefined') {
                    if ($(this).val() == "") {
                        var newtimestamp = GetTimeStamp();// $(this).attr("aria-default-column-value");
                        //alert(newtimestamp);
                        $(this).val(newtimestamp);
                    }
                }
                //alert($(this).attr("id"));
                var fn = $(this).attr("id").replace("ctl00_ContentPlaceHolder1_ucNCDEditGrid_", "").replace("ContentPlaceHolder1_ucNCDEditGrid_", "").replace("ContentPlaceHolder1_ctl00_", "").replace("ContentPlaceHolder1_ctl00_", "").replace("ctl00_","").replace("txt", "");

                if ($(this).attr("type") == "checkbox") {
                    $(this).val($(this).prop("checked"));
                }

                if ($(this).attr("aria-identity-column") != "true") {
                    if ($(this).attr("aria-computed-column") == "true" && (dMode == "save" || dMode == "add")) {
                        //Don't do anything.  we cannot save data to a computed column.
                    }
                    else if (fieldindex == 0) {
                        fieldlist = fieldlist + fn + "~" + $(this).val();
                    }
                    else {
                        fieldlist = fieldlist + "|" + fn + "~" + $(this).val();
                    }
                }
                ++fieldindex;
            }
        });

        console.log("Field List: "+fieldlist);
        return fieldlist;
    }

    function RunQuery(obj) {

        editMode = $(obj).text().toLowerCase();

        var tableName = $(".edit-grid-dropdown-filter option:selected").val();
        var primaryKeyValue = $(obj).attr("rel");
        var primaryKeyName = $(".grid-section").attr("primary-key-field");
        
        if (tableName != "") {
            var NewJSONDBQuery = {};
            var modeDisplay = "";

            NewJSONDBQuery.FieldList = GetFieldList(editMode);
            NewJSONDBQuery.TableName = tableName;
            NewJSONDBQuery.KeyFieldName = primaryKeyName;
            NewJSONDBQuery.KeyFieldValue = primaryKeyValue;
            NewJSONDBQuery.QueryType = editMode.split(" ")[0];
            NewJSONDBQuery.UserID = uid;

            CheckForIdentityField();
            //alert(identityField);
            if(identityField.toLowerCase() == primaryKeyName.toLowerCase()) {
                NewJSONDBQuery.KeyFieldIsIdentity = "true";
            }

            $("#lnkOK").text("OK");

            if (editMode == "add record") {
                modeDisplay = "added";
            }
            else if (editMode == "save") {
                modeDisplay = "updated";
            }
            else if (editMode == "delete") {
                modeDisplay = "deleted";
                var completeDelete = confirm('Are you sure you want to delete?');

                if (!completeDelete) {
                    return true;
                }
            }
            else if (editMode == "edit") {
                modeDisplay = "get";
                $("#lnkOK").text("Save");
                $("#lnkOK").attr("rel", primaryKeyValue);
                ClearFields(); 
            }

            var DTO = { 'NewJSONDBQuery': NewJSONDBQuery };
            console.log(JSON.stringify(DTO));
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../AISWS.asmx/JSONDBQuery",
                data: JSON.stringify(DTO),
                dataType: "json",
                beforeSend: function () { },
                success: function (data) {

                    if (modeDisplay == "get") {
                        var record = data.d.FieldList.split("|");
                        //alert(data.d.FieldList);
                        $.each(record, function (index, item) {
                            try {
                                var fieldName = item.split("~")[0];
                                var fieldValue = item.split("~")[1].replace("1/1/1900 12:00:00 AM", "").replace("12:00:00 AM", "");

                                //if (fieldName == "WeightUpdated") {
                                //    $("#ContentPlaceHolder1_ctl00_txt" + fieldName).hide();

                                //    //weightUpdated.after("<input type='checkbox' class='response-field' id=" + fieldName + "/>");
                                //}
                                //else
                                if ($("#ctl00_ContentPlaceHolder1_ctl00_txt" + fieldName).attr("type") == "checkbox") {
                                    $("#ctl00_ContentPlaceHolder1_ctl00_txt" + fieldName).prop("checked", (fieldValue.toLowerCase() == "true"));
                                }
                                else if ($("#ctl00_ContentPlaceHolder1_ucNCDEditGrid_txt" + fieldName).attr("type") == "checkbox") {
                                    $("#ctl00_ContentPlaceHolder1_ucNCDEditGrid_txt" + fieldName).prop("checked", (fieldValue.toLowerCase() == "true"));
                                }
                                else if ($("#ctl00_ContentPlaceHolder1_ucNCDEditGrid_txt" + fieldName).length > 0) {
                                    $("#ctl00_ContentPlaceHolder1_ucNCDEditGrid_txt" + fieldName).val(fieldValue);
                                }
                                else if($("#ContentPlaceHolder1_ctl00_txt" + fieldName).length>0) {
                                    $("#ContentPlaceHolder1_ctl00_txt" + fieldName).val(fieldValue);
                                }
                                else if ($("#ctl00_ContentPlaceHolder1_ctl00_txt" + fieldName).length > 0) {
                                    $("#ctl00_ContentPlaceHolder1_ctl00_txt" + fieldName).val(fieldValue);
                                }
                                else {
                                    $("#ContentPlaceHolder1_ctl00_txt" + fieldName).val(fieldValue);
                                }

                            }
                            catch (e) {
                                alert("get data: "+e.Message);
                            }
                        });


                        if ($("#ctl00_ContentPlaceHolder1_ucNCDEditGrid_txt" + NewJSONDBQuery.KeyFieldName).length > 0) {
                            $("#ctl00_ContentPlaceHolder1_ucNCDEditGrid_txt" + NewJSONDBQuery.KeyFieldName).val(NewJSONDBQuery.KeyFieldValue);
                        }
                        else if ($("#ContentPlaceHolder1_ucNCDEditGrid_txt" + NewJSONDBQuery.KeyFieldName).length>0) {
                            $("#ContentPlaceHolder1_ucNCDEditGrid_txt" + NewJSONDBQuery.KeyFieldName).val(NewJSONDBQuery.KeyFieldValue);
                        }
                        else if ($("#ContentPlaceHolder1_ctl00_txt" + NewJSONDBQuery.KeyFieldName).length>0) {
                            $("#ContentPlaceHolder1_ctl00_txt" + NewJSONDBQuery.KeyFieldName).val(NewJSONDBQuery.KeyFieldValue);
                        }
                        else if ($("#ctl00_ContentPlaceHolder1_ctl00_txt" + NewJSONDBQuery.KeyFieldName).length > 0) {
                            $("#ctl00_ContentPlaceHolder1_ctl00_txt" + NewJSONDBQuery.KeyFieldName).val(NewJSONDBQuery.KeyFieldValue);
                        }
                        else {
                            $("#ContentPlaceHolder1_ctl00_txt" + NewJSONDBQuery.KeyFieldName).val(NewJSONDBQuery.KeyFieldValue);
                        }
                    }
                    
                    if (modeDisplay != "get") {

                        try {
                            $("#edit-dialog").dialog("close");
                            //oEditGrid.fnReloadAjax(null, null, true);
                            //oEditGrid.DataTable().ajax.reload();
                            oEditGrid.ajax.reload(null,false);


                            if (modeDisplay != "deleted" && modeDisplay != "" && modeDisplay != null) {
                                alert("Data was successfully " + modeDisplay);
                            }
                        }
                        catch (e) { alert(e.message);}
                    }
                },
                error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
            });
        }
    }

    function ShowEditDialog(obj) {
        if ($(obj).text().toLowerCase().indexOf("add record") !=-1) {
            ClearFields();
            GetFieldList("");
        }

        $("#edit-dialog").dialog({
            width: "auto",
            height: "auto",
            modal: true,
            zIndex: 9000
            //open: function () {
            //    $('#edit-dialog').parent('.ui-dialog').css('zIndex', 99999)
            //        .nextAll('.ui-widget-overlay').css('zIndex', 88888);
            //}
        });
    }

    function ClearFields() {
        $(".edit-field >input").each(function () {

            if (!$(this).is(":disabled")) {
                $(this).val("");
            }
        });

        $(".edit-field >textarea").each(function () {
            $(this).val("");
        });

        if ($("#ctl00_ContentPlaceHolder1_ctl00_ddlTables").val() === "vw_NW_Consolidated_SurveyResults" && cid === "10025" && uid === "19") {
            $(".edit-field > span").each(function () {
                $(this).width("420px");
            });
        }
    }

    jQuery.fn.ForceNumericOnly =
    function () {
        return this.each(function () {
            $(this).keydown(function (e) {
                var key = e.charCode || e.keyCode || 0;
                // allow backspace, tab, delete, enter, arrows, numbers and keypad numbers ONLY
                // home, end, period, and numpad decimal

                if ((e.keyCode >= 48 && e.keyCode <= 57) && e.shiftKey == true)
                    return false;

                return (
                    key == 8 ||
                    key == 9 ||
                    key == 13 ||
                    key == 46 ||
                    key == 110 ||
                    key == 190 ||
                    (key >= 35 && key <= 40) ||
                    (key >= 48 && key <= 57) ||
                    (key >= 96 && key <= 105));
            });
        });
    };

    function GetTimeStamp() {
        // Create a date object with the current time
        var now = new Date();

        // Create an array with the current month, day and time
        var date = [now.getMonth() + 1, now.getDate(), now.getFullYear()];

        // Create an array with the current hour, minute and second
        var time = [now.getHours(), now.getMinutes(), now.getSeconds()];

        // Determine AM or PM suffix based on the hour
        var suffix = (time[0] < 12) ? "AM" : "PM";

        // Convert hour from military time
        time[0] = (time[0] < 12) ? time[0] : time[0] - 12;

        // If hour is 0, set it to 12
        time[0] = time[0] || 12;

        // If seconds and minutes are less than 10, add a zero
        for (var i = 1; i < 3; i++) {
            if (time[i] < 10) {
                time[i] = "0" + time[i];
            }
        }

        // Return the formatted string
        return date.join("/") + " " + time.join(":") + " " + suffix;
    }

    function GetQueryStringValue(key) {
        key = key.replace(/[*+?^$.\[\]{}()|\\\/]/g, "\\$&"); // escape RegEx meta chars
        var match = location.search.match(new RegExp("[?&]"+key+"=([^&]+)(&|$)"));
        return match && decodeURIComponent(match[1].replace(/\+/g, " "));
}
</script>
<style type="text/css">
    .edit-grid-filter 
    {
        display:inline-block;
        position:relative;
        top:15px;
        z-index:9000;
        height:0px;
        width:100%;
        text-align:center;
    }
</style>
<div class="user-control-widget">
    <div class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix" style="padding: 5px;">
        <asp:Label runat="server" ID="lblChartTitle" Text="" EnableViewState="false" CssClass="chart-title" />
    </div>
    <asp:Panel runat="server" ID="pChart" class="grid-section">
        <div id="edit-grid-filter-section" class="chart-title">
            <span>Tables:</span>
            <asp:DropDownList runat="server" ID="ddlTables" CssClass="edit-grid-dropdown-filter" />
        </div>
        <table id="edit-grid" class="grid-control">
            <thead>
                <tr>
                    <th></th>
                    <asp:Literal runat="server" ID="litHeaderRow" EnableViewState="false"></asp:Literal>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <asp:Literal runat="server" ID="litNoData" EnableViewState="false" ></asp:Literal>
                </tr>
            </tbody>
        </table>
        <br />
        <asp:HyperLink runat="server" ID="hypAddRecord" CssClass="add-record" Text="Add Record" NavigateUrl="#" onclick='$("#lnkOK").text("Add");ShowEditDialog(this); return false;' style="display:none;" />
        <br />
        <span class="edit-grid-message" style="display:block; color:#fff;"></span>
    </asp:Panel>
    <div id="edit-dialog" title="Edit Data" style="display:none;">
        <div style="padding:10px;">
             <asp:PlaceHolder runat="server" ID="phEditFields" EnableViewState="false">
             </asp:PlaceHolder>
             <div style="width:100%;text-align:right;padding:5px;">
                <a id="lnkOK" href="#" onclick="javascript:RunQuery(this);">Ok</a>
             </div>
            </div>
        </div>
    <span id="edit-mode"></span>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        $(".edit-grid-dropdown-filter").change(function () {
            var pageUrl = window.location.href.split("?")[0];
            sTable = $(this).find('option:selected').text();
            window.location.href = pageUrl + "?cid=" + cid + "&t=" + sTable;
        });

        if ($("#ctl00_ContentPlaceHolder1_ctl00_ddlTables").val() === "vw_NW_Consolidated_SurveyResults" && cid === "10025" && uid === "19") {

        }
 
    });

    $("#lnkOK").button();
    $(".add-record").button();

    if (cid == "10003" && $("#edit-grid").parent().attr("data-store-type") == "Drivers" &&
        $("#ctl00_lblCompanyName").tolowercase().indexOf("saabye") == -1) {
        $("#edit-grid-dropdown-filter option[value='Drivers']").remove();
    }
</script>