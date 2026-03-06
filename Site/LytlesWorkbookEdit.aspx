<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LytlesWorkbookEdit.aspx.cs" Inherits="AISReports.Site.LytlesWorkbookEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../script/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../script/jquery-ui-1.9.2.custom.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../css/redmond/jquery-ui-1.9.2.custom.min.css" />
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.css" />
    <link href='https://fonts.googleapis.com/css?family=Convergence|Bitter|Droid+Sans|Ubuntu+Mono' rel='stylesheet' type='text/css' />
    <style type="text/css">
        body {font-family:Lucida Grande, Lucida Sans, Arial, sans-serif;}
        td,span {font-size:17.6px;}
        /*td,input {margin:3px;}*/
        .date-section {width:190px;margin:7px;}
    </style>
    <script type="text/javascript">
        $(function () {
            $("#btnPrintJob").button();
            $("#btnSaveJob").button();
            $("#btnClearJob").button();
            $("#btnDeleteJob").button();
            $("#btnDeleteJob").css("color", "red");
        });

        function EmailMissingConfirmation() {
            var result= true;

            if ($("#EmailAddress").val().length == 0) {
              result=confirm("The Email address field has not been filled out.  Do you want to continue?");
            }

            return result;
        }

        function ContactCheckBoxChanged(ele) {

            if ($(ele).find("input").prop("checked")) {
                var currentUser = $("#hdnEditedBy").val();
                var msg = currentUser + " checked " + $(ele).attr("rel") + " on " + GetTimeStamp() + "\r\n";
                msg += $("#ContactedTransfereeNotes").val();

                $("#ContactedTransfereeNotes").val(msg);
                $("#hdnContactedTransfereeNotes").val(msg);
            }
        }

        function GetTimeStamp() {
            // Create a date object with the current time
            var now = new Date();

            // Create an array with the current month, day and time
            var date = [now.getMonth() + 1, now.getDate(), now.getFullYear()];

            // Create an array with the current hour, minute and second
            var time = [now.getHours(), now.getMinutes(), now.getSeconds()];

            // Determine AM or PM suffix based on the hour
            var suffix = (time[0] < 12) ? "AM" : "PM";

            // Convert hour from military time
            time[0] = (time[0] < 12) ? time[0] : time[0] - 12;

            // If hour is 0, set it to 12
            time[0] = time[0] || 12;

            // If seconds and minutes are less than 10, add a zero
            for (var i = 1; i < 3; i++) {
                if (time[i] < 10) {
                    time[i] = "0" + time[i];
                }
            }

            // Return the formatted string
            return date.join("/") + " " + time.join(":") + " " + suffix;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="workbook-item-detail" style="padding:10px;">
             <table>
                 <tr>
                     <td colspan="3">
                         <span>Office Assigned:</span>
                         <asp:DropDownList runat="server" ID="Offices" CssClass="ui-widget"></asp:DropDownList>
                     </td>
                     <td colspan="3">
                         <span>Account:</span>
                         <asp:DropDownList runat="server" ID="Accounts" CssClass="ui-widget"></asp:DropDownList>
                     </td>
                 </tr>
                 <tr>
                     <td><span>PPWK Complete:</span></td>
                     <td><asp:DropDownList runat="server" ID="PPWKStatuses" CssClass="ui-widget">
                            <asp:ListItem Value="N/A" Text="N/A">N/A</asp:ListItem>
                            <asp:ListItem Value="Not Completed" Text="Not Completed">Not Completed</asp:ListItem>
                            <asp:ListItem Value="Completed" Text="Completed">Completed</asp:ListItem>
                            <asp:ListItem Value="" Text=""></asp:ListItem>
                         </asp:DropDownList></td>
                     <td class="separator"></td>
                     <td><span>Trailer</span></td>
                     <td><asp:DropDownList runat="server" ID="Trailers" CssClass="ui-widget"></asp:DropDownList>
                     </td>
                 </tr>
                 <tr>
                     <td><span>Reg Number</span></td>
                     <td><asp:TextBox runat="server" ID="RegNumber" CssClass="ui-widget" /></td>
                     <td class="separator"></td>
                     <td><!--<span>Account</span>--></td>
                     <td><!--<input type="text" data-field-name="Account" aria-data-type="text" />--></td>
                 </tr>
                 <tr>
                     <td><span>Client Name</span></td>
                     <td><asp:TextBox runat="server" ID="Name" CssClass="ui-widget" /></td>
                     <td class="separator"></td>
                     <td><span>Move Coordinator</span></td>
                     <td><asp:DropDownList runat="server" ID="MoveAgents" CssClass="ui-widget"></asp:DropDownList></td>
                 </tr>
                  <tr>
                     <td style="vertical-align:top">
                         <span>Email Address</span><br />
                         <span>Pickup Location</span><br />
                         <span>Delivery Location</span>
                     </td>
                     <td style="vertical-align:top">
                         <asp:TextBox runat="server" TextMode="Email" ID="EmailAddress" CssClass="ui-widget"/><br />
                         <asp:TextBox runat="server" ID="PickupLocation" CssClass="ui-widget" /><br />
                         <asp:TextBox runat="server" ID="DeliveryLocation" CssClass="ui-widget"/>
                     </td>
                     <td class="separator"></td>
                     <td style="vertical-align:top;"><span>Detail</span></td>
                     <td><asp:TextBox runat="server" TextMode="MultiLine" Width="30" Height="3" cols="30" ID="Details" style="width:400px;height:60px;" CssClass="ui-widget"></asp:TextBox></td>
                 </tr>
                  <tr>
                     <td><span>Weight</span></td>
                     <td><asp:TextBox runat="server" ID="Weight" CssClass="ui-widget"/></td>
                     <td class="separator"></td>
                     <td>Notes</td>
                     <td>
                         <asp:TextBox runat="server" TextMode="MultiLine" Height="3" Width="60" ID="ContactedTransfereeNotes" CssClass="contacted-transferee-notes ui-widget" style="font-size:10px;width:400px;height:60px;" Enabled="false"></asp:TextBox>
                         <asp:HiddenField runat="server" ID="hdnContactedTransfereeNotes" />
                     </td>
                 </tr>
                 <tr>
                     <td colspan="2">
                         <span style="width:212px;margin-top:3px;">Shipment Cancelled</span>
                         <asp:CheckBox runat="server" ID="Cancelled" CssClass="ui-widget"/>
                         <br />
                         <span style="width:212px;margin-top:3px;">Shipment Delivered</span>
                         <asp:CheckBox runat="server" ID="ShipmentDelivered" CssClass="ui-widget"/>
                         <br />
                         <span style="width:212px;margin-top:3px;">CSR Contacted Packing</span>
                         <asp:CheckBox runat="server" ID="CSRContactedTransfereePK" aria-data-type="text" rel="Packing" onchange="javascript:ContactCheckBoxChanged(this);" CssClass="ui-widget"/>
                         <br />
                         <span style="width:212px;margin-top:3px;">CSR Contacted Loading</span>
                         <asp:CheckBox runat="server" ID="CSRContactedTransfereeLD" aria-data-type="text" rel="Loading" onchange="javascript:ContactCheckBoxChanged(this);" CssClass="ui-widget"/>
                         <br />
                         <span style="width:212px;margin-top:3px;">CSR Contacted Delivery</span>
                         <asp:CheckBox runat="server" ID="CSRContactedTransfereeDEL" aria-data-type="text" rel="Delivery" onchange="javascript:ContactCheckBoxChanged(this);" CssClass="ui-widget"/>
                     </td>
                     <td class="separator"></td>
                     <td></td>
                     <td>
                         <div style="display:inline-block;width:220px;">
                            <span>Origin Driver</span>
                         </div>
                         <div style="display:inline-block;width:220px;">
                            <span>Dest. Driver</span>
                         </div>
                         <br />
                         <div style="display:inline-block;border:1px solid #000;">
                            <asp:ListBox runat="server" ID="OriginDriverNames" SelectionMode="Multiple" Height="172" Width="220" CssClass="ui-widget"></asp:ListBox>
                         </div>
                         <div style="display:inline-block;border:1px solid #000;">
                            <asp:ListBox runat="server" ID="DestinationDriverNames" SelectionMode="Multiple"  Height="172" Width="220" CssClass="ui-widget"></asp:ListBox>
                         </div>
                     </td>
                 </tr>
                  <tr>
                     <td>
                         <div class="date-section">
                            <span>Pack Start Date</span>
                         </div>
                         <div class="date-section">
                            <span>Pack End Date</span>
                         </div>
                         <div class="date-section">
                            <span>Load Start Date</span>
                         </div>
                         <div class="date-section">
                            <span>Load End Date</span>
                         </div>
                         <div class="date-section">
                            <span>Delivery Start Date</span>
                         </div>
                         <div class="date-section">
                            <span>Delivery End Date</span>
                         </div>
                     </td>
                     <td>
                         <asp:TextBox runat="server" ID="PKStartDate" TextMode="Date" aria-data-type="datetime" CssClass="ui-widget" /><br />
                         <asp:TextBox runat="server" ID="PKEndDate" TextMode="Date" aria-data-type="datetime" CssClass="ui-widget" /><br />
                         <asp:TextBox runat="server" ID="LDStartDate" TextMode="Date" aria-data-type="datetime" CssClass="ui-widget" /><br />
                         <asp:TextBox runat="server" ID="LDEndDate" TextMode="Date" aria-data-type="datetime" CssClass="ui-widget" /><br />
                         <asp:TextBox runat="server" ID="DELStartDate" TextMode="Date" aria-data-type="datetime" CssClass="ui-widget" /><br />
                         <asp:TextBox runat="server" ID="DELEndDate" TextMode="Date" aria-data-type="datetime" CssClass="ui-widget" />
                     </td>
                     <td class="separator"></td>
                     <td style="vertical-align:top;"></td>
                     <td>
                         <div style="display:inline-block;width:220px;">
                             <span>Origin Helper(s)</span>
                         </div>
                         <div style="display:inline-block;width:220px;">
                             <span>Dest. Helper(s)</span>
                         </div>
                         <br />
                         <div style="display:inline-block;border:1px solid #000;">
                             <asp:ListBox runat="server" ID="OriginHelperNames" SelectionMode="Multiple" Height="172" Width="220" CssClass="ui-widget">
                             </asp:ListBox>
                         </div>
                         <div style="display:inline-block;border:1px solid #000;">
                             <asp:ListBox runat="server" ID="DestinationHelperNames" SelectionMode="Multiple" Height="172" Width="220" CssClass="ui-widget">
                             </asp:ListBox>
                         </div>
                     </td>
                 </tr>
             </table>
            <div>
                <div style="width:38%; padding:5px; display:inline-block;">
                    <asp:LinkButton runat="server" ID="btnDeleteJob" UseSubmitBehavior="false" OnClientClick="return confirm('Are you sure you want to delete?');" OnClick="btnDeleteJob_Click">Delete</asp:LinkButton>
                </div>
             <div style="width:59%; padding:5px;text-align:right;padding:5px; display:inline-block">
                  <asp:LinkButton runat="server" ID="btnPrintJob" rel="0" OnClientClick="javascript:window.parent.PrintJob(this);">Print</asp:LinkButton>
                  <asp:LinkButton runat="server" ID="btnSaveJob" UseSumbitBehavior="false" OnClientClick="return EmailMissingConfirmation();" OnClick="btnSaveJob_Click">Submit</asp:LinkButton>
                  <asp:LinkButton runat="server" ID="btnClearJob" Onclick="btnClearJob_Click" OnClientClick="javascript:window.parent.CloseEditDialog('none','','');">Reset</asp:LinkButton>
             </div>
             </div>
        </div>
        <asp:HiddenField ID="hdnEditedBy" runat="server" />
    </form>
</body>
</html>
