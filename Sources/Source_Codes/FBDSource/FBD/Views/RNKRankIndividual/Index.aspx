<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.RNKIndividualIndex>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Ranking for Individual Customer
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Ranking for Individual Customer</h2>

    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>

    <hr/>
		<h3><b>DISPLAY FILTER</b></h3>
		<% using(Html.BeginForm()){ %>
		<table width="100%">
		<tr>
		<td><b>FromDate</b></td>
		<td> <%=Html.Telerik().DatePickerFor(model => model.FromDate).Format("dd-MMM-yyyy").InputHtmlAttributes(new { @readonly = "true" })%>
        </td>
        <td><b>ToDate</b></td>
		<td> <%=Html.Telerik().DatePickerFor(model => model.ToDate).Format("dd-MMM-yyyy").InputHtmlAttributes(new { @readonly = "true" })%>
        </td>
		</tr>
		<tr>
		<td><b>Branch</b></td>
		<td> <%= Html.DropDownListFor(m=>m.BranchID, new SelectList(FBD.ViewModels.RNKRankingViewModel.SystemBranch as IEnumerable,
                "BranchID", "BranchName",Model.BranchID))%></td>
		</tr>
		<tr>
		<td><b>Số CIF</b></td>
		<td><%= Html.TextBoxFor(m=>m.Cif) %></td>
		</tr>
		<tr>
		<td></td><td><input type="submit" value="Search" /></td>
		</tr>
		</table>
		<%} %>
		<hr/>
		<hr/>
		<%
        Html.Telerik().Grid(Model.CustomerRanking)
            .Name("Grid")
            .Columns(columns =>
            {
                columns.Template(c =>
                    {%>
                        <%= Html.ActionLink("Detail", "DetailGeneral", new { id = c.ID })%> |
                        <%= Html.ActionLink("Print Report", "ExportIndividualInfo", "RPTIndividualReport", new { id = c.ID },null)%> |
                        <%= Html.ActionLink("Remove", "Delete", new { id = c.ID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete item" + c.ID + "?');" })%>
                        <%
        }).Title("").Width(100);
                columns.Bound(c => c.CustomersIndividuals.CIF).Title("CIF");
                columns.Bound(c => c.CustomersIndividuals.CustomerName).Title("Name");
                columns.Bound(c => c.Date).Title("Date");
                columns.Bound(c => c.CreditDepartment).Title("Credit Department");
                columns.Bound(c => c.TotalDebt).Title("Total Debt");
                columns.Bound(c => c.CollateralIndexScore).Title("Collateral Index Score");
                columns.Bound(c => c.BasicIndexScore).Title("Basic Index Score");                
                columns.Template(c => {%><%= Html.Encode(c.IndividualSummaryRanks!=null?c.IndividualSummaryRanks.Evaluation:null) %><%})
                    .Title("Rank");
                columns.Bound(c => c.UserID)
                    .Title("User ID");
                columns.Bound(c=>c.DateModified).Title("Date Modified");
            })
            .Sortable()
            .Pageable(p => p.PageSize(20))
            .Render();
    %>
    <hr />
        <input type="button" value="RANK NEW CUSTOMER" onclick="window.location.href='<%= Url.Action("Add" ) %>';"/>

    <%--<table>
        <tr>
            <th></th>
            <th>
                ID
            </th>
            <th>
                Date
            </th>
            <th>
                CreditDepartment
            </th>
            <th>
                TotalDebt
            </th>
            <th>
                BasicIndexScore
            </th>
            <th>
                CollateralIndexScore
            </th>
            <th>
                UserID
            </th>
            <th>
                DateModified
            </th>
        </tr>

    <% if(Model!=null)
        foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.ID }) %> |
                <%= Html.ActionLink("Delete", "Delete", new { id=item.ID })%>
            </td>
            <td>
                <%= Html.Encode(item.ID) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.Date)) %>
            </td>
            <td>
                <%= Html.Encode(item.CreditDepartment) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.TotalDebt)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.BasicIndexScore)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.CollateralIndexScore)) %>
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
    <%= Html.ActionLink("Rank for new customer", "Add") %>
    </h3>--%>
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>

