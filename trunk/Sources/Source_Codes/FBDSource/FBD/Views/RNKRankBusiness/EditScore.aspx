<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<FBD.ViewModels.RNKScaleRow>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit Score
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Score</h2>
    
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>

    <%Html.RenderPartial("CustomerInfo", FBD.ViewModels.RNKCustomerInfo.GetBusinessRankingInfo(System.Convert.ToInt32( Url.RequestContext.RouteData.Values["id"]))); %>

    <% using (Html.BeginForm("EditScoreCalculate","RNKRankBusiness"))
       { %>
        <%=Html.Hidden("rankID", ViewData["RankID"])%>
    <table>
        <tr>

            <th>
                Criteria ID
            </th>
            <th>
                Criteria Name
            </th>
            <th>
                Value
            </th>
        </tr>

    <% for(int i=0;i<Model.Count();i++) { %>
    
        <tr>
            <td>
            
                <%= Html.Encode(Model.ElementAt(i).CriteriaID) %>
                <%= Html.HiddenFor(m=> m[i].CustomerScaleID) %>
                <%= Html.HiddenFor(m=>m[i].RankingID) %>
                <%= Html.HiddenFor(m=>m[i].CriteriaID) %>
                <%= Html.Hidden("Index",i) %>
            </td>
            <td>
                <%= Html.Encode(Model.ElementAt(i).CriteriaName) %>
                <%= Html.HiddenFor(m=>m[i].CriteriaName )%>
            </td>
            <td>
                <%= Html.TextBoxFor(m=>m[i].Value)%>
            </td>
        </tr>
    
    <% } %>

    </table>
    <hr/>
	<hr/>
	<table>
	<tr>
	<td><input value="Calculate Score" type="submit"/></td>
	<td><input value="Cancel" type="button" onclick="window.location.href='<%= Url.Action("DetailScale", new { id = ViewData["RankID"] } ) %>';"/></td>
	</tr>
	</table>
	<br />
	<b>*Calculate:</b> Scale score will be displayed before next step<br />

<%} %>


</asp:Content>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>

