<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.SystemReportingPeriods>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Managing System reporting periods
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MANAGING SYSTEM REPORTING PERIODS</h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%><br /></p>
    
    <p>
        <%= Html.ActionLink("Add new reporting period", "Add") %>
    </p>
    <table>
        <tr>
            <th></th>
            <th>
                Period ID
            </th>
            <th>
                Period Name
            </th>
            <th>
                From Date
            </th>
            <th>
                To Date
            </th>
            <th>
                Active
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.PeriodID }) %> |
                <%= Html.ActionLink("Delete", "Delete", new { id=item.PeriodID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete "+item.PeriodName+"?');" })%>
            </td>
            <td>
                <%= Html.Encode(item.PeriodID) %>
            </td>
            <td>
                <%= Html.Encode(item.PeriodName) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:dd-MMM-yyyy}", item.FromDate)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:dd-MMM-yyyy}", item.ToDate)) %>
            </td>
            <td>
                <%= Html.Encode(item.Active) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Add new reporting period", "Add") %>
    </p>

</asp:Content>
