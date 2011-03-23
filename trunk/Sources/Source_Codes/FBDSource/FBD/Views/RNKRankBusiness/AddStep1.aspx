<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.RNKPeriodViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add Customer Ranking
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add Customer Ranking</h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>

    
<% using (Html.BeginForm())
                   { %>

       <p>Reporting Period<br/>   
          <%= Html.DropDownList("PeriodID", new SelectList(Model.ReportingPeriods as IEnumerable,
                "PeriodID", "PeriodName", Model != null ? Model.PeriodID : null))%>
            

        </p>

       <p>
       Customer List

        <br/>
            <%= Html.DropDownList("CustomerID", new SelectList(Model.BusinessCustomer as IEnumerable,
        "BusinessID", "CustomerName", Model != null ? Model.CustomerID : 0))%>
         </p>
      <input type="submit" value="Rank this customer" />

<%} %>

    
    
</asp:Content>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>
