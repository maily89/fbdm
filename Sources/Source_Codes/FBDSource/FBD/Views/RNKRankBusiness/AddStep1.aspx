<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.RNKPeriodViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add Customer Ranking
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add Customer Ranking</h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>
    <%Html.RenderPartial("BusinessStep",FBD.CommonUtilities.Constants.BusinessRankStep.ChooseCustomer); %>

<% using (Html.BeginForm())
                   { %>
       <table width="100%">
		<tr>
		<td><b>Reporting Period</b></td>
        <td> <%= Html.DropDownList("PeriodID", new SelectList(Model.ReportingPeriods as IEnumerable,
                "PeriodID", "PeriodName", Model != null ? Model.PeriodID : null))%></td>
        </tr>
		<tr>
		<td><b>Business customer</b></td>
		<td><%= Html.DropDownList("CustomerID", new SelectList(Model.BusinessCustomer as IEnumerable,
        "BusinessID", "CustomerName", Model != null ? Model.CustomerID : 0))%></td>
		</tr>
		<tr>
		<td></td>
		<td>
		<input value="Choose this customer" type="submit"/>
        <input type='button' onclick="window.location.href='<%= Url.Action("Index") %>';" value="Cancel" />

		</td>
		</tr>
	</table>

       

<%} %>

    
    
</asp:Content>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>
