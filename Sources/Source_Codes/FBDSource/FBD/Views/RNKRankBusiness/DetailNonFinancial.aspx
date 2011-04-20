<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.ViewModels.RNKNonFinancialRow>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Detail NonFinancial
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Detail NonFinancial</h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>

    <% Html.RenderPartial("DetailStep", FBD.CommonUtilities.Constants.BusinessRankStep.NonFinancial); %>
    <hr />
        <%Html.RenderPartial("PartNonFinancial", Model); %>

    <hr/>
	<table>
	<tr>
	<td><input type="button" value="EDIT THIS INFORMATION" onclick="window.location.href='<%= Url.Action("AddNonFinancialScore", new { id = ViewData["RankID"] ,Edit="Edit"} ) %>';"/></td>
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

