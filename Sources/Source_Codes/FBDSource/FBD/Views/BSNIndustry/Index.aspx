<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.BusinessIndustries>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Business Industries
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>LIST OF BUSINESS INDUSTRIES</h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>
    <h3>
        <%= Html.ActionLink("Add New Industry", "Add") %>
    </h3>
    <table>
        <tr>
            <th></th>
            <th>
                Industry ID
            </th>
            <th>
                Industry Name
            </th>
        </tr>

    <% if (Model!=null)
        foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.IndustryID }) %> |
                <%--<%= Html.ActionLink("Details", "Details", new { id=item.IndustryID })%> | TODO: co can link den list cac business lines?--%>
 
                <%= Html.ActionLink("Delete", "Delete", new { id=item.IndustryID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete "+item.IndustryName+"?');" })%>
            </td>
            <td>
                <%= Html.Encode(item.IndustryID) %>
            </td>
            <td>
                <%= Html.Encode(item.IndustryName) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <h3>
        <%= Html.ActionLink("Add New Industry", "Add") %>
    </h3>

</asp:Content>

