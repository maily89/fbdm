<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.RNKBusinessRankingViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	General Info
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>General Info</h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>

    <% Html.RenderPartial("DetailStep", FBD.CommonUtilities.Constants.BusinessRankStep.General); %>
    <hr />
    <hr/>
	<table width="100%">
		<tr>
		<td valign="top">
			<b>Reporting period: <span class="brownText"><%=Model.BusinessRanking.SystemReportingPeriods!=null?Model.BusinessRanking.SystemReportingPeriods.PeriodName:null %></span></b><br/>
			<b>Branch: <span class="brownText"><%=Model.CustomerInfo.Branch %></span></b><br/>
			<b>CIF: <span class="brownText"><%=Model.CustomerInfo.CIF  %></span></b><br/>
			<b>Name: <span class="brownText"><%=Model.CustomerInfo.CustomerName  %></span></b><br/>
			<b>Industry: <span class="brownText"><%=Model.BusinessRanking.BusinessIndustries!=null?Model.BusinessRanking.BusinessIndustries.IndustryName:null  %></span></b><br/>
			<b>Business type: <span class="brownText"><%=Model.BusinessRanking.BusinessTypes!=null?Model.BusinessRanking.BusinessTypes.TypeName:null  %></span></b><br/>
			<b>Tax code: <span class="brownText"><%=Model.BusinessRanking.TaxCode  %></span></b><br/>
			<b>Customer Type: <span class="brownText"><%=Model.BusinessRanking.SystemCustomerTypes!=null?Model.BusinessRanking.SystemCustomerTypes.TypeName:null  %></span></b><br/>
			<b>Customer group: <span class="brownText"><%=Model.BusinessRanking.CustomerGroup  %></span></b><br/>
			<b>Credit department: <span class="brownText"><%=Model.BusinessRanking.CreditDepartment %></span></b>
		</td>
		<td valign="top">
			<b>Loan term: <span class="brownText"><%=Model.BusinessRanking.CustomersLoanTerm!=null?Model.BusinessRanking.CustomersLoanTerm.LoanTermName:null %></span></b><br/>
			<b>Audited status: <span class="brownText"><%=Model.BusinessRanking.AuditedStatus %></span></b><br/>
			<b>Total debt: <span class="brownText"><%=Model.BusinessRanking.TotalDebt %></span></b><br/>
			<b>Scale: <span class="brownText"><%=Model.BusinessRanking.BusinessScales!=null?Model.BusinessRanking.BusinessScales.Scale:null %></span></b><br/>
			<b>Financial Score: <span class="brownText"><%=Model.BusinessRanking.FinancialScore %></span></b><br/>
			<b>Non Financial Score: <span class="brownText"><%=Model.BusinessRanking.NonFinancialScore %></span></b><br/>
			<b>Total Score: <span class="brownText"><%=Model.TotalScore %></span></b><br/>
			<b>Rank: <span class="brownText"><%=Model.BusinessRanking.BusinessRanks!=null?Model.BusinessRanking.BusinessRanks.Rank:null  %></span></b><br/>
			<b>User: <span class="brownText"><%=Model.BusinessRanking.UserID  %></span></b><br/>
			<b>Last modified: <span class="brownText"><%=Model.BusinessRanking.DateModified %></span></b><br/>
		</td>
		</tr>
	</table>
	<hr/>
	<table>
	<tr>
	<td><input type="button" value="EDIT THIS INFORMATION" onclick="window.location.href='<%= Url.Action("EditInfo", new { id = ViewData["RankID"] } ) %>';"/></td>
	<% using(Html.BeginForm("Rerank","RNKRankBusiness",new {id=ViewData["RankID"]})){ %>
	<td>
	<%=Html.Hidden("redirectAction","DetailGeneral") %>
	<input type="submit" value="RE-CALCULATE RANK" onclick = "javascript:return confirm('Recalculate rank will overwrite current class rank. Do you want to continue?');"/>
	</td>
	<%} %>
	</tr>
	</table>
    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
<script src="/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script> 
</asp:Content>

