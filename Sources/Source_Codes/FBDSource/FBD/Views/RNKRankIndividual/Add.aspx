<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.RNKIndividualViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add Individual ranking
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add Individual Ranking</h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>
    <%Html.RenderPartial("IndividualStep",FBD.CommonUtilities.Constants.IndividualRankStep.General); %>
    <%Html.RenderPartial("CustomerInfo", Model.CustomerInfo);%>

    <% using (Html.BeginForm())
                       { %>
        <table width="100%">
        <tr>
        <td><b>Date</b></td>
        <td>
        <%= Html.Telerik().DatePickerFor(model => model.Date).Format("dd-MMM-yyyy")%>
        </td>
        </tr>
        <tr>
        <td><b>Customer List</b></td>
        <td><%= Html.DropDownList("CustomerID", new SelectList(Model.IndividualCustomer as IEnumerable,
                                        "IndividualID", "CustomerName", Model != null ? Model.CustomerID : 0))%>
        </td>
        </tr>
        <tr>
        <td></td>
        <td>
                <input type="submit" value="Rank this customer" />
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
