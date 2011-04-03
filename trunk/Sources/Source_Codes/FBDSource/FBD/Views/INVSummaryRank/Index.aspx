<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.IndividualSummaryRanks>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Individual Summary Rank
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>LIST OF INDIVIDUAL SUMMARY RANKS</h2>
     <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>
    
    <h3>
        <%= Html.ActionLink("Add New", "Add") %>
    </h3>
    <table>
        <tr>
            <th></th>
            <th>
                ID
            </th>
            <th>
                Evaluation
            </th>
            <th>
                Basic Rank
            </th>
            <th>
                Collateral Rank
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.ID }) %> |
                <%= Html.ActionLink("Delete", "Delete", new { id = item.ID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete " + item.Evaluation + "?');" })%>
            </td>
            <td>
                <%= Html.Encode(item.ID) %>
            </td>
            <td>
                <%= Html.Encode(item.Evaluation) %>
            </td>
            <td>
                <%= Html.Encode(item.IndividualBasicRanks.Rank) %>
            </td>
            <td>
                <%= Html.Encode(item.IndividualCollateralRanks.Rank) %>
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

