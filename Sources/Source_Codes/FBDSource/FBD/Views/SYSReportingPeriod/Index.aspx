<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.SystemReportingPeriods>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
    <%=TempData["Message"]!=null?TempData["Message"]:"" %>
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
                <%= Html.ActionLink("Delete", "Delete", new { id=item.PeriodID })%>
            </td>
            <td>
                <%= Html.Encode(item.PeriodID) %>
            </td>
            <td>
                <%= Html.Encode(item.PeriodName) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.FromDate)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.ToDate)) %>
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
