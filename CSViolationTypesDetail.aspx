<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CSViolationTypesDetail.aspx.cs" Inherits="AISReports.CSViolationTypesDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body { font-family: Calibri, Arial, Verdana;}
        h1 {padding:5px;}
        td a.details-link {font-weight:bold; color:#000 !important;}
        .query-parameters-div { display:none;}
        /* th,td{max-width:120px !important ;word-wrap: break-word;} */
        .datatable-scroll {overflow-x: auto;overflow-y: visible;}
        #csa-safetypoints-definition-tabs {font-size:smaller;}
        #csa-points-by-location-grid {font-size:10pt;}
        #csa-points-by-location {width:375px;display:inline-block;border:1px solid green;padding-right:15px;position:relative;top:-30%;left:0px;}
        .additional-links-section{text-align:right; display:block;font-size:medium;}
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
            loadSummaryGrid();
        });

        function getSearchResults(ele) {

            $("#<% =txtViolationSection.ClientID %>").val($(ele).text());

            if ($("#<% =txtPVOName.ClientID %>").val() != "") {
                var pvoName = $("#<% =txtPVOName.ClientID %>").val();
                $("#<% =lblPVOName.ClientID %>").text(pvoName+", (Violation Section : " + $(ele).text()+")");
            }
            else {
                $("#<% =lblPVOName.ClientID %>").text("Violation Section : " + $(ele).text());
            }

            $("#<% =txtDisplayType.ClientID %>").val("Detail");
            $("#<% =lblReportDisplay.ClientID %>").text("CSA Violation Detail");
            $("#lnkDriverSummary").show();

            $("#<% =pChart.ClientID %>").hide();

            displayDetails();

            $("#<% =pChart2.ClientID %>").show();
            $("#<% =pChart2.ClientID %>").css("display", "inline-block");

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
            
            //$("#<% =txtPVOName.ClientID %>").val('');
            $("#<% =txtDisplayType.ClientID %>").val("Summary");

            if ($("#<% =txtPVOName.ClientID %>").val() != "") {
                var pvoName = $("#<% =txtPVOName.ClientID %>").val();
                $("#<% =lblPVOName.ClientID %>").text(pvoName);
            }
            else {
                $("#<% =lblPVOName.ClientID %>").text("");
            }

            $("#<% =lblReportDisplay.ClientID %>").text("CSA Violation Summary");
            $("#lnkDriverSummary").hide();

            $("#<% =pChart2.ClientID %>").hide();

            loadSummaryGrid();
            $("#<% =pChart.ClientID %>").show();
        }

        function displayDetails() {

            DataQuery.FieldList = "AgentCode|PVOCode|PVOName|MonthEndDate|ViolationSection|ViolationDescription|PointType|CSAPoints";  // $("#<% =pChart2.ClientID %>").attr("grid-display-fields");
            DataQuery.QueryType = $("#<% =pChart2.ClientID %>").attr("grid-query-type");
            DataQuery.QueryParamList = getQueryParameters();
            DataQuery.Query = "GetCSASafetyPoints";

            var DTO = { 'DataQuery': DataQuery };

            var dataUri = "../ListHandler.ashx?ct=grid&qt=" + DataQuery.QueryType + "&fld="+DataQuery.FieldList+"&qn=" + DataQuery.Query + "&pl=" + DataQuery.QueryParamList + "&qid=" + S4();

            $(".site-message-section").text(dataUri);
            $("#safetypoints-detail-grid").dataTable({
                "bDestroy": true,
                "sAjaxSource": dataUri,
                "aLengthMenu": [[10, 20, 25], [10, 20, 25]],
                "sPaginationType": "full_numbers",
                "sDom":'r<"H"lf><"datatable-scroll"t><"F"ip>',
                "bDeferRender": true,
                "bJQueryUI": true,
                "bAutoWidth": true
            });

            $(".current-view").val("Detail");
        }

        function loadSummaryGrid() {

            if ($("#<% =txtPVOName.ClientID %>").val() != "") {
                var pvoName = $("#<% =txtPVOName.ClientID %>").val();
                $("#<% =lblPVOName.ClientID %>").text(pvoName);
            }

            DataQuery.FieldList = $("#<% =pChart.ClientID %>").attr("grid-display-fields");
            DataQuery.QueryType = $("#<% =pChart.ClientID %>").attr("grid-query-type");
            DataQuery.QueryParamList = getQueryParameters();
            DataQuery.Query = "GetCSASafetyPointsCharts";

            var DTO = { 'DataQuery': DataQuery };

            var parmList = DataQuery.QueryParamList.replace("DisplayType~Summary", "ChartType~PointsByViolation").replace("DisplayType~Detail", "ChartType~PointsByViolation");
            var dataUri = "../ListHandler.ashx?ct=grid&qt=" + DataQuery.QueryType + "&fld=ViolationSection|ViolationDescription|TotalPoints&qn=" + DataQuery.Query + "&pl=" + parmList + "&qid=" + S4();

            $(".site-message-section").text(dataUri);
            $("#safetypoints-grid").dataTable({
                "bDestroy": true,
                "sAjaxSource": dataUri,
                "aLengthMenu": [[10, 20, 25], [10, 20, 25]],
                "sPaginationType": "full_numbers",
                "sDom": 'r<"H"lf><"datatable-scroll"t><"F"ip>',
                "bDeferRender": true,
                "bJQueryUI": true,
                "bAutoWidth": false,
                "fnRowCallback": function (nRow, aData, iDisplayIndex) {

                    //if (typeof detailLink != 'undefined') {
                    /* Append the grade to the default row class name */
                    $('td:eq(1)', nRow).html("<a href='#' title='View Violation Details' class='details-link' onclick='javascript: getSearchResults(this); return false;'>" + aData[1] + "</a>");
                    //}
                }
            });
        }

        function showDialog() {
            $("#csa-safetypoints-definition-dialog").dialog({ minWidth: 1100,
                    minHeight: 500,
                    modal: false});
            }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding-left:50px;">
        <h1>CSA Violation Types</h1>
        <h2><asp:Label runat="server" ID="lblReportDisplay" Text="CSA Violations Summary" /></h2>
        <h3><asp:Label runat="server" ID="lblPVOName" style="display:inline-block;font-weight:bold;"/></h3>
        <div class="additional-links-section">
            <!--<a class="violations-link" href="../fotemp/sms_appendixa_violationslist.xls" title="CSA Points Definition" target="_blank">Points Definition</a> <br /> 
            <a class="whats-new-link" href="http://csa.fmcsa.dot.gov/Whats_new.aspx" title="What's New: CSA News and Information" target="_blank">What's New: CSA News and Information</a> <br />-->
            <a href="#" id="lnkDriverSummary" onclick="displaySummary(); return false;" style="display:none;">Back to Violations Summary</a>
        </div>
        <div style="width:100%;">
            <asp:Panel runat="server" ID="pChart" class="grid-section" grid-query-type="storedproc" style="width:70%;display:inline-block;" >
                 <table id="safetypoints-grid" class="grid-control" style="width:100%;" >
                    <thead>
                        <tr>
                            <th></th>
                            <th>ViolationSection</th>
                            <th>ViolationDescription</th>
                            <th>TotalPoints</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="pChart2" class="grid-section" grid-query-type="storedproc" style="width:100%;display:none;" >
                <table id="safetypoints-detail-grid" class="grid-control" style="width:100%;" >
                    <thead>
                        <tr>
                            <th></th>
                            <th>AgentCode</th>
                            <th>PVOCode</th>
                            <th>PVOName</th>
                            <th>MonthEndDate</th>
                            <th>ViolationSection</th>
                            <th>ViolationDescription</th>
                            <th>PointType</th>
                            <th>CSAPoints</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <asp:Literal runat="server" ID="litDetailNoData" EnableViewState="false" ></asp:Literal>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
        </div>
        <asp:Label runat="server" ID="lblMessage" CssClass="site-message-section" />
    </div>
    <div class="query-parameters-div">
        <asp:TextBox runat="server" ID="txtAgent" data-field-name="AgentCode" />
        <asp:TextBox runat="server" ID="txtPVOName" data-field-name="PVOName" />
        <asp:TextBox runat="server" ID="txtMonthEndStartDate" data-field-name="MonthEndDate" />
        <asp:TextBox runat="server" ID="txtMonthEndEndDate" data-field-name="MonthEndDate" />
        <asp:TextBox runat="server" ID="txtPointType" data-field-name="PointType" />
        <asp:TextBox runat="server" ID="txtViolationSection" data-field-name="ViolationSection" />
        <asp:TextBox runat="server" ID="txtCompanyID" data-field-name="CompanyID" />
        <asp:TextBox runat="server" ID="txtDisplayType" data-field-name="DisplayType" Text="Summary" />
        <asp:TextBox runat="server" ID="txtUserID" data-field-name="UserID" Text="0" />
    </div>
    </form>
</body>
</html>