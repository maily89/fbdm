<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.BusinessScales>>" %>

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
                ScaleID
            </th>
            <th>
                FromValue
            </th>
            <th>
                ToValue
            </th>
            <th>
                Scale
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.ScaleID }) %> |
                <%= Html.ActionLink("Delete", "Delete", new { id = item.ScaleID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete " + item.Scale + "?');" })%>
            </td>
            <td>
                <%= Html.Encode(item.ScaleID) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.FromValue)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.ToValue)) %>
            </td>
            <td>
                <%= Html.Encode(item.Scale) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Add New", "Add") %>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

