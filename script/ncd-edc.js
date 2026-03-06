var UserType;
var UserID;

$(function () {
    UserType = $(".utype").text();
    UserID = $(".uid").text();

    SetInputMask();
    SetPickLists();
});

function SetInputMask() {
    //$("#edit-dialog >  .edit-field > input ").mask('000-00-0000');
    //alert($("#edit-dialog > div > .edit-field > input").length);
    var ctrlType="";
    $("#edit-dialog > div > .edit-field > input").each(function () {

        ctrlType = $(this).attr("data-field-name").toLowerCase();
        //alert($(this).attr("data-field-name"));
        //if ($(this).attr("data-field-name").length > 0) {
        //    alert(ctrlType);
        //}
        if (ctrlType.indexOf("phone") > -1) {
            $(this).mask('000-000-0000');
        }
        else if(ctrlType.indexOf("amount")>-1) {
            $(this).mask('#,###,000.00', { reverse: true, prefix: "$" });
        }
        else if (ctrlType.indexOf("date") > -1) {
            $(this).mask('99/999/9999');
            $(this).datepicker({
                changeMonth: true,
                changeYear: true
            });
        }
    });
}

function SetPickLists() {

    $("#edit-dialog > div > .edit-field> input[class*='aria-pick-list']").each(function (idx, ele) {
  
        var pickListName = $(ele).attr("aria-pick-list-name");

        $(ele).autocomplete({
            source: getPickList(pickListName),
            minLength: 0,
            select: function (evt, b) {
                if ($(ele).attr("data-field-name") == "PolicyType") {
                    HideFieldsByPolicyType(b.item.value);
                }
                $(this).autocomplete("close");
            }
        }).click(function () {
            $(this).autocomplete("search", "");
        });
    });
}
