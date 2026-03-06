<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CSSWeeklyDetail.aspx.cs" Inherits="AISReports.CSSWeeklyDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--<script type="text/javascript" src="../ais/script/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../ais/script/fusioncharts.js?123"></script>
    <script type="text/javascript" src="../ais/script/themes/fusioncharts.theme.fint.js"></script>
    <script type="text/javascript" src="../ais/script/themes/fusioncharts.theme.convergent.js"></script>
    <script type="text/javascript" src="../ais/script/themes/fusioncharts.theme.carbon.js"></script>
    <script type="text/javascript" src="../ais/script/themes/fusioncharts.theme.ais.js"></script>
    <script type="text/javascript" src="../ais/script/fusioncharts-jquery-plugin.js"></script>
    <script type="text/javascript" src="../ais/script/jquery-ui-1.9.2.custom.min.js"></script>-->
    <style type="text/css">
        .chart-style {display:inline !important;}
        /*.metric-section {display:block;width:400px;}*/
        .metric-label {display:inline-block;width:260px; font-size:small;font-weight:bold; padding:5px; text-align:right;}
        .metric-value {font-weight:bold;}
        .chart-column {width:500px; display:inline-block; padding:5px 0px 0px 0px;}

    </style>
    <script type="text/javascript">
        var cid;
        var code;

        $(function () {
            //alert($(".ui-tabs-active > a").attr("href").split("?")[1]);
            //alert(location.search);
            cid = getUrlParameter("cid");
            code = getUrlParameter("scac");
            var dataUri = "../ListHandler.ashx?gt=none&ct=grid&qt=storedproc&fld=&qn=Cartwright_CSSWeeklyReport&pl=KeyFieldValue~" + code + "&qid="+S4();

            $.ajax({
                dataType: "json",
                type: "GET",
                async: true,
                url: dataUri,
                error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); },
                success: function (data) {
                    //console.log(data.aaData);
                    $.each(data.aaData, function (idx, item) {
                        //CreateChart(idx, item[9]);
                        SetMetricInfo(idx,item);
                    });
                }
            });
        })

        function CreateChart(index, xml) {
            try {
                new FusionCharts({
                    width: "400px",
                    height: "400px;",
                    type: "column3d",
                    renderAt: "chart-"+index,
                    dataFormat: "xml",
                    dataSource: xml
                }).render();
            }
            catch (e) {
                alert(e);
            }
        }

        function SetMetricInfo(index,item) {
            var metricSection = $("#metric-section-" + index);

            $(metricSection).find(".metric-label-0").text(item[1]);
            $(metricSection).find(".metric-value-0").text(item[2]);
            $(metricSection).find(".metric-label-1").text(item[3]);
            $(metricSection).find(".metric-value-1").text(item[4]);
            $(metricSection).find(".metric-label-2").text(item[5]);
            $(metricSection).find(".metric-value-2").text(item[6]);
            $(metricSection).find(".metric-label-target").text(item[7]);
            $(metricSection).find(".metric-value-target").text(item[8]);
        }
    </script>
    <script type="text/javascript">
        function S4() {
            return (((1 + Math.random()) * 0x10000000) | 0).toString(16);
        }

        //function getUrlParameter(name) {
        //    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
        //    var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
        //    var results = regex.exec("?" + $(".ui-tabs-active > a").attr("href").split("?")[1]); //regex.exec("?" + $(".selected-metric").val().split("?")[1]);//regex.exec(location.search); 
        //    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
        //}
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <!--<div style="display:inline;">-->
        <div>
            <p>CSS Weekly Detail</p>
        </div>
        <!--<div class="chart-column">-->
        <div class="chart-style">
            <div id="chart-0"></div>
            <div id="metric-section-0" class="metric-section">
                <span class="metric-label metric-label-0"></span>
                <span class="metric-value metric-value-0"></span>
                <br />
                <span class="metric-label metric-label-1"></span>
                <span class="metric-value metric-value-1"></span>
                <br />
                <span class="metric-label metric-label-2"></span>
                <span class="metric-value metric-value-2"></span>
                <br />
                <span class="metric-label metric-label-target"></span>
                <span class="metric-value metric-value-target"></span>
                <br />
            </div>
        </div>
        <div class="chart-style">
            <div id="chart-1"></div>
            <div id="metric-section-1" class="metric-section">
                <span class="metric-label metric-label-0"></span>
                <span class="metric-value metric-value-0"></span>
                <br />
                <span class="metric-label metric-label-1"></span>
                <span class="metric-value metric-value-1"></span>
                <br />
                <span class="metric-label metric-label-2"></span>
                <span class="metric-value metric-value-2"></span>
                <br />
                <span class="metric-label metric-label-target"></span>
                <span class="metric-value metric-value-target"></span>
                <br />
            </div>
        </div>
        <div class="chart-style">
            <div id="chart-2"></div>
            <div id="metric-section-2" class="metric-section">
                <span class="metric-label metric-label-0"></span>
                <span class="metric-value metric-value-0"></span>
                <br />
                <span class="metric-label metric-label-1"></span>
                <span class="metric-value metric-value-1"></span>
                <br />
                <span class="metric-label metric-label-2"></span>
                <span class="metric-value metric-value-2"></span>
                <br />
                <span class="metric-label metric-label-target"></span>
                <span class="metric-value metric-value-target"></span>
                <br />
            </div>
        </div>
        <div class="chart-style">
            <div id="chart-3"></div>
            <div id="metric-section-3" class="metric-section">
                <span class="metric-label metric-label-0"></span>
                <span class="metric-value metric-value-0"></span>
                <br />
                <span class="metric-label metric-label-1"></span>
                <span class="metric-value metric-value-1"></span>
                <br />
                <span class="metric-label metric-label-2"></span>
                <span class="metric-value metric-value-2"></span>
                <br />
                <span class="metric-label metric-label-target"></span>
                <span class="metric-value metric-value-target"></span>
                <br />
            </div>
        </div>
        <!--</div>-->
         <!--<div class="chart-column">-->
        <div class="chart-style">
            <div id="chart-4"></div>
            <div id="metric-section-4" class="metric-section">
                <span class="metric-label metric-label-0"></span>
                <span class="metric-value metric-value-0"></span>
                <br />
                <span class="metric-label metric-label-1"></span>
                <span class="metric-value metric-value-1"></span>
                <br />
                <span class="metric-label metric-label-2"></span>
                <span class="metric-value metric-value-2"></span>
                <br />
                <span class="metric-label metric-label-target"></span>
                <span class="metric-value metric-value-target"></span>
                <br />
            </div>
        </div>
        <div class="chart-style">
            <div id="chart-5"></div>
            <div id="metric-section-5" class="metric-section">
                <span class="metric-label metric-label-0"></span>
                <span class="metric-value metric-value-0"></span>
                <br />
                <span class="metric-label metric-label-1"></span>
                <span class="metric-value metric-value-1"></span>
                <br />
                <span class="metric-label metric-label-2"></span>
                <span class="metric-value metric-value-2"></span>
                <br />
                <span class="metric-label metric-label-target"></span>
                <span class="metric-value metric-value-target"></span>
                <br />
            </div>
        </div>
        <div class="chart-style">
            <div id="chart-6"></div>
            <div id="metric-section-6" class="metric-section">
                <span class="metric-label metric-label-0"></span>
                <span class="metric-value metric-value-0"></span>
                <br />
                <span class="metric-label metric-label-1"></span>
                <span class="metric-value metric-value-1"></span>
                <br />
                <span class="metric-label metric-label-2"></span>
                <span class="metric-value metric-value-2"></span>
                <br />
                <span class="metric-label metric-label-target"></span>
                <span class="metric-value metric-value-target"></span>
                <br />
            </div>
        </div>
        <div class="chart-style">
            <div id="chart-7" class="chart-style"></div>
            <div id="metric-section-7" class="metric-section">
                <span class="metric-label metric-label-0"></span>
                <span class="metric-value metric-value-0"></span>
                <br />
                <span class="metric-label metric-label-1"></span>
                <span class="metric-value metric-value-1"></span>
                <br />
                <span class="metric-label metric-label-2"></span>
                <span class="metric-value metric-value-2"></span>
                <br />
                <span class="metric-label metric-label-target"></span>
                <span class="metric-value metric-value-target"></span>
                <br />
            </div>
        </div>
        <!--</div>-->
    <!--</div>-->
    </form>
</body>
</html>

