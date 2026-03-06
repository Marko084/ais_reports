var NewNCDClaimantQuery = { ClaimNumber: "", QueryType: "", Claimants: new Array() };
var claimNumber;
var ClaimantUserType;
var ClaimantUserID;
var claimantPDReserveAmountTotal;

$(function () {
    $("#lnkCloseClaimant").button();
    $("#lnkNewClaimant").button();
    $("#lnkSaveClaimant").button();

    ClaimantUserType = $(".utype").text();
    ClaimantUserID = $(".uid").text();

});

function GetClaimants(ele) {
    try
    {
        if (typeof ele !== "undefined") {
            claimNumber = $(ele).attr("rel");
        }

        if (policyType !== "WC") {
            $("#lnkNewClaimant").show();
            $("#claimant-dollars-section").show();
        }
        else {
            $("#lnkNewClaimant").hide();
            $("#claimant-dollars-section").hide();
        }

        NewNCDClaimantQuery.ClaimNumber = claimNumber;
        NewNCDClaimantQuery.CompanyID = cid; //$("#lblCID").text();
        NewNCDClaimantQuery.UserID = uid;
        NewNCDClaimantQuery.QueryType = "get";
        NewNCDClaimantQuery.Claimants = new Array();

        var DTO = { 'NewNCDClaimantQuery': NewNCDClaimantQuery };

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/NCDClaimantQuery",
            data: JSON.stringify(DTO),
            dataType: "json",
            beforeSend: function () { },
            error: function (xhr, textStatus, error) {alert("Error: " + xhr.responseText); },
            success: function (data) {
                var record = data.d.Claimants;
 
                ClearClaimantDialogFields();
                //alert("completed ClearClaimantDialogFields()");
                CreateClaimantSections(data.d.Claimants.length);
                //alert("completed CreateClaimantSections()");
                ShowClaimantDialog();

                $.each(record, function (index, item) {
                    try {

                        var claimIdx = index + 1;
                        var claimSection = $("#claimant-dialog > .claimant-scroll > .claimant-section >.claimant-" + claimIdx);
                        var dob = $.datepicker.formatDate("mm/dd/yy", new Date(parseInt(item.DOB.substr(6))));
                        var hireDate = $.datepicker.formatDate("mm/dd/yy", new Date(parseInt(item.HireDate.substr(6))));

                         $("#claimant-dialog > .claimant-scroll > .claimant-section > .claimant-" + claimIdx + "-header").text("Claimant " + claimIdx + " - " + item.Name);
                        $(claimSection).find("input[class='claimant-name']").val(item.Name);

                        if (claimIdx === 1) {
                            $(claimSection).find("input[class='claimant-name']").focus();
                        }

                         $(claimSection).find("textarea[class='claimant-address']").val(item.Address);
                         $(claimSection).find("input[class='claimant-email']").val(item.Email);
                         $(claimSection).find("input[class='claimant-phone1']").val(item.Phone1);
                        $(claimSection).find("input[class='claimant-phone2']").val(item.Phone2);

                        if (dob !== "01/01/1900" && dob !== "1/1/1900") {
                            $(claimSection).find(".claimant-dob").val(dob);
                        }

                        if (hireDate !== "01/01/1900" && hireDate !== "1/1/1900") {
                            $(claimSection).find(".claimant-hiredate").val(hireDate);
                        }
                        
                        $(claimSection).find("select[class='claimant-gender']").val(item.Gender).prop('selected', true);
                        $(claimSection).find("select[class='claimant-maritalstatus']").val(item.MaritalStatus).prop('selected', true);
                        $(claimSection).find("input[class='claimant-nbrofdependents']").val(item.NbrOfDependents);
                        $(claimSection).find("input[class='claimant-jobtitle']").val(item.JobTitle);
                        $(claimSection).find("select[class='claimant-jobstatus']").val(item.JobStatus).prop('selected', true);
                        $(claimSection).find("input[class='claimant-currentwage']").val(item.CurrentWage);
                        $(claimSection).find("select[class='claimant-wagetype']").val(item.WageType).prop('selected', true);
                         $(claimSection).find("input[class='claimant-ssn']").val(item.SSN);
                         //$(claimSection).find("input[class='claimant-reserve-amount']").val(item.ClaimantReserveAmount);
                         //$(claimSection).find("input[class='claimant-allocated-amount']").val(item.ClaimantAllocatedAmount);
                         //$(claimSection).find("input[class='claimant-unallocated-amount']").val(item.ClaimantUnallocatedAmount);
                         $(claimSection).find("input[class='property-damage-reserve-amount']").val(accounting.formatMoney(item.PropertyDamageReserveAmount));
                         $(claimSection).find("input[class='property-damage-paid-amount']").val(accounting.formatMoney(item.PropertyDamagePaidAmount));
                         $(claimSection).find("input[class='property-damage-incurred-amount']").val(accounting.formatMoney(item.PropertyDamageIncurredAmount));
                         $(claimSection).find("input[class='bodily-injury-reserve-amount']").val(accounting.formatMoney(item.BodilyInjuryReserveAmount));
                         $(claimSection).find("input[class='bodily-injury-paid-amount']").val(accounting.formatMoney(item.BodilyInjuryPaidAmount));
                         $(claimSection).find("input[class='bodily-injury-incurred-amount']").val(accounting.formatMoney(item.BodilyInjuryIncurredAmount));
                         $(claimSection).find("input[class='expenses-reserve-amount']").val(accounting.formatMoney(item.ExpensesReserveAmount));
                         $(claimSection).find("input[class='expenses-paid-amount']").val(accounting.formatMoney(item.ExpensesAllocatedAmount));
                         $(claimSection).find("input[class='expenses-incurred-amount']").val(accounting.formatMoney(item.ExpensesIncurredAmount));
                         $(claimSection).find("input[class='claimant-primarykeyfield']").val(item.pkClaimantID);
                         $(claimSection).find("span[class='claimaint-createdby-userid']").val(item.UserID);
                         $(claimSection).find("input[class='claimant-mode']").val("update");
                         $(claimSection).find(".delete-claimant").attr("rel", "claimant-" + claimIdx);

                         $(claimSection).find(".claimant-email-btn").show();

                         if (ClaimantUserType === "Manager") {
                             $(claimSection).find(".delete-claimant").show();
                         }
                         else if (item.UserID === ClaimantUserID) {
                             $(claimSection).find(".delete-claimant").show();
                         }
                         else {
                             $("#claimant-dialog > .claimant-scroll > .claimant-section >.claimant-" + claimIdx + " input").prop("disabled", true);
                         }
                    }
                    catch (e) {
                        console.log(e);
                        alert("Error Loading Claimants: " + e.Message);
                    }
                });

                CalculateIncurredGrandTotal();

                if (readOnlyMode === "true") {
                    SetClaimantReadOnlyMode();
                }
            }
        });
    }
    catch(e)
    {
        alert(e.message);
    }
}

function SaveClaimants()
{
    try
    {
        $("#claimant-dialog >  .claimant-scroll >  .claimant-section > .claimant-fields").each(function () {

            NewNCDClaimantQuery.QueryType = "update";
            NewNCDClaimantQuery.Claimants.push({
                pkClaimantID: $(this).find("input[class='claimant-primarykeyfield']").val(),
                CompanyID: cid, //$("#lblCID").text(),
                ClaimNumber: claimNumber,
                Name: $(this).find("input[class='claimant-name']").val(),
                Address: $(this).find("textarea[class='claimant-address']").val(),
                Email: $(this).find("input[class='claimant-email']").val(),
                Phone1: $(this).find("input[class='claimant-phone1']").val(),
                Phone2: $(this).find("input[class='claimant-phone2']").val(),
                SSN: $(this).find("input[class='claimant-ssn']").val(),
                DOB: ($(this).find(".claimant-dob").val().trim()=== "" ? "1/1/1900" : $(this).find(".claimant-dob").val().trim()),
                HireDate: ($(this).find(".claimant-hiredate").val().trim() === "" ? "1/1/1900" : $(this).find(".claimant-hiredate").val().trim()),
                Gender: $(this).find("select[class='claimant-gender']").val(),
                MaritalStatus: $(this).find("select[class='claimant-maritalstatus']").val(),
                NbrOfDependents: $(this).find("input[class='claimant-nbrofdependents']").val().trim() === "" ? 0 : $(this).find("input[class='claimant-nbrofdependents']").val().trim(),
                JobTitle: $(this).find("input[class='claimant-jobtitle']").val(),
                JobStatus: $(this).find("select[class='claimant-jobstatus']").val(),
                CurrentWage: $(this).find("input[class='claimant-currentwage']").val(),
                WageType: $(this).find("select[class='claimant-wagetype']").val(),
                PropertyDamageReserveAmount: ($(this).find("input[class='property-damage-reserve-amount']").val().trim() == "" ? 0 : accounting.unformat($(this).find("input[class='property-damage-reserve-amount']").val())),
                PropertyDamagePaidAmount: ($(this).find("input[class='property-damage-paid-amount']").val().trim() == "" ? 0 : accounting.unformat($(this).find("input[class='property-damage-paid-amount']").val())),
                PropertyDamageIncurredAmount: ($(this).find("input[class='property-damage-incurred-amount']").val().trim() == "" ? 0 : accounting.unformat($(this).find("input[class='property-damage-incurred-amount']").val())),
                BodilyInjuryReserveAmount: ($(this).find("input[class='bodily-injury-reserve-amount']").val().trim() == "" ? 0 : accounting.unformat($(this).find("input[class='bodily-injury-reserve-amount']").val())),
                BodilyInjuryPaidAmount: ($(this).find("input[class='bodily-injury-paid-amount']").val().trim() == "" ? 0 : accounting.unformat($(this).find("input[class='bodily-injury-paid-amount']").val())),
                BodilyInjuryIncurredAmount: ($(this).find("input[class='bodily-injury-incurred-amount']").val().trim() == "" ? 0 : accounting.unformat($(this).find("input[class='bodily-injury-incurred-amount']").val())),
                ExpensesReserveAmount: ($(this).find("input[class='expenses-reserve-amount']").val().trim() == "" ? 0 : accounting.unformat($(this).find("input[class='expenses-reserve-amount']").val())),
                ExpensesAllocatedAmount: ($(this).find("input[class='expenses-paid-amount']").val().trim() == "" ? 0 : accounting.unformat($(this).find("input[class='expenses-paid-amount']").val())),
                ExpensesIncurredAmount: ($(this).find("input[class='expenses-incurred-amount']").val().trim() == "" ? 0 : accounting.unformat($(this).find("input[class='expenses-incurred-amount']").val())),
                Mode: $(this).find("input[class='claimant-mode']").val(),
                UserID: uid,
                UpdatedBy: $("#ctl00_lblUserInfo").text().replace(/Welcome/i, "").trim()
            });
        });

        var DTO = { 'NewNCDClaimantQuery': NewNCDClaimantQuery };

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../AISWS.asmx/NCDClaimantQuery",
            data: JSON.stringify(DTO),
            dataType: "json",
            beforeSend: function () { },
            error: function (xhr, textStatus, error) { alert("Error: " + xhr.responseText); },
            success: function ()
            {
                alert("Data was successfully saved.");
                GetClaimants();
            }
        });
    }
    catch (e) {
        alert(e.message);
    }
}

function ShowClaimantDialog() {
    $("#claimant-dialog").dialog({
        minWidth: 735,
        //minHeight: 800,
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
            //$(".ui-dialog").find(".ui-widget-header").css("background", "gray");
            $("#claimant-dialog > .claimant-scroll > .claimant-section").accordion("destroy");
        }
    });
}

function ClearClaimantDialogFields() {
    $("#claimant-dialog > .claimant-scroll> .claimant-section").empty();
}

function CreateClaimantSections(claimCount) {

    var claimTemplate = $("#claimant-dialog > .claimant-template");
    var claimSection = $("#claimant-dialog > .claimant-scroll > .claimant-section");
       
    if (claimCount === 0) {
        claimCount = 1;
    }

    for (i = 0; i < claimCount; i++) {
        var template = $(claimTemplate).clone();
        var header = $(template).find("h3");
        var section = $(template).find("div");

        $(header).text("Claimant " + (i + 1).toString());
        $(header).addClass("claimant-" + (i + 1).toString()+"-header");
        $(section).addClass("claimant-" + (i + 1).toString());
        $(section).addClass("claimant-fields");
        $(section).show();
        $(claimSection).append(header);
        $(claimSection).append($(section));
    }
   
    if ($(claimSection).hasClass('ui-accordion')) {
        $(claimSection).accordion('destroy');
    }

    $(claimSection).find("a.claimant-email-btn").button();
    $(claimSection).find("a.delete-claimant").button();
    $(claimSection).accordion({ heightStyle: 'content' });

    SetClaimantInputMask();

    if (claimCount === 1) {
        claimSection = $("#claimant-dialog > .claimant-scroll > .claimant-section >.claimant-1");

        var claimantName = $("#edit-dialog input[data-field-name='ClaimantName']").val();
        $(claimSection).find("input[class='claimant-name']").val(claimantName);
    }
}

function SetClaimantInputMask() {
    $("#claimant-dialog >  .claimant-scroll >  .claimant-section input.claimant-ssn").mask('000-00-0000');

    var ctrlType="";
    $("#claimant-dialog >  .claimant-scroll >  .claimant-section input").each(function () {
        ctrlType = $(this).attr("class").toLowerCase();

        if (ctrlType.indexOf("phone") > -1) {
            $(this).mask('000-000-0000');
        }
        else if(ctrlType.indexOf("amount")>-1) {
            $(this).mask('#,###,000.00', { reverse: true, prefix: "$" });
        }
        else if (ctrlType.indexOf("dob") > -1 || ctrlType.indexOf("hiredate") > -1) {
           // $(this).mask('9/9/9999');
            $(this).datepicker({
                changeMonth: true,
                changeYear: true
            });
        }
    });
}

function SetClaimantReadOnlyMode() {
    $("#claimant-dialog >  .claimant-scroll >  .claimant-section input").each(function () {
        $(this).prop("disabled", true);
    });

    $("#claimant-dialog >  .claimant-scroll >  .claimant-section textarea").each(function () {
        $(this).prop("disabled", true);
    });

    $("#claimant-dialog >  .claimant-scroll >  .claimant-section select").each(function () {
        $(this).prop("disabled", true);
    });

    $("#claimant-dialog >  .claimant-scroll >  .claimant-section a").each(function () {
        $(this).hide();
    });

    $("#lnkNewClaimant").hide();
    $("#lnkSaveClaimant").hide();

}

function AddClaimantSection() {
    var claimSection = $("#claimant-dialog >.claimant-scroll > .claimant-section");
    var claimCount = $("#claimant-dialog > .claimant-scroll > .claimant-section > .claimant-fields").length;
    var template = $("#claimant-dialog > .claimant-template").clone();
    var header = $(template).find("h3");
    var section = $(template).find("div");

    $(header).text("Claimant " + (claimCount + 1).toString());
    $(section).addClass("claimant-" + (claimCount + 1).toString());
    $(section).addClass("claimant-fields");
    $(section).show();
    $(claimSection).append(header);
    $(claimSection).append($(section));
    
    //$(section).find(".delete-claimant").html();
    $(claimSection).accordion("destroy");
    $(claimSection).accordion({ heightStyle: 'content', active: claimCount });

    SetClaimantInputMask();
}

function CloseClaimantDialog() {
    $("#claimant-dialog >  .claimant-scroll > .claimant-section").accordion("destroy");
    $("#claimant-dialog").dialog("close");
}

function DeleteClaimant(ele)
{
    var result = confirm('Are you sure you want to delete?');

    if (result) {

        var claimantSectionName = $(ele).attr("rel");
        var claimSection = $("#claimant-dialog > .claimant-scroll > .claimant-section > ." + claimantSectionName);
        var header = $("#claimant-dialog > .claimant-scroll > .claimant-section > ." + claimantSectionName + "-header");

        $(claimSection).find(".claimant-mode").val("delete");
        $(claimSection).hide();
        $(header).hide();

        SaveClaimants();
    }
}

function GetClaimantEmail(ele) {
    var email=$(ele).parent().parent().find(".claimant-email").val();
    $(ele).attr("rel", email);
}

function CalculateIncurredAmount(ele) {
    var calcField = $(ele).attr("id");
    var paidAmount = "";
    var reserveAmount ="";
    var totalAmount = "";
    var ctrlID = "";

    $(ele).val(accounting.formatMoney($(ele).val()));

    try {
         if (calcField.indexOf("reserve-amount") > -1) {
            ctrlID = $(ele).attr("id").replace("reserve-amount", "paid-amount");

            reserveAmount = accounting.unformat($(ele).val());
            paidAmount = accounting.unformat($(ele).parent().parent().find("." + ctrlID).val());
            totalAmount = reserveAmount + paidAmount;

            ctrlID = ctrlID.replace("paid-amount", "incurred-amount");

            $(ele).parent().parent().find("." + ctrlID).val(accounting.formatMoney(totalAmount));
        }
        else if (calcField.indexOf("paid-amount") > -1) {
            ctrlID = $(ele).attr("id").replace("paid-amount", "reserve-amount");

            reserveAmount = accounting.unformat($(ele).val());
            paidAmount = accounting.unformat($(ele).parent().parent().find("." + ctrlID).val());
            totalAmount = reserveAmount + paidAmount;

            ctrlID = ctrlID.replace("reserve-amount", "incurred-amount");

            $(ele).parent().parent().find("." + ctrlID).val(accounting.formatMoney(totalAmount));
        }
    }
    catch (e) {
        console.log(e.message);
    }
}

function CalculateIncurredGrandTotal() {
    var totalPropertyDamageReserveAmt = 0.00;
    var totalPropertyDamagePaidAmt = 0.00;
    var totalPropertyDamageIncurredAmt = 0.00;
    var totalBodilyInjuryReserveAmt = 0.00;
    var totalBodilyInjuryIncurredAmt = 0.00;
    var totalBodilyInjuryPaidAmt = 0.00;
    var totalExpensesAllocatedAmt = 0.00;
    var totalExpensesIncurredAmt = 0.00;
    var totalExpensesReserveAmt = 0.00;
    var claimantName = "";

    claimantName = $("#claimant-dialog >  .claimant-scroll >  .claimant-section > .claimant-fields input[class=claimant-name]").val();

    $("#claimant-dialog >  .claimant-scroll >  .claimant-section > .claimant-fields input[class=property-damage-reserve-amount]").each(function () {
        totalPropertyDamageReserveAmt += accounting.unformat($(this).val());
    });

    $("#claimant-dialog >  .claimant-scroll >  .claimant-section > .claimant-fields input[class=property-damage-paid-amount]").each(function () {
        totalPropertyDamagePaidAmt += accounting.unformat($(this).val());
    });

    $("#claimant-dialog >  .claimant-scroll >  .claimant-section > .claimant-fields input[class=property-damage-incurred-amount]").each(function () {
        totalPropertyDamageIncurredAmt += accounting.unformat($(this).val());
    });

    //Medical section.
    $("#claimant-dialog >  .claimant-scroll >  .claimant-section > .claimant-fields input[class=bodily-injury-reserve-amount]").each(function () {
        totalBodilyInjuryReserveAmt += accounting.unformat($(this).val());
    });

    $("#claimant-dialog >  .claimant-scroll >  .claimant-section > .claimant-fields input[class=bodily-injury-incurred-amount]").each(function () {
        totalBodilyInjuryIncurredAmt += accounting.unformat($(this).val());
    });

    //totalBodilyInjuryIncurredAmt = totalBodilyInjuryIncurredAmt + GetMedicalCalculations();
    

    $("#claimant-dialog >  .claimant-scroll >  .claimant-section > .claimant-fields input[class=bodily-injury-paid-amount]").each(function () {
        totalBodilyInjuryPaidAmt += accounting.unformat($(this).val());
    });

    $("#claimant-dialog >  .claimant-scroll >  .claimant-section > .claimant-fields input[class=expenses-incurred-amount]").each(function () {
        totalExpensesIncurredAmt += accounting.unformat($(this).val());
    });

    $("#claimant-dialog >  .claimant-scroll >  .claimant-section > .claimant-fields input[class=expenses-paid-amount]").each(function () {
        totalExpensesAllocatedAmt += accounting.unformat($(this).val());
    });

    $("#claimant-dialog >  .claimant-scroll >  .claimant-section > .claimant-fields input[class=expenses-reserve-amount]").each(function () {
        totalExpensesReserveAmt += accounting.unformat($(this).val());
    });

    $("#edit-dialog input[data-field-name='ClaimantPDReserveAmountTotal']").val(accounting.formatMoney(totalPropertyDamageReserveAmt));
    $("#edit-dialog input[data-field-name='ClaimantPDPaidAmountTotal']").val(accounting.formatMoney(totalPropertyDamagePaidAmt));
    $("#edit-dialog input[data-field-name='ClaimantPDIncurredAmountTotal']").val(accounting.formatMoney(totalPropertyDamageIncurredAmt));
    $("#edit-dialog input[data-field-name='ClaimantBIReserveAmountTotal']").val(accounting.formatMoney(totalBodilyInjuryReserveAmt));
    $("#edit-dialog input[data-field-name='ClaimantBIIncurredAmountTotal']").val(accounting.formatMoney(totalBodilyInjuryIncurredAmt));
    $("#edit-dialog input[data-field-name='ClaimantBIPaidAmountTotal']").val(accounting.formatMoney(totalBodilyInjuryPaidAmt));
    $("#edit-dialog input[data-field-name='ClaimantExpenseReserveAmountTotal']").val(accounting.formatMoney(totalExpensesReserveAmt));
    $("#edit-dialog input[data-field-name='ClaimantExpenseAllocatedAmountTotal']").val(accounting.formatMoney(totalExpensesAllocatedAmt));
    $("#edit-dialog input[data-field-name='ClaimantExpenseIncurredAmountTotal']").val(accounting.formatMoney(totalExpensesIncurredAmt));
    $("#edit-dialog input[data-field-name='ClaimantName']").val(claimantName);
    $("#edit-dialog input[data-field-name='InjuredEmployee']").val(claimantName);

    ReloadConvergentClaimsGrid(true);
}