<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FBD.CommonUtilities.Constants.IndividualRankStep>" %>

<h3>In Process</h3>
	<hr/>
	<table width="100%">
		<tr>
			<td class="<%=Model==FBD.CommonUtilities.Constants.IndividualRankStep.ChooseCustomer?"currentStep":"step"%>">Choose Customer</td>
			<td>></td>
			<td class="<%=Model==FBD.CommonUtilities.Constants.IndividualRankStep.General?"currentStep":"step"%>">Input General Info</td>
			<td>></td>
			<td class="<%=Model==FBD.CommonUtilities.Constants.IndividualRankStep.Basic?"currentStep":"step"%>">Basic info</td>
			<td>></td>
			<td class="<%=Model==FBD.CommonUtilities.Constants.IndividualRankStep.Collateral?"currentStep":"step"%>">Collateral Info</td>
			<td>></td>
			<td class="<%=Model==FBD.CommonUtilities.Constants.IndividualRankStep.Ranking?"currentStep":"step"%>">Credit Rank</td>
		</tr>
	</table>
<hr/>
