<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FBD.CommonUtilities.Constants.BusinessRankStep>" %>

<h3>In Process</h3>
	<hr/>
	<table width="100%">
		<tr>
			<td class="<%=Model==FBD.CommonUtilities.Constants.BusinessRankStep.ChooseCustomer?"currentStep":"step"%>">Choose Customer</td>
			<td>></td>
			<td class="<%=Model==FBD.CommonUtilities.Constants.BusinessRankStep.General?"currentStep":"step"%>">Input General Info</td>
			<td>></td>
			<td class="<%=Model==FBD.CommonUtilities.Constants.BusinessRankStep.Scale?"currentStep":"step"%>">Scale Info</td>
			<td>></td>
			<td class="<%=Model==FBD.CommonUtilities.Constants.BusinessRankStep.Financial?"currentStep":"step"%>">Financial Info</td>
			<td>></td>
			<td class="<%=Model==FBD.CommonUtilities.Constants.BusinessRankStep.NonFinancial?"currentStep":"step"%>">Non-Financial Info</td>
			<td>></td>
			<td class="<%=Model==FBD.CommonUtilities.Constants.BusinessRankStep.Ranking?"currentStep":"step"%>">Credit Rank</td>
		</tr>
	</table>
<hr/>
