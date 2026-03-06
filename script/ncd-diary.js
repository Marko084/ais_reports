var NewNCDDiaryQuery = { ClaimNumber: "", QueryType: "", Diaries: new Array() };
var claimNumber;
var DiaryUserType;
var DiaryUserID;

$(function () {
    $("#lnkCloseDiary").button();
    $("#lnkNewDiary").button();
    $("#lnkSaveDiary").button();
    $(".diary-document-btn").button();

    DiaryUserType = $(".utype").text();
    DiaryUserID = $(".uid").text();

});

function GetDiaries(ele) {
    try
    {
        if (typeof ele !== "undefined") {
            claimNumber = $(ele).attr("rel");
        }

        NewNCDDiaryQuery.ClaimNumber = claimNumber;
        NewNCDDiaryQuery.CompanyID = cid; //$("#lblCID").text();
        NewNCDDiaryQuery.QueryType = "get";
        NewNCDDiaryQuery.UserID = uid;
        NewNCDDiaryQuery.NoteType = "Diary";
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
                var record = data.d.Diaries;

                ClearDiaryDialogFields();
                CreateDiarySections(data.d.Diaries.length);
                ShowDiaryDialog();

                $.each(record, function (index, item) {
                    try {

                       //alert(item.DiaryType);
                         var diaryIdx = index + 1;
                         var diarySection = $("#diary-dialog > .diary-scroll > .diary-section > .diary-" + diaryIdx);
                         
                         var reportDate = $.datepicker.formatDate("mm/dd/yy", new Date(parseInt(item.ReportDate.substr(6))));
                         var nextReportDate = $.datepicker.formatDate("mm/dd/yy", new Date(parseInt(item.NextReportDate.substr(6))));
                         var reportDateRangeText = (reportDate === "01/01/1900" ? "" : reportDate) + " - " + (nextReportDate == "01/01/1900" ? "" : nextReportDate);

                         var diaryHeaderText = "Diary " + diaryIdx + " : " + item.DiaryType + " " + (reportDateRangeText.replace(/\s/g, "").length > 1 ? reportDateRangeText : "");

                         $("#diary-dialog > .diary-scroll > .diary-section > .diary-" + diaryIdx + "-header").text(diaryHeaderText);

                         if (item.CompletedTF) {
                             $("#diary-dialog > .diary-scroll > .diary-section > .diary-" + diaryIdx + "-header").append("<img src='../images/workflow-complete.png' alt='Completed Diary'/>");
                         }
                         
                         $(diarySection).find(".diary-report-date").val((reportDate==="01/01/1900" ? "" :reportDate));
                         $(diarySection).find(".diary-next-report-date").val((nextReportDate === "01/01/1900" ? "" : nextReportDate));
                         $(diarySection).find(".diary-type-" + item.DiaryType.replace(/\s/g, "").toLowerCase()).addClass("diary-type-selected");
                         $(diarySection).find(".diary-comments").val(item.Comments);

                         var diaryDownloadBtn = $(diarySection).find(".diary-document-btn");
                         var diaryDeleteBtn = $(diarySection).find(".diary-delete-document-btn");
                         var diaryUploadBtn = $(diarySection).find(".fuDiaryDocument");

                         $(diaryUploadBtn).attr("diary-attachment-name", "Diary-" + item.ClaimNumber + "-" + item.pkDiaryID);
                         $(diaryUploadBtn).attr("rel", item.pkDiaryID);
                         $(diaryUploadBtn).addClass("diary-attachment-" + item.pkDiaryID);
                         $(diaryDeleteBtn).addClass("diary-attachment-" + item.pkDiaryID);
                         $(diaryDownloadBtn).addClass("diary-attachment-" + item.pkDiaryID);

                         if (item.DownloadUrl != "NOFILE") {

                             $(diaryDownloadBtn).find("span").text("Download " + item.AttachmentFileName);
                             $(diaryDownloadBtn).attr("href", item.DownloadUrl);
                             $(diaryDeleteBtn).attr("rel", item.DownloadUrl.split("=")[1]);
                             $(diaryDeleteBtn).attr("diary-pk", item.pkDiaryID);
                             $(diaryDownloadBtn).show();
                             $(diaryDeleteBtn).show();

                             $(diaryUploadBtn).hide();
                         }
                         else
                         {
                             $(diaryUploadBtn).hide();
                             $(diaryUploadBtn).show();
                         }

                         if (item.CompletedTF) {
                             $(diarySection).find(".diary-completed").prop("checked", true);
                         }
                         else {
                             $(diarySection).find(".diary-completed").prop("checked", false);
                         }

                         $(diarySection).find(".diary-primarykeyfield").val(item.pkDiaryID);
                         $(diarySection).find("span[class='diary-createdby-userid']").val(item.UserID);
                         $(diarySection).find("input[class='diary-mode']").val("update");
                         $(diarySection).find(".delete-diary").attr("rel", "diary-" + diaryIdx);

                         if (DiaryUserType === "Manager") {
                             $(diarySection).find(".delete-diary").show();
                         }
                         else if (item.UserID === ClaimantUserID) {
                             $(diarySection).find(".delete-diary").show();
                         }
                         else {
                         $("#diary-dialog > .diary-scroll > .diary-section > .diary-" + diaryIdx + " input").prop("disabled", true);
                         $("#diary-dialog > .diary-scroll > .diary-section > .diary-" + diaryIdx + " textarea").prop("disabled", true);
                         $("#diary-dialog > .diary-scroll > .diary-section > .diary-" + diaryIdx + " a").removeAttr("onclick");
                         }
 
                    }
                    catch (e) {
                        alert("Error on Diary module: "+e.message);
                    }
                });

                if (readOnlyMode === "true") {
                     SetDiaryReadOnlyMode();
                }
            }
        });
    }
    catch(e)
    {
        alert(e.message);
    }
}

function DeleteDiaryAttachment(ele){

    var deleteAttachment = confirm("Are you sure you want to delete?");

    if (deleteAttachment) {
        var NewCDDocumentQuery = {};

        NewCDDocumentQuery = {
            pkDocumentID : $(ele).attr("rel"),
            QueryType: "delete"
        }

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
                var diaryPK = $(ele).attr("diary-pk");
 
                $(".diary-attachment-" + diaryPK).each(function () {
                    if ($(this).attr("aria-diary-ctrl-type") == "download") {
                        $(this).hide();
                    }
                    else if ($(this).attr("aria-diary-ctrl-type") == "upload") {
                        $(this).show();
                    }
                    else if ($(this).attr("aria-diary-ctrl-type") == "delete") {
                        $(this).hide();
                    }
                });
            }
        });
    }
}

function LoadDiaryAttachment(ele) {

    $(ele).change(function (evt) {
        var files = evt.target.files;
        var file = files[0];
        var attachmentBase64String = "";
        var diaryID = $(ele).attr("rel");

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
                        UserID : DiaryUserID,
                        GroupName : $(ele).attr("diary-attachment-name"),
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

//var DiaryAttachmentUpload= function (evt) {
//    var files = evt.target.files;
//    var file = files[0];

//    if (files && file) {
//        var reader = new FileReader();

//        reader.onload = function (readerEvt) {
//            attachmentBase64String = readerEvt.target.result;
//        };

//        reader.onloadend = function (evt) {
//            attachmentAddedTF = true;
//        };

//        reader.readAsDataURL(file);
//    }
//};

function SaveDiaries()
{
    try
    {
        var attachmentAddedTF = false;
        var diary;

        $("#diary-dialog >  .diary-scroll >  .diary-section > .diary-fields").each(function () {
            attachmentAddedTF = false;
            diary = {
                pkDiaryID: $(this).find(".diary-primarykeyfield").val(),
                CompanyID: cid, //$("#lblCID").text(),
                ClaimNumber: claimNumber,
                ReportDate: ($(this).find(".diary-report-date").val().trim() == "" ? "1/1/1900" : $(this).find(".diary-report-date").val().trim()),
                NextReportDate: ($(this).find(".diary-next-report-date").val().trim() == "" ? "1/1/1900" : $(this).find(".diary-next-report-date").val().trim()),
                DiaryType: $(this).find(".diary-type-selected").text(),
                Comments: $(this).find(".diary-comments").val(),
                Mode: $(this).find("input[class='diary-mode']").val(),
                CompletedTF: $(this).find(".diary-completed").prop("checked"),
                UserID: uid,
                NoteType:"Diary",
                UpdatedBy: $("#ctl00_lblUserInfo").text().replace(/Welcome/i, "").trim()
            };

            NewNCDDiaryQuery.QueryType = "update";
           
            //if (typeof $(this).find(".fuDiaryDocument").prop("files")[0] != 'undefined') {
 
            //    var reader = new FileReader();
            //    var file = $(this).find(".fuDiaryDocument").prop("files")[0];

            //    reader.onload = function (readerEvt) {
            //        diary.AttachmentFile = readerEvt.target.result;
            //        diary.AttachmentFileName = $(this).find(".fuDiaryDocument").prop("files")[0]["name"];
            //    };

            //    reader.onloadend = function (evt) {
            //        attachmentAddedTF = true;
            //        //alert("uploaded.");
            //    };

            //    reader.readAsDataURL(file);
            //}
            //else
            //{
            //    attachmentAddedTF = true;
            //}

            //while (attachmentAddedTF==false) {
            //    //stay here until uploaded.
            //}

            //alert("diary " + diary.pkDiaryID + " pushed.")
            NewNCDDiaryQuery.Diaries.push(diary);
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
                GetDiaries();
            }
        });
    }
    catch (e) {
        alert(e.message);
    }
}

function ShowDiaryDialog() {
    $("#diary-dialog").dialog({
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
            $("#diary-dialog > .diary-scroll > .diary-section").accordion("destroy");
        }
    });
}

function ClearDiaryDialogFields() {
    $("#diary-dialog > .diary-scroll> .diary-section").empty();
}

function CreateDiarySections(diaryCount) {

    var diaryTemplate = $("#diary-dialog > .diary-template");
    var diarySection = $("#diary-dialog > .diary-scroll > .diary-section");
    
    if (diaryCount == 0) {
        diaryCount = 1;
    }

    for (i = 0; i < diaryCount; i++) {
        var template = $(diaryTemplate).clone();
        var header = $(template).find("h3");
        var section = $(template).find("div");

        $(header).text("Diary " + (i + 1).toString());
        $(header).addClass("diary-" + (i + 1).toString() + "-header");
        $(section).addClass("diary-" + (i + 1).toString());
        $(section).addClass("diary-fields");
        $(section).show();
        $(diarySection).append(header);
        $(diarySection).append($(section));
    }
   
    if ($(diarySection).hasClass('ui-accordion')) {
        $(diarySection).accordion('destroy');
    }

    $(diarySection).find("a.delete-diary").button();
    $(diarySection).accordion({ heightStyle: 'content' });

    SetDiaryInputMask();
}

function SetDiaryInputMask() {
    var ctrlType = "";

    $("#diary-dialog >  .diary-scroll > .diary-section input").each(function () {
        ctrlType = $(this).attr("class");

        if (ctrlType.indexOf("date") > -1) {
            //$(this).mask('99/999/9999');
            $(this).datepicker({
                changeMonth: true,
                changeYear: true
            });
        }
    });
}

function AddDiarySection() {
    var diarySection = $("#diary-dialog > .diary-scroll > .diary-section");
    var diaryCount = $("#diary-dialog > .diary-scroll > .diary-section > .diary-fields").length;
    var template = $("#diary-dialog > .diary-template").clone();
    var header = $(template).find("h3");
    var section = $(template).find("div");

    $(header).text("Diary " + (diaryCount + 1).toString());
    $(section).addClass("diary-" + (diaryCount + 1).toString());
    $(section).addClass("diary-fields");
    $(section).show();
    $(diarySection).append(header);
    $(diarySection).append($(section));

    $(diarySection).accordion("destroy");
    $(diarySection).accordion({ heightStyle: 'content', active: diaryCount });

    SetDiaryInputMask();
}

function CloseDiaryDialog() {
    $("#diary-dialog >  .diary-scroll > .diary-section").accordion("destroy");
    $("#diary-dialog").dialog("close");
}

function DeleteDiary(ele)
{
    var result = confirm('Are you sure you want to delete?');

    if (result) {

        var diarySectionName = $(ele).attr("rel");
        var diarySection = $("#diary-dialog > .diary-scroll > .diary-section > ." + diarySectionName);
        var header = $("#diary-dialog > .diary-scroll > .diary-section > ." + diarySectionName + "-header");

        $(diarySection).find(".diary-mode").val("delete");
        $(diarySection).hide();
        $(header).hide();

        SaveDiaries();
    }
}

function SetDiaryType(ele) {
    var diaryTypeSection = $(ele).parent().parent().attr("id");

    $("#"+diaryTypeSection+" > .diary-center a").removeClass("diary-type-selected");
    $(ele).addClass("diary-type-selected");
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

function SetDiaryReadOnlyMode() {
    $("#diary-dialog >  .diary-scroll > .diary-section input").each(function () {
        $(this).prop("disabled", true);
    });

    $("#diary-dialog >  .diary-scroll > .diary-section textarea").each(function () {
        $(this).prop("disabled", true);
    });

    $("#diary-dialog > .diary-scroll > .diary-section select").each(function () {
        $(this).prop("disabled", true);
    });

    $("#diary-dialog > .diary-scroll > .diary-section a").each(function () {
        $(this).hide();
    });

    $("#lnkNewDiary").hide();
    $("#lnkSaveDiary").hide();
    $(".diary-document-btn").hide();

}
