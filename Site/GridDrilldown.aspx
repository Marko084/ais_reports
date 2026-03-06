<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridDrilldown.aspx.cs" Inherits="AISReports.Site.GridDrilldown" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .cms-diary-notes {
           width:100%;
        }
        table tr td:nth-child(5){
            width:330px !important;
        }
        .odd-row { background-color:#ccc;}
        .even-row{background-color:#fff;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="drilldown-title"><asp:Label runat="server" ID="lblTitle"></asp:Label></div>
        <asp:GridView ID="GridView1" runat="server" RowStyle-CssClass="odd-row" AlternatingRowStyle-CssClass="even-row" CssClass="cms-diary-notes"></asp:GridView>
        <div><asp:Label runat="server" ID="lblMessage" style="font-size:x-large;"></asp:Label></div>
    </div>
        
    </form>
</body>
</html>
