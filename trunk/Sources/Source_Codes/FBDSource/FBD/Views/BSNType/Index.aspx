<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.BusinessTypes>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
    <%= TempData["Message"]!=null?TempData["Message"]:"" %>
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
        <%= Html.ActionLink("Create New", "Add") %>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

