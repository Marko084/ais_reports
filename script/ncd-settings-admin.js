
var uid = $(".uid").text();
var cid = $(".cid").text();
var consolidated = $(".consolidated-id").text();
var vChart;
var oEditGrid;
var oBlogGrid;
var oSearchGrid;
var oSearchBasicGrid;

var DataQuery = new function () {
    this.FieldList = "",
    this.TableList = "",
    this.FilterList = "",
    this.OrderByList = "",
    this.Query = "",
    this.QueryType = "",
    this.QueryParamList = ""
};

$(document).ready(function () {
    //TableTools.DEFAULTS.aButtons = [{ "sExtends": "xls", "sFileName": "Export.xls", "sButtonClass": "export-button" }];

    //TableTools.DEFAULTS.sSwfPath = "../media/swf/copy_csv_xls_pdf.swf";

    GenerateXamlCharts();
    GenerateGrids();
});

//load visifire charts 

function GenerateXamlCharts() {
    try {
       // $("#site-message-section").text("");
        $(".chart-section").each(function () {
            LoadChartData($(this));
        });
    }
    catch (e) {
        alert("GenerateXamlCharts() error: " + e.Message);
    }
};

function LoadChartData(div) {

    var dataUri = BuildDataUri(div);
    //alert(dataUri);
    LoadVisifireChart(div,dataUri);
};

function BuildDataUri(div) {

    var queryName = $(div).attr("chart-query");
    var paramNames = [];
    var paramValues = [];
    var chartType = $(div).attr("chart-type-selected");
    var chartTheme = $(div).attr("chart-theme");
    var customQuery = $(div).attr("chart-query-type");
    var chartParameters = $(div).attr("chart-parameters");
    var chartSettings = $(div).attr("chart-settings");
    var chartQueryString = "";

    for (var i = 0; i < DataQuery.QueryParamList.split("|").length; i++) {
        paramNames.push(DataQuery.QueryParamList.split("|")[i].split("~")[0]);
        paramValues.push(DataQuery.QueryParamList.split("|")[i].split("~")[1].split(",")[0]);
    }

    if (chartParameters != undefined) {
        for (var i = 0; i < chartParameters.split("^").length; i++) {
            paramNames.push(chartParameters.split("^")[i].split("=")[0]);
            paramValues.push(chartParameters.split("^")[i].split("=")[1]);
        }
    }

    if (typeof chartTheme != 'undefined' && chartTheme != "") {
        chartQueryString="query_name=" + queryName + "&pname=" + paramNames.toString() + "&pvalue=" + paramValues.toString() + "&theme="+chartTheme+"&chart_type=" + chartType;
    }
    else{
        chartQueryString = "query_name=" + queryName + "&pname=" + paramNames.toString() + "&pvalue=" + paramValues.toString() + "&theme=Theme3&chart_type=" + chartType;
    }

    if (typeof chartSettings != 'undefined' && chartSettings != "") {
        chartQueryString = chartQueryString + "&" + chartSettings.replace(/\^/g, "&");
    }

    if (typeof customQuery != 'undefined') {
        if (customQuery.toLowerCase() == "custom") {
            chartQueryString = chartQueryString + "&cquery=" + queryName;
        }
    }

    return chartQueryString;
}

function BuildDrillDownUri() {

    var drillDownDataUri = [];

    for (var i = 0; i < DataQuery.QueryParamList.split("|").length; i++) {
        if (i > 0) {
            drillDownDataUri.push("&");
        }

        drillDownDataUri.push(DataQuery.QueryParamList.split("|")[i].replace("~", "="));
    }

    return drillDownDataUri.join("");
};

function SetChartType(divId, ele) {
    
    $("#" + divId).attr("chart-type-selected", $(ele).attr("title"));
    LoadChartData($("#" + divId));
};

function LoadVisifireChart(div, dataUri) {

    var width = $(div).width();
    var height = $(div).height();
    var enableDrillDown = $(div).attr("chart-drilldown");
    var backGroundColor = "Black";
    var chartTheme = $(div).attr("chart-theme");

    if (typeof chartTheme != 'undefined' && chartTheme != "") {
        if (chartTheme.toLowerCase() != "theme3") {
            backGroundColor = "White";
        }
    };
    //alert(dataUri);
    //alert(enableDrillDown);
//    if ($("#site-message-section").text() == "") {
   $("#site-message-section").append("ChartData.aspx?"+dataUri+"<br/>");
    //}
    if (dataUri.indexOf("undefined") == -1) {
        $.ajax({
            dataType: "text",
            type: "GET",
            async: true,
            url: "ChartData.aspx?" + dataUri,
            success: function (responseText) {
                try {
                    vChart = new Visifire("../xap/Visifire.xap?qid="+S4(), width, height, backGroundColor);
                    vChart.setWindowlessState(true);
                    vChart.setDataXml(responseText);
                    //alert(responseText);

                    if (typeof enableDrillDown != 'undefined') {
                        vChart.attachEvent('DataPoint', 'MouseLeftButtonDown', OnDataPointMouseLeftButtonUp);
                    }

                    if (responseText.indexOf("DataPoint") == -1) {
                        vChart.setWindowlessState(true);
                    }

                    vChart.render($(div).attr("id"));
                }
                catch (e) {
                    alert("ajax error: " + e.Message);
                }
            }
        });
    }
};

$(window).resize(function () {

    try {

        $(".chart-section").each(function () {

            var width = $(this).width();
            var height = $(this).height();

            $(this).find("object").attr("width", width);
            $(this).find("object").attr("height", height);
        });

    }
    catch (e) {
    }
});

function OnDataPointMouseLeftButtonUp(e) {
    //var str;

    //str =  "Received arguments\n";
    //str += "------------------\n";
    //str += "ControlId = " + e.ControlId + "\n";
    // str += "Event = " + e.Event + "\n";
    //str += "ChartName = " + e.ChartName + "\n";
    //str += "Element = " + e.Element + "\n";
    //str += "MouseX  = " + e.MouseX + "\n";
    //str += "MouseY = " + e.MouseY + "\n";
    //str += "DataSeriesIndex = " + e.DataSeriesIndex + "\n";
    //str += "DataSeriesName = " + e.DataSeriesName + "\n";
    //str += "Name =" + e.Name + "\n";
    //str += "AxisLabel = " + e.AxisLabel + "\n";
    //str += "XValue = " + e.XValue + "\n";
    //str += "YValue = " + e.YValue + "\n";
    //str += "ZValue = " + e.ZValue + "\n";

    var chartDiv=$("#" + e.ControlId).parent()
    var queryUri = BuildDrillDownUri();
    var drillDownPage = $(chartDiv).attr("chart-drilldown");
    var drillDownUrl = location.protocol + "//" + document.location.hostname + getCurrentPath() +drillDownPage;
    //alert(drillDownUrl + "?" + queryUri + "&kpi=" + e.AxisLabel);
    //$("#site-message-section").append(drillDownUrl + "?" + queryUri + "&kpi=" + e.AxisLabel+"<br/>");
    $.fancybox({
        'height': '100%',
        'width': '100%',
        'href': drillDownUrl + "?"+queryUri+"&kpi=" + e.AxisLabel,
        'type': 'iframe'
    });

    return true;
};

function resizeFancyBox() {

    var iFrameContentHeight = document.getElementById('fancybox-frame').contentWindow.document.body.scrollHeight;
    var iFrameContentWidth = document.getElementById('fancybox-frame').contentWindow.document.body.scrollWidth; // id of iframe 
    var outer = $('#fancybox-wrap');
    var inner = $('#fancybox-inner');
    var paddingTotal = 60;
    var extra = 20; // some extra space to avoid Scrollbars 

    if (iFrameContentHeight > 0 && iFrameContentWidth > 0) {
        outer.css({
            height: iFrameContentHeight + paddingTotal + extra,
            width: iFrameContentWidth + paddingTotal + extra
        });
        inner.css({
            height: iFrameContentHeight + extra,
            width: iFrameContentWidth + extra
        });
        $.fancybox.center();
    }
};

function getCurrentPath() {

    var index = window.location.pathname.lastIndexOf("/")+1;
    return window.location.pathname.substring(0, index);
}

function getCurrentPage() {
    var sPath = window.location.pathname;
    var sPage = sPath.substring(sPath.lastIndexOf('/') + 1);
    return sPage;
}

function GenerateGrids() {
    try {
        $(".grid-section").each(function () {
            BuildGrid($(this));
        });
    }
    catch (e) {
        alert("GenerateGrids() error: " + e.Message);
    }
}

function BuildGrid(div) {

    try {

        var table = $(div).find(".grid-control");
        var gridType = $(div).attr("grid-type");
        var detailLink="";

        DataQuery.FieldList = $(div).attr("grid-display-fields");
        DataQuery.QueryType = $(div).attr("grid-query-type");

        if (typeof $(div).attr("grid-query") != 'undefined') {
            DataQuery.Query = $(div).attr("grid-query");
        }

        //alert("../ListHandler.ashx?ct=grid&qt=" + DataQuery.QueryType + "&fld=" + DataQuery.FieldList + "&qn=" + DataQuery.Query + "&pl=" + DataQuery.QueryParamList);
        
        //$("#site-message-section").text("../ListHandler.ashx?ct=grid&qt=" + DataQuery.QueryType + "&fld=" + DataQuery.FieldList + "&qn=" + DataQuery.Query + "&pl=" + DataQuery.QueryParamList);
        var dataUri = "../ListHandler.ashx?gt="+gridType+"&ct=grid&qt=" + DataQuery.QueryType + "&fld=" + DataQuery.FieldList + "&qn=" + DataQuery.Query + "&pl=" + DataQuery.QueryParamList + "&qid=" + S4();
        //alert(dataUri);
        //alert($(table).attr("id"));
        //alert($(table).attr("id"));

//        if ($("#site-message-section").text() == "") {
          //$("#site-message-section").append(dataUri+"<br/>");
//        }
        var DTO = { 'DataQuery': DataQuery };
        var pKeyFieldName;
        
        if (gridType == "display") {

            var newTableID = $(table).attr("id") + $(div).attr("id").replace("ctl00_ContentPlaceHolder1", "");

            $(table).attr("id", newTableID);

            var oDisplayGrid = $(table).DataTable({
                "bDeferRender": true,
                "bDestroy": true,
                columnDefs: [
                    { "orderable": false, "targets": "_all" }
                ],
                displayLength: 100,
                ajax: dataUri,
                language: {
                    "emptyTable": "No data found to display."
                },
                rowCallback: function (nRow, aData, iDisplayIndex) {
                    $('td:eq(0)', nRow).attr('style', 'width:5%;');

                    if (displayGridFormat.split("^")[0] == "") {
                        $('td:eq(1)', nRow).attr('style', 'width:84%');
                        $('td:eq(2)', nRow).attr('style', 'width:10%;');
                    }
                    else if (displayGridFormat.split("^")[0] == "format") {

                        var settings = displayGridFormat.split("^")[1].split("|");

                        for (var f = 0; f < settings.length; f++) {

                            var idx = settings[f].split("~")[0];
                            var dstyle = settings[f].split("~")[1];

                            $("td:eq(" + idx + ")", nRow).attr("style", dstyle + ";");
                        }
                    }
                }
            });

            if (typeof displayGridFilter == 'undefined') {
                $(".dataTables_length").hide();
                $(".dataTables_filter").hide();
                $(".dataTables_paginate").hide();
                $(".dataTables_info").hide();
            }

            $(".dataTables_processing").hide();

        }
        else if (gridType == "map") {
            var mapDiv = $(div).find(".map-settings");
            var map;
            var centroidX = 0;
            var centroidY = 0;
            var centroidZ = 0;
            var coordCounter = 0;

            map = new GMaps({
                div: $(mapDiv).attr("id"),
                lat: '34.5132990',
                lng: '-94.162880',
                zoom: 2
            });

            map.addControl({
                position: 'top_right',
                content: 'OA Marker is Red',
                style: {
                    margin: '5px 1px 1px 1px',
                    padding: '1px 5px',
                    border: 'solid 1px #717B87',
                    background: '#f00',
                    color: '#fff'
                }
            });

            map.addControl({
                position: 'top_right',
                content: 'DA Marker is Blue',
                style: {
                    margin: '5px 1px 1px 1px',
                    padding: '1px 5px',
                    border: 'solid 1px #717B87',
                    background: '#0000ff',
                    color: '#fff'
                }
            });

            $.getJSON(dataUri, function (data) {

                var result = JSON.parse(JSON.stringify(data));
                var latCoord = 0;
                var lonCoord = 0;

                $.each(result.aaData, function (index, item) {
                    try {

                        latCoord = item[4];
                        lonCoord = item[5];

                        map.addMarker({
                            lat: latCoord,
                            lng: lonCoord,
                            icon: item[8],
                            mouseover: function (e) {
                                this.infoWindow.open(this.map, this);
                            },
                            mouseout: function (e) {
                                this.infoWindow.close();
                            },
                            infoWindow: {
                                content: item[3]
                            }
                        });

                        latCoord = latCoord * (Math.PI / 180);
                        lonCoord = lonCoord * (Math.PI / 180);

                        centroidX = centroidX + (Math.cos(latCoord) * Math.cos(lonCoord));
                        centroidY = centroidY + (Math.cos(latCoord) * Math.sin(lonCoord));
                        centroidZ = centroidZ + Math.sin(latCoord);

                        coordCounter = coordCounter + 1;
                    }
                    catch (e) {
                        alert(e.Message);
                    }
                });

                if (coordCounter > 0) {
                    centroidX = centroidX / coordCounter;
                    centroidY = centroidY / coordCounter;
                    centroidZ = centroidZ / coordCounter;

                    var newLon = Math.atan2(centroidY, centroidX);
                    var newHyp = Math.sqrt((centroidX * centroidX) + (centroidY * centroidY));
                    var newLat = Math.atan2(centroidZ, newHyp);

                    newLat = newLat * 180 / Math.PI;
                    newLon = newLon * 180 / Math.PI;

                    map.setCenter(newLat, newLon);
                    map.setZoom(5);
                }
            });
        }
        else if (gridType == "blog") {

            var colIndex;
            detailLink = $(div).attr("detail-link");
            pKeyFieldName = $(div).attr("primary-key-field")
            var tableColumns = GetAdminTableColumnNames(table);

            oBlogGrid = $(table).DataTable({
                "bDeferRender": true,
                "bDestroy": true,
                ajax: dataUri,
                lengthMenu: [[10, 20, 25, -1], [10, 20, 25, "All"]],
                pagingType: "full_numbers",
                fixedHeader: true,
                colReorder: true,
                language: {
                    "emptyTable": "No data found to display."
                },
                dom: '<"fg-toolbar ui-toolbar ui-widget-header ui-helper-clearfix ui-corner-tl ui-corner-tr"Blfr><"dataTables_scroll"t><"fg-toolbar ui-toolbar ui-widget-header ui-helper-clearfix ui-corner-bl ui-corner-br"ip>',
                buttons: [{ extend: 'excel', filename: 'GridExport', className: 'ui-corner-br fg-button ui-button ui-state-default' }],
                rowCallback: function (nRow, aData, iDisplayIndex) {

                    if (aData[1] == 0) {
                        if (typeof pKeyFieldName == 'undefined') {
                            $("td:eq(1)", nRow).html("<span class='response-count red-btn' rel='" + aData[6] + "'>" + aData[1] + "</span>");
                        }
                        else {
                            colIndex = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == pKeyFieldName.toTitleCase().toLowerCase(); });
                            $("td:eq(1)", nRow).html("<span class='response-count red-btn' rel='" + aData[colIndex[0].idx] + "'>" + aData[1] + "</span>");
                        }
                    }
                    else {
                        $("td:eq(1)", nRow).html("<span class='response-count green-btn' rel='" + aData[6] + "'>" + aData[1] + "</span>");
                    }
                    //$("td:eq(0)", nRow).html(aData[0]);
                    if (pKeyFieldName == undefined) {
                        $("td:eq(2)", nRow).html("<a class='survey-detail' title='View survey detail' href='" + detailLink + aData[6] + "&cid=" + cid + "&amp;consolidated=" + consolidated + "' onclick='javascript:DisplaySurveyDetails(this);return false;'>Detail</a>");
                        $("td:eq(3)", nRow).html("<a class='create-comment' rel='" + aData[6] + "' title='Click to add a comment' href='#' onclick='javascript:getComments(this);return false;'>Add</a>");
                        $("td:eq(4)", nRow).html("<a href='#' class='view-comments' rel='" + aData[6] + "' title='Click to view comments' onclick='javascript:getComments(this);return false;'>Comments</a>");
                    }
                    else {
                        colIndex = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == pKeyFieldName.toTitleCase().toLowerCase(); });

                        $("td:eq(2)", nRow).html("<a class='survey-detail' title='View survey detail' href='" + detailLink + aData[colIndex[0].idx] + "&cid=" + cid + "&amp;consolidated=" + consolidated + "' onclick='javascript:DisplaySurveyDetails(this);return false;'>Detail</a>");
                        $("td:eq(3)", nRow).html("<a class='create-comment' rel='" + aData[colIndex[0].idx] + "' title='Click to add a comment' href='#' onclick='javascript:getComments(this);return false;'>Add</a>");
                        $("td:eq(4)", nRow).html("<a href='#' class='view-comments' rel='" + aData[colIndex[0].idx] + "' title='Click to view comments' onclick='javascript:getComments(this);return false;'>Comments</a>");
                    }
                }
            });

        }
        else if (gridType == "search") {

            detailLink = $(div).attr("detail-link");
            pKeyFieldName = $(div).attr("primary-key-field")
            var columnToHideIdx = -1;
            var tableColumns = GetAdminTableColumnNames(table);

            oSearchGrid = $(table).DataTable({
                "bDeferRender": true,
                "bDestroy": true,
                ajax: dataUri,
                lengthMenu: [[10, 20, 25, -1], [10, 20, 25, "All"]],
                pagingType: "full_numbers",
                fixedHeader: true,
                colReorder: true,
                language: {
                    "emptyTable": "No data found to display."
                },
                dom: '<"fg-toolbar ui-toolbar ui-widget-header ui-helper-clearfix ui-corner-tl ui-corner-tr"Blfr><"dataTables_scroll"t><"fg-toolbar ui-toolbar ui-widget-header ui-helper-clearfix ui-corner-bl ui-corner-br"ip>',
                buttons: [{ extend: 'excel', filename: 'GridExport', className: 'ui-corner-br fg-button ui-button ui-state-default' }],
                rowCallback: function (nRow, aData, iDisplayIndex) {

                    if (typeof detailLink != 'undefined') {
                        if (typeof pKeyFieldName == 'undefined') {
                            $('td:eq(0)', nRow).html("<a href='" + detailLink + aData[1] + "&amp;cid=" + cid + "&amp;consolidated=" + consolidated + "' onclick='javascript: FancyboxPopup(this); return false;'>Detail</a>");
                        }
                        else {
                            var colIndex = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == pKeyFieldName.toTitleCase().toLowerCase(); });

                            if (detailLink.indexOf("cid=") > -1 && typeof colIndex != 'undefined') {
                                $('td:eq(0)', nRow).html("<a href='" + detailLink + aData[colIndex[0].idx] + "&amp;consolidated=" + consolidated + " &amp;pkfn=" + pKeyFieldName + "' onclick='javascript: FancyboxPopup(this); return false;'>Detail</a>");
                            }
                            else {
                                $('td:eq(0)', nRow).html("<a href='" + detailLink + aData[1] + "&amp;cid=" + cid + "&amp;consolidated=" + consolidated + " &amp;pkfn=" + pKeyFieldName + "' onclick='javascript: FancyboxPopup(this); return false;'>Detail</a>");
                            }
                        }

                        columnToHideIdx = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == ("LowScoreLocation").toTitleCase().toLowerCase(); });

                        if (columnToHideIdx.length > 0) {
                            $.each(aData[columnToHideIdx[0].idx].split("|"), function (idx, field) {

                                var highlightIdx = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == field.toTitleCase().toLowerCase(); });

                                if (highlightIdx.length > 0) {
                                    $("td:eq(" + highlightIdx[0].idx + ")", nRow).attr("class", "highlighted-text");
                                }
                            });
                        }

                    }
                }
            });
        }
        else if (gridType == "search-basic") {

            detailLink = $(div).attr("detail-link");

            $(table).DataTable({
                "bDeferRender": true,
                "bDestroy": true,
                ajax: dataUri,
                lengthMenu: [[10, 20, 25, -1], [10, 20, 25, "All"]],
                pagingType: "full_numbers",
                fixedHeader: true,
                colReorder: true,
                language: {
                    "emptyTable": "No data found to display."
                },
                dom: '<"fg-toolbar ui-toolbar ui-widget-header ui-helper-clearfix ui-corner-tl ui-corner-tr"Blfr><"dataTables_scroll"t><"fg-toolbar ui-toolbar ui-widget-header ui-helper-clearfix ui-corner-bl ui-corner-br"ip>',
                buttons: [{ extend: 'excel', filename: 'GridExport', className: 'ui-corner-br fg-button ui-button ui-state-default' }],
                rowCallback: function (nRow, aData, iDisplayIndex) {

                    if (typeof detailLink != 'undefined') {
                        /* Append the grade to the default row class name */
                        $('td:eq(0)', nRow).html("<a href='" + detailLink + aData[1] + "&amp;cid=" + cid + "&amp;consolidated=" + consolidated + "' onclick='javascript: FancyboxPopup(this); return false;'>Detail</a>");
                    }
                }
            });

        }
        else if (gridType == "edit") {

            detailLink = $(div).attr("detail-link");
            pKeyFieldName = $(div).attr("primary-key-field");
            var tableColumns = GetAdminTableColumnNames(table);

            oEditGrid = $(table).DataTable({
                "bDeferRender": true,
                "bDestroy": true,
                ajax: dataUri,
                lengthMenu: [[10, 20, 25, -1], [10, 20, 25, "All"]],
                pagingType: "full_numbers",
                //fixedHeader: true,
                //colReorder: true,
                language: {
                    "emptyTable": "No data found to display."
                },
                dom: '<"fg-toolbar ui-toolbar ui-widget-header ui-helper-clearfix ui-corner-tl ui-corner-tr"Blfr><"dataTables_scroll"t><"fg-toolbar ui-toolbar ui-widget-header ui-helper-clearfix ui-corner-bl ui-corner-br"ip>',
                buttons: [{ extend: 'excel', filename: 'EditGridExport', className: 'ui-corner-br fg-button ui-button ui-state-default' }],
                rowCallback: function (nRow, aData, iDisplayIndex) {
                    var colIndex = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == pKeyFieldName.toTitleCase().toLowerCase(); });
                    var companyIdx = $.grep(tableColumns, function (e) {return e.name.toLowerCase() == ("Company ID").toLowerCase(); });
                    var shipmentNbrIdx = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == ("Shipment Number").toLowerCase(); });
                    var registrationNbrIdx =$.grep(tableColumns, function (e) { return e.name.toLowerCase() == ("Registration Number").toLowerCase(); });

                    if (shipmentNbrIdx.length==0) {
                        shipmentNbrIdx = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == ("Case Number").toLowerCase(); });
                    }

                    var shipmentNbr = "0";

                    //console.log("Shipment IDX: " + shipmentNbrIdx);
                    //console.log("Reg IDX: " + registrationNbrIdx)
                    if (shipmentNbrIdx.length > 0) {
                        shipmentNbr = aData[shipmentNbrIdx[0].idx];
                    }
                    else if(registrationNbrIdx.length>0) {
                        shipmentNbr = aData[registrationNbrIdx[0].idx];
                    }

                    var shipmentId = aData[colIndex[0].idx];
                    var cmpnyID = aData[companyIdx[0].idx];

                    $('td:eq(0)', nRow).html("<a href='#' rel='" + aData[colIndex[0].idx] + "' onclick='javascript:RunQuery(this);ShowEditDialog(this); return false;'>Edit</a>&nbsp;<a href='#' title='Edit Survey Results' onclick=\"javascript:EditSurveyResults('" + shipmentId + "','" + shipmentNbr + "','" + cmpnyID + "'); return false;\">Survey</a>&nbsp;<a href='#' rel='" + aData[colIndex[0].idx] + "' onclick='javascript: RunQuery(this); return false;'>Delete</a>");
                    //if (colIndex.length > 0) {
                    //    $('td:eq(0)', nRow).html("<a href='#' rel='" + aData[colIndex[0].idx] + "' onclick='javascript:RunQuery(this);ShowEditDialog(this); return false;'>Edit</a>&nbsp;<a href='#' rel='" + aData[colIndex[0].idx] + "' onclick='javascript: RunQuery(this); return false;'>Delete</a>");
                    //}
                }
            });

            var columnToHideIdx = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == ("CompanyID").toTitleCase().toLowerCase(); });

            if (columnToHideIdx.length > 0) {
                oEditGrid.column(columnToHideIdx[0].idx).visible(false);
            }

            columnToHideIdx = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == ("BatchID").toTitleCase().toLowerCase(); });

            if (columnToHideIdx.length > 0) {
                oEditGrid.column(columnToHideIdx[0].idx).visible(false);
            }

            columnToHideIdx = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == ("pkAgentDetailID").toTitleCase().toLowerCase(); });

            if (columnToHideIdx.length > 0) {
                oEditGrid.column(columnToHideIdx[0].idx).visible(false);
            }

            columnToHideIdx = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == ("ImportID").toTitleCase().toLowerCase(); });

            if (columnToHideIdx.length > 0) {
                oEditGrid.column(columnToHideIdx[0].idx).visible(false);
            }

            columnToHideIdx = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == ("DriverId").toTitleCase().toLowerCase(); });

            if (columnToHideIdx.length > 0) {
                oEditGrid.column(columnToHideIdx[0].idx).visible(false);
            }

            var columnToHideIdx = $.grep(tableColumns, function (e) { return e.name.toLowerCase() == ("ShipmentID").toTitleCase().toLowerCase(); });

            if (columnToHideIdx.length > 0) {
                oEditGrid.column(columnToHideIdx[0].idx).visible(false);
            }
        }
    }
    catch (e) {
        alert("BuildGrid() Error: " + e.Description);
    }
}

function FancyboxPopup(ele) {
    $.fancybox({
        'height': '70%',
        'width': '60%',
        'href': ele.href,
        'type': 'iframe'
    });
}

function S4() {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16);
}
