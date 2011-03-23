<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.BusinessScaleCriteria>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Business Scale Criteria
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MANAGING BUSINESS SCALE CRITERIA</h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p> 
    <h3>
        <%= Html.ActionLink("Add New Criteria", "Add") %>
    </h3>
    <table>
        <tr>
            <th></th>
            <th>
                CriteriaID
            </th>
            <th>
                CriteriaName
            </th>
            <th>
                Unit
            </th>
            <th>
                Formula
            </th>
            <th>
                ValueType
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.CriteriaID }) %> |
                <%= Html.ActionLink("Delete", "Delete", new { id = item.CriteriaID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete " + item.CriteriaName + "?');" })%>
            </td>
            <td>
                <%= Html.Encode(item.CriteriaID) %>
            </td>
            <td>
                <%= Html.Encode(item.CriteriaName) %>
            </td>
            <td>
                <%= Html.Encode(item.Unit) %>
            </td>
            <td>
                <%= Html.Encode(item.Formula) %>
            </td>
            <td>
                <%= Html.Encode(item.ValueType)=="0"?"Character":"Number" %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <h3>
        <%= Html.ActionLink("Add New Criteria", "Add") %>
    </h3>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

