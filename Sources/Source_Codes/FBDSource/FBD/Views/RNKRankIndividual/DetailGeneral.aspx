<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.RNKIndividualViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	General Info
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>General Info</h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>

    <% Html.RenderPartial("DetailStep", FBD.CommonUtilities.Constants.IndividualRankStep.General); %>
    <hr />
    <hr/>
	<table width="100%">
		<tr>
		<td valign="top">
			<b>Ranked Date:<span class="brownText"><%=Model.CustomerInfo.Date.ToShortDateString() %></span></b><br/>
			<b>Branch: <span class="brownText"><%=Model.CustomerInfo.Branch %></span></b><br/>
			<b>CIF Number: <span class="brownText"><%=Model.CustomerInfo.CIF  %></span></b><br/>
			<b>Name: <span class="brownText"><%=Model.CustomerInfo.CustomerName  %></span></b><br/>
			<b>Credit department: <span class="brownText"><%=Model.IndividualRanking.CreditDepartment %></span></b><br />
			<b>Purpose: <span class="brownText"><%=Model.IndividualRanking.IndividualBorrowingPurposes.Purpose %></span></b><br/>
		</td>
		<td valign="top">
			<b>Loan term: <span class="brownText"><%=Model.IndividualRanking.CustomersLoanTerm != null ? Model.IndividualRanking.CustomersLoanTerm.LoanTermName : null%></span></b><br/>
			<b>Total debt: <span class="brownText"><%=Model.IndividualRanking.TotalDebt%></span></b><br/>
			<b>Basic Score: <span class="brownText"><%=Model.IndividualRanking.BasicIndexScore%></span></b><br/>
			<b>Collateral Score: <span class="brownText"><%=Model.IndividualRanking != null ? Model.IndividualRanking.CollateralIndexScore : null%></span></b><br/>
			<b>Rank: <span class="brownText"><%=Model.IndividualRanking.IndividualSummaryRanks != null ? Model.IndividualRanking.IndividualSummaryRanks.Evaluation : null%></span></b><br/>
			<b>User Modified: <span class="brownText"><%=Model.IndividualRanking.UserID  %></span></b><br/>
			<b>Last modified: <span class="brownText"><%=Model.IndividualRanking.DateModified %></span></b><br/>
		</td>
		</tr>
	</table>
	<hr/>
	<table>
	<tr>
	<td><input type="button" value="EDIT THIS INFORMATION" onclick="window.location.href='<%= Url.Action("EditInfo", new { id = ViewData["RankID"] } ) %>';"/></td>
	<% using(Html.BeginForm("Rerank","RNKRankIndividual",new {id=ViewData["RankID"]})){ %>
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

