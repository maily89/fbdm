<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.CustomersIndividuals>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Managing Individual Customers
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MANAGING BUSINESS CUSTOMERS</h2>
    
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>

    <h3>
    <%= Html.ActionLink("Add New Individual Customer", "Add") %>
    </h3>
    <table>
        <tr>
            <th></th>
            <th>
                IndividualID
            </th>
            <th>
                CIF
            </th>
            <th>
                CustomerName
            </th>
        </tr>

    <% if (Model!=null)
        foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.IndividualID }) %> |
                <%= Html.ActionLink("Delete", "Delete", new { id = item.IndividualID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete " + item.CustomerName + "?');" })%>
            </td>
            <td>
                <%= Html.Encode(item.IndividualID) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.CIF)) %>
            </td>
            <td>
                <%= Html.Encode(item.CustomerName) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <h3>
        <%= Html.ActionLink("Add New Individual Customer", "Add")%>
    </h3>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

