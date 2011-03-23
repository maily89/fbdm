<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.ViewModels.RNKScaleRow>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add Score
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add Score</h2>
    <% using(Html.BeginForm("AddScore","RNKRankBusiness")){ %>
    <table>
        <tr>

            <th>
                RankingID
            </th>
            <th>
                CriteriaID
            </th>
            <th>
                Value
            </th>
        </tr>

    <% for(int i=0;i<Model.Count();i++) { %>
    
        <tr>
            <td>
                <%= Html.Encode(Model.ElementAt(i).RankingID) %>
            </td>
            <td>
                <%= Html.Encode(Model.ElementAt(i).RankingID) %>
            </td>
            <td>
                <%= Html.TextBoxFor(m=>m.ElementAt(i).Value)%>
            </td>
        </tr>
    
    <% } %>

    </table>
    <input type="submit" value="Save" />
<%} %>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

