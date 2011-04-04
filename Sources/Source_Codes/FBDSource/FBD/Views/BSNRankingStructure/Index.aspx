<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.BusinessRankingStructure>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Ranking Structure
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>RANKING STRUCTURE OF BANK</h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>
    <table>
        <tr>
            <th></th>

            <th>
                IndexType
            </th>
            <th>
                AuditedStatus
            </th>
            <th>
                Percentage
            </th>
        </tr>

    <% if (Model!=null)
        foreach (var item in Model) { %>
    
        <tr>
            <td>
               <%= Html.ActionLink("Edit", "Edit", new { id=item.ID }) %> |
            </td>
            <td>
                <%= Html.Encode(item.IndexType == FBD.CommonUtilities.Constants.RNK_STRUCTURE_FINANCIAL_INDEX ? "Financial Index" : "NonFinancial Index")%>
            </td>
            <td>
                <%= Html.Encode(item.AuditedStatus == FBD.CommonUtilities.Constants.RNK_STRUCTURE_AUDITED ? "Audited" : "Not Audited")%>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.Percentage)) %>
            </td>
        </tr>
    
    <% } %>

    </table>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

