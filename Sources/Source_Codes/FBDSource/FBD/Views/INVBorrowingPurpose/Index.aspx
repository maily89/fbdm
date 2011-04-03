<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.IndividualBorrowingPurposes>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Borrowing Purpose
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>LIST OF BORROWING PURPOSES</h2>
     <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>
    
    <h3>
        <%= Html.ActionLink("Create New", "Create") %>
    </h3>
    <table>
        <tr>
            <th></th>
            <th>
                PurposeID
            </th>
            <th>
                Purpose
            </th>
        </tr>

    <%   if (Model!=null){
        foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.PurposeID }) %> |
                <%= Html.ActionLink("Delete", "Delete", new { id = item.PurposeID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete " + item.Purpose + "?');" })%>
            </td>
            <td>
                <%= Html.Encode(item.PurposeID) %>
            </td>
            <td>
                <%= Html.Encode(item.Purpose) %>
            </td>
        </tr>
    
    <% } %>
    <%} %>

    </table>

    <h3>
        <%= Html.ActionLink("Create New", "Create") %>
    </h3>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

