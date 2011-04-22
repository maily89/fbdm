<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.RNKBusinessIndex>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Ranking for Business Customer
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Ranking for Business Customer</h2>

    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>

        <hr/>
		<h3><b>DISPLAY FILTER</b></h3>
		<% using(Html.BeginForm()){ %>
		<table width="100%">
		<tr>
		<td><b>Reporting Period</b></td>
		<td> <%= Html.DropDownListFor(m=>m.PeriodID, new SelectList(FBD.ViewModels.RNKRankingViewModel.ReportingPeriods as IEnumerable,
                "PeriodID", "PeriodName",Model.PeriodID))%></td>
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
                        <%= Html.ActionLink("Detail", "DetailGeneral", new { id = c.ID },null)%>|
                        <%= Html.ActionLink("Print","ExportBusinessInfo","RPTBusinessReport",  new { id = c.ID },null)%>|
                        <%= Html.ActionLink("Remove", "Delete", new { id = c.ID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete item" + c.ID + "?');" })%>
                        <%
        }).Title("").Width(70);
                columns.Bound(c => c.CustomersBusinesses.CIF).Title("CIF");
                columns.Bound(c => c.CustomersBusinesses.CustomerName).Title("Name");
                columns.Template(c => {%><%=Html.Encode(String.Format("{0:F}", c.BusinessScales!=null?c.BusinessScales.Scale:null)) %><%})
                    .Title("Scale");
                columns.Bound(c=>c.FinancialScore).Title("Financial Score");
                columns.Bound(c=>c.NonFinancialScore).Title("NonFinancial Score");
                columns.Template(c => {%><%= Html.Encode(c.BusinessRanks!=null?c.BusinessRanks.Rank:null) %><%})
                    .Title("Rank");
                columns.Bound(c => c.UserID)
                    .Title("User ID");
            })
            .Sortable()
            .Pageable(p => p.PageSize(20))
            .Render();
    %>
		
    <%--<table>
    
        <tr>
            <th></th>
            <th>
                CIF
            </th>
            <th>
                 Name
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
                Rank
            </th>
            <th>
                UserID
            </th>
            <th>
                DateModified
            </th>
        </tr>

    <% if (Model!=null)
        foreach (var item in Model.CustomerRanking) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Detail", "Edit", new { id=item.ID }) %> |
                <%= Html.ActionLink("Remove", "Delete", new { id=item.ID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete item" + item.ID + "?');" })%>
            </td>
            <td>
                <%= Html.Encode(item.CustomersBusinesses.CIF) %>
            </td>
            <td>
                <%= Html.Encode(item.CustomersBusinesses.CustomerName) %>
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
                <%= Html.Encode(item.BusinessRanks!=null?item.BusinessRanks.Rank:null) %>
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
    <b>Page 1 of 10 | << < 1 > >></b>--%>
	<hr/>
    
    
    <input type="button" value="RANK NEW CUSTOMER" onclick="window.location.href='<%= Url.Action("AddStep1" ) %>';"/>
    
    

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

