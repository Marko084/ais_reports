<%@ page language="C#" autoeventwireup="true" inherits="reports_CheckRequest2, App_Web_checkrequest - copy.aspx.dfa151d5" enableeventvalidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style> 
table {
    *border-collapse: collapse; /* IE7 and lower */
    border-spacing: 0;
    width: 100%;    
}

body {font-family:Calibri, Verdana, Tahoma}

.bordered {
    border: solid #ccc 1px;
    -moz-border-radius: 6px;
    -webkit-border-radius: 6px;
    border-radius: 6px;
    -webkit-box-shadow: 0 1px 1px #ccc; 
    -moz-box-shadow: 0 1px 1px #ccc; 
    box-shadow: 0 1px 1px #ccc;         
}

.bordered tr:hover {
    background: #fbf8e9;
    -o-transition: all 0.1s ease-in-out;
    -webkit-transition: all 0.1s ease-in-out;
    -moz-transition: all 0.1s ease-in-out;
    -ms-transition: all 0.1s ease-in-out;
    transition: all 0.1s ease-in-out;     
}    
    
.bordered td, .bordered th {
    border-left: 1px solid #ccc;
    border-top: 1px solid #ccc;
    padding: 10px;
    text-align: left;    
}

.bordered th {
    background-color: #dce9f9;
    background-image: -webkit-gradient(linear, left top, left bottom, from(#ebf3fc), to(#dce9f9));
    background-image: -webkit-linear-gradient(top, #ebf3fc, #dce9f9);
    background-image:    -moz-linear-gradient(top, #ebf3fc, #dce9f9);
    background-image:     -ms-linear-gradient(top, #ebf3fc, #dce9f9);
    background-image:      -o-linear-gradient(top, #ebf3fc, #dce9f9);
    background-image:         linear-gradient(top, #ebf3fc, #dce9f9);
    -webkit-box-shadow: 0 1px 0 rgba(255,255,255,.8) inset; 
    -moz-box-shadow:0 1px 0 rgba(255,255,255,.8) inset;  
    box-shadow: 0 1px 0 rgba(255,255,255,.8) inset;        
    border-top: none;
    text-shadow: 0 1px 0 rgba(255,255,255,.5); 
}

.bordered td:first-child, .bordered th:first-child {
    border-left: none;
}

.bordered th:first-child {
    -moz-border-radius: 6px 0 0 0;
    -webkit-border-radius: 6px 0 0 0;
    border-radius: 6px 0 0 0;
}

.bordered th:last-child {
    -moz-border-radius: 0 6px 0 0;
    -webkit-border-radius: 0 6px 0 0;
    border-radius: 0 6px 0 0;
}

.bordered th:only-child{
    -moz-border-radius: 6px 6px 0 0;
    -webkit-border-radius: 6px 6px 0 0;
    border-radius: 6px 6px 0 0;
}

.bordered tr:last-child td:first-child {
    -moz-border-radius: 0 0 0 6px;
    -webkit-border-radius: 0 0 0 6px;
    border-radius: 0 0 0 6px;
}

.bordered tr:last-child td:last-child {
    -moz-border-radius: 0 0 6px 0;
    -webkit-border-radius: 0 0 6px 0;
    border-radius: 0 0 6px 0;
}



/*----------------------*/

.zebra td, .zebra th {
    padding: 10px;
    border-bottom: 1px solid #f2f2f2;    
}

.zebra tbody tr:nth-child(even) {
    background: #f5f5f5;
    -webkit-box-shadow: 0 1px 0 rgba(255,255,255,.8) inset; 
    -moz-box-shadow:0 1px 0 rgba(255,255,255,.8) inset;  
    box-shadow: 0 1px 0 rgba(255,255,255,.8) inset;        
}

.zebra th {
    text-align: left;
    text-shadow: 0 1px 0 rgba(255,255,255,.5); 
    border-bottom: 1px solid #ccc;
    background-color: #eee;
    background-image: -webkit-gradient(linear, left top, left bottom, from(#f5f5f5), to(#eee));
    background-image: -webkit-linear-gradient(top, #f5f5f5, #eee);
    background-image:    -moz-linear-gradient(top, #f5f5f5, #eee);
    background-image:     -ms-linear-gradient(top, #f5f5f5, #eee);
    background-image:      -o-linear-gradient(top, #f5f5f5, #eee); 
    background-image:         linear-gradient(top, #f5f5f5, #eee);
}

.zebra th:first-child {
    -moz-border-radius: 6px 0 0 0;
    -webkit-border-radius: 6px 0 0 0;
    border-radius: 6px 0 0 0;  
}

.zebra th:last-child {
    -moz-border-radius: 0 6px 0 0;
    -webkit-border-radius: 0 6px 0 0;
    border-radius: 0 6px 0 0;
}

.zebra th:only-child{
    -moz-border-radius: 6px 6px 0 0;
    -webkit-border-radius: 6px 6px 0 0;
    border-radius: 6px 6px 0 0;
}

.zebra tfoot td {
    border-bottom: 0;
    border-top: 1px solid #fff;
    background-color: #f1f1f1;  
}

.zebra tfoot td:first-child {
    -moz-border-radius: 0 0 0 6px;
    -webkit-border-radius: 0 0 0 6px;
    border-radius: 0 0 0 6px;
}

.zebra tfoot td:last-child {
    -moz-border-radius: 0 0 6px 0;
    -webkit-border-radius: 0 0 6px 0;
    border-radius: 0 0 6px 0;
}

.zebra tfoot td:only-child{
    -moz-border-radius: 0 0 6px 6px;
    -webkit-border-radius: 0 0 6px 6px
    border-radius: 0 0 6px 6px
}

.float-left {float:left;}
.float-right {float:right;}
  
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:750px;">
        <div>
            <h2>Check Request Information</h2>
        </div>
        <div style="height:250px;">
            <div class="float-left">
                <table class="bordered" style="width:370px;">
                    <thead>
                        <tr>
                            <th colspan="2">Payee Information</th>
                        </tr>
                    </thead>
                    <tr>
                        <td>Company</td>
                        <td><asp:Label runat="server" ID="lblCompany" /></td>
                    </tr>
                    <tr>
                        <td>Payee Name</td>
                        <td><asp:Label runat="server" ID="lblPayeeName" /></td>
                    </tr>
                    <tr>
                        <td>Payee Address</td>
                        <td><asp:Label runat="server" ID="lblPayeeAddress" /></td>
                    </tr>
                    <tr>
                        <td>Payee City, State, Zip</td>
                        <td><asp:Label runat="server" ID="lblPayeeCityStateZip" /></td>
                    </tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
            
                </table>
            </div>
            <div class="float-right">
            <table class="bordered" style="width:370px;">
                <thead>
                    <tr>
                        <th colspan="2">Details</th>
                    </tr>
                </thead>
                <tr>
                    <td>Registration Number</td>
                    <td><asp:Label runat="server" ID="lblRegistrationNumber" /></td>
                </tr>
                <tr>
                    <td>Requested Date</td>
                    <td><asp:Label runat="server" ID="lblRequestedDate" /></td>
                </tr>
                <tr>
                    <td>Approval Amount</td>
                    <td><asp:Label runat="server" ID="lblApprovalAmount" /></td>
                </tr>
                <tr>
                    <td>Check Number</td>
                    <td><asp:Label runat="server" ID="lblCheckNumber" /></td>
                </tr>
                <tr>
                    <td>Adjustor Name</td>
                    <td><asp:Label runat="server" ID="lblAdjustorName" /></td>
                </tr>
            </table>
            </div>
        </div>
        <div style="height:20px;">&nbsp;</div>
        <div>
            <asp:Table runat="server" ID="tblChargeAmount" CssClass="bordered" Width="400px">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell Text="Account" />
                    <asp:TableHeaderCell Text="Amount" />
                </asp:TableHeaderRow>
            </asp:Table>
            <div style="height:20px;">&nbsp;</div>
            <div style="height:20px;">&nbsp;</div>
            <table class="bordered" style="width:730px;">
                <thead>
                    <tr>
                        <th>Notes</th>
                    </tr>
                </thead>
                <tr>
                    <td>
                     <asp:Label runat="server" ID="lblComments" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
        <asp:Button runat="server" ID="btnExport" Text="Print" OnClick="btnExport_Click" />
    </form>
</body>
</html>
