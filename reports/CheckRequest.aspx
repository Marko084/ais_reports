<%@ page language="C#" autoeventwireup="true" inherits="reports_CheckRequest, App_Web_checkrequest.aspx.dfa151d5" enableeventvalidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1 style="text-align:center;">CHECK REQUEST INFORMATION</h1>
        <table>
            <tr>
                <td>
                    <table class="bordered">
                        <thead>
                            <tr>
                                <th colspan="2">Payee Information</th>
                            </tr>
                        </thead>
                        <tr>
                            <td>Requested Date</td>
                            <td><asp:Label runat="server" ID="lblRequestedDate" /></td>
                        </tr>
                        <tr>
                            <td>Shipper Name</td>
                            <td><asp:Label runat="server" ID="lblShipperName" /></td>
                        </tr>
                        <tr>
                            <td>Registration Number</td>
                            <td><asp:Label runat="server" ID="lblRegistrationNumber" /></td>
                        </tr>
                        <tr>
                            <td>Company</td>
                            <td><asp:Label runat="server" ID="lblCompany" /></td>
                        </tr>
                        <tr>
                            <td>National Account</td>
                            <td><asp:Label runat="server" ID="lblNationalAccount" /></td>
                        </tr>
                        <tr>
                            <td>Amount Requested</td>
                            <td><asp:Label runat="server" ID="lblApprovalAmount" /></td>
                        </tr>
                        <tr>
                            <td>Payee Name</td>
                            <td><asp:Label runat="server" ID="lblPayeeName" /></td>
                        </tr>
                        <tr>
                            <td>Payee Address</td>
                            <td><asp:Label runat="server" ID="lblPayeeAddress" /></td>
                        </tr>
                        <tr>
                            <td>Payee City, State, Zip</td>
                            <td><asp:Label runat="server" ID="lblPayeeCityStateZip" /></td>
                        </tr>
                        <tr>
                            <td>Check Number</td>
                            <td><asp:Label runat="server" ID="lblCheckNumber" /></td>
                        </tr>
                         <tr>
                            <td>Date Mailed</td>
                            <td><asp:Label runat="server" ID="lblMailedDate" /></td>
                        </tr>
                         <tr>
                            <td>Date Received</td>
                            <td><asp:Label runat="server" ID="lblReceivedDate" /></td>
                        </tr>
                        <tr>
                            <td>Adjustor Name</td>
                            <td><asp:Label runat="server" ID="lblAdjustorName" /></td>
                        </tr>
                        <tr>
                            <td>Reason for Payment</td>
                            <td><asp:Label runat="server" ID="lblReasonForPayment" /></td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <div style="height:5px;">&nbsp;</div>
        <asp:Table runat="server" ID="tblChargeAmount" CssClass="bordered" Width="400px">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Text="Account" />
                <asp:TableHeaderCell Text="Amount" />
                <asp:TableHeaderCell Text="Company" />
                <asp:TableHeaderCell Text="" />
            </asp:TableHeaderRow>
        </asp:Table>
        <div style="height:5px;">&nbsp;</div>
        <asp:Table runat="server" ID="tblDriverInfo" CssClass="bordered" Width="400px">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell Text="Driver Name" />
                <asp:TableHeaderCell Text="Location" />
            </asp:TableHeaderRow>
        </asp:Table>
        <div style="height:5px;">&nbsp;</div>
        <table class="bordered">
            <tr>
                <td>Check Request Authorized By:</td>
                <td><asp:Label runat="server" ID="lblCheckRequestAuthorizedBy" /></td>
            </tr>
            <tr>
                <td>Check Request Authorized By 2:</td>
                <td><asp:Label runat="server" ID="lblCheckRequestAuthorizedBy2" /></td>
            </tr>
        </table>
        <div style="height:5px;">&nbsp;</div>
        <table class="bordered">
            <tr>
                <td>Need By:</td>
                <td><asp:Label runat="server" ID="lblNeededBy" /></td>
            </tr>
        </table>
        <div style="height:10px;">&nbsp;</div>
        <table class="bordered" style="width:730px;">
            <thead>
                <tr>
                    <th>Comments</th>
                </tr>
            </thead>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblComments" />
                </td>
            </tr>
        </table>
        <asp:Button runat="server" ID="btnExport" Text="Print" OnClick="btnExport_Click" />
    </form>
</body>
</html>
