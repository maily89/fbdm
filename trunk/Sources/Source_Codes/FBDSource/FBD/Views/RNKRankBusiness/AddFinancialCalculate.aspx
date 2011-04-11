<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<FBD.ViewModels.RNKFinancialRow>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
		View Financial Calculate

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>View Financial Calculate</h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>

    <%if (ViewData["Edit"] == null)
      { %>
    <%Html.RenderPartial("BusinessStep", FBD.CommonUtilities.Constants.BusinessRankStep.Financial);
      } %>
    <%Html.RenderPartial("CustomerInfo", FBD.ViewModels.RNKCustomerInfo.GetBusinessRankingInfo(System.Convert.ToInt32(ViewData["RankID"] ))); %>

    <% using (Html.BeginForm("SaveFinancialScore","RNKRankBusiness"))
       { %>
    <%=Html.Hidden("Edit",ViewData["Edit"]) %>
    <%=Html.Hidden("rankID", ViewData["RankID"])%>
    <%Html.RenderPartial("PartFinancial", Model); %>
	<table>
	<tr>
	<td><input value="Save Result Only" type="submit" name="SaveBack" /></td>
	<%if (ViewData["Edit"] == null)
   {%>
	<td><input value="Save Result Then Process Next" type="submit" name="SaveNext"/></td>
	<%} else{ %>
    <td><input value="Save Result Then Re-calculate rank" type="submit" name="SaveRerank"/></td>
	<%} %>
	<td><input value="Back" type="submit"  name="Back"/></td>
	</tr>
	</table>
	<br />

<%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
