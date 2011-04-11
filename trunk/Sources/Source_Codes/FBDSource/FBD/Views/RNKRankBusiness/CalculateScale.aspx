<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<FBD.ViewModels.RNKScaleRow>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= TempData["EditMode"]!=null?"Edit":"Add Score" %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= TempData["EditMode"]!=null?"Edit":"Add Score" %></h2>
    
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>

    <%Html.RenderPartial("CustomerInfo", FBD.ViewModels.RNKCustomerInfo.GetBusinessRankingInfo(System.Convert.ToInt32(ViewData["RankID"]))); %>

    <% using (Html.BeginForm("SaveScore", "RNKRankBusiness", new { id = ViewData["RankID"] }))
       { %>
    <%=Html.Hidden("rankID", ViewData["RankID"])%>
    <%Html.RenderPartial("PartScale", Model); %>
	<table>
	<tr>
	<td><input value="Save Result Only" type="submit"/></td>
	<td><input value="Save Result Then Re-calculate rank Only" type="submit"/></td>
	<td><input value="Back" type="submit" onclick="window.location.href='<%= Url.Action("AddFinancialScore", new { id = Url.RequestContext.RouteData.Values["id"] } ) %>';"/></td>
	</tr>
	</table>
	<br />

<%} %>


</asp:Content>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>

