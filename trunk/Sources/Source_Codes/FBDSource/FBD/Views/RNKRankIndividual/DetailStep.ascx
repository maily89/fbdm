<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FBD.CommonUtilities.Constants.IndividualRankStep>" %>

<table>
		<tbody>
		<tr>
		<td><input type="button" value="General Info" class="<%=Model==FBD.CommonUtilities.Constants.IndividualRankStep.General?"currentStep":"step"%>" onclick="window.location.href='<%= Url.Action("DetailGeneral", new { id = Url.RequestContext.RouteData.Values["id"] } ) %>';"/></td>
		<td><input type="button" value="Basic Info" class="<%=Model==FBD.CommonUtilities.Constants.IndividualRankStep.Basic?"currentStep":"step"%>" onclick="window.location.href='<%= Url.Action("DetailBasic { id = Url.RequestContext.RouteData.Values["id"] } ) %>';"/></td>
		<td><input type="button" value="Collateral Info"  class="<%=Model==FBD.CommonUtilities.Constants.IndividualRankStep.Collateral?"currentStep":"step"%>" onclick="window.location.href='<%= Url.Action("DetailCollateral", new { id = Url.RequestContext.RouteData.Values["id"] } ) %>';"/></td>
		</tr>
	</tbody>
</table>