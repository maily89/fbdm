<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<FBD.ViewModels.RNKBasicRow>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=TempData["EditMode"] != null ? "Edit Basic Score" : "Add Basic Score"%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=TempData["EditMode"] != null ? "Edit Basic Score" : "Add Basic Score"%></h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>

    <% using (Html.BeginForm(TempData["EditMode"] != null ? "EditBasicScore" : "AddBasicScore", "RNKRankIndividual", new { id = Url.RequestContext.RouteData.Values["id"] }))
       { %>
    <table>
        <tr>
            <th>IndexID</th>
            <th>
                Index Name
            </th>
            <th>
                Value
            </th>
        </tr>

    <% for (int i = 0; i < Model.Count(); i++)
       { %>
    
        <tr>
            <td>
                <%= Html.Encode(Model[i].Index.IndexID)%>
                <%= Html.HiddenFor(m => m[i].Index.IndexID)%>
                <%= Html.HiddenFor(m => m[i].RankingID)%>
                <%= Html.HiddenFor(m=>m[i].LeafIndex) %>
                <%= Html.HiddenFor(m=>m[i].CustomerBasicID) %>
                <%= Html.Hidden("Index", i)%>
            </td>
            <td>
                <%= Html.Encode(Model[i].Index.IndexName)%>
                
            </td>
            <td><% if (Model[i].Index.LeafIndex)
                   {
                       if (Model[i].Index.ValueType == "N")
                       {%>
                           <%=Html.TextBoxFor(m => m[i].Score)%>
                       <%}
                       else
                       {%>
                       
                           <%=Html.DropDownListFor(m => m[i].ScoreID, new SelectList(Model[i].ScoreList as IEnumerable,
               "ScoreID", "FixedValue",Model[i].ScoreID))%>
                       <%}
                   }
            %>
                
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

