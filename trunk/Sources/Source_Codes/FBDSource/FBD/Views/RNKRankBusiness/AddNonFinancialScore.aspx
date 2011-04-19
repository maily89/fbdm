<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<FBD.ViewModels.RNKNonFinancialRow>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=ViewData["Edit"] != null ? "Edit NonFinancial Score" : "Add NonFinancial Score"%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=ViewData["Edit"] != null ? "Edit NonFinancial Score" : "Add NonFinancial Score"%></h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>
        <%if (ViewData["Edit"] == null)
          { %>
    <%Html.RenderPartial("BusinessStep", FBD.CommonUtilities.Constants.BusinessRankStep.NonFinancial); %>
     <% } %>
    <%Html.RenderPartial("CustomerInfo", FBD.ViewModels.RNKCustomerInfo.GetBusinessRankingInfo(System.Convert.ToInt32(ViewData["RankID"]))); %>
    <% using (Html.BeginForm("AddNonFinancialCalculate", "RNKRankBusiness", new { id = ViewData["RankID"] }))
       { %>
           <%=Html.Hidden("Edit", ViewData["Edit"])%>
           <%=Html.Hidden("rankID", ViewData["RankID"])%>

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
            <td class="<%=!Model[i].LeafIndex?"brownText":"" %>">

                <%= Html.Encode(Model[i].Index.IndexID)%>
                

                <%= Html.HiddenFor(m => m[i].Index.IndexID)%>
                <%= Html.HiddenFor(m => m[i].Index.IndexName)%>
                <%= Html.HiddenFor(m => m[i].Index.ValueType)%>
                <%= Html.HiddenFor(m => m[i].RankingID)%>
                <%= Html.HiddenFor(m => m[i].LeafIndex)%>
                <%= Html.HiddenFor(m => m[i].CustomerScoreID)%>
                <%= Html.Hidden("Index", i)%>
            </td>
            <td class="<%=!Model[i].LeafIndex?"brownText":"" %>">

                <%= Html.Encode(Model[i].Index.IndexName)%>

            </td>
            <td><% if (Model[i].LeafIndex)
                   {
                       if (Model[i].Index.ValueType == FBD.CommonUtilities.Constants.INDEX_NUMERIC)
                       {%>
                           <%=Html.TextBoxFor(m => m[i].Score)%>
                       <%}
                       else
                       {%>
                       
                           <%=Html.DropDownListFor(m => m[i].ScoreID, new SelectList(Model[i].ScoreList as IEnumerable,
               "ScoreID", "FixedValue", Model[i].ScoreID))%>
                       <%}
                   }
            %>
                
            </td>
        </tr>
    
    <% } %>

    </table>
    <table>
	<tr>
	<td><input value="Calculate Result" type="submit"/></td>
	<%if (ViewData["Edit"] == null)
   {%>
	<td><input value="Skip this process" type="button" onclick="window.location.href='<%= Url.Action("Ranking", new { id = ViewData["RankID"] } ) %>';"/></td>
	<%}
   else
   { %>
    <td><input value="Cancel" type="button" onclick="window.location.href='<%= Url.Action("DetailNonFinancial", new { id = ViewData["RankID"] } ) %>';"/></td>
	<%} %>
	</tr>
	</table>
	<br />
	<%if (ViewData["Edit"] == null)
   {%>
	<b>*Calculate:</b> Non Financial score will be displayed before next step<br />
	<b>*Skip:</b> NonFinancial score will get 0 as default

    
    <%}
       }
           %>
</asp:Content>


<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>

