<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDAdHocQuery.ascx.cs" Inherits="AISReports.UserControls.NCDAdHocQuery" %>
<style type="text/css">
    .accordion div span {display:block;}
    .accordion div span span {display:inline;}
    .accordion { font-size:small !important;}
    .left-content{width:30%; height:100%;display:inline-block;float:left; margin-right:10px;}
    .main-content{display:inline-block; height:100%;margin:auto;width:68%;float:left;}
    .query-window{vertical-align:top;}
    .bottom-main-content{vertical-align:top;}
    .context-group-by {cursor:pointer;}
    .aggregate-indicator {color:red !important;font-weight:bold !important;}
   /* #filter-dialog select {font-size:smaller; }
    #filter-dialog input {font-size:smaller;}
    #filter-dialog button {font-size:smaller;} */
    #where-clause-list {display:none;}
    .export-button span{color:#fff; margin:5px;}
    /*.dt-hscroll-wrapper{overflow-x: auto;overflow-y: visible;} */
    span.customSelect {
	font-size:11px;
	background-color: #f5f0de;
	color:#7c7c7c;
	padding:5px 7px;
	border:1px solid #e7dab0;
	-moz-border-radius: 5px;
	-webkit-border-radius: 5px;
	border-radius: 5px 5px;
}
span.customSelect.changed {
	background-color: #f0dea4;
}
.customSelectInner {
	background:url(../images/customSelect-arrow.gif) no-repeat center right;
}
</style>
<script type="text/javascript" src="..\script\jquery.mousewheel.js"></script>
<script type="text/javascript">
    var filterControlIndex = 0;
    var savedQueries = new Array();
    var selectedSavedQueryName = "";
    var savedQueryLoaded = false;
    var oAdHocTable;


    $(function () {

        //if ($(".grid-section").attr("grid-export-pdf") == "true") {
        //    TableTools.DEFAULTS.aButtons = [{
        //        "sExtends": "xls",
        //        "sFileName": "AdHocResults.xls",
        //        "sButtonClass": "export-button"
        //    },
        //    {
        //        "sExtends": "pdf",
        //        "sFileName": "AdHocResults.pdf",
        //        "sButtonClass": "export-button"
        //    }];
        //}
        //else {
        //    TableTools.DEFAULTS.aButtons = [{
        //        "sExtends": "xls",
        //        "sFileName": "AdHocResults.xls",
        //        "sButtonClass": "export-button"
        //    }];
        //}

        //TableTools.DEFAULTS.sSwfPath = "../media/swf/copy_csv_xls_pdf.swf";

        $(".accordion").accordion({
            collapsible: true,
            active: false,
            autoHeight: false
        });

        $("#filter-dialog").hide();
        $("#saved-queries-dialog").hide();
        $("#tabs").tabs();

        GetData($(".grid-section"));
        showTabs("#tabs-1");

        $("#btnSavedQueryOK").button();
        $("#btnClear").button();
        $("#btnFilter").button();
        $("#btnRunQuery").button();
        $("#btnSaveQuery").button();
        $("#btnDeleteQuery").button();
        $("#btnOK").button();

        //GetSavedQueryList();
        LoadContextMenu();

        $("#adhoc-grid").DataTable();

        //alert($(".datatable-scroll").html());
        //$(".datatable-scroll").mousewheel(function (event, delta) {
        //    this.scrollLeft -= (delta * 30);
        //    event.preventDefault();
        //});

    });

    function showTabs(ele) {
        var currentTabId = ele.replace(window.location.href, "");
        var selectedTab= "";

        if ($("#query-fields").text().length > 0) {
            var result = confirm('If you switch tabs, all current selected information will be lost.  Do you want to continue?');

            if (result == true) {
                $("#btnClear").click();
            }
            else {
                return;
            }
        }

        $("#tab-items li a").each(function (index) {
            var tabId = $(this).attr("href")
            if (currentTabId == tabId) {
                $(tabId).show();
                selectedTab = $(this).text();
            }
            else {
                $(tabId).hide();
            }
        });

        //alert($(ele).html());
        GetSavedQueryList(selectedTab);
    }

    function CheckBoxSelected(ele) {
        //console.log(ele);
        LoadSelectedField(ele);
        LoadSelectedTable(ele);

        if ($(ele).parent().parent().find("label").length > 0) {
            GroupByClause();
        }
 
    }

    function LoadSelectedField(ele) {

        var queryFieldList = $("#query-fields").text();
        var queryFieldName = $(ele).attr("rel");
        var dataType = $(ele).attr("field-data-type");
        var queryFieldLabel = $(ele).next().text();
        
        if (queryFieldName.toLowerCase().indexOf(queryFieldLabel.toLowerCase()) == -1) {
            queryFieldName =queryFieldName.split(".")[0]+".["+queryFieldName.split(".")[1]+"]" + " as [" + queryFieldLabel + "]";
        }

        //alert("field Name: "+queryFieldLabel+"  is checked: "+$(ele).is(":checked"));
        if ($(ele).is(":checked")) {

            try {
                if (queryFieldList.indexOf(queryFieldName) == -1) {
                    if (queryFieldList.length == 0) {
                        $("#query-fields").text(queryFieldName)
                    }
                    else {
                        $("#query-fields").append(", " + queryFieldName);
                    }
                }
            }
            catch (e) {
                alert(e.Description);
                alert(e.Message);
            }

        }
        else {
            queryFieldList = queryFieldList.replace(queryFieldName, "").replace(/\,\s\,+/g, ",").replace(/^\,|\,$/g, "");
            $("#query-fields").text($.trim(queryFieldList).replace(/\,$/, ""));
        }

        //console.log("load selected fields: " + $("#query-fields").text());
        

    }

    function LoadSelectedTable(ele) {

        var queryFieldList = $("#query-fields").text();
        var queryTableList = $("#query-tables").text();
        var tableName = $(ele).attr("rel").split(".")[0];

        if ($(ele).is(":checked")) {
            if (queryTableList.length == 0 && queryTableList.indexOf(tableName) == -1) {
                $("#query-tables").text(tableName)
            }
            else if (queryTableList.length > 0 && queryTableList.indexOf(tableName) == -1) {
                $("#query-tables").append("," + tableName);
            }
        }
        else if (queryFieldList.indexOf(tableName) == -1) {
            queryTableList = queryTableList.replace(tableName, "").replace(",,", ",").replace(/^\,|\,$/g, "");
            $("#query-tables").text(queryTableList);
        }
    }

    function LoadFilterFields() {

        var fieldValue;
        var fieldText;
        var fields = new Array();
        var cboField = $("<select id='cboField" + filterControlIndex + "' rel='" + filterControlIndex + "' class='dropdownlist-field-names' style='padding-left:5px;' />");
        var cboOperand = $("<select id='cboOperand" + filterControlIndex + "' style='width:140px;padding-left:5px;' />");
        var cboAndOr = $("<select id='cboAndOr" + filterControlIndex + "' rel='" + filterControlIndex + "' style='width:65px;padding-left:5px;' />");
        var txtFilterValue = $("<input type=text id='txtFilterValue" + filterControlIndex + "' />");
        var btnRemove = $("<button id='btnRemoveFilter" + filterControlIndex + "' rel='" + filterControlIndex + "' style='width:90px;padding-left:5px;'>Remove</button>");

        var selectedTab = $("#tabs li.ui-tabs-active").attr("aria-controls");
        $("#" + selectedTab + " input[type='checkbox']").each(function () {
            fields.push($(this).attr("rel"));
        });

        fields.sort();
        //var fields = $("#query-fields").text().split(",");
        var operands = ["= '#'|is equal to", "<> '#'|is not equal to", "> '#'|is greater than", ">= '#'|is greater than/equal to ", "< '#'|is less than",
                        "<= '#'|is less than/equal to", "like '#%'|starts with", "like '%#'|ends with", "like '%#%'|contains", "not like '%#%'|does not contain"];
        var andOr = ["", "and", "or"];

        $('<option />', { value: "", text: "" }).appendTo(cboField);

        for (var i = 0; i < fields.length; i++) {

            fieldValue = fields[i];
            fieldText = fields[i].split('.')[1];

            if (fieldText.indexOf(" as ") != -1) {
                fieldText = fieldText.split(" as ")[1].replace("[", "").replace("]", "");
                fieldValue = fieldValue.split(" as ")[0];
            }

            $('<option />', { value: $.trim(fieldValue), text: fieldText.toTitleCase() }).appendTo(cboField);
        }

        cboField.appendTo($("#filter-parameters"));

        // $("#filter-parameters").append("&nbsp;");

        for (var j = 0; j < operands.length; j++) {

            fieldValue = operands[j].split('|')[0];
            fieldText = operands[j].split('|')[1];

            $('<option />', { value: fieldValue, text: fieldText }).appendTo(cboOperand);
        }

        cboOperand.appendTo($("#filter-parameters"));
        //$("#filter-parameters").append("&nbsp;");

        for (var k = 0; k < andOr.length; k++) {

            fieldValue = andOr[k];
            fieldText = andOr[k];

            $('<option />', { value: fieldValue, text: fieldText }).appendTo(cboAndOr);
        }

        txtFilterValue.appendTo($("#filter-parameters"));

        $(txtFilterValue).keyup(function () {
            if ($("#cboField" + filterControlIndex).val() != "") {
                ManageWhereClause(filterControlIndex - 1, "add");
            }
        });

        // $("#filter-parameters").append("&nbsp;");

        cboAndOr.appendTo($("#filter-parameters"));

        // $("#filter-parameters").append("&nbsp;");

        btnRemove.appendTo($("#filter-parameters"));

        $("#filter-parameters").append("<br id='line-break" + filterControlIndex + "'/>");

        if (filterControlIndex > 0) {
            $("#btnRemoveFilter" + (filterControlIndex - 1)).attr("disabled", "disabled");
        }

        $(btnRemove).click(function () {

            var rowIndex = $(this).attr("rel");

            $("#cboField" + rowIndex).remove();
            $("#cboOperand" + rowIndex).remove();
            $("#txtFilterValue" + rowIndex).remove();
            $("#cboAndOr" + rowIndex).remove();
            $("#line-break" + rowIndex).remove();
            $(this).remove();

            if ((rowIndex - 1) > -1) {
                $("#btnRemoveFilter" + (rowIndex - 1)).removeAttr("disabled");
            }

            if (rowIndex != 0) {
                rowIndex = rowIndex - 1;
                $("#cboAndOr" + rowIndex).val("");
            }
            else {
                $("#query-where-clause").empty();
                ShowFilter(false);
            }

            ManageWhereClause(rowIndex, "delete");
        });

        $(cboField).click(function () {

            if ($(this).val() == "") {
                $("#cboAndOr" + $(this).attr("rel")).attr("disabled", "disabled");
            }
            else {
                $("#cboAndOr" + $(this).attr("rel")).removeAttr("disabled");
            }

            $(txtFilterValue).datepicker("destroy");
            $(txtFilterValue).autocomplete("destroy");

            if ($(this).val().toLowerCase().split(".").length > 1 && $(this).val().toLowerCase().split(".")[1].indexOf("date") > -1) {
                $(txtFilterValue).datepicker({
                    beforeShow: function (input) { $(input).css({ "position": "relative", zIndex: 9999 }); }
                });

                $(txtFilterValue).change(function () {
                    ManageWhereClause(filterControlIndex - 1, "add");
                });
            }
            else {
                $(txtFilterValue).autocomplete({
                    source: "../ListHandler.ashx?cid=" + cid + "&uid=" + uid + "&fn=" + $(this).val().split(".")[1] + "&tn=" + $(this).val().split(".")[0] + "&qt=oldlookup" + "&qid=" + S4(),
                    minLength: 1,
                    select: function (e, ui) {
                        $(this).val(ui.item.value);
                        ManageWhereClause(filterControlIndex - 1, "add");
                    }
                });
            }

        });

        $(cboOperand).change(function () {
            ManageWhereClause(filterControlIndex - 1, "add");
        });

        $(cboAndOr).change(function () {

            var idxDifference = filterControlIndex - parseInt($(this).attr("rel"));

            if (filterControlIndex > -1) {
                //$("#btnRemoveFilter" + $(this).attr("rel")).attr("disabled", "disabled");
            }

            if ($(this).val() != "" && idxDifference == 1) {
                ManageWhereClause(filterControlIndex - 1, "add");
                LoadFilterFields();
            }

            RefreshFilterFields();

        });

        filterControlIndex++;
    }

    function RefreshFilterFields() {

        $("#where-clause-list").empty();
        $("#query-where-clause").empty();
        var whereClause = "";

        for (idx = 0; idx < filterControlIndex; idx++) {
            try {
                var cfield = $("#filter-parameters").find("#cboField" + idx);
                var cOp = $("#filter-parameters").find("#cboOperand" + idx);
                var cAO = $("#filter-parameters").find("#cboAndOr" + idx);
                var txt = $("#filter-parameters").find("#txtFilterValue" + idx);

                lstItem = $("<li></li>");
                lstItem.attr("id", "wcItem" + idx);
                lstItem.attr("wcdetail", cfield.val() + "|" + cOp.val() + "|" + txt.val() + "|" + cAO.val());
                lstItem.text(cfield.val().trim() + " " + cOp.val().replace("#", txt.val()).trim() + " " + cAO.val().trim() + " ");

                //var lstItem = $("<li id='wcItem" + idx + "' wcDetail='" + cfield.val() + "|" + cOp.val() + "|" + txt.val() + "|" + cAO.val() + "'></li>");

                //lstItem.text(cfield.val() + " " + cOp.val().replace("$eq$", "=").replace("#", txt.val()) + " " + cAO.val() + " ");

                $("#where-clause-list").append(lstItem);
            }
            catch (e) {
                // alert(e.Message);
            }

        }

        whereClause = $("#where-clause-list").text(); //.replace(/\|/g, "");

        if (whereClause != "") {
            whereClause = " WHERE " + whereClause;
        }

        $("#query-where-clause").text(whereClause);
    }

    function GetResults() {

        if ($(".query-fields").text() == "") {
            GetData($(".grid-section"));
        }
        else {
            alert("you must select at least one field.");
        }
    }

    function GetData(div) {

        try {

            var table = $(div).find(".grid-control");
            var gridType = $(div).attr("grid-type");
            var selectedFields = $("#query-fields").text().split(',');

            if (typeof oAdHocTable != 'undefined' && selectedFields != null && selectedFields[0].trim() !="") {
                oAdHocTable.clear();
                oAdHocTable.destroy();
            }

            DataQuery.FieldList = $(div).attr("grid-display-fields");
            DataQuery.QueryType = $(div).attr("grid-query-type");
            DataQuery.Query = $(".query-window").text();

            if (DataQuery.Query.replace(/\s/g, "") == "SELECTFROM") {
                DataQuery.Query = "";
            }

            var dataUri = "../ListHandler.ashx?ct=grid&pl="+DataQuery.QueryParamList+"&qt=" + DataQuery.QueryType + "&qid=" + S4();

            //$("#site-message-section").append(dataUri+"<br/>");
            if (selectedFields[0] != "") {

                $("#adhoc-header").html('<th></th>');

                for (var i = 0; i < selectedFields.length; i++) {
                    var gridheader = selectedFields[i].split('.')[1];

                    if (gridheader.indexOf(" as [") != -1) {
                        gridheader = gridheader.split(" as ")[1].replace("[", "").replace("]", "");
                    }

                    if (IsComputedFieldTF(selectedFields[i])) {
                        gridheader = "<span style='color:red;'>*</span>  " + gridheader.split(') as ')[1].trim();
                    }

                    $("#adhoc-header").append("<th field-name='" + selectedFields[i] + "'>" + gridheader.toTitleCase() + "</th>");
                }
            }
            //console.log(JSON.stringify(DataQuery));
            //"bDestroy": true,
            //        "sAjaxSource": dataUri,
            //        "aLengthMenu": [[10, 20, 25, -1], [10, 20, 25, "All"]],
            //        "iDisplayLength": 25,
            //        "sPaginationType": "full_numbers",
            //        "sDom": '<"H"Tfr><"datatable-scroll"t><"F"ip>',
            //        "bDeferRender": true,
            //        "bJQueryUI": true,
            //        "bScrollCollapse": true,
            //"sDom": '<"H"Tlfr><"datatable-scroll"t><"F"ip>',
          
            //console.log($("#adhoc-header").html());

            if (DataQuery.Query != "") {
                oAdHocTable=$(table).DataTable({
                    //"bDeferRender": true,
                    destroy: true,
                    ajax: dataUri,
                    lengthMenu: [[10, 20, 25, -1], [10, 20, 25, "All"]],
                    pagingType: "full_numbers",
                    fixedHeader: false,
                    colReorder: true,
                    language: {
                        "emptyTable": "No data found to display."
                    },
                    dom: '<"fg-toolbar ui-toolbar ui-widget-header ui-helper-clearfix ui-corner-tl ui-corner-tr"Blfr><"dataTables_scroll"t><"fg-toolbar ui-toolbar ui-widget-header ui-helper-clearfix ui-corner-bl ui-corner-br"ip>',
                    buttons: [{ extend: 'excel', filename: 'GridExport', className: 'ui-corner-br fg-button ui-button ui-state-default' }]
                    ,"fnServerParams": function (aoData) {
                        aoData.push({ "name": "fieldlist", "value": DataQuery.FieldList },
                                { "name": "query", "value": DataQuery.Query },
                                { "name": "querytype", "value": DataQuery.QueryType },
                                { "name": "queryparamlist", "value": DataQuery.QueryParamList },
                                { "name": "tablelist", "value": DataQuery.TableList },
                                { "name": "filterlist", "value": DataQuery.FilterList },
                                { "name": "orderbylist", "value": DataQuery.OrderByList });
                    }
                    //,exportOptions: {
                    //    format: {
                    //        body: function (data, row, column, node) {
                    //            data.forEach(FormatDataForExport);
                    //        }
                    //     }
                    // }    
                    //rowCallback: function (nRow, aData, iDisplayIndex) {

                    //}
                });
            }

            //$(table).wrap('<div style="overflow: auto; width: ' + $(table).parent().parent().parent().width() * 0.95 + 'px"/>');

        }
        catch (e) {
            alert("BuildGrid() Error: " + e.Description);
        }
    }

    function ShowFilter(reloadTF) {
        if ($("#query-fields").text().length > 0) {

            if ($("#where-clause-list li").length == 0) {
                $("#filter-parameters").empty();
                LoadFilterFields();
            }

            if (reloadTF) {
                $("#filter-dialog").dialog({ minWidth: 750,
                    minHeight: 500,
                    modal: true,
                    zIndex: 9000
                });
            }
        }
        else {
            alert("No fields to filter.");
        }
    }

    function ManageWhereClause(idx, mode) {
        var cfield = $("#filter-parameters").find("#cboField" + idx);
        var cOp = $("#filter-parameters").find("#cboOperand" + idx);
        var cAO = $("#filter-parameters").find("#cboAndOr" + idx);
        var txt = $("#filter-parameters").find("#txtFilterValue" + idx);
        var whereClause = "";

        var lstItem = $("#where-clause-list").find("#wcItem" + idx);

        if (lstItem != 'undefined') {
            lstItem.remove();
        }

        if (mode == "delete") {
            RefreshFilterFields();
            filterControlIndex--;
        }

        if (mode == "add") {

            try {
                lstItem = $("<li></li>");
                lstItem.attr("id", "wcItem" + idx);
                lstItem.attr("wcdetail", cfield.val() + "|" + cOp.val() + "|" + txt.val() + "|" + cAO.val());
                lstItem.text(cfield.val().trim() + " " + cOp.val().replace("#", txt.val()).trim() + " " + cAO.val().trim() + " ");

                $("#where-clause-list").append(lstItem);
            }
            catch (e) {
                alert(e.Message);
            }
        }

        whereClause = $("#where-clause-list").text().replace(/\|/g, "");

        if (whereClause != "") {
            whereClause = " WHERE " + whereClause; //.replace(/\and$/, "").replace(/\or$/, "");
        }

        $("#query-where-clause").text(whereClause);
    }

    function startProgress() {
        $("body").css("cursor", "progress");
        $(".action-button").hide();
        $(".progress-button").show

    }

    function endProgress() {
        $("body").css("cursor", "auto");
        $(".action-button").show();
        $(".progress-button").hide();
    }

    function ClearFilterFields() {
        filterControlIndex = 0;
        $("#filter-parameters").empty();
    }

    function ClearFieldSelections() {
        $(".aggregate-indicator").remove();
    }

    function LoadSavedQuery() {

        if ($("#ddlQueryName").val() != "") {

            ClearFilterFields();
            ClearFieldSelections();
            $("#query-fields").empty();
            $("#query-groupby-clause").empty();
            $("#query-groupby-fields").empty();

            $.ajax({
                type: "GET",
                contentType: "json",
                cache: false,
                url: "../ListHandler.ashx?cid=" + cid + "&uid=" + uid + "&qn=" + $("#ddlQueryName").val() + "&qt=savedquery",
                success: function (result) {

                    for (var i = 0; i < result.length; i++) {
                                                
                        $("#query-fields").text(result[i].queryfields);

                        //alert("LoadSavedQuery: "+result[i].queryfields);
                        SetSelectedFieldCheckboxes(result[i].queryfields);

                        $("#query-tables").text(result[i].querytables);

                        $("#where-clause-list").html(result[i].querywhere);

                        if ($("#where-clause-list").text().trim().length > 0) {
                            $("#query-where-clause").text(" WHERE " + $("#where-clause-list").text());
                        }
                        else {
                            $("#query-where-clause").text("");
                        }

                        
                        $("#where-clause-list li").each(function () {
                            LoadFilterFields();

                            var index = parseInt($(this).attr("id").replace("wcItem", ""));
                            var detail = $(this).attr("wcdetail").replace(/\"\s/g, "'").split("|");

                            $.each(detail, function (i, v) {
                                if (i == 0) {
                                    $("#cboField" + index).val($.trim(v));
                                }
                                else if (i == 1) {
                                    $("#cboOperand" + index).val(v);
                                }
                                else if (i == 2) {
                                    $("#txtFilterValue" + index).val(v);
                                }
                                else if (i == 3) {
                                    $("#cboAndOr" + index).val(v);
                                }
                            });
                        });
                    }

                    savedQueryLoaded = true;
                    GroupByClause();

                  $("#btnRunQuery").click();
                },
                error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
            });
        }
    }

    function SetSelectedFieldCheckboxes(value) {
        var cbList = value.split(",");
        var aggFunctionExistsTF = false;
        
        $("#tabs").find('input[type=checkbox]:checked').removeAttr('checked');
       
        $.each(cbList, function (index, value) {
            var fieldname = value.trim().split(" as [")[0];
            fieldname = fieldname.replace(/[\[\]\s]+/g, "")
            var aggfunction = "";
            var chkBox = $("#tabs input[rel='" + fieldname + "']");;
            //alert("field name: " + fieldname);
            if (value.indexOf("(") > 0) {
                aggfunction = value.split("(")[0].trim();
                fieldname = value.split(" as ")[0].trim().split("(")[1].replace(")","");
                chkBox = $("#tabs input[rel='" + fieldname + "']");
            }
            
            if (aggfunction != "") {
                if ($(chkBox).siblings(".aggregate-indicator").length==0) {
                    $("<label class='aggregate-indicator'>  (" + aggfunction + ")</label>").insertAfter($(chkBox).siblings("span"));
                }
                else {
                    $(chkBox).siblings(".aggregate-indicator").text(" (" + aggfunction + ")");
                }

                aggFunctionExistsTF = true;
            }

            $(chkBox).attr("checked", "true");
        });

        if (!aggFunctionExistsTF) {
            GroupByClause();
        }
    }

    function SetWhereClause() {
        var ul = $("#where-clause-list");
        
        var rowCounter = 0;
        var wcDetail = "";
        var wcText = "";
        var cfield;
        var cOp;
        var cAO;
        var txt;
        var whereClause = "";

        $(ul).empty();

        $("#filter-parameters").children().each(function (i,ctrl) {

            if ($(ctrl).attr("id").indexOf("cboField") !== -1) {
                cfield = $(ctrl);
            }
            else if ($(ctrl).attr("id").indexOf("cboOperand") !== -1) {
                cOp = $(ctrl);
            }
            else if ($(ctrl).attr("id").indexOf("txtFilterValue") !== -1) {
                txt = $(ctrl);
            }
            else if ($(ctrl).attr("id").indexOf("cboAndOr") !== -1) {
                cAO = $(ctrl);
            }

            if($(ctrl).attr("id").indexOf("line-break") !==-1){
                var liItem = $("<li></li>");

                liItem.attr("id", "wcItem" + rowCounter);
                liItem.attr("wcdetail", wcDetail);
                liItem.attr("wcdetail", cfield.val() + "|" + cOp.val() + "|" + txt.val() + "|" + cAO.val());
                liItem.text(cfield.val().trim() + " " + cOp.val().replace("#", txt.val()).trim() + " " + cAO.val().trim() + " ");

                $("#where-clause-list").append(liItem);

                rowCounter++;
            }

            //console.log($(ctrl).attr("id"));
        });
        
        whereClause = $("#where-clause-list").text().replace(/\|/g, "");

        if (whereClause != "") {
            whereClause = " WHERE " + whereClause; //.replace(/\and$/, "").replace(/\or$/, "");
        }

        $("#query-where-clause").text(whereClause);
    }

    function GetSavedQueryList(tabName) {
        //$("#site-message-section").text("../ListHandler.ashx?cid=" + cid + "&uid=" + uid + "&fn=QueryName&tn=SavedQueries&qt=lookup");
        var options = new Array();

        var selectedTab = "";

        if (tabName != "") {
            selectedTab = tabName;
        }
        else {
            selectedTab=$("#tabs li.ui-tabs-active > a").text();
        }


        $("#ddlQueryName").find('option').remove().end()

        $.ajax({
            type: "GET",
            contentType: "json",
            cache: false,
            url: "../ListHandler.ashx?cid=" + cid + "&uid=" + uid + "&tabname="+selectedTab+"&fn=QueryName&tn=cms_SavedQueries&qt=lookup",
            success: function (result) {

                options.push('<option value="">[Select One]</option>');

                if (typeof result != 'undefined' && result !=null) {
                    for (var i = 0; i < result.length; i++) {
                        options.push('<option value="', result[i].label, '">', result[i].value, '</option>');
                    }

                    $("#ddlQueryName").html(options.join(''));
                    $("#saved-queries-section").show();
                }
                else
                {
                    $("#saved-queries-section").hide();
                }

            },
            error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
        });
    }

    function ShowSavedQueryWindow(wtype) {

        if (wtype == "save") {
            $('#saved-queries-dialog').attr("title", "Save Query");

            $('#saved-queries-dialog').dialog({ minWidth: 480,
                minHeight: 170,
                modal: true
            });

            if ($("#ddlQueryName").find(":selected").index() > 0) {
                $("#txtSavedQueryName").val($("#ddlQueryName").find(":selected").text());
                ValidateQueryText($("#ddlQueryName").find(":selected").text());
            }

            $("#lblQueryName").text("Name of Query:");
            $("#txtSavedQueryName").show();
            $("#txtQueryToDelete").hide();
            $("#btnSavedQueryOK").html("Ok");
        }

        if (wtype == "delete") {
            $('#saved-queries-dialog').attr("title", "Delete Query");

            $('#saved-queries-dialog').dialog({ minWidth: 450,
                minHeight: 240,
                modal: true
            });

            $("#lblQueryName").text("Select a query:");
            $("#txtSavedQueryName").hide();
            $("#txtQueryToDelete").show();
            $("#btnSavedQueryOK").html("Delete");
        }
    }

    function SaveQuery() {
        try {
            var SavedQuery = {};
            var modeDisplay = "added";
            var completeAction = true;
            var currentTabName = $("#tabs ul .ui-tabs-active").text();

            SavedQuery.CompanyID = cid;
            SavedQuery.UserID = uid;
            SavedQuery.QueryName = $("#txtSavedQueryName").val();
            SavedQuery.QueryFields = $("#query-fields").text();
            SavedQuery.QueryTables = $("#query-tables").text();
            SavedQuery.QueryWhere = $("#where-clause-list").html(); // decodeURIComponent($("#where-clause-list").html()).replace(/\'=\"/g, "");
            SavedQuery.TabName = currentTabName;

            var DTO = { 'SavedQuery': SavedQuery };
            var url = "../AISWS.asmx/AddSavedQuery";

            if ($('#spnAlreadyExistsMessage').is(":visible")) {
                url = "../AISWS.asmx/UpdateSavedQuery";
                modeDisplay = "updated";
            }

            if ($("#btnSavedQueryOK").text() == "Delete") {
                SavedQuery.QueryName = $("#txtQueryToDelete").val();
                url = "../AISWS.asmx/DeleteSavedQuery";
                modeDisplay = "deleted";
                completeAction = confirm("Are you sure you want to delete this query?");
            }

            if (completeAction) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: url,
                    data: JSON.stringify(DTO),
                    dataType: "json",
                    success: function () { alert("Query " + SavedQuery.QueryName + " was successfully " + modeDisplay + "."); GetSavedQueryList(""); },
                    error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
                });
            }
        }
        catch (e) {
            alert(e.message);
        }

        $('#saved-queries-dialog').dialog("close");
    }

    function ValidateQueryName(txt) {
        var queryExists = false;

        $('#message-area').hide();

        $("#ddlQueryName option").each(function (i) {

            if ($(this).text().toLowerCase() == txt.value.toLowerCase()) {
                queryExists = true;
            }
        });

        if (queryExists) {
            $('#message-area').fadeIn("300"); // show();
            $('#spnAlreadyExistsMessage').show();
        }
        else {
            $('#spnAlreadyExistsMessage').hide();
        }
    }

    function ValidateQueryText(txt) {
        var queryExists = false;

        $('#message-area').hide();

        $("#ddlQueryName option").each(function (i) {

            if ($(this).text().toLowerCase() == txt.toLowerCase()) {
                queryExists = true;
            }
        });

        if (queryExists) {
            $('#message-area').fadeIn("300"); // show();
            $('#spnAlreadyExistsMessage').show();
        }
        else {
            $('#spnAlreadyExistsMessage').hide();
        }
    }

    function CleanUpQuery() {
        $('#btnSaveQuery').removeAttr('disabled');

        SetWhereClause();
        var where = $("#query-where-clause").text().trim().replace(/\s\s/g, " ").replace(/\$eq\$/g, "=");
        var regexAnd = /and\s=\s\'\'/g;
        var regexOr = /or\s=\s\'\'/g;

        $("#query-where-clause").text(where.replace(regexAnd, "").replace(regexOr, ""));

        $('#btnRunQuery').click();
        $('#filter-dialog').dialog('close')

        
    }

    function ExportToExcel() {
          $("#adhoc-grid").table2excel({
            name: "Ad Hoc Export",
            filename: "AdHocExport"
    });

        //$("#export-result").text($("#adhoc-grid").html());
    }

    function LoadContextMenu() {
        $.contextMenu({
            selector: '.context-group-by',
            callback: function (key, options) {
                var m = "clicked: " + key;
                //window.console && console.log(m) || alert(m);
                $(this).find("label").remove();

                if (key != "clear") {
                    $("<label class='aggregate-indicator'>  (" + key.toTitleCase() + ")</label>").insertAfter($(this).find("span"));
                    GroupByClause();
                }
                else {
                    GroupByClause();
                }

                //CheckBoxSelected($(this).find("input"));

                //alert($(this).find("input").attr("rel"));
            },
            items: {
                "sum": { name: "Sum", icon: "sum" },
                "avg": { name: "Average", icon: "avg" },
                "count": { name: "Count", icon: "count" },
                "stddev": { name: "Std Dev.", icon: "stdev" },
                "min": { name: "Min", icon: "min" },
                "max": { name: "Max", icon: "max" },
                "clear": { name: "Clear", icon: "clear" }
            }
        });

        //$('.context-menu-one').on('click', function (e) {
        //    console.log('clicked', this);
        //})
    }

    function GroupByClause() {

        $("#query-groupby-clause").empty();
        $("#query-groupby-fields").empty();
        
        var selectQueryList = "";
        var groupByList = "";
        var aggFunctionExistsTF = false;
        
        var selectList = $("#query-fields").text().split(",");

        $.each(selectList,function (idx,value) {
            var fieldName = value.split(" as ")[0].replace(/[\[\]\s]+/g, "").trim();

            if (fieldName.indexOf("(") > -1) {
                fieldName = fieldName.split("(")[1].replace(")", "").trim();
            }

           var chkBox=$(".ui-accordion-content > span > input[rel='" + fieldName + "']");

           if ($(chkBox).is(":checked")) {

                if ($(chkBox).siblings("label").length > 0) {

                    var aggfunction = $(chkBox).siblings("label").text().replace(/\(/g, '').replace(/\)/g, '');
                    aggFunctionExistsTF = true;

                    if (selectQueryList.length > 0) {
                        selectQueryList = selectQueryList + "," + aggfunction + "(" + chkBox.attr("rel") + ") as " + chkBox.attr("rel").split(".")[1];
                    }
                    else {
                        selectQueryList = aggfunction + "(" + chkBox.attr("rel") + ") as " + chkBox.attr("rel").split(".")[1];
                    }
                }
                else {
                    if (selectQueryList.length > 0) {
                        selectQueryList = selectQueryList + ", " + chkBox.attr("rel");
                    }
                    else {
                        selectQueryList = chkBox.attr("rel");
                    }

                    if (groupByList.length > 0) {
                        groupByList = groupByList + ", " + chkBox.attr("rel");
                    }
                    else {
                        groupByList = chkBox.attr("rel");
                    }
                }
            }
        });

        var sortedQueryList = "";

        sortedQueryList = selectQueryList; //$("#query-fields").text().trim();

        if (sortedQueryList.length > 0) {
            $("#query-fields").empty();
            $("#query-fields").text(sortedQueryList.replace(/Sum /g, ""));
 
            if (groupByList.length > 0 && aggFunctionExistsTF) {
                $("#query-groupby-clause").text(" GROUP BY ");
                $("#query-groupby-fields").text(groupByList);
            }
        }

        savedQueryLoaded = false;
    }

    function IsComputedFieldTF(value) {
        if (value.toLowerCase().indexOf("count(") > -1 || value.toLowerCase().indexOf("sum(") > -1 ||
                        value.toLowerCase().indexOf("avg(") > -1 || value.toLowerCase().indexOf("stdev(") > -1 ||
                        value.toLowerCase().indexOf("min(") > -1 || value.toLowerCase().indexOf("max(") > -1) {
            return true;
        }
        else {
            return false;
        }
    }

    function SetGridHorizontalScroll() {
        $(".datatable-scroll").mousewheel(function (event, delta) {
            this.scrollLeft -= (delta * 30);
            event.preventDefault();
        });
    }

    function FormatDataForExport(item) {
        if (moment.isDate(item)) {
            console.log("it's a date! :)");
            return moment(item, "YYYY-MM-DD");
        }
        console.log("It's not a date! :(");
        return item;
    }
</script>

<div style="padding-top:15px;" class="adhocquery-div-container">
 <div style="height:45px;" class="adhocquery-div">
    <div class="left-content"></div>
    <div class="main-content">
        <div id="saved-queries-section" class="ui-state-default ui-corner-all" style="float:right;display:none;width:300px;padding:7px;">
            <span>My Saved Queries:</span>
            <select id="ddlQueryName" class="lookup-dropdown" onchange="javascript:LoadSavedQuery();">
            </select>
        </div>
    </div>
</div>
<div class="left-content">
    <div id="tabs">
        <ul id="tab-items">
            <asp:PlaceHolder runat="server" ID="phTabs"></asp:PlaceHolder>
        </ul>
        <asp:PlaceHolder runat="server" ID="phFieldGroups"></asp:PlaceHolder>
    </div>
</div>
<div class="main-content">
      <asp:Panel runat="server" ID="pChart" class="grid-section" >
          <!--<a href="javascript:void(0);" onclick="javascript:ExportToExcel();">Export Test</a>--->
        <table id="adhoc-grid" class="grid-control">
            <thead>
                <tr id="adhoc-header">
                   <th></th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <asp:Literal runat="server" ID="litNoData" EnableViewState="false" ></asp:Literal>
                </tr>
            </tbody>
        </table>
        <span class="search-grid-message" style="display:block; color:#fff;"></span>
    </asp:Panel>
    <div style="width:100%; text-align:right;padding-top:3px;">
        <button id="btnClear" class="action-button">Clear</button>
        <button id="btnFilter" class="action-button ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" role="button" onclick="javascript:ShowFilter(true); return false;"><span class="ui-button-text">Filter</span></button>
        <button id="btnSaveQuery" onclick="javascript:ShowSavedQueryWindow('save'); return false;" class="action-button">Save Query</button>
        <button id="btnDeleteQuery" onclick="javascript:ShowSavedQueryWindow('delete'); return false;" class="action-button">Delete Query</button>
        <button id="btnRunQuery" onclick="javascript:GetResults(); return false;" class="action-button">Run Query</button>
        <img src="../images/inline-ajax-indicator.gif" class="progress-button" alt="" style="display:none;" />
    </div>
    <div style="margin-top:20px;">
        <div class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix" style="padding:5px;">
            <span>Query Window</span>
        </div>
        <div class="query-window" style="background-color:#fff; border: 1px solid #ccc;">
            <span id="query-type" style="display:block;white-space:pre;font-weight:bold;">SELECT </span>
            <span id="query-fields"  style="display:block;white-space:normal;"></span>
            <span id="query-from" style="display:block;white-space:pre;font-weight:bold;"> FROM </span>
            <span id="query-tables" style="display:block;white-space:pre;"></span>
            <span id="query-where-clause" style="display:inline-block;white-space:normal;"></span>
            <span id="query-groupby-clause" style="display:block;white-space:normal;font-weight:bold;"></span>
            <span id="query-groupby-fields" style="display:block;white-space:normal;"></span>
        </div>
    </div>
</div>
</div>
<div id="filter-dialog" title="Filter Your Search">
    <div id="filter-parameters"></div>
    <div style="width:100%;text-align:right;padding:5px;">
        <button id="btnOK" onclick="javascript:CleanUpQuery();">Ok</button>
    </div>
</div>
<div id="saved-queries-dialog" title="Save Query">
    <div id="saved-query-names">
        <span id="lblQueryName">Name of Query:</span>
        <input type="text" id="txtSavedQueryName" onkeyup="javascript:ValidateQueryName(this);" />
        <input type="text" id="txtQueryToDelete" class="lookup-combobox" data-field-name="QueryName" data-table-type="cms_SavedQueries" style="display:none;"   />
        <button id="btnSavedQueryOK" onclick="javascript:SaveQuery();" style="padding:3px;">Ok</button>
    </div>
    <div class="ui-widget" style="width:100%;padding: 10px 3px 3px 3px;">
        <div id="message-area" class="ui-state-highlight ui-corner-all" style="margin-top: 7px; padding: 0 .7em;display:none;">
            <span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span>
            <span id="spnAlreadyExistsMessage" style="display:none;" >A saved query with this name already exists.  The current query with this name will be overwritten when you click 'OK'.</span>
            <span id="spnNoNameForQuery" style="display:none;">You must enter a name for your query.</span>
        </div>
    </div>
</div>
<ul id="where-clause-list">
</ul>
<div style="color:#fff;height:700px;" id="export-result"></div>
