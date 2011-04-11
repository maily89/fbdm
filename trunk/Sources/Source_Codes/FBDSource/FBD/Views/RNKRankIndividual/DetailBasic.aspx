<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.ViewModels.RNKFinancialRow>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Detail Financial
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Detail Financial</h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>
    
    <% Html.RenderPartial("DetailStep", FBD.CommonUtilities.Constants.IndividualRankStep.Basic); %>
    <hr />
        <%Html.RenderPartial("PartBasic", Model); %>
    <hr/>
	<table>
	<tr>
	<td><input type="button" value="EDIT THIS INFORMATION" onclick="window.location.href='<%= Url.Action("AddBasicScore", new { id = ViewData["RankID"], Edit="Edit" } ) %>';"/></td>
	<% using(Html.BeginForm("Rerank","RNKRankBusiness",new {id=ViewData["RankID"]})){ %>
	<td>
	<%=Html.Hidden("redirectAction","DetailGeneral") %>
	<input type="submit" value="RE-CALCULATE RANK" />
	</td>
	<%} %>
	</tr>
	</table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
<script src="/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script> 
</asp:Content>

