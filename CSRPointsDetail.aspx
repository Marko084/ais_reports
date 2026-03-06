<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CSRPointsDetail.aspx.cs" Inherits="AISReports.CSRPointsDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body {
            font-family: Calibri, Arial, Verdana;
        }

        h1 {
            padding: 5px;
        }

        td a.details-link {
            color: #0a28ba;
            font-weight: bold;
        }

        .datatable-scroll {
            overflow-x: auto;
            overflow-y: visible;
        }
        
        .highlight td {background:yellow url(images/clear-sm.png) repeat-x top; padding-left:5px; white-space:nowrap; text-align:left !important;}
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
            DataQuery.Query = "GetCSRQualityRating";

            var DTO = { 'DataQuery': DataQuery };

            var dataUri = "../ListHandler.ashx?ct=grid&qt=" + DataQuery.QueryType + "&fld=" + DataQuery.FieldList + "&qn=" + DataQuery.Query + "&pl=" + DataQuery.QueryParamList + "&qid=" + S4();

            //alert(dataUri);
            //$(".site-message").text(dataUri);
            $("#pvopoints-grid").dataTable({
                "bDestroy": true,
                "sAjaxSource": dataUri,
                "aLengthMenu": [[10, 20, 25, -1], [10, 20, 25, "All"]],
                "iDisplayLength": -1,
                "sPaginationType": "full_numbers",
                "bDeferRender": true,
                "bJQueryUI": true,
                "bAutoWidth": false,
                "fnRowCallback": function (nRow, aData, iDisplayIndex) {

                    if (parseFloat(aData[3]) < 4.30) {
                        /* Append the grade to the default row class name */
                        //$('td', nRow).html().addClass("highlight")
                        try {
                            $(nRow).removeClass("even");
                            $(nRow).removeClass("odd");
                            $(nRow).addClass("highlight");
                        }
                        catch (e) {
                            //alert(e.Message);
                        }
                        
                    }
                }
            });

        });

        function getSearchResults(ele) {
            var score = $(ele).parent().next().text();

            $("#<% =txtCSR.ClientID %>").val($(ele).text());
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

        function getQueryParameters() {
            var queryParams = "";
            var paramName = "";
            var paramValue = "";

            $(".query-parameters-div input:text").each(function () {

                paramName = $(this).attr("data-field-name");
                paramValue = $(this).val();

                if (queryParams.length == 0) {
                    queryParams = paramName + "~" + paramValue;
                }
                else {
                    queryParams += "|" + paramName + "~" + paramValue;
                }
            });

            return queryParams;
        }

        function displaySummary() {
            $("#<% =txtCSR.ClientID %>").val('');
            $("#<% =txtDisplayType.ClientID %>").val("Summary");

            $("#<% =pChart2.ClientID %>").hide();
            $("#<% =pChart.ClientID %>").show();
        }

        function displayDetails() {

            DataQuery.FieldList = $("#<% =pChart2.ClientID %>").attr("grid-display-fields");
            DataQuery.QueryType = $("#<% =pChart2.ClientID %>").attr("grid-query-type");
            DataQuery.QueryParamList = getQueryParameters();
            DataQuery.Query = "GetCSRQualityRating";

            var DTO = { 'DataQuery': DataQuery };

            var dataUri = "../ListHandler.ashx?ct=grid&qt=" + DataQuery.QueryType + "&fld=" + DataQuery.FieldList + "&qn=" + DataQuery.Query + "&pl=" + DataQuery.QueryParamList + "&qid=" + S4();

            //alert(dataUri);
           // $(".site-message").text(dataUri);
            $("#pvopoints-detail-grid").dataTable({
                "bDestroy": true,
                "sAjaxSource": dataUri,
                "aLengthMenu": [[10, 20, 25], [10, 20, 25]],
                "sPaginationType": "full_numbers",
                "sDom": 'r<"H"lf><"datatable-scroll"t><"F"ip>',
                "bDeferRender": true,
                "bJQueryUI": true,
                "bAutoWidth": false
            });

            $(".current-view").val("Detail");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding-left: 50px;">
            <h1>Nelson Westerberg CSR Quality Rating</h1>
            <asp:Panel runat="server" ID="pChart" class="grid-section" grid-query-type="storedproc" Style="width: 80%;">
                <h2>
                    <asp:Label runat="server" ID="lblDriverSummary" Text="12 Month Rolling Average thru " /><% =DateTime.Now.ToString("dd-MMM-yy") %></h2>
                <table id="pvopoints-grid" class="grid-control" style="width: 100%;">
                    <thead>
                        <tr>
                            <th></th>
                            <asp:Literal runat="server" ID="litHeaderRow" EnableViewState="false"></asp:Literal>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <asp:Literal runat="server" ID="litNoData" EnableViewState="false"></asp:Literal>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="pChart2" class="grid-section" grid-query-type="storedproc" Style="width: 100%; display: none;">
                <h2>
                    <asp:Label runat="server" ID="lblDriverDetail" Text="Driver Detail" /></h2>
                <div style="width: 100%; height: 50px; padding: 5px;">
                    <h3>
                        <asp:Label runat="server" ID="lblPVOName" Style="display: inline-block; font-weight: bold;" /></h3>
                    <a href="#" id="lnkDriverSummary" onclick="javascript:window.location.href=window.location.href;" style="float: right; position: relative; top: -20px;">Back to PVO Summary</a>
                </div>
                <table id="pvopoints-detail-grid" class="grid-control" style="width: 100%;">
                    <thead>
                        <tr>
                            <th></th>
                            <asp:Literal runat="server" ID="litDetailHeaderRow" EnableViewState="false"></asp:Literal>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <asp:Literal runat="server" ID="litDetailNoData" EnableViewState="false"></asp:Literal>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
            <asp:Label runat="server" ID="lblMessage" CssClass="site-message" />
        </div>
        <div class="query-parameters-div" style="display: none;">
            <asp:TextBox runat="server" ID="txtAgency" data-field-name="Agency" />
            <asp:TextBox runat="server" ID="txtCSR" data-field-name="CustomerServiceRep" />
            <asp:TextBox runat="server" ID="txtCompletionStartDate" data-field-name="CompletionStartDate" />
            <asp:TextBox runat="server" ID="txtCompletionEndDate" data-field-name="CompletionEndDate" />
            <asp:TextBox runat="server" ID="txtDeliveryStartDate" data-field-name="DeliveryStartDate" />
            <asp:TextBox runat="server" ID="txtDeliveryEndDate" data-field-name="DeliveryEndDate" />
            <asp:TextBox runat="server" ID="txtBookerNo" data-field-name="BookerNo" />
            <asp:TextBox runat="server" ID="txtCompanyID" data-field-name="CompanyID" />
            <asp:TextBox runat="server" ID="txtDisplayType" data-field-name="DisplayType" Text="Summary" />
            <asp:TextBox runat="server" ID="txtUserID" data-field-name="UserID" Text="0" />
        </div>
    </form>
</body>
</html>
