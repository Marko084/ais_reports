<%@ Page Title="" Language="C#" MasterPageFile="~/Site/MasterPage.Master" AutoEventWireup="true" CodeBehind="SurveyResults.aspx.cs" Inherits="AISReports.Site.SurveyResults" %>
<%@ Register Src="~/UserControls/SearchFilter.ascx" TagName="SearchFilter" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:SearchFilter ID="SearchFilter1" runat="server"  />
    <asp:Literal runat="server" ID="litScript" EnableViewState="false">
    </asp:Literal>
    <div style="padding-left:40px;">
        <div id="header">
            <asp:PlaceHolder runat="server" ID="phContentHeaderColumn"></asp:PlaceHolder>
        </div>
        <div id="contentwrapper">
            <div id="contentcolumn">
                <div class="innerwrapper">
                    <asp:PlaceHolder runat="server" ID="phContentCenterColumn"></asp:PlaceHolder>
                </div>
            </div>
        </div>
        <div id="leftcolumn">
            <div class="innerwrapper">
                <asp:PlaceHolder runat="server" ID="phContentLeftColumn"></asp:PlaceHolder>
            </div>
        </div>
        <div id="rightcolumn">
            <div class="innerwrapper">
                <asp:PlaceHolder runat="server" ID="phContentRightColumn"></asp:PlaceHolder>
            </div>
        </div>
        <div id="footer">
            <asp:PlaceHolder runat="server" ID="phContentFooterColumn"></asp:PlaceHolder>
        </div>
    </div>
</asp:Content>
