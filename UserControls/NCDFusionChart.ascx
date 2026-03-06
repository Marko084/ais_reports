<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDFusionChart.ascx.cs" Inherits="AISReports.UserControls.NCDFusionChart" %>
<style type="text/css">
    .hideNoData {display:none;}
    .showNoData {font-family:Tahoma;font-size:22px; position:relative; top:100px; text-align:center;  z-index:999999;}
    .chart-button-section {display:inline;float:right; width:125px;text-align:right;padding-right:5px;padding-top:4px;}
    .chart-button-section span {color:#fff;font-weight:bold;display:inline-block;cursor: pointer;padding:2px;}
    .chart-button-icon {width: 19px; height: 16px; background-image: url(../images/chart-buttons.png); }
    .chart-button-area {width: 19px; height: 16px;background-position: 25px 0; }
    .chart-button-bar {background-position: 0 0; }
    .chart-button-bubble {width: 19px; height: 16px;background-position: -50px 0; }
    .chart-button-column {width: 19px; height: 16px;background-position: -25px 0; }
    .chart-button-doughnut {width: 19px; height: 16px;background-position: -75px 0; }
    .chart-button-line {width: 19px; height: 16px;background-position: 100px 0; }
    .chart-button-combo {width: 19px; height: 16px;background-position: 75px 0; }
    .chart-button-combo2 {width: 19px; height: 16px;background-position: 75px 0; }
    .chart-button-pie {width: 19px; height: 16px;background-position: 50px 0; }
    .chart-title-text {width:56%;}
    .chart-button-stackedcolumn {width: 19px; height: 16px;background-position: -25px 0; }
</style>

<div class="user-control-widget" style="border: 1px solid transparent;">
      <div class="chart-title" style="width:100%;">
        <asp:Label runat="server" ID="lblChartTitle" CssClass="chart-title-text"  />
        <asp:PlaceHolder runat="server" ID="PlaceHolder1" />
      </div>
    <div id="errorDIV" class="hideNoData">
        <b>There is no data to display in this chart.</b>
    </div>
    <asp:Panel runat="server" ID="pChart" style="text-align:center;width:100%;" class="fusion-chart-section"></asp:Panel>
</div>
