<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChartImage.aspx.cs" Inherits="AISReports.Site.ChartImage" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
        <asp:Chart ID="Chart1" Height="400px" Width="600px" runat="server" palette="SemiTransparent" RenderType="BinaryStreaming" ImageType="Png" ImageLocation="~/TempImages/ChartPic_#SEQ(350,3)"  BackColor="Silver" BackSecondaryColor="White" BackGradientStyle="TopBottom" borderwidth="2" bordercolor="Black" >
        <titles>
            <asp:Title ShadowColor="Transparent" Font="Trebuchet MS, 18.25pt, style=Bold" ShadowOffset="0" Text="Binary Streaming" ForeColor="26, 59, 105"></asp:Title>
        </titles>
        <Series>
            <asp:Series Name="Series1" ChartType="Line" MarkerStyle="Circle" MarkerSize="12" Label="#VALY" >
                
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1" BorderColor="Black" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="White" ShadowColor="Transparent">
            <area3dstyle Rotation="10" perspective="10" enable3d="False" Inclination="0" IsRightAngleAxes="False" wallwidth="0" IsClustered="False">
            </area3dstyle>
            <axisy linecolor="64, 64, 64, 64" Interval="0.5" IsStartedFromZero="true" Minimum="0" Maximum="5" title="Avg. Score">
                <labelstyle font="Trebuchet MS, 14.25pt, style=Bold" ForeColor="26, 59, 105"/>
                <majorgrid linecolor="64, 64, 64, 64"/>
            </axisy>
           <AxisX IsMarginVisible="False" linecolor="64, 64, 64, 64" title="Month" >
                <Labelstyle font="Trebuchet MS, 14.25pt, style=Bold" Interval="1" />
                <majorgrid linecolor="64, 64, 64, 64"/>
            </AxisX>
      
            </asp:ChartArea>
        </ChartAreas>
         <BorderSkin SkinStyle="Emboss" />
    </asp:Chart>
<asp:Image runat="server" Id="imgTest" />