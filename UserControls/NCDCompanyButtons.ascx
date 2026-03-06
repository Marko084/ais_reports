<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDCompanyButtons.ascx.cs" Inherits="AISReports.UserControls.NCDCompanyButtons" %>
<script type="text/javascript">
    $(document).ready(function () {
//        if ($(".uid").text() == "24") {
//            $(".siteIconStyle a").each(function () {
//                var uIndex = $(this).attr("href").split("/").length;
//                var page = $(this).attr("href").split("/")[uIndex - 1];

//                $(this).parent().append("<br/>");
//                $(this).parent().append("<a href='../site/" + page + "'>New Layout</a>");
//            });
//        }

//        if ($(".uid").text() == "16") {
//            $(".siteIconStyle a").each(function () {

//                if ($(this).attr("href").indexOf("cid=10003") > -1) {

//                    var uIndex = $(this).attr("href").split("/").length;
//                    var page = $(this).attr("href").split("/")[uIndex - 1];

//                    $(this).parent().append("<br/>");
//                    $(this).parent().append("<a href='../ars/DriverScoreCard.aspx?cid=10003&rtype=bdef'>Old Deficient Score Card - Booker</a>");
//                    $(this).parent().append("<br/>");
//                    $(this).parent().append("<a href='../ars/DriverScoreCard.aspx?cid=10003&rtype=bdefhc'>Old Deficient Score Card - hauler</a>");
//                }
//            });
//        }
    });
</script>
    <div style="padding-top:35px;">
        <div class="siteIconArea">
            <span>Click on one of the company links below:</span>
            <br />
            <br />
            <asp:Literal runat="server" ID="litCompanyLinks" />
        </div>
    </div>