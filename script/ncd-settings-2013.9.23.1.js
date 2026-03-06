
var uid = $(".uid").text();
var cid = $(".cid").text();
var consolidated = $(".consolidated-id").text();
var vChart;
var oEditGrid;

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

    GenerateXamlCharts();
    GenerateGrids();
});

//load visifire charts 

function GenerateXamlCharts() {
    try {
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
    var chartQueryString = "";

    for (var i = 0; i < DataQuery.QueryParamList.split("|").length; i++) {
        paramNames.push(DataQuery.QueryParamList.split("|")[i].split("~")[0]);
        paramValues.push(DataQuery.QueryParamList.split("|")[i].split("~")[1].split(",")[0]);
    }

    if (typeof chartTheme != 'undefined' && chartTheme != "") {
        chartQueryString="query_name=" + queryName + "&pname=" + paramNames.toString() + "&pvalue=" + paramValues.toString() + "&theme="+chartTheme+"&chart_type=" + chartType;
    }
    else{
        chartQueryString = "query_name=" + queryName + "&pname=" + paramNames.toString() + "&pvalue=" + paramValues.toString() + "&theme=Theme3&chart_type=" + chartType;
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
     //$("#site-message-section").text("ChartData.aspx?"+dataUri);
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
    //$("#site-message-section").text(drillDownUrl + "?" + queryUri + "&kpi=" + e.AxisLabel);
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
            //$("#site-message-section").text(dataUri);
//        }
        var DTO = { 'DataQuery': DataQuery };

        if (gridType == "display") {
            $(table).dataTable({
                "bProcessing": true,
                "bServerSide": true,
                "bJQueryUI": true,
                "bDestroy": true,
                "sAjaxSource": dataUri,
                "oLanguage": { "sZeroRecords": "No data found to display." },
                "aoColumns": [null,{ "sClass": "grid-cell-nowrap" }, null]

            });

            $("#display-grid_filter").hide();
            $("#display-grid_processing").hide();
            $("#display-grid_length").hide();
            $("#display-grid_paginate").hide();
           // $("#display-grid_paginate").parent().hide();

            $(".DataTables_sort_icon").hide();
        }
        else if (gridType == "blog") {

            detailLink = $(div).attr("detail-link");

            $(table).dataTable({
                "aLengthMenu": [[10, 20, 25], [10, 20, 25]],
                "bJQueryUI": true,
                "sAjaxSource": dataUri,
                "sPaginationType": "full_numbers",
                "sDom": '<"H"lfr><"datatable-scroll"t><"F"ip>',
                "oLanguage": { "sZeroRecords": "No data found to display." },
                "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                    if (aData[1] == 0) {
                        $("td:eq(1)", nRow).html("<span class='response-count red-btn' rel='" + aData[6] + "'>" + aData[1] + "</span>");
                    }
                    else {
                        $("td:eq(1)", nRow).html("<span class='response-count green-btn' rel='" + aData[6] + "'>" + aData[1] + "</span>");
                    }
                    //$("td:eq(0)", nRow).html(aData[0]);
                    $("td:eq(2)", nRow).html("<a class='survey-detail' title='View survey detail' href='" + detailLink + aData[6] + "&cid=" + cid + "&amp;consolidated=" + consolidated + "' onclick='javascript:DisplaySurveyDetails(this);return false;'>Detail</a>");
                    $("td:eq(3)", nRow).html("<a class='create-comment' rel='" + aData[6] + "' title='Click to add a comment' href='#' onclick='javascript:getComments(this);return false;'>Add</a>");
                    $("td:eq(4)", nRow).html("<a href='#' class='view-comments' rel='" + aData[6] + "' title='Click to view comments' onclick='javascript:getComments(this);return false;'>Comments</a>");
                }
        });

        }
        else if (gridType == "search") {

            detailLink = $(div).attr("detail-link");

            $(table).dataTable({
                "bDestroy": true,
                "sAjaxSource": dataUri,
                "aLengthMenu": [[10, 20, 25], [10, 20, 25]],
                "sPaginationType": "full_numbers",
                "bDeferRender": true,
                "bJQueryUI": true,
                "oLanguage": { "sZeroRecords": "No data found to display." },
                "sDom": '<"H"Tfr><"datatable-scroll"t><"F"ip>',                
                "oTableTools": { "aButtons": [{
                    "sExtends": "xls",
                    "sFileName": "SurveyResults.xls"
                }],
                    "sSwfPath": "../media/swf/copy_csv_xls_pdf.swf"
                },
                "fnRowCallback": function (nRow, aData, iDisplayIndex) {

                    if (typeof detailLink != 'undefined') {
                        /* Append the grade to the default row class name */
                        $('td:eq(0)', nRow).html("<a href='" + detailLink + aData[1] + "&amp;cid=" + cid + "&amp;consolidated="+consolidated+"' onclick='javascript: FancyboxPopup(this); return false;'>Detail</a>");
                    }
                }
            });

        }
        else if (gridType == "edit") {

            detailLink = $(div).attr("detail-link");
            var pKeyFieldName = $(div).attr("primary-key-field");

           $("#site-message-section").text(dataUri);
            //"sDom": '<"H"Tfr>t<"F"ip>',
            oEditGrid = $(table).dataTable({
                "bDestroy": true,
                "sAjaxSource": dataUri,
                "aLengthMenu": [[10, 20, 25], [10, 20, 25]],
                "sPaginationType": "full_numbers",
                "bScrollCollapse": true,
                "bDeferRender": true,
                "bJQueryUI": true,
                "oLanguage": { "sZeroRecords": "No data found to display." },
                "sDom": 'r<"H"lf><"datatable-scroll"t><"F"ip>',
                "oTableTools": { "aButtons": [{
                    "sExtends": "xls",
                    "sFileName": "SurveyResults.xls"
                }],
                    "sSwfPath": "../media/swf/copy_csv_xls_pdf.swf"
                },
                "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                    var colIndex = oEditGrid.fnGetColumnIndex(pKeyFieldName);
                    $('td:eq(0)', nRow).html("<a href='#' rel='" + aData[colIndex] + "' onclick='javascript:RunQuery(this);ShowEditDialog(this); return false;'>Edit</a>&nbsp;<a href='#' rel='" + aData[colIndex] + "' onclick='javascript: RunQuery(this); return false;'>Delete</a>");
                }
            });

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
