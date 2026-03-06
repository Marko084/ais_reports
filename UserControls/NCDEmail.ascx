<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDEmail.ascx.cs" Inherits="AISReports.UserControls.NCDEmail" %>
<style type="text/css">
    .emailer-section span {display:block;}
    .emailer-section a {float:right;}
    .emailer-section a span {text-align:left;}

</style>
<div class="user-control-widget" style="display:none;">
    <div id="email-dialog" title="Control Title">
        <asp:Panel runat="server" ID="pChart" CssClass="emailer-section">
             <div>
                <span>To:</span>
                <asp:TextBox runat="server" ID="txtToName" Columns="50" CssClass="to-name" ReadOnly="true" /><br />
                <asp:TextBox runat="server" ID="txtToAddress" Columns="50" CssClass="to-address" style="display:none;"  />
                <span>From:</span>
                <asp:TextBox runat="server" ID="txtFromAddress" Columns="50" CssClass="from-address"  />
                <span>Subject:</span>
                <asp:TextBox runat="server" ID="txtEmailSubject" Columns="50" CssClass="email-subject"  />
                <span>Message:</span>
                <asp:TextBox runat="server" ID="txtMessage" TextMode="MultiLine" Rows="15" Columns="50" CssClass="email-message" />
                 <asp:Label runat="server" ID="lblEmailBody" CssClass="default-email-body" style="display:none;" />
                 <span>Attachment:</span>
                 <input type="file" id="fupEmailAttachmentUpload">
                <br />
                <br />
                <span id="queryTest" style="color:white;"></span>
                 <img id="loader" style="display:none;" width="20" src="../images/loadingGif.gif" />
                <a href="javascript:void(0);" id="txtSendEmail" onclick="javascript:SendEmail();" style="width:62px !important;">Send</a>
            </div> 
            <div style="display:block;width:300px; height:40px;">
                <span id="lblMessage" class="error-msg ui-state-error ui-corner-all" style="display:block; padding:3px; font-weight:normal;"></span>
            </div>
        </asp:Panel>
    </div>
</div>
<script type="text/javascript">
    var claimID = "";
    var commentGUID = S4();
    $("#lblMessage").text("");
    $("#txtSendEmail").button();

    //$(document).ready(function () {
    //   // alert(cid);
    //});

    var HandleFileSelect = function (evt) {
        var files = evt.target.files;
        var file = files[0];

        $("#lblMessage").text("");
        $("#lblMessage").hide();

        if (files && file) {
            var reader = new FileReader();

            reader.onload = function (readerEvt) {
                $("#loader").show();
                $("#txtSendEmail").button('disable');
                attachmentBase64String = readerEvt.target.result;
            };

            reader.onloadend = function (evt) {
                attachmentAddedTF = true;
                $("#txtSendEmail").button('enable');
                $("#loader").hide();
            };

            reader.readAsDataURL(file);
        }
    };

    function SendEmail() {

        var querystring = encodeURIComponent("email=" + $(".to-address").val() + "|cid=" + cid + "|claimid=" + claimID + "|ctype=Claim|ctid=" + commentGUID)
        var replyLink = "If you'd like to comment, please reply <a href='http://" + url + "/OnlineForms/DriverComment.aspx?id=" + querystring+"'>here</a>";
        var NewNCDMailMessage = {};

        $("#loader").show();

        NewNCDMailMessage.FromAddress = $(".from-address").val();
        NewNCDMailMessage.ToAddress = $(".to-name").val();
        NewNCDMailMessage.Subject = $(".email-subject").val();
        NewNCDMailMessage.Message = "<p>" + $(".email-message").val() + "</p>" + replyLink;
        NewNCDMailMessage.CCAddress = "";
        NewNCDMailMessage.BCCAddress = "";

        if (attachmentAddedTF) {

            if ($("#fupEmailAttachmentUpload")[0].files[0].size > 9999999) {
                $("#lblMessage").text("Attachment must be less than 10MB.");
                $("#lblMessage").show();
                $("#loader").hide();
                return;
            }
            else {
                $("#lblMessage").text("");
                $("#lblMessage").hide();
                NewNCDMailMessage.AttachmentFile = attachmentBase64String;
                NewNCDMailMessage.AttachmentFileName = $("#fupEmailAttachmentUpload")[0].files[0].name;
            }
        }

        var DTO = { 'NewNCDMailMessage': NewNCDMailMessage };

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/SendEmailMessage",
            data: JSON.stringify(DTO),
            dataType: "json",
            beforeSend: function () { },
            success: function (data) {
                $("#lblMessage").text("Email successfully sent.");
                $("#lblMessage").show();
                SaveEmailInformation();
                $("#loader").hide();
            },
            error: function (xhr, textStatus, error) {
                $("#lblMessage").text("Error: " + xhr.responseText);
                $("#lblMessage").show();
                $("#loader").hide();
            }
        });
    }

    function ShowEmailDialog(obj) {
        ClearControls();
        LoadSendFunctions(obj);

        $("#email-dialog").dialog({
            width: 'auto',
            height: 'auto',
            modal: true,
            zIndex: 9000
        });

        $("#lblMessage").hide();
    }

    function ClearControls() {
        $(".emailer-section input").each(function () {
            $(this).val("");
        });

        $(".emailer-section textarea").each(function () {
            $(this).val("");
        });

        $("#lblMessage").text("");
    }

    function LoadSendFunctions(obj) {
        if (typeof $(".emailer-section").attr("control-title") != 'undefined') {
            $("#email-dialog").attr("title", $(".emailer-section").attr("control-title"));
        }

        claimID = $(obj).attr("aria-claim-id");
        var pvoName = $(obj).attr("rel");

        $(".to-name").val(pvoName);
        $(".to-name").attr("disabled", "disabled");
        $(".from-address").attr("disabled", "disabled");
        $(".from-address").val("ClaimNotification@AtlanticRelocation.com");
        $(".email-message").val($(".default-email-body").text());
        $(".email-subject").val("Claim on Shipment");

        var driverQuery = "select emailaddress from drivers where companyid=10003 and pvoname='"+pvoName+"'";

        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "../ListHandler.ashx?gt=none&qt=text&pl=&qn=" + driverQuery,
            data:"",
            dataType: "json",
            beforeSend: function () { },
            success: function (data) {
                //alert(data);
                $(".to-address").val(data);

                if (data == "") {
                    alert("Error: The Driver " + pvoName + " was not found in the Driver table.  Make sure you have a valid driver.");
                    $("#txtSendEmail").hide();
                }
                else {
                    $("#txtSendEmail").show();
                }
            },
            error: function (xhr, textStatus, error) {
                $("#lblMessage").text("Error: " + xhr.responseText);
                $("#lblMessage").show();
            }
        });
    }

    function SaveEmailInformation() {

        var NewSurveyComment = {};

        var comments = "Driver: " + $(".to-name").val() +"\r\n"+
                       "Subject: "+$(".email-subject").val()+"\r\n"+
                       "Message: " + $(".email-message").val();

        NewSurveyComment.UserID = uid;
        NewSurveyComment.CompanyID = cid;
        NewSurveyComment.Comments = comments;
        NewSurveyComment.SurveyID = claimID;
        NewSurveyComment.CommentType = "Claims";
        NewSurveyComment.ParentID = "0";
        NewSurveyComment.UserType = "";
        NewSurveyComment.CommentGUID = commentGUID;

        var DTO = { 'NewSurveyComment': NewSurveyComment };

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/AddSurveyComment",
            data: JSON.stringify(DTO),
            dataType: "json",
            success: function () { },
            error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
        });
    }

</script>