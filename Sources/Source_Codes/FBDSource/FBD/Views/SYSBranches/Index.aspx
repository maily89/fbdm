<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.SystemBranches>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Managing System branches
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MANAGING SYSTEM BRANCHES</h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%><br /></p>
    
    <p>
        <%= Html.ActionLink("Add new branch", "Add") %>
    </p>
    <table>
        <tr>
            <th></th>
            <th>BranchID</th>
            <th>BranchName</th>
            <th>Active</th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.BranchID }) %> |
                <%= Html.ActionLink("Delete", "Delete", new { id = item.BranchID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete the branch " + item.BranchName + "?');" })%>
            </td>
            <td>
                <%= Html.Encode(item.BranchID) %>
            </td>
            <td>
                <%= Html.Encode(item.BranchName) %>
            </td>
            <td>
                <%= Html.Encode(item.Active) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Add new branch", "Add") %>
    </p>

</asp:Content>
