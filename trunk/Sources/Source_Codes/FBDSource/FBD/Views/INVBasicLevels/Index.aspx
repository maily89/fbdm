<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.IndividualBasicIndexLevels>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Individual Basic Index Level
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Individual Basic Index Level</h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>
    
    <h3><%= Html.ActionLink("Create New", "Create") %></h3>
    <table>
        <tr>
            <th></th>
            <th>
                LevelID
            </th>
            <th>
                Score
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.LevelID }) %> |
                <%= Html.ActionLink("Delete", "Delete", new { id = item.LevelID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete levels " + item.LevelID + "?');" })%>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.LevelID)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.Score)) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <h3>
        <%= Html.ActionLink("Create New", "Create") %>
    </h3>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

