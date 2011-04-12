<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FBD.CommonUtilities.Constants.BusinessRankStep>" %>

<table>
		<tbody>
		<tr>
		<td><input type="button" value="General Info" class="<%=Model==FBD.CommonUtilities.Constants.BusinessRankStep.General?"currentStep":"step"%>" onclick="window.location.href='<%= Url.Action("DetailGeneral", new { id = Url.RequestContext.RouteData.Values["id"] } ) %>';"/></td>
		<td><input type="button" value="Scale Info" class="<%=Model==FBD.CommonUtilities.Constants.BusinessRankStep.Scale?"currentStep":"step"%>" onclick="window.location.href='<%= Url.Action("DetailScale", new { id = Url.RequestContext.RouteData.Values["id"] } ) %>';"/></td>
		<td><input type="button" value="Financial Info"  class="<%=Model==FBD.CommonUtilities.Constants.BusinessRankStep.Financial?"currentStep":"step"%>" onclick="window.location.href='<%= Url.Action("DetailFinancial", new { id = Url.RequestContext.RouteData.Values["id"] } ) %>';"/></td>
		<td><input type="button" value="Non Financial Info" class="<%=Model==FBD.CommonUtilities.Constants.BusinessRankStep.NonFinancial?"currentStep":"step"%>" onclick="window.location.href='<%= Url.Action("DetailNonFinancial", new { id = Url.RequestContext.RouteData.Values["id"] } ) %>';"/></td>
		</tr>
	</tbody>
</table>