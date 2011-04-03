<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.IndividualCollateralRanks>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Individual Collateral Rank 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
    <h2>LIST OF INDIVIDUAL COLLATERAL RANK</h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>
    
    <h3>
        <%= Html.ActionLink("Add New", "Add") %>
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
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.RankID }) %> |
                <%= Html.ActionLink("Delete", "Delete", new { id = item.RankID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete " + item.Rank + "?')" })%>
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
        </tr>
    
    <% } %>

    </table>

    <h3>
        <%= Html.ActionLink("Add New", "Add") %>
    </h3>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

