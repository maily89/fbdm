<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.BusinessRanks>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Business Rank
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MANAGING BUSINESS RANK</h2>
    <div style="color:Red"><%= TempData["Message"]!=null?TempData["Message"]:"" %></div>
    <h3>
        <%= Html.ActionLink("Add New Business Rank", "Add") %>
    </h3>
    <table>
        <tr>
            <th></th>
            <th>
                RankID
            </th>
            <th>
                FromValue
            </th>
            <th>
                ToValue
            </th>
            <th>
                Rank
            </th>
            <th>
                Evaluation
            </th>
            <th>
                RiskGroup
            </th>
            <th>
                DebtGroup
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.RankID }) %> |
                <%= Html.ActionLink("Delete", "Delete", new { id = item.RankID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete " + item.Rank + "?');" })%>
            </td>
            <td>
                <%= Html.Encode(item.RankID) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.FromValue)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.ToValue)) %>
            </td>
            <td>
                <%= Html.Encode(item.Rank) %>
            </td>
            <td>
                <%= Html.Encode(item.Evaluation) %>
            </td>
            <td>
                <%= Html.Encode(item.RiskGroup) %>
            </td>
            <td>
                <%= Html.Encode(item.DebtGroup) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <h3>
        <%= Html.ActionLink("Add New", "Add") %>
    </h3>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

