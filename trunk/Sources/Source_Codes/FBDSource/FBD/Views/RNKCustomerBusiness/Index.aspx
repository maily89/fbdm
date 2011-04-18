﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.CustomersBusinesses>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Managing Business Customers
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MANAGING BUSINESS CUSTOMERS</h2>
    
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>
        <h3><b>DISPLAY FILTER</b></h3>
		<% using(Html.BeginForm()){ %>
		<table width="100%">
		<tr>
		<td><b>Branch</b></td>
		<td> <%= Html.DropDownList("BranchID", new SelectList(FBD.ViewModels.RNKRankingViewModel.SystemBranch as IEnumerable,
                                 "BranchID", "BranchName", ViewData["BranchID"]),"All Branches", new { onchange = "this.form.submit();" })%></td>
		</tr>
		</table>
		<%} %>
		<hr/>
    <h3>
    <%= Html.ActionLink("Add New Business Customer", "Add") %>
    </h3>
    <% Html.Telerik().Grid(Model)
           .Name("Grid")
           .Columns(columns =>
               {
                   columns.Template(c =>
                   {%>
                        <%= Html.ActionLink("Edit", "Edit", new { id = c.BusinessID })%> |
                        <%= Html.ActionLink("Delete", "Delete", new { id = c.BusinessID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete " + c.CustomerName + "?');" })%>
<%
        }).Title("").Width(100);
                   columns.Bound(c => c.CIF).Title("CIF");
                   columns.Bound(c => c.CustomerName).Title("Customer Name");
                   
                   
               })
           .Sortable()
           .Pageable(p=> p.PageSize(20))
           .Render();
        
    %>
    <%--<table>
        <tr>
            <th></th>
            <th>
                BusinessID
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
                <%= Html.ActionLink("Edit", "Edit", new { id=item.BusinessID }) %> |
                <%= Html.ActionLink("Delete", "Delete", new { id = item.BusinessID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete " + item.CustomerName + "?');" })%>
            </td>
            <td>
                <%= Html.Encode(item.BusinessID) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.CIF)) %>
            </td>
            <td>
                <%= Html.Encode(item.CustomerName) %>
            </td>
        </tr>
    
    <% } %>

    </table>--%>

    <h3>
        <%= Html.ActionLink("Add New Business Customer", "Add")%>
    </h3>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

