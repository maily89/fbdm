<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.SystemReportingPeriods>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Business Clustering</h2>
    <td>
        <%= Html.DropDownList("PeriodReport", new SelectList(Model as IEnumerable, "PeriodID", "PeriodName"), new { onchange = "loadPartial();" })%>
    </td>
    <div id="partial">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

    <script src="/Scripts/jquery-1.4.1.js" type="text/javascript"></script>

    <script type="text/javascript">
        function loadPartial() {
            var ID = $("#PeriodReport").val();
            if (ID > 0) {
                document.getElementById("partial").innerHTML = "<img src='../../Content/images/wait.gif' />";
                $("#partial").load('/BSNMining/Cluster/' + ID);
            }
        }
    </script>

</asp:Content>
