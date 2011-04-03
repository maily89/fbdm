<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.BusinessFinancialIndexLevels>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Financial Index Level
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>LIST OF FINANCIAL INDEX LEVELS</h2>
    
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>
    
    <h3>
        <%= Html.ActionLink("Add New Level", "Add") %>
    </h3>
    
    <table>
        <tr>
            <th></th>
            <th>
                Level ID
            </th>
            <th>
                Score
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.LevelID }) %> |
 
                <%= Html.ActionLink("Delete", "Delete", new { id=item.LevelID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete "+item.LevelID+"?');" })%>
            </td>
            <td>
                <%= Html.Encode(item.LevelID) %>
            </td>
            <td>
                <%= Html.Encode(item.Score) %>
            </td>
        </tr>
    
    <% } %>

    </table>
    
    <h3>
        <%= Html.ActionLink("Add New Level", "Add") %>
    </h3>

</asp:Content>
