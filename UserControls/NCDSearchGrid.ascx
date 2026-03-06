<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDSearchGrid.ascx.cs" Inherits="AISReports.UserControls.NCDSearchGrid" %>
<script type="text/javascript">

    $(document).ready(function () {
        $(".slide-out-div input").each(function () {
            //alert($(this).attr("data-table-type"));
            try {
                if ($(this).attr("data-table-type").toLowerCase() == "question") {
                    loadQuestions();
                    $("#search-grid-filter-section").addClass("search-grid-filter");
                    $("#search-grid-filter-section").show();
                    $(this).hide();
                    $(this).prev().hide();
                }

            }
            catch (e) {
                //console.log(e.message);
            }
        });

        //alert("here!!");
        //$(".grid-drilldown").fancybox({
        //    width: "50%",
        //    height: "50%",
        //    type: "iframe"
        //});
    });

    function getSearchGridFilterResults(select) {
        $(".slide-out-div input").each(function () {
            try {
                if ($(this).attr("data-table-type").toLowerCase() == "question") {
                    $(this).val($(select).val());
                    getResultByFilter();
                }
            }
            catch (e) {
                //console.log(e.message);
            }
        });

    }

    function loadQuestions() {
        try {
            var url = "../ListHandler.ashx?cid=" + cid + "&uid=" + uid + "&consolidated=" + consolidated + "&fn=Question&tn=Question&qt=lookup";
            $.getJSON(url, function (data) {
                $.each(data, function (index, item) {
                    $('#search-grid-dropdown-filter').append($('<option></option>').val(item.value).html(item.label));
                });
            });
        }
        catch (e) {
            //alert(e.message);
        }
    }

    function ShowDetails(ele) {
       
        $.fancybox({
            'height': '600',
            'width': '300',
            'href': ele.href,
            'type': 'iframe',
            autoSize: false
        });

        //$.fancybox({ live: false });

        return false;
    }
</script>
<style type="text/css">
    .search-grid-filter 
    {
        display:inline-block;
        position:relative;
        top:40px;
        z-index:9000;
        height:0px;
        width:100%;
        text-align:center;
    }

    .DTTT_button span {color:#fff !important;}
    .highlighted-text {font-weight:bold; color:red;}
    
    #search-grid-dropdown-filter
    {
        width:350px;
    }
</style>
<div class="user-control-widget">
    <div class="chart-title">
        <asp:Label runat="server" ID="lblChartTitle" Text="" />
    </div>
    <asp:Panel runat="server" ID="pChart" class="grid-section">
        <div id="search-grid-filter-section" style="display:none;" >
            <span>Sort by Question:</span>
            <select id="search-grid-dropdown-filter" onchange="javascript: getSearchGridFilterResults(this);">
                <option value="">None</option>
            </select>
        </div>
        <table id="search-grid" class="grid-control">
            <thead>
                <tr>
                    <th></th>
                    <asp:Literal runat="server" ID="litHeaderRow" EnableViewState="false"></asp:Literal>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <asp:Literal runat="server" ID="litNoData" EnableViewState="false" ></asp:Literal>
                </tr>
            </tbody>
        </table>
        <span class="search-grid-message" style="display:block; color:#fff;"></span>
    </asp:Panel>
    <div style="display:none;">
        <div id="record-details">
            <span>Hello!</span>
        </div>
    </div>
</div>