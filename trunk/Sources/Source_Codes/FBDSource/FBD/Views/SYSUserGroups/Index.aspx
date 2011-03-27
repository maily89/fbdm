<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.SystemUserGroups>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Managing System User Groups
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MANAGING SYSTEM USER GROUPS</h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%><br /></p>
    
    <p>
        <%= Html.ActionLink("Add new group", "Add") %>
    </p>
    <table> 
        <tr>
            <th></th>
            <th>Group ID</th>
            <th>Group Name</th>
        </tr>
        <% foreach (var item in Model)  { %>
        
            <tr>
                <td>
                    <%= Html.ActionLink("Edit", "Edit", new { id = item.GroupID })%> 
                    <%= Html.ActionLink("Delete", "Delete", new { id = item.GroupID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete the group "+item.GroupName+"?');" })%>
                </td>
                <td>
                    <%= Html.Encode(item.GroupID) %>
                </td>
                <td>
                    <%= Html.Encode(item.GroupName) %>
                </td>
            </tr>
        <% } %>
    </table>

    <p>
        <%= Html.ActionLink("Add new group", "Add") %>
    </p>

</asp:Content>


