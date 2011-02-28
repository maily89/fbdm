<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.SystemBranches>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	System branch
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MANAGING BRANCHES</h2>
    
    <%= TempData["Message"]!=null?TempData["Message"]:"" %>
    <p>
        <%= Html.ActionLink("Add New Branch", "Add") %>
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
                <%= Html.ActionLink("Delete", "Delete", new { id=item.BranchID })%>
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
        <%= Html.ActionLink("Add New Branch", "Add") %>
    </p>

</asp:Content>
