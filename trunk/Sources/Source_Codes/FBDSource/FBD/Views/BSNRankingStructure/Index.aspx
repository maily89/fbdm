<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.BusinessRankingStructure>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

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
                <%= Html.Encode(item.IndexType) %>
            </td>
            <td>
                <%= Html.Encode(item.AuditedStatus) %>
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

