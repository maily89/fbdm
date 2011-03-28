<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<FBD.ViewModels.RNKScaleRow>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= TempData["EditMode"]!=null?"Edit":"Add Score" %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= TempData["EditMode"]!=null?"Edit":"Add Score" %></h2>
    <% using (Html.BeginForm(TempData["EditMode"] != null ? "EditScale" : "AddScore", "RNKRankBusiness"))
       { %>
    <table>
        <tr>

            <th>
                Criteria ID
            </th>
            <th>
                Criteria Name
            </th>
            <th>
                Score
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
    <input type="submit" value="Save" />
<%} %>


</asp:Content>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>

