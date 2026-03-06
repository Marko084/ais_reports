<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentImageManager.aspx.cs" Inherits="AISReports.Site.DocumentImageManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body {font-family: Calibri Arial Verdana;}
        table#image-documents-grid tbody tr.odd td{font-family: Calibri, Verdana, Arial;}
        table#image-documents-grid tbody tr.even td{font-family: Calibri, Verdana, Arial;}
        span {display:block; padding:3px 0px 2px 0px; font-weight:bold;}
        .doc-upload {
            display: inline-block;
            width: 400px;
            height: 300px;
            float: left;
            color: #fff;
        }

        .doc-upload fieldset {font-family: Calibri Arial Verdana;}

        .doc-grid {
            display: inline-block;
            height: 300px;
            float: left;
            color: #fff;
        }
    </style>
    <script type="text/javascript" src="../script/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../script/jquery-migrate-1.2.1.js"></script>
    <script type="text/javascript" src="../script/jquery.tabSlideOut.v1.3.js"></script>
    <script type="text/javascript" src="../script/jquery-ui-1.9.2.custom.min.js"></script>
    <script type="text/javascript" src="../script/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../script/dataTables.fnReloadAjax.js"></script>
    <script type="text/javascript" src="../script/dataTables.fnGetColumnIndex.js"></script>
    <script type="text/javascript" src="../script/dataTables.fnHideColumns.js"></script>
    <script type="text/javascript" src="../script/dataTables.scroller.min.js"></script>
    <script type="text/javascript" src="../script/TableTools.min.js"></script>
    <script type="text/javascript" src="../script/ZeroClipboard.js"></script>
    <link rel="stylesheet" href="../css/TableTools_JUI.css" type="text/css" />
    <link href="../Content/DataTables/css/demo_table_jui_20140226001.css" rel="stylesheet" />
    <!-- <link href="../css/menu.css" rel="stylesheet" type="text/css" />
   <link href="../css/dark-hive/jquery-ui-1.9.1.custom.css" rel="stylesheet" type="text/css" />-->
	<script type="text/javascript" src="../script/hoverIntent.js"></script>
    <script type="text/javascript" src="../script/json3.min.js"></script>
   
</head>
<body>
    <form id="form1" runat="server">

    <div style="display:block;width:1200px;height:370px;">
        <div class="doc-upload" style="padding-right:20px;">
            <fieldset>
                <legend>Document Uploader</legend>
                <span>Upload File:</span>
                <asp:FileUpload runat="server" ID="fuFile" style="width:300px;" CssClass="file-upload-control"  />
                <span id="categoryLabel">Category:</span>
                <asp:TextBox runat="server" ID="txtGroupName" style="width:380px;" data-field-name="groupname" data-table-type="cms_Documents" CssClass="lookup-combobox" />
                <span>Document Name:</span>
                <asp:TextBox runat="server" ID="txtFileName" style="width:380px;" data-field-name="documentname" data-table-type="cms_Documents" CssClass="lookup-combobox" />
                <asp:Button ID="btnAdd" runat="server" Text="Add Document" OnClick="btnAdd_Click" />
            </fieldset>
        </div>
        <div class="doc-grid">
            <fieldset>
                <legend>Uploaded Documents</legend>
                <table id="image-documents-grid" class="grid-control">
                <thead>
                    <tr>
                        <th></th>
                        <th>pkDocumentID</th>
                        <th id="category-field">Category</th>
                        <th>ClaimType</th>
                        <th>Document Name</th>
                        <th>DocumentExtension</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                </tbody>
            </table>
                <iframe width="0" height="0" id="frmImageDocument" name="frmImageDocument"></iframe>
            </fieldset>
        </div>
    </div>
    <div style="display:block;width:100%; height:40px;">
        <asp:Label runat="server" ID="lblMessage" class="error-msg ui-state-error ui-corner-all" style="display:block; padding:3px; font-weight:normal;"></asp:Label>
    </div>
        
    <script type="text/javascript">
        var oDocumentsGrid;
        var cid = getParameterByName("cid");
        var catName = getParameterByName("catname");
        var catLabel = getParameterByName("catlabel");
        $(".error-msg").hide();

        if (cid === "10103") {
            $(".file-upload-control").prop("disabled", true);
            $(".lookup-combobox").prop("disabled", true);
            $("#btnAdd").prop("disabled", true);
        }

        BuildDocumentGrid();

        function BuildDocumentGrid() {

            try {

                var documentQuery = "select pkDocumentID,GroupName,GroupDescription,DocumentName,DocumentExtension from cms_Documents where CompanyID=" + cid;

                if (catName.length > 0) {
                    documentQuery = "select pkDocumentID,GroupName,GroupDescription,DocumentName,DocumentExtension from cms_Documents where CompanyID=" + cid + " and GroupName='" + catName + "'";
                }

                if (catLabel.length > 0) {
                    $("#categoryLabel").text(catLabel);
                    $("#category-field").text(catLabel);
                }
                else {
                    $("#categoryLabel").text("Claim Number");
                    $("#category-field").text("Claim Number");
                }

                var dataUri = "../ListHandler.ashx?gt=docimg&ct=grid&qt=text&fld=&qn=" + documentQuery + "&pl=&qid=" + S4();
                var gridButtons = "";

                oDocumentsGrid=$("#image-documents-grid").dataTable({
                    "bDestroy": true,
                    "sAjaxSource": dataUri,
                    "aLengthMenu": [[10, 20, 25, -1], [10, 20, 25, "All"]],
                    "sPaginationType": "full_numbers",
                    "bDeferRender": true,
                    "bJQueryUI": true,
                    "oLanguage": { "sZeroRecords": "No data found to display." },
                    "sDom": '<"H"lfr><"datatable-scroll"t><"F"ip>',
                    "fnRowCallback": function (nRow, aData, iDisplayIndex) {

                        if (cid === "10103") {
                            gridButtons = "<a target='frmImageDocument' href='../GetFile.aspx?id=" + aData[1] + "' title='Click to view'>View</a>";
                        }
                        else {
                            gridButtons = "<a href='javascript:void(0)' rel='" + aData[1] + "' title='Delete document' onclick='javascript:DeleteDocument(this);'>Delete</a>&nbsp;<a target='frmImageDocument' href='../GetFile.aspx?id=" + aData[1] + "' title='Click to view'>View</a>";
                        }

                        $("td:eq(0)", nRow).html(gridButtons);
                        $("td:eq(3)", nRow).html(aData[4] + "." + aData[5]);
                        
                    }
                });

                var columnToHideIdx = oDocumentsGrid.fnGetColumnIndex(("pkDocumentID"));

                if (columnToHideIdx > -1) {
                    oDocumentsGrid.fnSetColumnVis(columnToHideIdx, false);
                }

                var columnToHideIdx = oDocumentsGrid.fnGetColumnIndex(("ClaimType"));

                if (columnToHideIdx > -1 && cid !="10122") {
                    oDocumentsGrid.fnSetColumnVis(columnToHideIdx, false);
                }

                columnToHideIdx = oDocumentsGrid.fnGetColumnIndex(("DocumentExtension"));

                if (columnToHideIdx > -1) {
                    oDocumentsGrid.fnSetColumnVis(columnToHideIdx, false);
                }
            }
            catch (e) {
                //alert("Error: " + e.Message);
            }
        }

        function DeleteDocument(obj) {

            $(".error-msg").text("");
            var deleteTF = confirm('Are you sure you want to delete this document?');

            if(deleteTF)
            {
                var NewJSONDBQuery = {};
                var modeDisplay = "";

                NewJSONDBQuery.FieldList = "";
                NewJSONDBQuery.TableName = "cms_Documents";
                NewJSONDBQuery.KeyFieldName = "pkDocumentID";
                NewJSONDBQuery.KeyFieldValue = $(obj).attr("rel");
                NewJSONDBQuery.QueryType = "delete";

                var DTO = { 'NewJSONDBQuery': NewJSONDBQuery };
                //alert(JSON.stringify(DTO));
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "../AISWS.asmx/JSONDBQuery",
                    data: JSON.stringify(DTO),
                    dataType: "json",
                    beforeSend: function () { },
                    success: function (data) {
                        $(".error-msg").text("Document deleted.");
                        oDocumentsGrid.fnReloadAjax();
                    },
                    error: function (xhr, textStatus, error) { $(".error-msg").text("Error: " + xhr.responseText); }
                });
            }
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

        function S4() {
            return (((1 + Math.random()) * 0x10000000) | 0).toString(16);

        }
    </script>
    </form>
</body>
</html>

