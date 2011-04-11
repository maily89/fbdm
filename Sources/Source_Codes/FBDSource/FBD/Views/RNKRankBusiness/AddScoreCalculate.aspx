<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.ViewModels.RNKScaleRow>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	AddScoreCalculate
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>AddScoreCalculate</h2>

    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>

    <%Html.RenderPartial("BusinessStep",FBD.CommonUtilities.Constants.BusinessRankStep.Scale); %>

    <%Html.RenderPartial("CustomerInfo", FBD.ViewModels.RNKCustomerInfo.GetBusinessRankingInfo(System.Convert.ToInt32(ViewData["RankID"] ))); %>

    <% using (Html.BeginForm("SaveScore","RNKRankBusiness"))
       { %>
    <%=Html.Hidden("rankID", ViewData["RankID"])%>
    <%Html.RenderPartial("PartScale", Model); %>
	<table>
	<tr>
	<td><input value="Save Result Only" type="submit" name="SaveBack" /></td>
	<td><input value="Save Result Then Process Next" type="submit" name="SaveNext"/></td>
	<td><input value="Back" type="submit"  name="Back"/></td>
	</tr>
	</table>
	<br />

<%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

