<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
<div id="err" class="err-message"> </div>
<table>
<tr>
<td>FromDate :<%= Html.Telerik().DatePicker()
            .Name("DatePicker")
                              .HtmlAttributes(new { id = "fromDate", onchange = "loadPartial()"})
                              .InputHtmlAttributes(new { @readonly = "true"})
                              .Format("dd/MM/yyyy")

            
            %></td>

<td>ToDate :<%= Html.Telerik().DatePicker()
                .Name("ToDate")
                .HtmlAttributes(new { id = "toDate", onchange = "loadPartial()" })
                            .InputHtmlAttributes(new{@readonly = "true"})
                                .Format("dd/MM/yyyy")
                 %></td>
</tr>
</table>
<div id="partial">
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="/Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        function loadPartial() {
            $("#err").html("");
            var strFromDate = $("#fromDate-input").val();
            if (strFromDate != null) {
            var arrFromDate = new Array();
            arrFromDate = strFromDate.split('/');
            strFromDate = arrFromDate[2] + "-" + arrFromDate[1] + "-" + arrFromDate[0]; 
            }
            var strToDate = $("#toDate-input").val();
            if (strFromDate != null) {
            var arrToDate = new Array();
            arrToDate = strToDate.split('/');
            strToDate = arrToDate[2] + "-" + arrToDate[1] + "-" + arrToDate[0]; 
            }
            
            var fromDate = Date.parse(strFromDate);
            var toDate = Date.parse(strToDate);

            if (fromDate > 0 && toDate > 0) {
                if (fromDate - toDate > 0)
                    $("#err").html("Invalid data : ToDate must be later than fromDate");
                else {
                    $("#partial").html("<img src='/Content/images/loading.gif' />");
                    $("#partial").load('/INVMining/Cluster/?fromDate=' + strFromDate+'&toDate='+strToDate);
                }
            }
       }
    </script>

</asp:Content>
