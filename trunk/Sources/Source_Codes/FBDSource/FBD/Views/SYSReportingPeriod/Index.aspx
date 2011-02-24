<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.SystemReportingPeriods>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	System reporting periods
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MANAGING REPORTING PERIODS</h2>
    <p class = "message"><%=TempData["Message"]!=null?TempData["Message"]:"" %></p>
    <p>
        <%= Html.ActionLink("Add new Reporting Period", "Add") %>
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
        <%= Html.ActionLink("Add new Reporting Period", "Add") %>
    </p>

</asp:Content>
