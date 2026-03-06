
var uid = $(".uid").text();
var cid = $(".cid").text();
var utype = $(".utype").text();
var consolidated = $(".consolidated-id").text();
var vChart;
var oEditGrid;
var oClaimsGrid;
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
    TableTools.DEFAULTS.aButtons = [{ "sExtends": "xls", "sFileName": "Export.xls", "sButtonClass": "export-button" }];

    TableTools.DEFAULTS.sSwfPath = "../media/swf/copy_csv_xls_pdf.swf";

    GenerateFusionCharts();
    //GenerateXamlCharts();
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

function GenerateFusionCharts() {
    try {
        // $("#site-message-section").text("");
        $(".fusion-chart-section").each(function () {
            LoadFusionChartData($(this));
        });
    }
    catch (e) {
        alert("GenerateFusionCharts() error: " + e.Message);
    }
};

function LoadFusionChartData(div) {

    var dataUri = BuildDataUri(div);
    LoadFusionChart(div, dataUri);
};

function LoadChartData(div) {

    var dataUri = BuildDataUri(div);
    //alert(dataUri);

    if ($(div).hasClass("zingchart")) {
        LoadZingChart(div, dataUri);
    }
    else {
        LoadVisifireChart(div, dataUri);
    }
};

function BuildDataUri(div) {

    var queryName = $(div).attr("chart-query");
    var paramNames = [];
    var paramValues = [];
    var chartType = $(div).attr("chart-type-selected");
    var chartTheme = $(div).attr("chart-theme"); //$(".site-chart-style").text(); 
    var customQuery = $(div).attr("chart-query-type");
    var chartParameters = $(div).attr("chart-parameters");
    var chartSettings = $(div).attr("chart-settings");
    var chartQueryString = "";

    if ($(div).hasClass("fusion-chart-section")) {
        chartTheme = $(".site-chart-style").text();
    }

    if ($(div).hasClass("zingchart")) {
        if (chartType.toLowerCase() == "column") {
            chartType = "vbar";
        }
        else if (chartType.toLowerCase() == "bar") {
            chartType = "hbar";
        }
    }

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

    //alert(paramNames.toString());
    //alert(paramValues.toString());

    if (typeof chartTheme != 'undefined' && chartTheme != "") {
        chartQueryString = "query_name=" + queryName + "&pname=" + paramNames.toString() + "&pvalue=" + paramValues.toString() + "&theme=" + chartTheme + "&chart_type=" + chartType;
    }
    else {
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

function BuildDrillDownUri(div) {


    //alert(div);
    var drillDownDataUri = [];
    var chartParameters = $(div).attr("chart-parameters");
    var queryString = "";

    try {
        if (chartParameters != undefined) {
            for (var i = 0; i < chartParameters.split("^").length; i++) {
                queryString = queryString + "&" + chartParameters.split("^")[i];
            }
        }

        for (var i = 0; i < DataQuery.QueryParamList.split("|").length; i++) {
            if (i > 0) {
                drillDownDataUri.push("&");
            }

            drillDownDataUri.push(DataQuery.QueryParamList.split("|")[i].replace("~", "="));
        }
    }
    catch (e) {
        // alert(e.Message);
    }

    return drillDownDataUri.join("") + queryString;
};

function SetChartType(divId, ele) {

    $("#" + divId).attr("chart-type-selected", $(ele).attr("title"));
    LoadChartData($("#" + divId));
};

function SetFusionChartType(divID, ele) {

    $("#"+divID).attr("chart-type-selected", $(ele).attr("title"));
    LoadFusionChartData($("#" + divID));
};

function LoadZingChart(div, dataUri) {

    var queryUri = "../aisrest.svc/getzingchartdata?" + dataUri;
    //console.log(queryUri);
    $.getJSON(queryUri, function (data) {
       // console.log(data);

        $(div).zingchart({
            data: data
        })

        if (data.errormessage !== "") {
            $(".zingchart-error-message").text(data.errormessage);
            $(".zingchart-error-message").fadeIn();
        }
        else {
            $(".zingchart-error-message").hide();
        }
    });
}

function LoadFusionChart(div, dataUri) {
    alert("test!!");
    var width = $(div).width();
    var height = $(div).height();
    var enableDrillDown = $(div).attr("chart-drilldown");
    //var backGroundColor = "Black";
    //var chartTheme = "fint";
    var chartIs3D = false;

    //if ($(div).is("[chart-theme]")) {
    //    chartTheme = $(div).attr("chart-theme")
    //}

    //if ($(div).is("[chart-settings]")) {
    if ($(".site-chart-style").text()=="ais"){
            chartIs3D = true;
    }
    //}
    var chartType = GetQueryStringValueByName("chart_type",dataUri).toLowerCase();

    chartType = GetFusionChartTypes(chartType,chartIs3D);
    //dataUri = dataUri.replace(/Theme3/ig, "fint");

    if (dataUri.indexOf("undefined") == -1) {
        $.ajax({
            dataType: "text",
            type: "GET",
            async: true,
            url: "../FusionChartXmlHandler.ashx?" + dataUri,
            error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); },
            success: function (data) {
                try {

                    var chartXmlDoc = $.parseXML(data);
                    var $xml = $(chartXmlDoc);
  
                    var multiDataSet = "";
                    
                    if ($xml.find("dataset").length > 1 && chartType != "doughnut" && chartType != "pie" && chartType != "bubble" && chartType != "area2d") {
                        chartType = "mscombi2d";
                        alert("combo chart!!");
                    }
                    alert(chartType);
                    $(div).insertFusionCharts({
                        "type": chartType,
                        "width": "100%",
                        "height": "100%",
                        "dataFormat": "xml",
                        "dataSource": data
                    });

                    if (typeof enableDrillDown != 'undefined') {
                        $(div).bind('fusionchartsdataplotclick', function (event, args) {

                            var queryUri = BuildDrillDownUri($(this));
                            var drillDownPage = $(this).attr("chart-drilldown");
                            var drillDownUrl = location.protocol + "//" + document.location.hostname + getCurrentPath() + drillDownPage;

                            $.fancybox({
                                'height': '100%',
                                'width': '100%',
                                'href': drillDownUrl + "?" + queryUri + "&kpi=" + args.toolText.split(',')[0],
                                'type': 'iframe'
                            });
                        });
                    }
                }
                catch (e) {
                    alert("ajax error: " + e.Message);
                }
            }
        });
    }
};

function GetQueryStringValueByName(name,dataUri) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(dataUri);
    if (results == null)
        return "";
    else
        return decodeURIComponent(results[1].replace(/\+/g, " "));
}

function GetFusionChartTypes(cType,is3D) {
    var  result = (is3D ? "bar3d" :"bar2d");

    if (cType.toLowerCase() == "column") {
        return (is3D ? "column3d" : "column2d");
    }
    else if (cType.toLowerCase() == "pie") {
        return (is3D ? "pie3d" : "pie2d");
    }
    else if (cType.toLowerCase() == "doughnut") {
        return (is3D ? "doughnut3d" : "doughnut2d");
    }
    else if (cType.toLowerCase() == "line") {
        return "line";
    }
    else if (cType.toLowerCase() == "area") {
        return "area2d"; //(is3D ? "area3d" : "area2d");
    }
    else if (cType.toLowerCase() == "bubble") {
        return "radar";
    }

    return result;
}

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
    //$("#site-message-section").append("ChartData.aspx?"+dataUri+"<br/>");
    //}
    if (dataUri.indexOf("undefined") == -1) {
        $.ajax({
            dataType: "text",
            type: "GET",
            async: true,
            url: "ChartData.aspx?" + dataUri,
            success: function (responseText) {
                try {
                    vChart = new Visifire("../xap/Visifire.xap?qid=" + S4(), width, height, backGroundColor);
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

    var chartDiv = $("#" + e.ControlId).parent()
    var queryUri = BuildDrillDownUri(chartDiv);
    var drillDownPage = $(chartDiv).attr("chart-drilldown");
    var drillDownUrl = location.protocol + "//" + document.location.hostname + getCurrentPath() + drillDownPage;
    //alert(drillDownUrl + "?" + queryUri + "&kpi=" + e.AxisLabel);
    //$("#site-message-section").append(drillDownUrl + "?" + queryUri + "&kpi=" + e.AxisLabel+"<br/>");
    $.fancybox({
        'height': '100%',
        'width': '100%',
        'href': drillDownUrl + "?" + queryUri + "&kpi=" + e.AxisLabel,
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

    var index = window.location.pathname.lastIndexOf("/") + 1;
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
        var detailLink = "";
        var displayGridFormat = "";
        var dataUri = "";
        var chartParameters = $(div).attr("chart-parameters");
        var displayGridFilter = $(div).attr("display-grid-filter");

        DataQuery.FieldList = $(div).attr("grid-display-fields");
        DataQuery.QueryType = $(div).attr("grid-query-type");

        if (typeof $(div).attr("grid-query") != 'undefined') {
            DataQuery.Query = $(div).attr("grid-query");
        }

        if (typeof $(div).attr("display-grid-format") != 'undefined') {
            displayGridFormat = $(div).attr("display-grid-format");
        }

        if (chartParameters != undefined) {
            chartParameters = "|" + chartParameters.replace(/=/g, "~");
        }
        else {
            chartParameters = "";
        }

        dataUri = "../ListHandler.ashx?gt=" + gridType + "&ct=grid&qt=" + DataQuery.QueryType + "&fld=" + DataQuery.FieldList + "&qn=" + DataQuery.Query + "&pl=" + DataQuery.QueryParamList + chartParameters + "&qid=" + S4();
        //console.log("dataUri: " + dataUri);
        //        if ($("#site-message-section").text() == "") {
        //$("#site-message-section").append(dataUri+"<br/>");
        //        }
        var DTO = { 'DataQuery': DataQuery };
        //console.log(DataQuery);
        var pKeyFieldName;

        if (gridType == "display") {
            //alert($(div).attr("id"));
            var newTableID = $(table).attr("id") + $(div).attr("id").replace("ctl00_ContentPlaceHolder1", "");
            //alert(newTableID);
            $(table).attr("id", newTableID);
            var oDisplayGrid = $(table).dataTable({
                "bDeferRender": true,
                "bJQueryUI": true,
                "bDestroy": true,
                "bFilter": true,
                "bInfo": true,
                "sPaginationType": "full_numbers",
                "iDisplayLength": 100,
                "sAjaxSource": dataUri,
                "oLanguage": { "sZeroRecords": "No data found to display." },
                "sDom": '<"H"Tlfr><"datatable-scroll"t><"F"ip>',
                "oTableTools": {
                    "aButtons": [{
                        "sExtends": "xls",
                        "sFileName": "ExportFile.xls"
                    }],
                    "sSwfPath": "../media/swf/copy_csv_xls_pdf.swf"
                },
                "fnRowCallback": function (nRow, aData, iDisplayIndex) {
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
                //console.log("displayGridFilter2: " + displayGridFilter);
                $(".dataTables_length").hide();
                $(".dataTables_filter").hide();
                $(".dataTables_paginate").hide();
                $(".dataTables_info").hide();
            }

            //$(".dataTables_length").hide();
            //$(".dataTables_filter").hide();
            $(".dataTables_processing").hide();
            // $(".dataTables_paginate").hide();
            $(".DataTables_sort_icon").hide();

            if (typeof ($(div).attr("show-export-button")) == 'undefined') {
                $(".DTTT_container").hide();
            }
            else {
                $(".DTTT_container").show();
            }

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
            //alert(pKeyFieldName.toTitleCase());
            oBlogGrid = $(table).dataTable({
                "aLengthMenu": [[10, 20, 25, -1], [10, 20, 25, "All"]],
                "bJQueryUI": true,
                "sAjaxSource": dataUri,
                "sPaginationType": "full_numbers",
                "sDom": '<"H"lfr><"datatable-scroll"t><"F"ip>',
                "oLanguage": { "sZeroRecords": "No data found to display." },
                "fnRowCallback": function (nRow, aData, iDisplayIndex) {

                    if (aData[1] == 0) {
                        if (pKeyFieldName == undefined) {
                            $("td:eq(1)", nRow).html("<span class='response-count red-btn' rel='" + aData[6] + "'>" + aData[1] + "</span>");
                        }
                        else {
                            colIndex = oBlogGrid.fnGetColumnIndex(pKeyFieldName.toTitleCase());
                            $("td:eq(1)", nRow).html("<span class='response-count red-btn' rel='" + aData[colIndex] + "'>" + aData[1] + "</span>");
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
                        colIndex = oBlogGrid.fnGetColumnIndex(pKeyFieldName.toTitleCase());

                        $("td:eq(2)", nRow).html("<a class='survey-detail' title='View survey detail' href='" + detailLink + aData[colIndex] + "&cid=" + cid + "&amp;consolidated=" + consolidated + "' onclick='javascript:DisplaySurveyDetails(this);return false;'>Detail</a>");
                        $("td:eq(3)", nRow).html("<a class='create-comment' rel='" + aData[colIndex] + "' title='Click to add a comment' href='#' onclick='javascript:getComments(this);return false;'>Add</a>");
                        $("td:eq(4)", nRow).html("<a href='#' class='view-comments' rel='" + aData[colIndex] + "' title='Click to view comments' onclick='javascript:getComments(this);return false;'>Comments</a>");
                    }
                }
            });

        }
        else if (gridType == "search") {

            detailLink = $(div).attr("detail-link");
            pKeyFieldName = $(div).attr("primary-key-field")
            var columnToHideIdx = -1;
            //Rlfrtip 'r<"H"Rlf><"datatable-scroll"t><"F"ip>'
            oSearchGrid = $(table).dataTable({
                "bDestroy": true,
                "sAjaxSource": dataUri,
                "aLengthMenu": [[10, 20, 25, -1], [10, 20, 25, "All"]],
                "sPaginationType": "full_numbers",
                "bDeferRender": true,
                "bJQueryUI": true,
                "oLanguage": { "sZeroRecords": "No data found to display." },
                "sDom": '<"H"Tlfr><"datatable-scroll"t><"F"ip>',
                "oTableTools": {
                    "aButtons": [{
                        "sExtends": "xls",
                        "sFileName": "SurveyResults.xls"
                    }],
                    "sSwfPath": "../media/swf/copy_csv_xls_pdf.swf"
                },
                "fnRowCallback": function (nRow, aData, iDisplayIndex) {

                    if (typeof detailLink != 'undefined') {
                        /* Append the grade to the default row class name */


                        if (typeof pKeyFieldName == 'undefined') {
                            $('td:eq(0)', nRow).html("<a href='" + detailLink + aData[1] + "&amp;cid=" + cid + "&amp;consolidated=" + consolidated + "' onclick='javascript: FancyboxPopup(this); return false;'>Detail</a>");
                        }
                        else {
                            var colIndex = oSearchGrid.fnGetColumnIndex(pKeyFieldName.toTitleCase());

                            if (detailLink.indexOf("cid=") > -1) {
                                $('td:eq(0)', nRow).html("<a href='" + detailLink + aData[colIndex] + "&amp;consolidated=" + consolidated + " &amp;pkfn=" + pKeyFieldName + "' onclick='javascript: FancyboxPopup(this); return false;'>Detail</a>");
                            }
                            else {
                                $('td:eq(0)', nRow).html("<a href='" + detailLink + aData[colIndex] + "&amp;cid=" + cid + "&amp;consolidated=" + consolidated + " &amp;pkfn=" + pKeyFieldName + "' onclick='javascript: FancyboxPopup(this); return false;'>Detail</a>");
                            }
                        }

                        columnToHideIdx = oSearchGrid.fnGetColumnIndex(("LowScoreLocation").toTitleCase());

                        if (columnToHideIdx > -1) {
                            $.each(aData[columnToHideIdx].split("|"), function (idx, field) {

                                var highlightIdx = oSearchGrid.fnGetColumnIndex(field.toTitleCase());

                                if (highlightIdx > -1) {
                                    $("td:eq(" + highlightIdx + ")", nRow).attr("class", "highlighted-text");
                                }
                            });
                        }

                    }
                }
            });

            columnToHideIdx = oSearchGrid.fnGetColumnIndex(("LowScoreLocation").toTitleCase());

            if (columnToHideIdx > -1) {
                oSearchGrid.fnSetColumnVis(columnToHideIdx, false);
            }
            
            //try {
            //    new $.fn.dataTable.ColReorder(oSearchGrid);
            //}
            //catch (e) {
            //    alert(e.message);
            //}

        }
        else if (gridType == "search-basic") {

            detailLink = $(div).attr("detail-link");

            $(table).dataTable({
                "bDestroy": true,
                "sAjaxSource": dataUri,
                "aLengthMenu": [[10, 20, 25, -1], [10, 20, 25, "All"]],
                "sPaginationType": "full_numbers",
                "bDeferRender": true,
                "bJQueryUI": true,
                "oLanguage": { "sZeroRecords": "No data found to display." },
                "sDom": '<"H"Tlfr><"datatable-scroll"t><"F"ip>',
                "oTableTools": {
                    "aButtons": [{
                        "sExtends": "xls",
                        "sFileName": "SurveyResults.xls"
                    }],
                    "sSwfPath": "../media/swf/copy_csv_xls_pdf.swf"
                },
                "fnRowCallback": function (nRow, aData, iDisplayIndex) {

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

            //$("#site-message-section").append(dataUri);
            //"sDom": '<"H"Tfr>t<"F"ip>',
            oEditGrid = $(table).dataTable({
                "bDestroy": true,
                "sAjaxSource": dataUri,
                "aLengthMenu": [[10, 20, 25, -1], [10, 20, 25, "All"]],
                "sPaginationType": "full_numbers",
                "bScrollCollapse": true,
                "bDeferRender": true,
                "bJQueryUI": true,
                "oLanguage": { "sZeroRecords": "No data found to display." },
                "sDom": '<"H"Tlfr><"datatable-scroll"t><"F"ip>', //'r<"H"lf><"datatable-scroll"t><"F"ip>',
                "oTableTools": {
                    "aButtons": [{
                        "sExtends": "xls",
                        "sFileName": "Export.xls"
                    }],
                    "sSwfPath": "../media/swf/copy_csv_xls_pdf.swf"
                },
                "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                    var colIndex = oEditGrid.fnGetColumnIndex(pKeyFieldName.toTitleCase());
                    $('td:eq(0)', nRow).html("<a href='#' rel='" + aData[colIndex] + "' onclick='javascript:RunQuery(this);ShowEditDialog(this); return false;'>Edit</a>&nbsp;<a href='#' rel='" + aData[colIndex] + "' onclick='javascript: RunQuery(this); return false;'>Delete</a>");
                }
            });

            var columnToHideIdx = oEditGrid.fnGetColumnIndex(("CompanyID").toTitleCase());

            if (columnToHideIdx > -1) {
                oEditGrid.fnSetColumnVis(columnToHideIdx, false);
            }

            columnToHideIdx = oEditGrid.fnGetColumnIndex(("BatchID").toTitleCase());

            if (columnToHideIdx > -1) {
                oEditGrid.fnSetColumnVis(columnToHideIdx, false);
            }

            columnToHideIdx = oEditGrid.fnGetColumnIndex(("pkAgentDetailID").toTitleCase());

            if (columnToHideIdx > -1) {
                oEditGrid.fnSetColumnVis(columnToHideIdx, false);
            }

            columnToHideIdx = oEditGrid.fnGetColumnIndex(("ImportID").toTitleCase());

            if (columnToHideIdx > -1) {
                oEditGrid.fnSetColumnVis(columnToHideIdx, false);
            }

            columnToHideIdx = oEditGrid.fnGetColumnIndex(("DriverId").toTitleCase());

            if (columnToHideIdx > -1) {
                oEditGrid.fnSetColumnVis(columnToHideIdx, false);
            }

            //if (typeof ($(div).attr("show-export-button")) == 'undefined') {
            //    $(".DTTT_container").hide();
           // }
            //else {
               // $(".DTTT_container").show();
            //}

        }
        else if (gridType == "claims")
        {

            //$("#site-message-section").text(dataUri);

            var claimsApprovalModule = false;
            var gmApprovalModule = false;
            var gridReadOnly = $(".slide-out-div #ctl00_ContentPlaceHolder1_SearchFilter1_txtgridreadonly").val();
            var documentsLink = "";
            var sendEmailID = "";
            var dashboardMode;
            var nationalAccount;

            if (typeof gridReadOnly == 'undefined') {
                gridReadOnly = "false";
            }

            detailLink = $(div).attr("detail-link");
            pKeyFieldName = $(div).attr("primary-key-field");
            documentsLink = $(div).attr("document-site-url");
            dashboardMode = $(div).attr("dashboard-mode");

            if (typeof $(div).attr("show-email-link") != 'undefined') {
                sendEmailID = $(div).attr("send-to-field");
            }

            if (typeof $(div).attr("adjustor-approval-module") != 'undefined') {
                if ($(div).attr("adjustor-approval-module") == "true") {
                    claimsApprovalModule = true;
                    DataQuery.QueryParamList = DataQuery.QueryParamList + "|UseAdjustorClaimsApprovalModule~1";
                }
            }

            if (typeof $(div).attr("gm-approval-module") != 'undefined') {
                if ($(div).attr("gm-approval-module") == "true") {
                    gmApprovalModule = true;
                    DataQuery.QueryParamList = DataQuery.QueryParamList + "|UseGMClaimsApprovalModule~1";
                }
            }

            if (typeof detailLink != 'undefined') {
                DataQuery.QueryParamList = DataQuery.QueryParamList + "|ShowDetailTF~1";
            }

            dataUri = dataUri.split("&pl=")[0] + "&pl=" + DataQuery.QueryParamList + "&qid=" + S4();

            var checkRequestLinkFieldName = "";

            if (typeof $(div).attr("checkrequest-key-field") != 'undefined') {
                checkRequestLinkFieldName = $(div).attr("checkrequest-key-field");
            }

            // $("#site-message-section").text(dataUri);
            //"sDom": '<"H"Tfr>t<"F"ip>',
            oClaimsGrid = $(table).dataTable({
                "bDestroy": true,
                "stateSave": true,
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

                    var colIndex = oClaimsGrid.fnGetColumnIndex(pKeyFieldName.toTitleCase());
                    var claimsStatusIdx = oClaimsGrid.fnGetColumnIndex(("ClaimStatus").toTitleCase());
                    var checkRequestLinkFieldIdx = -1;
                    var surveyDetailLinkHtml = "";
                    var surveyDetailIdx = oClaimsGrid.fnGetColumnIndex(("SurveyTF").toTitleCase());
                    var surveyDetailExistsTF = false;
                    var newCommentIdx = oClaimsGrid.fnGetColumnIndex("New CR Comment");
                    var newCRIdx = oClaimsGrid.fnGetColumnIndex("New CR Created");
                    var claimsDocumentsLinkButton = "";
                    var sendEmailLink = "";
                    var gmApprovalColumnIdx = oClaimsGrid.fnGetColumnIndex("GM Approved");
                    var firstNameIdx = oClaimsGrid.fnGetColumnIndex(("FirstName").toTitleCase());
                    var lastNameIdx = oClaimsGrid.fnGetColumnIndex(("LastName").toTitleCase());
                    var shipperName = "";
                    var nationalAcctIdx = oClaimsGrid.fnGetColumnIndex(("NationalAccount").toTitleCase());
                    
                    if (firstNameIdx > -1 && lastNameIdx > -1) {
                       shipperName= aData[firstNameIdx] + " " + aData[lastNameIdx];
                    }
                    else if (firstNameIdx > -1) {
                        shipperName = aData[firstNameIdx];
                    }
                    else if (lastNameIdx > -1) {
                        shipperName = aData[lastNameIdx];
                    }

                    if (checkRequestLinkFieldName != "") {
                        checkRequestLinkFieldIdx = oClaimsGrid.fnGetColumnIndex(checkRequestLinkFieldName.toTitleCase());
                    }

                    if (sendEmailID != "") {
                        var driverCommentIdx = oClaimsGrid.fnGetColumnIndex(("NewDriverComment").toTitleCase());

                        if (aData[driverCommentIdx] == "YES") {
                            $("td:eq(" + driverCommentIdx + ")", nRow).html("<span class='yellow-exclamation-mark' title='New Driver Comment created.'>YES</span>")
                        }
                        else {
                            $("td:eq(" + driverCommentIdx + ")", nRow).html("<span style='color:transparent;'>NO</span>")
                        }
                        //sendEmailLink = "&nbsp;<a href='javascript:void(0)' aria-claim-id='" + aData[colIndex] + "' rel='" + aData[sendEmailIdx] + "' onclick=javascript:ShowEmailDialog(this);>Driver</a>";

                    }

                    if (typeof documentsLink != 'undefined' && documentsLink == 'true')
                    {
                        var regNoIdx = oClaimsGrid.fnGetColumnIndex("Claim Number");
                        
                        claimsDocumentsLinkButton = "&nbsp;<a href='#' rel='" + aData[regNoIdx] + "' onclick='javascript:ShowFilteredDocumentLibrary(this); return false;'>Docs</a>"+
                                                    "&nbsp;<a href='#' rel='" + aData[regNoIdx] + "' onclick='javascript:ShowCheckDialog(this); return false;'>Checks</a>";
                    }
                    else if (documentsLink=="standard") {
                        var regNoIdx = oClaimsGrid.fnGetColumnIndex(("RegistrationNumber").toTitleCase());
                        claimsDocumentsLinkButton = "&nbsp;<a href='#' rel='" + aData[regNoIdx] + "' onclick='javascript:ShowFilteredDocumentLibrary(this); return false;'>Docs</a>";
                    }
                    else if (typeof documentsLink != 'undefined') {
                        try {
                            var regNoIdx = oClaimsGrid.fnGetColumnIndex(("RegistrationNumber").toTitleCase());
                            var lastNameIdx = oClaimsGrid.fnGetColumnIndex(("LastName").toTitleCase());

                            claimsDocumentsLinkButton = "&nbsp;<a href='" + documentsLink + "/" + aData[regNoIdx] + "-" + aData[lastNameIdx] + "' onclick='javascript:ShowClaimsDocuments(this); return false;'>Docs</a>";
                        }
                        catch (e)
                        { }
                    }

                    if (surveyDetailIdx > -1) {

                        if (aData[surveyDetailIdx] == "YES") {
                            surveyDetailExistsTF = true;
                        }
                        else {
                            surveyDetailExistsTF = false;
                        }
                    }

                    if (typeof detailLink != 'undefined' && surveyDetailExistsTF) {
                        if (checkRequestLinkFieldName != "") {
                            surveyDetailLinkHtml = "&nbsp;<a style='color:black;font-weight:bold;' href='" + detailLink + aData[checkRequestLinkFieldIdx] + "&amp;cid=" + cid + "&amp;consolidated=1' onclick='javascript: FancyboxPopup(this); return false;'>Survey</a>&nbsp;";
                        }
                        else {
                            surveyDetailLinkHtml = "&nbsp;<a style='color:black;font-weight:bold;' href='" + detailLink + aData[colIndex] + "&amp;cid=" + cid + "&amp;consolidated=1' onclick='javascript: FancyboxPopup(this); return false;'>Survey</a>&nbsp;";
                        }
                    }

                    if (newCRIdx > -1) {
                        if (aData[newCRIdx] == "YES") {
                            $('td:eq(3)', nRow).html("<span class='blue-exclamation-mark' title='New Check Request created.'>YES</span>")
                        }
                        else {
                            $('td:eq(3)', nRow).html("<span style='color:transparent;'>NO</span>")
                        }
                    }

                    if (claimsApprovalModule == true) {

                        //aria-shippername='"+shipperName+" rel='"

                        $('td:eq(0)', nRow).html("<a href='#' rel='" + aData[colIndex] + "' aria-natl-acct='" + aData[nationalAcctIdx] + "' aria-checkrequest-id='" + checkRequestLinkFieldName + "~" + aData[checkRequestLinkFieldIdx] + "' onclick='javascript:RunQuery(this);ShowEditDialog(this); return false;'>Edit</a>&nbsp;<a href='#' rel='" + aData[colIndex] + "' onclick='javascript: RunQuery(this); return false;'>Delete</a>&nbsp;<a href='#' rel='" + aData[checkRequestLinkFieldIdx] + "' aria-natl-acct='" + aData[nationalAcctIdx] +"' aria-cr-shippername='" + shipperName + "' aria-claim-id='" + aData[colIndex] + "' aria-checkrequest-id='" + checkRequestLinkFieldName + "~" + aData[checkRequestLinkFieldIdx] + "' onclick='javascript: ViewCheckRequests(this); SetCheckRequestCommentsAsRead(this);return false;'>Check Request</a>" + claimsDocumentsLinkButton + surveyDetailLinkHtml);

                        if (aData[newCommentIdx] == "YES") {
                            $('td:eq(2)', nRow).html("<span class='red-exclamation-mark' title='New GM comment added.'>YES</span>")
                        }
                        else {
                            $('td:eq(2)', nRow).html("<span style='color:transparent;'>NO</span>")
                        }

                    }
                    else if (gmApprovalModule == true) {
                        $('td:eq(0)', nRow).html("<a href='#' rel='" + aData[colIndex] + "' aria-natl-acct='" + aData[nationalAcctIdx] + "' aria-checkrequest-id='" + checkRequestLinkFieldName + "~" + aData[checkRequestLinkFieldIdx] + "' onclick='javascript:RunQuery(this);ShowEditDialog(this); return false;'>View</a>&nbsp;<a href='#' rel='" + aData[checkRequestLinkFieldIdx] + "' aria-claim-id='" + aData[colIndex] + "' aria-natl-acct='" + aData[nationalAcctIdx] +"' aria-checkrequest-id='" + checkRequestLinkFieldName + "~" + aData[checkRequestLinkFieldIdx] + "' onclick='javascript: ViewCheckRequests(this); SetCheckRequestCommentsAsRead(this); return false;'>Check Request</a>" + claimsDocumentsLinkButton + surveyDetailLinkHtml);

                        if (aData[newCommentIdx] == "YES") {
                            $('td:eq(2)', nRow).html("<span class='red-exclamation-mark' title='New Adjustor comment added.'>YES</span>")
                        }
                        else {
                            $('td:eq(2)', nRow).html("<span style='color:transparent;'>NO</span>")
                        }
                    }
                    else if (dashboardMode == "true") {
                        $('td:eq(0)', nRow).html("<a href='#' rel='" + aData[colIndex] + "' onclick='javascript:RunQuery(this);ShowEditDialog(this); return false;'>Edit</a>" + claimsDocumentsLinkButton);
                    }
                    else if (documentsLink == "true") {
                        $('td:eq(0)', nRow).html("<a href='#' rel='" + aData[colIndex] + "' onclick='javascript:RunQuery(this);ShowEditDialog(this); return false;'>Edit</a>&nbsp;<a href='#' rel='" + aData[colIndex] + "' onclick='javascript: RunQuery(this); return false;'>Delete</a>" + claimsDocumentsLinkButton);
                    }
                    else if (documentsLink == "standard") {
                        $('td:eq(0)', nRow).html("<a href='#' rel='" + aData[colIndex] + "' onclick='javascript:RunQuery(this);ShowEditDialog(this); return false;'>Edit</a>&nbsp;<a href='#' rel='" + aData[colIndex] + "' onclick='javascript: RunQuery(this); return false;'>Delete</a>" + claimsDocumentsLinkButton);
                    }
                    else if (gridReadOnly == "false") {
                        $('td:eq(0)', nRow).html("<a href='#' rel='" + aData[colIndex] + "' onclick='javascript:RunQuery(this);ShowEditDialog(this); return false;'>Edit</a>&nbsp;<a href='#' rel='" + aData[colIndex] + "' onclick='javascript: RunQuery(this); return false;'>Delete</a>" + surveyDetailLinkHtml);
                    }
                    else if (gridReadOnly == "true") {
                        $('td:eq(0)', nRow).html("<a href='#' rel='" + aData[colIndex] + "' onclick='javascript:RunQuery(this);ShowEditDialog(this); return false;'>View</a>");
                        $("#claims-comments > div > div > a#add-comment").parent().hide();
                        $(".add-record").hide();
                        $("#Comments").attr("disabled", "disable");
                    }

                    if (gmApprovalModule == true) {

                        if (aData[gmApprovalColumnIdx].indexOf("NO") > -1) {
                            $("td:eq(1)", nRow).html("<span class='green-btn' rel='" + aData[claimsStatusIdx] + "'>OPEN</span>");
                        }
                        else {
                            $("td:eq(1)", nRow).html("<span class='brown-btn' rel='" + aData[claimsStatusIdx] + "'>APPROVED</span>");
                        }
                    }
                    else if (claimsApprovalModule == true) {
                        //alert(aData[aData.length - 1]);
                        //if (aData[aData.length - 1].indexOf("NO") == -1) {
                        //    $("td:eq(1)", nRow).html("<span class='brown-btn' rel='" + aData[claimsStatusIdx] + "'>APPROVED</span>");
                        //}
                        if (gmApprovalColumnIdx > -1 && aData[gmApprovalColumnIdx].indexOf("YES") > -1) {
                            $('td:eq(3)', nRow).html("<span class='green-exclamation-mark' title='Check Request approved by GM.'>YES</span>")
                        }

                        if (aData[claimsStatusIdx] == "CPOTENTIAL") {
                            $("td:eq(1)", nRow).html("<span class='pink-btn' rel='" + aData[claimsStatusIdx] + "'>POTENTIAL</span>");
                        }
                        else if (aData[claimsStatusIdx] == "CFORMSENT") {
                            $("td:eq(1)", nRow).html("<span class='blue-btn' rel='" + aData[claimsStatusIdx] + "'>FORMSENT</span>");
                        }
                        else if (aData[claimsStatusIdx] == "CPENDING") {
                            $("td:eq(1)", nRow).html("<span class='orange-btn' rel='" + aData[claimsStatusIdx] + "'>PENDING</span>");
                        }
                        else if (aData[claimsStatusIdx] == "C1OPEN") {
                            $("td:eq(1)", nRow).html("<span class='green-btn' rel='" + aData[claimsStatusIdx] + "'>OPEN</span>");
                        }
                        else if (aData[claimsStatusIdx] == "CCLOSED") {
                            $("td:eq(1)", nRow).html("<span class='red-btn' rel='" + aData[claimsStatusIdx] + "'>CLOSED</span>");
                        }
                        else {
                            $("td:eq(1)", nRow).html("<span class='none-status' rel='" + aData[claimsStatusIdx] + "'>" + aData[claimsStatusIdx] + "</span>");
                        }
                    }
                    else {
                        if (aData[claimsStatusIdx] == "CAPPROVED") {
                            $("td:eq(1)", nRow).html("<span class='brown-btn' rel='" + aData[claimsStatusIdx] + "'>APPROVED</span>");
                        }
                        else if (aData[claimsStatusIdx] == "CPOTENTIAL") {
                            $("td:eq(1)", nRow).html("<span class='pink-btn' rel='" + aData[claimsStatusIdx] + "'>POTENTIAL</span>");
                        }
                        else if (aData[claimsStatusIdx] == "CFORMSENT") {
                            $("td:eq(1)", nRow).html("<span class='blue-btn' rel='" + aData[claimsStatusIdx] + "'>FORMSENT</span>");
                        }
                        else if (aData[claimsStatusIdx] == "CPENDING") {
                            $("td:eq(1)", nRow).html("<span class='orange-btn' rel='" + aData[claimsStatusIdx] + "'>PENDING</span>");
                        }
                        else if (aData[claimsStatusIdx] == "C1OPEN") {
                            $("td:eq(1)", nRow).html("<span class='green-btn' rel='" + aData[claimsStatusIdx] + "'>OPEN</span>");
                        }
                        else if (aData[claimsStatusIdx] == "CCLOSED") {
                            $("td:eq(1)", nRow).html("<span class='red-btn' rel='" + aData[claimsStatusIdx] + "'>CLOSED</span>");
                        }
                        else {
                            $("td:eq(1)", nRow).html("<span class='none-status' rel='" + aData[claimsStatusIdx] + "'>" + aData[claimsStatusIdx] + "</span>");
                        }
                    }
                }
            });

            var columnToHideIdx = oClaimsGrid.fnGetColumnIndex(("CompanyID").toTitleCase());

            if (columnToHideIdx > -1) {
                oClaimsGrid.fnSetColumnVis(columnToHideIdx, false);
            }

            columnToHideIdx = oClaimsGrid.fnGetColumnIndex(("BatchID").toTitleCase());

            if (columnToHideIdx > -1) {
                oClaimsGrid.fnSetColumnVis(columnToHideIdx, false);
            }

            columnToHideIdx = oClaimsGrid.fnGetColumnIndex(("UserID").toTitleCase());

            if (columnToHideIdx > -1) {
                oClaimsGrid.fnSetColumnVis(columnToHideIdx, false);
            }

            columnToHideIdx = oClaimsGrid.fnGetColumnIndex(("ImportID").toTitleCase());

            if (columnToHideIdx > -1) {
                oClaimsGrid.fnSetColumnVis(columnToHideIdx, false);
            }

            columnToHideIdx = oClaimsGrid.fnGetColumnIndex("GM Approved");

            if (columnToHideIdx > -1) {
                oClaimsGrid.fnSetColumnVis(columnToHideIdx, false);
            }

            columnToHideIdx = oClaimsGrid.fnGetColumnIndex(("SurveyTF").toTitleCase());

            if (columnToHideIdx > -1) {
                oClaimsGrid.fnSetColumnVis(columnToHideIdx, false);
            }
        }
        else if (gridType == "table-chart") {
            var loader = $(div).find(".chart-loading");
            var headerSection = $("<thead></thead>");
            var bodySection = $("<tbody></tbody>");
            var headerRow = $("<tr/>");
            var tooltipField = $(div).attr("aria-tooltip-field");
            var tooltipTitle = $(div).attr("aria-tooltip-title");
            var tooltipQuery = $(div).attr("aria-tooltip-query");
            var fieldIdx = -1;
            var agencyNameColIdx = -1;
            var agencyTextExistsTF = false;
            var addNewColumnHeaderSectionTF = false;
            var totalsSectionExistsTF = false;

            $(loader).show();
            $(table).hide();
            $(table).html("");

            $(headerSection).appendTo(table);
            $(bodySection).appendTo(table);
            $(headerRow).appendTo(headerSection);

            //first column is blank.
            $("<th></th>").appendTo(headerRow);

            $.each(DataQuery.FieldList.split("|"), function (idx, item) {
                try {

                    if (item.toLowerCase() == "agencyname") {
                        agencyNameColIdx = idx + 1;
                        $("<th class='col-status'>" + (item).toTitleCase() + "</th>").appendTo(headerRow);
                    }
                    else if (item.toLowerCase() == "region") {
                        $("<th>" + (item).toTitleCase() + "</th>").appendTo(headerRow);
                    }
                    else if (typeof tooltipTitle == 'undefined') {
                        $("<th>" + (item).toTitleCase() + "</th>").appendTo(headerRow);
                    }
                    else {
                        $("<th><a href='#' class='chart-tooltip' ttQuery='gettableheaderdescription' ttTitle='" + (item).toTitleCase() + "'>" + (item).toTitleCase() + "</a></th>").appendTo(headerRow);
                    }

                    if (item.toLowerCase() == tooltipField.toLowerCase()) {
                        fieldIdx = idx + 1;
                    }
                }
                catch (e) {
                    //console.log(e.message);
                }
            });

            $.getJSON(dataUri, function (data) {
                var result = JSON.parse(JSON.stringify(data));

                $.each(result.aaData, function (index, row) {
                    var newRow = $("<tr/>");

                    if (addNewColumnHeaderSectionTF) {
                        addNewColumnHeaderSectionTF = false;
                        var headerClone = $(headerRow).clone();
                        $(headerClone).appendTo(table);
                    }
                    else if ((index & 1) && !totalsSectionExistsTF) {
                        $(newRow).attr("class", "alt-row");
                    }
                    else if (totalsSectionExistsTF) {
                        $(newRow).attr("class", "totals-row");
                    }

                    $.each(row, function (i, cell) {

                        if (cell.toLowerCase() == "blank") {
                            $(newRow).addClass("blank-row");
                            $(newRow).removeClass("alt-row");
                        }

                        if (cell.toLowerCase() == "totalssection") {
                            $(newRow).addClass("blank-row");
                            $(newRow).removeClass("alt-row");
                            totalsSectionExistsTF = true;
                        }

                        if (cell.toLowerCase() == "blank" || cell.toLowerCase() == "totalssection") {
                            $("<td></td>").appendTo(newRow);
                        }
                        else if (fieldIdx === i) {
                            if (cell.toLowerCase().indexOf("|") > 0) {
                                cell = cell.split("|")[1];
                                $("<td class='header-row-first-cell' colspan='" + (row.length - 1) + "'>" + cell + "</td>").appendTo(newRow);
                                $(newRow).addClass("header-row");

                                addNewColumnHeaderSectionTF = true;
                                return false;
                            }
                            else if (cell.toLowerCase() == "all") {
                                $("<td>" + cell + "</td>").appendTo(newRow);
                            }
                            else {
                                $("<td><a href='#' class='chart-tooltip' ttQuery='" + tooltipQuery + "' ttTitle='" + tooltipTitle + "'>" + cell + "</a></td>").appendTo(newRow);
                            }
                        }
                        else if (i == agencyNameColIdx) {
                            $("<td class='col-status'>" + cell + "</td>").appendTo(newRow);
                        }
                        else {
                            $("<td>" + cell + "</td>").appendTo(newRow);
                        }

                        if (i == agencyNameColIdx && cell.length > 0) {
                            agencyTextExistsTF = true;
                        }

                    })

                    $(newRow).appendTo(bodySection);
                });

            }).always(function () {
                $(loader).hide();
                $(table).fadeIn();
                SetToolTips();

                if (agencyTextExistsTF) {
                    $(".col-status").show();
                }
                else {
                    $(".col-status").hide();
                }

            })
        }

    }
    catch (e) {
        alert("BuildGrid() Error: " + e.Description);
    }
}

function SetToolTips() {
    $('.chart-tooltip').tooltipster({
        position: "right",
        contentAsHTML: true,
        functionInit: function (origin, content) {

            var toolTipQuery = $(origin).attr("ttQuery");
            var toolTipTitle = $(origin).attr("ttTitle");
            var paramValue = $(origin).text().replace(/%/g, '');
            var dataUrl = "../AISRest.svc/" + toolTipQuery + "/" + paramValue;

            //console.log(dataUrl)
            $.getJSON(dataUrl, function (data) {
                var result = JSON.parse(JSON.stringify(data));
                origin.tooltipster("content", "<b>" + toolTipTitle + "</b><br/></br/>" + result);
            });

            // this returned string will overwrite the content of the tooltip for the time being
            return "<img src='../images/ajax-loader.gif' border='0'/>&nbsp;Loading detail...";
        }
    });
}

function FancyboxPopup(ele) {
    $.fancybox({
        'height': '90%',
        'width': '90%',
        'href': ele.href,
        'type': 'iframe'
    });
}

function S4() {
    return (((1 + Math.random()) * 0x10000000) | 0).toString(16);

}
