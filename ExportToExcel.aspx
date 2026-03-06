<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExportToExcel.aspx.cs" Inherits="AISReports.ExportToExcel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label runat="server" ID="lblMessage" Text="Checking for data to export..." />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" ShowHeader="true" OnRowDataBound="GridView1_RowDataBound">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
