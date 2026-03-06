<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chart.aspx.cs" Inherits="AISReports.Chart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="fancybox/jquery-1.4.3.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">

        //$(function () {
//            alert('normal script load...');
//            google.load("visualization", "1", { packages: ["corechart"] });
//            google.setOnLoadCallback(drawChart);

//        function drawChart() {
//            var data = google.visualization.arrayToDataTable([
//          ['Year', 'Sales', 'Expenses'],
//          ['2004', 1000, 400],
//          ['2005', 1170, 460],
//          ['2006', 660, 1120],
//          ['2007', 1030, 540]
//        ]);

//            var options = {
//                title: 'Company Performance',
//                titleTextStyle: { color: '#FFFFFF' },
//                legend: {textStyle: {color: '#FFFFFF'} },
//                vAxis: { title: 'Year', titleTextStyle: { color: '#FFFFFF' }, gridlines: { color: '#FFFFFF' }, baselineColor: '#FFFFFF', textStyle: {color:'#FFFFFF'} },
//                hAxis: {title: 'Score', titleTextStyle: {color: '#FFFFFF'}, gridlines: {color: '#FFFFFF'}, baselineColor : '#FFFFFF',textStyle: {color:'#FFFFFF'} },
//                backgroundColor: {stroke:'#FFFFFF',strokeWidth: '#FFFFFF', fill: '#000000'},
//                is3D: true
//            };

//            var chart = new google.visualization.BarChart(document.getElementById('chart_div'));
//            chart.draw(data, options);
//        };
    //});
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal runat="server" ID="litJS" />
    <div id="chart_div" style="width:730px; height:780px;"></div>
    <asp:TextBox runat="server" ID="txtScript" TextMode="MultiLine" Columns="30" Rows="20" />
    </form>
</body>
</html>
