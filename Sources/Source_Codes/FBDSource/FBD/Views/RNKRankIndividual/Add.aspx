<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.RNKIndividualViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add Individual ranking
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add Individual Ranking</h2>

    <% using (Html.BeginForm())
                       { %>

           <p>Date<br/>   
              <%= Html.Telerik().DatePickerFor(model => model.Date).Format("dd-MM-yyyy")%>
                
            </p>

           <p>
           Customer List

            <br/>
                <%= Html.DropDownList("CustomerID", new SelectList(Model.IndividualCustomer as IEnumerable,
                                "IndividualID", "CustomerName", Model != null ? Model.CustomerID : 0))%>
             </p>
          <input type="submit" value="Rank this customer" />

    <%} %>
</asp:Content>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>
