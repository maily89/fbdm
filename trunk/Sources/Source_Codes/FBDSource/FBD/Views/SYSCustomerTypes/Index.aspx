<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.SystemCustomerTypes>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Managing System customer types
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MANAGING SYSTEM CUSTOMER TYPES</h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%><br /></p>
    
   <p>
        <%= Html.ActionLink("Add new customer type", "Add") %>
    </p>

    <table>
        <tr>
            <th></th>
            <th>
                Type ID
            </th>
            <th>
                Type Name
            </th>
        </tr>
 
    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.TypeID }) %> |
                <%= Html.ActionLink("Delete", "Delete", new { id = item.TypeID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete " + item.TypeName + "?');" })%>
            </td>
            <td>
                <%= Html.Encode(item.TypeID) %>
            </td>
            <td>
                <%= Html.Encode(item.TypeName) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Add new customer type", "Add") %>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

