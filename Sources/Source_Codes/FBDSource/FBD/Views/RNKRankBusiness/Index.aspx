<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.CustomersBusinessRanking>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Ranking for Business Customer
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Ranking for Business Customer</h2>

    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>

    <h3>
    <%= Html.ActionLink("Rank for new customer", "AddStep1") %>
    </h3>
    
    <table>
        <tr>
            <th></th>
            <th>
                ID
            </th>
            
            <th>
                Scale
            </th>
            <th>
                FinancialScore
            </th>
            <th>
                NonFinancialScore
            </th>
            <th>
                UserID
            </th>
            <th>
                DateModified
            </th>
        </tr>

    <% if (Model!=null)
        foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.ID }) %> |
                <%= Html.ActionLink("Remove", "Delete", new { id=item.ID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete item" + item.ID + "?');" })%>
            </td>
            <td>
                <%= Html.Encode(item.ID) %>
            </td>

            <td>
                <%= Html.Encode(String.Format("{0:F}", item.BusinessScales!=null?item.BusinessScales.Scale:null)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.FinancialScore)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.NonFinancialScore)) %>
            </td>
            <td>
                <%= Html.Encode(item.UserID) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.DateModified)) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <h3>
        <%= Html.ActionLink("Rank for new customer", "AddStep1") %>
    </h3>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

