<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ARSDriverTierRanking.aspx.cs" Inherits="AISInc.reports.ARSDriverTierRanking" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Atlantic Relocation Driver Tier Ranking Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <h2>Atlantic Relocation Systems, Inc.</h2>
        <h2>Driver Tier Ranking Report</h2>
        <div>
            <p><b>Period: <asp:Label runat="server" ID="lblReportDateRange"></asp:Label></b></p>
            <div style="width:47%;display:inline-block;">
                <b><span>Driver Ranking Scores</span></b>
                <asp:Table runat="server" ID="tblReport" CssClass="pure-table pure-table-bordered ranking-report-section">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell Text="Team" />
                        <asp:TableHeaderCell Text="PVO Name" />
                        <asp:TableHeaderCell Text="Score" />
                        <asp:TableHeaderCell Text="# Surveys" />
                        <asp:TableHeaderCell Text="Agent" />
                    </asp:TableHeaderRow>
                </asp:Table>
            </div>
            <div style="width:47%;vertical-align:top;display:inline-block; float:right;">
                <b><span>Driver Team Counts</span></b>
                <asp:Table runat="server" ID="tblMatrix" CssClass="pure-table pure-table-bordered pure-table-striped matrix-report-section">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell Text="Team" />
                        <asp:TableHeaderCell Text="Platinum" />
                        <asp:TableHeaderCell Text="Gold" />
                        <asp:TableHeaderCell Text="Silver" />
                        <asp:TableHeaderCell Text="Bronze" />
                        <asp:TableHeaderCell Text="Total" />
                    </asp:TableHeaderRow>
                </asp:Table>
                <br />
                <b><span>Driver Percentages By Team</span></b>
                <asp:Table runat="server" ID="tblMatrixPercentage" CssClass="pure-table pure-table-bordered pure-table-striped matrix-report-section">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell Text="Team" />
                        <asp:TableHeaderCell Text="Platinum" />
                        <asp:TableHeaderCell Text="Gold" />
                        <asp:TableHeaderCell Text="Silver" />
                        <asp:TableHeaderCell Text="Bronze" />
                    </asp:TableHeaderRow>
                </asp:Table>
                <br />
                <b><span>Driver Percentages By Agent</span></b>
                <asp:Table runat="server" ID="tblMatrixPercentageByAgent" CssClass="pure-table pure-table-bordered pure-table-striped matrix-report-section">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell Text="Agent" />
                        <asp:TableHeaderCell Text="Platinum" />
                        <asp:TableHeaderCell Text="Gold" />
                        <asp:TableHeaderCell Text="Silver" />
                        <asp:TableHeaderCell Text="Bronze" />
                    </asp:TableHeaderRow>
                </asp:Table>
            </div>
        </div>
    </form>
</body>
</html>
