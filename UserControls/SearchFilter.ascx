<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchFilter.ascx.cs" Inherits="AISReports.UserControls.SearchFilter" %>
<style type="text/css">
    .ui-tabs-hide { display: none !important; }
    .search-criteria-header {font-size:16pt !important; font-weight:bold; color:#fff; display:block;width:100%;}
    #searchFilterTabs { 
    padding: 0px; 
    background: none; 
    border-width: 0px; 
    } 
    #searchFilterTabs .ui-tabs-nav { 
        padding-left: 0px; 
        background: transparent; 
        border-width: 0px 0px 1px 0px; 
        -moz-border-radius: 0px; 
        -webkit-border-radius: 0px; 
        border-radius: 0px; 
    } 
    #searchFilterTabs .ui-tabs-panel { 
        background: #transparent repeat-x scroll 50% top; 
        border-width: 0px 1px 1px 1px; 
        color:#000;
    }
    .show-default-report {
        display: none;
    }
  
</style>
<script type="text/javascript">
    var arsClaimReportTF = false;
    var CallTrackerPageTF = false;
    var InvoiceReleasingPageTF = false;

    $(document).ready(function () {
        SetSearchFilterCheckBoxStatuses();
        GetSearchFilterCheckBoxStatuses();

        CallCenterPageTF = document.location.href.toLowerCase().indexOf("calltracker.aspx") > -1;
        InvoiceReleasingPageTF =document.location.href.toLowerCase().indexOf("invoicereleasing.aspx") > -1;

        buildQueryParamList();

        if ((cid == "10064" || cid == "10003") && document.location.href.toLowerCase().indexOf("qualitycontrolreport")>-1) {
            $("#<% =btnPostBackSearch.ClientID %>").attr("use-postback-search", "true");
            arsClaimReportTF = true;
        }

        if ($("#<% =btnPostBackSearch.ClientID %>").attr("use-postback-search") == "true" || document.location.href.toLowerCase().indexOf("adminreports") > -1) {
            $("#btnSearch").hide();
            $("#<% =btnPostBackSearch.ClientID %>").show();
            $("#<% =btnPostBackSearch.ClientID %>").button();
        }
        else {
            $("#<% =btnPostBackSearch.ClientID %>").hide();
            $("#btnSearch").show();
            $("#btnSearch").button();
        }

        var searchCtrls = $(".slide-out-div input[type=text]").length;
        searchCtrls = searchCtrls + $(".slide-out-div input[type=checkbox]").length;

        if (searchCtrls == 0) {
           $(".slide-out-div").hide();
        }

        $("#searchFilterTabs").tabs();

        setReportOnlyFields($("#ctl00_ContentPlaceHolder1_SearchFilter1_txtreportname").val());

        try {
            if ($(".show-default-report").val() == "True" && arsClaimReportTF==false) {
                $(".show-default-report").val("False");
                $("#ctl00_ContentPlaceHolder1_SearchFilter1_btnPostBackSearch").click();
            }
        }
        catch (e) {
            alert(e.Message);
        }

        hideSearchCriteriaSection();
        $("#btnLytlesSearch").button();
        $("#ctl00_ContentPlaceHolder1_SearchFilter1_txtcustomdriverstf").show();
        $("#ctl00_ContentPlaceHolder1_SearchFilter1_txtnoncustomdriverstf").show();

        if (CallCenterPageTF || InvoiceReleasingPageTF) {
            $("#btnSearch").hide();
            $("#<% =btnPostBackSearch.ClientID %>").hide();
            $("#btnFilter").show();
            $("#btnFilter").button();
        }

    });

    if (typeof String.prototype.endsWith !== 'function') {
        String.prototype.endsWith = function(suffix) {
            return this.indexOf(suffix, this.length - suffix.length) !== -1;
        };
    }

    function getResult() {
        if (window.location.href.toLowerCase().indexOf("driversafetypoints") > -1) {
            DataQuery.QueryParamList = getQueryParametersDriverSafetyPoints();
        }
        else {
            DataQuery.QueryParamList = getQueryParameters();
        }
        //alert(DataQuery.QueryParamList);
       // GenerateXamlCharts();
        GenerateFusionCharts();
        GenerateGrids();
        $(".handle").click();
    }

    function getResultByFilter() {
        buildQueryParamList();

       // GenerateXamlCharts();
        GenerateFusionCharts();
        GenerateGrids();
    }

    function getQueryParametersDriverSafetyPoints() {
        var queryParams = "";
        var paramName = "";
        var paramValue = "";
        try {
            $(".slide-out-div input").each(function () {
                //try {

                if ($(this).attr("type") == "checkbox" || $(this).attr("type") == "text") {
                    paramName = $(this).attr("data-parameter-name");

                    if ($(this).attr("type") == "checkbox") {
                        paramValue = $(this).is(":checked");
                    }
                    else {
                        paramValue = $(this).val();
                    }

                    //                if (paramName.toLowerCase().endsWith("enddate") && paramValue == "") {
                    //                    paramValue = "6/5/2079";
                    //                }
                    //                else if (paramName.toLowerCase().endsWith("startdate") && paramValue == "") {
                    //                    paramValue = "1/1/1900";
                    //                }

                    if (queryParams.length == 0 && paramName.toLowerCase() == "question") {
                        queryParams = paramName + "~" + encodeURIComponent(paramValue.split(",")[0]);
                    }
                    else if (queryParams.length > 0 && paramName.toLowerCase() == "question") {
                        queryParams += "|" + paramName + "~" + encodeURIComponent(paramValue.split(",")[0]);
                    }
                    else if (queryParams.length == 0) {
                        queryParams = paramName + "~" + encodeURIComponent(paramValue);
                    }
                    else {
                        queryParams += "|" + paramName + "~" + encodeURIComponent(paramValue);
                    }
                    //}
                    //            catch (e) {
                    //                alert(paramName + ":  " + e.Message);
                    //            }
                }
            });

            if (queryParams.length == 0) {
                queryParams = queryParams + "UserID~" + uid;
            }
            else {
                queryParams = queryParams + "|UserID~" + uid;
            }

            queryParams = queryParams + "|CompanyID~" + cid;

            if (typeof (consolidated) !== "undefined") {
                queryParams = queryParams + "|ConsolidatedTF~" + consolidated;
            }
        }
        catch (e) {
            alert(e.Message);
        }
        return queryParams;
    }

    function getQueryParameters() {
        var queryParams = "";
        var paramName = "";
        var paramValue = "";
        try
        {
            $(".slide-out-div input").each(function () {
                //try {

                if ($(this).attr("type") == "checkbox" || $(this).attr("type") == "text") {

                    //if ($(this).attr("type") == "checkbox") {
                       // paramName = $(this).parent().attr("data-parameter-name");
                   // }
                   // else {
                        paramName = $(this).attr("data-parameter-name");
                    //}

                    if ($(this).attr("type") == "checkbox") {
                        paramValue = $(this).is(":checked");
                    }
                    else {
                        paramValue = $(this).val();
                    }

                    //                if (paramName.toLowerCase().endsWith("enddate") && paramValue == "") {
                    //                    paramValue = "6/5/2079";
                    //                }
                    //                else if (paramName.toLowerCase().endsWith("startdate") && paramValue == "") {
                    //                    paramValue = "1/1/1900";
                    //                }

                    if (queryParams.length == 0 && paramName.toLowerCase() == "question") {
                        queryParams = paramName + "~" + encodeURIComponent(paramValue.split(",")[0]);
                    }
                    else if (queryParams.length > 0 && paramName.toLowerCase() == "question") {
                        queryParams += "|" + paramName + "~" + encodeURIComponent(paramValue.split(",")[0]);
                    }
                    else if (queryParams.length == 0) {
                        queryParams = paramName + "~" + encodeURIComponent(paramValue);
                    }
                    else {
                        queryParams += "|" + paramName + "~" + encodeURIComponent(paramValue);
                    }
                    //}
                    //            catch (e) {
                    //                alert(paramName + ":  " + e.Message);
                    //            }
                }
            });

            if (queryParams.length == 0) {
                queryParams = queryParams + "UserID~" + uid;
            }
            else {
                queryParams = queryParams + "|UserID~" + uid;
            }
        
            queryParams = queryParams + "|CompanyID~" + cid;

            if (typeof (consolidated) !== "undefined") {
                queryParams = queryParams + "|ConsolidatedTF~" + consolidated;
            }
        }
        catch(e)
        {
            console.log("Search Filter: " +e);
        }

        return queryParams;
    }

    function getQueryParametersQueryString() {
        var queryParams = "";
        var paramName = "";
        var paramValue = "";
        
//        if (paramName.toLowerCase().endsWith("enddate")) {
//            paramValue = "6/5/2079";
//        }
//        else if (paramName.toLowerCase().endsWith("startdate")) {
//            paramValue = "1/1/1900";
//        }

        $(".slide-out-div input:text").each(function () {
            paramName = $(this).attr("data-parameter-name");
            paramValue = $(this).val();

            if (queryParams.length == 0) {
                queryParams = paramName + "=" + paramValue;
            }
            else {
                queryParams += "&" + paramName + "=" + paramValue;
            }

            if (CallCenterPageTF || InvoiceReleasingPageTF) {
                switch(paramName) {
                  case "completionstartdate":
                        CompletionStartDate = new Date(paramValue === "" ? "1/1/1900" : paramValue);
                    break;
                  case "completionenddate":
                        CompletionEndDate = new Date(paramValue === "" ? "12/31/9999" : paramValue);
                        break;
                  case "deliveryenddate":
                        DeliveryEndDate = new Date(paramValue === "" ? "12/31/9999" : paramValue);
                        break;
                  case "deliverystartdate":
                        DeliveryStartDate = new Date(paramValue === "" ? "1/1/1900" : paramValue);
                        break;
                    case "loadenddate":
                        DeliveryEndDate = new Date(paramValue === "" ? "12/31/9999" : paramValue);
                        break;
                    case "loadstartdate":
                        DeliveryStartDate = new Date(paramValue === "" ? "1/1/1900" : paramValue);
                        break
                    case "aisreleasedenddate":
                        AISReleasedEndDate = new Date(paramValue === "" ? "12/31/9999" : paramValue);
                        break;
                    case "aisreleasedstartdate":
                        AISReleasedStartDate = new Date(paramValue === "" ? "1/1/1900" : paramValue);
                        break
                  default:
                    // code block
                }
            }
        });

        if (CallCenterPageTF || InvoiceReleasingPageTF) {
            queryParams = queryParams + "&Status=" + statusFilter;
        }

        if (queryParams.length == 0) {
            queryParams = queryParams + "UserID=" + uid;
        }
        else {
            queryParams = queryParams + "&UserID=" + uid;
        }

        queryParams = queryParams + "&CompanyID=" + cid;

        if (typeof (consolidated) !== "undefined") {
            queryParams = queryParams + "&ConsolidatedTF~" + consolidated
        }

       //alert(queryParams);
        return queryParams;
    }

    function getSelectionCriteria() {
        $(".report-query-params").val(getQueryParameters());
    }

    function buildQueryParamList() {
        try {
            DataQuery.QueryParamList = getQueryParameters();

            if (CallCenterPageTF) {
                
                DataQuery.QueryParamList = getQueryParametersQueryString();
                //console.log(DataQuery.QueryParamList);
            }
            else if (InvoiceReleasingPageTF) {
                DataQuery.FilterList = GetWhereClauseFilter();
                DataQuery.QueryParamList = getQueryParametersQueryString();
            }
        }
        catch (e) {
            console.log("buildQueryParamList Error: "+e);
        }
    }

    function setReportOnlyFields(value) {
        var rptName = value;

        if ($(".slide-out-div input[report-param-show='hide']").length > 0) {
            //do nothing.
        }
        else if($(".slide-out-div input[report-param-show*='" + rptName + "']").length>0)
        {
            $(".slide-out-div input[report-param-show='']").each(function () {
                if ($(this).is(":visible") &&
                    ($(this).attr("id").toLowerCase().indexOf("reportname") == -1 &&
                    $(this).attr("id").toLowerCase().indexOf("btnsearch") == -1 &&
                    $(this).attr("id").toLowerCase().indexOf("btnpostbacksearch") == -1)) {
                    $(this).hide();
                    $(this).val("");
                    $(this).prev().hide();
                }
            });

            $(".slide-out-div input[report-param-show*='" + rptName + "']").each(function () {

                if (!$(this).is(":visible")) {
                    $(this).show();
                    $(this).prev().show();
                }
            });
        }
        else
        {
            $(".slide-out-div input[report-param-show='none']").each(function () {
                if (!$(this).is(":visible") &&
                    ($(this).attr("id").toLowerCase().indexOf("reportname")==-1 &&
                    $(this).attr("id").toLowerCase().indexOf("btnsearch") == -1 &&
                    $(this).attr("id").toLowerCase().indexOf("btnpostbacksearch") == -1)) {
                        $(this).show();
                        $(this).prev().show();

                }
            });

            $(".slide-out-div input[report-param-show!='none']").each(function () {
                if ($(this).is(":visible") &&
                    ($(this).attr("id").toLowerCase().indexOf("reportname") == -1 &&
                    $(this).attr("id").toLowerCase().indexOf("btnsearch") == -1 &&
                    $(this).attr("id").toLowerCase().indexOf("btnpostbacksearch") == -1)) {
                    $(this).hide();
                    $(this).val("");
                    $(this).prev().hide();

                    if ($(this).attr("class") == "truefalse-checkbox") {
                        $(this).find("input").hide();
                    }

                }
            });
        }

        $("#ctl00_ContentPlaceHolder1_SearchFilter1_txtcustomdriverstf").show();
        $("#ctl00_ContentPlaceHolder1_SearchFilter1_txtnoncustomdriverstf").show();
    }

    function hideSearchCriteriaSection()
    {
        var showSection = false;

        $(".slide-out-div input").each(function (idx, ele) {
            if($(ele).is(":visible"))
            {
                showSection = true;
            }
        });

        if (!showSection) {
            $(".handle").hide();
        }
    }

        function FilterGrid() {
            buildQueryParamList();

            if (InvoiceReleasingPageTF) {
                LoadInvoiceReleasingData(DataQuery.FilterList);
            }
            else {
                oEditGrid.draw();
            }

    }

    function ClickEventTest() {
        alert("Hello!");
    }

    function GetWhereClauseFilter() {
        var filter = "";

        $(".slide-out-div input:text").each(function () {
            paramName = $(this).attr("data-parameter-name");
            paramValue = $(this).val();

            if (paramName.toLowerCase().indexOf("startdate") > 0 && paramValue !== "" && filter.length===0) {
                filter += paramName.replace("start", "") + " >= '" + paramValue + "' "
            }
            else if (paramName.toLowerCase().indexOf("startdate") > 0 && paramValue !== "") {
                filter += " and "+ paramName.replace("start", "") + " >= '" + paramValue + "' "
            }
            else if (paramName.toLowerCase().indexOf("enddate") > 0 && paramValue !== "" && filter.length === 0) {
                filter += paramName.replace("end", "") + " <= '" + paramValue + "' ";
            }
            else if (paramName.toLowerCase().indexOf("enddate") > 0 && paramValue !== "" && filter.length > 0) {
                filter += " and " + paramName.replace("end", "") + " <= '" + paramValue + "' ";
            }
            else if (filter.length === 0 && paramValue !== ""){
                filter +=paramName + "='" + paramValue + "' ";
            }
            else if (paramValue !== "" && filter.length>0){
                filter += filter +" and "+ paramName + "='" + paramValue + "' ";
            }
        });

        if (filter.length > 0) {
            filter = " where " + filter;
        }

        return filter;
    }
</script>
<div class="slide-out-div">
    <a class="handle" href="javascript:void(0)">Content</a>
    <span class="search-criteria-header">Search Criteria</span>
    <asp:PlaceHolder runat="server" ID="phFilterControls">
    </asp:PlaceHolder>
   
    <br /><br />
    <a href="#" id="btnSearch" onclick="javascript: getResult(); return false;">Search</a>
    <a href="javascript:void(0);" id="btnFilter" onclick="javascript:FilterGrid();return false;" style="display:none;">Filter</a>
    <asp:Button runat="server" ID="btnPostBackSearch" OnClick="btnPostBackSearch_Click" Text="Go" OnClientClick="javascript: getSelectionCriteria();" />
    <a href="javascript:void(0);" id="btnLytlesSearch" onclick="javascript: GetLytlesSelectionCriteria();" style="display:none;">Search</a>
</div>
<asp:TextBox runat="server" ID="txtDisplayDefaultReport" CssClass="show-default-report" />
<asp:TextBox runat="server" ID="txtCheckBoxStatuses" CssClass="checkbox-statuses" style="display:none;"/>
<asp:HiddenField ID="hdnSearchCriteriaSelectedTab" runat="server" Value="1" />
<script type="text/javascript">

    setDateTabs();

    function setDateTabs() {
        var tabDiv = $("<div id='searchFilterTabs'></div>");
        var ul = $("<ul></ul>");
        var tabCounter = 1;
        var indexCounter = 0;
        var foundFields = "";
        var dateFields = new Array();

        if ($(".date-textbox").length > 2) {
            $(".date-textbox").each(function () {
                if (foundFields.indexOf($(this).attr("data-field-name")) == -1) {
                    var tabName = $(this).prev().text().replace(/start/i, "").replace(/end/i, "");
                    ul.append("<li><a href='#searchFilterTabs-" + tabCounter.toString() + "' style='font-size:xx-small;' onclick='javascript:clearDates("+tabCounter+");'>" + tabName + "</a></li>");

                    foundFields += $(this).attr("data-field-name");
                    tabCounter++;
                }

                dateFields[indexCounter] = $(this).attr("id")+"|"+tabCounter;
                indexCounter++;

            });

            tabDiv.append(ul);

            tabCounter = 0;

           for (i = 0; i < dateFields.length; i++) {

                var sectionDiv;

                if (i % 2 == 0) {
                    tabCounter++;
                    sectionDiv = $("<div id='searchFilterTabs-" + tabCounter.toString() + "' class='search-filter-tab'></div>");
                }

                var dateField = $("#" + dateFields[i].split("|")[0]);

                $(dateField).prev().appendTo(sectionDiv);
                $(dateField).appendTo(sectionDiv);

                tabDiv.append(sectionDiv);
            }

            $(".slide-out-div > span:contains('Search Criteria')").after(tabDiv);
        }
    }

    function clearDates(index) {
        $(".search-filter-tab").each(function () {
            if ($(this).attr("id") != "searchFilterTabs-" + index) {
                $("#" + $(this).attr("id") + " input[type=text]").each(function () {
                    $(this).val("");
                });
                $("#<%= hdnSearchCriteriaSelectedTab.ClientID %>").val(index);
            }
        });
    }

    function GetSearchFilterCheckBoxStatuses() {

        var checkBoxResult = "";
        var checkBoxFieldName;
        var checkBoxChecked;

        $(".slide-out-div input[type='checkbox']").each(function (i, item) {
            checkBoxFieldName = $(item).attr("data-field-name");
            checkBoxChecked = $(item).is(":checked");

            if (checkBoxResult.length == 0) {
                checkBoxResult = checkBoxFieldName + "~" + checkBoxChecked;
            }
            else {
                checkBoxResult += "|"+checkBoxFieldName + "~" + checkBoxChecked;
            }
            

        });

        $(".checkbox-statuses").val(checkBoxResult);

    }

    function SetSearchFilterCheckBoxStatuses() {

        var checkBoxData = $(".checkbox-statuses").val().trim();
        var checkBoxResult = "";
        var cbxFields = checkBoxData.split("|");
        var checkBoxFieldName;
        var checkBoxChecked;

        if (checkBoxData.length > 0) {
            $.each(cbxFields, function (i, item) {
                checkBoxFieldName = item.split("~")[0];
                checkBoxChecked = JSON.parse(item.split("~")[1]);
                
                $(".slide-out-div input[data-field-name='" + checkBoxFieldName + "']").prop("checked", checkBoxChecked);

                if (checkBoxResult.length == 0) {
                    checkBoxResult = checkBoxFieldName + "~" + checkBoxChecked;
                }
                else {
                    checkBoxResult += "|" + checkBoxFieldName + "~" + checkBoxChecked;
                }

            });
        }

        $(".checkbox-statuses").val(checkBoxResult);

    }

</script>
