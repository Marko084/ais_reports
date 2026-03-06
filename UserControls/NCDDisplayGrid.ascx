<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDDisplayGrid.ascx.cs" Inherits="AISReports.UserControls.NCDDisplayGrid" %>
<style type="text/css">
    .DTTT_button span {color:#fff !important;}
</style>
<div class="user-control-widget">
    <div class="chart-title">
        <asp:Label runat="server" ID="lblChartTitle" Text="" />
    </div>
    <asp:Panel runat="server" ID="pChart" class="grid-section">
        <table id="display-grid" class="grid-control display-grid">
            <thead>
                <tr>
                    <th></th>
                    <asp:Literal runat="server" ID="litHeaderRow" EnableViewState="false"></asp:Literal>
                </tr>
            </thead>
            <tbody>
            <tr>
                <asp:Literal runat="server" ID="litNoData" EnableViewState="false" ></asp:Literal>
            </tr>
            </tbody>
        </table>
    </asp:Panel>
</div>