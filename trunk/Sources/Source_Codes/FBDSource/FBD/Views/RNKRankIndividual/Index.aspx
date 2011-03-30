<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.CustomersIndividualRanking>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Ranking for Individual Customer
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Ranking for Individual Customer</h2>

    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>

    <h3>
    <%= Html.ActionLink("Rank for new customer", "Add") %>
    </h3>
    <table>
        <tr>
            <th></th>
            <th>
                ID
            </th>
            <th>
                Date
            </th>
            <th>
                CreditDepartment
            </th>
            <th>
                TotalDebt
            </th>
            <th>
                BasicIndexScore
            </th>
            <th>
                CollateralIndexScore
            </th>
            <th>
                UserID
            </th>
            <th>
                DateModified
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.ID }) %> |
                <%= Html.ActionLink("Delete", "Delete", new { id=item.ID })%>
            </td>
            <td>
                <%= Html.Encode(item.ID) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.Date)) %>
            </td>
            <td>
                <%= Html.Encode(item.CreditDepartment) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.TotalDebt)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.BasicIndexScore)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.CollateralIndexScore)) %>
            </td>
            <td>
                <%= Html.Encode(item.UserID) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.DateModified)) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <h3>
    <%= Html.ActionLink("Rank for new customer", "Add") %>
    </h3>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

