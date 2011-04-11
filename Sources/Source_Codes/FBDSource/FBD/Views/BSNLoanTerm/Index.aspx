<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.CustomersLoanTerm>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Loan Terms
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>LIST OF LOAN TERMS</h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>

    <h3>
        <%= Html.ActionLink("Add New", "Add") %>
    </h3>
    <table>
        <tr>
            <th></th>
            <th>
                LoanTermID
            </th>
            <th>
                LoanTermName
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.LoanTermID }) %> |
                <%= Html.ActionLink("Delete", "Delete", new { id=item.LoanTermID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete "+item.LoanTermID+"?');" })%>
            </td>
            <td>
                <%= Html.Encode(item.LoanTermID) %>
            </td>
            <td>
                <%= Html.Encode(item.LoanTermName) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <h3>
        <%= Html.ActionLink("Add New", "Add") %>
    </h3>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

