<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDDocuments.ascx.cs" Inherits="AISReports.UserControls.NCDDocuments" %>
<style type="text/css">
    #report-section {overflow:scroll; background-color:#fff; height: 800px;padding:10px;}
    .doc-list-group {float:left; width: 350px;padding:10px;}
    .doc-list-body {border: 1px solid black;}
    ul {list-style: none;}
    
</style>
<div style="margin-top:50px;width:98%;">
    <div class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix" style="padding: 5px;">
        <asp:Label runat="server" ID="lblSectionTitle" style="display:inline-block;float:left;padding:3px;font-size:larger" Text="Downloadable Documents" />
     </div>
     <div id="report-section">
        <asp:PlaceHolder ID="phDocuments" runat="server" />
     </div>

</div>