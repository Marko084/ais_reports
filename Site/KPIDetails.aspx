<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPIDetails.aspx.cs" Inherits="AISReports.Site.KPIDetails" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>KPI Details</title>
    <%--<link href="../css/styles.css" rel="stylesheet" type="text/css" />--%>
    <link href="../css/dark-hive/grid-style.css" rel="stylesheet" type="text/css" />

    <script src="../script/ais.js" type="text/javascript"></script>
    <script type="text/javascript" src="../script/jquery-1.6.2.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".grid th").each(function () {
                if ($(this).text() != "") {
                    $(this).html("<a href=\"javascript:sortGrid('" + $(this).text() + "');\">" + $(this).text() + "</a>");
                }
            });
        });

        function sortGrid(colName) {
            var currentColName = $(".textbox-column-sort").val();
            var sortOrder=$(".textbox-sort-order").val();

            if (currentColName == colName) {
                if (sortOrder == "ASC") {
                    $(".textbox-sort-order").val("DESC");
                }
                else {
                    $(".textbox-sort-order").val("ASC");
                }
            }
            else {
                $(".textbox-sort-order").val("ASC");
            }

            $(".textbox-column-sort").val(colName);

            __doPostBack('lnkColumnSort', '');

        }
    </script>
    <style type="text/css">
        table th.a:link {text-decoration:underline;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="grid-title">
        <b><span>Key Performance Indicator:</span></b>&nbsp;<asp:Label runat="server" ID="lblKPIDetail" />
    </div>
    <asp:Literal runat="server" ID="litKPIDetail" />
    <asp:LinkButton runat="server" ID="lnkColumnSort" CssClass="link-column-sort" style="display:none;"/>
    <asp:TextBox runat="server" ID="txtColumnSort" CssClass="textbox-column-sort" style="display:none;" />
    <asp:TextBox runat="server" ID="txtSortOrder" CssClass="textbox-sort-order" Text="ASC" style="display:none;"/>
    <asp:Label runat="server" ID="lblMessage" />
    </form>
</body>
</html>
