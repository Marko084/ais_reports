<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AISDriverTierRanking.ascx.cs" Inherits="AISReports.UserControls.AISDriverTierRanking" %>
<asp:Literal runat="server" ID="litReportStyle" />
<link rel="stylesheet" href="../css/reports/driver-tier-report.css?2" type="text/css" media="screen" />
<link rel="stylesheet" href="https://unpkg.com/purecss@2.0.3/build/pure-min.css" integrity="sha384-cg6SkqEOCV1NbJoCu11+bm0NvBRc8IYLRGXkmNrqUBfTjmMYwNKPWBTIKyw9mHNJ" crossorigin="anonymous">
<style type="text/css">
    .loader-style {
        width:100%;
        text-align:center;
    }
    .loader-style h3 {
        position:relative;
        top:-255px;
        font-size:x-large;
    }
</style>
<script type="text/javascript">

    var loader = "<div class='loader-style'><img src='../images/report-loading-2.gif' /><h3><label>Loading Report...</label></h3></div>";
    $(function () {
        $(".report-body").html(loader);
        $(".report-body").load("../reports/ARSDriverTierRanking.aspx");
    });

    function LoadDriverTierRankingReport(params) {
        var team = GetParamValue(params, "team");
        var quarterdate = GetParamValue(params, "quarterdate");
        var surveystartdate = GetParamValue(params, "surveystartdate");
        var surveyenddate = GetParamValue(params, "surveyenddate");

        if (surveystartdate === "") {
            surveystartdate = "1/1/1900";
        }

        if (surveyenddate === "") {
            surveyenddate = "12/31/9999";
        }

        $(".report-body").html(loader);
        $(".report-body").load("../reports/ARSDriverTierRanking.aspx?q="+quarterdate+"&t="+team+"&ssd="+surveystartdate+"&sed="+surveyenddate);
    }

    function LoadDriverTierRankingDetail(link, pvoName, team,quarterDate) {
        var shortName = $(link).text();
        $("#frame-report-detail").attr("src", "../reports/ARSDriverTierRankingDetail.aspx?pvo=" +pvoName +"&q="+quarterDate);
        $("#report-dialog").dialog({
            width: '90%',
            height: 500,
            title: "Driver Tier Ranking Details for "+shortName,
            resize: function (event, ui) {

            },
            position: {
                  my: "top center",
                  at: "top center",
                  of: "#contentcolumn"
            }
        });
    };

    function GetParamValue(params,paramName) {
        var paramItem = params.split('|').filter(p => p.toLowerCase().indexOf(paramName.toLowerCase()) > -1);
        var result="";

        if (paramItem.length>0) {
            result = paramItem[0].split("~")[1];
        }

        return result;
    }

</script>
<style type="text/css">
    iframe { width:100%; height:100%; border:none;}
</style>
<div style="margin-top:50px;width:98%;">
    <div class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix" style="padding: 5px;">
        <asp:Label runat="server" ID="lblControlTitle" style="display:inline-block;float:left;padding:3px;font-size:larger" Text="Standard Reports" />
    </div>
    <div class="report-section">
        <div class="report-body">
        </div>
    </div>
</div>
<div id="report-dialog" title="Driver Tier Ranking Details" style="display:none;">
    <iframe id="frame-report-detail" width="1200" height="1200">

    </iframe>
</div>