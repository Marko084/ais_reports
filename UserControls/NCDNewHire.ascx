<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDNewHire.ascx.cs" Inherits="AISReports.UserControls.NCDNewHire" %>
<style type="text/css">
    .driver-link {position:relative; left:40px; width:95px;text-align:left;font-size:smaller !important;}
    .edit-field span {display:inline-block; width:300px; vertical-align: top;}
    .question-label {display:block !important;width:100% !important;}
    .total-error {background-color:yellow;}
    .total-good {background-color:transparent;}
    .comment-id {display:none;} 
    .accordion div span {display:inline-block; width:275px;}
    .accordion div span span {display:inline;}
    .accordion { font-size:small !important;}
    .data-indicator {float:right; width:20px; height:20px; }
    .response-field {width:200px;}
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

    input[type='checkbox'] { text-align:left !important;}

    .none-status {color:transparent;}
    .status-legend {float:right; height:25px; padding-right:15px;}
    .status-legend span {vertical-align: middle !important; padding-top:2px !important; font-size:x-small;}
    .status-legend img {border: 0 none transparent; position:relative; top:2px;}
    .gm-btn { background-image: url('../images/green-gear-icon.png'); display:inline-block; background-repeat: no-repeat;background-position: center; width:100%; height:16px; color:transparent;}
    .corp-btn { background-image: url('../images/blue-gear-icon.png'); display:inline-block; background-repeat: no-repeat;background-position: center; width:100%; height:16px; color:transparent;}
    .exec-btn { background-image: url('../images/orange-gear-icon.png'); display:inline-block; background-repeat: no-repeat;background-position: center; width:100%; height:16px; color:transparent;}
    .finance-btn { background-image: url('../images/red-gear-icon.png'); display:inline-block; background-repeat: no-repeat;background-position: center; width:100%; height:16px; color:transparent;}
    .complete-btn { background-image: url('../images/workflow-complete.png'); display:inline-block; background-repeat: no-repeat;background-position: center; width:100%; height:16px; color:transparent;}
    .comment-indicator {
        display: inline-block;
        height: 15px;
        background-repeat: no-repeat;
        background-position: center;
        width: 15px;
        color: transparent;
        background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAPCAMAAADarb8dAAAABGdBTUEAALGPC/xhBQAAAAFzUkdCAK7OHOkAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAASxQTFRFAAAAvb7Avb7AuX13t1xVtlNMtlRNt19YuouGvb7AuYR/tkxDtlRMupKOuXt1tTcutT83uoiDuo+LtToxtUY9vKqovb7At1hRt2VevK2rtkpCvb7AuX54vLKwuHJqu5+ZuYB6vLWzvLGwtTowtkxEvb7At1tTuGliupaStT01tkpBvLCvuYB6tTsytUQ7uo+KuoyHtlJKt1pTu5yYuo2It2Jbt1hQt1lRt2Zfu5iUvb7AtC4lz3pz897b8NXRxmFYtC8mvUc+/vv7////9+rotTIozXVtv01Eym1kvEY8tTcuukA2/vz89ubk7czJ36WgtDIp1ImCxmJawE5F+/TytjMptDQqtTAm+vLw7c3J8tvY5LSvyWlhwlZNtzgu7czItjUs8NfT5rm0tDMq7MM1xQAAADt0Uk5TAAAOaazLx6NYBl3s3D54/vlORf3yIQHPoS70CWIzc0NgMCn+8QfGlzr77Bll/fNASuDMLlWYtrOOQwIQTUE5AAAAAWJLR0RDZ9ANYgAAAAlwSFlzAAALEwAACxMBAJqcGAAAAMJJREFUCNctj2lDQWEQhUcR11KylEiizZpdMtRlrl1lyRaK/P//0Dvv7XyY58yZL2cAhA4OjaYjs0UBXQarDau1l1fV7tCD4xOsN4i0JjpPeXe5EVsk1Eb0eAHOzhGxw0FXGB/ARU+wPyB6E0R/AC6Z+E70wRwG4UoGI6IxUw3BtQwm9DllzsIQuWEzXyzl4fYODPfSfa0kHkSPaIzdesMznuBmyRTit/Yj9se03j2TVbe7X9zn8v/fgVIolp7KzxX2f8RfKoqIlSIdAAAAYnRFWHRjb21tZW50AGJvcmRlciBiczowIGJjOiMwMDAwMDAgcHM6MCBwYzojMDAwMDAwIGVzOjAgZWM6IzAwMDAwMCBjazphNjEzOWE1MmNkM2FiNmE0M2Q5MDM1MTg3NTQzOTAyNiI6ldMAAAAldEVYdGRhdGU6Y3JlYXRlADIwMTQtMTItMDhUMTE6MjA6MTUrMDA6MDDCf8A8AAAAJXRFWHRkYXRlOm1vZGlmeQAyMDE0LTEyLTA4VDExOjIwOjE1KzAwOjAwsyJ4gAAAABl0RVh0U29mdHdhcmUAQWRvYmUgSW1hZ2VSZWFkeXHJZTwAAAAASUVORK5CYII=);
    }
    #show-comments{color:#000;}
    #show-cr-comments{color:#000;}
    #frmClaimsDocuments {background:#fff url('../images/clear-sm.png') 50% 50% repeat !important;}
    #messageBox {display:inline-block; width:280px;}
</style>
<script type="text/javascript">

    var dataTableName;
    var primaryKeyFieldName;
    var oNewHireGrid;
    var primaryKeyValue;
    var loggedInUser;

    $(document).ready(function () {

        $("#lnkOK").button();
        $(".add-record").button();
        $("#add-comment").button();
        $("#lnkSubmitForApproval").button();
        $("#lnkPrint").button();

        loggedInUser = $.trim($("#ctl00_lblUserInfo").text().replace("Welcome", "").replace("IMPERSONATION MODE ", ""));
        var table = $("#edit-grid");
        var div = $("div[grid-type='newhire']");

        dataTableName = $(".edit-grid-dropdown-filter").val();
        primaryKeyFieldName = $(div).attr("primary-key-field");

        //dataUri = "../ListHandler.ashx?gt=edit&ct=grid&qt=text&fld=&qn=select * from " + dataTableName + "&pl=&qid=" + S4();
        dataUri = "../ListHandler.ashx?gt=edit&ct=grid&qt=storedproc&fld=&qn=ars_getnewhiredata&pl=companyid~"+cid+"|userid~"+uid+"&qid=" + S4();

        oNewHireGrid = $(table).dataTable({
            "bDestroy": true,
            "sAjaxSource": dataUri,
            "aLengthMenu": [[10, 20, 25, -1], [10, 20, 25, "All"]],
            "sPaginationType": "full_numbers",
            "bScrollCollapse": true,
            "bDeferRender": true,
            "bJQueryUI": true,
            "oLanguage": { "sZeroRecords": "No data found to display." },
            "sDom": 'r<"H"lf><"datatable-scroll"t><"F"ip>',
            "oTableTools": {
                "aButtons": [{
                    "sExtends": "xls",
                    "sFileName": "SurveyResults.xls"
                }],
                "sSwfPath": "../media/swf/copy_csv_xls_pdf.swf"
            },
            "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                var colIndex = oNewHireGrid.fnGetColumnIndex(primaryKeyFieldName.toTitleCase());
                var statusIdx = oNewHireGrid.fnGetColumnIndex("Status");
                var commentsIdx = oNewHireGrid.fnGetColumnIndex("Exec Comment");
               // console.log("Exec Comment IDX: " + commentsIdx);
                $('td:eq(0)', nRow).html("<a href='#' rel='" + aData[colIndex] + "' onclick='javascript:ShowEditDialog(this,\"get\"); return false;'>Edit</a>" +
                    "&nbsp;<a href='#' rel='" + aData[colIndex] + "' onclick='javascript: RunQuery(this,\"delete\"); return false;'>Delete</a>" +
                    "&nbsp;<a href='#' rel='" + aData[colIndex] + "' onclick='javascript:PrintNewHireItem(this); return false;'>Print</a>" +
                    "&nbsp;<span rel='" + aData[colIndex] + "'"+(aData[commentsIdx]=="True" ? " class='comment-indicator' " : "")+"></span>");
                $("td:eq(1)", nRow).html("<span class='" + aData[statusIdx].toLowerCase()+ "-btn'>" + aData[statusIdx] + "</span>");
            }
        });

        var columnToHideIdx = oNewHireGrid.fnGetColumnIndex(("CompanyID").toTitleCase());

        if (columnToHideIdx > -1) {
            oNewHireGrid.fnSetColumnVis(columnToHideIdx, false);
        }

        columnToHideIdx = oNewHireGrid.fnGetColumnIndex(("BatchID").toTitleCase());

        if (columnToHideIdx > -1) {
            oNewHireGrid.fnSetColumnVis(columnToHideIdx, false);
        }

        columnToHideIdx = oNewHireGrid.fnGetColumnIndex(("ImportID").toTitleCase());

        if (columnToHideIdx > -1) {
            oNewHireGrid.fnSetColumnVis(columnToHideIdx, false);
        }

        //console.log("utype: " + utype);
        if (utype == "CORP") {
            oNewHireGrid.fnSetColumnVis(oNewHireGrid.fnGetColumnIndex(("PayType").toTitleCase()),false);
            oNewHireGrid.fnSetColumnVis(oNewHireGrid.fnGetColumnIndex(("PayRate").toTitleCase()),false);
        }

        ShowPayTypeSubControls(false);
        
        
    })

    function PrintNewHireItem(ele) {
        var url = "../PrintPage.aspx?cid=" + cid + "&tn=ars_consolidated_newemployee&qtype=newhire&kn=ImportID&kv=" + $(ele).attr("rel");
        $("#newhire-print-frame").attr("src", url);
    }

    function ReloadGrid() {
        //oNewHireGrid.fnReloadAjax();
        oNewHireGrid.ajax.reload(null,false);
    }

    function isDate(val) {
        var d = new Date(val);
        return !isNaN(d.valueOf());
    }

    function ShowPayTypeSubControls(visibleTF) {

        var guaranteedDDL = $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_ddlGuaranteedPeriod");
        var lengthOfTxt = $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txtLengthOfTimePeriod");
        $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txtExecComment").parent().hide();

        if (visibleTF) {
            guaranteedDDL.parent().show();
            lengthOfTxt.parent().show();
        }
        else {
            guaranteedDDL.parent().hide();
            lengthOfTxt.parent().hide();

            guaranteedDDL.val("");
            lengthOfTxt.val("");
        }
    }

    function SetPickLists() {
        var positionTypesList = ["CSR", "DISPATCHER", "ACCOUNTING CLERK", "ACCOUNTING MANAGER", "SALES MANAGER", "OPERATIONS MANAGER", "GENERAL MANAGER",
                                     "RECEPTIONIST", "SALES REP", "TELEMARKETER", "RECORDS STG CREW", "RECORDS STG MANAGER", "O & I SUPERVISOR", "HELPER",
                                     "PACKER", "WAREHOUSE WORKER", "WAREHOUSE SUPERVISOR", "ASST WAREHOUSE MGR", "WAREHOUSE MANAGER", "NON CDL DRIVER",
                                     "CDL CLASS B DRIVER", "CDL CLASS A DRIVER", "MECHANIC", "COMMISSION OTR DRIVER"];
        var yesNoList = ["YES", "NO"];
        var branchLocations = ["1008", "1021", "1037", "1038", "1039", "1148", "1253", "1287", "1427", "1651", "1676", "1679", "2611"];
        var payTypes = ["Salary Exempt", "Salary Non-Exempt", "Hourly Paid Full", "Hourly Part Time", "Draw vs. Commission"];
        var payRates = ["Hourly", "Salary"];

        $("#edit-dialog input").each(function (idx, ele) {
            if ($(ele).attr("data-field-name").toLowerCase() == "positiontype") {

                $(ele).autocomplete({
                    source: positionTypesList,
                    minLength: 0,
                    select: function () {
                        $(this).autocomplete("close");
                    }
                }).click(function () {
                    $(this).autocomplete("search");
                });

                //$(ele).autocomplete({
                //    source: positionTypesList
                //});
            }
            else if ($(ele).attr("data-field-name").toLowerCase() == "negativedrugresultreceived") {

                //$(ele).autocomplete({
                //    source: yesNoList
                //});
                $(ele).autocomplete({
                    source: yesNoList,
                    minLength: 0,
                    select: function () {
                        $(this).autocomplete("close");
                    }
                }).click(function () {
                    $(this).autocomplete("search");
                });
            }
        });

        $("#edit-dialog select").each(function (idx, ele) {
            var dataFieldName = $(ele).attr("data-field-name").toLowerCase();

            if (dataFieldName == "branchlocation") {
                $(ele).empty();

                var list = $(ele);
                list.append(new Option("", ""));

                $.each(branchLocations, function (index, item) {
                    list.append(new Option(item, item));
                });
            }
            else if (dataFieldName == "paytype") {
                $(ele).empty();

                var list = $(ele);
                list.append(new Option("", ""));

                $.each(payTypes, function (index, item) {
                    list.append(new Option(item, item));
                });

                $(list).change(function () {
                    if ($(this).val() == "Draw vs. Commission") {
                        ShowPayTypeSubControls(true);
                    }
                    else {
                        ShowPayTypeSubControls(false);
                    }
                });
            }
            else if (dataFieldName == "guaranteedperiod") {
                $(ele).empty();

                var list = $(ele);
                list.append(new Option("", ""));

                $.each(yesNoList, function (index, item) {
                    list.append(new Option(item, item));
                });
            }
            else if (dataFieldName == "payrate") {
                $(ele).empty();

                var list = $(ele);
                list.append(new Option("", ""));

                $.each(payRates, function (index, item) {
                    list.append(new Option(item, item));
                });
            }
        });
    }

    function ShowApproveButton() {
        var status = $(".edit-field >input[data-field-name='Status']").val().toLowerCase();

        $("#lnkSubmitForApproval").hide();

        if (status == "" && (utype.toLowerCase()=="manager" || utype.toLowerCase()=="user")) {
            return false;
        }
        else if (status == "gm") {

            if (utype.toLowerCase() == "gm" && $(".edit-field >input[data-field-name='GMApprovalDate']").val() == "") {
                $("#lnkSubmitForApproval").show();
                return true;
            }

            return false;
        }
        else if (status == "corp") {
            if (utype.toLowerCase() == "corp" && $(".edit-field >input[data-field-name='VerificationAdminDate']").val() == "") {
                $("#lnkSubmitForApproval").show();
                return true;
            }
            return false;
        }
        else if (status == "exec") {
            if (utype.toLowerCase() == "exec" && $(".edit-field >input[data-field-name='ExecutiveApprovalDate']").val() == "") {
                $("#lnkSubmitForApproval").show();
                return true;
            }
            return false;
        }
        else if (status == "finance") {
            if (utype.toLowerCase() == "finance" && $(".edit-field >input[data-field-name='PayrollEnteredDate']").val() == "") {
                $("#lnkSubmitForApproval").show();
                return true;
            }
            return false;
        }
    }

    function ShowEditDialog(obj,mode) {

        ClearFields();
        SetFieldBehaviors();

        DisplayMessage("","hide");
        
        $("#add-comment").show();
        $("#lnkSubmitForApproval").attr("rel", $(obj).attr("rel"));
        $("#lnkPrint").attr("rel", $(obj).attr("rel"));

        if (mode != "open") {
            primaryKeyValue = $(obj).attr("rel");
            RunQuery(obj, mode);
            LoadComments();
        }

        if ($(obj).text().toLowerCase() == "add record") {
            $('#lnkOK').text('Add');
            $("#lnkOK").attr("onclick", "javascript:RunQuery(this,'add');");
            $("#lnkOK").removeAttr("rel");
            $("#add-comment").hide();
            $("#lnkSubmitForApproval").hide();

            var ts = GetTimeStamp();

            $(".edit-field >input[data-field-name='CreatedDate']").val(ts);
            $(".edit-field >input[data-field-name='LastUpdatedDate']").val(ts);
            $(".edit-field >input[data-field-name='CreatedBy']").val($.trim($("#ctl00_lblUserInfo").text().replace("Welcome", "").replace("IMPERSONATION MODE ", "")));
            $(".edit-field >input[data-field-name='LastUpdatedBy']").val($.trim($("#ctl00_lblUserInfo").text().replace("Welcome", "").replace("IMPERSONATION MODE ", "")));

            SetUserName();
        }
        else if ($(obj).text().toLowerCase() == "edit") {
            $("#lnkOK").text('Update');
            $("#lnkOK").attr("rel", $(obj).attr("rel"));
            $("#lnkOK").attr("onclick", "javascript:RunQuery(this,'update');");
        }

        $("#edit-dialog").dialog({
            minWidth: 1120,
            minHeight: 650,
            modal: true,
            zIndex: 9000
        });

        SetPickLists();
        SetInputMask();
    }

    function ClearFields() {

        $(".edit-field >input").each(function () {
            if (!$(this).is(':disabled') && $(this).attr("aria-data-type") == "bit") {
                $(this).prop('checked', false);
            }
            else if (!$(this).is(':disabled')) {
                $(this).val("");
            }
            else if ($(this).is(':disabled') && $(this).attr("aria-data-type") == "numeric") {
                $(this).val("0");
            }
            else if ($(this).is(':disabled') && $(this).attr("aria-data-type") == "varchar") {
                $(this).val("");
            }

        });

        $(".edit-field >textarea").each(function () {
            $(this).val("");
        });

        $(".edit-field >select").each(function () {
            $(this).val("");
        });

        $(".comments-list-area").empty();

    }

    function RunQuery(obj, mode) {

        mode=mode.toLowerCase();

        if (mode == "delete") {
            var deleteRecord = confirm('Are you sure you want to delete this record?');

            if (!deleteRecord) {
                return false;
            }
        }
        else if (mode == "update" || mode=="updatecomments" ||"submitapproval") {
            $(".edit-field >input[data-field-name='LastUpdatedDate']").val(GetTimeStamp());
            $(".edit-field >input[data-field-name='LastUpdatedBy']").val(loggedInUser);
        }

        var NewJSONDBQuery = {};

        if (mode == "submitapproval") {
            NewJSONDBQuery.FieldList = GetFieldList("update");
            NewJSONDBQuery.QueryType = ("update");
        }
        else if (mode == "updatecomments") {
            NewJSONDBQuery.FieldList = GetFieldList("updatecomments");
            NewJSONDBQuery.QueryType = ("update");
        }
        else {
            NewJSONDBQuery.FieldList = GetFieldList(mode);
            NewJSONDBQuery.QueryType = (mode);
        }
        
        NewJSONDBQuery.TableName = dataTableName;
        NewJSONDBQuery.KeyFieldName = primaryKeyFieldName;
        NewJSONDBQuery.KeyFieldValue = $(obj).attr("rel");
        NewJSONDBQuery.UserID = uid;
        
        var DTO = { 'NewJSONDBQuery': NewJSONDBQuery };
       // console.log(JSON.stringify(DTO));
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/JSONDBQuery",
            data: JSON.stringify(DTO),
            dataType: "json",
            beforeSend: function () { },
            success: function (data) {
                if (mode.toLowerCase() == "get") {

                    SetPickLists();
                    var record = data.d.FieldList.split("|");

                    console.log(record);

                    $.each(record, function (index, item) {
                        try {
                            var fieldName = item.split("~")[0];
                            var fieldValue = item.split("~")[1].replace("1/1/1900 12:00:00 AM", "").replace("12:00:00 AM", "");
                            var editFieldCtrl = "";
  
                            if (fieldName == "BranchLocation") {
                                editFieldCtrl = $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_ddl" + fieldName);
                            }
                            else if (fieldName == "PayType") {
                                editFieldCtrl = $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_ddl" + fieldName);
                                if (fieldValue == "Draw vs. Commission") {
                                    ShowPayTypeSubControls(true);
                                }
                                else {
                                    ShowPayTypeSubControls(false);
                                }
                            }
                            else if (fieldName == "GuaranteedPeriod") {
                                editFieldCtrl = $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_ddl" + fieldName);
                            }
                            else if (fieldName == "PayRate") {
                                editFieldCtrl = $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_ddl" + fieldName);
                            }
                            else {
                                editFieldCtrl = $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txt" + fieldName);
                            }

                            if ($(editFieldCtrl).attr("aria-data-type").toLowerCase() == "bit") {

                                if (fieldValue == "False" || fieldValue == "") {
                                    $(editFieldCtrl).prop('checked', false);
                                }
                                else if (fieldValue == "True") {
                                    $(editFieldCtrl).prop('checked', true);
                                }
                            }
                            else if (fieldName.toLowerCase().indexOf("amount") > 0) {
                                $(editFieldCtrl).val(accounting.formatMoney(fieldValue));
                            }
                            else {
                                $(editFieldCtrl).val(fieldValue);
                            }
                        }
                        catch (e) {
                            console.log(e.Message);
                        }
                    });

                    SetUserName();
                    ShowApproveButton();
                }
                else if (mode == "add" || mode=="submitapproval") {
                    SendEmail();
                    ReloadGrid();
                }

                if (mode.toLowerCase() == "update" || mode.toLowerCase()=="delete") {
                    ReloadGrid();
                    DisplayMessage("Record " + mode + "d.", "show");
                }

                if (mode.toLowerCase() == "update" && GetWorkflowStatus() == "Completed")
                {
                    SendEmail();
                }

                if (mode.toLowerCase() == "updatecomments") {
                    ReloadGrid();
                }

               
            }
        });
    }

    function GetFieldList(mode) {
        var list = "";

        if (mode == "get") {
            $("#edit-detail-section input, #edit-detail-section select").each(function () {
                if (list.length == 0) {
                   list = $(this).attr("data-field-name");
                }
                else {
                    list += "|" + $(this).attr("data-field-name");
                }
            });

            return list;
        }

        if (mode == "add" || mode == "save" || mode == "update" || mode == "updatecomments") {
            $("#edit-detail-section input, #edit-detail-section select").each(function () {

                try {
                    var fn = $(this).attr("data-field-name");
                    var fv = $(this).val();
                
                    if ($(this).attr("aria-data-type") == "numeric") {
                        fv = fv.replace("$", "");
                        fv = fv.replace("", "0");
                        fv = fv.replace(",", "");
                    }
                    else if (mode == "updatecomments" && $(this).attr("data-field-name") == "ExecComment" && (loggedInUser != "Jon Schroeder" || loggedInUser != "Mark Hopkins")) {
                        fv = true;
                    }
                    else if (mode == "updatecomments" && $(this).attr("data-field-name") == "ExecComment" && (loggedInUser != "Jon Schroeder" || loggedInUser != "Mark Hopkins")) {
                        fv = false;
                    }
                    else if ($(this).attr("aria-data-type") == "bit") {
                        fv = $(this).is(":checked");
                    }
                    //console.log("mode: " + mode + "  fn: " + fn + "   value: " + fv);
                    if ($(this).attr("aria-identity-column") === "false" &&
                        typeof $(this).attr("aria-indicator-column") === 'undefined' &&
                        $(this).attr("aria-computed-column") === "false") {

                        if (list.length == 0) {
                            list = fn+"~"+fv;
                        }
                        else {
                            list += "|" + fn + "~" + fv;
                        }
                    }
                }
                catch (e) {
                   // console.log(e.message);
                }
            });

            return list;
        }
    }

    function SetFieldBehaviors() {
        $("#edit-detail-section input").each(function () {
            if($(this).attr("aria-data-type") == "datetime") {
                $(this).datepicker();
            }
            else if ($(this).attr("aria-data-type") == "varchar") {
                $(this).autocomplete({
                    source: "../ListHandler.ashx?cid=" + cid + "&uid=" + uid + "&fn=" + $(this).attr("data-field-name") + "&tn=" + $(this).attr("data-table-type") + "&qt=lookup&qid=" + S4(),
                minLength: 1
            });
            }
        });
        
    }

    function LoadComments() {

        $("#comments").val('');
        $("#show-comments").empty();

        try {
            $.ajax({
                type: "GET",
                contentType: "text/html",
                cache: false,
                url: "../DeficientSurveyComments.aspx?sid=" + primaryKeyValue + "&cid=" + cid + "&ctype=NewHire",
                success: function () {
                    $("#show-comments").load("../DeficientSurveyComments.aspx?sid=" + primaryKeyValue + "&cid=" + cid + "&ctype=newhire&key=" + new Date().getTime());
                },
                error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
            });

        }
        catch (e) {
            alert(e.message);
        }
    }

    function SaveComments() {

        var NewSurveyComment = {};

        NewSurveyComment.UserID = uid;
        NewSurveyComment.CompanyID = cid;
        NewSurveyComment.Comments = $("#comments").val();
        NewSurveyComment.SurveyID = primaryKeyValue;
        NewSurveyComment.CommentType = "NewHire";
        NewSurveyComment.ParentID = "0";
        NewSurveyComment.UserType = "";

        var DTO = { 'NewSurveyComment': NewSurveyComment };

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/AddSurveyComment",
            data: JSON.stringify(DTO),
            dataType: "json",
            success: function () {
                LoadComments();

                $(".edit-field >input[data-field-name='LastUpdatedDate']").val(GetTimeStamp());
                $(".edit-field >input[data-field-name='LastUpdatedBy']").val($.trim($("#ctl00_lblUserInfo").text().replace("Welcome", "").replace("IMPERSONATION MODE ", "")));

                //$("#lnkOK").click();

                var updateBtn = $("<a></a>");

                $(updateBtn).attr("href", "#");
                $(updateBtn).attr("rel", $("#lnkOK").attr("rel"));

                RunQuery($(updateBtn), "updatecomments");
            },
            error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
        });
    }

    function GetTimeStamp() {
        var date = new Date();
        var hours = date.getHours();
        var minutes = date.getMinutes();
        var seconds = date.getSeconds();
        var strTime = hours + ':' + minutes + ':' + seconds + '.000';

        return date.getMonth() + 1 + "/" + date.getDate() + "/" + date.getFullYear() + " " + strTime;
    }

    function SendEmail() {

        var status = GetWorkflowStatus();
        var firstName = $(".edit-field >input[data-field-name='FirstName']").val();
        var lastName = $(".edit-field >input[data-field-name='LastName']").val();
        var originatorEmail = $(".edit-field >input[data-field-name='OriginatorEmail']").val();
        
        var replyLink = "<a href='http://" + url + "/site/NewHire.aspx?cid=" + cid + "'>AIS Reports Management Site</a>";
        var NewNCDMailMessage = {};

        NewNCDMailMessage.FromAddress = "NewHire@atlanticrelocation.com";

        if (status == "Completed") {
            NewNCDMailMessage.ToAddress = originatorEmail;
            NewNCDMailMessage.Subject = "New hire workflow for "+firstName+" "+lastName+" has been completed.";
            NewNCDMailMessage.Message = "<p>Please log into " + replyLink + " to view details. </p>";
        }
        else {
            NewNCDMailMessage.ToAddress = $(".send-to-address").val();
            NewNCDMailMessage.Subject = "New hire request for "+firstName+" "+lastName+" needs " + status + " approval.";
            NewNCDMailMessage.Message = "<p>Please log into " + replyLink + " to approve this request. </p>";
        }

        NewNCDMailMessage.CCAddress = "";
        NewNCDMailMessage.BCCAddress = "";

        var DTO = { 'NewNCDMailMessage': NewNCDMailMessage };

        //console.log(JSON.stringify(DTO));
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/SendEmailMessage",
            data: JSON.stringify(DTO),
            dataType: "json",
            beforeSend: function () { },
            success: function (data) {
                if (status == "Completed") {
                    DisplayMessage("An e-mail has been sent to " + originatorEmail+ " as complete.", "show");
                }
                else {
                    DisplayMessage("An e-mail has been sent to " + $(".send-to-address").val() + " for approval.", "show");
                }
                //$("#edit-dialog").dialog("close");
            },
            error: function (xhr, textStatus, error) {
                alert(xhr.responseText);
            }
        });
    }

    function GetWorkflowStatus() {
        var status = $(".edit-field >input[data-field-name='Status']").val();
        var userType = $(".utype").text();

        if (status == "" && userType == "GM") {
            return "Corporate";
        }
        if (status == "") {
            return "General Manager";
        }
        else if (status == "GM") {
            return "Corporate";
        }
        else if (status == "CORP") {
            return "Executive";
        }
        else if (status == "EXEC") {
            return "Finance";
        }
        else {
            return "Completed";
        }
    }
    
    function GetWorkflowStep() {
        var status = $(".edit-field >input[data-field-name='Status']").val();
        
        if (status == "") {
            return "GM";
        }
        else if (status == "GM") {
            return "CORP";
        }
        else if (status == "CORP") {
            return "EXEC";
        }
        else if (status == "EXEC") {
            return "FINANCE";
        }
        else {
            return "COMPLETE";
        }
    }

    function SetUserName() {
        var gmName = $(".edit-field >input[data-field-name='GMApprovalName']").val();
        var corpName = $(".edit-field >input[data-field-name='VerificationAdminName']").val();
        var execName = $(".edit-field >input[data-field-name='ExecutiveApprovalName']").val();
        var financeName = $(".edit-field >input[data-field-name='PayrollAdminCompletedBy']").val();
        var origEmail = $(".edit-field >input[data-field-name='OriginatorEmail']").val();
        var createdBy = $(".edit-field >input[data-field-name='CreatedBy']").val();

        if (utype.toLowerCase() == "gm" && gmName=="") {
            $(".edit-field >input[data-field-name='GMApprovalName']").val(loggedInUser);
        }
        else if (utype.toLowerCase() == "corp" && corpName == "") {
            $(".edit-field >input[data-field-name='VerificationAdminName']").val(loggedInUser);
        }
        else if (utype.toLowerCase() == "exec" && execName == "") {
            $(".edit-field >input[data-field-name='ExecutiveApprovalName']").val(loggedInUser);
        }
        else if (utype.toLowerCase() == "finance" && financeName == "") {
            $(".edit-field >input[data-field-name='PayrollAdminCompletedBy']").val(loggedInUser);
        }

        if (utype.toLowerCase() == "corp") {
            $(".edit-field >input[data-field-name='BackgroundCIDNumber']").removeAttr("disabled");
            $(".edit-field >input[data-field-name='BackgroundApprovalDate']").removeAttr("disabled");
        }
        else {
            $(".edit-field >input[data-field-name='BackgroundCIDNumber']").attr("disabled", "disabled");
            $(".edit-field >input[data-field-name='BackgroundApprovalDate']").attr("disabled", "disabled");
        }

        if (origEmail == "") {
            $(".edit-field >input[data-field-name='OriginatorEmail']").val($(".originator-email").text());
        }

        if (loggedInUser == createdBy) {
            $(".edit-field >select[data-field-name='PayRate']").removeAttr("disabled");
            $(".edit-field >input[data-field-name='PayRateAmount']").removeAttr("disabled");
            $(".edit-field >select[data-field-name='PayType']").removeAttr("disabled");
            $(".edit-field >input[data-field-name='GuaranteedPeriod']").removeAttr("disabled");
            $(".edit-field >input[data-field-name='LengthOfTimePeriod']").removeAttr("disabled");
        }
        else
        {
            $(".edit-field >select[data-field-name='PayRate']").attr("disabled", "disabled");
            $(".edit-field >input[data-field-name='PayRateAmount']").attr("disabled", "disabled");
            $(".edit-field >select[data-field-name='PayType']").attr("disabled", "disabled");
            $(".edit-field >input[data-field-name='GuaranteedPeriod']").attr("disabled", "disabled");
            $(".edit-field >input[data-field-name='LengthOfTimePeriod']").attr("disabled", "disabled");
        }
    }

    function DisplayMessage(message, display) {

        $("#messageBox div p label").text("");
        $("#messageBox div p label").text(message);

        if (display == "hide") {
            $("#messageBox").hide();
        }
        else {
            $("#messageBox").fadeOut(500);
            $("#messageBox").fadeIn(500);
            //$("#messageBox").show();
        }
    }

    function SetInputMask() {

        var ctrlType = "";
        $(".edit-field > input").each(function () {
            ctrlType = $(this).attr("data-field-name").toLowerCase();

            if (ctrlType.indexOf("phone") > -1) {
                $(this).mask('000-000-0000');
            }
            else if (ctrlType.indexOf("amount") > -1) {
                $(this).mask('#,###,000.00', { reverse: true, prefix: "$" });
            }
            else if (ctrlType.indexOf("dob") > -1) {
                $(this).mask('99/999/9999');
                $(this).datepicker({
                    changeMonth: true,
                    changeYear: true
                });
            }
        });
    }
</script>
<asp:Literal runat="server" ID="litScript" EnableViewState="false" />

<div class="user-control-widget">
    <div class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix" style="padding: 5px;">
        <asp:Label runat="server" ID="lblChartTitle" Text="" EnableViewState="false" />
    </div>
    <asp:Panel runat="server" ID="pChart" class="grid-section">
        <div id="edit-grid-filter-section" class="chart-title">
            <span>Tables:</span>
            <asp:DropDownList runat="server" ID="ddlTables" CssClass="edit-grid-dropdown-filter" />
            <div class="status-legend">
                <span><img src="../images/green-gear-icon.png" />  Needs GM Approval  ></span>
                <span><img src="../images/blue-gear-icon.png"/>  Needs Verification Admin Approval  ></span>
                <span><img src="../images/orange-gear-icon.png"/>  Needs Executive Approval  ></span>
                <span><img src="../images/red-gear-icon.png"/>  Needs Payroll Admin Approval  ></span>
                <span><img src="../images/workflow-complete.png"/>  Completed</span>
            </div>
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
        <asp:HyperLink runat="server" ID="hypAddRecord" CssClass="add-record" Text="Add Record" NavigateUrl="javascript:void(0)" onclick="ShowEditDialog(this,'open'); return false;" style="display:none;" />
        <asp:HyperLink runat="server" ID="hypViewDocuments" CssClass="get-documents" Text="View/Edit Documents" NavigateUrl="javascript:void(0)" onclick="ShowDocumentLibrary();" style="display:none;" />
        <asp:HyperLink runat="server" ID="hypCreateICS" CssClass="create-ics" Text="Create Appointment" NavigateUrl="javascript:void(0)" onclick='ShowCalendarDialog(); return false;' style="display:none;" />
        <br />
        <span class="edit-grid-message" style="display:block; color:#fff;"></span>
    </asp:Panel>
    
    <div id="edit-dialog" title="Edit Data" style="display:none;">
        <div style="height:770px;overflow-y:scroll;padding:2px;float:left;display:inline;white-space:nowrap;" id="edit-detail-section">
             <asp:PlaceHolder runat="server" ID="phEditFields" EnableViewState="false">
             </asp:PlaceHolder>
             <div style="width:98%;text-align:right;padding:3px;">
                 <div id="messageBox" class="ui-widget">
	                <div class="ui-state-highlight ui-corner-all" style="margin-top: 20px; padding: 0 .7em;text-align:left;">
		                <p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span>
		                <label style="white-space:pre-wrap">Test</label></p>
	                </div>
                </div>
                 <a id="lnkSubmitForApproval" href="javascript:void(0)" onclick="javascript:RunQuery(this,'submitapproval');">Submit for Approval</a>
                <a id="lnkOK" href="javascript:void(0)" onclick="javascript:RunQuery(this,'add');">Ok</a>
                 <a id="lnkPrint" href="javascript:void(0)" onclick="javascript:PrintNewHireItem(this);">Print</a>
                <a id="lnkDriverEmail" href="javascript:void(0)" onclick="javascript:ShowEmailDialog(this);" style="display:none;">Driver Notification</a>
             </div>
        </div>
        <div style="float:left;display:inline;padding-left:10px;" id="newhire-comments">
            <div>
                <div class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix" style="padding:5px;">Enter Comment</div>
                 <textarea id="comments" rows="10"  style="width:470px;text-align:left;">
                </textarea>
                <div style="text-align:right;padding-top:5px;">
                    <a href="#" id="add-comment" onclick="javascript:SaveComments(); return false;" style="width:75px;">Add</a>
                </div>
            </div>
            <div style="padding-top:15px;">
                <div class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix" style="padding:5px;">Comments History</div>
                <div id="show-comments" style="width:470px;height:400px; border: 1px solid #000;background-color:#cccccc; padding-left:3px; vertical-align:top; overflow-y:scroll;"></div>
            </div>
        </div>
    </div>
    <span id="edit-mode"></span>
    <asp:TextBox runat="server" ID="txtFieldMapList" style="display:none;" />
    <asp:TextBox runat="server" ID="txtSendToAddress" CssClass="send-to-address" style="display:none;" />
    <asp:Label runat="server" ID="txtOriginatorEmail" CssClass="originator-email" style="display:none;" />
</div>
<iframe id="newhire-print-frame" frameborder="0" width="0" height="0"></iframe>