<%@ Page Title="" Language="C#" MasterPageFile="~/Site/MasterPage.Master" AutoEventWireup="true" CodeBehind="MoveHappyCallTracker.aspx.cs" Inherits="AISReports.Site.MoveHappyCallTracker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .edit-field {display:inline-block;font-size:small; font-family:Calibri, Tahoma, Arial; padding:5px;}
        .edit-field span {display:inline-block; width:160px; vertical-align:top;}
        .edit-field input {width:195px; font-size:small;}
        .survey-question {display:block !important;}
        .survey-question span {white-space:nowrap;display:inline-block; width:700px;}
        .follow-up-needed { background-image: url('../images/workflow-complete.png'); display:inline-block; background-repeat: no-repeat;background-position: center; width:100%; height:15px; color:transparent;}
        .green-btn { background-image: url('../images/green-button.png'); display:inline-block; background-repeat: no-repeat;background-position: center; width:100%; height:15px; color:transparent;}
        .red-btn { background-image: url('../images/red-button.png');display:inline-block; background-repeat: no-repeat;background-position: center; width:100%; height:15px; color:transparent;}
        .orange-btn { background-image: url('../images/orange-button.png');display:inline-block; background-repeat: no-repeat;background-position: center; width:100%; height:15px; color:transparent;}
        .blue-btn { background-image: url('../images/blue-button.png');display:inline-block; background-repeat: no-repeat;background-position: center; width:100%; height:15px; color:transparent;}
        .none-status {color:transparent;}
        .current-filter-section {display:inline-block;}
        .cos-type-filter-section {display:inline-block;}
        .status-legend {float:right; height:25px; padding-right:15px;}
        .status-legend span {cursor:pointer;}
        .response-score-value {display:inline-block; width:50px !important; padding-left:10px; font-size:12pt;}
        .update-record-section {padding:3px; text-align:right;}
    </style>
    <script type="text/javascript" src="../script/global-functions.js"></script>
    <script type="text/javascript" src="../script/ncd-functions.js"></script>
    <script type="text/javascript">
        var oEditGrid;
        var sTable;
        var primaryFieldKeyName;
        var identityField;
        var editCompanyID;
        var updateStatus = false;
        var thresholdScore = 0;

        $(document).ready(function () {
            primaryKeyFieldName = $(".edit-grid-dropdown-filter option:selected").attr("aria-primary-key-field");
            sTable = getUrlParameter("t");
            editCompanyID = "10122"; //getUrlParameter("cid");

            LoadData();

            $(".edit-grid-dropdown-filter").change(function () {
                sTable = $(this).find('option:selected').text();
                var ecid = $(this).find('option:selected').attr("aria-company-id");

                if (sTable != "NONE") {
                    var pageUrl = window.location.href.split("?")[0];
                    
                    window.location.href = pageUrl + "?cid="+ecid+"&t=" + sTable;
                }
            });

            //if (sTable != "" && sTable != "NONE") {
            //    primaryKeyFieldName = $(".edit-grid-dropdown-filter option:selected").attr("aria-primary-key-field");

            //    LoadData();

            //}

            var smsBtn = $("<button id='SendSMS'>SendSMS</button>")

            $("#lnkAddRecord").button();
            $("#lnkOK").button();
            $("#ctl00_ContentPlaceHolder1_txtThirdFollowUpCallDate").parent().after(smsBtn);
            $("#ContentPlaceHolder1_txtThirdFollowUpCallDate").parent().after(smsBtn);

            smsBtn.button();

            if (editCompanyID == "10108") {

                var updateRecordBtn = $("<button id='UpdateRecord' rel='recordupdate' onclick='javascript:RunQuery(this);'>Update Record</button>");
                var updateRecordSection = $("<div class='update-record-section'></div>");

                updateRecordSection.append(updateRecordBtn);
                $(smsBtn).parent().parent().after(updateRecordSection);
                updateRecordBtn.button();
            }

        });

        function LoadData() {
            var query = $(".grid-section").attr("grid-query"); //select * from " + sTable
            var dataUri = "../ListHandler.ashx?gt=edit&ct=grid&qt=text&fld=&qn=" + query+ "&pl=&qid=" + S4();
            var table = $("#callcenter-grid");
            var tableColumns = GetAdminTableColumnNames(table);

            //console.log(tableColumns);
            oEditGrid = $(table).DataTable({
                "bDeferRender": true,
                "bDestroy": true,
                ajax: dataUri,
                lengthMenu: [[10, 20, 25, -1], [10, 20, 25, "All"]],
                pagingType: "full_numbers",
                colReorder: true,
                language: {
                    "emptyTable": "No data found to display."
                },
                dom: '<"fg-toolbar ui-toolbar ui-widget-header ui-helper-clearfix ui-corner-tl ui-corner-tr"Blfr><"dataTables_scroll"t><"fg-toolbar ui-toolbar ui-widget-header ui-helper-clearfix ui-corner-bl ui-corner-br"ip>',
                buttons: [{ extend: 'excel', filename: 'UsersGridExport', className: 'ui-corner-br fg-button ui-button ui-state-default' }],
                rowCallback: function (nRow, aData, iDisplayIndex) {
                    var colIndex = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == primaryKeyFieldName.toTitleCase().toLowerCase(); }); //oEditGrid.fnGetColumnIndex(primaryKeyFieldName.toTitleCase());
                    var statusIdx = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == "status"; }); //oEditGrid.fnGetColumnIndex("Status");
                    var phoneIdx = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == ("CellPhoneNumber").toTitleCase().toLowerCase(); }); //oEditGrid.fnGetColumnIndex(("CellPhoneNumber").toTitleCase());
                    var followupNeededIdx = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == ("FollowUpNeeded").toTitleCase().toLowerCase(); });

                    var smsLink = "";//"<a href='javascript:void(0);' rel='DOD' aria-primary-key-name='"+primaryKeyFieldName+"' onclick='SendSurveySms(" + editCompanyID + "," + "\"" + (phoneIdx.length>0 ? aData[phoneIdx[0].idx] : "") + "\"" + "," + "\"" + aData[colIndex[0].idx] + "\"" + ",this); return false;'>SMS</a>";
                    var editLink = "<a href='#' rel='" + aData[colIndex[0].idx] + "' onclick='javascript:RunQuery(this);ShowEditDialog(this); return false;'>Edit</a>";
                    var deleteLink = "<a href='#' rel='" + aData[colIndex[0].idx] + "' onclick='javascript: RunQuery(this); return false;'>Delete</a>";

                    $('td:eq(0)', nRow).html(editLink+"&nbsp;"+deleteLink+"&nbsp;"+smsLink);

                    if ((followUpNeededIdx.length > 0 && aData[followupNeededIdx[0].idx] == "True" && aData[statusIdx[0].idx] != "Completed") ||
                        (statusIdx.length > 0 && aData[statusIdx[0].idx] == "Follow-up Needed")) {
                        $("td:eq(1)", nRow).html("<span class='follow-up-needed' rel='Follow-up Needed'>Follow-up Needed</span>");
                    }
                    else if (statusIdx.length>0 && aData[statusIdx[0].idx] == "Spoke To") {
                        $("td:eq(1)", nRow).html("<span class='green-btn' rel='" + aData[statusIdx[0].idx] + "'>Spoke To</span>");
                    }
                    else if (statusIdx.length > 0 && aData[statusIdx[0].idx] == "Open") {
                        $("td:eq(1)", nRow).html("<span class='blue-btn' rel='" + aData[statusIdx[0].idx] + "'>Open</span>");
                    }
                    else if (statusIdx.length > 0 && aData[statusIdx[0].idx] == "Unable To Reach") {
                        $("td:eq(1)", nRow).html("<span class='red-btn' rel='" + aData[statusIdx[0].idx] + "'>Unable To Reach</span>");
                    }
                    else if (statusIdx.length > 0 && aData[statusIdx[0].idx] == "Disc") {
                        $("td:eq(1)", nRow).html("<span class='orange-btn' rel='" + aData[statusIdx[0].idx] + "'>Disc</span>");
                    }
                    else if (statusIdx.length > 0){
                        $("td:eq(1)", nRow).html("<span class='none-status' rel='" + aData[statusIdx[0].idx] + "'>" + aData[statusIdx[0].idx] + "</span>");
                    }
                }
            });

            var statusIdx = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == "status"; });
            var followUpNeededIdx = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == "follow up needed"; });

            $.fn.dataTable.ext.search.push(
                function (settings, data, dataIndex) {
                    var statusFilter = $(".current-filter").text();
                    var gridStatus = data[statusIdx[0].idx];
                    var followUpNeeded = "False";

                    if (followUpNeededIdx.length>0) {
                        data[followUpNeededIdx[0].idx];
                    }

                    var result = false;

                    //console.log("follow Up: " + followUpNeeded);

                    if (statusFilter == "Follow-up Needed" || followUpNeeded == "True") {
                        return true;
                    }
                    else if ((statusFilter == gridStatus || statusFilter == "All") && statusFilter !="Follow-up Needed") {
                        return true;
                    }
                    else { return false;}
                }
            );

            $(".status-legend span").each(function () {
                //var status = $(this).text().trim();

                $(this).click(function () {
                    $(".current-filter").text($(this).text().replace("View","").trim());
                    oEditGrid.draw();
                });
            });

            //console.log(tableColumns);
            var columnToHideIdx = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == ("CompanyID").toTitleCase().toLowerCase(); }); //oEditGrid.fnGetColumnIndex(("CompanyID").toTitleCase());
 
            if (columnToHideIdx.length > 0) {
                oEditGrid.column(columnToHideIdx[0].idx).visible(false);
            }

            columnToHideIdx = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == ("BatchID").toTitleCase().toLowerCase(); }); //oEditGrid.fnGetColumnIndex(("BatchID").toTitleCase());

            if (columnToHideIdx.length > 0) {
                oEditGrid.column(columnToHideIdx[0].idx).visible(false);
            }

            columnToHideIdx = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == ("ImportID").toTitleCase().toLowerCase(); }); //oEditGrid.fnGetColumnIndex(("ImportID").toTitleCase());

            if (columnToHideIdx.length > 0) {
                oEditGrid.column(columnToHideIdx[0].idx).visible(false);
            }
        }

        function getUrlParameter(name) {
            name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
            var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
            var results = regex.exec(location.search);
            return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
        }

        function SendSurveySms(c, a, rid, ele) {
            var NewNCDSmsSender = {};

            NewNCDSmsSender.CompanyID = c;
            NewNCDSmsSender.BatchID = -1;
            NewNCDSmsSender.PhoneNumber = a;
            NewNCDSmsSender.SurveyID = rid;
            NewNCDSmsSender.QueryType = "sendsms";
            NewNCDSmsSender.SurveyType = $(ele).attr("rel");
            NewNCDSmsSender.PrimaryKeyFieldName = $(ele).attr("aria-primary-key-name");

            var DTO = { 'NewNCDSmsSender': NewNCDSmsSender };

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../AISWS.asmx/NCDSmsSenderQuery",
                data: JSON.stringify(DTO),
                dataType: "json",
                success: function (data) { alert("SMS Message sent successfully."); },
                error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
            });
        }

        function CheckForIdentityField() {
            $(".response-field").each(function () {

                if ($(this).attr("aria-identity-column") == "true") {
                    identityField = $(this).attr("data-field-name");
                }
            });
        }

        function SetDefaultFields() {
            $(".response-field").each(function () {
                if ($(this).attr("data-field-name") == "Status") {
                    var statusDD = $(this);

                    if ($(statusDD).children("option").length == 0) {
                        $(statusDD).append($("<option value=''></option>"));
                        $(statusDD).append($("<option value='Completed'>Completed</option>"));
                        $(statusDD).append($("<option value='Open'>Open</option>"));
                        $(statusDD).append($("<option value='Unable To Reach'>Unable To Reach</option>"));
                        $(statusDD).append($("<option value='Spoke To'>Spoke To</option>"));
                        //$(statusDD).append($("<option value='Follow-up Needed'>Follow-up Needed</option>"));
                        $(statusDD).append($("<option value='Disc'>Disc</option>"));
                    }
                }
                else if ($(this).attr("data-field-name") == "Contacted") {
                    var statusDD = $(this);

                    if ($(statusDD).children("option").length == 0) {
                        $(statusDD).append($("<option value=''></option>"));
                        $(statusDD).append($("<option value='YES'>YES</option>"));
                        $(statusDD).append($("<option value='NO'>NO</option>"));
                    }
                }
            });
        }

        function SetQuestionDropDown(obj) {
            var responseListName = $(obj).attr("response-list-type");
            var dataFieldName = $(obj).attr("data-field-name");
            
            if (dataFieldName != "Comments") {
                $(obj).autocomplete({
                    source: getResponseList(responseListName),
                    minLength: 0,
                    select: function (ev, ui) {
                        $(this).attr("response-value", ui.item.value);
                        $(this).attr("response-text", ui.item.label);
                        $(this).autocomplete("close");
                        SetScoreValue($(this));
                    },
                    change: function (ev, ui) {
                        SetScoreValue($(this));
                    }
                }).click(function () {
                    //alert("here!!");
                    $(this).val("");
                    $(this).autocomplete("search");
                });
           }
        }

        function SetAutoCompleteDropDown(obj) {
            var tableType = $(obj).attr("data-table-type");
            var fieldName = $(obj).attr("data-field-name");
            $(obj).autocomplete({
                source: "../ListHandler.ashx?cid=" + editCompanyID + "&uid=0&fn=" + fieldName + "&tn=" + tableType + "&qt=lookup&qid=" + S4(),
                minLength: 1
            });

            SetInputMask(obj);
        }

        function RunQuery(obj) {

            editMode = $(obj).text().toLowerCase();
            var tableName = $(".edit-grid-dropdown-filter option:selected").val();
            var primaryKeyValue = $(obj).attr("rel");
            updateStatus = false;

            if (editMode == "complete survey") {
                updateStatus = true;
            }
                   
            if (tableName != "") {
                var NewJSONDBQuery = {};
                var modeDisplay = "";

                NewJSONDBQuery.FieldList = GetFieldList(editMode);
                NewJSONDBQuery.TableName = tableName;
                NewJSONDBQuery.KeyFieldName = primaryKeyFieldName;
                NewJSONDBQuery.KeyFieldValue = primaryKeyValue;
                NewJSONDBQuery.QueryType = ((editMode == "save" || editMode == "complete survey" || editMode == "update record") ? "save" : editMode.split(" ")[0]);
                
                CheckForIdentityField();

                if (identityField.toLowerCase() == primaryKeyFieldName.toLowerCase()) {
                    NewJSONDBQuery.KeyFieldIsIdentity = "true";
                }

                $("#lnkOK").html("<span>OK</span>");

                if (editMode == "add record") {
                    modeDisplay = "added";
                }
                else if (editMode == "save" || editMode == "complete survey" || editMode == "update record") {
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

                    if (editCompanyID == "10108"){
                        $("#lnkOK").html("<span>Complete Survey</span>");
                    }
                    else {
                        $("#lnkOK").html("<span>Save</span>");
                    }

                    $("#lnkOK").attr("rel", primaryKeyValue);
                    $("#UpdateBtn").attr("rel", primaryKeyValue);
                    ClearFields();
                    var smsLinkBtn = $(obj).next().next();
                    $("#SendSMS").attr("onclick", $(smsLinkBtn).attr("onclick"));
                    $("#SendSMS").attr("aria-primary-key-name", $(smsLinkBtn).attr("aria-primary-key-name"));
                    $("#SendSMS").attr("rel", "DOD");
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
                            var smsData
                            $.each(record, function (index, item) {
                                try {
                                    var fieldName = item.split("~")[0];
                                    var fieldValue = item.split("~")[1].replace("1/1/1900 12:00:00 AM", "").replace("12:00:00 AM", "").replace("1/1/1971", "");

                                    
                                    if (fieldName == "Status")
                                    {
                                        $("#ctl00_ContentPlaceHolder1_cmb" + fieldName).val(fieldValue);
                                        $("#ContentPlaceHolder1_cmb" + fieldName).val(fieldValue);
                                    }
                                    else if (fieldName == "Contacted") {
                                        $("#ctl00_ContentPlaceHolder1_cmb" + fieldName).val(fieldValue);
                                        $("#ContentPlaceHolder1_cmb" + fieldName).val(fieldValue);
                                    }
                                    else if (fieldName == "FollowUpNeeded") {
                                        if (fieldValue.toLowerCase() == "true") {
                                            $("#ctl00_ContentPlaceHolder1_txt" + fieldName).prop("checked", true);
                                            $("#ContentPlaceHolder1_txt" + fieldName).prop("checked", true);
                                        }
                                        else {
                                            $("#ctl00_ContentPlaceHolder1_txt" + fieldName).prop("checked", false);
                                            $("#ContentPlaceHolder1_txt" + fieldName).prop("checked", false);
                                        }
                                    }
                                    else if ($("#ContentPlaceHolder1_ctl00_txt" + fieldName).length > 0) {
                                        $("#ContentPlaceHolder1_ctl00_txt" + fieldName).val(fieldValue);
                                    }
                                    else if ($("#ctl00_ContentPlaceHolder1_txt" + fieldName).length > 0) {
                                        $("#ctl00_ContentPlaceHolder1_txt" + fieldName).val(fieldValue);
                                    }
                                    else if ($("#ctl00_ContentPlaceHolder1_ctl00_txt" + fieldName).length > 0) {
                                        $("#ctl00_ContentPlaceHolder1_ctl00_txt" + fieldName).val(fieldValue);
                                    }
                                        else if ($("#ContentPlaceHolder1_txt" + fieldName).length > 0) {
                                        $("#ContentPlaceHolder1_txt" + fieldName).val(fieldValue);
                                    }
                                    else {
                                        $("#ContentPlaceHolder1_ctl00_txt" + fieldName).val(fieldValue);
                                    }

                                    if (typeof $("#ctl00_ContentPlaceHolder1_txt" + fieldName).attr("response-list-type") != 'undefined' && fieldName !="Comments") {
                                        SetQuestionDropDown($("#ctl00_ContentPlaceHolder1_txt" + fieldName));
                                    }
                                    else if (typeof $("#ContentPlaceHolder1_txt" + fieldName).attr("response-list-type") != 'undefined' && fieldName !="Comments") {
                                        SetQuestionDropDown($("#ContentPlaceHolder1_txt" + fieldName));
                                    }
                                    else if (fieldName != "Status" && fieldName != "FollowUpNeeded") {

                                        if ($("#ContentPlaceHolder1_ctl00_txt" + fieldName).length > 0) {
                                            SetAutoCompleteDropDown($("#ctl00_ContentPlaceHolder1_txt" + fieldName));
                                        }
                                        else if($("#ContentPlaceHolder1_txt" + fieldName).length > 0) {
                                            SetAutoCompleteDropDown($("#ContentPlaceHolder1_txt" + fieldName));
                                        }
                                    }
                                }
                                catch (e) {
                                    alert("get data: " + e.Message);
                                }
                            });

                            SetScoreFields();
                            GetScoreTotal();
                            GetScoreThreshold();
                        }

                        if (modeDisplay != "get") {

                            $("#edit-dialog").dialog("close");
                            oEditGrid.ajax.reload(null,true);

                            if (modeDisplay != "deleted" && modeDisplay != "" && modeDisplay != null) {
                                alert("Data was successfully " + modeDisplay);
                            }
                        }
                    },
                    error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
                });
            }
        }

        function SetScoreValue(obj) {
            var responseText = $(obj).attr("response-text");
            var responseValue = $(obj).attr("response-value");
            var scoreField = $(obj).attr("data-field-name");

            //if (editCompanyID!= "10108") {
            //    responseText = $(obj).attr("response-value");
            //    responseValue = $(obj).attr("response-text");
            //}

            $(obj).val(responseValue);

            var scoreValueLabel = $(obj).next();
            $(scoreValueLabel).text(responseText);
            GetScoreTotal();
        }

        function GetScoreTotal() {
            var total = 0;

            //if (editCompanyID == "10108") {
                if ($(".survey-question > input[response-value]").length > 0) {
                    $(".survey-question > input[response-value]").each(function () {
                        if ($(this).attr("data-field-name") != "CoordinatorQuestion" && $(this).attr("data-field-name") != "DamagesScore") {
                            total += parseInt($(this).attr("response-value"));
                        }
                    });
                }
            //}
            //else {
            //    if ($(".survey-question > input[response-value]").length > 0) {
            //        $(".survey-question > input[response-value]").each(function () {
            //            if ($(this).attr("data-field-name") != "CoordinatorQuestion") {
            //                total += parseInt($(this).next().text());
            //            }
            //        });
            //    }
            //}

            $("#totalQuestionScore").text(total);
        }

        function SetScoreFields() {
            //alert("here!!");
            $(".survey-question > input[response-list-type]").each(function () {
                if ($(this).attr("data-field-name") != "CoordinatorQuestion") {
                    var responseListName = $(this).attr("response-list-type");
                    var responseList = getResponseList(responseListName);
                    var responseText = $(this).val();
                    var fieldName = $(this).attr("data-field-name");
                    var responseCtrl = $(this);
                    var scoreLabelCtrl;
                    //console.log("field name: " + fieldName);
                    //console.log(responseList);
                    //console.log("response text: " + responseText);
                   
                    if ($(".survey-question > span[rel='" + fieldName + "']").length == 0) {
                        scoreLabelCtrl = $("<span class='response-score-value' rel='" + fieldName + "'></span>");
                    }
                    else {
                        scoreLabelCtrl = $(".survey-question > span[rel='" + fieldName + "']");
                    }

                    if (responseText.length > 0) {
                            
                        $.each(responseList, function (key, item) {
                            //if (editCompanyID != "10108") {
                                
                            //    if (item.label == responseText) {
                            //        $(responseCtrl).attr("response-value", item.value);
                            //        $(responseCtrl).attr("response-text", item.label);
                            //        $(scoreLabelCtrl).text(item.value);
                            //        return false;
                            //    }
                            //}
                            //else {
                                if (item.value== responseText) {
                                    
                                    $(responseCtrl).attr("response-value", item.value);
                                    $(responseCtrl).attr("response-text", item.value);
                                    $(scoreLabelCtrl).text(item.label);
                                    return false;
                                }
                            //}
                        });
                    }
                    else {
                        $(responseCtrl).attr("response-value", "0");
                        $(responseCtrl).attr("response-text", "");
                        $(scoreLabelCtrl).text("0");
                    }


                    if ($(".survey-question > span[rel='"+fieldName+"']").length == 0) {
                        $(responseCtrl).after($(scoreLabelCtrl));
                    }
                }
            });
        }

        function ShowEditDialog(obj) {
            if ($(obj).text().toLowerCase().indexOf("add record") != -1) {
                ClearFields();
                GetFieldList("");
                $("#SendSMS").removeAttr("onclick");
            }
            else {
                ClearFields();
            }

            SetDefaultFields();

            $("#edit-dialog").dialog({
                width: "1200px",
                height: "auto",
                modal: true,
                zIndex: 99999,
                open: function () {
                   // $('#edit-dialog').parent('.ui-dialog').css('zIndex', 99999)
                       // .nextAll('.ui-widget-overlay').css('zIndex', 88888);

                    if ($(obj).text().toLowerCase().indexOf("add record") != -1) {
                        $(".survey-question > input[response-list-type]").each(function () {
                            SetQuestionDropDown($(this));

                            var fieldName = $(this).attr("data-field-name");

                            if ($(".survey-question > span[rel='" + fieldName + "']").length == 0) {
                                $(this).after("<span class='response-score-value' rel='" + fieldName + "'></span>");
                            }
                        });
                    }
                }
            });
        }

        function ClearFields() {
            $(".edit-field >input").each(function () {

                var dataFieldName = $(this).attr("data-field-name");

                if (typeof dataFieldName != 'undefined') {
                    if (dataFieldName.toLowerCase() != "batchid" &&
                        dataFieldName.toLowerCase() != "companyid" &&
                        dataFieldName.toLowerCase() != "importid" &&
                        dataFieldName.toLowerCase() != "shipmentid" &&
                        dataFieldName !="") {
                        $(this).val("");
                    }
                }

                if ($(this).is(":checked")) {
                    $(this).prop("checked", false);
                }
            });

            $(".edit-field >textarea").each(function () {
                $(this).val("");
            });

            $(".edit-field >input").each(function () {
                $(this).val("");
            });

            $(".edit-field >select").each(function () {
                $(this).val("");
            });

            $(".edit-field > .response-score-value").each(function () {
                $(this).text("");
            });

            $(".edit-field > input[response-text]").each(function () {
                $(this).removeAttr("response-value");
                $(this).removeAttr("response-text");
                $(this).val("");
            });

            $("#SendSMS").removeAttr("onclick");
        }

        function GetFieldList(dMode) {
            var fieldlist = "";
            var fieldindex = 0;
            var totalScore = parseInt($("#totalQuestionScore").text());
            
            $(".response-field").each(function () {

                if ($(this).attr("aria-identity-column") == "true") {
                    identityField = $(this).attr("data-field-name");
                }
                                
                if ($(this).attr("aria-identity-column") == "false") {

                    if ($(this).attr("aria-data-type").indexOf('date') != -1) {
                        $(this).datepicker({
                            changeMonth: true,
                            changeYear: true
                        });
                    }
                    
                    if (($(this).attr("aria-data-type") == "numeric" ||
                        $(this).attr("aria-data-type") == "decimal" ||
                        $(this).attr("aria-data-type") == "int") && $(this).val() == "") {
                        
                        if ($(this).attr("id").toLowerCase().indexOf("companyid") == -1) {
                            $(this).val("0");
                        }
                    }
                    
                    //if ($(this).attr("aria-computed-column") == "true") {
                    //    $(this).attr("disabled", true)
                    //}

                    if ($(this).attr("data-field-name") != "undefined") {
                        
                        var fieldName = $(this).attr("data-field-name");
                        var tableType = $(this).attr("data-table-type");
                        
                        if (typeof $(this).attr("picklist-field-name") != "undefined") {
                            fieldName = $(this).attr("picklist-field-name");
                            tableType = $(this).attr("picklist-table-type");
                        }
                    }
                    
                    if (typeof $(this).attr("aria-default-column") != 'undefined') {
                        if ($(this).val() == "") {
                            var newtimestamp = GetTimeStamp();
                            $(this).val(newtimestamp);
                        }
                    }

                    var attrID = $(this).attr("id");
                    var fn = "";

                    if (attrID.indexOf("ctl00_ContentPlaceHolder1") > -1) {
                        fn = $(this).attr("id").replace("ctl00_ContentPlaceHolder1", "").replace("_txt", "").replace("_cmb", "");
                    }
                    else if (attrID.indexOf("ContentPlaceHolder1") > -1) {
                        fn = $(this).attr("id").replace("ContentPlaceHolder1", "").replace("_txt", "").replace("_cmb", "");
                    }

                    if ($(this).attr("aria-identity-column") != "true") {
                        if ($(this).attr("aria-computed-column") == "true" && (dMode == "save" || dMode == "add")) {
                            //Don't do anything.  we cannot save data to a computed column.
                        }
                        else if ((dMode == "save" || dMode == "complete survey" || dMode == "update record") && AllQuestionsAnswered() && fn == "Status" && !isNaN(totalScore) && totalScore < thresholdScore && editCompanyID=="10108"){
                            if (fieldindex == 0 && updateStatus) {
                                fieldlist = fieldlst + fn + "~Completed";
                            }
                            else if(updateStatus){
                                fieldlist = fieldlist + "|" + fn + "~Completed";
                            }
                            else {
                                fieldlist = fieldlist + "|" + fn + "~"+$(this).val();
                            }
                        }
                        else if ((dMode == "save" || dMode == "complete survey" || dMode == "update record") && AllQuestionsAnswered() && fn == "Status" && !isNaN(totalScore) && totalScore >= thresholdScore && editCompanyID == "10108") {
                            if ($(this).val() != "Completed") {
                                if (fieldindex == 0) {
                                    fieldlist = fieldlist + fn + "~Follow-up Needed";
                                }
                                else {
                                    fieldlist = fieldlist + "|" + fn + "~Follow-up Needed";
                                }
                            }
                            else
                            {
                                if (fieldindex == 0) {
                                    fieldlist = fieldlist + fn + "~" + $(this).val();
                                }
                                else {
                                    fieldlist = fieldlist + "|" + fn + "~" + $(this).val();
                                }
                            }
                        }
                        else if (fn == "SurveyReceivedDate" && dMode == "complete survey" && editCompanyID == "10108")
                        {
                            fieldlist = fieldlist + "|"+fn + "~" + GetTimeStamp();
                        }
                        else if (fn == "CompletionDate" && dMode == "complete survey" && editCompanyID == "10108") {
                            fieldlist = fieldlist + "|" + fn + "~" + GetTimeStamp();
                        }
                        else if (fn == "FollowUpStatus" && editCompanyID == "10108" && dMode == "update record")
                        {
                            //alert($(".edit-field >input[data-field-name='FirstCallDate']").val());
                            if ($(".edit-field >input[data-field-name='FirstFollowUpCallDate']").val().trim().length > 0 &&
                                $(".edit-field >input[data-field-name='FirstFollowUpCallDate']").val().trim() !="//") {
                                fieldlist = fieldlist + "|" + fn + "~AIS Call FollowUp";
                            }
                            else {
                                fieldlist = fieldlist + "|" + fn + "~";
                            }
                        }
                        else if (fn == "Source" && editCompanyID == "10108" && dMode == "update record") {
                            //alert($(".edit-field >input[data-field-name='FirstCallDate']").val());
                            if ($(".edit-field >input[data-field-name='FirstCallDate']").val().trim().length > 0 &&
                                $(".edit-field >input[data-field-name='FirstCallDate']").val().trim() !="//") {
                                fieldlist = fieldlist + "|" + fn + "~AIS Call";
                            }
                            //else if ($(".edit-field >input[data-field-name='FirstFollowUpCallDate']").val().trim().length > 0) {
                            //    fieldlist = fieldlist + "|" + fn + "~AIS Call";
                            //}
                        }
                        else if (fn == "FollowUpNeeded") {

                            if (!isNaN(totalScore) && totalScore>85 && editCompanyID=="10108") {
                                $(this).prop('checked', true);
                            }

                            if (fieldindex == 0) {
                                fieldlist = fieldlst + fn + "~" + $(this).is(":checked").toString();
                            }
                            else {
                                fieldlist = fieldlist + "|" + fn + "~" + $(this).is(":checked").toString();
                            }
                        }
                        else if (fn == "Source" && dMode == "complete survey" && editCompanyID == "10108") {
                            fieldlist = fieldlist + "|" + fn + "~AIS Call";
                        }
                        else if (fn == "CompletedTF" && !isNaN(totalScore) && totalScore > 0 && editCompanyID == "10108") {
                            fieldlist = fieldlist + "|" + fn + "~True";
                        }
                        else if (fieldindex == 0) {
                            fieldlist = fieldlist + fn + "~" + ($(this).val() == "//" ? "1/1/1900" : $(this).val());
                        }
                        else {
                            fieldlist = fieldlist + "|" + fn + "~" + ($(this).val() == "//" ? "1/1/1900" : $(this).val());
                        }
                    }
                    ++fieldindex;
                }
            });

            return fieldlist;
        }

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

        function S4() {
            return (((1 + Math.random()) * 0x10000000) | 0).toString(16);

        }

        function SetInputMask(ele) {
            var ctrlType = $(ele).attr("data-field-name");

            if (typeof ctrlType != 'undefined') {
                if (ctrlType.toLowerCase().indexOf("phone") > -1) {
                    $(ele).mask('000-000-0000');
                }
                else if (ctrlType.toLowerCase().indexOf("amount") > -1) {
                    $(ele).mask('#,###,000.00', { reverse: true, prefix: "$" });
                }
                else if (ctrlType.toLowerCase().indexOf("dob") > -1) {
                    $(ele).mask('99/999/9999');
                }
                else if (ctrlType.toLowerCase().indexOf("date") > -1) {
                    $(ele).mask('99/999/9999');
                }
            }
        }

        function AllQuestionsAnswered() {
            var result = true;

            $(".survey-question > input[response-list-type]").each(function () {
                if ($(this).val().length == 0) {
                    result=false;
                }
            });

            return result;
        }

        function GetScoreThreshold() {

            thresholdScore = 0;

            try {
                var gbl = $("#ctl00_ContentPlaceHolder1_txtGBL").val();

                if (typeof gbl !== 'undefined') {
                    var dataUri = "../ListHandler.ashx?gt=users&ct=grid&qt=StoredProc&fld=&qn=Cartwright_GetThresholdScore&pl=GBL~" + gbl + "&qid=" + S4();

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: dataUri,
                        dataType: "json",
                        success: function (data) { thresholdScore = parseInt(data.aaData[0][1]); },
                        error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
                    });
                }
            }
            catch (e)
            {
                console.log("GetScoreThreshold Error: " + e);
            }
        }
    </script>
    <asp:Literal runat="server" ID="litScript"></asp:Literal>

    <div>
        <h1 style="padding:15px;"><font style="color:#fff;">Call Tracker</font></h1>
        <asp:Panel runat="server" ID="pChart" class="grid-section" grid-query-type="text" grid-type="search" style="width:95%;" >
            <div id="edit-grid-filter-section" class="chart-title">
                <span>Tables:</span>
                <asp:DropDownList runat="server" ID="ddlTables" CssClass="edit-grid-dropdown-filter" />
                <div class="current-filter-section">
                    <span class="current-filter">All</span>
                </div>
                <div class="status-legend">
                    <span>View All</span>
                    <!--<span><img src="../images/workflow-complete.png" border="0"/> Follow-up Needed</span>-->
                    <span id="blue-status"><img src="../images/blue-button.png" border="0"/>  Open</span>
                    <span id="green-status"><img src="../images/green-button.png" border="0"/>  Spoke To</span>
                    <span id="orange-status"><img src="../images/orange-button.png" border="0"/>  Disc</span>
                    <span id="red-status"><img src="../images/red-button.png" border="0"/>  Unable To Reach</span>
                </div>
            </div>
            <table id="callcenter-grid" class="grid-control" style="width:100%;" >
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
            <a id="lnkAddRecord" class="add-record" href="javascript:void(0)" onclick='$("#lnkOK").text("Add");ShowEditDialog(this); return false;'>Add Record</a>
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
</asp:Content>

