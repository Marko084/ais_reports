<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDLytlesWorkbookGrid.ascx.cs" Inherits="AISReports.UserControls.NCDLytlesWorkbookGrid" %>
  <script type="text/javascript" src="https://maps.google.com/maps/api/js?key=AIzaSyCglfuybluYEixFBAB-jSAdFUoOMO2SAKg""></script>
  <script type="text/javascript" src="../script/gmaps.js"></script>
  <script type="text/javascript" src="../script/prettify.js"></script>
  <link href='https://fonts.googleapis.com/css?family=Convergence|Bitter|Droid+Sans|Ubuntu+Mono' rel='stylesheet' type='text/css' />
  <!--<link href='../css/geo-map-styles.css' rel='stylesheet' type='text/css' />
  <link href='../css/prettify.css' rel='stylesheet' type='text/css' />
  <link rel="stylesheet" type="text/css" href="../css/examples.css" />-->
  <link rel="stylesheet" type="text/css" href="../Content/toastr.css" />
  <script type="text/javascript" src="../Scripts/toastr.js"></script>
<style type="text/css">
    .edit-field span {display:inline-block; width:300px;height:21px;float: left; vertical-align: top;}
    .question-label {display:block !important;width:100% !important;}
    td > span {display:inline-block;height:27px; vertical-align:middle;}
    td > input {padding-bottom:3px;}
    .office9999,.office9999 td {background:#00e5ee; padding-left:5px; text-align:left; border:solid 1px #fff;}
    .office385,.office385 td {background:#ff0000; padding-left:5px; text-align:left; border:solid 1px #fff;}
    .office1385,.office1385 td {background:#ff00ff; padding-left:5px;  text-align:left; border:solid 1px #fff;}
    .office1302,.office1302 td {background:#ffffff; padding-left:5px;  text-align:left; border:solid 1px #fff;}
    .office1305,.office1305 td {background:#ffff00; padding-left:5px; text-align:left; border:solid 1px #fff;}
    /*.move-legend-section {display:inline-block; color:#ffffff; text-align:left;width:33%; } */
    .move-legend-section{display:none;}
    /*.move-legend-section span {height:7px;width:15px;} */
    .filter-section {display:inline-block;width:66%;}
    .table-cell-checkbox {text-align:center !important; vertical-align: middle !important; width:30px !important;}
    .table-cell-multi-checkbox {text-align:center !important; vertical-align: middle !important; white-space:nowrap;}
    .day-group-header td span{display:block !important; padding: 7px 0px 7px 0px !important; font-size:14px; white-space:nowrap;font-weight:bold;}
    .cancelled-record td { text-decoration: line-through;}
    .cancelled-record td a {text-decoration:none !important;}
    .map-route-link {color:black;cursor:pointer;}
    .DataTables_sort_wrapper {height:51px !important; font-size:12px;}
    .edit-column-style {width:100px !important;}
    .no-wrap-column-style {white-space:nowrap !important; font-size:9px;}
    .day-print-btn {font-weight:bold;}
    table.dataTable tbody td {
        padding: 5px 5px !important;
        }

    table.dataTable thead th, table.dataTable thead td {
    white-space:normal !important;
    }
</style>
<asp:Literal runat="server" ID="litScript" />
<script type="text/javascript">
    var editMode;
    var identityField = "";
    var oGrid;
    var uid;

    $(document).ready(function () {
        
        try
        {
            var dataUri = "../LytlesHandler.ashx?qt=lookup&cid=" + cid + "&uid=" + uid + "&tn=Lytles_ScheduleBook&fn=Account&term=%&qid=" + S4();
            SetSelectControls(dataUri, $("#companyFilter"));

            $("#edit-dialog div table td input[aria-data-type='datetime']").datepicker({
                changeMonth: true,
                changeYear: true,
                showOn: "button",
                buttonImage: "../images/calendar.png",
                buttonImageOnly: true,
                buttonText: "Select date"
            });

            var startStartDate = $("#ctl00_ContentPlaceHolder1_SearchFilter1_txtstartstartdate").val();
            var startEndDate = $("#ctl00_ContentPlaceHolder1_SearchFilter1_txtstartenddate").val();
            var paramList = "StartStartDate~" + startStartDate + "|StartEndDate~" + startEndDate + "|Account~";

            $("#workbook-grid thead tr th").each(function () {
                if ($(this).text() == "CSR Contacted Transferee PK")
                {
                    $(this).text("CSR Cntd Transf.");
                }
            });

            GetScheduleBook(paramList);

            $("#btnSearch").hide();
            $("#btnLytlesSearch").show();
        }
        catch (e) {
            toastr.error(e.message, 'General Error',{ "positionClass": "toast-top-center" });
        }
        
    });

    function EmptyRecord() {
        return "OfficeAssigned~|Account~|PPWK~N%2FA|Trailer~|MoveAgent~|OriginDriverNames~|DestinationDriverNames~|OriginHelperNames~|DestinationHelperNames~|RegNumber~|Name~|EmailAddress~|PickupLocation~|DeliveryLocation~|Weight~|PKStartDate~|PKEndDate~|LDStartDate~|LDEndDate~|DELStartDate~|DELEndDate~|Cancelled~false|ShipmentDelivered~false|CSRContactedTransfereePK~false|CSRContactedTransfereeLD~false|CSRContactedTransfereeDEL~false|Details~|ContactedTransfereeNotes~";
    }

    function PopulateDaysInMonthControl() {
            var selectedMonth = $("#monthFilter").val();
            var selectedYear = $("#yearFilter").val();
            var selectedDay = $("#dayFilter").val();
            var dayCount = 0;
            var dayList = "<option value=''>Select a Day</option>";

            if (selectedMonth == "") {
                selectedMonth = new Date().getMonth() + 1;
            }

            if (selectedYear == "") {
                selectedYear = new Date().getFullYear();
            }

            dayCount = daysInMonth(selectedMonth, selectedYear);
            
            for (i = 0; i < dayCount; i++) {
                dayList += "<option value='" + (i + 1) + "'>" + (i + 1) + "</option>";
            }

            $("#dayFilter").empty();
            $("#dayFilter").append(dayList);
            $("#dayFilter").val(selectedDay);
    }

    function Sleep(milliSeconds) {
        var startTime = new Date().getTime(); // get the current time
        while (new Date().getTime() < startTime + milliSeconds); // hog cpu
    }

    function EditDialogCompleted(data)
    {
        var useEnc = false;
        if (typeof data === 'string' && data.indexOf("ENC|") == 0) {
            useEnc = true;
            data = data.substring(4).split("|");
        }

        $.each(data, function (index, item) {
            try {
                if (!item) return;
                var parts = item.split("~");
                var fieldName = parts[0];
                var val = parts[1] || "";
                var fieldValue = (useEnc ? decodeURIComponent(val) : val).replace("1/1/1900 12:00:00 AM", "").replace("12:00:00 AM", "");

                if (fieldName == "ShipmentDelivered") {
                    $("#edit-dialog div table td input[data-field-name='" + fieldName + "']").prop("checked",(fieldValue==="True"));
                }
                else if (fieldName == "CSRContactedTransfereePK" || fieldName == "CSRContactedTransfereeLD" || fieldName == "CSRContactedTransfereeDEL") {
                    $("#edit-dialog div table td input[data-field-name='" + fieldName + "']").prop("checked", (fieldValue === "True"));
                }
                else if (fieldName == "Cancelled") {
                    $("#edit-dialog div table td input[data-field-name='" + fieldName + "']").prop("checked", (fieldValue === "True"));
                }
                else if ($("#edit-dialog div table td input[data-field-name='" + fieldName + "']").length) {
                    $("#edit-dialog div table td input[data-field-name='" + fieldName + "']").val(fieldValue);
                }
                else if ($("#edit-dialog div table td textarea[data-field-name='" + fieldName + "']").length) {
                    $("#edit-dialog div table td textarea[data-field-name='" + fieldName + "']").val(fieldValue);
                }
                else if (fieldName == "OriginHelperNames") {
                    var resultArray = fieldValue.split(",");
                    $("#edit-dialog div table td select[data-field-name='" + fieldName + "']").val(resultArray);
                }
                else if (fieldName == "DestinationHelperNames") {
                    var resultArray = fieldValue.split(",");
                    $("#edit-dialog div table td select[data-field-name='" + fieldName + "']").val(resultArray);
                }
                else if ($("#edit-dialog div table td select[data-field-name='" + fieldName + "']").length) {
                    $("#edit-dialog div table td select[data-field-name='" + fieldName + "']").val(fieldValue);
                }
            }
            catch (e) {
                toastr.error("get data: " + e.Message,'Edit Form Error',{ "positionClass": "toast-top-center" });
            }
        });
    }

    function GetLytlesSelectionCriteria() {
        var paramList = "";
        var paramName = "";
        var paramValue = "";

        $(".slide-out-div input:text").each(function () {
            paramName = $(this).attr("data-parameter-name");
            paramValue = $(this).val();

            if (paramList.length == 0) {
                paramList = paramName + "~" + paramValue;
            }
            else {
                paramList += "|" + paramName + "~" + paramValue;
            }
        });

        GetScheduleBook(paramList);

        return paramList;
    }

    function SetFilterLabel() {
        $(".search-filter-label").text("Lytle's Transfer and Storage - " + $("#monthFilter option:selected").text() + " " + $("#yearFilter option:selected").text())
    }

    function FilterData(ele) {

        var currentMonth = $("#monthFilter").val();
        var currentYear = $("#yearFilter").val();
        var lastDayOfMonth = daysInMonth(currentMonth, currentYear);
        var selectedCompany = "";
        var startStartDate = $("#ctl00_ContentPlaceHolder1_SearchFilter1_txtstartstartdate").val();
        var startEndDate = $("#ctl00_ContentPlaceHolder1_SearchFilter1_txtstartenddate").val();
        var currentDay;

        if (currentMonth != "" && currentYear != "") {

            if ($(ele).attr("id") == "monthFilter" || $(ele).attr("id") == "yearFilter") {
                PopulateDaysInMonthControl();
            }

            currentDay = $("#dayFilter").val();

            if (currentDay == "") {
                startStartDate = currentMonth + "/1/" + currentYear;
                startEndDate = currentMonth + "/" + lastDayOfMonth + "/" + currentYear;
            }
            else {
                startStartDate = currentMonth + "/" + currentDay + "/" + currentYear;
                startEndDate = currentMonth + "/" + currentDay + "/" + currentYear;
            }

        }

        if($("#companyFilter").val() !="All") {
            selectedCompany=$("#companyFilter").val();
        }

        if ((currentMonth != "" && currentYear != "") || (currentMonth == "" && currentYear == "")) {
            var paramList = "StartStartDate~" + startStartDate + "|StartEndDate~" + startEndDate + "|Account~" + selectedCompany;

            GetScheduleBook(paramList);
            SetFilterLabel();
        }

    }

    function daysInMonth(month, year) {
        var dd = new Date(year, month, 0);
        return dd.getDate();
    }

    function LoadSelectionFields() {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../LytlesHandler.ashx?gt=grid&ct=grid&qt=storedproc&fld=TableName|Name&qn=Lytles_GetWorkbookLists&pl=CompanyID~10102&qid=" + S4(),
            dataType: "json",
            success: function (data) {
                var accountsList = "";
                var helpersList = "";
                var officesList = "";
                var driversList = "";
                var trailersList = "";
                var moveAgentsList = "";

                try{
                    $.each(data.aaData, function (idx, item) {

                        if (item[1] == "Accounts") {
                            accountsList= accountsList + "<option value='" + item[2] + "'>" + item[2] + "</option>";
                        }
                        else if (item[1] == "Drivers") {
                            driversList= driversList + "<option value='" + item[2] + "'>" + item[2] + "</option>";
                        }
                        else if (item[1] == "Helpers") {
                            helpersList= helpersList + "<option value='" + item[2] + "'>" + item[2] + "</option>";
                        }
                        else if (item[1] == "Offices") {
                            officesList= officesList + "<option value='" + item[2] + "'>" + item[2] + "</option>";
                        }
                        else if (item[1] == "MoveAgents") {
                            moveAgentsList= moveAgentsList + "<option value='" + item[2] + "'>" + item[2] + "</option>";
                        }
                        else if (item[1] == "Trailers") {
                            trailersList= trailersList +"<option value='" + item[2] + "'>" + item[2] + "</option>";
                        }
                    });
                    $("#officeAssigned").html(officesList);
                    $("#account").html(accountsList);
                    $("#trailer").html(trailersList);
                    $("#moveAgentNames").html(moveAgentsList);
                    $("#originDriverNames").html(driversList);
                    $("#destinationDriverNames").html(driversList);
                    $("#originHelperNames").html(helpersList);
                    $("#destinationHelperNames").html(helpersList);
   
                }
                catch (e) {
                    toastr.error("Error: " + xhr.responseText,'Loading Controls Error',{ "positionClass": "toast-top-center" });
                }
               
            },
            error: function (xhr, textStatus, error) { toastr.error("Error: " + xhr.responseText,'Loading Controls Error',{ "positionClass": "toast-top-center" }); }
        });
    }

    function SetSelectionFields(ele,allTF) {

       // $("#edit-dialog div table td select").each(function () {

           var dataFieldName=$(ele).attr("data-field-name");
           var dataUri = "";
           var searchFieldName = "";
           var tableName = "";
           var officeNumber;
           var originHelperSelect = $("#edit-dialog div table td select[data-field-name='OriginHelperNames']");
           var destinationHelperSelect = $("#edit-dialog div table td select[data-field-name='DestinationHelperNames']");
           var trailerSelect = $("#edit-dialog div table td select[data-field-name='Trailer']");
           var moveAgentSelect = $("#edit-dialog div table td select[data-field-name='MoveAgent']");
           var originDriverSelect = $("#edit-dialog div table td select[data-field-name='OriginDriverNames']");
           var destinationDriverSelect = $("#edit-dialog div table td select[data-field-name='DestinationDriverNames']");

           if (dataFieldName == "OfficeAssigned" && allTF) {
               searchFieldName = $(ele).attr("search-field-name");
               tableName = $(ele).attr("table-name");
               //dataUri = "../LytlesHandler.ashx?qt=lookup&cid=" + cid + "&uid=" + uid + "&tn=" + tableName + "&fn=" + searchFieldName + "&term=%&qid=" + S4();
               //SetSelectControls(dataUri, $(ele))
           }

           if (dataFieldName == "Account" && allTF) {
               searchFieldName = $(ele).attr("search-field-name");
               tableName = $(ele).attr("table-name");
               //dataUri = "../LytlesHandler.ashx?qt=lookup&cid=" + cid + "&uid=" + uid + "&tn=" + tableName + "&fn=" + searchFieldName + "&term=%&qid=" + S4();
               //SetSelectControls(dataUri, $(ele))
           }

           officeNumber = $("#edit-dialog div table td select[data-field-name='OfficeAssigned']").val();

           if (officeNumber == null) {
               officeNumber = "1305";
           }

           searchFieldName = $(originHelperSelect).attr("search-field-name");
           tableName = $(originHelperSelect).attr("table-name");
           //dataUri = "../LytlesHandler.ashx?qt=lookup&cid=" + cid + "&uid=" + uid + "&tn=" + tableName + "&fn=HelperName&term=" + officeNumber + "&qid=" + S4();
           //SetSelectControls(dataUri, $(originHelperSelect));

           searchFieldName = $(destinationHelperSelect).attr("search-field-name");
           tableName = $(destinationHelperSelect).attr("table-name");
           //dataUri = "../LytlesHandler.ashx?qt=lookup&cid=" + cid + "&uid=" + uid + "&tn=" + tableName + "&fn=HelperName&term=" + officeNumber + "&qid=" + S4();
           //SetSelectControls(dataUri, $(destinationHelperSelect));

           if (dataFieldName == "Trailer" && allTF) {
               searchFieldName = $(trailerSelect).attr("search-field-name");
               tableName = $(trailerSelect).attr("table-name");
               //dataUri = "../LytlesHandler.ashx?qt=lookup&cid=" + cid + "&uid=" + uid + "&tn=" + tableName + "&fn=" + searchFieldName + "&term=&qid=" + S4();
               //SetSelectControls(dataUri, $(trailerSelect))
           }

           searchFieldName = $(moveAgentSelect).attr("search-field-name");
           tableName = $(moveAgentSelect).attr("table-name");
           //dataUri = "../LytlesHandler.ashx?qt=lookup&cid=" + cid + "&uid=" + uid + "&tn=" + tableName + "&fn=" + searchFieldName +"&qid=" + S4();
           //SetSelectControls(dataUri, $(moveAgentSelect))

           searchFieldName = $(originDriverSelect).attr("search-field-name");
           tableName = $(originDriverSelect).attr("table-name");
           //dataUri = "../LytlesHandler.ashx?qt=lookup&cid=" + cid + "&uid=" + uid + "&tn=" + tableName + "&fn=DriverName&term=" + officeNumber + "&qid=" + S4();
           //SetSelectControls(dataUri, $(originDriverSelect))

           searchFieldName = $(destinationDriverSelect).attr("search-field-name");
           tableName = $(destinationDriverSelect).attr("table-name");
           //dataUri = "../LytlesHandler.ashx?qt=lookup&cid=" + cid + "&uid=" + uid + "&tn=" + tableName + "&fn=DriverName&term=" + officeNumber + "&qid=" + S4();
           //SetSelectControls(dataUri, $(destinationDriverSelect))

    }

    function SetSelectControls(dataUri, ele) {

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: dataUri,
            dataType: "json",
            success: function (data) {
                var result = "";
                try{
                    $.each(data, function (idx, item) {
                        result = result + "<option value='" + item.value + "'>" + item.value + "</option>";
                    });
                }
                catch (e) {
                    //alert($(ele).attr("id")+" Error: "+e.message);
                }

                $(ele).empty();
                $(ele).html(result);

                if($(ele).attr("id")=="companyFilter"){
                    $("#companyFilter").prepend("<option value='All'>ALL</option>").val('All');
                    //FilterData();
                }
                //else if($(ele).attr("id")=="officeAssigned"){
                //    $("#officeAssigned").prepend("<option value=''></option>").val('');
                //}
                //else if ($(ele).attr("id") == "account") {
                //    $("#account").prepend("<option value=''></option>").val('');
                //}
                //else if ($(ele).attr("id") == "trailer") {
                //    $("#trailer").prepend("<option value=''></option>").val('');
                //    $("#trailer").val("N/A");
                //}
                
            },
            error: function (xhr, textStatus, error) { toastr.error("Error: " + xhr.responseText,'Loading Controls Error',{ "positionClass": "toast-top-center" }); }
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
            $(this).autocomplete("search");
        });
    }

    function GetScheduleBook(paramList) {

        if (paramList.length == 0) {
            paramList = "@CompanyID~" + cid + "|ConsolidatedTF~0|UserID~" + uid;
        }
        else {
            paramList += "|@CompanyID~" + cid + "|ConsolidatedTF~0|UserID~" + uid;
        }

        var table = $("#workbook-grid");
        var tableColumns = GetTableColumnNames(table);
        //console.log(tableColumns);

        var colIndex = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == "import id"; });
        var officeIdx = 17;
        var nameIdx = 6;
        var dayLabelIdx = 0;
        var primaryKeyIdx = 1;

        var DataQuery = {
            FieldList: $("#<%= pChart.ClientID %>").attr("grid-display-fields"), TableList: "", FilterList: "",
            OrderByList: "", Query: "Lytles_GetWorkbook", QueryType: "storedproc", QueryParamList: paramList
        };

        var dataUri = "../LytlesHandler.ashx?gt=grid&ct=grid&qt=" + DataQuery.QueryType + "&fld=" + DataQuery.FieldList + "&qn=" + DataQuery.Query + "&pl=" + DataQuery.QueryParamList + "&qid=" + S4();

        oGrid = $(table).DataTable({
            "bDeferRender": true,
            "bDestroy": true,
            ajax: dataUri,
            fixedHeader: true,
            colReorder:true,
            //"sPaginationType": "full_numbers",
            //"aLengthMenu": [[-1, 25, 50, 100], ["All", 25, 50, 100]],
            "aoColumnDefs": [{ className: "table-cell-multi-checkbox", "aTargets": [2] },
                             { className: "table-cell-checkbox", "aTargets": [16, 18] },
                             { className: "edit-column-style", "aTargets": [1] },
                             { className: "no-wrap-column-style", "aTargets": [3, 4, 5, 6, 7,8,9,10,11,12,13] }],
            displayLength: -1,
            language: {
                "emptyTable": "No data found to display."
            },
            //dom: '<"fg-toolbar ui-toolbar ui-widget-header ui-helper-clearfix ui-corner-tl ui-corner-tr"Blfr><"dataTables_scroll"t><"fg-toolbar ui-toolbar ui-widget-header ui-helper-clearfix ui-corner-bl ui-corner-br"ip>',
           // buttons: [{ extend: 'excel', filename: 'EditGridExport', className: 'ui-corner-br fg-button ui-button ui-state-default' }],
            "initComplete": function (oSettings, json) {
                if ($("#defaultDayAnchor").val() == "") {
                    location.hash = "#" + GetCurrentDayAnchor();
                    $("#defaultDayAnchor").val(GetCurrentDayAnchor());
                }
            },
            rowCallback: function (nRow, aData, iDisplayIndex) {
                //colIndex = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == "import id"; });
                //nameIdx = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == "name"; });
                //officeIdx = oGrid.fnGetColumnIndex("Office Assigned");
                if (typeof colIndex[0] != 'undefined') {
                    primaryKeyIdx = colIndex[0].idx;
                }

                 var editLink = "<a href='#' rel='" + aData[primaryKeyIdx] + "' onclick='javascript:RunQuery(this); return false;'>Edit</a>&nbsp;";
                //var editLink = "<a href='#' rel='" + aData[primaryKeyIdx] + "' onclick='javascript:RunQuery(this);ShowEditDialog(this); return false;'>Edit</a>&nbsp;";
                var deleteLink = ""; // "<a href='#' rel='" + aData[primaryKeyIdx] + "' onclick='javascript: RunQuery(this); return false;'>Delete</a>&nbsp;";
                var printLink = "<a href='#' rel='" + aData[primaryKeyIdx] + "' onclick='javascript: PrintJob(this); return false;'>Print</a>";
                var officeNumber = aData[officeIdx];

                //nameIdx = oGrid.fnGetColumnIndex("Name");
                
                //colIndex = oGrid.fnGetColumnIndex("Import ID");
                //console.log("Day label Idx :" + nameIdx);
                var pickupLoc = "";
                var delLoc = "";
                var recType = aData[8].replace(" ","|");

                try {

                    if (aData[7].split(' to ').length < 2) {
                        pickupLoc = aData[7].split(" to ")[0];
                    }
                    else {
                        pickupLoc = aData[7].split(" to ")[0];
                        delLoc = aData[7].split(" to ")[1];
                    }

                   var mapUrl = "https://www.google.com/maps/dir/" + pickupLoc.replace(/ /g, "+") + "/" + delLoc.replace(/ /g, "+");
                   $('td:eq(4)', nRow).html("<a href='" + mapUrl + "' rel='" + aData[primaryKeyIdx] + "' target='_blank' class='map-route-link'>" + aData[7] + "</a>");
                }
                catch (e) {
                    console.log(e.Message);
                }

                //console.log("Cancelled column Idx: " + oGrid.fnGetColumnIndex("Cancelled"));
                //console.log("Shipment Delivered column Idx: " + oGrid.fnGetColumnIndex("Shipment Delivered"));
                $('td:eq(0)', nRow).html(editLink + deleteLink+printLink);
                $('td:eq(1)', nRow).html("<input type='checkbox' " + (aData[2] == 'True' ? "checked" : "") + " disabled='disabled' "+(recType.indexOf("PK")==-1 ? "style='display:none;'" : "")+" />" +
                                         "<input type='checkbox' " + (aData[3] == 'True' ? "checked" : "") + " disabled='disabled' " + (recType.indexOf("LD") == -1 ? "style='display:none;'" : "") + "/>" +
                                         "<input type='checkbox' " + (aData[4] == 'True' ? "checked" : "") + " disabled='disabled' " + (recType.indexOf("DEL") == -1 ? "style='display:none;'" : "") + "/>");

                //Cancelled Column.
                //$('td:eq(13)', nRow).html("<input type='checkbox' " + (aData[16] == 'True' ? "checked" : "") + " disabled='disabled' />");

                //Shipment Delivered Column.
                $('td:eq(13)', nRow).html("<input type='checkbox' " + (aData[18] == 'True' ? "checked" : "") + " disabled='disabled' />");

                $(nRow).removeClass("even");
                $(nRow).removeClass("odd");

                if (officeNumber == "9999") {
                    $(nRow).addClass("office9999");
                }
                else if (officeNumber == "385") {
                    $(nRow).addClass("office385");
                }
                else if (officeNumber == "1385") {
                    $(nRow).addClass("office1385");
                }
                else if (officeNumber == "1302") {
                    $(nRow).addClass("office1302");
                }
                else if (officeNumber == "1305") {
                    $(nRow).addClass("office1305");
                }

                //console.log("Account: "+oGrid.fnGetColumnIndex("Account"))
                if (aData[primaryKeyIdx] == 0) {
                    $('td:eq(0)', nRow).html("<span id='" + aData[nameIdx].replace(/\s+/g, '') + "'>" + aData[nameIdx] + "</span>");
                    $('td:eq(1)', nRow).html("<a href='javascript:void(0);' class='day-print-btn' rel='"+aData[19]+"' onclick='javascript:PrintWorkbookByDay(this);'>Print Day</a>");
                    $('td:eq(2)', nRow).html("");
                    $('td:eq(3)', nRow).html("");
                    $('td:eq(4)', nRow).html("");
                    $('td:eq(12)', nRow).html("");
                    $('td:eq(13)', nRow).html("");
                    $('td:eq(14)', nRow).html("");
                    $(nRow).addClass("day-group-header");
                }
                else if (aData[16] == 'True') {
                    $(nRow).addClass("cancelled-record");
                }
            }
        });
        //new FixedHeader(oGrid);

        ////oGrid.fnSetColumnVis(15, false);
        //oGrid.fnSetColumnVis(3, false);
        //oGrid.fnSetColumnVis(4, false);
        //oGrid.fnSetColumnVis(colIndex, false);
        //oGrid.fnSetColumnVis(officeIdx, false);
        //oGrid.fnSetColumnVis(16, false);

        oGrid.column(3).visible(false);
        oGrid.column(4).visible(false);
        oGrid.column(primaryKeyIdx).visible(false);
        oGrid.column(officeIdx).visible(false);
        oGrid.column(16).visible(false);

        $(".dataTables_length").hide();
        $(".dataTables_paginate").hide();

    }

    function PrintWorkbookByDay(ele)
    {
        var url = "../PrintPage.aspx?cid=" + cid + "&cdate=" + $(ele).attr("rel");
        $("#frmDownloadPDF").attr("src", url);
    }

    function GetEditFormValues() {
        var fieldList = "";

        $("#edit-dialog div table td select").each(function () {
                var val = $(this).val() || "";
                if (fieldList == "") {
                    fieldList = $(this).attr("data-field-name") + "~" + encodeURIComponent(val);
                }
                else {
                    fieldList += "|" + $(this).attr("data-field-name") + "~" + encodeURIComponent(val);
                }
        });

        $("#edit-dialog div table td input[type='text']").each(function () {
                var val = $(this).val() || "";
                if (fieldList == "") {
                    fieldList = $(this).attr("data-field-name") + "~" + encodeURIComponent(val);
                }
                else {
                    fieldList += "|" + $(this).attr("data-field-name") + "~" + encodeURIComponent(val);
                }
        });

        $("#edit-dialog div table td input[type='checkbox']").each(function () {
            if (fieldList == "") {
                fieldList = $(this).attr("data-field-name") + "~" + $(this).is(":checked");
            }
            else {
                fieldList += "|" + $(this).attr("data-field-name") + "~" + $(this).is(":checked");
            }
        });

        $("#edit-dialog div table td textarea").each(function () {
                var val = $(this).val() || "";
                if (fieldList == "") {
                    fieldList = $(this).attr("data-field-name") + "~" + encodeURIComponent(val);
                }
                else {
                    fieldList += "|" + $(this).attr("data-field-name") + "~" + encodeURIComponent(val);
                }
        });

        return "ENC|" + fieldList.replace(/null/g, "");
    }

    function RunQuery(obj) {

        var tableName = $(".edit-grid-dropdown-filter option:selected").val();
        var primaryKeyValue = $(obj).attr("rel");
        var primaryKeyName = "ImportID";

        editMode = $(obj).text().toLowerCase().split(" ")[0];

        if (primaryKeyValue == "0") {
            editMode = "add";
        }
        else if (editMode == "submit") {
            editMode = "save";
        }

        if (editMode == "add") {

            if ($("#edit-dialog div table td input[data-field-name='EmailAddress']").val() == ""){
                var confirmVal = confirm("The Email address field has not been filled out.  Do you want to continue?");

                if (confirmVal == false) {
                    return true;
                }
            }
        }
        
        if (tableName != "") {
            var NewJSONDBQuery = {};
            var modeDisplay = "";

            if (editMode == "edit") {
                NewJSONDBQuery.FieldList = "ENC|" + $(".grid-section").attr("database-fields");
            }
            else if (editMode == "save" || editMode == "add") {
                NewJSONDBQuery.FieldList = GetEditFormValues(); //$(".grid-section").attr("database-fields");
            }
            else if (editMode == "delete") {
                NewJSONDBQuery.FieldList = GetEditFormValues();
            }
            else {
                NewJSONDBQuery.FieldList = "";
            }

            NewJSONDBQuery.TableName = "Lytles_Schedulebook";
            NewJSONDBQuery.KeyFieldName = primaryKeyName;
            NewJSONDBQuery.KeyFieldValue = primaryKeyValue;
            NewJSONDBQuery.QueryType = (editMode === "delete" ? "save" : editMode);
            NewJSONDBQuery.KeyFieldIsIdentity = "true";
            NewJSONDBQuery.UserID = uid;

            if (editMode == "add") {
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
                $("#btnSaveJob").attr("rel", primaryKeyValue);
                $("#btnDeleteJob").attr("rel", primaryKeyValue);
                $("#btnPrintJob").attr("rel", primaryKeyValue);
                ClearFields(); 
            }

            if (NewJSONDBQuery.FieldList === "ENC|" + EmptyRecord() && (editMode==="edit" || editMode ==="add")) {
                toastr.error("There was an error with " + editMode + " this record.","Empty Record Detected",{ "positionClass": "toast-top-center" });
                return true;
            }
            var DTO = { 'NewJSONDBQuery': NewJSONDBQuery };
            //console.log(JSON.stringify(DTO));
            //alert("Done!");
            
            //return true;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../AISWS.asmx/JSONDBQuery",
                data: JSON.stringify(DTO),
                dataType: "json",
                beforeSend: function () {
                    
                    //LoadSelectionFields();
                    //SetSelectionFields($("#edit-dialog div table td select[data-field-name='OfficeAssigned']"), true);
                    //SetSelectionFields($("#edit-dialog div table td select[data-field-name='Account']"), true);
                    //SetSelectionFields($("#edit-dialog div table td select[data-field-name='Trailer']"), true);
                },
                success: function (data) {
                    LoadSelectionFields();
                    RunChangeLog(primaryKeyValue);

                    if (modeDisplay == "get") {
                        var fList = data.d.FieldList;
                        var useEnc = false;
                        if (fList.indexOf("ENC|") == 0) {
                            useEnc = true;
                            fList = fList.substring(4);
                        }
                        var record = fList.split("|");
                        var fieldName = "";
                        var fieldValue = "";

                        $.each(record, function (index, item) {
                            try{
                                if (!item) return;
                                var parts = item.split("~");
                                fieldName = parts[0];
                                var val = parts[1] || "";
                                fieldValue = (useEnc ? decodeURIComponent(val) : val).replace("1/1/1900 12:00:00 AM", "").replace("12:00:00 AM", "");

                                if (fieldName == "Account") {

                                    $("#edit-dialog div table td select[data-field-name='" + fieldName + "']").val(fieldValue);
                                    SetSelectionFields($("#edit-dialog div table td select[data-field-name='Account']"), false);
                                }

                                if (fieldName == "Trailer") {

                                    $("#edit-dialog div table td select[data-field-name='" + fieldName + "']").val(fieldValue);
                                    SetSelectionFields($("#edit-dialog div table td select[data-field-name='Trailer']"), false);
                                }

                                if (fieldName == "OfficeAssigned") {

                                    $("#edit-dialog div table td select[data-field-name='" + fieldName + "']").val(fieldValue);
                                    SetSelectionFields($("#edit-dialog div table td select[data-field-name='OfficeAssigned']"), false);
                                    setTimeout(function () {
                                        EditDialogCompleted(record);
                                        ShowEditDialog(obj);
                                    },500);
                                }
                            }
                            catch (e) {
                                toastr.error(e.message+' Please contact AIS.', 'Error Loading Form', { "positionClass": "toast-top-center" });
                            }
                        });

                        //if (editMode == "edit") {
                        //    ShowEditDialog(obj);
                        //}
                    }

                    if (modeDisplay != "get") {

                        $("#edit-dialog").dialog("close");
                        //oGrid.fnReloadAjax(null, null, true);
                        oGrid.ajax.reload();

                        if (modeDisplay != "deleted" && modeDisplay != "" && modeDisplay != null) {
                            toastr.success('Data was successfully ' + modeDisplay,'Record Saved',{ "positionClass": "toast-top-center" });
                        }
                        else if (modeDisplay == "deleted") {
                            toastr.info('Data was successfully ' + modeDisplay, 'Deleted Record',{ "positionClass": "toast-top-center" });
                        }
                    }

                },
                error: function (xhr, textStatus, error) { toastr.error("Error: " + xhr.responseText,"Error Updating Record",{ "positionClass": "toast-top-center" }); }
            });
        }
    }

    function ShowEditDialog(obj) {

        if ($(obj).text().toLowerCase().indexOf("add job to workbook") != -1) {
           // ClearFields();
            $(obj).attr("rel", "0");
            //$("#btnSaveJob").attr("rel", "0");
           // $("#btnDeleteJob").attr("rel", "0");
            //LoadSelectionFields();
            //SetSelectionFields($("#edit-dialog div table td select[data-field-name='OfficeAssigned']"), true);
            //SetSelectionFields($("#edit-dialog div table td select[data-field-name='Account']"), true);
            //SetSelectionFields($("#edit-dialog div table td select[data-field-name='Trailer']"), true);
        }
        var currentUser = $("#ctl00_lblUserInfo").text().replace("Welcome ", "");
        var editID = $(obj).attr("rel");
        $("#edit-dialog").empty();
        $("#edit-dialog").append($("<iframe id='workbookEditForm' width='1090' height='780' frameborder='0' />").attr("src","LytlesWorkbookEdit.aspx?id="+editID+"&editedby="+currentUser)).dialog({ width: "auto", height: "auto", modal: true, zIndex: 9000 });
        //$("#edit-dialog").dialog({
        //    width: "auto",
        //    height: "auto",
        //    modal: true,
        //    zIndex: 9000
        //});
    }

    function CloseEditDialog(status,msg,title) {
        
        if (status === "error") {
            toastr.error(msg, title, { "positionClass": "toast-top-center" });
            return false;
        }
        else if (status === "success") {
            toastr.success(msg, title, { "positionClass": "toast-top-center" });
        }
        else if (status === "warning") {
            toastr.warning(msg, title, { "positionClass": "toast-top-center" });
        }
        else if(status==="info") {
            toastr.info(msg, title, { "positionClass": "toast-top-center" });
        }
        
        oGrid.ajax.reload();
        $("#edit-dialog").dialog("close");

        return false;
    }

    function ClearFields() {
        $("#edit-dialog div table td input[type='text']").each(function () {
            if (!$(this).is(":disabled")) {
                $(this).val("");
            }
        });

        $("#edit-dialog div table td input[type='checkbox']").each(function () {
            if (!$(this).is(":disabled")) {
                $(this).prop('checked', false);
            }
        });

        $("#edit-dialog div table td textarea").each(function () {
            $(this).val("");
        });

        $("#edit-dialog div table td select").each(function () {
            if ($(this).attr("data-field-name") != "PPWK") {
                $(this).empty();
            }
        });
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

    function GetCurrentDayAnchor() {

        var d = new Date();
        var weekday = ["Sun","Mon","Tue","Wed","Thu", "Fri","Sat"];
        var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
                          "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

        var wDay = weekday[d.getDay()];
        var m = monthNames[d.getMonth()];
        var mDay = d.getDate();

        //alert(wDay + m + mDay);
        return wDay + m + mDay;
    }

    function GetMapRoute(ele) {
        
        try {
            //var map;
            var dataUri = "../LytlesHandler.ashx?gt=none&ct=grid&qt=storedproc&fld=&qn=lytles_getmaproute&pl=ImportID~" + $(ele).attr("rel") + "&qid=" + S4();
            //$("#nomap").hide();

            //$("#map-dialog").dialog({
            //    width: "650",
            //    height: "450",
            //    modal: true
            //});

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: dataUri,
                dataType: "json",
                success: function (data) {

                    //alert(JSON.stringify(data));
                    var originLocation = data.aaData[0][1];
                    var destLocation = data.aaData[0][2];

                    //if (data.aaData.length == 2) {
                    //    originLocation = data.aaData[0][4] + ", " + data.aaData[0][6];
                    //    destLocation = data.aaData[1][4] + ", " + data.aaData[1][6];
                    //}
                    //else if(data.aaData.length==1)
                    //{
                    //    if (data.aaData[0][1] == "Pickup") {
                    //        originLocation = data.aaData[0][4] + ", " + data.aaData[0][6];
                    //    }
                    //    else {
                    //        destLocation = data.aaData[0][4] + ", " + data.aaData[0][6];
                    //    }
                    //}

                     var mapUrl = "https://www.google.com/maps/dir/" + originLocation.replace(/ /g, "+") + "/" + destLocation.replace(/ /g, "+");
                    $(this).attr("href", mapUrl);
                     //alert(mapUrl);
                     return mapUrl;
                   //$(this).click();
                    //window.open("https://www.google.com/maps/dir/" + originLocation.replace(/ /g, "+") + "/" + destLocation.replace(/ /g, "+"));

                },
                error: function (xhr, textStatus, error) { toastr.error("Error: " + xhr.responseText); }
            });

        }
        catch (e) {
            toastr.error(e.message,"Mapping Data Error",{ "positionClass": "toast-top-center" });
        }
    }

    function ContactCheckBoxChanged(ele) {
        
        if ($(ele).prop("checked")) {
            var currentUser = $("#ctl00_lblUserInfo").text().replace("Welcome ", "");
            var msg = currentUser + " checked " + $(ele).attr("rel") + " on " + GetTimeStamp() + "\r\n";
            msg += $("#ContactedTransfereeNotes").val();

            $("#ContactedTransfereeNotes").val(msg);
        }
    }

    function PrintJob(ele) {
        var recID = $(ele).attr("rel");
        var url="../PrintPage.aspx?cid="+cid+"&tn=vw_lytles_schedulebook&kn=importid&kv="+recID;
        $("#frmDownloadPDF").attr("src", url);
    }

    function DeleteJob(ele) {
        $("#CancelledShipmentCheckBox").prop('checked', true);
        RunQuery(ele);
    }

    function RunChangeLog(id) {
        uid = $(".uid").text();
        $.ajax({
            url: "../ListHandler.ashx?gt=none&ct=clist&qt=storedproc&fld=RowCount&qn=Lytles_Workbook_ChangeLogTracker&pl=UserID~"+uid+"|ImportID~"+id+"&qid=" + S4(),
            dataType: "json",
            success: function (result) {
                console.log(result);
            },
            error: function (xhr, textStatus, error) { toastr.error("Error: " + error,"Recording Change Log Error"); }
        });
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
    <br />
    <a href="javascript:void(0);" id="lnkAddRecord" rel="0" onclick='ShowEditDialog(this); return false;'>Add Job to Workbook</a>
    <br />
    <br />
    <div class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix" style="padding: 5px;">
        <asp:Label runat="server" ID="lblChartTitle" Text="" EnableViewState="false" />
    </div>
    <asp:Panel runat="server" ID="pChart" class="grid-section">
        <div id="edit-grid-filter-section" class="chart-title" style="text-align:center;height:80px;padding:5px;">
            <div class="filter-section">
                <span style="display:none;">Tables:</span>
                <asp:DropDownList runat="server" ID="ddlTables" CssClass="edit-grid-dropdown-filter" style="display:none;" />
                <span class="search-filter-label"></span>
                <br />
                <select id="monthFilter" onchange="javascript:FilterData(this);">
                    <option value="" selected>Select a Month</option>
                    <option value="1">January</option>
                    <option value="2">February</option>
                    <option value="3">March</option>
                    <option value="4">April</option>
                    <option value="5">May</option>
                    <option value="6">June</option>
                    <option value="7">July</option>
                    <option value="8">August</option>
                    <option value="9">September</option>
                    <option value="10">October</option>
                    <option value="11">November</option>
                    <option value="12">December</option>
                </select>
                <select id="yearFilter" onchange="javascript:FilterData(this);">
                    <option value="" selected>Select a Year</option>
                    <option value="2016">2016</option>
                    <option value="2017">2017</option>
                    <option value="2018">2018</option>
                    <option value="2019">2019</option>
                    <option value="2020">2020</option>
                    <option value="2021">2021</option>
                    <option value="2022">2022</option>
                    <option value="2023">2023</option>
                    <option value="2024">2024</option>
                    <option value="2025">2025</option>
                </select>
                <select id="dayFilter" onchange="javascript:FilterData(this);">
                    <option value="" selected>Select a Day</option>
                </select>
                
                <br />
                <span>Company:</span>
                <select id="companyFilter" onchange="javascript:FilterData(this);">
                    <option value="ALL">All</option>
                </select>
            </div>
            <div class="move-legend-section">
                <div>
                    <label>Color Indicates Office Number</label>
                </div>
                <div>
                    <label>385</label>
                    <span class="office385"></span>
                    <label>1302</label>
                    <span class="office1302"></span>
                    <label>1305</label>
                    <span class="office1305"></span>
                    <label>1385</label>
                    <span class="office1385"></span>
                    <label>9999</label>
                    <span class="office9999"></span>
                </div>
            </div>
         </div>
        <table id="workbook-grid" class="grid-control">
            <thead>
                <tr>
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

        <span class="edit-grid-message" style="display:block; color:#fff;"></span>
    </asp:Panel>
    <div id="edit-dialog" title="Add/Edit Workbook Job" style="display:none;">
        <div id="workbook-item-detail" style="padding:10px;">
             <table>
                 <tr>
                     <td colspan="3">
                         <span>Office Assigned:</span>
                         <select data-field-name="OfficeAssigned" id="officeAssigned" aria-data-type="text" table-name="Lytles_OfficeAssigned" search-field-name="OfficeName"></select>
                     </td>
                     <td colspan="3">
                         <span>Account:</span>
                         <select data-field-name="Account" id="account" aria-data-type="text" table-name="Lytles_Accounts" search-field-name="AccountName"></select>
                     </td>
                 </tr>
                 <tr>
                     <td><span>PPWK Complete:</span></td>
                     <td><select data-field-name="PPWK" aria-data-type="text">
                            <option value="N/A" selected>N/A</option>
                            <option value="Not Completed">Not Completed</option>
                            <option value="Completed">Completed</option>
                         </select></td>
                     <td class="separator"></td>
                     <td><span>Trailer</span></td>
                     <td><select data-field-name="Trailer" id="trailer" aria-data-type="text" table-name="Lytles_Trailers" search-field-name="TrailerNumber">
                         </select>
                     </td>
                 </tr>
                 <tr>
                     <td><span>Reg Number</span></td>
                     <td><input type="text" data-field-name="RegNumber" aria-data-type="text" /></td>
                     <td class="separator"></td>
                     <td><!--<span>Account</span>--></td>
                     <td><!--<input type="text" data-field-name="Account" aria-data-type="text" />--></td>
                 </tr>
                 <tr>
                     <td><span>Client Name</span></td>
                     <td><input type="text" data-field-name="Name" aria-data-type="text" /></td>
                     <td class="separator"></td>
                     <td><span>Move Coordinator</span></td>
                     <td><select id="moveAgentNames" data-field-name="MoveAgent" aria-data-type="text" table-name="Lytles_MoveAgents" search-field-name="MoveAgentName"></select></td>
                 </tr>
                  <tr>
                     <td style="vertical-align:top">
                         <span>Email Address</span><br />
                         <span>Pickup Location</span><br />
                         <span>Delivery Location</span>
                     </td>
                     <td style="vertical-align:top">
                         <input type="text" data-field-name="EmailAddress" aria-data-type="text"  /><br />
                         <input type="text" data-field-name="PickupLocation" aria-data-type="text" /><br />
                         <input type="text" data-field-name="DeliveryLocation" aria-data-type="text" />
                     </td>
                     <td class="separator"></td>
                     <td style="vertical-align:top;"><span>Detail</span></td>
                     <td><textarea rows="3" cols="30" data-field-name="Details" aria-data-type="text"></textarea></td>
                 </tr>
                  <tr>
                     <td><span>Weight</span></td>
                     <td><input type="text" data-field-name="Weight" aria-data-type="text" /></td>
                     <td class="separator"></td>
                     <td>Notes</td>
                     <td><textarea rows="3" cols="60" id="ContactedTransfereeNotes" data-field-name="ContactedTransfereeNotes" aria-data-type="text" style="font-size:10px;" disabled></textarea></td>
                 </tr>
                 <tr>
                     <td colspan="2">
                         <span style="width:212px;margin-top:3px;">Shipment Cancelled</span>
                         <input type="checkbox" id="CancelledShipmentCheckBox" data-field-name="Cancelled" aria-data-type="text"  search-field-name="Cancelled" />
                         <br />
                         <span style="width:212px;margin-top:3px;">Shipment Delivered</span>
                         <input type="checkbox" data-field-name="ShipmentDelivered" aria-data-type="text"  search-field-name="ShipmentDelivered" />
                         <br />
                         <span style="width:212px;margin-top:3px;">CSR Contacted Packing</span>
                         <input type="checkbox" data-field-name="CSRContactedTransfereePK" aria-data-type="text" aria-checkbox-type="PK" rel="Packing"  search-field-name="CSRContactedTransfereePK" onchange="javascript:ContactCheckBoxChanged(this);" />
                         <br />
                         <span style="width:212px;margin-top:3px;">CSR Contacted Loading</span>
                         <input type="checkbox" data-field-name="CSRContactedTransfereeLD" aria-data-type="text" aria-checkbox-type="LD" rel="Loading" search-field-name="CSRContactedTransfereeLD" onchange="javascript:ContactCheckBoxChanged(this);" />
                         <br />
                         <span style="width:212px;margin-top:3px;">CSR Contacted Delivery</span>
                         <input type="checkbox" data-field-name="CSRContactedTransfereeDEL" aria-data-type="text" aria-checkbox-type="DEL" rel="Delivery"  search-field-name="CSRContactedTransfereeDEL" onchange="javascript:ContactCheckBoxChanged(this);" />
                     </td>
                     <td class="separator"></td>
                     <td>
                         <span>Origin Driver</span><br /><br />
                         <span>Dest. Driver</span>
                     </td>
                     <td>
                         <select id="originDriverNames" data-field-name="OriginDriverNames" aria-data-type="text" table-name="Lytles_Drivers" search-field-name="OriginDriverName"></select><br /><br />
                         <select id="destinationDriverNames" data-field-name="DestinationDriverNames" aria-data-type="text" table-name="Lytles_Drivers" search-field-name="DestinationDriverName"></select>
                     </td>
                 </tr>
                  <tr>
                     <td>
                         <span>Pack Start Date</span><br />
                         <span>Pack End Date</span><br />
                         <span>Load Start Date</span><br />
                         <span>Load End Date</span><br />
                         <span>Delivery Start Date</span><br />
                         <span>Delivery End Date</span>
                     </td>
                     <td>
                         <input type="text" data-field-name="PKStartDate" aria-data-type="datetime" /><br />
                         <input type="text" data-field-name="PKEndDate" aria-data-type="datetime" /><br />
                         <input type="text" data-field-name="LDStartDate" aria-data-type="datetime" /><br />
                         <input type="text" data-field-name="LDEndDate" aria-data-type="datetime" /><br />
                         <input type="text" data-field-name="DELStartDate" aria-data-type="datetime" /><br />
                         <input type="text" data-field-name="DELEndDate" aria-data-type="datetime" />
                     </td>
                     <td class="separator"></td>
                     <td style="vertical-align:top;"><span>Origin Helper(s)</span></td>
                     <td>
                         <select id="originHelperNames" data-field-name="OriginHelperNames" size=8 multiple style="height:100%;" table-name="Lytles_Helpers" search-field-name="OriginHelperName">
                         </select>
                         <span style="display:inline-block;height:164px;vertical-align:top;">Dest. Helper(s)</span>
                         <select id="destinationHelperNames" data-field-name="DestinationHelperNames" size=8 multiple style="height:100%;" table-name="Lytles_Helpers" search-field-name="DestinationHelperName">
                         </select>
                     </td>
                 </tr>
             </table>
            <div style="width:38%; padding:5px; display:inline-block;">
                <a href="javascript:void(0);" id="btnDeleteJob" rel="0" onclick="javascript:DeleteJob(this);">Delete</a>
            </div>
             <div style="width:58%;text-align:right;padding:5px; display:inline-block;">
                  <a href="javascript:void(0);" id="btnPrintJob" rel="0" onclick="javascript:PrintJob(this);">Print</a>
                  <a href="javascript:void(0);" id="btnSaveJob" rel="0" onclick="javascript:RunQuery(this);">Submit</a>
                  <a href="javascript:void(0);" id="btnClearJob" onclick='javascript:$("#edit-dialog").dialog("close");'>Reset</a>
             </div>
        </div>
    </div>
    <span id="edit-mode"></span>
</div>

<div id="map-dialog" title="Pickup and Delivery Map" style="display:none;">
   <div id="map" style="width:600px; height:400px;"></div>
    <div id="nomap"></div>
</div>
<iframe id="frmDownloadPDF" width="0" height="0" frameborder="0" src=""></iframe>
<input type="text" id="defaultDayAnchor" style="display:none;" />

<script type="text/javascript">
    $("#btnSaveJob").button();
    $("#btnDeleteJob").button();
    $("#btnDeleteJob").css("color", "red");
    $("#btnClearJob").button();
    $("#lnkAddRecord").button();
    $("#btnPrintJob").button();

</script>