<%@ Page Title="" Language="C#" MasterPageFile="~/Site/MasterPage.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="AISReports.Site.Dashboard" %>
<%@ Register Src="~/UserControls/SearchFilter.ascx" TagName="SearchFilter" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:SearchFilter ID="SearchFilter1" runat="server"  />
    <asp:Literal runat="server" ID="litScript" EnableViewState="false">
    </asp:Literal>
     <div style="padding-left:40px;" class="grid-container site-page-div">
        <div id="header" class="content-header">
            <asp:PlaceHolder runat="server" ID="phContentHeaderColumn"></asp:PlaceHolder>
        </div>
        <div id="contentwrapper" class="content-main">
            <div id="contentcolumn">
                <div class="innerwrapper">
                    <asp:PlaceHolder runat="server" ID="phContentCenterColumn"></asp:PlaceHolder>
                </div>
            </div>
        </div>
        <div id="leftcolumn" class="content-left">
            <div class="innerwrapper">
                <asp:PlaceHolder runat="server" ID="phContentLeftColumn"></asp:PlaceHolder>
            </div>
        </div>
        <div id="rightcolumn" class="content-right">
            <div class="innerwrapper">
                <asp:PlaceHolder runat="server" ID="phContentRightColumn"></asp:PlaceHolder>
            </div>
        </div>
        <div id="footer" class="content-footer">
             <div class="innerwrapper">
                <asp:PlaceHolder runat="server" ID="phContentFooterColumn"></asp:PlaceHolder>
             </div>
        </div>
     </div>
</asp:Content>
