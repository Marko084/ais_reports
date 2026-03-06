var NewNCDDiaryQuery = { ClaimNumber: "", QueryType: "", Diaries: new Array() };
var claimNumber;
var NoteUserType;
var NoteUserID;

$(function () {
    $("#lnkCloseNote").button();
    $("#lnkNewNote").button();
    $("#lnkSaveNote").button();
    $(".note-document-btn").button();
    $("#note-filter-type-clearfilter").button();

    NoteUserType = $(".utype").text();
    NoteUserID = $(".uid").text();
});

function GetNotes(ele) {
    try
    {
        if (typeof ele !== "undefined") {
            claimNumber = $(ele).attr("rel");
        }

        NewNCDDiaryQuery.ClaimNumber = claimNumber;
        NewNCDDiaryQuery.CompanyID = cid; //$("#lblCID").text();
        NewNCDDiaryQuery.QueryType = "get";
        NewNCDDiaryQuery.UserID = uid;
        NewNCDDiaryQuery.NoteType = "Note";
        NewNCDDiaryQuery.Diaries = new Array();

        var DTO = { 'NewNCDDiaryQuery': NewNCDDiaryQuery };

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/NCDDiaryQuery",
            data: JSON.stringify(DTO),
            dataType: "json",
            beforeSend: function () { },
            error: function (xhr, textStatus, error) {alert("Error: " + xhr.responseText); },
            success: function (data) {
                var records = data.d.Diaries.filter(function (item) {
                    if (readOnlyMode === "true") {
                        return item.DiaryType.toLowerCase() === "investigation" ||
                               item.DiaryType.toLowerCase() ==="report";
                    }
                    else {
                        return item;
                    }
                });

                ClearNoteDialogFields();
                CreateNoteSections(records.length);
                ShowNoteDialog();

                $.each(records, function (index, item) {
                    try {
                         var noteIdx = index + 1;
                         var noteSection = $("#note-dialog > .note-scroll > .note-section > .note-" + noteIdx);
                         
                         var reportDate = $.datepicker.formatDate("mm/dd/yy", new Date(parseInt(item.ReportDate.substr(6))));
                         var noteHeaderText = "Note " + noteIdx + " : " + item.DiaryType + " " + (reportDate.replace(/\s/g, "").length > 1 ? reportDate : "");

                         $("#note-dialog > .note-scroll > .note-section > .note-" + noteIdx + "-header").text(noteHeaderText);

                         if (item.CompletedTF) {
                             $("#note-dialog > .note-scroll > .note-section > .note-" + noteIdx + "-header").append("<img src='../images/workflow-complete.png' alt='Completed Note'/>");
                         }
                         
                         $(noteSection).find(".note-report-date").val(reportDate==="01/01/1900" ? getDate :reportDate);
                         $(noteSection).find(".note-type-" + item.DiaryType.replace(/\s/g, "").toLowerCase()).addClass("note-type-selected");
                         $(noteSection).find(".note-comments").val(item.Comments);

                         var noteDownloadBtn = $(noteSection).find(".note-document-btn");
                         var noteDeleteBtn = $(noteSection).find(".note-delete-document-btn");
                         var noteUploadBtn = $(noteSection).find(".fuNoteDocument");

                         $(noteUploadBtn).attr("note-attachment-name", "Note-" + item.ClaimNumber + "-" + item.pkDiaryID);
                         $(noteUploadBtn).attr("rel", item.pkDiaryID);
                         $(noteUploadBtn).addClass("note-attachment-" + item.pkDiaryID);
                         $(noteDeleteBtn).addClass("note-attachment-" + item.pkDiaryID);
                         $(noteDownloadBtn).addClass("note-attachment-" + item.pkDiaryID);

                         if (item.DownloadUrl !== "NOFILE") {
                             $(noteDownloadBtn).find("span").text("Download " + item.AttachmentFileName);
                             $(noteDownloadBtn).attr("href", item.DownloadUrl);
                             $(noteDeleteBtn).attr("rel", item.DownloadUrl.split("=")[1]);
                             $(noteDeleteBtn).attr("note-pk", item.pkDiaryID);
                             $(noteDownloadBtn).show();
                             $(noteDeleteBtn).show();

                             $(noteUploadBtn).hide();
                         }
                         else
                         {
                             $(noteUploadBtn).hide();
                             $(noteUploadBtn).show();
                         }

                         if (item.CompletedTF) {
                             $(noteSection).find(".note-completed").prop("checked", true);
                         }
                         else {
                             $(noteSection).find(".note-completed").prop("checked", false);
                         }

                         $(noteSection).find(".note-primarykeyfield").val(item.pkDiaryID);
                         $(noteSection).find("span[class='note-createdby-userid']").val(item.UserID);
                         $(noteSection).find("input[class='note-mode']").val("update");
                         $(noteSection).find(".delete-note").attr("rel", "note-" + noteIdx);

                         if (NoteUserType === "Manager") {
                             $(noteSection).find(".delete-note").show();
                         }
                         else if (item.UserID === ClaimantUserID) {
                             $(noteSection).find(".delete-note").show();
                         }
                         else {
                         $("#note-dialog > .note-scroll > .note-section > .note-" + noteIdx + " input").prop("disabled", true);
                         $("#note-dialog > .note-scroll > .note-section > .note-" + noteIdx + " textarea").prop("disabled", true);
                         $("#note-dialog > .note-scroll > .note-section > .note-" + noteIdx + " a").removeAttr("onclick");
                         }
                    }
                    catch (e) {
                        alert("Error on Note module: "+e.message);
                    }
                });

                if (readOnlyMode === "true") {
                     SetNoteReadOnlyMode();
                }
            }
        });
    }
    catch(e)
    {
        alert(e.message);
    }
}

function DeleteNoteAttachment(ele){

    var deleteAttachment = confirm("Are you sure you want to delete?");

    if (deleteAttachment) {
        var NewCDDocumentQuery = {};

        NewCDDocumentQuery = {
            pkDocumentID: $(ele).attr("rel"),
            QueryType: "delete"
        };

        var DTO = { 'NewNCDDocumentQuery': NewCDDocumentQuery };

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/NCDDocumentQuery",
            data: JSON.stringify(DTO),
            dataType: "json",
            beforeSend: function () { },
            error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); },
            success: function () {
                var notePK = $(ele).attr("note-pk");
 
                $(".note-attachment-" + notePK).each(function () {
                    if ($(this).attr("aria-note-ctrl-type") === "download") {
                        $(this).hide();
                    }
                    else if ($(this).attr("aria-note-ctrl-type") === "upload") {
                        $(this).show();
                    }
                    else if ($(this).attr("aria-note-ctrl-type") === "delete") {
                        $(this).hide();
                    }
                });
            }
        });
    }
}

function LoadNoteAttachment(ele) {

    $(ele).change(function (evt) {
        var files = evt.target.files;
        var file = files[0];
        var attachmentBase64String = "";
        var noteID = $(ele).attr("rel");

        if (files && file) {
            var reader = new FileReader();

            reader.onload = function (readerEvt) {
                attachmentBase64String = readerEvt.target.result;
            };

            reader.onloadend = function (evt) {
                try {
                    var NewCDDocumentQuery = {};

                    NewCDDocumentQuery= {
                        CompanyID : cid, //$(".cid").text(),
                        UserID : NoteUserID,
                        GroupName : $(ele).attr("note-attachment-name"),
                        GroupDescription : "",
                        DocumentFile : attachmentBase64String,
                        DocumentName : file.name,
                        QueryType : "add"
                    }

                    var DTO = {'NewNCDDocumentQuery': NewCDDocumentQuery};

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "../AISWS.asmx/NCDDocumentQuery",
                        data: JSON.stringify(DTO),
                        dataType: "json",
                        beforeSend: function () { },
                        error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); },
                        success: function () {
                        }
                    });
                }
                catch (e) {
                    alert(e.Message);
                }
                
            };

            reader.readAsDataURL(file);
        }
    });
}

function SaveNotes()
{
    try
    {
        var attachmentAddedTF = false;
        var note;

        $("#note-dialog >  .note-scroll >  .note-section > .note-fields").each(function () {
            attachmentAddedTF = false;
            note = {
                pkDiaryID: $(this).find(".note-primarykeyfield").val(),
                CompanyID: cid, //$("#lblCID").text(),
                ClaimNumber: claimNumber,
                ReportDate: ($(this).find(".note-report-date").val().trim() === "" ? "1/1/1900" : $(this).find(".note-report-date").val().trim()),
                NextReportDate: "1/1/1900",
                DiaryType: $(this).find(".note-type-selected").text(),
                Comments: $(this).find(".note-comments").val(),
                Mode: $(this).find("input[class='note-mode']").val(),
                CompletedTF: $(this).find(".note-completed").prop("checked"),
                UserID: uid,
                NoteType:"Note",
                UpdatedBy: $("#ctl00_lblUserInfo").text().replace(/Welcome/i, "").trim()
            };

            NewNCDDiaryQuery.QueryType = "update";
            NewNCDDiaryQuery.Diaries.push(note);
        });

        //alert("finished.");
        var DTO = { 'NewNCDDiaryQuery': NewNCDDiaryQuery };
        //console.log(JSON.stringify(DTO));
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/NCDDiaryQuery",
            data: JSON.stringify(DTO),
            dataType: "json",
            beforeSend: function () { },
            error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); },
            success: function ()
            {
                alert("Data was successfully saved.");
                GetNotes();
            }
        });
    }
    catch (e) {
        alert(e.message);
    }
}

function ShowNoteDialog() {
    $("#note-dialog").dialog({
        minWidth: 1096,
        minHeight: 800,
        modal: true,
        zIndex: 9000,
        position: {
            my: "top center",
            at: "top center",
            of: "#contentcolumn"
        },
        open: function () {
            //setTimeout("SetClaimantInputMask()", 300);
            //$("#ui-accordion-1-header-0").trigger("click");
        },
        close: function () {
            $(".ui-dialog").find(".ui-widget-header").css("background", "gray");
            $("#note-dialog > .note-scroll > .note-section").accordion("destroy");
        }
    });
}

function ClearNoteDialogFields() {
    $("#note-dialog > .note-scroll> .note-section").empty();
}

function CreateNoteSections(noteCount) {

    var noteTemplate = $("#note-dialog > .note-template");
    var noteSection = $("#note-dialog > .note-scroll > .note-section");
    
    if (noteCount === 0) {
        noteCount = 1;
    }

    for (i = 0; i < noteCount; i++) {
        var template = $(noteTemplate).clone();
        var header = $(template).find("h3");
        var section = $(template).find("div");

        $(header).text("Note " + (i + 1).toString());
        $(header).addClass("note-" + (i + 1).toString() + "-header");
        $(section).addClass("note-" + (i + 1).toString());
        $(section).addClass("note-fields");
        $(section).show();
        $(noteSection).append(header);
        $(noteSection).append($(section));
    }
   
    if ($(noteSection).hasClass('ui-accordion')) {
        $(noteSection).accordion('destroy');
    }

    $(noteSection).find("a.delete-note").button();
    $(noteSection).accordion({ heightStyle: 'content' });

    SetNoteInputMask();
}

function SetNoteInputMask() {
    var ctrlType = "";

    $("#note-dialog > .note-scroll > .note-section input").each(function () {
        ctrlType = $(this).attr("class");

        if (ctrlType.indexOf("date") > -1) {
            //$(this).mask('99/999/9999');
            $(this).datepicker({
                changeMonth: true,
                changeYear: true
            });
        }

        if (ctrlType.indexOf("note-report-date") > -1) {
            $(this).val(GetCurrentDate());
        }
    });
}

function AddNoteSection() {
    var noteSection = $("#note-dialog > .note-scroll > .note-section");
    var noteCount = $("#note-dialog > .note-scroll > .note-section > .note-fields").length;
    var template = $("#note-dialog > .note-template").clone();
    var header = $(template).find("h3");
    var section = $(template).find("div");

    $(header).text("Note " + (noteCount + 1).toString());
    $(section).addClass("note-" + (noteCount + 1).toString());
    $(section).addClass("note-fields");
    $(section).show();
    $(noteSection).append(header);
    $(noteSection).append($(section));

    $(noteSection).accordion("destroy");
    $(noteSection).accordion({ heightStyle: 'content', active: noteCount });

    SetNoteInputMask();
}

function CloseNoteDialog() {

    $(".note-filter a").removeClass("note-filtertype-selected");
    $("#note-dialog >  .note-scroll > .note-section").accordion("destroy");
    $("#note-dialog").dialog("close");
}

function DeleteNote(ele)
{
    var result = confirm('Are you sure you want to delete?');

    if (result) {

        var noteSectionName = $(ele).attr("rel");
        var noteSection = $("#note-dialog > .note-scroll > .note-section > ." + noteSectionName);
        var header = $("#note-dialog > .note-scroll > .note-section > ." + noteSectionName + "-header");

        $(noteSection).find(".note-mode").val("delete");
        $(noteSection).hide();
        $(header).hide();

        SaveNotes();
    }
}

function SetNoteType(ele) {
    var noteTypeSection = $(ele).parent().parent().attr("id");

    $("#"+noteTypeSection+" > .note-center a").removeClass("note-type-selected");
    $(ele).addClass("note-type-selected");
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

function GetCurrentDate() {
    // Create a date object with the current time
    var today = new Date();

    return today.getMonth() + 1 + "/" + today.getDate() + "/" + today.getFullYear();
}

function SetNoteReadOnlyMode() {
    $("#note-dialog >  .note-scroll > .note-section input").each(function () {
        $(this).prop("disabled", true);
    });

    $("#note-dialog >  .note-scroll > .note-section textarea").each(function () {
        $(this).prop("disabled", true);
    });

    $("#note-dialog > .note-scroll > .note-section select").each(function () {
        $(this).prop("disabled", true);
    });

    $("#note-dialog > .note-scroll > .note-section a").each(function () {
        $(this).hide();
    });

    $("#lnkNewNote").hide();
    $("#lnkSaveNote").hide();
    $(".note-document-btn").hide();
    //$("#note-filter-type-medical").hide();
    //$("#note-filter-type-legal").hide();
    //$("#note-filter-type-subrogation").hide();
    //$("#note-filter-type-planofaction").hide();
    //$("#note-filter-type-nurse").hide();
    //$("#note-filter-type-supervisor").hide();
}

function SetFilterNoteType(ele) {
    var noteFilterType = $(ele).text().toLowerCase();

    $(".note-section > h3").each(function () {
        var noteHeader = $(this).text().toLowerCase();
        var notePanelID = $(this).attr("aria-controls");
        var panelIsVisible = $(this).attr("aria-selected");

        if (noteHeader.indexOf(noteFilterType) > -1 || noteFilterType==="clear filter") {
            $(this).show();

            if (panelIsVisible === "true") {
                $("#" + notePanelID).show();
            }
        }
        else {
            $(this).hide();
            $("#" + notePanelID).hide();
        }

    });

    if (noteFilterType !== "clear filter") {
        $(".note-filter a").removeClass("note-filtertype-selected");

        $(ele).addClass("note-filtertype-selected");
    }
}
