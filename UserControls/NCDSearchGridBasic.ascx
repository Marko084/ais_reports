<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDSearchGridBasic.ascx.cs" Inherits="AISReports.UserControls.NCDSearchGridBasic" %>
<style type="text/css">
    .search-grid-filter 
    {
        display:inline-block;
        position:relative;
        top:40px;
        z-index:9000;
        height:0px;
        width:100%;
        text-align:center;
    }
</style>
<div class="user-control-widget">
    <div class="chart-title">
        <asp:Label runat="server" ID="lblChartTitle" Text="" />
    </div>
    <asp:Panel runat="server" ID="pChart" class="grid-section">
        <!--<div id="search-grid-filter-section" style="display:none;" >
            <span>Sort by Question:</span>
            <select id="search-grid-dropdown-filter" onchange="javascript: getSearchGridFilterResults(this);">
                <option value="">None</option>
            </select>
        </div>-->
        <table id="search-grid-basic" class="grid-control">
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
        <span class="search-grid-basic-message" style="display:block; color:#fff;"></span>
    </asp:Panel>
    
</div>