<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.RNKRankIndividualFinal>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Ranking
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Ranking</h2>
     <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>

    <%if (ViewData["Edit"] == null)
      { %>
    <%Html.RenderPartial("IndividualStep", FBD.CommonUtilities.Constants.IndividualRankStep.Ranking);
      } %>
    <%Html.RenderPartial("CustomerInfo", FBD.ViewModels.RNKCustomerInfo.GetIndividualRankingInfo(System.Convert.ToInt32(ViewData["RankID"] ))); %>


    <table width="100%">
		<tbody><tr>
			<th>
				<b>Total Basic Score: <span class="brownText"><%= Html.Encode(String.Format("{0:F}", Model.BasicScore)) %></span></b><br>
				<b>Basic Rank: <span class="brownText"><%= Html.Encode(String.Format("{0:F}", Model.BasicRank)) %></span></b><br>
			</th>
			<th>
				<b>Total Non-Basic Score: <span class="brownText"><%= Html.Encode(String.Format("{0:F}", Model.CollateralScore)) %></span></b><br>
				<b>Non-Basic Rank: <span class="brownText"><%= Html.Encode(String.Format("{0:F}", Model.CollateralRank)) %></span></b><br>
			</th>
		</tr>
	</tbody>
	</table>
    <hr />
    <b>
    Class Rank<span class="brownText"><%= Html.Encode(Model.ClassRank) %></span><br />
    Cluster Rank<span class="brownText"><%= Html.Encode(Model.ClusterRank) %></span><input type="button" value="Calculate Cluster Rank" /><br />
    </b>    
    <hr />
    <%if (ViewData["Edit"]==null){ %>
    <input type="button" value="OK" onclick="window.location.href='<%= Url.Action("Index" ) %>';"/>   
    <%} else{ %>
    <input type="button" value="OK" onclick="window.location.href='<%= Url.Action(ViewData["redirectAction"]!=null?ViewData["redirectAction"].ToString():"Index", new { id = ViewData["RankID"] })%>';"/>  
    <%} %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

