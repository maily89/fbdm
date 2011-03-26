<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.SystemRights>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Managing System Rights
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MANAGING SYSTEM RIGHTS</h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%><br /></p>
    
    <p>
        <%= Html.ActionLink("Add new right", "Add") %>
    </p>
    <table>
        <tr>
            <th></th>
            <th>
                Right ID
            </th>
            <th>
                Right Name
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.RightID }) %> |
                <%= Html.ActionLink("Delete", "Delete", new { id = item.RightID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete the right name " + item.RightName + "?');" })%>
            </td>
            <td>
                <%= Html.Encode(item.RightID) %>
            </td>
            <td>
                <%= Html.Encode(item.RightName) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Add new right", "Add") %>
    </p>

</asp:Content>

