<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeficientSurveyComments.aspx.cs" Inherits="AISReports.DeficientSurveyComments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .comments-list-area {font-family: Helvetica Neue, Lucida Grande, Segoe UI, Arial, Helvetica, Verdana, sans-serif; font-size:small;} 
        .logger-info { font-size:smaller;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="comments-list-area">
        <asp:Label runat="server" ID="lblNoCommentsFound" Text="No comments found for this record." Visible="false"/>
        <asp:Repeater runat="server" ID="rpComments">
            <ItemTemplate>
                <b class="comment-id"><%# Eval("SurveyID") %></b>
                <br class="comment-id" />
                <span class="comment-detail"><%# Eval("Comments") %></span>
                <br /><br />
                <span class="logger-info">Logged by <%# Eval("UserName") %> <br />at <%#Eval("CreatedDate")%></span>
            </ItemTemplate>
            <SeparatorTemplate>
                <hr />
            </SeparatorTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
</html>
