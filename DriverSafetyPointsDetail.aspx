<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DriverSafetyPointsDetail.aspx.cs" Inherits="AISReports.DriverSafetyPointsDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body { font-family: Calibri, Arial, Verdana;}
        h1 {padding:5px;}
        td a.details-link { color:#0a28ba; font-weight:bold;}
        .datatable-scroll {overflow-x: auto;overflow-y: visible;}
    </style>
     <script type="text/javascript">

        var DataQuery = new function () {
            this.FieldList = "",
            this.TableList = "",
            this.FilterList = "",
            this.OrderByList = "",
            this.Query = "",
            this.QueryType = "",
            this.QueryParamList = ""
        };

        $(function () {
            //$(".grid-section").each(function () {
            DataQuery.FieldList = $("#<% =pChart.ClientID %>").attr("grid-display-fields");
            DataQuery.QueryType = $("#<% =pChart.ClientID %>").attr("grid-query-type");
            DataQuery.QueryParamList = getQueryParameters();
            DataQuery.Query = "GetDriverSafetyPoints";

            var DTO = { 'DataQuery': DataQuery };

            var dataUri = "../ListHandler.ashx?ct=grid&qt=" + DataQuery.QueryType + "&fld=" + DataQuery.FieldList + "&qn=" + DataQuery.Query + "&pl=" + DataQuery.QueryParamList + "&qid=" + S4();

            //alert(dataUri);
            //$(".site-message").text(dataUri);
            $("#safetypoints-grid").DataTable({
                "bDestroy": true,
                "bDeferRender": true,
                ajax: dataUri,
                lengthMenu: [[10, 20, 25,-1], [10, 20, 25,"All"]],
                pagingType: "full_numbers",
                colReorder: true,
                language: {
                    "emptyTable": "No data found to display."
                },
                dom: '<"fg-toolbar ui-toolbar ui-widget-header ui-helper-clearfix ui-corner-tl ui-corner-tr"Blfr><"dataTables_scroll"t><"fg-toolbar ui-toolbar ui-widget-header ui-helper-clearfix ui-corner-bl ui-corner-br"ip>',
                buttons: [{ extend: 'excel', filename: 'GridExport', className: 'ui-corner-br fg-button ui-button ui-state-default' }],
                rowCallback : function (nRow, aData, iDisplayIndex) {

                    //if (typeof detailLink != 'undefined') {
                    /* Append the grade to the default row class name */
                    $('td:eq(3)', nRow).html("<a href='#' title='View Driver Details' class='details-link' onclick='javascript: getSearchResults(this); return false;'>" + aData[3] + "</a>");
                    //}
                }
            });

        });

        function getSearchResults(ele) {
            var score = $(ele).parent().next().text();

            $("#<% =txtPVOName.ClientID %>").val($(ele).text());
            $("#<% =lblPVOName.ClientID %>").text($(ele).text() + "                 Score:  " + score);
            $("#<% =txtDisplayType.ClientID %>").val("Detail");

            $("#<% =pChart.ClientID %>").hide();

            displayDetails();
            $("#<% =pChart2.ClientID %>").show();
        }

        function setPVOName(pvoName) {
            $(".pvoname").val(pvoName);
        }

        function S4() {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16);
        }

        //function getQueryParameters() {
        //    var queryParams = "";
        //    var paramName = "";
        //    var paramValue = "";

        //    $(".query-parameters-div input:text").each(function () {

        //        paramName = $(this).attr("data-field-name");
        //        paramValue = $(this).val();

        //        if (queryParams.length == 0) {
        //            queryParams = paramName + "~" + paramValue;
        //        }
        //        else {
        //            queryParams += "|" + paramName + "~" + paramValue;
        //        }
        //    });
            
        //    return queryParams;
        //}

        function displaySummary() {
            $("#<% =txtPVOName.ClientID %>").val('');
            $("#<% =txtDisplayType.ClientID %>").val("Summary");

            $("#<% =pChart2.ClientID %>").hide();
            $("#<% =pChart.ClientID %>").show();
        }

        function displayDetails() {
            DataQuery.FieldList = $("#<% =pChart2.ClientID %>").attr("grid-display-fields");
            DataQuery.QueryType = $("#<% =pChart2.ClientID %>").attr("grid-query-type");
            DataQuery.QueryParamList = getQueryParameters();
            DataQuery.Query = "GetDriverSafetyPoints";

            var DTO = { 'DataQuery': DataQuery };

            var dataUri = "../ListHandler.ashx?ct=grid&qt=" + DataQuery.QueryType + "&fld=" + DataQuery.FieldList + "&qn=" + DataQuery.Query + "&pl=" + DataQuery.QueryParamList + "&qid=" + S4();

            //alert(dataUri);
            //$(".site-message").text(dataUri);
            $("#safetypoints-detail-grid").DataTable({
                "bDestroy": true,
                "bDeferRender": true,
                ajax: dataUri,
                lengthMenu: [[10, 20, 25, -1], [10, 20, 25, "All"]],
                pagingType: "full_numbers",
                colReorder: true,
                language: {
                    "emptyTable": "No data found to display."
                },
                dom: '<"fg-toolbar ui-toolbar ui-widget-header ui-helper-clearfix ui-corner-tl ui-corner-tr"Blfr><"dataTables_scroll"t><"fg-toolbar ui-toolbar ui-widget-header ui-helper-clearfix ui-corner-bl ui-corner-br"ip>',
                buttons: [{ extend: 'excel', filename: 'GridExport', className: 'ui-corner-br fg-button ui-button ui-state-default' }]
           });

            $(".current-view").val("Detail");
        }
     </script>
</head>
<body>
    <form id="form1" runat="server">
         <div style="padding-left:50px;">
    <h1>Driver Safety Points</h1>
        <asp:Panel runat="server" ID="pChart" class="grid-section" grid-query-type="storedproc" style="width:80%;" >
            <h2><asp:Label runat="server" ID="lblDriverSummary" Text="Driver Summary" /></h2>
            <table id="safetypoints-grid" class="grid-control" style="width:100%;" >
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
        <asp:Panel runat="server" ID="pChart2" class="grid-section" grid-query-type="storedproc" style="width:100%;display:none;" >
            <h2><asp:Label runat="server" ID="lblDriverDetail" Text="Driver Detail" /></h2>
            <div style="width:100%;height:50px;padding:5px;">
                <h3><asp:Label runat="server" ID="lblPVOName" style="display:inline-block;font-weight:bold;"/></h3>
                <a href="#" id="lnkDriverSummary" onclick="javascript:window.location.href=window.location.href;" style="float:right;position:relative;top:-20px;">Back to Driver Summary</a>
            </div>
            <table id="safetypoints-detail-grid" class="grid-control" style="width:100%;" >
                <thead>
                    <tr>
                        <th></th>
                        <asp:Literal runat="server" ID="litDetailHeaderRow" EnableViewState="false"></asp:Literal>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <asp:Literal runat="server" ID="litDetailNoData" EnableViewState="false" ></asp:Literal>
                    </tr>
                </tbody>
            </table>
        </asp:Panel>
        <asp:Label runat="server" ID="lblMessage" />
    </div>
    <div class="query-parameters-div" style="display:none;">
        <asp:TextBox runat="server" ID="txtAgent" data-field-name="Agent" />
        <asp:TextBox runat="server" ID="txtPVOName" data-field-name="PVOName" />
        <asp:TextBox runat="server" ID="txtEnteredStartDate" data-field-name="StartEnteredDate" />
        <asp:TextBox runat="server" ID="txtEnteredEndDate" data-field-name="EndEnteredDate" />
        <asp:TextBox runat="server" ID="txtViolationType" data-field-name="ViolationType" />
        <asp:TextBox runat="server" ID="txtCompanyID" data-field-name="CompanyID" />
        <asp:TextBox runat="server" ID="txtDisplayType" data-field-name="DisplayType" Text="Summary" />
        <asp:TextBox runat="server" ID="txtUserID" data-field-name="UserID" Text="0" />
    </div>
    </form>
</body>
</html>
