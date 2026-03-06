<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDDeficiencyBlog.ascx.cs" Inherits="AISReports.UserControls.NCDDeficiencyBlog" %>
 <script type="text/javascript">

        var selectedSurvey;
        var commentsCID;
        $(document).ready(function () {

            $(".handle").hide();

            try {

                if (cid == "10025") {
                    commentsCID = "1";
                }
                else {
                    commentsCID = cid;
                }

                $("#show-comments").load("../DeficientSurveyComments.aspx?sid=NA&ctype=DeficientSurvey&cid=" + commentsCID).delay(1000).fadeIn(800);


            }
            catch (e) {
                alert(e.Message);
            }

            $(".dataTables_length label select").change(function () {
                ShowResponses();
            });

            $("#add-comment").button();

        });

        function getComments(ele) {
            selectedSurvey = $(ele);
            LoadComments();
        }

        function addComments() {
            if ($("#SurveyID").text() != "No survey selected") {

                if ($("#Comments").val() != "") {
                    SaveComments();
                    LoadComments();
                }
                else {
                    alert("No comments entered. Please enter comments.");
                }
            }
            else {
                alert("No survey selected to comment.");
            }

            ShowResponses();
        }

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regexS = "[\\?&]" + name + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var results = regex.exec(window.location.search);
            if (results == null)
                return "";
            else
                return decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        function DisplaySurveyDetails(ele) {

            $.fancybox({
                'width': 700,
                'height': 610,
                'autoScale': false,
                'transitionIn': 'fade',
                'transitionOut': 'fade',
                'href': $(ele).attr("href"),
                'type': 'iframe'
            });
        }

        function SaveComments() {

            var NewSurveyComment = {};

            NewSurveyComment.UserID =uid;
            NewSurveyComment.CompanyID = commentsCID;
            NewSurveyComment.Comments = $("#Comments").val();
            NewSurveyComment.SurveyID = $("#SurveyID").text();
            NewSurveyComment.CommentType = "DeficientSurvey";
            NewSurveyComment.ParentID = "0";
            NewSurveyComment.UserType = "";

            var DTO = { 'NewSurveyComment': NewSurveyComment };

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../AISWS.asmx/AddSurveyComment",
                data: JSON.stringify(DTO),
                dataType: "json",
                success: function () { LoadComments(); },
                error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); },
                complete: function () { ShowResponses(); }
            });
        }

        function LoadComments() {

            var sid = $(selectedSurvey).attr("rel");

            $("#SurveyID").text(sid);
            $("#Comments").val('');

            $("#show-comments").empty();

            try {
                $.ajax({
                    type: "GET",
                    contentType: "text/html",
                    cache: false,
                    url: "../DeficientSurveyComments.aspx?sid=" + sid + "&cid=" + commentsCID + "&ctype=deficientsurvey",
                    success: function () {
                        $("#show-comments").load("../DeficientSurveyComments.aspx?sid=" + sid + "&cid=" + commentsCID + "&ctype=deficientsurvey&key=" + new Date().getTime());
                    },
                    error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); }
                });

            }
            catch (e) {
                alert(e.message);
            }
        }

        function ShowResponses() {
            $(".response-count").each(function () {

                //alert("SurveyID: " + $("#SurveyID").text() + "  REL: " + $(this).attr("rel") + "VALUE: " + $(this).text());
                try {
                    if ($(this).text() == "0" && $("#SurveyID").text() == $(this).attr("rel")) {

                        $(this).text("1");
                        $(this).removeClass("red-btn");
                        $(this).addClass("green-btn");
                    }
                    else if ($(this).text() == "0") {
                        $(this).removeClass("green-btn");
                        $(this).addClass("red-btn");
                    }
                    else {
                        $(this).removeClass("red-btn");
                        $(this).addClass("green-btn");
                    }

                    //alert($(this).attr("rel") + "   " + $("#SurveyID").text() + "  " + $(this).text());
                }
                catch (e) {
                    //alert(e.Message);
                }
            });
        }
        
 </script> 
<style type="text/css">
    .green-btn { background-image: url('../images/green-button.png')}
    .red-btn { background-image: url('../images/red-button.png')}
    .response-count {display:inline-block; width:15px; height:15px; color:transparent; }
     .widthFull {width: 100%;}
     #nav {font-size:1em;}

    .displayNone {display:none;}
</style>
     <div>
        <div id="header">
            <asp:PlaceHolder runat="server" ID="phContentHeaderColumn"></asp:PlaceHolder>
        </div>
        <div style="padding:6px;color:#fff;">
            To make a comment on a survey,  click on the 'Add' link in the grid next to the selected survey, add your comments to the textbox below and click 'Add Comments' to save.  Use the 'Search' box to filter your selection.
        </div>
        <table border="0" cellpadding="2" cellspacing="2" style="width:80%;">
        <tr>
            <td>
             <asp:Panel runat="server" ID="pChart" class="grid-section">
                    <table id="blog-grid" class="grid-control">
                        <thead>
                            <tr>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <asp:Literal runat="server" ID="litHeaderRow" EnableViewState="false"></asp:Literal>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Literal runat="server" ID="litBody" EnableViewState="false"></asp:Literal>
                        </tbody>
                    </table>
               </asp:Panel>
             </td>
             <td rowspan="2" style="vertical-align:top;">
               <div class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix" style="padding:5px;">Comments</div>
               <div id="show-comments" style="width:300px;height:600px; border: 1px solid #000;background-color:#cccccc; padding-left:3px; vertical-align:top; overflow-y:scroll;">
               </div>
             </td>
          </tr>
          <tr>
           <td>
           <div>
            <div class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix" style="padding:5px;">
            <span>Shipment Number: </span><span id="SurveyID">No survey selected</span>
            </div>
            <textarea id="Comments" rows="10" cols="120" style="text-align:left;width:99%;">
            </textarea>
            <br />
            <a href="#" id="add-comment" onclick="javascript:addComments(); return false;">Add Comment</a>
           </div>
           </td>
           </tr>
           </table>
      </div>
    <asp:Label runat="server" ID="lblTableName" CssClass="field-table-name" style="display:none;" />
    <asp:Label runat="server" ID="lblFieldName" CssClass="field-name" style="display:none;" />
    <asp:Label runat="server" ID="UserID" CssClass="user-id" style="display:none;" />