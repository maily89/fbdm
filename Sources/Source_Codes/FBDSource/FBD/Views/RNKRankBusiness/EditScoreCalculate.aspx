<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.ViewModels.RNKScaleRow>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit ScoreCalculate
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit ScoreCalculate</h2>

    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>


    <%Html.RenderPartial("CustomerInfo", FBD.ViewModels.RNKCustomerInfo.GetBusinessRankingInfo(System.Convert.ToInt32(ViewData["RankID"] ))); %>

    <% using (Html.BeginForm("EditScoreSave","RNKRankBusiness",new{id=ViewData["RankID"]}))
       { %>
    <%=Html.Hidden("rankID", ViewData["RankID"])%>
    <%Html.RenderPartial("PartScale", Model); %>
	<table>
	<tr>
	<td><input value="Save Result Only" type="submit" name="SaveBack" /></td>
	<td><input value="Save Result Then Re-calculate rank" type="submit" name="SaveRerank"/></td>
	<td><input value="Back" type="submit"  name="Back"/></td>
	</tr>
	</table>
	<br />

<%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

