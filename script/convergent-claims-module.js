var editMode;
var identityField = "";
var sid;
var mappedFieldList = "";
var redExclamationMarkImg = "<img width='8' height='22' title='' alt='' src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAgAAAAWCAMAAADKDS1SAAAABGdBTUEAALGPC/xhBQAAAAFzUkdCAK7OHOkAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAKVQTFRF/////v7/+evq993e/v/+8Kur1RUV1BMS65aX4Vxc1BIS2zw831FR2zc34WFh3URD///+53p64FZW7JeX5G5u8bW06YyL9tLS76qr+/Hw1BQV9cjJ/v792Coq+ebm30xN1Rsa/fv65W5u2jg3/v7+65eX421t8rq67Z2d+/Dw//7+65iY1BIT1RIS3U1M3lBQ/fj46Y6P3EJC/v3976ys65OT+unqSi7BrwAAAAFiS0dEAIgFHUgAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAB3SURBVAjXVc7XDsJADETRBTaUS+i9hLqQEHr9/09D9j4kmZc5kmXZxmQplStWEVRrdWnbgKYihJaiDZ2uqAd9xQCGujWCsWICU8VszkKxjFj5G+vN1mO3dx6HozOFuDg5Safny/UmD9k78JDJE16pzN6f7y+38gcN8QdQ/s/F/gAAAGJ0RVh0Y29tbWVudABib3JkZXIgYnM6MCBiYzojMDAwMDAwIHBzOjAgcGM6IzAwMDAwMCBlczowIGVjOiMwMDAwMDAgY2s6YTYxMzlhNTJjZDNhYjZhNDNkOTAzNTE4NzU0MzkwMjYiOpXTAAAAJXRFWHRkYXRlOmNyZWF0ZQAyMDE0LTEyLTA3VDIyOjQ4OjE5KzAwOjAwsfwO0AAAACV0RVh0ZGF0ZTptb2RpZnkAMjAxNC0xMi0wN1QyMjo0ODoxOSswMDowMMChtmwAAAAASUVORK5CYII='/>"
var selectedCheckRequestLink;
var oCVClaimsGrid;
var policyType;
var readOnlyMode;
var PageLoadedTF = false;
var NewNCDExpenseQuery;
   
$(document).ready(function () {

    PageLoadedTF = true;
    $(".edit-grid-dropdown-filter").change(function () {
        var pageUrl = window.location.href.split("?")[0];
        var sTable = $(this).find('option:selected').val();
        window.location.href = pageUrl + "?cid=" + cid + "&t=" + sTable;
    });

    //var searchSection = $("#edit-grid_filter > label");
    //var searchTextBox = $(searchSection).find("input");

    //$(searchSection).prepend("Search by Claimant Name, Social Security Number or Claim Number:");

        $(".accordion").accordion({
            collapsible: true,
            active: false,
            autoHeight: false
        });

        $('#cr-tabs').tabs();
        $(".get-documents").button();
        $("#add-comment").button();
        $(".add-record").button();

        readOnlyMode = $(".slide-out-div #ctl00_ContentPlaceHolder1_SearchFilter1_txtgridreadonly").val();

        $("#lnkCheckRequest").button();
        $(".view-docs-btn").button();
        $(".view-docs-btn").show();
        $("#lnkReserveChanges").button();

        if (readOnlyMode !== "true") {
            SetGridToDashboardMode();
            SetMedicalCalculations();
            SetExpenseCalculations();
            SetInsuredInputLengths();
            SetCalculateLagTime();

            $("#lnkOK").button();
            $("#lnkApprovalOK").button();
            $("#lnkDriverEmail").button();
            $(".add-record").button();
            $(".create-ics").button();
            $(".print-button").button();
            $("#add-cr-comment").button();
        }
        else {
            $("#search-grid_filter").hide();
            $(".add-record").hide();
        }
    });

    function CloseAccordions() {

        $(".ui-accordion-header").each(function (i) {
                $(".accordion").accordion("option", "active", i);
        });
    }

    function DefaultNullNumericToZero() {
        //alert($("#edit-dialog input[aria-data-type='numeric']").length);
        $("#edit-dialog input[aria-data-type='numeric']").each(function () {
            if ($(this).val() === "") {
                $(this).val("0.00");
            }
        });

        $("#edit-dialog input[aria-data-type='int']").each(function () {
            if ($(this).val() === "") {
                $(this).val("0");
            }
        });
    }

    function SetReadOnlyMode() {
        $("#edit-dialog input").each(function () {
            $(this).prop("disabled", true);
        });

        $("#edit-dialog textarea").each(function () {
            $(this).prop("disabled", true);
        });

        $("#assigned-adjuster").prop("disabled", true);
        $("#lnkOK").hide();
        $("#lnkApprovalOK").hide();
        $("#lnkDriverEmail").hide();
        $(".add-record").hide();
        //$(".get-documents").hide();
        $(".create-ics").hide();
        $(".print-button").hide();
        $("#add-cr-comment").hide();
       /// $("#lnkCheckRequest").hide();
       // $(".view-docs-btn").hide();
    }

    function SetReadOnlyFields() {
        $("#edit-dialog input[aria-computed-column='true']").prop("disabled", true);
        $("#edit-dialog input[data-field-name='IndemnityIncurredAmountTotal']").prop("disabled", true);
        $("#edit-dialog input[data-field-name='MedicalIncurredAmountTotal']").prop("disabled", true);
        $("#edit-dialog input[data-field-name='ExpensesTotalIncurredAmount']").prop("disabled", true);
        $("#edit-dialog input[data-field-name='ExpensesAllocatedPaidAmount']").prop("disabled", true);
        $("#edit-dialog input[data-field-name='ExpensesUnallocatedPaidAmount']").prop("disabled", true);
        $("#edit-dialog input[data-field-name='ExpensesOutstandingAmountTotal']").prop("disabled", true);
        $("#edit-dialog input[data-field-name='IndemnityPaidAmount']").prop("disabled", true);
        $("#edit-dialog input[data-field-name='MedicalPaidAmount']").prop("disabled", true);
        $("#edit-dialog input[data-field-name='MedicalOutstandingAmount']").prop("disabled", true);
        $("#edit-dialog input[data-field-name='IndemnityOutstandingAmount']").prop("disabled", true);
        $("#edit-dialog input[data-field-name='TotalPaidAmount']").prop("disabled", true);
        $("#edit-dialog input[data-field-name='TotalOutstandingAmount']").prop("disabled", true);
        $("#edit-dialog input[data-field-name='TotalIncurredAmount']").prop("disabled", true);
        $("#insured-table-grid input[data-field-name='ContactEmailAddress']").prop("disabled", true);
        $("#insured-table-grid input[data-field-name='ContactName']").prop("disabled", true);
        $("#insured-table-grid input[data-field-name='PolicyNumber']").prop("disabled", true);
        $("#insured-table-grid input[data-field-name='EffectiveDate']").prop("disabled", true);
        $("#insured-table-grid input[data-field-name='ExpirationDate']").prop("disabled", true);
        $("#insured-table-grid input[data-field-name='CoverageDate']").prop("disabled", true);
        $("#insured-table-grid input[data-field-name='InsuranceCompany']").prop("disabled", true);

        //$("#edit-dialog input[data-field-name='ClaimantPDReserveAmountTotal']").parent().hide();
        //$("#edit-dialog input[data-field-name='ClaimantPDPaidAmountTotal']").parent().hide();
        //$("#edit-dialog input[data-field-name='ClaimantPDIncurredAmountTotal']").parent().hide();
        //$("#edit-dialog input[data-field-name='ClaimantBIReserveAmountTotal']").parent().hide();
        //$("#edit-dialog input[data-field-name='ClaimantBIPaidAmountTotal']").parent().hide();
        //$("#edit-dialog input[data-field-name='ClaimantBIIncurredAmountTotal']").parent().hide();
        //$("#edit-dialog input[data-field-name='ClaimantExpenseReserveAmountTotal']").parent().hide();
        //$("#edit-dialog input[data-field-name='ClaimantExpenseAllocatedAmountTotal']").parent().hide();
        //$("#edit-dialog input[data-field-name='ClaimantExpenseIncurredAmountTotal']").parent().hide();
    }

    function SetGridToDashboardMode() {
        $(".grid-section").each(function () {
            if ($(this).attr("dashboard-mode") == "true") {
                $(this).find(".chart-title").hide();
            }
        });
    }

    function CreateClaimantAndDiaryButton(claimNumber) {
        if ($("#lnkViewEditClaimants").length === 0) {
            $("<a id='lnkViewEditClaimants' class='view-claimants-btn' rel='" + claimNumber + "' href='#' onclick='javascript:CloseAccordions();GetClaimants(this);' style='margin-bottom:2px; margin-top:3px;'>View/Edit Claimants</a>").appendTo($("#viewclaimants"));
            $("#lnkViewEditClaimants").button();
        }
        else {
            $("#lnkViewEditClaimants").attr("rel", claimNumber);
        }

        if ($("#lnkViewEditDiaries").length === 0) {
            $("<a id='lnkViewEditDiaries' class='view-diaries-btn' rel='" + claimNumber + "' href='#' onclick='javascript:CloseAccordions();GetDiaries(this);'>View/Edit Diaries</a>").appendTo($("#viewdiaries"));
            $("#lnkViewEditDiaries").button();
        }
        else {
            $("#lnkViewEditDiaries").attr("rel", claimNumber);
        }

        if ($("#lnkViewEditNotes").length === 0) {
            $("<div id='viewnotes' class='edit-field'><a id='lnkViewEditNotes' class='view-notes-btn' rel='" + claimNumber + "' href='#' onclick='javascript:CloseAccordions();GetNotes(this);'>View/Edit Notes</a></div>").insertAfter($("#viewdiaries"));
            $("#lnkViewEditNotes").button();
        }
        else {
            $("#lnkViewEditNotes").attr("rel", claimNumber);
        }

        if (readOnlyMode === "true") {
            $("#lnkViewEditDiaries").hide();
        }
    }

    function SetInsuredInputLengths() {
        $("#insured-table-grid td input").each(function () {
            if ($(this).attr("aria-data-type") == "datetime") {
                $(this).addClass("insured-input-datetime");
            }
            else if ($(this).attr("data-field-name").toLowerCase() == "insuredname") {
                $(this).addClass("insured-input-text-short");
            }
            else if ($(this).attr("data-field-name").toLowerCase() == "policynumber") {
                $(this).addClass("insured-input-text-shorter");
            }
            else if ($(this).attr("data-field-name").toLowerCase() == "contactemailaddress" ||
                     $(this).attr("data-field-name").toLowerCase() == "contactname") {
                $(this).addClass("insured-input-text-contactinfo");
            }
            else {
                $(this).addClass("insured-input-text");
            }
        });
    }

    function setAutoComplete(obj, responseName) {
        $(obj).autocomplete({
            source: getResponseList(responseName),
            minLength: 0,
            select: function () {
                $(this).autocomplete("close");

            }
        }).click(function () {
            $(this).autocomplete("search","");
        });
    }

    function SetPickLists() {
        
        $("#edit-detail-section > .edit-field> input[class*='aria-pick-list']").each(function (idx, ele) {
            var pickListName = $(ele).attr("aria-pick-list-name");
             $(ele).autocomplete({
                source: getPickList(pickListName),
                minLength: 0,
                select: function (evt,b) {
                    if ($(ele).attr("data-field-name") === "PolicyType") {
                        HideFieldsByPolicyType(b.item.value);
                    }
                    $(this).autocomplete("close");
                }
            }).click(function () {
                $(this).autocomplete("search","");
            });
        });

        $("#edit-detail-section > .edit-field> textarea[class*='aria-pick-list']").each(function (idx, ele) {
            var pickListName = $(ele).attr("aria-pick-list-name");
            $(ele).autocomplete({
                source: getPickList(pickListName),
                minLength: 0,
                select: function () {
                    $(this).autocomplete("close");
                }
            }).click(function () {
                $(this).autocomplete("search", "");
            });
        });

        $("#edit-detail-section > div > div > .edit-field> input[class*='aria-pick-list']").each(function (idx, ele) {
            var pickListName = $(ele).attr("aria-pick-list-name");
            $(ele).autocomplete({
                source: getPickList(pickListName),
                minLength: 0,
                select: function () {
                     $(this).autocomplete("close");
                }
            }).click(function () {
                $(this).autocomplete("search","");
            });
        });

        $("#edit-detail-section > div > div > .edit-field> textarea[class*='aria-pick-list']").each(function (idx, ele) {
            var pickListName = $(ele).attr("aria-pick-list-name");
            $(ele).autocomplete({
                source: getPickList(pickListName),
                minLength: 0,
                select: function () {
                    $(this).autocomplete("close");
                }
            }).click(function () {
                $(this).autocomplete("search", "");
            });
        });

        $("#insured-detail-section > .edit-field> input[class*='aria-pick-list']").each(function (idx, ele) {
            var pickListName = $(ele).attr("aria-pick-list-name");
            $(ele).autocomplete({
                source: getPickList(pickListName),
                minLength: 0,
                select: function () {
                    $(this).autocomplete("close");
                }
            }).click(function () {
                $(this).autocomplete("search");
            });
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

    function GetFieldList(dMode, fieldType) {
        var fieldlist = "";
        var fieldlist2 = "";
        var fieldindex = 0;
        var mapKeyFieldName = "";
        var mapTableName = "";
        mappedFieldList = "";

        var claimsDiv;
        var insuredDiv;

        if (fieldType === "approval") {
            claimsDiv = $("#edit-approval-section");
            fieldlist = SetupFieldFunctions(claimsDiv, dMode);
        }
        else {
            claimsDiv = $("#edit-detail-section");
            fieldlist = SetupFieldFunctions(claimsDiv,dMode);

            insuredDiv = $("#insured-detail-section");
            //alert($(insuredDiv).html());
            fieldlist2 = SetupFieldFunctions(insuredDiv, dMode);
            fieldlist = fieldlist + "|" + fieldlist2;
        }
        //alert(fieldlist.replace(/ContentPlaceHolder1_ctl01_/gi, ""));
        return fieldlist.replace(/ContentPlaceHolder1_ctl01_/gi, "").replace(/ContentPlaceHolder1_ctl03_/gi, "").replace(/ContentPlaceHolder1_ctl04_/gi, "").replace(/ctl00_/gi,"");
    }

    function SetupFieldFunctions(div,dMode) {
        var fieldlist = "";
        var fieldindex = 0;
        var mapKeyFieldName = "";
        var mapTableName = "";
        mappedFieldList = "";

        if ($("#ContentPlaceHolder1_ctl00_txtFieldMapList").attr("aria-map-key-field")) {
            mapKeyFieldName = $("#ContentPlaceHolder1_ctl00_txtFieldMapList").attr("aria-map-key-field");
            mapTableName = $("#ContentPlaceHolder1_ctl00_txtFieldMapList").attr("aria-map-table-name");
            mappedFieldList = $("#ContentPlaceHolder1_ctl00_txtFieldMapList").val();
        }
        
        $(div).find(".response-field").each(function () {

            if ($(this).attr("aria-identity-column") === "true") {
                identityField = $(this).attr("data-field-name");
            }

            if ($(this).attr("aria-identity-column") === "false") {
                var responseListName = $(this).attr("response-list-type");

                if (responseListName !== 'undefined') {
                    setAutoComplete($(this), responseListName);
                }

                if ($(this).attr("aria-data-type").indexOf('date') != -1) {
                    //alert(typeof $(".grid-section").attr("createics")!='undefined')
                    if (typeof $(".grid-section").attr("createics") != 'undefined') {
                        if ($(".grid-section").attr("createics").indexOf($(this).attr("data-field-name").toLowerCase()) != -1) {
                            $(this).datepicker({
                                changeMonth: true,
                                changeYear:true,
                                onSelect: function (dateText, inst) {
                                    ShowCalendarDialog(dateText);
                                }
                            });
                        }
                        else {
                            $(this).datepicker({
                                changeMonth: true,
                                changeYear: true
                            });
                        }
                    }
                    else {
                        $(this).datepicker({
                            changeMonth: true,
                            changeYear: true
                        });
                    }
                }

                if (($(this).attr("aria-data-type") === "numeric" ||
                     $(this).attr("aria-data-type") === "decimal" ||
                     $(this).attr("aria-data-type") === "int") && $(this).val() === "") {

                    if ($(this).attr("id").toLowerCase().indexOf("companyid") !== -1) {
                        $(this).val(cid);
                    }
                    else {
                        $(this).val("0");
                    }

                    $(this).ForceNumericOnly();

                }

                if ($(this).attr("aria-computed-column") === "true") {
                    $(this).attr("disabled", true);
                }

                if ($(this).attr("aria-populate-list") !== "undefined") {

                    var fieldName = $(this).attr("data-field-name");
                    var tableType = "Convergent_PolicyInfo";
                    var populateFields = $(this).attr("aria-populate-list");

                    if (populateFields === undefined) {
                        tableType = $(this).attr("data-table-type");
                    }
                 
                    $(this).autocomplete({
                        source: "../ListHandler.ashx?cid=" + cid + "&uid=" + uid + "&fn=" + fieldName + "&tn=" + tableType + "&qt=lookup&qid=" + S4(),
                        minLength: 1
                    });

                    $(this).on("autocompleteselect", function (e, ui) {

                        var NewJSONDBQuery = {};
                        NewJSONDBQuery.FieldList = populateFields;
                        NewJSONDBQuery.TableName = tableType;
                        NewJSONDBQuery.KeyFieldName = fieldName;
                        NewJSONDBQuery.KeyFieldValue = ui.item.value;
                        NewJSONDBQuery.QueryType = "get";

                        var DTO = { 'NewJSONDBQuery': NewJSONDBQuery };

                        GetMultiSelectData(DTO);
                    });

                }
                else if ($(this).attr("data-field-name") !== "undefined") {

                    var fieldName = $(this).attr("data-field-name");
                    var tableType = $(this).attr("data-table-type");

                    if (typeof $(this).attr("picklist-field-name") !== "undefined") {
                        fieldName = $(this).attr("picklist-field-name");
                        tableType = $(this).attr("picklist-table-type");
                    }

                    $(this).autocomplete({
                        source: "../ListHandler.ashx?cid=" + cid + "&uid=" + uid + "&fn=" + fieldName + "&tn=" + tableType + "&qt=lookup&qid=" + S4(),
                        minLength: 1
                    });

                    if (mapKeyFieldName !== "" && mapKeyFieldName.toLowerCase() === $(this).attr("data-field-name").toLowerCase()) {

                        $(this).on("autocompleteselect", function (e, ui) {

                            if (dMode === "") {

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

                    if (typeof fieldName !== 'undefined') {
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

                if ($(this).attr("aria-data-type") === "numeric") {
                    fv = accounting.unformat(fv);
                }

                if ($(this).attr("aria-identity-column") !== "true" && typeof $(this).attr("aria-indicator-column") === 'undefined') {
                    if ($(this).attr("aria-computed-column") === "true" && (dMode === "save" || dMode === "add")) {
                        //Don't do anything.  we cannot save data to a computed column.
                    }
                    else if ($(this).attr("aria-indicator-column") === 'true') {
                        //Don't do anything.  we cannot save data to a indicator column.
                    }
                    else if (fieldindex === 0) {
                        fieldlist = fieldlist + fn + "~" + fv;
                    }
                    else {
                        fieldlist = fieldlist + "|" + fn + "~" + fv;
                    }
                }
                ++fieldindex;
            }
        });

        return fieldlist;
    }

    function BuildFieldList(fl, fName, fValue) {
        var flArray = fl.split("|");
        var result = "";
        //var currentValue="";

        if (flArray.length === 1) {
            result = fName + "~" + fValue;
        }
        else {

            for (var i = 0; i < flArray.length; i++) {
                var item = flArray[i];
                //alert(item);
                if (item.indexOf(fName) != -1) {
                    item = item + "^" + fValue;
                }

                if (i === 0) {
                    result = item;
                }
                else {
                    result = result + "|" + item;
                }
            }
        }

        return result;
    }

    function SetInputMask() {

        $("#edit-dialog input").each(function () {
            if ($(this).attr("data-field-name") === undefined) {
                //Do Nothing.
            }
            else if ($(this).attr("aria-data-type") === "numeric" &&
                $(this).attr("data-field-name").toLowerCase().indexOf("recovery") > -1) {
                $(this).mask('-9000.00', { reverse: true });
                $(this).css("color", "red");
            }
            else if ($(this).attr("aria-data-type") === "numeric" && 
                     $(this).attr("data-field-name").toLowerCase() === "ttd" &&
                     $(this).attr("data-field-name").toLowerCase() === "ppd") {
                $(this).mask('00000000.00', {reverse: true});
            }
            else if ($(this).attr("data-field-name").toLowerCase() === "ssn") {
                $(this).mask('000-00-0000');
            }
            else if ($(this).attr("data-field-name").toLowerCase().indexOf("phone") > -1) {
                $(this).mask('999-999-9999');
            }

            if ($(this).attr("aria-data-type") === "numeric" && $(this).val() === "" &&
                $(this).attr("data-field-name").toLowerCase() !== "indemnityppdamount" &&
                $(this).attr("data-field-name").toLowerCase() !== "indemnityttdamount") {
                $(this).mask('00000000.00', { reverse: true });
                $(this).val("0.00");
            }
            else if ($(this).attr("aria-data-type") === "integer" && $(this).val() === "") {
                $(this).val("0");
            }

            if ($(this).attr("data-field-name") === "TTD" || $(this).attr("data-field-name") === "PPD") {
                $(this).focus(function () { $(this).select(); });
            }

            if ($(this).attr("data-field-name") === "IndemnityTTDTotal" ||
                $(this).attr("data-field-name") === "IndemnityPPDTotal" ||
                $(this).attr("data-field-name") === "ClaimNumber") {
                $(this).prop('disabled', true);
            }

            if ($(this).attr("aria-data-type") === "numeric" && $(this).attr("data-field-name").toLowerCase().indexOf("ppd") === -1 &&
                $(this).attr("data-field-name").toLowerCase().indexOf("ttd") === -1) {
                $(this).change(function () {
                    LogExpenseUpdates($(this).attr("data-field-name"), $(this).val());
                });
            }

        });
    }

    function RunQuery(obj) {
        
        editMode = $(obj).text().toLowerCase();
        var queryType;

        if (editMode === "view checks") {
            queryType = "save";
        }
        else {
            queryType = editMode;
        }

        var tableName = $(".edit-grid-dropdown-filter option:selected").val();
        var primaryKeyValue = $(obj).attr("rel");
        var primaryKeyName = $(".grid-section").attr("primary-key-field");

        $(".grid-section").each(function () {
            primaryKeyName = $(this).attr("primary-key-field");
            return false;
        });

        if (typeof (primaryKeyName) === "undefined") {
            primaryKeyName = "ImportID";
        }

        if (primaryKeyValue === undefined) {
            primaryKeyValue ="0";
        }

        sid = primaryKeyValue;

        if (tableName !== "") {
            var NewJSONDBQuery = {};
            var modeDisplay = "";

            NewJSONDBQuery.FieldList = GetFieldList(queryType, "claim");
            NewJSONDBQuery.TableName = tableName;
            NewJSONDBQuery.KeyFieldName = primaryKeyName;
            NewJSONDBQuery.KeyFieldValue = primaryKeyValue;
            NewJSONDBQuery.QueryType = queryType.split(" ")[0];
            NewJSONDBQuery.UserID = uid;
            
            CheckForIdentityField();

            if (identityField.toLowerCase() === primaryKeyName.toLowerCase()) {
                NewJSONDBQuery.KeyFieldIsIdentity = "true";
            }

            if (editMode === "view checks") {
                editMode = "save";
                modeDisplay = "";
            }
            else if (editMode === "add claim" || editMode === "add") {
                modeDisplay = "added";
            }
            else if (editMode === "save") {
                modeDisplay = "updated";
            }
            else if (editMode === "delete") {
                modeDisplay = "deleted";
                var completeDelete = confirm('Are you sure you want to delete?');

                if (!completeDelete) {
                    return true;
                }
            }
            else if (editMode === "view" || editMode === "view claim") {
                NewJSONDBQuery.QueryType = "edit";
                modeDisplay = "get";
                $("#lnkOK").html("<span class='ui-button-text'>Save</span>");
                $("#lnkOK").attr("rel", primaryKeyValue);
                ClearFields();
                LoadComments();
            }
            else if (editMode === "edit") {
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

                    if (modeDisplay === "get") {
                        var record = data.d.FieldList.split("|");

                        $.each(record, function (index, item) {
                            try {
                                var fieldName = item.split("~")[0];
                                var fieldValue = item.split("~")[1].replace("1/1/1900 12:00:00 AM", "").replace("12:00:00 AM", "");

                                if (fieldName.toLowerCase() === "registrationnumber") {
                                    regNo = fieldValue;
                                }

                                if (fieldName.toLowerCase() === "claimnumber") {
                                    $("#edit-dialog #lnkCheckRequest").attr("rel", primaryKeyValue);
                                    $("#edit-dialog #lnkCheckRequest").attr("aria-claim-id", fieldValue);
                                    $("#edit-dialog #lnkReserveChanges").attr("aria-claim-id", fieldValue);
                                }

                                if (fieldName.toLowerCase() === "lastname") {
                                    lastName = fieldValue;
                                }

                                if (fieldName.toLowerCase() === "contactemailaddress" && ($("#ContentPlaceHolder1_ctl00_pChart").attr("show-email-link")==="true" || $("#ContentPlaceHolder1_ctl03_pChart").attr("show-email-link")==="true")) {

                                    var claimID = $("#lnkDriverEmail").attr("aria-claim-id");
                                    $("#lnkDriverEmail").attr("rel", fieldValue);

                                    if ($("#edit-dialog input[data-field-name='ContactEmailAddress']").next().length && $("#edit-dialog input[data-field-name='ContactEmailAddress']").next().is("select")) {
                                        $("#edit-dialog input[data-field-name='ContactEmailAddress']").next().val(fieldValue.toUpperCase());

                                        if ($("#edit-dialog input[data-field-name='ContactEmailAddress']").next().length && $("#edit-dialog input[data-field-name='ContactEmailAddress']").next()[0].selectedIndex == -1) {
                                            $("#edit-dialog input[data-field-name='ContactEmailAddress']").append('<option value="' + fieldValue.toUpperCase() + '">' + fieldValue.toUpperCase() + '</option>');
                                            $("#edit-dialog input[data-field-name='ContactEmailAddress']").next().val(fieldValue.toUpperCase());
                                        }
                                    }

                                    $("#lnkDriverEmail").attr("onclick", "javascript:PopulateEmailAddress(this);ShowEmailDialog(this);");
                                }

                                if ($("#edit-dialog input[data-field-name='" + fieldName + "']").length > 0) {
                                    $("#edit-dialog input[data-field-name='" + fieldName + "']").val(fieldValue);
                                }
                                else if ($("#edit-dialog textarea[data-field-name='" + fieldName + "']").length > 0) {
                                    $("#edit-dialog textarea[data-field-name='" + fieldName + "']").val(fieldValue);
                                }

                                if ($("#edit-dialog input[data-field-name='TTD']").val() === "") {
                                    $("#edit-dialog input[data-field-name='TTD']").val("0.667");
                                }

                                if ($("#edit-dialog input[data-field-name='PPD']").val() === "") {
                                    $("#edit-dialog input[data-field-name='PPD']").val("0.667");
                                }

                                if (editMode === "view") {
                                    $("#edit-dialog input[data-field-name='" + fieldName + "']").attr("disabled", "disabled");
                                }

                                if ($("#edit-dialog input[data-field-name='" + fieldName + "']").attr("aria-data-type") === "numeric" &&
                                    fieldName !== "TTD" && fieldName !== "PPD" &&
                                    fieldName !== "IndemnityTTDAmount" && fieldName !== "IndemnityPPDAmount") {
                                    var moneyValue = $("#edit-dialog input[data-field-name='" + fieldName + "']").val();
                                    $("#edit-dialog input[data-field-name='" + fieldName + "']").val(accounting.formatMoney(moneyValue));
                                }

                                if (fieldName === "PolicyType") {
                                    HideFieldsByPolicyType(fieldValue);
                                }
                            }
                            catch (e) {
                                alert(e.Message);
                                console.log(e);
                            }
                        });
                        
                        SetPickLists();
                        //SetDriverCommentsAsRead();
                        LockEditFields();

                        if (readOnlyMode === "true") {
                            SetReadOnlyMode();
                        }
                        else if (PageLoadedTF) {
                            SetupWorkersCompGrouping();
                            SetReadOnlyFields();
                            SetAWWValues();
                            SetTTDCalculations();
                            SetPPDCalculations();

                            PageLoadedTF = false;
                        }

                        AssignAdjuster();
                        CreateClaimantAndDiaryButton($("#edit-dialog input[data-field-name='ClaimNumber']").val());

                        $(".view-docs-btn").attr("rel", $("#edit-dialog input[data-field-name='ClaimNumber']").val());
                        $("#edit-dialog input[data-field-name='" + NewJSONDBQuery.KeyFieldName + "']").val(NewJSONDBQuery.KeyFieldValue);
                    }

                    if (modeDisplay === "updated" || modeDisplay === "view checks update") {

                        var NewNCDUsersQuery = {
                            CompanyID: cid,
                            UserID: $("#edit-dialog input[data-field-name='UserID']").val(),
                            UserType: "",
                            ClaimNumber: $("#edit-dialog input[data-field-name='ClaimNumber']").val(),
                            QueryType: "updateuser",
                            Users: new Array()
                        };

                        DTO = { 'NewNCDUsersQuery': NewNCDUsersQuery };

                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "../AISWS.asmx/NCDUsersQuery",
                            data: JSON.stringify(DTO),
                            dataType: "json",
                            beforeSend: function () { },
                            success: function (data) { SetPickLists(); },
                            error: function (xhr, textStatus, error) { SetPickLists(); alert( xhr.responseText); }
                        });
                    }

                    if (modeDisplay !== "get") {

                        if (editMode === "approve") {
                            ReloadConvergentClaimsGrid(false);
                        }
                        else {
                            ReloadConvergentClaimsGrid(true);
                        }

                        if (modeDisplay !== "deleted" && modeDisplay !== "" && modeDisplay !== null) {
                            alert("Data was " + modeDisplay + " successfully.");
                        }

                        if (modeDisplay === "added")
                        {
                            location.reload();
                        }
                    }

                    SetInputMask();
                    NewNCDExpenseQuery = { ClaimNumber: "", QueryType: "", Diaries: new Array() };
                },
                error: function (xhr, textStatus, error) {alert("Error: " + xhr.responseText); }
            });
        }
    }

    function PopulateEmailAddress(obj) {
        $(obj).attr("rel", $("#edit-dialog input[data-field-name='ContactEmailAddress']").val());
    }

    function GetMappedField(fieldName, mode) {

        var fieldArray = mappedFieldList.split("|");
        var fieldList = "";
        var result = "";

        $.each(fieldArray, function (i) {
            var source = fieldArray[i].split("=")[0];
            var dest = fieldArray[i].split("=")[1];

            if (mode === "source" && fieldName.toLowerCase() === source.toLowerCase()) {
                result = dest;
            }

            if (mode === "destination" && fieldName.toLowerCase() === dest.toLowerCase()) {
                result = source;
            }

            if (mode === "list") {
                if (fieldList.length == 0) {
                    fieldList = source;
                }
                else {
                    fieldList += "|" + source;
                }
            }
        });

        if (mode === "list") {
            return fieldList;
        }
        return result;
    }

    function GetMultiSelectData(DTO) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/JSONDBQuery",
            data: JSON.stringify(DTO),
            dataType: "json",
            beforeSend: function () { },
            success: function (data) {
                var record = data.d.FieldList.split("|");

                $.each(record, function (index, item) {
                    try {
                        //alert(item);
                        var fieldName = item.split("~")[0];
                        var fieldValue = item.split("~")[1].replace("1/1/1900 12:00:00 AM", "").replace("12:00:00 AM", "");

                        $("#edit-dialog input[data-field-name='" + fieldName + "']").val(fieldValue);
                        //$("#ContentPlaceHolder1_ctl00_txt" + fieldName).val(fieldValue);
                    }
                    catch (e) {
                        console.log(e.message);
                    }
                });
            }
        });
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

                if (modeDisplay === "edit") {

                    var record = data.d.FieldList.split("|");

                    $.each(record, function (index, item) {
                        try {
                            //alert(item);
                            var fieldName = item.split("~")[0];
                            var fieldValue = item.split("~")[1].replace("1/1/1900 12:00:00 AM", "").replace("12:00:00 AM", "");

                            var destFieldName = GetMappedField(fieldName, "source");

                            if (destFieldName !== "") {
                                $("#edit-dialog input[data-field-name='" + destFieldName + "']").val(fieldValue);
                               // $("#ContentPlaceHolder1_ctl00_txt" + destFieldName).val(fieldValue);
                            }
                        }
                        catch (e) {
                            alert(e.Message);
                        }
                    });

                    //$("#ContentPlaceHolder1_ctl00_txt" + NewJSONDBQuery.KeyFieldName).val(NewJSONDBQuery.KeyFieldValue);
                }
            },
            error: function (xhr, textStatus, error) { alert("4 Error: " + xhr.responseText); }
        });
    }

function ShowEditDialog(obj) {
    var actionType = $(obj).text().toLowerCase();

    if (actionType==="add claim") {
        ClearFields();
        GetFieldList("", "claim");
    }

        if ($(".grid-section").attr("show-email-link") === "true") {
            $("#edit-dialog #lnkDriverEmail").attr("aria-claim-id", $(obj).attr("rel"));
        }

        if ($(".user-control-widget div[gm-approval-module='true']").length > 0) {
            $("#edit-dialog #lnkOK").hide();
        }

        if (actionType === "add claim") {
            $("#lnkViewEditClaimants").hide();
            $("#lnkViewEditDiaries").hide();
            AssignAdjuster();
        }
        else {
            $("#lnkViewEditClaimants").show();
            $("#lnkViewEditDiaries").show();
        }

        //$("#edit-detail-section input[aria-data-type='numeric']").currency();
        //alert("Here!!");
        $("#edit-dialog").dialog({
            minWidth: 1190,
            minHeight: 600,
            modal: true,
            zIndex: 9000,
            position: {
                my: "top center",
                at: "top center",
                of: "#contentcolumn"
            },
            open: function () {
                setTimeout("SetPickLists()", 200);
                setTimeout("DisplayFieldGroups()", 200);
                setTimeout("SetInputMask()", 200);
                setTimeout("DisplayClaimStatusText()", 200);
                //setTimeout("HideFieldsByPolicyType()", 300);

                if ($("#edit-dialog input[data-field-name='TTD']").val() === "0" ||
                    $("#edit-dialog input[data-field-name='TTD']").val() === "") {
                    $("#edit-dialog input[data-field-name='TTD']").val("0.667");
                }

                if ($("#edit-dialog input[data-field-name='PPD']").val() === "0" ||
                    $("#edit-dialog input[data-field-name='PPD']").val() === "") {
                    $("#edit-dialog input[data-field-name='PPD']").val("0.667");
                }

                if (actionType === "add claim" && PageLoadedTF) {
                    SetupWorkersCompGrouping();
                    SetReadOnlyFields();
                    SetTTDCalculations();
                    SetPPDCalculations();

                    PageLoadedTF = false;
                }

                $(".claims-comments-log a").each(function () {
                    $(this).removeClass("claimhistory-type-selected");
                });

                $(".claims-comments-log > a[aria-history-type='Claim']").addClass("claimhistory-type-selected");

            },
            close: function () {
                $(".ui-dialog").find(".ui-widget-header").css("background", "gray");
            }
        });
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
                if ($(this).val() !== "0.00" && $(this).val() !== "") {
                    if (!$(panelHeader).find("img").is(":visible")) {
                        $(panelHeader).find("img").attr("src", "../images/icon-doc_opt.png");
                        $(panelHeader).find("img").show();
                    }
                }
            });
        });

        if (readOnlyMode === "true") {
            $("div[aria-group-section='Workers Comp Reserve Worksheet']").hide();
        }
    }

    function ClearFields() {
        $(".edit-field >input").each(function () {
            if (!$(this).is(':disabled')) {
                $(this).val("");
            }
            else if ($(this).is(':disabled') && $(this).attr("aria-data-type") === "numeric") {
                $(this).val("0");
            }
            else {
                $(this).val("");
            }
        });

        $("#insured-table-grid tbody > tr > td > input").each(function () {
            $(this).val("");
        });

        $(".edit-field >textarea").each(function () {
            $(this).val("");
            //$(this).focus();
        });

        $(".edit-field >select").each(function () {
            $(this).val("");
        });

        $(".edit-field >div > br").remove();
        $(".comments-list-area").text("");
        $("#claims-print-frame").removeAttr("src");

        HideFields();
    }

    function ReloadConvergentClaimsGrid(refreshCR) {
        //oClaimsGrid.fnReloadAjax();
        if (refreshCR) {
            oClaimsGrid.ajax.reload(null, true);
            ReloadConvergentEditClaimsWindow();
        }
        else {
            oClaimsGrid.ajax.reload(null, false);
        }
}

function ReloadConvergentEditClaimsWindow(claimNumber) {
    
    var link = $("<a rel='" + sid + "'>Edit</a>");

    if (sid !== "0") {
        RunQuery(link);
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
                    key === 8 ||
                    key === 9 ||
                    key === 13 ||
                    key === 46 ||
                    key === 110 ||
                    key === 190 ||
                    (key >= 35 && key <= 40) ||
                    (key >= 48 && key <= 57) ||
                    (key >= 96 && key <= 105));
            });
        });
    };

    function ShowClaimsDocuments(ele) {
        var iframe = $("#frmClaimsDocuments");

        iframe.attr("src", $(ele).attr("href"));

        $("#claim-documents-dialog").dialog({
            width: "auto",
            height: "auto",
            zIndex: 9000,
            position: {
                my: "top center",
                at: "top center",
                of: "#contentcolumn"
            },
            close: function () {
                $(iframe).attr("src", "");
            }
        });

        //$("#claim-documents-dialog").append("<iframe src='http://www.google.com' height='800' width='1096' name='frmClaimsDocuments' id='frmClaimsDocuments'></iframe>");
    }

    function LockAuthorizedByField()
    {
        var authorizedUsers = "tbd|tbd2";
        var currentUserName = $.trim($("#ctl00_lblUserInfo").text().toLowerCase().replace("welcome,", ""));
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

        //$("#cr-tabs-fieldlist div> #ddlAuthorizedBy2").each(function (idx, ele) {
        //    if (userAuthorizedTF && currentUserName === "david woodhouse") {
        //        $(ele).removeAttr("disabled");
        //        $("#cr-tabs-fieldlist div> #ddlAuthorizedBy2 option[value='Caroline Amerson']").remove();
        //    }
        //    else {
        //        $(ele).attr("disabled", "disabled");
        //    }
        //});
    }

    function AssignAdjuster() {
        var recordUserID = $("#edit-dialog input[data-field-name='UserID']").val();
        var adjusterCtrl;
        var NewNCDUsersQuery;
        var DTO;

        if ($("#edit-dialog #assigned-adjuster").length>0) {
            adjusterCtrl = $("#edit-dialog #assigned-adjuster");
            $(adjusterCtrl).empty();
            $(adjusterCtrl).append("<option>Not Assigned</option>");
        } 
        else {
            adjusterCtrl = $("<select id='assigned-adjuster'><option value='"+uid+"'>Not Assigned</option></select>");

            $(adjusterCtrl).change(function () {
                $("#edit-dialog input[data-field-name='UserID']").val($(this).val());
            });
        }

        $("#edit-dialog input[data-field-name='UserID']").prev().text("Assigned Adjuster");
        $("#edit-dialog input[data-field-name='UserID']").hide();
        $("#edit-dialog input[data-field-name='UserID']").after($(adjusterCtrl));
        $("#edit-dialog input[data-field-name='UserID']").parent().show();

        NewNCDUsersQuery = { CompanyID: cid, UserID: recordUserID, UserType:"Adjuster", QueryType: "getbyusertype", Users: new Array() };
        DTO = { 'NewNCDUsersQuery': NewNCDUsersQuery };

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/NCDUsersQuery",
            data: JSON.stringify(DTO),
            dataType: "json",
            beforeSend: function () { },
            error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); },
            success: function (data) {
                var record = data.d.Users;
                var userName;
                var adjusterFoundTF = false;

                $.each(record, function (index, item) {
                    userName=item.FirstName+' '+item.LastName;

                    if (item.UserID === recordUserID) {
                        $(adjusterCtrl).append("<option value='" + item.UserID + "' selected>" + userName + "</option>");
                    } else {
                        $(adjusterCtrl).append("<option value='" + item.UserID + "'>" + userName + "</option>");
                    }
 
                });
            }
        });


    }

    function SetAssignedAdjuster(ele) {
        alert($(ele).val());
    }

    function HideFields() {
        try {
            var firstTabSelectedTF = false;
            var btnText = $("#edit-approval-section #lnkApprovalOK").text().toLowerCase();

            if (btnText === "add") {
                firstTabSelectedTF = true;
            }

            $("#cr-tabs-fieldlist div> #ctl00_ContentPlaceHolder1_ctl00_txtNewCRTF").each(function (idx, ele) {
                if ($(ele).val() === "" || firstTabSelectedTF) {
                    $(ele).val("True");
                    $(ele).attr("disabled", "disabled");
                }

             $(ele).parent().hide();
            });
        }
        catch (e) {
            alert(e.Message);
        }
    }

   function DisplayClaimStatusText() {
        var claimStatus = $("#edit-dialog input[data-field-name='ClaimStatus']"); //$("#ContentPlaceHolder1_ctl00_txtClaimStatus").val();

        if ($(claimStatus).val() === "O") {
            $(claimStatus).val("OPEN");
        }
        else if ($(claimStatus).val() === "C") {
            $(claimStatus).val("CLOSED");
        }
        else {
            $(claimStatus).val("NEW");
        }
    }

    function SetSubGroupLabel(ele, labelName) {
        if ($(".group-sub-label").length < 3) {
            $(ele).parent().before("<div class='edit-field ui-accordion-header ui-state-active ui-corner-all group-sub-label' aria-group-name='Workers Comp Reserve Worksheet'><span class='accordion-text'>" + labelName + "</span></div>");
        }
    }

function SetTTDCalculations() {
    
        var ttd = $("#edit-dialog input[data-field-name='TTD']");
        var ttdTotal = $("#edit-dialog input[data-field-name='IndemnityTTDTotal']");
        var ppdTotal = $("#edit-dialog input[data-field-name='IndemnityPPDTotal']");
        var ttdAmount = $("#edit-dialog input[data-field-name='IndemnityTTDAmount']");
        var ttdSection=$("#edit-dialog input[data-field-name='TTD']").parent();
        var ttdTotalSection = $("#edit-dialog input[data-field-name='IndemnityTTDTotal']").parent();
        var indemnityReserveAmt = $("#edit-dialog input[data-field-name='IndemnityIncurredAmountTotal']");
        var indemnityOutstandingAmt = $("#edit-dialog input[data-field-name='IndemnityOutstandingAmount']");
        var indemnityPaidAmt = $("#edit-dialog input[data-field-name='IndemnityPaidAmount']");
        
        var ttdCalc = 0.00;
        var ttdAmountVal;
        var ttdVal;
        var ttdTotalVal;
        var ppdTotalVal;
        var indemnityPaidAmtVal;
        var awwAmount = $("#edit-dialog input[data-field-name='AWW']");
        var ttdAWWAmount = $("#edit-dialog input[data-field-name='TTDAWWAmount']");

        ttdAWWAmount.parent().hide();
        var awwAmountVal = accounting.unformat($(awwAmount).val());

        if (accounting.unformat($(ttdAWWAmount).val()) > 0) {
            awwAmountVal = accounting.unformat(ttdAWWAmount.val());
        }

        $(ttdAWWAmount).val(accounting.formatMoney(awwAmountVal));
        $(ttdAWWAmount).attr("calc-field-name", "TTDAWWAmount");
        $(ttdAmount).parent().addClass("subtotal-calc-section");
        $(ttdAmount).after($(ttdTotal));
        $(ttdAmount).after("<label>=</label>");
        $(ttdAmount).after($(ttd));
        $(ttdAmount).after("<label>X</label>");
        $(ttdAmount).before(ttdAWWAmount);
        $(ttdAmount).before("<label>X</label>");
        
        $(ttdSection).hide();
        $(ttdTotalSection).hide();

    $(ttdAWWAmount).change(function () {
        $(this).val(accounting.formatMoney($(this).val()));
        ttdAmountVal = accounting.unformat($(ttdAmount).val());
        awwAmountVal = accounting.unformat($(this).val());
        ttdVal = accounting.unformat($(ttd).val());
        ttdCalc = 0.00;
        ttdCalc = awwAmountVal * ttdAmountVal * ttdVal;

        $(ttdTotal).val(accounting.formatMoney(ttdCalc));
        ppdTotalVal = accounting.unformat($(ppdTotal).val());
        ttdTotalVal = accounting.unformat($(ttdTotal).val());
        indemnityPaidAmtVal = accounting.unformat($(indemnityPaidAmt).val());
            
        $(indemnityOutstandingAmt).val(accounting.formatMoney(ttdTotalVal + ppdTotalVal - indemnityPaidAmtVal));
        $(indemnityReserveAmt).val(accounting.formatMoney(ttdTotalVal + ppdTotalVal));

        LogExpenseUpdates($(this).attr("data-field-name"), $(this).val());

        SetTotalReserveAmounts();
     });

        $(ttdAmount).change(function () {
            awwAmountVal = accounting.unformat($(ttdAWWAmount).val());

           ttdAmountVal = accounting.unformat($(ttdAmount).val());
           ttdVal = accounting.unformat($(ttd).val());
           ttdCalc = 0.00;
           ttdCalc = awwAmountVal * ttdAmountVal * ttdVal;

           $(ttdTotal).val(accounting.formatMoney(ttdCalc));
           ppdTotalVal = accounting.unformat($(ppdTotal).val());
           ttdTotalVal = accounting.unformat($(ttdTotal).val());
           indemnityPaidAmtVal = accounting.unformat($(indemnityPaidAmt).val());

            $(indemnityOutstandingAmt).val(accounting.formatMoney(ttdTotalVal + ppdTotalVal - indemnityPaidAmtVal));
            $(indemnityReserveAmt).val(accounting.formatMoney(ttdTotalVal + ppdTotalVal));

            LogExpenseUpdates($(this).attr("data-field-name"), $(this).val());

            SetTotalReserveAmounts();
        });

        $(ttd).change(function () {
            awwAmountVal = accounting.unformat($(ttdAWWAmount).val());
            ttdAmountVal = accounting.unformat($(ttdAmount).val());
            ttdVal = accounting.unformat($(ttd).val());
            ppdTotalVal = accounting.unformat($(ppdTotal).val());
            ttdCalc = 0.00;
            ttdCalc = awwAmountVal * ttdAmountVal * ttdVal;
            $(ttdTotal).val(accounting.formatMoney(ttdCalc));
            ppdTotalVal = accounting.unformat($(ppdTotal).val());
            ttdTotalVal = accounting.unformat($(ttdTotal).val());
            indemnityPaidAmtVal = accounting.unformat($(indemnityPaidAmt).val());

            $(indemnityOutstandingAmt).val(accounting.formatMoney(ttdTotalVal + ppdTotalVal - indemnityPaidAmtVal));
            $(indemnityReserveAmt).val(accounting.formatMoney(ttdTotalVal + ppdTotalVal));

            LogExpenseUpdates($(this).attr("data-field-name"), $(this).val());

            SetTotalReserveAmounts();
        });
    }

    function SetOutstandingTotals(ppdTotalVal,ttdTotalVal) {
        $("#edit-dialog input[data-field-name='IndemnityOutstandingAmount']").val(accounting.formatMoney(ttdTotalVal + ppdTotalVal));
    }

    function SetPPDCalculations() {
        var ppd = $("#edit-dialog input[data-field-name='PPD']");
        var ppdTotal = $("#edit-dialog input[data-field-name='IndemnityPPDTotal']");
        var ttdTotal = $("#edit-dialog input[data-field-name='IndemnityTTDTotal']");
        var ppdAmount = $("#edit-dialog input[data-field-name='IndemnityPPDAmount']");
        var ppdSection = $("#edit-dialog input[data-field-name='PPD']").parent();
        var ppdTotalSection = $("#edit-dialog input[data-field-name='IndemnityPPDTotal']").parent();
        var indemnityReserveAmt = $("#edit-dialog input[data-field-name='IndemnityIncurredAmountTotal']");
        var indemnityOutstandingAmt = $("#edit-dialog input[data-field-name='IndemnityOutstandingAmount']");
        var indemnityPaidAmt = $("#edit-dialog input[data-field-name='IndemnityPaidAmount']");
        var ppdCalc = 0.00;
        var ppdTotalVal;
        var ttdTotalVal;
        var ppdAmountVal;
        var ppdVal;
        var indemnityReserveAmtTotal;
        var indemnityPaidAmtVal;
        var awwAmount = $("#edit-dialog input[data-field-name='AWW']");
        var ppdAWWAmount = $("#edit-dialog input[data-field-name='PPDAWWAmount']");

       ppdAWWAmount.parent().hide();
       
        var awwAmountVal = accounting.unformat($(awwAmount).val());

        if (accounting.unformat($(ppdAWWAmount).val()) > 0) {
            awwAmountVal = accounting.unformat(ppdAWWAmount.val());
        }

        $(ppdAWWAmount).val(accounting.formatMoney(awwAmountVal));
        $(ppdAmount).parent().addClass("subtotal-calc-section");
        $(ppdAmount).after($(ppdTotal));
        $(ppdAmount).after("<label>=</label>");
        $(ppdAmount).after($(ppd));
        $(ppdAmount).after("<label>X</label>");
        $(ppdAmount).before(ppdAWWAmount);
        $(ppdAmount).before("<label>X</label>");

        $(ppdSection).hide();
        $(ppdTotalSection).hide();
        $(ppdAWWAmount).attr("calc-field-name", "PPDAWWAmount");

        $(ppdAWWAmount).change(function () {
            $(this).val(accounting.formatMoney($(this).val()));
            awwAmountVal = accounting.unformat($(this).val());
            ppdAmountVal = accounting.unformat($(ppdAmount).val());
            ppdVal = accounting.unformat($(ppd).val());
            ppdCalc = 0.00;
            ppdCalc = awwAmountVal * ppdAmountVal * ppdVal;

            $(ppdTotal).val(accounting.formatMoney(ppdCalc));
            ttdTotalVal = accounting.unformat($(ttdTotal).val());
            ppdTotalVal = accounting.unformat($(ppdTotal).val());
            indemnityPaidAmtVal = accounting.unformat($(indemnityPaidAmt).val());

            indemnityReserveAmtTotal = ttdTotalVal + ppdTotalVal;

            $(indemnityOutstandingAmt).val(accounting.formatMoney(indemnityReserveAmtTotal - indemnityPaidAmtVal));
            $(indemnityReserveAmt).val(accounting.formatMoney(indemnityReserveAmtTotal));
            $(this).val(accounting.formatMoney($(this).val()));

            LogExpenseUpdates($(this).attr("data-field-name"), $(this).val());

            SetTotalReserveAmounts();
        });

        $(ppdAmount).change(function () {
            awwAmountVal = accounting.unformat($(ppdAWWAmount).val());
            ppdAmountVal = accounting.unformat($(this).val());
            ppdVal = accounting.unformat($(ppd).val());
            ppdCalc = 0.00;
            ppdCalc = awwAmountVal * ppdAmountVal * ppdVal;

            $(ppdTotal).val(accounting.formatMoney(ppdCalc));
            ttdTotalVal = accounting.unformat($(ttdTotal).val());
            ppdTotalVal = accounting.unformat($(ppdTotal).val());
            indemnityPaidAmtVal = accounting.unformat($(indemnityPaidAmt).val());

            indemnityReserveAmtTotal = ttdTotalVal + ppdTotalVal;

            $(indemnityOutstandingAmt).val(accounting.formatMoney(indemnityReserveAmtTotal - indemnityPaidAmtVal));
            $(indemnityReserveAmt).val(accounting.formatMoney(indemnityReserveAmtTotal));

            LogExpenseUpdates($(this).attr("data-field-name"), $(this).val());

            SetTotalReserveAmounts();
        });

        $(ppd).change(function () {
            awwAmountVal = accounting.unformat($(ppdAWWAmount).val());
            ppdAmountVal = accounting.unformat($(ppdAmount).val());
            ppdVal = accounting.unformat($(this).val());
            ppdCalc = 0.00;
            ppdCalc = awwAmountVal * ppdAmountVal * ppdVal;

            $(ppdTotal).val(accounting.formatMoney(ppdCalc));
            ttdTotalVal = accounting.unformat($(ttdTotal).val());
            ppdTotalVal = accounting.unformat($(ppdTotal).val());
            indemnityPaidAmtVal = accounting.unformat($(indemnityPaidAmt).val());

            indemnityReserveAmtTotal = ttdTotalVal + ppdTotalVal;

            $(indemnityOutstandingAmt).val(accounting.formatMoney(indemnityReserveAmtTotal - indemnityPaidAmtVal));
            $(indemnityReserveAmt).val(accounting.formatMoney(indemnityReserveAmtTotal));

            SetTotalReserveAmounts();
        });
    }

    function SetAWWValues()
    {
        var awwAmount = $("#edit-dialog input[data-field-name='AWW']");

        $(awwAmount).change(function () {
            var ttdAWW = $("#edit-dialog input[calc-field-name='TTDAWWAmount']");
            var ppdAWW = $("#edit-dialog input[calc-field-name='PPDAWWAmount']"); 

            $(ttdAWW).val($(awwAmount).val());
            $(ppdAWW).val($(awwAmount).val());

            $(ttdAWW).change();
            $(ppdAWW).change();

            SetTotalReserveAmounts();
        });

       $(awwAmount).change();

    }
    
    function SetExpenseCalculations() {
        var attorneyFees = $("#edit-dialog input[data-field-name='AttorneyFees']");
        var nurseCaseManagerFees = $("#edit-dialog input[data-field-name='NurseCaseManagerFees']");
        var imeCosts = $("#edit-dialog input[data-field-name='IMECosts']");
        var surveillanceCosts = $("#edit-dialog input[data-field-name='SurveillanceCosts']");
        var outsideAdjusterExpense = $("#edit-dialog input[data-field-name='OutsideAdjusterExpense']");
        var billReviewFees = $("#edit-dialog input[data-field-name='BillReviewFees']");
        var urReviews = $("#edit-dialog input[data-field-name='URReviews']");
        var ediExpenses = $("#edit-dialog input[data-field-name='EDIExpenses']");
        var vocRehabCosts = $("#edit-dialog input[data-field-name='VocRehabCosts']");
        var witnessFees = $("#edit-dialog input[data-field-name='WitnessFees']");
        var axReconstructionFee = $("#edit-dialog input[data-field-name='AxReconstructionFee']");
        var defenseAttorneyFees = $("#edit-dialog input[data-field-name='DefenseAttorneyFees']");
        var miscLegalExpensesAmount = $("#edit-dialog input[data-field-name='MiscLegalExpensesAmount']");
        var lostWages = $("#edit-dialog input[data-field-name='LostWages']");
        var estimatedPAndS = $("#edit-dialog input[data-field-name='EstimatedPAndS']");
        var expertFees = $("#edit-dialog input[data-field-name='ExpertFees']");
        var expensesTotal = 0.00;

        var expensesIncurredAmt = $("#edit-dialog input[data-field-name='ExpensesTotalIncurredAmount']");

        var expensesAllocatedAmtTotal = 0.00;
        var expensesUnallocatedAmtTotal = 0.00;
        var expensesOutstandingAmtTotal = 0.00;
        var expensesOutstandingAmt = $("#edit-dialog input[data-field-name='ExpensesOutstandingAmountTotal']");
        var expensesUnallocatedAmt = $("#edit-dialog input[data-field-name='ExpensesUnallocatedPaidAmount']");
        var expensesAllocatedAmt = $("#edit-dialog input[data-field-name='ExpensesAllocatedPaidAmount']");
        
        $(attorneyFees).blur(function () {
            try {
                expensesTotal = accounting.unformat($(attorneyFees).val()) + accounting.unformat($(nurseCaseManagerFees).val()) +
                             accounting.unformat($(imeCosts).val()) + accounting.unformat($(surveillanceCosts).val()) +
                             accounting.unformat($(outsideAdjusterExpense).val()) + accounting.unformat($(billReviewFees).val()) +
                             accounting.unformat($(urReviews).val()) + accounting.unformat($(ediExpenses).val()) +
                             accounting.unformat($(vocRehabCosts).val()) + accounting.unformat($(witnessFees).val()) +
                             accounting.unformat($(axReconstructionFee).val()) + accounting.unformat($(defenseAttorneyFees).val()) +
                             accounting.unformat($(miscLegalExpensesAmount).val()) + accounting.unformat($(lostWages).val()) +
                             accounting.unformat($(estimatedPAndS).val()) + accounting.unformat($(expertFees).val());

                expensesAllocatedAmtTotal = accounting.unformat($(expensesAllocatedAmt).val());
                expensesUnallocatedAmtTotal = accounting.unformat($(expensesUnallocatedAmt).val());
                expensesOutstandingAmtTotal = expensesTotal - (expensesAllocatedAmtTotal + expensesUnallocatedAmtTotal);

                $(expensesOutstandingAmt).val(accounting.formatMoney(expensesOutstandingAmtTotal));
                $(expensesAllocatedAmt).val(accounting.formatMoney(expensesAllocatedAmtTotal));
                $(expensesUnallocatedAmt).val(accounting.formatMoney(expensesUnallocatedAmtTotal));

                $(expensesIncurredAmt).val(accounting.formatMoney(expensesTotal));
                $(this).val(accounting.formatMoney($(this).val()));

                SetTotalReserveAmounts();
            }
            catch (e) {
                alert(e.message);
            }
        });

        $(estimatedPAndS).blur(function () {
            try {
                expensesTotal = accounting.unformat($(attorneyFees).val()) + accounting.unformat($(nurseCaseManagerFees).val()) +
                             accounting.unformat($(imeCosts).val()) + accounting.unformat($(surveillanceCosts).val()) +
                             accounting.unformat($(outsideAdjusterExpense).val()) + accounting.unformat($(billReviewFees).val()) +
                             accounting.unformat($(urReviews).val()) + accounting.unformat($(ediExpenses).val()) +
                             accounting.unformat($(vocRehabCosts).val()) + accounting.unformat($(witnessFees).val()) +
                             accounting.unformat($(axReconstructionFee).val()) + accounting.unformat($(defenseAttorneyFees).val()) +
                             accounting.unformat($(miscLegalExpensesAmount).val()) + accounting.unformat($(lostWages).val()) +
                             accounting.unformat($(estimatedPAndS).val()) + accounting.unformat($(expertFees).val());

                expensesAllocatedAmtTotal = accounting.unformat($(expensesAllocatedAmt).val());
                expensesUnallocatedAmtTotal = accounting.unformat($(expensesUnallocatedAmt).val());
                expensesOutstandingAmtTotal = expensesTotal - (expensesAllocatedAmtTotal + expensesUnallocatedAmtTotal);

                $(expensesOutstandingAmt).val(accounting.formatMoney(expensesOutstandingAmtTotal));
                $(expensesAllocatedAmt).val(accounting.formatMoney(expensesAllocatedAmtTotal));
                $(expensesUnallocatedAmt).val(accounting.formatMoney(expensesUnallocatedAmtTotal));

                $(expensesIncurredAmt).val(accounting.formatMoney(expensesTotal));
                $(this).val(accounting.formatMoney($(this).val()));

                SetTotalReserveAmounts();
            }
            catch (e) {
                alert(e.message);
            }
        });

        $(lostWages).blur(function () {
            try {
                expensesTotal = accounting.unformat($(attorneyFees).val()) + accounting.unformat($(nurseCaseManagerFees).val()) +
                             accounting.unformat($(imeCosts).val()) + accounting.unformat($(surveillanceCosts).val()) +
                             accounting.unformat($(outsideAdjusterExpense).val()) + accounting.unformat($(billReviewFees).val()) +
                             accounting.unformat($(urReviews).val()) + accounting.unformat($(ediExpenses).val()) +
                             accounting.unformat($(vocRehabCosts).val()) + accounting.unformat($(witnessFees).val()) +
                             accounting.unformat($(axReconstructionFee).val()) + accounting.unformat($(defenseAttorneyFees).val()) +
                             accounting.unformat($(miscLegalExpensesAmount).val()) + accounting.unformat($(lostWages).val()) +
                             accounting.unformat($(estimatedPAndS).val()) + accounting.unformat($(expertFees).val());

                expensesAllocatedAmtTotal = accounting.unformat($(expensesAllocatedAmt).val());
                expensesUnallocatedAmtTotal = accounting.unformat($(expensesUnallocatedAmt).val());
                expensesOutstandingAmtTotal = expensesTotal - (expensesAllocatedAmtTotal + expensesUnallocatedAmtTotal);

                $(expensesOutstandingAmt).val(accounting.formatMoney(expensesOutstandingAmtTotal));
                $(expensesAllocatedAmt).val(accounting.formatMoney(expensesAllocatedAmtTotal));
                $(expensesUnallocatedAmt).val(accounting.formatMoney(expensesUnallocatedAmtTotal));

                $(expensesIncurredAmt).val(accounting.formatMoney(expensesTotal));
                $(this).val(accounting.formatMoney($(this).val()));

                SetTotalReserveAmounts();
            }
            catch (e) {
                alert(e.message);
            }
        });

        $(miscLegalExpensesAmount).blur(function () {
            try {
                expensesTotal = accounting.unformat($(attorneyFees).val()) + accounting.unformat($(nurseCaseManagerFees).val()) +
                             accounting.unformat($(imeCosts).val()) + accounting.unformat($(surveillanceCosts).val()) +
                             accounting.unformat($(outsideAdjusterExpense).val()) + accounting.unformat($(billReviewFees).val()) +
                             accounting.unformat($(urReviews).val()) + accounting.unformat($(ediExpenses).val()) +
                             accounting.unformat($(vocRehabCosts).val()) + accounting.unformat($(witnessFees).val()) +
                             accounting.unformat($(axReconstructionFee).val()) + accounting.unformat($(defenseAttorneyFees).val()) +
                             accounting.unformat($(miscLegalExpensesAmount).val()) + accounting.unformat($(lostWages).val()) +
                             accounting.unformat($(estimatedPAndS).val()) + accounting.unformat($(expertFees).val());

                expensesAllocatedAmtTotal = accounting.unformat($(expensesAllocatedAmt).val());
                expensesUnallocatedAmtTotal = accounting.unformat($(expensesUnallocatedAmt).val());
                expensesOutstandingAmtTotal = expensesTotal - (expensesAllocatedAmtTotal + expensesUnallocatedAmtTotal);

                $(expensesOutstandingAmt).val(accounting.formatMoney(expensesOutstandingAmtTotal));
                $(expensesAllocatedAmt).val(accounting.formatMoney(expensesAllocatedAmtTotal));
                $(expensesUnallocatedAmt).val(accounting.formatMoney(expensesUnallocatedAmtTotal));

                $(expensesIncurredAmt).val(accounting.formatMoney(expensesTotal));
                $(this).val(accounting.formatMoney($(this).val()));

                SetTotalReserveAmounts();
            }
            catch (e) {
                alert(e.message);
            }
        });

        $(defenseAttorneyFees).blur(function () {
            try {
                expensesTotal = accounting.unformat($(attorneyFees).val()) + accounting.unformat($(nurseCaseManagerFees).val()) +
                             accounting.unformat($(imeCosts).val()) + accounting.unformat($(surveillanceCosts).val()) +
                             accounting.unformat($(outsideAdjusterExpense).val()) + accounting.unformat($(billReviewFees).val()) +
                             accounting.unformat($(urReviews).val()) + accounting.unformat($(ediExpenses).val()) +
                             accounting.unformat($(vocRehabCosts).val()) + accounting.unformat($(witnessFees).val()) +
                             accounting.unformat($(axReconstructionFee).val()) + accounting.unformat($(defenseAttorneyFees).val()) +
                             accounting.unformat($(miscLegalExpensesAmount).val()) + accounting.unformat($(lostWages).val()) +
                             accounting.unformat($(estimatedPAndS).val()) + accounting.unformat($(expertFees).val());

                expensesAllocatedAmtTotal = accounting.unformat($(expensesAllocatedAmt).val());
                expensesUnallocatedAmtTotal = accounting.unformat($(expensesUnallocatedAmt).val());
                expensesOutstandingAmtTotal = expensesTotal - (expensesAllocatedAmtTotal + expensesUnallocatedAmtTotal);

                $(expensesOutstandingAmt).val(accounting.formatMoney(expensesOutstandingAmtTotal));
                $(expensesAllocatedAmt).val(accounting.formatMoney(expensesAllocatedAmtTotal));
                $(expensesUnallocatedAmt).val(accounting.formatMoney(expensesUnallocatedAmtTotal));

                $(expensesIncurredAmt).val(accounting.formatMoney(expensesTotal));
                $(this).val(accounting.formatMoney($(this).val()));

                SetTotalReserveAmounts();
            }
            catch (e) {
                alert(e.message);
            }
        });

        $(axReconstructionFee).blur(function () {
            try {
                expensesTotal = accounting.unformat($(attorneyFees).val()) + accounting.unformat($(nurseCaseManagerFees).val()) +
                             accounting.unformat($(imeCosts).val()) + accounting.unformat($(surveillanceCosts).val()) +
                             accounting.unformat($(outsideAdjusterExpense).val()) + accounting.unformat($(billReviewFees).val()) +
                             accounting.unformat($(urReviews).val()) + accounting.unformat($(ediExpenses).val()) +
                             accounting.unformat($(vocRehabCosts).val()) + accounting.unformat($(witnessFees).val()) +
                             accounting.unformat($(axReconstructionFee).val()) + accounting.unformat($(defenseAttorneyFees).val()) +
                             accounting.unformat($(miscLegalExpensesAmount).val()) + accounting.unformat($(lostWages).val()) +
                             accounting.unformat($(estimatedPAndS).val()) + accounting.unformat($(expertFees).val());

                expensesAllocatedAmtTotal = accounting.unformat($(expensesAllocatedAmt).val());
                expensesUnallocatedAmtTotal = accounting.unformat($(expensesUnallocatedAmt).val());
                expensesOutstandingAmtTotal = expensesTotal - (expensesAllocatedAmtTotal + expensesUnallocatedAmtTotal);

                $(expensesOutstandingAmt).val(accounting.formatMoney(expensesOutstandingAmtTotal));
                $(expensesAllocatedAmt).val(accounting.formatMoney(expensesAllocatedAmtTotal));
                $(expensesUnallocatedAmt).val(accounting.formatMoney(expensesUnallocatedAmtTotal));

                $(expensesIncurredAmt).val(accounting.formatMoney(expensesTotal));
                $(this).val(accounting.formatMoney($(this).val()));

                SetTotalReserveAmounts();
            }
            catch (e) {
                alert(e.message);
            }
        });

        $(witnessFees).blur(function () {
            try {
                expensesTotal = accounting.unformat($(attorneyFees).val()) + accounting.unformat($(nurseCaseManagerFees).val()) +
                    accounting.unformat($(imeCosts).val()) + accounting.unformat($(surveillanceCosts).val()) +
                    accounting.unformat($(outsideAdjusterExpense).val()) + accounting.unformat($(billReviewFees).val()) +
                    accounting.unformat($(urReviews).val()) + accounting.unformat($(ediExpenses).val()) +
                    accounting.unformat($(vocRehabCosts).val()) + accounting.unformat($(witnessFees).val()) +
                    accounting.unformat($(axReconstructionFee).val()) + accounting.unformat($(defenseAttorneyFees).val()) +
                    accounting.unformat($(miscLegalExpensesAmount).val()) + accounting.unformat($(lostWages).val()) +
                    accounting.unformat($(estimatedPAndS).val()) + accounting.unformat($(expertFees).val());

                expensesAllocatedAmtTotal = accounting.unformat($(expensesAllocatedAmt).val());
                expensesUnallocatedAmtTotal = accounting.unformat($(expensesUnallocatedAmt).val());
                expensesOutstandingAmtTotal = expensesTotal - (expensesAllocatedAmtTotal + expensesUnallocatedAmtTotal);

                $(expensesOutstandingAmt).val(accounting.formatMoney(expensesOutstandingAmtTotal));
                $(expensesAllocatedAmt).val(accounting.formatMoney(expensesAllocatedAmtTotal));
                $(expensesUnallocatedAmt).val(accounting.formatMoney(expensesUnallocatedAmtTotal));

                $(expensesIncurredAmt).val(accounting.formatMoney(expensesTotal));
                $(this).val(accounting.formatMoney($(this).val()));

                SetTotalReserveAmounts();
            }
            catch (e) {
                alert(e.message);
            }
        });

        $(vocRehabCosts).blur(function () {
            try {
                expensesTotal = accounting.unformat($(attorneyFees).val()) + accounting.unformat($(nurseCaseManagerFees).val()) +
                             accounting.unformat($(imeCosts).val()) + accounting.unformat($(surveillanceCosts).val()) +
                             accounting.unformat($(outsideAdjusterExpense).val()) + accounting.unformat($(billReviewFees).val()) +
                             accounting.unformat($(urReviews).val()) + accounting.unformat($(ediExpenses).val()) +
                             accounting.unformat($(vocRehabCosts).val()) + accounting.unformat($(witnessFees).val()) +
                             accounting.unformat($(axReconstructionFee).val()) + accounting.unformat($(defenseAttorneyFees).val()) +
                             accounting.unformat($(miscLegalExpensesAmount).val()) + accounting.unformat($(lostWages).val()) +
                             accounting.unformat($(estimatedPAndS).val()) + accounting.unformat($(expertFees).val());

                expensesAllocatedAmtTotal = accounting.unformat($(expensesAllocatedAmt).val());
                expensesUnallocatedAmtTotal = accounting.unformat($(expensesUnallocatedAmt).val());
                expensesOutstandingAmtTotal = expensesTotal - (expensesAllocatedAmtTotal + expensesUnallocatedAmtTotal);

                $(expensesOutstandingAmt).val(accounting.formatMoney(expensesOutstandingAmtTotal));
                $(expensesAllocatedAmt).val(accounting.formatMoney(expensesAllocatedAmtTotal));
                $(expensesUnallocatedAmt).val(accounting.formatMoney(expensesUnallocatedAmtTotal));

                $(expensesIncurredAmt).val(accounting.formatMoney(expensesTotal));
                $(this).val(accounting.formatMoney($(this).val()));

                SetTotalReserveAmounts();
            }
            catch (e) {
                alert(e.message);
            }
        });

        $(ediExpenses).blur(function () {
            try {
                expensesTotal = accounting.unformat($(attorneyFees).val()) + accounting.unformat($(nurseCaseManagerFees).val()) +
                             accounting.unformat($(imeCosts).val()) + accounting.unformat($(surveillanceCosts).val()) +
                             accounting.unformat($(outsideAdjusterExpense).val()) + accounting.unformat($(billReviewFees).val()) +
                             accounting.unformat($(urReviews).val()) + accounting.unformat($(ediExpenses).val()) +
                             accounting.unformat($(vocRehabCosts).val()) + accounting.unformat($(witnessFees).val()) +
                             accounting.unformat($(axReconstructionFee).val()) + accounting.unformat($(defenseAttorneyFees).val()) +
                             accounting.unformat($(miscLegalExpensesAmount).val()) + accounting.unformat($(lostWages).val()) +
                             accounting.unformat($(estimatedPAndS).val()) + accounting.unformat($(expertFees).val());

                expensesAllocatedAmtTotal = accounting.unformat($(expensesAllocatedAmt).val());
                expensesUnallocatedAmtTotal = accounting.unformat($(expensesUnallocatedAmt).val());
                expensesOutstandingAmtTotal = expensesTotal - (expensesAllocatedAmtTotal + expensesUnallocatedAmtTotal);

                $(expensesOutstandingAmt).val(accounting.formatMoney(expensesOutstandingAmtTotal));
                $(expensesAllocatedAmt).val(accounting.formatMoney(expensesAllocatedAmtTotal));
                $(expensesUnallocatedAmt).val(accounting.formatMoney(expensesUnallocatedAmtTotal));

                $(expensesIncurredAmt).val(accounting.formatMoney(expensesTotal));
                $(this).val(accounting.formatMoney($(this).val()));

                SetTotalReserveAmounts();
            }
            catch (e) {
                alert(e.message);
            }
        });

        $(urReviews).blur(function () {
            try {
                expensesTotal = accounting.unformat($(attorneyFees).val()) + accounting.unformat($(nurseCaseManagerFees).val()) +
                             accounting.unformat($(imeCosts).val()) + accounting.unformat($(surveillanceCosts).val()) +
                             accounting.unformat($(outsideAdjusterExpense).val()) + accounting.unformat($(billReviewFees).val()) +
                             accounting.unformat($(urReviews).val()) + accounting.unformat($(ediExpenses).val()) +
                             accounting.unformat($(vocRehabCosts).val()) + accounting.unformat($(witnessFees).val()) +
                             accounting.unformat($(axReconstructionFee).val()) + accounting.unformat($(defenseAttorneyFees).val()) +
                             accounting.unformat($(miscLegalExpensesAmount).val()) + accounting.unformat($(lostWages).val()) +
                             accounting.unformat($(estimatedPAndS).val()) + accounting.unformat($(expertFees).val());

                expensesAllocatedAmtTotal = accounting.unformat($(expensesAllocatedAmt).val());
                expensesUnallocatedAmtTotal = accounting.unformat($(expensesUnallocatedAmt).val());
                expensesOutstandingAmtTotal = expensesTotal - (expensesAllocatedAmtTotal + expensesUnallocatedAmtTotal);

                $(expensesOutstandingAmt).val(accounting.formatMoney(expensesOutstandingAmtTotal));
                $(expensesAllocatedAmt).val(accounting.formatMoney(expensesAllocatedAmtTotal));
                $(expensesUnallocatedAmt).val(accounting.formatMoney(expensesUnallocatedAmtTotal));

                $(expensesIncurredAmt).val(accounting.formatMoney(expensesTotal));
                $(this).val(accounting.formatMoney($(this).val()));

                SetTotalReserveAmounts();
            }
            catch (e) {
                alert(e.message);
            }
        });

        $(billReviewFees).blur(function () {
            try {
                expensesTotal = accounting.unformat($(attorneyFees).val()) + accounting.unformat($(nurseCaseManagerFees).val()) +
                             accounting.unformat($(imeCosts).val()) + accounting.unformat($(surveillanceCosts).val()) +
                             accounting.unformat($(outsideAdjusterExpense).val()) + accounting.unformat($(billReviewFees).val()) +
                             accounting.unformat($(urReviews).val()) + accounting.unformat($(ediExpenses).val()) +
                             accounting.unformat($(vocRehabCosts).val()) + accounting.unformat($(witnessFees).val()) +
                             accounting.unformat($(axReconstructionFee).val()) + accounting.unformat($(defenseAttorneyFees).val()) +
                             accounting.unformat($(miscLegalExpensesAmount).val()) + accounting.unformat($(lostWages).val()) +
                             accounting.unformat($(estimatedPAndS).val()) + accounting.unformat($(expertFees).val());

                expensesAllocatedAmtTotal = accounting.unformat($(expensesAllocatedAmt).val());
                expensesUnallocatedAmtTotal = accounting.unformat($(expensesUnallocatedAmt).val());
                expensesOutstandingAmtTotal = expensesTotal - (expensesAllocatedAmtTotal + expensesUnallocatedAmtTotal);

                $(expensesOutstandingAmt).val(accounting.formatMoney(expensesOutstandingAmtTotal));
                $(expensesAllocatedAmt).val(accounting.formatMoney(expensesAllocatedAmtTotal));
                $(expensesUnallocatedAmt).val(accounting.formatMoney(expensesUnallocatedAmtTotal));

                $(expensesIncurredAmt).val(accounting.formatMoney(expensesTotal));
                $(this).val(accounting.formatMoney($(this).val()));

                SetTotalReserveAmounts();
            }
            catch (e) {
                alert(e.message);
            }
        });

        $(outsideAdjusterExpense).blur(function () {
            try {
                expensesTotal = accounting.unformat($(attorneyFees).val()) + accounting.unformat($(nurseCaseManagerFees).val()) +
                             accounting.unformat($(imeCosts).val()) + accounting.unformat($(surveillanceCosts).val()) +
                             accounting.unformat($(outsideAdjusterExpense).val()) + accounting.unformat($(billReviewFees).val()) +
                             accounting.unformat($(urReviews).val()) + accounting.unformat($(ediExpenses).val()) +
                             accounting.unformat($(vocRehabCosts).val()) + accounting.unformat($(witnessFees).val()) +
                             accounting.unformat($(axReconstructionFee).val()) + accounting.unformat($(defenseAttorneyFees).val()) +
                             accounting.unformat($(miscLegalExpensesAmount).val()) + accounting.unformat($(lostWages).val()) +
                             accounting.unformat($(estimatedPAndS).val()) + accounting.unformat($(expertFees).val());

                expensesAllocatedAmtTotal = accounting.unformat($(expensesAllocatedAmt).val());
                expensesUnallocatedAmtTotal = accounting.unformat($(expensesUnallocatedAmt).val());
                expensesOutstandingAmtTotal = expensesTotal - (expensesAllocatedAmtTotal + expensesUnallocatedAmtTotal);

                $(expensesOutstandingAmt).val(accounting.formatMoney(expensesOutstandingAmtTotal));
                $(expensesAllocatedAmt).val(accounting.formatMoney(expensesAllocatedAmtTotal));
                $(expensesUnallocatedAmt).val(accounting.formatMoney(expensesUnallocatedAmtTotal));

                $(expensesIncurredAmt).val(accounting.formatMoney(expensesTotal));
                $(this).val(accounting.formatMoney($(this).val()));

                SetTotalReserveAmounts();
            }
            catch (e) {
                alert(e.message);
            }
        });

        $(surveillanceCosts).blur(function () {
            try {
                expensesTotal = accounting.unformat($(attorneyFees).val()) + accounting.unformat($(nurseCaseManagerFees).val()) +
                             accounting.unformat($(imeCosts).val()) + accounting.unformat($(surveillanceCosts).val()) +
                             accounting.unformat($(outsideAdjusterExpense).val()) + accounting.unformat($(billReviewFees).val()) +
                             accounting.unformat($(urReviews).val()) + accounting.unformat($(ediExpenses).val()) +
                             accounting.unformat($(vocRehabCosts).val()) + accounting.unformat($(witnessFees).val()) +
                             accounting.unformat($(axReconstructionFee).val()) + accounting.unformat($(defenseAttorneyFees).val()) +
                             accounting.unformat($(miscLegalExpensesAmount).val()) + accounting.unformat($(lostWages).val()) +
                             accounting.unformat($(estimatedPAndS).val()) + accounting.unformat($(expertFees).val());

                expensesAllocatedAmtTotal = accounting.unformat($(expensesAllocatedAmt).val());
                expensesUnallocatedAmtTotal = accounting.unformat($(expensesUnallocatedAmt).val());
                expensesOutstandingAmtTotal = expensesTotal - (expensesAllocatedAmtTotal + expensesUnallocatedAmtTotal);

                $(expensesOutstandingAmt).val(accounting.formatMoney(expensesOutstandingAmtTotal));
                $(expensesAllocatedAmt).val(accounting.formatMoney(expensesAllocatedAmtTotal));
                $(expensesUnallocatedAmt).val(accounting.formatMoney(expensesUnallocatedAmtTotal));

                $(expensesIncurredAmt).val(accounting.formatMoney(expensesTotal));
                $(this).val(accounting.formatMoney($(this).val()));

                SetTotalReserveAmounts();
            }
            catch (e) {
                alert(e.message);
            }
        });

        $(imeCosts).blur(function () {
            try {
                expensesTotal = accounting.unformat($(attorneyFees).val()) + accounting.unformat($(nurseCaseManagerFees).val()) +
                             accounting.unformat($(imeCosts).val()) + accounting.unformat($(surveillanceCosts).val()) +
                             accounting.unformat($(outsideAdjusterExpense).val()) + accounting.unformat($(billReviewFees).val()) +
                             accounting.unformat($(urReviews).val()) + accounting.unformat($(ediExpenses).val()) +
                             accounting.unformat($(vocRehabCosts).val()) + accounting.unformat($(witnessFees).val()) +
                             accounting.unformat($(axReconstructionFee).val()) + accounting.unformat($(defenseAttorneyFees).val()) +
                             accounting.unformat($(miscLegalExpensesAmount).val()) + accounting.unformat($(lostWages).val()) +
                    accounting.unformat($(estimatedPAndS).val()) + accounting.unformat($(expertFees).val());

                expensesAllocatedAmtTotal = accounting.unformat($(expensesAllocatedAmt).val());
                expensesUnallocatedAmtTotal = accounting.unformat($(expensesUnallocatedAmt).val());
                expensesOutstandingAmtTotal = expensesTotal - (expensesAllocatedAmtTotal + expensesUnallocatedAmtTotal);

                $(expensesOutstandingAmt).val(accounting.formatMoney(expensesOutstandingAmtTotal));
                $(expensesAllocatedAmt).val(accounting.formatMoney(expensesAllocatedAmtTotal));
                $(expensesUnallocatedAmt).val(accounting.formatMoney(expensesUnallocatedAmtTotal));

                $(expensesIncurredAmt).val(accounting.formatMoney(expensesTotal));
                $(this).val(accounting.formatMoney($(this).val()));

                SetTotalReserveAmounts();
            }
            catch (e) {
                alert(e.message);
            }
        });

        $(nurseCaseManagerFees).blur(function () {
            try {
                expensesTotal = accounting.unformat($(attorneyFees).val()) + accounting.unformat($(nurseCaseManagerFees).val()) +
                             accounting.unformat($(imeCosts).val()) + accounting.unformat($(surveillanceCosts).val()) +
                             accounting.unformat($(outsideAdjusterExpense).val()) + accounting.unformat($(billReviewFees).val()) +
                             accounting.unformat($(urReviews).val()) + accounting.unformat($(ediExpenses).val()) +
                             accounting.unformat($(vocRehabCosts).val()) + accounting.unformat($(witnessFees).val()) +
                             accounting.unformat($(axReconstructionFee).val()) + accounting.unformat($(defenseAttorneyFees).val()) +
                             accounting.unformat($(miscLegalExpensesAmount).val()) + accounting.unformat($(lostWages).val()) +
                    accounting.unformat($(estimatedPAndS).val()) + accounting.unformat($(expertFees).val());

                expensesAllocatedAmtTotal = accounting.unformat($(expensesAllocatedAmt).val());
                expensesUnallocatedAmtTotal = accounting.unformat($(expensesUnallocatedAmt).val());
                expensesOutstandingAmtTotal = expensesTotal - (expensesAllocatedAmtTotal + expensesUnallocatedAmtTotal);

                $(expensesOutstandingAmt).val(accounting.formatMoney(expensesOutstandingAmtTotal));
                $(expensesAllocatedAmt).val(accounting.formatMoney(expensesAllocatedAmtTotal));
                $(expensesUnallocatedAmt).val(accounting.formatMoney(expensesUnallocatedAmtTotal));

                $(expensesIncurredAmt).val(accounting.formatMoney(expensesTotal));
                $(this).val(accounting.formatMoney($(this).val()));

                SetTotalReserveAmounts();
            }
            catch (e) {
                alert(e.message);
            }
        });

        $(expertFees).blur(function () {
            try {
                expensesTotal = accounting.unformat($(attorneyFees).val()) + accounting.unformat($(nurseCaseManagerFees).val()) +
                    accounting.unformat($(imeCosts).val()) + accounting.unformat($(surveillanceCosts).val()) +
                    accounting.unformat($(outsideAdjusterExpense).val()) + accounting.unformat($(billReviewFees).val()) +
                    accounting.unformat($(urReviews).val()) + accounting.unformat($(ediExpenses).val()) +
                    accounting.unformat($(vocRehabCosts).val()) + accounting.unformat($(witnessFees).val()) +
                    accounting.unformat($(axReconstructionFee).val()) + accounting.unformat($(defenseAttorneyFees).val()) +
                    accounting.unformat($(miscLegalExpensesAmount).val()) + accounting.unformat($(lostWages).val()) +
                    accounting.unformat($(estimatedPAndS).val()) + accounting.unformat($(expertFees).val());

                expensesAllocatedAmtTotal = accounting.unformat($(expensesAllocatedAmt).val());
                expensesUnallocatedAmtTotal = accounting.unformat($(expensesUnallocatedAmt).val());
                expensesOutstandingAmtTotal = expensesTotal - (expensesAllocatedAmtTotal + expensesUnallocatedAmtTotal);

                $(expensesOutstandingAmt).val(accounting.formatMoney(expensesOutstandingAmtTotal));
                $(expensesAllocatedAmt).val(accounting.formatMoney(expensesAllocatedAmtTotal));
                $(expensesUnallocatedAmt).val(accounting.formatMoney(expensesUnallocatedAmtTotal));

                $(expensesIncurredAmt).val(accounting.formatMoney(expensesTotal));
                $(this).val(accounting.formatMoney($(this).val()));

                SetTotalReserveAmounts();
            }
            catch (e) {
                alert(e.message);
            }
        });
    }

    function GetExpenseCalculations() {
        var attorneyFees = $("#edit-dialog input[data-field-name='AttorneyFees']");
        var nurseCaseManagerFees = $("#edit-dialog input[data-field-name='NurseCaseManagerFees']");
        var imeCosts = $("#edit-dialog input[data-field-name='IMECosts']");
        var surveillanceCosts = $("#edit-dialog input[data-field-name='SurveillanceCosts']");
        var outsideAdjusterExpense = $("#edit-dialog input[data-field-name='OutsideAdjusterExpense']");
        var billReviewFees = $("#edit-dialog input[data-field-name='BillReviewFees']");
        var urReviews = $("#edit-dialog input[data-field-name='URReviews']");
        var ediExpenses = $("#edit-dialog input[data-field-name='EDIExpenses']");
        var vocRehabCosts = $("#edit-dialog input[data-field-name='VocRehabCosts']");
        var witnessExpertFees = $("#edit-dialog input[data-field-name='WitnessExpertFees']");
        var axReconstructionFee = $("#edit-dialog input[data-field-name='AxReconstructionFee']");
        var defenseAttorneyFees = $("#edit-dialog input[data-field-name='DefenseAttorneyFees']");
        var miscLegalExpensesAmount = $("#edit-dialog input[data-field-name='MiscLegalExpensesAmount']");
        var lostWages = $("#edit-dialog input[data-field-name='LostWages']");
        var estimatedPAndS = $("#edit-dialog input[data-field-name='EstimatedPAndS']");
        var expensesReservedAmt = $("#edit-dialog input[data-field-name='ExpensesTotalIncurredAmount']");
        var expensesTotal = 0.00;

        expensesTotal = accounting.unformat($(attorneyFees).val()) + accounting.unformat($(nurseCaseManagerFees).val()) +
                             accounting.unformat($(imeCosts).val()) + accounting.unformat($(surveillanceCosts).val()) +
                             accounting.unformat($(outsideAdjusterExpense).val()) + accounting.unformat($(billReviewFees).val()) +
                             accounting.unformat($(urReviews).val()) + accounting.unformat($(ediExpenses).val()) +
                             accounting.unformat($(vocRehabCosts).val()) + accounting.unformat($(witnessExpertFees).val()) +
                             accounting.unformat($(axReconstructionFee).val()) + accounting.unformat($(defenseAttorneyFees).val()) +
                             accounting.unformat($(miscLegalExpensesAmount).val()) + accounting.unformat($(lostWages).val()) +
                            accounting.unformat($(estimatedPAndS).val());

        return expensesTotal;
    }

    function GetMedicalCalculations() {
        var medical = $("#edit-dialog input[data-field-name='Medical']");
        var hospitalER = $("#edit-dialog input[data-field-name='HospitalER']");
        var dr = $("#edit-dialog input[data-field-name='DR']");
        var pt = $("#edit-dialog input[data-field-name='PT']");
        var tests = $("#edit-dialog input[data-field-name='Tests']");
        var misc = $("#edit-dialog input[data-field-name='Misc']");

        var medicalTotal = 0.00;

        medicalTotal = accounting.unformat($(medical).val()) + accounting.unformat($(hospitalER).val()) +
                        accounting.unformat($(dr).val()) + accounting.unformat($(pt).val()) +
                        accounting.unformat($(tests).val()) + accounting.unformat($(misc).val());

        return medicalTotal;
    }

    function GetPPDAndTTDCalculations() {
        var ttdTotal = accounting.unformat($("#edit-dialog input[data-field-name='IndemnityTTDTotal']").val());
        var ppdTotal = accounting.unformat($("#edit-dialog input[data-field-name='IndemnityPPDTotal']").val());
        var grandTotal = ttdTotal + ppdTotal;

        return grandTotal;
    }

    function SetMedicalCalculations() {
        var medical = $("#edit-dialog input[data-field-name='Medical']");
        var hospitalER = $("#edit-dialog input[data-field-name='HospitalER']");
        var dr = $("#edit-dialog input[data-field-name='DR']");
        var pt = $("#edit-dialog input[data-field-name='PT']");
        var tests = $("#edit-dialog input[data-field-name='Tests']");
        var misc = $("#edit-dialog input[data-field-name='MiscMedicalExpense']");
        var medicalOutstandingAmt = $("#edit-dialog input[data-field-name='MedicalOutstandingAmount']");
        var medicalIncurredAmtTotal = $("#edit-dialog input[data-field-name='MedicalIncurredAmountTotal']");
        var medicalTotal = 0.00;
        var medicalPaidAmtVal = 0.00;
        var medicalPaidAmt = $("#edit-dialog input[data-field-name='MedicalPaidAmount']");

        $(medical).blur(function () {
            medicalTotal = accounting.unformat($(medical).val()) + accounting.unformat($(hospitalER).val()) +
                accounting.unformat($(dr).val()) + accounting.unformat($(pt).val()) +
                accounting.unformat($(tests).val()) + accounting.unformat($(misc).val());
            
            medicalPaidAmtVal = accounting.unformat($(medicalPaidAmt).val());

            $(medicalOutstandingAmt).val(accounting.formatMoney(medicalTotal-medicalPaidAmtVal));
            $(medicalIncurredAmtTotal).val(accounting.formatMoney(medicalTotal));
            $(this).val(accounting.formatMoney($(this).val()));

            SetTotalReserveAmounts();
        });

        $(hospitalER).blur(function () {
            medicalTotal = accounting.unformat($(medical).val()) + accounting.unformat($(hospitalER).val()) +
                accounting.unformat($(dr).val()) + accounting.unformat($(pt).val()) +
                accounting.unformat($(tests).val()) + accounting.unformat($(misc).val());

            medicalPaidAmtVal = accounting.unformat($(medicalPaidAmt).val());

            $(medicalOutstandingAmt).val(accounting.formatMoney(medicalTotal - medicalPaidAmtVal));
            $(medicalIncurredAmtTotal).val(accounting.formatMoney(medicalTotal));
            $(this).val(accounting.formatMoney($(this).val()));

            SetTotalReserveAmounts();
        });

        $(dr).blur(function () {
            medicalTotal = accounting.unformat($(medical).val()) + accounting.unformat($(hospitalER).val()) +
                accounting.unformat($(dr).val()) + accounting.unformat($(pt).val()) +
                accounting.unformat($(tests).val()) + accounting.unformat($(misc).val());

            medicalPaidAmtVal = accounting.unformat($(medicalPaidAmt).val());

            $(medicalOutstandingAmt).val(accounting.formatMoney(medicalTotal - medicalPaidAmtVal));
            $(medicalIncurredAmtTotal).val(accounting.formatMoney(medicalTotal));
            $(this).val(accounting.formatMoney($(this).val()));

            SetTotalReserveAmounts();
        });

        $(pt).blur(function () {
            medicalTotal = accounting.unformat($(medical).val()) + accounting.unformat($(hospitalER).val()) +
                accounting.unformat($(dr).val()) + accounting.unformat($(pt).val()) +
                accounting.unformat($(tests).val()) + accounting.unformat($(misc).val());

            medicalPaidAmtVal = accounting.unformat($(medicalPaidAmt).val());

            $(medicalOutstandingAmt).val(accounting.formatMoney(medicalTotal - medicalPaidAmtVal));
            $(medicalIncurredAmtTotal).val(accounting.formatMoney(medicalTotal));
            $(this).val(accounting.formatMoney($(this).val()));

            SetTotalReserveAmounts();
        });

        $(tests).blur(function () {
            medicalTotal = accounting.unformat($(medical).val()) + accounting.unformat($(hospitalER).val()) +
                accounting.unformat($(dr).val()) + accounting.unformat($(pt).val()) +
                accounting.unformat($(tests).val()) + accounting.unformat($(misc).val());

            medicalPaidAmtVal = accounting.unformat($(medicalPaidAmt).val());

            $(medicalOutstandingAmt).val(accounting.formatMoney(medicalTotal - medicalPaidAmtVal));
            $(medicalIncurredAmtTotal).val(accounting.formatMoney(medicalTotal));
            $(this).val(accounting.formatMoney($(this).val()));

            SetTotalReserveAmounts();
        });

        $(misc).blur(function () {
            medicalTotal = accounting.unformat($(medical).val()) + accounting.unformat($(hospitalER).val()) +
                accounting.unformat($(dr).val()) + accounting.unformat($(pt).val()) +
                accounting.unformat($(tests).val()) + accounting.unformat($(misc).val());

            medicalPaidAmtVal = accounting.unformat($(medicalPaidAmt).val());

            $(medicalOutstandingAmt).val(accounting.formatMoney(medicalTotal - medicalPaidAmtVal));
            $(medicalIncurredAmtTotal).val(accounting.formatMoney(medicalTotal));
            $(this).val(accounting.formatMoney($(this).val()));

            SetTotalReserveAmounts();
        });
}

    function SetTotalReserveAmounts() {
        var medicalPaidAmt = accounting.unformat($("#edit-dialog input[data-field-name='MedicalPaidAmount']").val());
        var indemnityPaidAmt = accounting.unformat($("#edit-dialog input[data-field-name='IndemnityPaidAmount']").val());
        var expensesAllocatedPaidAmt = accounting.unformat($("#edit-dialog input[data-field-name='ExpensesAllocatedPaidAmount']").val()); 
        var expensesUnallocatedPaidAmt = accounting.unformat($("#edit-dialog input[data-field-name='ExpensesUnallocatedPaidAmount']").val()); 
        var indemnityOutstandingAmt = accounting.unformat($("#edit-dialog input[data-field-name='IndemnityOutstandingAmount']").val()); 
        var expensesOutstandingAmt = accounting.unformat($("#edit-dialog input[data-field-name='ExpensesOutstandingAmountTotal']").val()); 
        var medicalOutstandingAmt = accounting.unformat($("#edit-dialog input[data-field-name='MedicalOutstandingAmount']").val()); 
        var indemnityIncurredAmt = accounting.unformat($("#edit-dialog input[data-field-name='IndemnityIncurredAmountTotal']").val()); 
        var medicalIncurredAmt = accounting.unformat($("#edit-dialog input[data-field-name='MedicalIncurredAmountTotal']").val()); 
        var expensesIncurredAmt = accounting.unformat($("#edit-dialog input[data-field-name='ExpensesTotalIncurredAmount']").val()); 

        var totalPaidAmt = $("#edit-dialog input[data-field-name='TotalPaidAmount']");
        var totalOutstandingAmt = $("#edit-dialog input[data-field-name='TotalOutstandingAmount']");
        var totalIncurredAmt = $("#edit-dialog input[data-field-name='TotalIncurredAmount']");

        $(totalPaidAmt).val(accounting.formatMoney(medicalPaidAmt + indemnityPaidAmt + expensesAllocatedPaidAmt + expensesUnallocatedPaidAmt));
        $(totalOutstandingAmt).val(accounting.formatMoney(indemnityOutstandingAmt + expensesOutstandingAmt + medicalOutstandingAmt));
        $(totalIncurredAmt).val(accounting.formatMoney(indemnityIncurredAmt + medicalIncurredAmt + expensesIncurredAmt));
    }

    function HideFieldsByPolicyType(ptype) {

        //$("#edit-dialog input[data-field-name='Claimant']").parent().hide();

        policyType = $("#edit-dialog input[data-field-name='PolicyType']").val();

        var reserveWorksheetSectionHeaderSpan = $("#edit-dialog h3[aria-controls='workers_comp_reserve_worksheet'] >span");

        if (typeof ptype !== 'undefined') {
            if (ptype.toLowerCase() === "wc") {
                $(reserveWorksheetSectionHeaderSpan).text("Workers Comp Reserve Worksheet");
            }
            else if (ptype.toLowerCase() === "al") {
                $(reserveWorksheetSectionHeaderSpan).text("Auto Liability Reserve Worksheet");
            }
            else if (ptype.toLowerCase() === "gl") {
                $(reserveWorksheetSectionHeaderSpan).text("General Liability Reserve Worksheet");
            }

            policyType = ptype;
        }

        if (policyType === "WC") {
            $("#edit-dialog input[data-field-name='InjuredEmployee']").parent().show();
            $("#edit-dialog input[data-field-name='WaitPeriod']").parent().show();
            $("#edit-dialog textarea[data-field-name='DescInj']").parent().show();
            $("#edit-dialog input[data-field-name='AWW']").parent().show();
            $("#edit-dialog input[data-field-name='WaitPeriod']").parent().show();

            $("#edit-dialog input[data-field-name='Tests']").parent().show();
            $("#edit-dialog input[data-field-name='PT']").parent().show();
            $("#edit-dialog input[data-field-name='DR']").parent().show();
            $("#edit-dialog input[data-field-name='HospitalER']").parent().show();
            //$("#edit-dialog .subtotal-calc-section").show();
            $("#edit-dialog input[data-field-name='IndemnityTTDAmount']").parent().show();
            $("#edit-dialog input[data-field-name='IndemnityTTDTotal']").parent().show();
            $("#edit-dialog input[data-field-name='TTD']").parent().show();
            $("#edit-dialog input[data-field-name='IndemnityPPDAmount']").parent().show();
            $("#edit-dialog input[data-field-name='IndemnityPPDTotal']").parent().show();
            $("#edit-dialog input[data-field-name='PPD']").parent().show();
            //$("#edit-dialog input[data-field-name='TTDPaidToDate']").parent().show();

            //display these Workmen's comp section fields.
            $("#edit-dialog input[data-field-name='AttorneyFees']").parent().show();
            $("#edit-dialog input[data-field-name='NurseCaseManagerFees']").parent().show();
            $("#edit-dialog input[data-field-name='IMECosts']").parent().show();
            $("#edit-dialog input[data-field-name='SurveillanceCosts']").parent().show();
            $("#edit-dialog input[data-field-name='OutsideAdjusterExpense']").parent().show();
            $("#edit-dialog input[data-field-name='BillReviewFees']").parent().show();
            $("#edit-dialog input[data-field-name='URReviews']").parent().show();
            $("#edit-dialog input[data-field-name='VocRehabCosts']").parent().show();
            $("#edit-dialog input[data-field-name='WitnessExpertFees']").parent().show();

            //hide these Workmen's comp section fields.
            $("#edit-dialog input[data-field-name='EDIExpenses']").parent().hide();
            $("#edit-dialog input[data-field-name='ExpertFees']").parent().hide();
            $("#edit-dialog input[data-field-name='AxReconstructionFee']").parent().hide();
            $("#edit-dialog input[data-field-name='DefenseAttorneyFees']").parent().hide();
            $("#edit-dialog input[data-field-name='MiscLegalExpensesAmount']").parent().hide();
            $("#edit-dialog input[data-field-name='LostWages']").parent().hide();
            $("#edit-dialog input[data-field-name='EstimatedPAndS']").parent().hide();

            $("#edit-dialog input[data-field-name='BuildingRepairAmount']").parent().hide();
            $("#edit-dialog input[data-field-name='EquipmentBillAmount']").parent().hide();
            $("#edit-dialog input[data-field-name='OtherDamageAmount']").parent().hide();
            $("#edit-dialog input[data-field-name='VehicleRepairAmount']").parent().hide();
            $("#edit-dialog input[data-field-name='TowingBillAmount']").parent().hide();
            $("#edit-dialog input[data-field-name='MiscPDExpense']").parent().hide();
            $("#edit-dialog input[data-field-name='StorageFees']").parent().hide();
            $("#edit-dialog input[data-field-name='DownTime']").parent().hide();
            $("#edit-dialog input[data-field-name='RentalAmount']").parent().hide();
            //$("#edit-dialog input[data-field-name='LossType']").parent().hide();
           
            $("div[aria-group-section='Auto Driver Details']").hide();
            $("div[aria-group-section='Loss Details - Indemnity']").show();
            $("#edit-dialog .group-sub-label").show();

            UpdateSectionText("Loss Details - Property Damage", "Property Damage", "Indemnity");
            UpdateSectionText("Loss Details - Bodily Injury", "Bodily Injury", "Medical");

        }
        else if (policyType === "AL") {

            //show these workmen's comp section fields.
            $("#edit-dialog input[data-field-name='MiscLegalExpensesAmount']").parent().show();
            $("#edit-dialog input[data-field-name='LostWages']").parent().show();
            $("#edit-dialog input[data-field-name='EstimatedPAndS']").parent().show();
            $("#edit-dialog input[data-field-name='AttorneyFees']").parent().show();
            $("#edit-dialog input[data-field-name='WitnessFees']").parent().show();
            $("#edit-dialog input[data-field-name='ExpertFees']").parent().show();
            $("#edit-dialog input[data-field-name='AxReconstructionFee']").parent().show();
            $("#edit-dialog input[data-field-name='RentalAmount']").parent().show();
            $("#edit-dialog input[data-field-name='StorageFees']").parent().show();
            $("#edit-dialog input[data-field-name='DownTime']").parent().show();
            $("#edit-dialog input[data-field-name='MiscPDExpense']").parent().show();

            $("#edit-dialog input[data-field-name='LossType']").parent().show();
            $("div[aria-group-section='Auto Driver Details']").show();

            //hide these workers comp section fields.
            $("#edit-dialog input[data-field-name='BuildingRepairAmount']").parent().hide();
            $("#edit-dialog input[data-field-name='EquipmentBillAmount']").parent().hide();
            $("#edit-dialog input[data-field-name='OtherDamageAmount']").parent().hide();

            $("#edit-dialog input[data-field-name='Tests']").parent().hide();
            $("#edit-dialog input[data-field-name='PT']").parent().hide();
            $("#edit-dialog input[data-field-name='DR']").parent().hide();
            $("#edit-dialog input[data-field-name='HospitalER']").parent().hide();

            $("#edit-dialog input[data-field-name='IndemnityTTDAmount']").parent().hide();
            $("#edit-dialog input[data-field-name='IndemnityTTDTotal']").parent().hide();
            $("#edit-dialog input[data-field-name='TTD']").parent().hide();
            $("#edit-dialog input[data-field-name='IndemnityPPDAmount']").parent().hide();
            $("#edit-dialog input[data-field-name='IndemnityPPDTotal']").parent().hide();
            $("#edit-dialog input[data-field-name='DefenseAttorneyFees']").parent().hide();
            $("#edit-dialog input[data-field-name='PPD']").parent().hide();
            //$("#edit-dialog input[data-field-name='TTDPaidToDate']").parent().hide();
            $("#edit-dialog input[data-field-name='InjuredEmployee']").parent().hide();
            $("#edit-dialog input[data-field-name='WaitPeriod']").parent().hide();
            $("#edit-dialog input[data-field-name='NurseCaseManagerFees']").parent().hide();
            $("#edit-dialog input[data-field-name='IMECosts']").parent().hide();
            $("#edit-dialog input[data-field-name='SurveillanceCosts']").parent().hide();
            $("#edit-dialog input[data-field-name='OutsideAdjusterExpense']").parent().hide();
            $("#edit-dialog input[data-field-name='BillReviewFees']").parent().hide();
            $("#edit-dialog input[data-field-name='URReviews']").parent().hide();
            $("#edit-dialog input[data-field-name='EDIExpenses']").parent().hide();
            $("#edit-dialog input[data-field-name='VocRehabCosts']").parent().hide();
            $("#edit-dialog input[data-field-name='InjuredEmployee']").parent().hide();
            $("#edit-dialog textarea[data-field-name='DescInj']").parent().hide();

            $("#edit-dialog input[data-field-name='AWW']").parent().hide();
            $("#edit-dialog input[data-field-name='WaitPeriod']").parent().hide();

            $("#edit-dialog .group-sub-label").each(function () {
                if ($(this).find("span").text() === "Indemnity") {
                    $(this).hide();
                }
                else {
                    $(this).show();
                }
            });

            $("div[aria-group-section='Loss Details - Indemnity']").hide();

            UpdateSectionText("Loss Details - Property Damage", "Indemnity", "Property Damage");
            UpdateSectionText("Loss Details - Bodily Injury", "Medical", "Bodily Injury");
        }
        else if (policyType === "GL") {

            //show these workmen's comp section fields.
            $("#edit-dialog input[data-field-name='BuildingRepairAmount']").parent().show();
            $("#edit-dialog input[data-field-name='EquipmentBillAmount']").parent().show();
            $("#edit-dialog input[data-field-name='OtherDamageAmount']").parent().show();
            $("#edit-dialog input[data-field-name='MiscLegalExpensesAmount']").parent().show();
            $("#edit-dialog input[data-field-name='LostWages']").parent().show();
            $("#edit-dialog input[data-field-name='EstimatedPAndS']").parent().show();
            $("#edit-dialog input[data-field-name='AttorneyFees']").parent().show();
            $("#edit-dialog input[data-field-name='WitnessFees']").parent().show();
            $("#edit-dialog input[data-field-name='ExpertFees']").parent().show();
            $("#edit-dialog input[data-field-name='AxReconstructionFee']").parent().show();
            $("#edit-dialog input[data-field-name='RentalAmount']").parent().show();
            $("#edit-dialog input[data-field-name='StorageFees']").parent().show();
            $("#edit-dialog input[data-field-name='DownTime']").parent().show();
            $("#edit-dialog input[data-field-name='MiscPDExpense']").parent().show();

            $("#edit-dialog input[data-field-name='LossType']").parent().show();

            //hide these workers comp section fields.
            $("#edit-dialog input[data-field-name='VehicleRepairAmount']").parent().hide();
            $("#edit-dialog input[data-field-name='TowingBillAmount']").parent().hide();

            $("#edit-dialog input[data-field-name='Tests']").parent().hide();
            $("#edit-dialog input[data-field-name='PT']").parent().hide();
            $("#edit-dialog input[data-field-name='DR']").parent().hide();
            $("#edit-dialog input[data-field-name='HospitalER']").parent().hide();

            $("#edit-dialog input[data-field-name='IndemnityTTDAmount']").parent().hide();
            $("#edit-dialog input[data-field-name='IndemnityTTDTotal']").parent().hide();
            $("#edit-dialog input[data-field-name='TTD']").parent().hide();
            $("#edit-dialog input[data-field-name='IndemnityPPDAmount']").parent().hide();
            $("#edit-dialog input[data-field-name='IndemnityPPDTotal']").parent().hide();
            $("#edit-dialog input[data-field-name='DefenseAttorneyFees']").parent().hide();
            $("#edit-dialog input[data-field-name='PPD']").parent().hide();
           // $("#edit-dialog input[data-field-name='TTDPaidToDate']").parent().hide();
            $("#edit-dialog input[data-field-name='InjuredEmployee']").parent().hide();
            $("#edit-dialog input[data-field-name='WaitPeriod']").parent().hide();
            $("#edit-dialog input[data-field-name='NurseCaseManagerFees']").parent().hide();
            $("#edit-dialog input[data-field-name='IMECosts']").parent().hide();
            $("#edit-dialog input[data-field-name='SurveillanceCosts']").parent().hide();
            $("#edit-dialog input[data-field-name='OutsideAdjusterExpense']").parent().hide();
            $("#edit-dialog input[data-field-name='BillReviewFees']").parent().hide();
            $("#edit-dialog input[data-field-name='URReviews']").parent().hide();
            $("#edit-dialog input[data-field-name='EDIExpenses']").parent().hide();
            $("#edit-dialog input[data-field-name='VocRehabCosts']").parent().hide();
            $("#edit-dialog input[data-field-name='InjuredEmployee']").parent().hide();
            $("#edit-dialog textarea[data-field-name='DescInj']").parent().hide();
            $("#edit-dialog input[data-field-name='AWW']").parent().hide();
            $("#edit-dialog input[data-field-name='WaitPeriod']").parent().hide();

            $("div[aria-group-section='Auto Driver Details']").hide();
            $("div[aria-group-section='Loss Details - Indemnity']").hide();

            $("#edit-dialog .group-sub-label").each(function () {
                if ($(this).find("span").text() === "Indemnity") {
                    $(this).hide();
                }
                else {
                    $(this).show();
                }
            });

            UpdateSectionText("Loss Details - Property Damage", "Indemnity", "Property Damage");
            UpdateSectionText("Loss Details - Bodily Injury", "Medical", "Bodily Injury");
        }
    }

    function LockEditFields() {
        var currentUserID = $(".uid").text();
        var userType=$(".utype").text();

        if (userType === "Manager") {
            $("#edit-dialog input").prop("disabled", false);
            $("#edit-dialog textarea").prop("disabled", false);
        }
        else if (currentUserID !== $("#edit-dialog input[data-field-name='UserID']").val()) {
            $("#edit-dialog input").prop("disabled", true);
            $("#edit-dialog textarea").prop("disabled", true);
        }
    }

    function UpdateSectionText(sectionName, currentText, updateText)
    {
       // $("div[aria-group-section='" + sectionName + "'] span.accordion-text").text(sectionName.replace(currentText, updateText));
       // $("div[aria-group-name='" + sectionName + "'] span").each(function () {
      //      var labelText = $(this).text().replace(currentText, updateText);
      //      $(this).text(labelText);
       // });
    }

    function SetCalculateLagTime()
    {
        var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
        var lossDate;
        var receivedDate;
        var diffDays = 0;

        try {
            $("#edit-dialog input[data-field-name='ReceivedDate']").change(function () {

                receivedDate = new Date($("#edit-dialog input[data-field-name='ReceivedDate']").val());
                lossDate = new Date($("#edit-dialog input[data-field-name='LossDate']").val())
                diffDays=Math.round(Math.abs((receivedDate.getTime() - lossDate.getTime()) / (oneDay)));

                $("#edit-dialog input[data-field-name='LagTime']").val(diffDays);
            });

            $("#edit-dialog input[data-field-name='LossDate']").change(function () {

                receivedDate = new Date($("#edit-dialog input[data-field-name='ReceivedDate']").val());
                lossDate = new Date($("#edit-dialog input[data-field-name='LossDate']").val())
                diffDays = Math.round(Math.abs((receivedDate.getTime() - lossDate.getTime()) / (oneDay)));

                $("#edit-dialog input[data-field-name='LagTime']").val(diffDays);
            });
        }
        catch (e) {
            console.log(e);
        }

}

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
                    setTimeout(LoadDiaryNotes, 300,"Claim");
                },
                error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
            });

        }
        catch (e) {
            alert(e.message);
        }
    }

    function LoadDiaryNotes(noteType) {
        var claimNumber = $("input[data-field-name='ClaimNumber']").last().val();
        var dataUri = "../ListHandler.ashx?gt=users&ct=grid&qt=StoredProc&fld=&qn=Convergent_GetClaimsNotesHtml&pl=ClaimNumber~" + claimNumber + "|ROMode~"+readOnlyMode+"|NoteType~"+noteType+"&qid=" + S4();

        $.ajax({
            type: "GET",
            contentType: "text/html",
            cache: false,
            url: dataUri,
            success: function (data) {
                //console.log(data.aaData[0][1]);
                $(".comments-list-area").html(data.aaData[0][1]);
                //alert("Done!");
            },
            error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
        });

        //$(".comments-list-area").append("<span class='comment-detail'>Bruce Test</span>");
    }

    function LoadCRComments(showMsg) {

        $("#CRComments").val('');
        $("#show-cr-comments").empty();

        var crAnchor = $("#cr-tabs ul> li.ui-state-active > a");
        var crCommentID = sid;

        if (typeof $(crAnchor).attr("rel") !== 'undefined') {
            crCommentID = $(crAnchor).attr("rel").split("~")[1];

            try {
                $.ajax({
                    type: "GET",
                    contentType: "text/html",
                    cache: false,
                    url: "../DeficientSurveyComments.aspx?sid=" + crCommentID + "&cid=" + cid + "&ctype=checkrequest",
                    success: function () {
                        $("#show-cr-comments").load("../DeficientSurveyComments.aspx?sid=" + crCommentID + "&cid=" + cid + "&ctype=checkrequest&key=" + new Date().getTime());
                    },
                    error: function (xhr, textStatus, error) { alert("8 Error: " + xhr.responseText); }
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
            url: "../AISWS.asmx/SetCommentAsRead",
            data: JSON.stringify(DTO),
            dataType: "json",
            success: function () {
                //oClaimsGrid.fnReloadAjax();
                oClaimsGrid.ajax.reload(null, false);
                LoadCRComments(showMsgTF);
            },
            error: function (xhr, textStatus, error) { alert("9 Error: " + xhr.responseText); }
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

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/AddSurveyComment",
            data: JSON.stringify(DTO),
            dataType: "json",
            success: function () { LoadCRComments(false); },
            error: function (xhr, textStatus, error) { alert("13 Error: " + xhr.responseText); }
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
            error: function (xhr, textStatus, error) { alert("14 Error: " + xhr.responseText); }
        });
    }

    function PrintCheckRequest(obj) {
        var printUrl = "../reports/CheckRequest.aspx?" + $(obj).attr("rel");
        $("#claims-print-frame").attr("src", printUrl);
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

        if (domainUrl === "localhost") {
            domainUrl = "localhost/ais";
        }

        var printUrl = "http://" + domainUrl + "/PrintPage.aspx?pdata=" + encodeURIComponent(claimsData);

        $("#edit-dialog").dialog("close");

        //alert(printUrl);
        $("#claims-print-frame").attr("src", printUrl);

    }

    function SetupWorkersCompGrouping() {
        var fieldName;

        //alert($("#edit-dialog input").length);
        $("#edit-dialog input").each(function () {
            fieldName = $(this).attr("data-field-name");

            if (fieldName === "VehicleRepairAmount") {
                SetSubGroupLabel($(this), "Indemnity");
            }
            else if (fieldName === "Medical") {
                SetSubGroupLabel($(this), "Medical");
            }
            else if (fieldName === "AttorneyFees") {
                SetSubGroupLabel($(this), "Expenses");
            }
        });
    }

    function FilterClaimsCommentsByType(ele) {

        var noteType = $(ele).attr("aria-history-type");

        LoadDiaryNotes(noteType);

        $(".claims-comments-log a").each(function () {
            $(this).removeClass("claimhistory-type-selected");
        });

        $(ele).addClass("claimhistory-type-selected");


}

function LogExpenseUpdates(fieldUpdated,fieldValue) {
    var claimNumber = $("#edit-dialog input[data-field-name='ClaimNumber']").val();
    var comment = fieldUpdated + " changed to " + accounting.formatMoney(fieldValue);

    var alreadyExists = NewNCDExpenseQuery.Diaries.filter(function (d) {
        return d.Comments === comment;
    });

    if (alreadyExists.length === 0) {
        var diary = {
            pkDiaryID: 0,
            CompanyID: cid,
            ClaimNumber: claimNumber,
            ReportDate: "1/1/1900",
            NextReportDate: "1/1/1900",
            DiaryType: "Value Updated",
            Comments: comment,
            Mode: "Add",
            CompletedTF: false,
            UserID: uid,
            UpdatedBy: $("#ctl00_lblUserInfo").text().replace(/Welcome/i, "").trim()
        };

        NewNCDExpenseQuery.QueryType = "update";
        NewNCDExpenseQuery.NoteType = "Expense Change";
        NewNCDExpenseQuery.Diaries.push(diary);
    }
}

function SaveLogExpenseUpdates() {
    var DTO = { 'NewNCDDiaryQuery': NewNCDExpenseQuery };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../AISWS.asmx/NCDDiaryQuery",
        data: JSON.stringify(DTO),
        dataType: "json",
        beforeSend: function () { },
        error: function (xhr, textStatus, error) { alert("Error saving reserve changes: " + xhr.responseText); },
        success: function () {
            console.log("Data was successfully saved.");
            NewNCDExpenseQuery = { ClaimNumber: "", QueryType: "", Diaries: new Array() };
        }
    });
}

function SaveReserveChanges() {

    var NCDReserveChangeDetailQuery = { ClaimNumber: "", CompanyID:cid, UserID:uid, QueryType: "add", ReserveChangeDetails: new Array() };

    var reserveChange = {
        pkReserveChangeDetailID: 0,
        CompanyID: cid,
        ChangeDate: "1/1/1900",
        ClaimNumber: $("#edit-dialog input[data-field-name='ClaimNumber']").val(),
        Adjuster: $("#edit-dialog #assigned-adjuster option:selected").text(),
        ClaimantName: $("#edit-dialog input[data-field-name='ClaimantName']").val(),
        ChangeType: "",
        CurrentTotalIncurred:0.00,
        NewTotalIncurred: accounting.unformat($("#edit-dialog input[data-field-name='TotalIncurredAmount']").val()),
        ChangeAmount:0.00,
        UpdatedBy: $("#ctl00_lblUserInfo").text().replace(/Welcome/i, "").trim()
    };

    NCDReserveChangeDetailQuery.ClaimNumber = reserveChange.ClaimNumber;
    
    NCDReserveChangeDetailQuery.ReserveChangeDetails.push(reserveChange);

    var DTO = { 'NewNCDReserveChangeDetailQuery': NCDReserveChangeDetailQuery };

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../AISWS.asmx/NCDReserveChangeDetailQuery",
        data: JSON.stringify(DTO),
        dataType: "json",
        beforeSend: function () { },
        error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); },
        success: function () {
            console.log("Reserve change was successfully saved.");
        }
    });
}