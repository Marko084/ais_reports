<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDClaimsGrid.ascx.cs" Inherits="AISReports.UserControls.NCDClaimsGrid" %>
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
    .lookup-combobox {width:227px;}
    .ddl-combobox {width:227px;}
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
    .brown-btn { background-image: url('../images/yellow-button.png'); display:inline-block; background-repeat: no-repeat;background-position: center; width:100%; height:15px; color:transparent; }
    .green-btn { background-image: url('../images/green-button.png'); display:inline-block; background-repeat: no-repeat;background-position: center; width:100%; height:15px; color:transparent;}
    .red-btn { background-image: url('../images/red-button.png');display:inline-block; background-repeat: no-repeat;background-position: center; width:100%; height:15px; color:transparent;}
    .orange-btn { background-image: url('../images/orange-button.png');display:inline-block; background-repeat: no-repeat;background-position: center; width:100%; height:15px; color:transparent;}
    .blue-btn { background-image: url('../images/blue-button.png');display:inline-block; background-repeat: no-repeat;background-position: center; width:100%; height:15px; color:transparent;}
    .pink-btn { background-image: url('../images/pink-button.png'); display:inline-block; background-repeat: no-repeat;background-position: center; width:100%; height:15px; color:transparent;}
    .approved-btn { background-image: url('../images/green_check.png'); background-repeat: no-repeat; display:inline-block; background-position: center; width:100%;height:20px;color:transparent;}
    .pink-status-btn {color:transparent; }
    .none-status {color:transparent;}
     .status-legend {float:right; height:25px; padding-right:15px;}
    .blue-exclamation-mark {
        display: inline-block;
        height:15px;
        background-repeat: no-repeat;
        background-position: center;
        width: 100%;
        color: transparent;
        background-image: url('../images/blue-exclamation-icon.png');
    }
    .green-exclamation-mark {
        display: inline-block;
        height: 15px;
        background-repeat: no-repeat;
        background-position: center;
        width: 100%;
        color: transparent;
        background-image: url('../images/green-exclamation-icon.png');
    }

    .yellow-exclamation-mark {
        display: inline-block;
        height:15px;
        background-repeat: no-repeat;
        background-position: center;
        width: 100%;
        color: transparent;
        background-image: url('../images/yellow-exclamation-icon.png');
    }

    .down-arrow {
        display: inline-block;
        cursor:pointer;
        background-color:#00a4e2;
        border:1px solid #fff;
        border-radius:2px;
        height:16px;
        vertical-align:bottom;
        position: relative;
        top: -3px;
    }
    .down-arrow span{
        display:block;
        width: 0px;
        height: 0px;
        border-left: 10px solid transparent;
        border-right: 10px solid transparent;
        border-top: 10px solid #fff;
    }

    .claims-dropdown {width:204px;}
    .cr-dropdown {width:231px;}
    .red-exclamation-mark { display:inline-block; height: 15px;background-repeat: no-repeat;background-position: center; width:100%; color:transparent;
                            background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAPCAMAAADarb8dAAAABGdBTUEAALGPC/xhBQAAAAFzUkdCAK7OHOkAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAASxQTFRFAAAAvb7Avb7AuX13t1xVtlNMtlRNt19YuouGvb7AuYR/tkxDtlRMupKOuXt1tTcutT83uoiDuo+LtToxtUY9vKqovb7At1hRt2VevK2rtkpCvb7AuX54vLKwuHJqu5+ZuYB6vLWzvLGwtTowtkxEvb7At1tTuGliupaStT01tkpBvLCvuYB6tTsytUQ7uo+KuoyHtlJKt1pTu5yYuo2It2Jbt1hQt1lRt2Zfu5iUvb7AtC4lz3pz897b8NXRxmFYtC8mvUc+/vv7////9+rotTIozXVtv01Eym1kvEY8tTcuukA2/vz89ubk7czJ36WgtDIp1ImCxmJawE5F+/TytjMptDQqtTAm+vLw7c3J8tvY5LSvyWlhwlZNtzgu7czItjUs8NfT5rm0tDMq7MM1xQAAADt0Uk5TAAAOaazLx6NYBl3s3D54/vlORf3yIQHPoS70CWIzc0NgMCn+8QfGlzr77Bll/fNASuDMLlWYtrOOQwIQTUE5AAAAAWJLR0RDZ9ANYgAAAAlwSFlzAAALEwAACxMBAJqcGAAAAMJJREFUCNctj2lDQWEQhUcR11KylEiizZpdMtRlrl1lyRaK/P//0Dvv7XyY58yZL2cAhA4OjaYjs0UBXQarDau1l1fV7tCD4xOsN4i0JjpPeXe5EVsk1Eb0eAHOzhGxw0FXGB/ARU+wPyB6E0R/AC6Z+E70wRwG4UoGI6IxUw3BtQwm9DllzsIQuWEzXyzl4fYODPfSfa0kHkSPaIzdesMznuBmyRTit/Yj9se03j2TVbe7X9zn8v/fgVIolp7KzxX2f8RfKoqIlSIdAAAAYnRFWHRjb21tZW50AGJvcmRlciBiczowIGJjOiMwMDAwMDAgcHM6MCBwYzojMDAwMDAwIGVzOjAgZWM6IzAwMDAwMCBjazphNjEzOWE1MmNkM2FiNmE0M2Q5MDM1MTg3NTQzOTAyNiI6ldMAAAAldEVYdGRhdGU6Y3JlYXRlADIwMTQtMTItMDhUMTE6MjA6MTUrMDA6MDDCf8A8AAAAJXRFWHRkYXRlOm1vZGlmeQAyMDE0LTEyLTA4VDExOjIwOjE1KzAwOjAwsyJ4gAAAABl0RVh0U29mdHdhcmUAQWRvYmUgSW1hZ2VSZWFkeXHJZTwAAAAASUVORK5CYII=);
}

    #show-comments{color:#000;}
    #show-cr-comments{color:#000;}
    #frmClaimsDocuments {background:#fff url('../images/clear-sm.png') 50% 50% repeat !important;}
    #ctl00_ContentPlaceHolder1_ctl00_txtPayeeAddress, #ctl00_ContentPlaceHolder1_ctl00_txtPayeeCityStateZip { width:300px !important;}
</style>
<asp:Literal runat="server" ID="litScript" EnableViewState="false" />
<script type="text/javascript" src="../script/jquery.currency.js"></script>
<script type="text/javascript" src="../script/ncd.multi-field-populate.js"></script>
<script type="text/javascript">
    var editMode;
    var identityField = "";
    var sid;
    var mappedFieldList = "";
    var redExclamationMarkImg = "<img width='8' height='22' title='' alt='' src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAgAAAAWCAMAAADKDS1SAAAABGdBTUEAALGPC/xhBQAAAAFzUkdCAK7OHOkAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAKVQTFRF/////v7/+evq993e/v/+8Kur1RUV1BMS65aX4Vxc1BIS2zw831FR2zc34WFh3URD///+53p64FZW7JeX5G5u8bW06YyL9tLS76qr+/Hw1BQV9cjJ/v792Coq+ebm30xN1Rsa/fv65W5u2jg3/v7+65eX421t8rq67Z2d+/Dw//7+65iY1BIT1RIS3U1M3lBQ/fj46Y6P3EJC/v3976ys65OT+unqSi7BrwAAAAFiS0dEAIgFHUgAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAB3SURBVAjXVc7XDsJADETRBTaUS+i9hLqQEHr9/09D9j4kmZc5kmXZxmQplStWEVRrdWnbgKYihJaiDZ2uqAd9xQCGujWCsWICU8VszkKxjFj5G+vN1mO3dx6HozOFuDg5Safny/UmD9k78JDJE16pzN6f7y+38gcN8QdQ/s/F/gAAAGJ0RVh0Y29tbWVudABib3JkZXIgYnM6MCBiYzojMDAwMDAwIHBzOjAgcGM6IzAwMDAwMCBlczowIGVjOiMwMDAwMDAgY2s6YTYxMzlhNTJjZDNhYjZhNDNkOTAzNTE4NzU0MzkwMjYiOpXTAAAAJXRFWHRkYXRlOmNyZWF0ZQAyMDE0LTEyLTA3VDIyOjQ4OjE5KzAwOjAwsfwO0AAAACV0RVh0ZGF0ZTptb2RpZnkAMjAxNC0xMi0wN1QyMjo0ODoxOSswMDowMMChtmwAAAAASUVORK5CYII='/>"
    var selectedCheckRequestLink;

    $(document).ready(function () {

        alert("hello!");
        $(".edit-grid-dropdown-filter").change(function () {
            //alert("here!");
            var pageUrl = window.location.href.split("?")[0];
            var sTable = $(this).find('option:selected').val();
            window.location.href = pageUrl + "?cid=" + cid + "&t=" + sTable;
        });

        if ($("#ctl00_lblCompanyName").text().toLowerCase() == "dms - claims" ||
            $("#ctl00_lblCompanyName").text().toLowerCase() == "ace claims" ||
            $("#ctl00_lblCompanyName").text().toLowerCase() == "coastal carrier") {
            $("#pink-status").removeClass("pink-status-btn");
            $("#brown-status").hide();
        }

        if (cid == "10064" || cid == "10003") {
            $("#pink-status").hide();
            $("#brown-status").hide();
        }

        if ($("#ctl00_lblCompanyName").text().toLowerCase() == "move happy") {
            $("#brown-status >label").text("Acknowledged");
            $("#pink-status >label").text("In Progress");
            $("#blue-status >label").text("Denied");
            $("#orange-status >label").text("Released");
            $("#green-status >label").text("Accounting");
            $("#red-status >label").text("Settled");
        }

        if ($(".grid-section").attr("claims-approval-module") == "false")
            $("#brown-status").hide();

        var isGMModule = $(".user-control-widget div[gm-approval-module='true']").length;

        if (isGMModule == 1) {
            $("#pink-status").hide();
            $("#orange-status").hide();
            $("#red-status").hide();
            $("#blue-status").hide();
            $("#green-status").show();
            $("#brown-status").show();
        }

        $(".accordion").accordion({
            collapsible: true,
            active: false,
            autoHeight: false
        });

        $('#cr-tabs').tabs();

        $("#add-comment").button();

        $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txtUserID").hide();
        $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txtCreatedDate").hide();     
        //$("select")
        //  .each(function () {
        //      $(this).attr("aria-origWidth", $(this).outerWidth())
        //  })
        //  .mouseenter(function () {
        //      $(this).css("width", "auto");
        //  })
        //  .bind("blur change", function () {
        //      $(this).css("width", $(this).attr("aria-origWidth"));
        //  });

        if (cid == "10122") {
            CreateDropDownBox($("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txtValuation"), "");
            CreateDropDownBox($("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txtClaimType"), "");
            $(".docs-module-btn").show();
            $(".docs-module-btn").button();
        }

    });

    function setAutoComplete(obj, responseName) {
        $(obj).autocomplete({
            source: getResponseList(responseName),
            minLength: 0,
            select: function (e,ui) {
                $(this).autocomplete("close");

                if ($(this).attr("data-field-name") == "RegistrationNumber" &&
                    $("div[grid-type='claims']").attr("multipicklistmap")) {
                    MultiControlPickList($(this),ui.item.value);
                }
            }
        }).click(function () {
            $(this).autocomplete("search");
            
        });

        if ($(obj).is("[picklist-field-name]") &&
            $(obj).attr("picklist-field-name") == "drivername" && (cid == "10003" || cid == "10064")) {
            //CreateDropDownBox(obj,"claims-dropdown");
        }
        
    }

    function CreateDropDownBox(ele, style) {
        console.log(ele);
        var fieldName = $(ele).attr("data-field-name");
        var tableType = $(ele).attr("data-table-type");
        var controlValue = $(ele).val();

        var valuationList = ["#.60/lb", "ACV", "FVP", "MI"];
        var claimTypeList = ["BILLING", "COMPLAINT","HHG", "MISSING", "PROPERTY DAMAGE", "THEFT","3RD PARTY"];
        var currentList;

        if (fieldName == "Valuation") {
            currentList = valuationList;
        }
        else if (fieldName = "ClaimType") {
            currentList = claimTypeList;
        }

        try {
                var ddl = $("<select id=cbo"+fieldName+" class='" + style + "' />");
                var options = [];

                options.push('<option value=""></option>');

                if ($(ele).next().length && $(ele).next().is("select")) {
                    ddl = $(ele).next();
                }

                $.each(currentList, function (index, item) {
                    options.push('<option value="'+item+'">'+item+'</option>');
                });

                $(ddl).html(options.join(''));
                $(ele).after(ddl);
                $(ele).hide();

                if ($(ele).val() !== "") {
                    $(ddl).val($(ele).val());
                }

                $(ddl).change(function () {
                    $(ele).val($(this).val());
                });
            }
        catch (e)
        { console.log(e); }


       // if (typeof fieldName === 'undefined') {
           // fieldName = "drivername";
           // tableType = "drivers";
        //}

        //var queryUrl = "../ListHandler.ashx?cid=" + cid + "&uid=" + uid + "&fn=" + fieldName + "&tn=" + tableType + "&qt=lookup&qid=" + S4();

        //$.ajax({
        //    type: "GET",
        //    contentType: "json",
        //    url: queryUrl,
        //    cache: false,
        //    success: function (data) {
        //        try {
        //            console.log(queryUrl);
        //            var ddl = $("<select class='" + style + "' />");
        //            var options = [];

        //            options.push('<option value=""></option>');

        //            if ($(ele).next().length && $(ele).next().is("select")) {
        //                ddl = $(ele).next();
        //            }

        //            $.each(data, function (index, item) {
        //                options.push('<option value="',
        //                    item.label, '">',
        //                    item.value, '</option>');
        //            });

        //            $(ddl).html(options.join(''));
        //            $(ele).after(ddl);
        //            $(ele).hide();

        //            if ($(ele).val() !== "") {
        //                $(ddl).val($(ele).val());
        //            }

        //            $(ddl).change(function () {
        //                $(ele).val($(this).val());
        //            });
        //        }
        //        catch (e) { console.log(e);}
        //    },
        //    error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
        //});

    }

    function SetCheckRequestPickLists() {
        var isGMModule = $(".user-control-widget div[gm-approval-module='true']").length;

        if (isGMModule == 0) {
            $("#cr-tabs-fieldlist input[class*='aria-pick-list']").each(function (idx, ele) {
                var pickListName = $(ele).attr("aria-pick-list-name");
                //if (!$(ele).hasClass("ui-autocomplete-input")) {
                $(ele).autocomplete({
                    source: getPickList(pickListName),
                    minLength: 0,
                    select: function () {
                        $(this).autocomplete("close");
                    }
                }).click(function () {
                    $(this).autocomplete("search");
                });
                //}
            });
        }


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

    function GetFieldList(dMode, fieldType) {
        var fieldlist = "";
        var fieldindex = 0;
        var mapKeyFieldName = "";
        var mapTableName = "";
        mappedFieldList = "";
       // alert("here!!")
        if ($("#ctl00_ContentPlaceHolder1_ctl00_txtFieldMapList").attr("aria-map-key-field")) {
            mapKeyFieldName = $("#ctl00_ContentPlaceHolder1_ctl00_txtFieldMapList").attr("aria-map-key-field");
            mapTableName = $("#ctl00_ContentPlaceHolder1_ctl00_txtFieldMapList").attr("aria-map-table-name");
            mappedFieldList = $("#ctl00_ContentPlaceHolder1_ctl00_txtFieldMapList").val();
        }

        var claimsDiv;

        if (fieldType == "approval") {
            claimsDiv = $("#edit-approval-section")
        }
        else {
            claimsDiv = $("#edit-detail-section")
        }

        $(claimsDiv).find(".response-field").each(function () {

            if ($(this).attr("aria-identity-column") == "true") {
                identityField = $(this).attr("data-field-name");
            }

            if ($(this).attr("aria-identity-column") == "false") {
                var responseListName = $(this).attr("response-list-type");

                if (responseListName != 'undefined') {
                    setAutoComplete($(this), responseListName);
                }

                if ($(this).attr("aria-data-type").indexOf('date') != -1) {
                    //alert(typeof $(".grid-section").attr("createics")!='undefined')
                    if (typeof $(".grid-section").attr("createics") != 'undefined') {
                        if ($(".grid-section").attr("createics").indexOf($(this).attr("data-field-name").toLowerCase()) != -1) {
                            $(this).datepicker({
                                onSelect: function (dateText, inst) {
                                    ShowCalendarDialog(dateText);
                                }
                            });
                        }
                        else {
                            $(this).datepicker();
                        }
                    }
                    else {
                        $(this).datepicker();
                    }
                }

                if (($(this).attr("aria-data-type") == "numeric" ||
                     $(this).attr("aria-data-type") == "decimal" ||
                     $(this).attr("aria-data-type") == "int") && $(this).val() == "") {

                    if ($(this).attr("id").toLowerCase().indexOf("companyid") != -1) {
                        if (cid == "10025") {
                            $(this).val("1");
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
                        tableType = $(this).attr("picklist-table-type");
                        //fieldName = fieldName + "|" + $(this).attr("picklist-field-name");
                        //tableType = tableType + "|" + $(this).attr("picklist-table-type");
                    }

                    $(this).autocomplete({
                        source: "../ListHandler.ashx?cid=" + cid + "&uid=" + uid + "&fn=" + fieldName + "&tn=" + tableType + "&qt=lookup&qid=" + S4(),
                        minLength: 1
                    });

                    if (mapKeyFieldName != "" && mapKeyFieldName.toLowerCase() == $(this).attr("data-field-name").toLowerCase()) {

                        $(this).on("autocompleteselect", function (e, ui) {

                            if (dMode == "") {

                                var NewJSONDBQuery = {};
                                NewJSONDBQuery.FieldList = GetMappedField("", "list");
                                NewJSONDBQuery.TableName = mapTableName;
                                NewJSONDBQuery.KeyFieldName = GetMappedField(mapKeyFieldName, "destination");
                                NewJSONDBQuery.KeyFieldValue = ui.item.value;
                                NewJSONDBQuery.QueryType = "edit";

                                var DTO = { 'NewJSONDBQuery': NewJSONDBQuery };
                                GetMappedData(DTO, "edit");

                            }
                        });
                    }

                    if (typeof fieldName != 'undefined') {
                        if (fieldName.toLowerCase() == "amount1117" || fieldName.toLowerCase() == "amountchargedtopvo" || fieldName.toLowerCase() == "amountchargedtocompany") {
                            $(this).keyup(function () {

                                var totalAmtPaid = parseFloat($("input[data-field-name='AmountChargedToPVO']").val()) + parseFloat($("input[data-field-name='AmountChargedToCompany']").val()) + parseFloat($("input[data-field-name='Amount1117']").val());

                                $("input[data-field-name='TotalClaimPaidAccuracy']").val(totalAmtPaid);

                                if (parseFloat($("input[data-field-name='TotalClaimPaid']").val()) != totalAmtPaid) {
                                    $(".ui-dialog").find(".ui-widget-header").css("background", "red");
                                }
                                else {
                                    $(".ui-dialog").find(".ui-widget-header").css("background", "gray");
                                }
                            });
                        }
                    }

                }

                var fn = $(this).attr("id").replace("ctl00_ContentPlaceHolder1_ctl00_", "").replace("ContentPlaceHolder1_ctl00_", "").replace("txt", "");
                var fv = $(this).val();

                if ($(this).attr("aria-data-type") == "numeric") {
                    fv = fv.replace("$", "");
                }

                if (fn == "RecID") {
                    fv = $("#lnkApprovalOK").attr("aria-claim-id");
                    fieldlist = fieldlist + "|"+fn + "~" + fv;
                }
                else if ($(this).attr("aria-identity-column") != "true" && typeof $(this).attr("aria-indicator-column") == 'undefined') {
                    if ($(this).attr("aria-computed-column") == "true" && (dMode == "save" || dMode == "add")) {
                        //Don't do anything.  we cannot save data to a computed column.
                    }
                    else if ($(this).attr("aria-indicator-column") == 'true') {
                        //Don't do anything.  we cannot save data to a indicator column.
                    }
                    else if (fieldindex == 0) {
                        //fieldlist = BuildFieldList(fieldlist, fn, fv);
                        //fieldlist = fieldlist + $(this).attr("id").replace("ContentPlaceHolder1_ctl00_", "").replace("txt", "") + "~" + $(this).val();
                        fieldlist = fieldlist + fn + "~" + fv;
                    }
                    else {
                        //fieldlist = BuildFieldList(fieldlist, fn, fv);
                        //fieldlist = fieldlist + "|" + $(this).attr("id").replace("ContentPlaceHolder1_ctl00_", "").replace("txt", "") + "~" + $(this).val();
                        fieldlist = fieldlist + "|" + fn + "~" + fv;
                    }
                }
                ++fieldindex;
            }
        });

        return fieldlist.replace(/ctl00_ContentPlaceHolder1_ctl00_/gi, "").replace(/ContentPlaceHolder1_ctl01_/gi, "").replace(/ctl00_/gi, "");
    }

    function BuildFieldList(fl, fName, fValue) {
        var flArray = fl.split("|");
        var result = "";
        //var currentValue="";

        if (flArray.length == 1) {
            result = fName + "~" + fValue;
        }
        else {

            for (var i = 0; i < flArray.length; i++) {
                var item = flArray[i];
                //alert(item);
                if (item.indexOf(fName) != -1) {
                    item = item + "^" + fValue;
                }

                if (i == 0) {
                    result = item;
                }
                else {
                    result = result + "|" + item;
                }
            }
        }

        return result;
    }

    function EditClaimApproval(obj) {

        editMode = $(obj).text().toLowerCase();

        var tableName = $(".edit-grid-dropdown-filter option:selected").val().toLowerCase().replace("_claims", "_claims_approval");
        var primaryKeyValue = $(obj).attr("rel");
        var primaryKeyName = $(".grid-section").attr("checkrequest-key-field");
        
        if (editMode == "save") {
            SetNewCRCreatedToFalse();
        }

        sid = primaryKeyValue;
        console.log($(obj));
        if (typeof $(obj).attr("rel") != 'undefined' && ($(obj).text().toLowerCase() == "save" || $(obj).text().toLowerCase() == "approve" || $(obj).text().toLowerCase() == "delete")) {

            var primaryKeyInfo = "";

            if ($(obj).attr("rel").indexOf("~") > -1) {
            }
            else {
                primaryKeyInfo=$(obj).attr("aria-checkrequest-id");
            }
            primaryKeyName = $(obj).attr("rel").split("~")[0];
            primaryKeyValue = $(obj).attr("rel").split("~")[1];
        }

        if (tableName != "") {
            var NewJSONDBQuery = {};
            var modeDisplay = "";

            var gmApprovalIndicator = "";
            var gmClaimAmount = "";
            var gmGlCodeType = "";
            var gmGlCode = "";
            var gmClaimLocation = "";

            NewJSONDBQuery.FieldList = GetFieldList(editMode, "approval");
            NewJSONDBQuery.TableName = tableName;
            NewJSONDBQuery.KeyFieldName = primaryKeyName;
            NewJSONDBQuery.KeyFieldValue = primaryKeyValue;
            NewJSONDBQuery.QueryType = editMode.split(" ")[0];
            NewJSONDBQuery.UserID = uid;

            CheckForIdentityField();

            if (identityField.toLowerCase() == primaryKeyName.toLowerCase()) {
                NewJSONDBQuery.KeyFieldIsIdentity = "true";
            }

            $("#lnkOK").html("<span class='ui-button-text'>OK</span>");

            if (editMode == "add" || editMode == "save" || editMode == "approve") {
                var claimGMField = "";
                var newField = false;
                var claimFieldValues = "";
                var driverNames = "";
                var chargeToControlCount = 0;
                var driverControlCount = 0;
                var agentCode = $(".slide-out-div #ctl00_ContentPlaceHolder1_SearchFilter1_txtgmfilter").val();
                var isGMsRecord = false;

                if (editMode == "add") {
                    NewJSONDBQuery.KeyFieldName = "";
                    NewJSONDBQuery.KeyFieldValue = "";
                    modeDisplay = "added";
                }
                else if (editMode == "save") {
                    NewJSONDBQuery.KeyFieldName = primaryKeyName;
                    NewJSONDBQuery.KeyFieldValue = primaryKeyValue;
                    modeDisplay = "updated";
                }
                else if (editMode == "approve") {
                    NewJSONDBQuery.KeyFieldName = primaryKeyName;
                    NewJSONDBQuery.KeyFieldValue = primaryKeyValue;
                    NewJSONDBQuery.QueryType = "save";
                    modeDisplay = "approved";
                }
                else if (editMode == "delete") {
                    NewJSONDBQuery.KeyFieldName = primaryKeyName;
                    NewJSONDBQuery.KeyFieldValue = primaryKeyValue;
                    modeDisplay = "deleted";
                }

                if (editMode == "add" || editMode == "save" || editMode == "approve") {
                    $("#edit-approval-section [id='ddlChargeToAccount'],[id='ddlCompany'],[id='ddlAuthorizedBy'],[id='txtDriverName'],[id='ddlAuthorizedBy2']").each(function () {

                        if ($(this).attr("id").replace("ddl", "") == "Company") {
                            NewJSONDBQuery.FieldList = NewJSONDBQuery.FieldList + "|Company~" + $(this).val();
                        }
                        else if ($(this).attr("id").replace("ddl", "") == "AuthorizedBy") {
                            NewJSONDBQuery.FieldList = NewJSONDBQuery.FieldList + "|AuthorizedBy~" + $(this).find("option:selected").text();
                        }
                        else if ($(this).attr("id").replace("ddl", "") == "AuthorizedBy2") {
                            NewJSONDBQuery.FieldList = NewJSONDBQuery.FieldList + "|AuthorizedBy2~" + $(this).find("option:selected").text();
                        }
                        else if ($(this).attr("id").replace("ddl", "") == "ChargeToAccount") {

                            if ($(this).hasClass("claimsgmlocation")) {
                                gmClaimLocation = $(this).val();
                            }
                            else if ($(this).hasClass("glcode")) {
                                gmGlCode = $(this).val();
                            }
                            else if ($(this).hasClass("glcodetype")) {
                                gmGlCodeType = $(this).val();
                            }
                            else if ($(this).hasClass("gm-approval-indicator")) {
                                gmApprovalIndicator = $(this).text();
                                
                                if ($.trim(gmApprovalIndicator.toLowerCase()) == "napprove") {
                                    gmApprovalIndicator = "N";
                                }
                            }
                            else if ($(this).hasClass("gmclaimamount")) {
                                gmClaimAmount = $(this).val();
                            }

                            if (editMode == "approve") {
                                if (chargeToControlCount == 4) {
                                    if (isGMsRecord) {
                                        claimFieldValues = claimFieldValues + gmGlCode + "^" + gmGlCodeType + "^" + gmClaimLocation + "^" + gmClaimAmount + "^" + agentCode + "?";
                                        isGMsRecord = false;
                                    }
                                    else {
                                        claimFieldValues = claimFieldValues + gmGlCode + "^" + gmGlCodeType + "^" + gmClaimLocation + "^" + gmClaimAmount + "^" + gmApprovalIndicator + "?";
                                    }

                                    chargeToControlCount = -1;

                                }
                            }
                            else {
                                if (chargeToControlCount == 4) {
                                    claimFieldValues = claimFieldValues + gmGlCode + "^" + gmGlCodeType + "^" + gmClaimLocation + "^" + gmClaimAmount + "^" + gmApprovalIndicator + "?";
                                    chargeToControlCount = -1;
                                }
                            }

                            chargeToControlCount++;
                        }
                        else if ($(this).attr("id").replace("txt", "") == "DriverName") {
                            
                            if (driverNames == "") {
                                driverNames = $(this).val();
                            }
                            else if (driverControlCount == 2) {
                                driverNames = driverNames + "?" + $(this).val();
                                driverControlCount = 0;
                            }
                            else {
                                driverNames = driverNames + "^" + $(this).val();
                            }

                            driverControlCount++;
                        }
                    });
                }

                if (claimFieldValues != "") {
                    // alert(claimFieldValues);
                    NewJSONDBQuery.FieldList = NewJSONDBQuery.FieldList + "|ChargeToAccount~" + claimFieldValues.substring(0, claimFieldValues.length - 1);
                }

                if (driverNames != "") {
                    NewJSONDBQuery.FieldList = NewJSONDBQuery.FieldList + "|DriverName~" + driverNames;
                }

                //alert(NewJSONDBQuery.FieldList);
            }
            else if (editMode == "delete") {
                modeDisplay = "deleted";
                var completeDelete = confirm('Are you sure you want to delete?');

                if (!completeDelete) {
                    return true;
                }
            }
            else if (editMode == "view check requests" ||
                     editMode == "check request") {

                NewJSONDBQuery.QueryType = "edit";
                modeDisplay = "get";
                $("#lnkApprovalOK").html("<span class='ui-button-text'>Add</span>");
                $("#lnkApprovalOK").attr("rel", primaryKeyValue);
                $("#lnkDeleteClaim").attr("rel", primaryKeyValue);
                $("#lnkDeleteClaim").hide();
                ClearFields();
                ClearTabs();
                ShowClaimApprovalDialog($(obj), primaryKeyName, primaryKeyValue);
                LoadCRComments(false);
                SetCheckRequestPickLists();

                $("#ctl00_ContentPlaceHolder1_ctl00_txtShipperName").val($(obj).attr("aria-cr-shippername"));
                $("#ctl00_ContentPlaceHolder1_ctl00_txtNationalAccount").val($(obj).attr("aria-natl-acct"));

                var recIDField = $("#ctl00_ContentPlaceHolder1_ctl00_txtRecID");
                $(recIDField).val($(obj).attr("aria-claim-id"));

                $(recIDField).parent().hide();
                
                return;
            }
            else if (editMode == "edit") {
                modeDisplay = "get";
                $("#lnkApprovalOK").html("<span class='ui-button-text'>Save</span>");
                $("#lnkApprovalOK").attr("rel", primaryKeyValue);
                ClearFields();
                LoadCRComments(false);
            }

            var DTO = { 'NewJSONDBQuery': NewJSONDBQuery };

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
                                //alert(item);
                                var fieldName = item.split("~")[0];
                                var fieldValue = item.split("~")[1].replace("1/1/1900 12:00:00 AM", "").replace("12:00:00 AM", "");

                                //if (fieldName.toLowerCase() == "shippername" && fieldValue.length == 0) {
                                //    $("#ContentPlaceHolder1_ctl00_txt" + fieldName).val($(obj).attr("aria-shippername"));
                                //}
                                //else {
                                $("#ctl00_ContentPlaceHolder1_ctl00_txt" + fieldName).val(fieldValue);
                                //}

                                if (editMode == "view") {
                                    $("#ctl00_ContentPlaceHolder1_ctl00_txt" + fieldName).attr("disabled", "disabled");
                                }
                            }
                            catch (e) {
                                alert(e.Message);
                            }
                        });

                        $("#ctl00_ContentPlaceHolder1_ctl00_txt" + NewJSONDBQuery.KeyFieldName).val(NewJSONDBQuery.KeyFieldValue);

                    }
 
                    if (modeDisplay != "get") {

                        if (modeDisplay != "deleted" && modeDisplay != "" && modeDisplay != null) {
                            alert("Data was successfully " + modeDisplay + ".");
                        }

                        if (editMode == "approve") {
                            ReloadGrid(false);
                        }
                        else {
                            ReloadGrid(true);
                        }
                    }
                },
                error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
            });
        }

    }

    function GetClaimsData(DTO, qType) {
        var record;
        var isGMModule = $(".user-control-widget div[gm-approval-module='true']").length;
        var gmLocationCode = "";
        var claimID = $(selectedCheckRequestLink).attr("aria-claim-id");

        if (isGMModule > 0) {
            gmLocationCode = $("#ctl00_ContentPlaceHolder1_SearchFilter1_txtgmfilter").val().replace(/\$/g, "|");
        }

        if (gmLocationCode != "" && gmLocationCode.indexOf("[") > -1)
        {
            var prefix = "";
            var locationList = gmLocationCode.split('[');

            gmLocationCode = locationList[0] + locationList[1].substring(0, 1) + locationList[2].substring(0, 1) +
                             "|" + locationList[0] + locationList[1].substring(1, 2) + locationList[2].substring(1, 2);
        }
                
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/JSONDBQuery",
            data: JSON.stringify(DTO),
            dataType: "json",
            beforeSend: function () { },
            success: function (data) {

                record = data.d.FieldList.split("|");
                var checkRequestIDList = [];
                var currentClaimImportID;

                $.each(record, function (i, item) {
                    if (item.split("~")[0] == "RecID") {
                        if (item.split("~")[1] == claimID){
                            checkRequestIDList.push({ImportID: currentClaimImportID, RecID: item.split("~")[1] });
                        }
                    }

                    if (item.split("~")[0] == "ImportID") {
                        currentClaimImportID = item.split("~")[1];
                    }
                    
                });
                console.log(checkRequestIDList);
               // $.each(checkRequestIDList, function (i, item) {
                    //console.log(item);
                    if (qType == "tabs") {
                        BuildTabs(checkRequestIDList, isGMModule);
                        SetCRTabNames();
                    }
                    else if (qType == "getcheckrequest") {
  
                        $.each(record, function (index, item) {
                            try {

                                var fieldName = item.split("~")[0];
                                var fieldValue = item.split("~")[1].replace("1/1/1900 12:00:00 AM", "").replace("12:00:00 AM", "");
                                var rowCount = 0;
                                var rows;
                                var columnIdx;
                                var rowIdx;
                                var cols;

                                if (fieldName == "DriverName") {
                                    rows = fieldValue.split("?");
                                    columnIdx = 0;
                                    rowIdx = 0;

                                    for (j = 0; j < rows.length; j++) {
                                        cols = rows[j].split("^");
                                        //alert(cols.toString());

                                        if (j > 0) {
                                            $("#cr-tabs-fieldlist #lnkAddDriver").click();
                                        }

                                        $("#cr-tabs-fieldlist input[id='txtDriverName']").each(function (idx, ele) {

                                            if ($(ele).val() == "") {
                                                $(ele).val(cols[columnIdx]);
                                            }

                                            //if ((idx % 2) == 0) {
                                            //    CreateDropDownBox(ele, "cr-dropdown");
                                            //}

                                            columnIdx++;

                                            if (columnIdx == 2) {
                                                columnIdx = 0;
                                            }

                                            if (isGMModule == 1) {
                                                $(ele).attr("disabled", "disabled");
                                                //$(ele).next().attr("disabled", "disabled");
                                            }
                                        });
                                    }

                                }
                                else if (fieldName == "ChargeToAccount") {
                                    rows = fieldValue.split("?");
                                    columnIdx = 0;
                                    rowCount = 0;

                                    for (i = 0; i < rows.length; i++) {
                                        cols = rows[i].split("^");

                                        if (i > 0) {
                                            $("#cr-tabs-fieldlist #lnkChargeToAdd").click();
                                        }
                                        var gmClaimLocation = cols[2];
                                        var gmClaimAmount = cols[3];

                                        $.each(cols, function (idx, rec) {
                                            $("#cr-tabs-fieldlist select[id='ddlChargeToAccount'],input[id='ddlChargeToAccount'],span[id='ddlChargeToAccount']").each(function (ix, ele) {
                                                if (ix == columnIdx) {
                                                    if ($(ele).is("span")) {
                                                        $(ele).text(rec);

                                                        if (rec != "N") {
                                                            $(ele).show();
                                                            $(ele).addClass("approved-btn");
                                                        }
                                                        else if (gmLocationCode != "" && $(ele).prev().length && $(ele).prev().val().indexOf(gmLocationCode) > -1) {

                                                            var checkRequestLocationCode = $.trim($(ele).prev().val().split("-")[0]);

                                                            $(ele).append("<a href='javascript:void(0)' rel='" + checkRequestLocationCode + "' class='gm-approval-btn' onclick='SetCheckRequestAsViewed(this);GMApproveCharge(this);'>Approve</a>");
                                                            $(ele).show();
                                                            $(ele).addClass("none-status");
                                                        }
                                                        else if (gmLocationCode != "" && $(ele).prev().length && gmLocationCode.split("|").length > 1 &&
                                                            jQuery.inArray($.trim($(ele).prev().val().split("-")[0]), gmLocationCode.split("|")) > -1) {

                                                            var checkRequestLocationCode = $.trim($(ele).prev().val().split("-")[0]);

                                                            console.log("GM Filter: " + gmLocationCode);
                                                            console.log("Location Code: " + $.trim($(ele).prev().val().split("-")[0]));
                                                            console.log(jQuery.inArray($.trim($(ele).prev().val().split("-")[0]), gmLocationCode.split("|")));
                                                            //($(ele).prev().val().indexOf(gmLocationCode.split("|")[0]) > -1 || $(ele).prev().val().indexOf(gmLocationCode.split("|")[1]) > -1))
                                                            $(ele).append("<a href='javascript:void(0)' rel='" + checkRequestLocationCode + "' class='gm-approval-btn' onclick='SetCheckRequestAsViewed(this);GMApproveCharge(this);'>Approve</a>");
                                                            $(ele).show();
                                                            $(ele).addClass("none-status");
                                                        }
                                                        else {
                                                            $(ele).hide();
                                                        }
                                                    }
                                                    else {
                                                        if ($(ele).hasClass("gmclaimamount")) {
                                                            $(ele).val(gmClaimAmount);
                                                        }
                                                        else if ($(ele).hasClass("claimsgmlocation")) {
                                                            $(ele).val(gmClaimLocation);
                                                        }
                                                        else {
                                                            $(ele).val(rec);
                                                        }
                                                    }

                                                    if (isGMModule == 1) {
                                                        $(ele).attr("disabled", "disabled");
                                                    }
                                                    columnIdx++;
                                                    return false;
                                                }

                                            });
                                        });

                                        rowCount++;
                                    }
                                }
                                else {
                                    $("#cr-tabs-fieldlist #ctl00_ContentPlaceHolder1_ctl00_txt" + fieldName).val(fieldValue);

                                    if ($("#cr-tabs-fieldlist #ctl00_ContentPlaceHolder1_ctl00_txt" + fieldName).next().length && $("#cr-tabs-fieldlist #ctl00_ContentPlaceHolder1_ctl00_txt" + fieldName).next().is("select")) {
                                        $("#cr-tabs-fieldlist #ctl00_ContentPlaceHolder1_ctl00_txt" + fieldName).next().val(fieldValue);
                                    }

                                    if (isGMModule == 1) {
                                        $("#cr-tabs-fieldlist #ctl00_ContentPlaceHolder1_ctl00_txt" + fieldName).attr("disabled", "disabled");
                                    }

                                    $("#cr-tabs-fieldlist #ddl" + fieldName).val(fieldValue);

                                    if (isGMModule == 1) {
                                        $("#cr-tabs-fieldlist #ddl" + fieldName).attr("disabled", "disabled");
                                        $("#cr-tabs-fieldlist #lnkChargeToAdd").hide();
                                        $("#cr-tabs-fieldlist #lnkAddDriver").hide();
                                    }

                                    if (fieldName == "NationalAccount") {
                                        $("#cr-tabs-fieldlist #ctl00_ContentPlaceHolder1_ctl00_txt" + fieldName).val("");
                                        $("#cr-tabs-fieldlist #ctl00_ContentPlaceHolder1_ctl00_txt" + fieldName).val($("#lnkCheckRequest").attr("aria-natl-acct"));
                                    }

                                }
                            }
                            catch (e) {
                                alert(e.Message);
                            }
                        });

                        SetCheckRequestPickLists();
                        LoadCRComments(false);
                        SetCommentsAsRead(true);

                        //if ($(ele).attr("aria-data-type") == "currency") {
                        //    $(ele).currency();
                        //}
                    }

                //});
                
                $("#cr-tabs-fieldlist div> #ctl00_ContentPlaceHolder1_ctl00_txtApprovedBy").focus();
                $("#cr-tabs-fieldlist input[aria-data-type='currency']").currency();
                //$("#cr-tabs-fieldlist input[aria-data-type='numeric']").currency();
            },
            error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
        });

        return record;
    }

    function RunQuery(obj) {

        editMode = $(obj).text().toLowerCase();

        var tableName = $(".edit-grid-dropdown-filter option:selected").val();
        var primaryKeyValue = $(obj).attr("rel");
        var primaryKeyName = $(".grid-section").attr("primary-key-field");

        //if ($(obj).text().toLowerCase() == "view claim") {
        //    primaryKeyValue = $(obj).attr("aria-claim-id");
        //}

        sid = primaryKeyValue;

        if (tableName != "") {
            var NewJSONDBQuery = {};
            var modeDisplay = "";

            NewJSONDBQuery.FieldList = GetFieldList(editMode, "claim");
            NewJSONDBQuery.TableName = tableName;
            NewJSONDBQuery.KeyFieldName = primaryKeyName;
            NewJSONDBQuery.KeyFieldValue = primaryKeyValue;
            NewJSONDBQuery.QueryType = editMode.split(" ")[0];
            NewJSONDBQuery.UserID = uid;

            CheckForIdentityField();
            //alert(identityField);
            if (identityField.toLowerCase() == primaryKeyName.toLowerCase()) {
                NewJSONDBQuery.KeyFieldIsIdentity = "true";
            }

            $("#lnkOK").html("<span class='ui-button-text'>OK</span>");

            if (editMode == "add record" || editMode == "add") {
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
            else if (editMode == "view" || editMode == "view claim") {
                NewJSONDBQuery.QueryType = "edit";
                modeDisplay = "get";
                $("#lnkOK").html("<span class='ui-button-text'>Save</span>");
                $("#lnkOK").attr("rel", primaryKeyValue);
                ClearFields();
                LoadComments();
            }
            else if (editMode == "edit") {
                modeDisplay = "get";
                $("#lnkOK").html("<span class='ui-button-text'>Save</span>");
                $("#lnkOK").attr("rel", primaryKeyValue);
                ClearFields();
                LoadComments();
            }


            var DTO = { 'NewJSONDBQuery': NewJSONDBQuery };
            
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../AISWS.asmx/JSONDBQuery",
                data: JSON.stringify(DTO),
                dataType: "json",
                beforeSend: function () { },
                success: function (data) {

                    var lastName = "";
                    var regNo = "";
                    
                    if (modeDisplay == "get") {
                        var record = data.d.FieldList.split("|");
                        console.log("data: "+record)
                        $.each(record, function (index, item) {
                            try {
                                //alert(item);
                                var fieldName = item.split("~")[0];
                                var fieldValue = item.split("~")[1].replace("1/1/1900 12:00:00 AM", "").replace("12:00:00 AM", "");

                                if (fieldName.toLowerCase() == "registrationnumber") {
                                    regNo = fieldValue;
                                }

                                if (fieldName.toLowerCase() == "lastname") {
                                    lastName = fieldValue;
                                }
                                
                                if (fieldName.toLowerCase() == "pvodriver" && $("#ctl00_ContentPlaceHolder1_ctl00_pChart").attr("show-email-link") == "true") {

                                    var claimID = $("#lnkDriverEmail").attr("aria-claim-id");
                                    $("#lnkDriverEmail").attr("rel", fieldValue);

                                    if ($("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txtPVODriver").next().length && $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txtPVODriver").next().is("select")) {
                                        $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txtPVODriver").next().val(fieldValue.toUpperCase());

                                        if ($("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txtPVODriver").next().length && $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txtPVODriver").next()[0].selectedIndex == -1) {
                                            $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txtPVODriver").append('<option value="' + fieldValue.toUpperCase() + '">' + fieldValue.toUpperCase() + '</option>');
                                            $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txtPVODriver").next().val(fieldValue.toUpperCase());
                                        }
                                    }

                                    if ($("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txtPVODriver").prev().length && $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txtPVODriver").prev().find("a").length == 0) {
                                        $("<a class='driver-link' rel='" + fieldValue + "' aria-claim-id='" + claimID + "' href='javascript:void(0)' onclick='javascript:ShowEmailDialog(this);'>Driver Email</a>").appendTo($("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txtPVODriver").prev());
                                         $(".driver-link").button();
                                    }
                                    else {
                                        if ($("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txtPVODriver").prev().length) {
                                            $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txtPVODriver").prev().find("a").attr("rel", fieldValue);
                                            $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txtPVODriver").prev().find("a").attr("aria-claim-id", claimID);
                                        }
                                    }
                                }

                                $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txt" + fieldName).val(fieldValue);

                                if (cid == 10122) {
                                    if (fieldName == "Valuation" || fieldName == "ClaimType") {
                                        $("#cbo" + fieldName).val(fieldValue);
                                    }
                                }
                                
                                if (editMode == "view") {
                                    $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txt" + fieldName).attr("disabled", "disabled");
                                }

                                if (fieldName == "ClaimNumber" && cid=="10122") {
                                    $(".docs-module-btn").attr("rel", fieldValue);
                                    $(".docs-module-btn").attr("cat-label", "Claim Number");
                                }
                            }
                            catch (e) {
                                alert(e.Message);
                            }
                        });

                        try {
                            var recordTimeStamp = GetTimeStamp();
                            $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txtCreatedDate").val(recordTimeStamp);
                            $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txtUserID").val(uid);
                        }
                        catch (e) { }

                        SetDriverCommentsAsRead();

                        var docSiteUrl = $(".grid-section").attr("document-site-url");

                        if (typeof docSiteUrl != 'undefined') {
                            $(".view-docs-btn").show();
                            $(".view-docs-btn").attr("href", docSiteUrl + "/" + regNo + "-" + lastName);
                        }

                        $("#edit-dialog #ctl00_ContentPlaceHolder1_ctl00_txt" + NewJSONDBQuery.KeyFieldName).val(NewJSONDBQuery.KeyFieldValue);
                    }

                    if (modeDisplay != "get") {

                        if (editMode == "approve") {
                            ReloadGrid(false);
                        }
                        else if (editMode == "save") {
                            ReloadGrid(false);
                            $("#edit-dialog").dialog("destroy");
                        }
                        else {
                            ReloadGrid(true);
                        }

                        if (modeDisplay != "deleted" && modeDisplay != "" && modeDisplay != null) {
                            alert("Data was successfully " + modeDisplay);

                            if (modeDisplay == "added") {
                                $("#edit-dialog").dialog("destroy");
                            }
                        }
                    }
                },
                error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
            });
        }
    }

    function GetMappedField(fieldName, mode) {

        var fieldArray = mappedFieldList.split("|");
        var fieldList = "";
        var result = "";

        $.each(fieldArray, function (i) {
            var source = fieldArray[i].split("=")[0];
            var dest = fieldArray[i].split("=")[1];

            if (mode == "source" && fieldName.toLowerCase() == source.toLowerCase()) {
                result = dest;
            }

            if (mode == "destination" && fieldName.toLowerCase() == dest.toLowerCase()) {
                result = source;
            }

            if (mode == "list") {
                if (fieldList.length == 0) {
                    fieldList = source;
                }
                else {
                    fieldList += "|" + source;
                }
            }
        });

        if (mode == "list") {
            return fieldList;
        }
        return result;
    }

    function GetMappedData(DTO, modeDisplay) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/JSONDBQuery",
            data: JSON.stringify(DTO),
            dataType: "json",
            beforeSend: function () { },
            success: function (data) {

                if (modeDisplay == "edit") {

                    var record = data.d.FieldList.split("|");

                    $.each(record, function (index, item) {
                        try {
                            //alert(item);
                            var fieldName = item.split("~")[0];
                            var fieldValue = item.split("~")[1].replace("1/1/1900 12:00:00 AM", "").replace("12:00:00 AM", "");

                            var destFieldName = GetMappedField(fieldName, "source");

                            if (destFieldName != "") {
                                $("#ctl00_ContentPlaceHolder1_ctl00_txt" + destFieldName).val(fieldValue);
                            }
                        }
                        catch (e) {
                            alert(e.Message);
                        }
                    });

                    //$("#ContentPlaceHolder1_ctl00_txt" + NewJSONDBQuery.KeyFieldName).val(NewJSONDBQuery.KeyFieldValue);
                }
            },
            error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
        });
    }

    function ShowEditDialog(obj) {
        if ($(obj).text().toLowerCase().indexOf("add record") != -1) {
            ClearFields("add");
            selectedCheckRequestLink = null;
            GetFieldList("", "claim");
            $(".docs-module-btn").hide();
        }

        var checkRequestID = $(obj).attr("aria-checkrequest-id");
        
        if (typeof checkRequestID != 'undefined') {
            $("#edit-dialog #lnkCheckRequest").attr("rel", checkRequestID.split("~")[1]);
            $("#edit-dialog #lnkCheckRequest").attr("aria-claim-id", $(obj).attr("rel"));
            $("#edit-dialog #lnkCheckRequest").attr("aria-checkrequest-id", $(obj).attr("aria-checkrequest-id"));
            $("#edit-dialog #lnkCheckRequest").attr("aria-natl-acct", $(obj).attr("aria-natl-acct"));
        }
        else {
            $("#edit-dialog #lnkCheckRequest").hide();
        }

        if ($(".grid-section").attr("show-email-link") == "true") {
            $("#edit-dialog #lnkDriverEmail").attr("aria-claim-id", $(obj).attr("rel"));
        }

        if ($(".user-control-widget div[gm-approval-module='true']").length > 0) {
            $("#edit-dialog #lnkOK").hide();
        }

        //$("#edit-detail-section input[aria-data-type='numeric']").currency();

        $("#edit-dialog").dialog({
            minWidth: 1096,
            minHeight: 800,
            modal: true,
            zIndex: 9000,
            open: function () {
                setTimeout("setEditBackground()", 300);
                setTimeout("DisplayFieldGroups()", 300);
            },
            close: function () {
                $(".ui-dialog").find(".ui-widget-header").css("background", "gray");
            }
        });
    }

    function ShowClaimApprovalDialog(obj, keyName, keyValue) {
        ClearFields();
        ClearTabs();
        $("#edit-approval-section").find("#ctl00_ContentPlaceHolder1_ctl00_txt" + keyName).val(keyValue);
        $("#edit-approval-section").find("#ctl00_ContentPlaceHolder1_ctl00_txt" + keyName).attr("disabled", "disabled");
        $("#edit-approval-section").find("#ctl00_ContentPlaceHolder1_ctl00_txtNationalAccount").val($(obj).attr("aria-natl-acct"));
        $("#edit-approval-section").find("#ctl00_ContentPlaceHolder1_ctl00_txtNationalAccount").attr("disabled", "disabled");

        CheckForExistingRequests(obj);

        $("#claim-approval-dialog").dialog({
            minWidth: 1450,
            minHeight: 800,
            modal: true,
            zIndex: 9000,
            close: function () {
                $(".ui-dialog").find(".ui-widget-header").css("background", "gray");
                //oClaimsGrid.fnReloadAjax();
                $("#CheckRequestTabStatus").val("add");
                $("#CheckRequestTabNames").val("");

                oClaimsGrid.ajax.reload(null, false);

            }
        });
    }

    function CheckForExistingRequests(obj) {

        var primaryKeyValue = $(obj).attr("rel");
        var primaryKeyName = $(".grid-section").attr("checkrequest-key-field");

        var NewJSONDBQuery = {};
        var modeDisplay = "";

        NewJSONDBQuery.FieldList = "ImportID|RecID"
        NewJSONDBQuery.TableName = $(".edit-grid-dropdown-filter option:selected").val().toLowerCase().replace("_claims", "_claims_approval");
        NewJSONDBQuery.KeyFieldName = primaryKeyName
        NewJSONDBQuery.KeyFieldValue = primaryKeyValue;
        NewJSONDBQuery.QueryType = "get";

        var DTO = { 'NewJSONDBQuery': NewJSONDBQuery };

        GetClaimsData(DTO, "tabs");

    }

    function BuildTabs(result, isGMModule) {
        var claimTabIdx = 2;
        var addSetNewCRAsViewedFunction = "";

        if (isGMModule == 1) {
            $("#cr-tabs #cr-tabs1").hide();
            $("#cr-tabs li[aria-controls='cr-tabs1']").hide();

            //addSetNewCRAsViewedFunction = "SetCheckRequestAsViewed(this);";
        }

        $.each(result, function (index, item) {

            try {
                //if ($.trim(item).length > 0) {
                    $("#cr-tabs > ul").append("<li><a href='#cr-tabs" + claimTabIdx + "' class='tab-edit-menu' rel='ImportID~" + item.ImportID.toString() +
                                              "' onclick='javascript:LoadCRTab(this);" + 
                                              "'><span>Check Request " + (claimTabIdx - 1) +"</span></a></li>");

                    $("#cr-tabs").append("<div id='cr-tabs" + claimTabIdx + "'></div>");
                    claimTabIdx++;
               // }
            }
            catch (e) {
                alert(e.Message);
            }
        });

        $('#cr-tabs').tabs("refresh");

        if (isGMModule == 1) {
            $("#cr-tabs li[aria-controls='cr-tabs2'] a").click();
        }
        else {
            $('#cr-tabs').tabs({ selected: 0 });

            $.contextMenu({
                selector: '.tab-edit-menu',
                build: function ($trigger, e) {
                    // this callback is executed every time the menu is to be shown
                    // its results are destroyed every time the menu is hidden
                    // e is the original contextmenu event, containing e.pageX and e.pageY (amongst other data)
                    return {
                        callback: function (key, options) {
                            var m = "clicked: " + key;
                            window.console && console.log(m) || alert(m);
                        },
                        items: {
                            "edit":
                                {
                                    name: "Edit",
                                    icon: "edit",
                                    callback: function (key, options) {
                                        $(this).find("span").hide();
                                        $(this).append("<input type='text' class='new-tab-name-field' />");
                                    }
                                },
                            "quit": {
                                name: "Cancel",
                                icon: "quit",
                                callback: function (key, options) {
                                    $(this).find("span").show();
                                    $(this).find(".new-tab-name-field").remove();
                                }
                            },
                            "save": {
                                name: "Save",
                                icon: "save",
                                callback: function (key, options) {
                                    var newTabName = $.trim($(this).find(".new-tab-name-field").val());

                                    if (newTabName.length > 0) {
                                        $(this).find("span").text(newTabName);
                                        $(this).find(".new-tab-name-field").remove();
                                        $(this).find("span").show();

                                        GetCRTabNames();
                                        SaveCRTabNames();
                                        
                                    }
                                    else {
                                        $(this).find("span").show();
                                        $(this).find(".new-tab-name-field").remove();
                                    }
                                }
                            }
                        }
                    };
                }
            });

            
        }
    }

    function LoadCRTab(obj) {
        var crDivName = $(obj).attr("href");
        var crDiv = $("#" + crDivName.replace("#", ""));
        var isGMModule = $(".user-control-widget div[gm-approval-module='true']").length;
        var tableName = $(".edit-grid-dropdown-filter option:selected").val().toLowerCase().replace("_claims", "_claims_approval");
        var printUrl = "";

        if ($("#ctl00_ContentPlaceHolder1_ctl00_txtNationalAccount").length > 0) {
            var natlAcct= $("#ctl00_ContentPlaceHolder1_ctl00_txtNationalAccount").val();
            $("#lnkCheckRequest").attr("aria-natl-acct", natlAcct);
            $(obj).attr("aria-natl-acct", natlAcct);

        }
        
        if (typeof $(obj).attr("rel") != 'undefined') {
            printUrl = "crkfld=" + $(obj).attr("rel").split("~")[0] + "&crid=" + $(obj).attr("rel").split("~")[1] + "&tbl=" + tableName;
        }

        if ($(obj).attr("href") == "#cr-tabs1") {
            $("#edit-approval-section #lnkApprovalOK").html("<span class='ui-button-text'>Add</span>");
            $("#edit-approval-section #lnkApprovalOK").attr("rel", "");
            $("#ctl00_ContentPlaceHolder1_ctl00_lnkPrintCheckRequest").hide();
            $("#edit-approval-section #lnkDeleteClaim").hide();
        }
        else if (isGMModule == 1) {
            $("#edit-approval-section #lnkApprovalOK").html("<span class='ui-button-text'>Approve</span>");
            $("#edit-approval-section #lnkApprovalOK").attr("rel", $(obj).attr("rel"));
            $("#edit-approval-section #lnkApprovalOK").hide();
            $("#edit-approval-section #lnkDeleteClaim").attr("rel", $(obj).attr("rel"));
            $("#edit-approval-section #lnkDeleteClaim").hide();
            $("#edit-approval-section #ctl00_ContentPlaceHolder1_ctl00_lnkPrintCheckRequest").attr("rel", printUrl);
            $("#ctl00_ContentPlaceHolder1_ctl00_lnkPrintCheckRequest").show();
        }
        else {
            $("#edit-approval-section #lnkApprovalOK").html("<span class='ui-button-text'>Save</span>");
            $("#edit-approval-section #lnkApprovalOK").attr("rel", $(obj).attr("rel"));
            $("#edit-approval-section #lnkDeleteClaim").attr("rel", $(obj).attr("rel"));
            $("#edit-approval-section #lnkDeleteClaim").show();
            $("#edit-approval-section #ctl00_ContentPlaceHolder1_ctl00_lnkPrintCheckRequest").attr("rel", printUrl);
            $("#ctl00_ContentPlaceHolder1_ctl00_lnkPrintCheckRequest").show();
            
        }

        ClearFields();

        if ($(obj).is("[aria-cr-shippername]")) {
            $("#ctl00_ContentPlaceHolder1_ctl00_txtShipperName").val($(obj).attr("aria-cr-shippername"));
        }

        if ($(obj).is("[aria-natl-acct]")) {
            $("#ctl00_ContentPlaceHolder1_ctl00_txtNationalAccount").val($(obj).attr("aria-natl-acct"));
        }

        $("#cr-tabs-fieldlist").appendTo($(crDiv));

        console.log($(obj));
        var primaryKeyValue = $(obj).attr("rel").split("~")[1];
        var primaryKeyName = $(obj).attr("rel").split("~")[0];

        var NewJSONDBQuery = {};
        var modeDisplay = "";

        NewJSONDBQuery.FieldList = GetFieldList("get", "approval") + "|ChargeToAccount~|AuthorizedBy~|Company~|DriverName~|AuthorizedBy2~";
        NewJSONDBQuery.TableName = tableName;
        NewJSONDBQuery.KeyFieldName = primaryKeyName;
        NewJSONDBQuery.KeyFieldValue = primaryKeyValue;
        NewJSONDBQuery.QueryType = "get";

        var DTO = { 'NewJSONDBQuery': NewJSONDBQuery };

        GetClaimsData(DTO, "getcheckrequest");
        LockAuthorizedByField();


  
        //if (isGMModule == 1) {
        //    var authorizedBy = $("#cr-tabs-fieldlist #ContentPlaceHolder1_ctl00_txtApprovedBy").val();
        //    var agentCode = $("#ContentPlaceHolder1_SearchFilter1_txtgmfilter").val();
        //    var lnkBtn = $("#edit-approval-section #lnkApprovalOK");

        //    alert("authorized by: " + authorizedBy);
        //    alert("agent code: " + agentCode);
        //    alert(authorizedBy.indexOf(agentCode));

        //    if (authorizedBy.indexOf(agentCode) != -1) {
        //        $(lnkBtn).text("Completed");
        //        $(lnkBtn).attr("disabled", "disabled");
        //    }
        //    else {
        //        $(lnkBtn).removeAttr("disabled");
        //    }
        //}
    }

    function ClearCommentIndicator(obj) {
    }

    function DisplayFieldGroups() {
        $("div[aria-group-section] >h3 >img").hide();

        $("div[aria-group-name]").each(function () {

            var groupDiv = $(this);
            var groupPanel = $(groupDiv).parent().parent();
            var panelHeader = $(groupPanel).find("h3");

            $(groupDiv).children("input").each(function () {
                if ($(this).val() != "0.00" && $(this).val() != "") {
                    if (!$(panelHeader).find("img").is(":visible")) {
                        $(panelHeader).find("img").attr("src", "../images/icon-doc_opt.png");
                        $(panelHeader).find("img").show();
                    }
                }
            });
        });
    }

    function setEditBackground() {
        // alert($("input[data-field-name='TotalClaimPaidAccuracy']").val());

        if ($("input[data-field-name='TotalClaimPaidAccuracy']").val() != "0.00") {
            $(".ui-dialog").find(".ui-widget-header").css("background", "red");
        }
        else {
            $(".ui-dialog").find(".ui-widget-header").css("background", "gray");
        }
    }

    function ClearFields(mode) {
        $(".edit-field >input").each(function () {
            if (!$(this).is(':disabled')) {
                $(this).val("");
            }
            else if ($(this).is(':disabled') && ($(this).attr("aria-data-type") == "numeric" || $(this).attr("aria-data-type") == "int")) {
                $(this).val("0");
            }
            else if ($(this).is(':disabled') && $(this).attr("aria-data-type") == "varchar" && mode=="add") {
                $(this).val("");
            }
        });

        $(".edit-field >textarea").each(function () {
            $(this).val("");
            //$(this).focus();
        });

        $(".edit-field >select").each(function () {
            $(this).val("");
        });

        $(".edit-field >div > [id='ddlChargeToAccount']").each(function () {
            $(this).val("");

            if (($(this).attr("id") == "ddlChargeToAccount") &&
                !$(this).hasClass("aria-charge-to-fields")) {
                $(this).remove();
            }

            if ($(this).is("span")) {
                $(this).text("N");
                $(this).removeClass("approved-btn");
                //$(this).hide();
            }
        });

        //$(".edit-field >div [id='lnkApproveCharge']").each(function () {
        //    $(this).remove();
        //});

        $(".edit-field >div > [id='txtDriverName']").each(function () {
            $(this).val("");

            if (!$(this).hasClass("aria-driver-fields")) {
                $(this).remove();
            }
        });

        $(".edit-field >div > br").remove();
        $(".comments-list-area").text("");
        $("#claims-print-frame").removeAttr("src");

        HideFields();
    }

    function ClearTabs() {
        $("#cr-tabs-fieldlist").appendTo($("#cr-tabs1"));

        $("#cr-tabs ul >li >a").each(function () {

            if ($(this).attr("href") != "#cr-tabs1") {
                var crDivName = $(this).attr("href").replace("#", "");
                //alert($(this).parent().html());
                $("#" + crDivName).remove();
                $(this).parent().remove();
            }
        });
    }

    function ReloadGrid(refreshCR) {
        //ClearFields();
        //GetFieldList("");
        //$("#edit-dialog").dialog("close");
        //alert($(selectedCheckRequestLink).text())
        if ($(selectedCheckRequestLink).text() == "Check Request") {
            $("#ctl00_ContentPlaceHolder1_ctl00_lnkPrintCheckRequest").hide();

            if (refreshCR) {
                ViewCheckRequests($(selectedCheckRequestLink));
            }

            //oClaimsGrid.fnReloadAjax();
            oClaimsGrid.ajax.reload(null, false);
        }
        else {
            //location.reload();
            oClaimsGrid.ajax.reload(null, false);
        }
        // oEditGrid.fnReloadAjax();
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

    function ViewCheckRequests(obj) {

        selectedCheckRequestLink = obj;
        var shipperName = $(obj).attr("aria-cr-shippername");
        //var nationalAccount = $(obj).attr("aria-natl-acct");
        
        if ($("#edit-dialog").is(":data(dialog)")) {
            $("#edit-dialog").dialog("close");
        }
        
        if ($("#ctl00_ContentPlaceHolder1_ctl00_pChart").attr("adjustor-approval-module") == "true" &&
            ($(obj).text().toLowerCase() == "view check requests" || $(obj).text().toLowerCase() == "check request")) {

            $("#lblCheckRequestClaimID").text($(obj).attr("rel"));
            $("#lnkViewClaim").attr("rel", $(obj).attr("aria-claim-id"));
            $("#lnkViewClaim").attr("aria-checkrequest-id", $(obj).attr("aria-checkrequest-id"));
            $("#NewCheckRequestTab").attr("aria-cr-shipperName", shipperName);
            //$("#NewCheckRequestTab").attr("rel", $(obj).attr("aria-checkrequest-id"));
        }
        else if ($(".user-control-widget div[gm-approval-module='true']").length > 0 &&
                 ($(obj).text().toLowerCase() == "view check requests" || $(obj).text().toLowerCase() == "check request")) {

            $("#lblCheckRequestClaimID").text($(obj).attr("rel"));
            $("#lnkViewClaim").attr("rel", $(obj).attr("aria-claim-id"));
            $("#lnkViewClaim").attr("aria-checkrequest-id", $(obj).attr("aria-checkrequest-id"));
        }

        $("#lnkApprovalOK").attr("aria-claim-id", $(obj).attr("aria-claim-id"));
        $("#ctl00_ContentPlaceHolder1_ctl00_txtShipperName").val(shipperName);
        //$("#ctl00_ContentPlaceHolder1_ctl00_txtNationalAccount").val(nationalAccount);

        EditClaimApproval($(obj));
        LockAuthorizedByField();

        return false;
    }

    function ViewClaim(obj) {

        //alert($(obj).attr("rel"));
        $("#claim-approval-dialog").dialog("close");
        ShowEditDialog($(obj));
        RunQuery($(obj));

        return false;
    }

    function ShowClaimsDocuments(ele) {
        var iframe = $("#frmClaimsDocuments");

        iframe.attr("src", $(ele).attr("href"));

        $("#claim-documents-dialog").dialog({
            width: "auto",
            height: "auto",
            zIndex: 9000,
            close: function () {
                $(iframe).attr("src", "");
            }
        });

        //$("#claim-documents-dialog").append("<iframe src='http://www.google.com' height='800' width='1096' name='frmClaimsDocuments' id='frmClaimsDocuments'></iframe>");
    }

    function LockAuthorizedByField()
    {
        var authorizedUsers = "caroline amerson|david woodhouse";
        var currentUserName = $.trim($("#ctl00_lblUserInfo").text().toLowerCase().replace("welcome", ""));
        var userAuthorizedTF=false;

        if(authorizedUsers.indexOf(currentUserName)>-1) {
            userAuthorizedTF=true;
        }

        $("#cr-tabs-fieldlist div> #ddlAuthorizedBy").each(function (idx, ele) {
            if (userAuthorizedTF && currentUserName=="caroline amerson") {
                $(ele).removeAttr("disabled");
                $("#cr-tabs-fieldlist div> #ddlAuthorizedBy option[value='David Woodhouse']").remove();
            }
            else {
                $(ele).attr("disabled", "disabled");
            }
        });

        $("#cr-tabs-fieldlist div> #ddlAuthorizedBy2").each(function (idx, ele) {
            if (userAuthorizedTF && currentUserName == "david woodhouse") {
                $(ele).removeAttr("disabled");
                $("#cr-tabs-fieldlist div> #ddlAuthorizedBy2 option[value='Caroline Amerson']").remove();
            }
            else {
                $(ele).attr("disabled", "disabled");
            }
        });
    }

    function SetNewCRCreatedToFalse() {
        var firstTabSelectedTF = false;

        if ($("#edit-approval-section #lnkApprovalOK").text() == "Add") {
            firstTabSelectedTF = true;
        }
        $("#cr-tabs-fieldlist div> #ctl00_ContentPlaceHolder1_ctl00_txtNewCRTF").each(function (idx, ele) {

            if (!firstTabSelectedTF) {
                $(ele).val("False");
            }
        });

        //alert(firstTabSelectedTF);
    }

    function HideFields() {
        try {
            var firstTabSelectedTF = false;
            var btnText = $("#edit-approval-section #lnkApprovalOK").text().toLowerCase();

            if (btnText == "add") {
                firstTabSelectedTF = true;
            }

            $("#cr-tabs-fieldlist div> #ctl00_ContentPlaceHolder1_ctl00_txtNewCRTF").each(function (idx, ele) {
                if ($(ele).val() == "" || firstTabSelectedTF) {
                    $(ele).val("True");
                    $(ele).attr("disabled", "disabled");
                }

             $(ele).parent().hide();
            });

           // $("#cr-tabs-fieldlist div> #ContentPlaceHolder1_ctl00_txtTabNames").parent().hide();

            //$("#cr-tabs-fieldlist div > #lnkApproveCharge").each(function (idx, ele) {
            //    var addLink = $(ele).prev();

            //    if ($(addLink).is(":visible")) {
            //        $(ele).hide();
            //    }
            //    else if (btnText == "add" || btnText == "save") {
            //        $(ele).hide();
            //    }
            //    else {
            //        $(ele).show();
            //    }
            //});
            
        }
        catch (e) {
            alert(e.Message);
        }
    }

    function SetCRTabNames() {

        var NewJSONDBQuery = {};

        NewJSONDBQuery.FieldList = "TabNames~";
        NewJSONDBQuery.TableName = "ARS_Consolidated_Claims_Tabs";
        NewJSONDBQuery.KeyFieldName = "RegistrationNumber";
        NewJSONDBQuery.KeyFieldValue = $("#cr-tabs-fieldlist #ctl00_ContentPlaceHolder1_ctl00_txtRegistrationNumber").val();
        NewJSONDBQuery.QueryType = "get";

        var DTO = { 'NewJSONDBQuery': NewJSONDBQuery };

        //alert(JSON.stringify(DTO));
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/JSONDBQuery",
            data: JSON.stringify(DTO),
            dataType: "json",
            beforeSend: function () { },
            success: function (data) {
 
                var record = $.trim(data.d.FieldList).replace("TabNames~","").split("=");
                
                if (record.length > 0) {
                    $.each(record, function (index, item) {
                        var tabInfoID = item.split("^")[0].replace("<","~");
                        var tabName = item.split("^")[1];

                        $(".ui-tabs-nav > li a[rel='" + tabInfoID + "']").find("span").text(tabName);

                        if ($.trim(tabName).length > 0) {
                            $("#CheckRequestTabStatus").val("update");
                        }
                        else {
                            $("#CheckRequestTabStatus").val("add");
                        }
                    });
                    

                    GetCRTabNames();
                }
                else {
                    $("#CheckRequestTabStatus").val("add");
                }
            },
            error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
        });
    }

    function GetCRTabNames() {
        try {
            var newTabNames = "";
            
            $(".ui-tabs-nav > li").each(function () {
                var tabInfoID = $(this).find("a").attr("rel");
                var tabName = $(this).find("a > span").text();

                if (tabName.toLowerCase() != "add check request" && $.trim(tabName).length>0) {
                    if (newTabNames.length == 0) {
                        newTabNames = newTabNames + tabInfoID + "^" + tabName;
                    }
                    else {
                        newTabNames = newTabNames + "|" + tabInfoID + "^" + tabName;
                    }
                }
            });

            $("#CheckRequestTabNames").val(newTabNames);
        }
        catch (e) {
            alert(e.message);
        }
    }

    function SaveCRTabNames() {
        
        var NewJSONDBQuery = {};
        var mode = $("#CheckRequestTabStatus").val();

        NewJSONDBQuery.FieldList = "TabNames~"+$("#CheckRequestTabNames").val().replace(/\~/g,"<").replace(/\|/g,"=");
        NewJSONDBQuery.TableName = "ARS_Consolidated_Claims_Tabs";
        NewJSONDBQuery.KeyFieldName = "RegistrationNumber";
        NewJSONDBQuery.KeyFieldValue = $("#cr-tabs-fieldlist #ctl00_ContentPlaceHolder1_ctl00_txtRegistrationNumber").val();
        NewJSONDBQuery.QueryType = mode;

        var DTO = { 'NewJSONDBQuery': NewJSONDBQuery };

        //alert(JSON.stringify(DTO));
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/JSONDBQuery",
            data: JSON.stringify(DTO),
            dataType: "json",
            beforeSend: function () { },
            success: function (data) { },
            error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
        });

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
</script>

<div class="user-control-widget">
    <div class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix" style="padding: 5px;">
        <asp:Label runat="server" ID="lblChartTitle" Text="" EnableViewState="false" />
    </div>
    <asp:Panel runat="server" ID="pChart" class="grid-section">
        <div id="edit-grid-filter-section" class="chart-title">
            <span>Tables:</span>
            <asp:DropDownList runat="server" ID="ddlTables" CssClass="edit-grid-dropdown-filter" />
            <div class="status-legend">
                <span id="brown-status"><img src="../images/yellow-button.png" border="0"/>  <label>Claim Approved</label></span>
                <span id="pink-status" class="pink-status-btn"><img src="../images/pink-button.png" border="0"/>  <label>Potential Claim</label></span>
                <span id="blue-status"><img src="../images/blue-button.png" border="0"/>  <label>Claim Form Sent</label></span>
                <span id="green-status"><img src="../images/green-button.png" border="0"/>  <label>Open Claim</label></span>
                <span id="orange-status"><img src="../images/orange-button.png" border="0"/>  <label>Pending Claim</label></span>
                <span id="red-status"><img src="../images/red-button.png" border="0"/>  <label>Closed Claim</label></span>
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
        <asp:HyperLink runat="server" ID="hypAddRecord" CssClass="add-record" Text="Add Record" NavigateUrl="javascript:void(0)" onclick='$("#lnkOK").html("<span class=ui-button-text>Add</span>");ShowEditDialog(this); return false;' style="display:none;" />
        <asp:HyperLink runat="server" ID="hypViewDocuments" CssClass="get-documents" Text="View/Edit Documents" NavigateUrl="javascript:void(0)" onclick="ShowDocumentLibrary();" style="display:none;" />
        <asp:HyperLink runat="server" ID="hypCreateICS" CssClass="create-ics" Text="Create Appointment" NavigateUrl="javascript:void(0)" onclick='ShowCalendarDialog(); return false;' style="display:none;" />
        <br />
        <span class="edit-grid-message" style="display:block; color:#fff;"></span>
    </asp:Panel>
    
    <div id="edit-dialog" title="Edit Data" style="display:none;">
        <div style="height:800px;overflow-y:scroll;padding:2px;float:left;display:inline;" id="edit-detail-section">
             <asp:PlaceHolder runat="server" ID="phEditFields" EnableViewState="false">
             </asp:PlaceHolder>
             <div style="width:98%;text-align:right;padding:5px;">
                <asp:LinkButton runat="server" ID="lnkPrint" Text="Print" CssClass="print-button" OnClientClick="javascript:printPage();return false;" />&nbsp;
                <a id="lnkOK" href="javascript:void(0)" onclick="javascript:RunQuery(this);">Ok</a>
                <a id="lnkCheckRequest" href="javascript:void(0)" onclick="javascript:ViewCheckRequests(this); return false;">View Check Requests</a>
                <a class="view-docs-btn" style="display:none;" href="javascript:void(0)" onclick="javascript:ShowClaimsDocuments(this); return false;">View Docs</a>
                <a class="docs-module-btn" style="display:none;" href="javascript:void(0)" onclick="javascript:ShowFilteredDocumentLibrary(this); return false;">View Docs</a>
                <a id="lnkDriverEmail" href="javascript:void(0)" onclick="javascript:ShowEmailDialog(this);" style="display:none;">Driver Notification</a>
             </div>
        </div>
        <div style="float:left;display:inline;padding-left:10px;" id="claims-comments">
            <div>
                <div class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix" style="padding:5px;">Enter Comment</div>
                 <textarea id="Comments" rows="10"  style="width:470px;text-align:left;">
                </textarea>
                <div style="text-align:right;padding-top:5px;">
                    <a href="#" id="add-comment" onclick="javascript:SaveComments(); return false;" style="display:block !important; width:75px;">Add</a>
                </div>
            </div>
            <div style="padding-top:15px;">
                <div class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix" style="padding:5px;">Comments History</div>
                <div id="show-comments" style="width:470px;height:400px; border: 1px solid #000;background-color:#cccccc; padding-left:3px; vertical-align:top; overflow-y:scroll;"></div>
            </div>
        </div>
    </div>
    <div id="claim-approval-dialog" title="Check Request" style="display:none;">
        <div style="height:800px;width:900px;overflow-y:scroll;padding:2px;float:left;display:inline;" id="edit-approval-section">
            <div id="cr-tabs">
                <ul>
                    <li><a href="#cr-tabs1" id="NewCheckRequestTab" onclick='javascript:LoadCRTab(this);'>Add Check Request</a></li>
                </ul>
                <div id="cr-tabs1">
                    <div id="cr-tabs-fieldlist">
                        <asp:PlaceHolder runat="server" ID="phClaimsApproval" EnableViewState="false">
                        </asp:PlaceHolder>
                    </div>
                </div>
                <input type="text" id="CheckRequestTabNames" style="display:none;"/>
                <input type="text" id="CheckRequestTabStatus" style="display:none;" value="add"/>
            </div>
            <div style="width:98%;text-align:right;padding:5px;">
                <a id="lnkDeleteClaim" href="#" onclick="javascript:EditClaimApproval(this); return false;" style="float:left;">Delete</a>
                <span id="lblCheckRequestClaimID" style="display:none;"></span>
                <asp:LinkButton runat="server" ID="lnkPrintCheckRequest" Text="Print" style="display:none;" CssClass="print-button" OnClientClick="javascript:PrintCheckRequest(this);return false;" />&nbsp;<a id="lnkApprovalOK" href="#" onclick="javascript:EditClaimApproval(this);return false;">OK</a>
                <a id="lnkViewClaim" href="#" onclick="javascript:ViewClaim(this); return false;">View Claim</a>
                <a class="view-docs-btn" href="#" style="display:none;" onclick="javascript:ShowClaimsDocuments(this); return false;">View Docs</a>
            </div>
        </div>
        <div id="claim-documents-dialog" title="Claims Documents" style="display:none;">
            <iframe height="800" width="1096" src="" name="frmClaimsDocuments" id="frmClaimsDocuments"></iframe>
        </div>
        <div style="float:left;display:inline;padding-left:10px;" id="cr-comments">
            <div>
                <div class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix" style="padding:5px;">Enter Comment</div>
                 <textarea id="CRComments" rows="10"  style="width:470px;text-align:left;z-index:1000 !important;">
                </textarea>
                <div style="text-align:right;padding-top:5px;">
                    <a href="#" id="add-cr-comment" onclick="javascript:SaveCRComments(); return false;" style="display:block !important; width:75px;">Add</a>
                </div>
            </div>
            <div style="padding-top:15px;">
                <div class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix" style="padding:5px;">Comments History</div>
                <div id="show-cr-comments" style="width:470px;height:400px; border: 1px solid #000;background-color:#cccccc; padding-left:3px; vertical-align:top; overflow-y:scroll;"></div>
            </div>
        </div>
    </div>
    <span id="edit-mode"></span>
    <asp:TextBox runat="server" ID="txtFieldMapList" style="display:none;" />
    <asp:Label runat="server" ID="lblClaimsApprovalFieldList" CssClass="claims-approval-field-list" style="display:none;" />
</div>
<iframe id="claims-print-frame" frameborder="0" width="5" height="5"></iframe>

<script type="text/javascript">
    $("#lnkOK").button();
    $("#lnkApprovalOK").button();
    $("#lnkViewClaim").button();
    $("#lnkDeleteClaim").button();
    $("#lnkDriverEmail").button();
    $(".add-record").button();
    $(".get-documents").button();
    $(".create-ics").button();
    $(".print-button").button();
    //$(".add-charge-to-button").button();
    $("#add-cr-comment").button();
    $("#lnkCheckRequest").button();
    $(".view-docs-btn").button();

    function AddChargeToAccountRow(obj) {
        var parentDiv = $(obj).parent();

        $(parentDiv).append("<br/>")

        $(obj).parent().find(".aria-charge-to-fields").each(function () {
            var newDDL = $(this).clone();

            if ($(newDDL).is("input")) {
                $(newDDL).val("");
            }
            else if ($(newDDL).is("span")) {
                $(newDDL).text("N");
                $(newDDL).removeClass("approved-btn");
                //$(newDDL).hide();
            }

            $(newDDL).removeClass("aria-charge-to-fields");
            $(newDDL).appendTo($(parentDiv));
        });

        if (!$(obj).is(":visible")) {
            var approveBtn = $(obj).parent().find("#lnkApproveCharge").clone();
            $(approveBtn).appendTo($(parentDiv));
        }
        
        SetCheckRequestPickLists();
    }

    function AddDriverRow(obj) {
        var parentDiv = $(obj).parent();
        var idx = 0;

        $(parentDiv).append("<br/>")

        $(obj).parent().find(".aria-driver-fields").each(function () {
            var newTxt = $(this).clone();

            $(newTxt).val("");

            $(newTxt).removeClass("aria-driver-fields");
            $(newTxt).appendTo($(parentDiv));

            //if (idx == 0) {
            //    CreateDropDownBox(newTxt, "cr-dropdown");
            //}

            idx++;
        });

        SetCheckRequestPickLists();

    }

    function printPage() {

        var claimsData = "";

        $("#edit-detail-section div").each(function () {
            if ($(this).is(":visible")) {
                try {
                    if ($(this).find("span").text().toLowerCase() != "print") {
                        claimsData = claimsData + $(this).find("span").text() + "~";

                        if ($(this).children("textarea").length == 1) {
                            claimsData = claimsData + $(this).find("textarea").val() + "|";
                        }
                        else {
                            claimsData = claimsData + $(this).find("input").val() + "|";
                        }
                    }
                }
                catch (e) {
                    //alert(e.Message);
                }
            }
        });

        var commentsText = "";
        $("#show-comments").contents().find(".comment-detail").each(function () {
            commentsText = commentsText + $(this).text() + "\r\n";
        });

        claimsData = claimsData + "Comments~" + commentsText + "|";

        var domainUrl = document.location.hostname;

        if (document.location.port != "80" && document.location.port != "") {
            domainUrl = document.location.hostname + ":" + document.location.port;
        }

        if (domainUrl == "localhost") {
            domainUrl = "localhost/ais";
        }

        var printUrl = "http://" + domainUrl + "/PrintPage.aspx?pdata=" + encodeURIComponent(claimsData);

        $("#edit-dialog").dialog("close");

        //alert(printUrl);
        $("#claims-print-frame").attr("src", printUrl);

    }
   </script>
<script type="text/javascript">

    function LoadComments() {

        $("#Comments").val('');
        $("#show-comments").empty();

        try {
            $.ajax({
                type: "GET",
                contentType: "text/html",
                cache: false,
                url: "../DeficientSurveyComments.aspx?sid=" + sid + "&cid=" + cid + "&ctype=claims",
                success: function () {
                    $("#show-comments").load("../DeficientSurveyComments.aspx?sid=" + sid + "&cid=" + cid + "&ctype=claims&key=" + new Date().getTime());
                },
                error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
            });

        }
        catch (e) {
            alert(e.message);
        }
    }

    function LoadCRComments(showMsg) {

        $("#CRComments").val('');
        $("#show-cr-comments").empty();

        var crAnchor = $("#cr-tabs ul> li.ui-state-active > a");
        var crCommentID = sid;

        if (typeof $(crAnchor).attr("rel") != 'undefined') {
            crCommentID = $(crAnchor).attr("rel").split("~")[1];

            try {
                $.ajax({
                    type: "GET",
                    contentType: "text/html",
                    cache: false,
                    url: "../DeficientSurveyComments.aspx?sid=" + crCommentID + "&cid=" + cid + "&ctype=checkrequest",
                    success: function () {
                        $("#show-cr-comments").load("../DeficientSurveyComments.aspx?sid=" + crCommentID + "&cid=" + cid + "&ctype=checkrequest&key=" + new Date().getTime());
                        //if (showMsg) {
                        //    var ctrlList = "";
                        //    $("#cr-tabs-fieldlist > .edit-field > input").each(function () {
                        //        ctrlList = ctrlList + "\r\n" + $(this).attr("id");
                        //    });

                        //    alert(ctrlList);
                        //}
                    },
                    error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
                });

            }
            catch (e) {
                alert(e.message);
            }
        }
    }

    function SetCommentsAsRead(showMsgTF) {
        var crAnchor = $("#cr-tabs ul> li.ui-state-active > a");
        var crCommentID = sid;
        //var userType = "";

        if (typeof $(crAnchor).attr("rel") != 'undefined') {
            crCommentID = $(crAnchor).attr("rel").split("~")[1];
        }

        if ($(".grid-section").attr("adjustor-approval-module") == "true") {
            userType = "GM";
        }
        else if ($(".user-control-widget div[gm-approval-module='true']").length > 0) {
            userType = "Adjustor";
        }

        var NewSurveyComment = {};

        NewSurveyComment.UserID = uid;

        if (cid == "10003") {
            NewSurveyComment.CompanyID = cid + ",10064";
        }
        else if (cid == "10064") {
            NewSurveyComment.CompanyID = cid + ",10003";
        }
        else {
            NewSurveyComment.CompanyID = cid;
        }

        NewSurveyComment.Comments = $("#CRComments").val();
        NewSurveyComment.SurveyID = crCommentID;
        NewSurveyComment.CommentType = "CheckRequest";
        NewSurveyComment.ParentID = $("#lblCheckRequestClaimID").text();
        NewSurveyComment.UserType = userType;

        var DTO = { 'NewSurveyComment': NewSurveyComment };

        //alert(JSON.stringify(DTO));

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/SetCommentAsRead",
            data: JSON.stringify(DTO),
            dataType: "json",
            success: function () {
                //oClaimsGrid.fnReloadAjax();
                oClaimsGrid.ajax.reload(null, false);
                LoadCRComments(showMsgTF);
            },
            error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
        });

    }

    function SetDriverCommentsAsRead() {
        var crAnchor = $("#cr-tabs ul> li.ui-state-active > a");
        var crCommentID = sid;
        var userType = "Driver";

        if (typeof $(crAnchor).attr("rel") != 'undefined') {
            crCommentID = $(crAnchor).attr("rel").split("~")[1];
        }

        var NewSurveyComment = {};

        NewSurveyComment.UserID = "0";

        if (cid == "10003") {
            NewSurveyComment.CompanyID = cid + ",10064";
        }
        else if (cid == "10064") {
            NewSurveyComment.CompanyID = cid + ",10003";
        }
        else {
            NewSurveyComment.CompanyID = cid;
        }

        NewSurveyComment.Comments = $("#CRComments").val();
        NewSurveyComment.SurveyID = crCommentID;
        NewSurveyComment.CommentType = "Claims";
        NewSurveyComment.ParentID = "0";
        NewSurveyComment.UserType = userType;

        var DTO = { 'NewSurveyComment': NewSurveyComment };

        //alert(JSON.stringify(DTO));

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/SetCommentAsRead",
            data: JSON.stringify(DTO),
            dataType: "json",
            success: function () {
                //oClaimsGrid.fnReloadAjax();
                oClaimsGrid.ajax.reload(null, false);
                LoadCRComments(false);
            },
            error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
        });

    }

    function SetCheckRequestCommentsAsRead(obj) {

        var userType = "";
        var NewSurveyComment = {};

        NewSurveyComment.UserID = uid;

        if ($(".grid-section").attr("adjustor-approval-module") == "true") {
            userType = "GM";
        }
        else if ($(".user-control-widget div[gm-approval-module='true']").length > 0) {
            userType = "Adjustor";
        }

        if (cid == "10003") {
            NewSurveyComment.CompanyID = cid + ",10064";
        }
        else if (cid == "10064") {
            NewSurveyComment.CompanyID = cid + ",10003";
        }
        else {
            NewSurveyComment.CompanyID = cid;
        }

        NewSurveyComment.Comments = $("#CRComments").val();
        NewSurveyComment.SurveyID = "";
        NewSurveyComment.CommentType = "CheckRequest";
        NewSurveyComment.ParentID = $(obj).attr("rel");
        NewSurveyComment.UserType = userType;

        var DTO = { 'NewSurveyComment': NewSurveyComment };

        //alert(JSON.stringify(DTO));

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/SetCheckRequestCommentsAsRead",
            data: JSON.stringify(DTO),
            dataType: "json",
            success: function () {
                //oClaimsGrid.fnReloadAjax();
                oClaimsGrid.ajax.reload(null, false);
                LoadCRComments(false);
            },
            error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
        });

    }

    function SetCheckRequestAsViewed(obj)
    {
        var NewJSONDBQuery = {};
        var tabID= $(obj).parent().parent().parent().parent().parent().attr("id");

        var crPK =$("#cr-tabs").find("a[href='#"+tabID+"']").attr("rel");
        var crID =crPK.split("~")[1];

        NewJSONDBQuery.FieldList = "NewCRTF~False";
        NewJSONDBQuery.TableName = "ARS_Consolidated_Claims_Approval";
        NewJSONDBQuery.KeyFieldName = "ImportID";
        NewJSONDBQuery.KeyFieldValue = crID;
        NewJSONDBQuery.QueryType = "update";

        var DTO = { 'NewJSONDBQuery': NewJSONDBQuery };
        //alert(JSON.stringify(DTO));
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/JSONDBQuery",
            data: JSON.stringify(DTO),
            dataType: "json",
            beforeSend: function () { },
            success: function (data) { },
            error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
        });

    }

    function SaveCRComments() {

        //find active Check Request tab.
        var crAnchor = $("#cr-tabs ul> li.ui-state-active > a");
        var crCommentID = sid;
        var userType = "";

        if (typeof $(crAnchor).attr("rel") != 'undefined') {
            crCommentID = $(crAnchor).attr("rel").split("~")[1];
        }

        if ($(".grid-section").attr("adjustor-approval-module") == "true") {
            userType = "Adjustor";
        }
        else if ($(".user-control-widget div[gm-approval-module='true']").length > 0) {
            userType = "GM";
        }

        var NewSurveyComment = {};

        NewSurveyComment.UserID = uid;
        NewSurveyComment.CompanyID = cid;
        NewSurveyComment.Comments = $("#CRComments").val();
        NewSurveyComment.SurveyID = crCommentID;
        NewSurveyComment.CommentType = "CheckRequest";
        NewSurveyComment.ParentID = $("#lblCheckRequestClaimID").text();
        NewSurveyComment.UserType = userType;

        var DTO = { 'NewSurveyComment': NewSurveyComment };

        //alert(JSON.stringify(DTO));

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/AddSurveyComment",
            data: JSON.stringify(DTO),
            dataType: "json",
            success: function () { LoadCRComments(false); },
            error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
        });
    }

    function SaveComments() {

        var NewSurveyComment = {};

        NewSurveyComment.UserID = uid;
        NewSurveyComment.CompanyID = cid;
        NewSurveyComment.Comments = $("#Comments").val();
        NewSurveyComment.SurveyID = sid;
        NewSurveyComment.CommentType = "Claims";
        NewSurveyComment.ParentID = "0";
        NewSurveyComment.UserType = "";

        var DTO = { 'NewSurveyComment': NewSurveyComment };

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/AddSurveyComment",
            data: JSON.stringify(DTO),
            dataType: "json",
            success: function () { LoadComments(); },
            error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
        });
    }

    function PrintCheckRequest(obj) {
        //EditClaimApproval($("#lnkApprovalOK"));
        var printUrl = "../reports/CheckRequest.aspx?" + $(obj).attr("rel");
        $("#claims-print-frame").attr("src", printUrl);
    }

    function GMApproveCharge(obj) {
        // alert("approved.");
        $(obj).hide();
        $(obj).parent().addClass("approved-btn");
        // $(obj).parent().text($("#ContentPlaceHolder1_SearchFilter1_txtgmfilter").val());
        $(obj).parent().text($(obj).attr("rel"));
        $("#lnkApprovalOK").click();
    }
</script>