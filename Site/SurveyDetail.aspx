<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveyDetail.aspx.cs" Inherits="AISReports.Site.SurveyDetail" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title runat="Server" id="HeaderTitle"></title>
    <style type="text/css">
        body {font-family:tahoma,arial; font-size:12px;}
        .admin-edit-field {padding:3px;}
        .aisname{font-family:tahoma,arial; font-size:10pt;font-weight:bold;color:#8E3C3E;}
        .aistitle{font-family:tahoma,arial; font-size:14pt;font-weight:bold;}
        .aisdescription{font-family:tahoma,arial; font-size:11pt;}
        .aisquestions{font-family:tahoma,arial; font-size:11pt;font-weight:bold; display:inline-block; vertical-align:top;width:420px;}
        .aisanswers{font-family:tahoma,arial; font-size:11pt; display:inline-block;}
        .aisaddress{font-family:tahoma,arial; font-size:9px;color:#000000;}
        .qcc-survey-form {border: solid 1px #000; font-size:16pt; width:1000px;}
        .qcc-survey-form th  { background-color:#ccc; border: solid 1px #000; }
        .qcc-form-column1 { background-color:#ccc; }
        .qcc-survey-form td {border: solid 1px #000; width:150px !important; text-align:center;}
        .qcc-form-comments-header { width:550px; }
        .qcc-form-comments { width:550px !important; text-align:left !important;}
    </style>
     <script type="text/javascript">
         var win = null;
         function printPage() {

             win = window.open();
             self.focus();
             var domainUrl = document.location.hostname;

             if (document.location.port != "80") {
                 domainUrl = document.location.hostname + ":" + document.location.port
             }

             win.document.open();
             win.document.write('<' + 'html' + '><' + 'head' + '>');
             //win.document.write("<link rel='stylesheet' type='text/css' href='http://" + domainUrl + "/css/ars_survey_detail.css'/>");
             win.document.write('<style>body, div { font-family: Verdana;font-size:8pt;} .print-survey-button{display:none;} ');
             win.document.write('td {font-size: 11pt;} ');
             win.document.write('.aisname{font-family:tahoma,arial; font-size:10pt;font-weight:bold;color:#8E3C3E;} ');
             win.document.write('.aistitle{font-family:tahoma,arial; font-size:14pt;font-weight:bold;} ');
             win.document.write('.aisdescription{font-family:tahoma,arial; font-size:10px;} ');
             win.document.write('.aisquestions{font-family:tahoma,arial; font-size:12px;font-weight:bold; display:inline-block; width:400px;} ');
             win.document.write('.aisanswers{font-family:tahoma,arial; font-size:12px; display:inline-block;margin-left:10px;} ');
             win.document.write('.address{font-family:tahoma,arial; font-size:9px;color:#000000;} ');
             win.document.write('</style>');
             win.document.write('<' + '/' + 'head' + '><' + 'body' + '>');
             win.document.write(document.getElementById('survey-details-section').innerHTML);
             win.document.write('<' + '/' + 'body' + '><' + '/' + 'html' + '>');
             win.document.close();
             win.print();
             win.close();
         }
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="survey-details-section">
        <asp:Literal runat="server" ID="litSurveyDetail" />
    </div>
    <div style="text-align:center;width:98%;">
        <asp:Button runat="server" ID="btnPrint" Text="Print" OnClientClick="javascript:printPage();return false;" />
    </div>
    </form>
</body>
</html>