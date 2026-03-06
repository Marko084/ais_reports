<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WCCDetail.aspx.cs" Inherits="AISReports.WCCDetail" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../script/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../script/jquery-ui-1.8.16.custom.min.js"></script>
    <link href="css/redmond/jquery-ui-1.9.2.custom.css" rel="Stylesheet" type="text/css" />
    <link href="css/redmond/master-styles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
     .section-header-row {background-color: #ccc;}
     .section-header-row td {color:#000000 !important;}
    </style>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#<% = ddlCriteriaList.ClientID %>").change(function () {
                setChartData($(this).find(":selected").attr("aria-chart-url"));
            });

            $(".chart-title span").each(function () {
                if ($(this).text() == "") {
                    $(this).parent().hide();
                }
                else {
                    $(this).parent().show();
                }
            });

            setSectionHeader("Safety Compliance", "Safety CSA Pts");
            setSectionHeader("Hauling Award", "PVO Rating");
            setSectionHeader("Technology Rating", "Load Date");

            $(".upload-date").text("Data displayed is through 3/22/2019");

        });
        
        function showChartPopup() {
            $("#dialog").dialog({ height: 870, width: 790, stack: false, zIndex: "9999" });
        }

        function setChartData(url) {
            $(".iframe-chart").attr("src",url);
        }

        $("#wccCharts").button();

        function setSectionHeader(sectionHeader, searchText) {
            $(".grid").each(function () {

                var tr = $("#" + $(this).attr("id") + " tr td").filter(function () {
                    return $(this).text() == searchText;
                }).closest("tr");

                var cCount = $(tr).children('td').length;
                var rowHtml = "<tr class='section-header-row'><td>" + sectionHeader + "</td>";
                var blankRowHtml = "<tr><td>&nbsp;</td>";

                for (i = 0; i < cCount - 1; i++) {
                    rowHtml += "<td></td>";
                    blankRowHtml += "<td></td>";
                }

                rowHtml += "</tr>";
                blankRowHtml += "</tr>";

                $(tr).before(blankRowHtml);
                $(tr).before(rowHtml);
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="height:93px;">
                <h2 style="display:inline-block; width:850px;">World Class Commitment  - Cumulative Convention Year <asp:Label ID="lblYear" runat="server" /></h2>
                <a href="#" id="wccCharts" onclick="showChartPopup();" >View Charts</a>
                <br />
                <h4 style="display:inline-block;position:relative; top:-28px;"><asp:Label runat="server" ID="lblUploadDate" CssClass="upload-date" /></h4>
        </div>
        <div class="wcc-section">
            <div class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix" style="padding: 5px;">
                <asp:Label runat="server" ID="GridViewLabel1" />
            </div>
            <div id="grid-view-1" style="padding-top:0px;">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" CssClass="grid column-width" GridLines="Horizontal"
                               PagerStyle-CssClass="pager" RowStyle-Wrap="false" AlternatingRowStyle-Wrap="false"
                               OnRowDataBound="GridView_RowDataBound" Font-Size="Small" HeaderStyle-HorizontalAlign="Center">
                    <AlternatingRowStyle CssClass="alt-row" />
                </asp:GridView>
            </div>
        </div>
        <div class="wcc-section">
            <div class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix" style="padding:5px;">
                <asp:Label runat="server" ID="GridViewLabel2" />
            </div>
            <div id="grid-view-2">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="true" CssClass="grid column-width" GridLines="Horizontal"
                               PagerStyle-CssClass="pager" RowStyle-Wrap="false" AlternatingRowStyle-Wrap="false"
                               OnRowDataBound="GridView_RowDataBound" Font-Size="Small" HeaderStyle-HorizontalAlign="Center">
                    <AlternatingRowStyle CssClass="alt-row" />
                </asp:GridView>
            </div>
        </div>
        <div class="wcc-section">
            <div class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix" style="padding:5px;">
                <asp:Label runat="server" ID="GridViewLabel3" />
            </div>
            <div id="grid-view-3">
                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="true" CssClass="grid column-width" GridLines="Horizontal"
                               PagerStyle-CssClass="pager" RowStyle-Wrap="false" AlternatingRowStyle-Wrap="false"
                               OnRowDataBound="GridView_RowDataBound"  Font-Size="Small" HeaderStyle-HorizontalAlign="Center">
                    <AlternatingRowStyle CssClass="alt-row" />
                    
                </asp:GridView>
            </div>
        </div>
        <div class="wcc-section">
            <div class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix" style="padding:5px;">
                <asp:Label runat="server" ID="GridViewLabel4" />
            </div>
            <div id="grid-view-4">
                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="true" CssClass="grid column-width" GridLines="Horizontal"
                               PagerStyle-CssClass="pager" RowStyle-Wrap="false" AlternatingRowStyle-Wrap="false"
                               OnRowDataBound="GridView_RowDataBound" Font-Size="Small" HeaderStyle-HorizontalAlign="Center">
                    <AlternatingRowStyle CssClass="alt-row" />
                </asp:GridView>
            </div>
        </div>
    </div>

        <div id="dialog" title="World Class Committment Charts" style="display:none;">
            <span>Select criteria to chart:</span>
            <asp:DropDownList runat="server" ID="ddlCriteriaList" CssClass="chart-filter-dropdown"/>
            <span style="display:none;">Select year:</span>
            <asp:DropDownList runat="server" ID="ddlCriteriaYear" style="display:none;" />
            <iframe runat="server" id="frmChart" class="iframe-chart" frameborder="0" scrolling="no" width="730" height="780"></iframe>
        </div>
        <div>
            <asp:Label runat="server" ID="lblFilter" style="display:none;" />
            <asp:Label runat="server" ID="lblLocation" style="display:none;" />
        </div>
    </form>
</body>
</html>