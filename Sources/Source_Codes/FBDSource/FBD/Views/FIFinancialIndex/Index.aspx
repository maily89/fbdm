<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.BusinessFinancialIndex>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Financial Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MANAGING FINANCIAL INDEX</h2>
    
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>
    
    <h3>
        <%= Html.ActionLink("Add New Index", "Add") %>
    </h3>
    
    <table>
        <tr>
            <th></th>
            <th>
                Index ID
            </th>
            <th>
                Index Name
            </th>
            <th>
                Unit
            </th>
            <th>
                Formula
            </th>
            <th>
                Value Type
            </th>
            <th>
                Leaf Index
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.IndexID }) %> |
 
                <%= Html.ActionLink("Delete", "Delete", new { id=item.IndexID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete "+item.IndexName+"?');" })%>
            </td>
            <td>
                <%= Html.Encode(item.IndexID) %>
            </td>
            <td>
                <%= Html.Encode(item.IndexName) %>
            </td>
            <td>
                <%= Html.Encode(item.Unit) %>
            </td>
            <td>
                <%= Html.Encode(item.Formula) %>
            </td>
            <td>
                <%= Html.Encode(item.ValueType) %>
            </td>
            <td>
                <%= Html.Encode(item.LeafIndex) %>
            </td>
        </tr>
    
    <% } %>

    </table>
   
    <h3>
        <%= Html.ActionLink("Add New Index", "Add") %>
    </h3>
   
</asp:Content>