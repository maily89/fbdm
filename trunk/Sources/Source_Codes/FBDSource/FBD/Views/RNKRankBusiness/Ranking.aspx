<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.RNKRankFinal>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Ranking
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Ranking</h2>
    
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>

    <% if (ViewData["Edit"] == null)
       { %>
    <%Html.RenderPartial("BusinessStep", FBD.CommonUtilities.Constants.BusinessRankStep.Ranking);
       } %>
    <%Html.RenderPartial("CustomerInfo", FBD.ViewModels.RNKCustomerInfo.GetBusinessRankingInfo(System.Convert.ToInt32(ViewData["RankID"]))); %>

    <b>
    ScaleScore<span class="brownText"><%= Html.Encode(String.Format("{0:F}", Model.ScaleScore)) %></span><br />
    Scale<span class="brownText"><%= Html.Encode(Model.Scale) %></span><br />
    
    </b>
    <hr />
    <table width="100%">
		<tbody><tr>
			<th>
				<b>Total Financial Score: <span class="brownText"><%= Html.Encode(String.Format("{0:F}", Model.FinancialScore)) %></span></b><br>
				<b>Multiple Proportion: <span class="brownText"><%= Html.Encode(String.Format("{0:F}", Model.FinancialProportion)) %></span></b><br>
				<b>Financial Result: <span class="brownText"><%= Html.Encode(String.Format("{0:F}", Model.FinancialResult)) %></span></b><br>
			</th>
			<th>
				<b>Total Non-Financial Score: <span class="brownText"><%= Html.Encode(String.Format("{0:F}", Model.NonFinancialScore)) %></span></b><br>
				<b>Multiple Proportion: <span class="brownText"><%= Html.Encode(String.Format("{0:F}", Model.NonFinancialProportion)) %></span></b><br>
				<b>Non-Financial Result: <span class="brownText"><%= Html.Encode(String.Format("{0:F}", Model.NonFinancialResult)) %></span></b><br>
			</th>
		</tr>
	</tbody>
	</table>
    <hr />
    <b>
    Total Score<span class="brownText"><%= Html.Encode(String.Format("{0:F}", Model.TotalScore)) %></span><br />
    Class Rank<span class="brownText"><%= Html.Encode(Model.ClassRank) %></span><br />
    Cluster Rank<span class="brownText"><%= Html.Encode(Model.ClusterRank) %></span><input type="button" value="Calculate Cluster Rank" onclick="window.location.href='<%= Url.Action("ClusterOneCustomer", new { id = ViewData["RankID"] }) %>';"/><br />
    </b>    
    <hr />
    <%if (ViewData["Edit"]==null){ %>
    <input type="button" value="OK" onclick="window.location.href='<%= Url.Action("Index" ) %>';"/>   
    <%} else{ %>
    <input type="button" value="OK" onclick="window.location.href='<%= Url.Action(ViewData["redirectAction"].ToString(), new { id = ViewData["RankID"] })%>';"/>  
    <%} %>

        

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

