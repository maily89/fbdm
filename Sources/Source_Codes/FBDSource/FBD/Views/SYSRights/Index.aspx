<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.SystemRights>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Right Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MANAGING SYSTEM RIGHT INDEX</h2>
    <%= TempData["Message"]!=null?TempData["Message"]:"" %>
    <table>
        <tr>
            <th></th>
            <th>
                RightID
            </th>
            <th>
                RightName
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
        <%= Html.ActionLink("Add new System Right", "Add") %>
    </p>

</asp:Content>

