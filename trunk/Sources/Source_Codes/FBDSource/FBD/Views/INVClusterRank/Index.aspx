<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.IndividualClusterRanks>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
     <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>

    <table>
        <tr>
            <th></th>
            <th>
                ID
            </th>
            <th>
                Rank
            </th>
            <th>
                Centroid(X,Y)
            </th>
            
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.RankID }) %> |
                 <%= Html.ActionLink("Delete", "Delete", new { id = item.RankID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete rank " + item.Rank + "?');" })%>
            </td>
            <td>
                <%= Html.Encode(item.RankID) %>
            </td>
            <td>
                <%= Html.Encode(item.Rank) %>
            </td>
            <td>
                (<%= Html.Encode(String.Format("{0:F}", item.CentroidX)) %>,<%= Html.Encode(String.Format("{0:F}", item.CentroidY)) %>)
            </td>
        </tr>
    
    <% } %>

    </table>
    <%=Html.ActionLink("Add new centroid","Add") %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

