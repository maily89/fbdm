<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<FBD.ViewModels.RNKFinancialRow>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add Financial Score
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add Financial Score</h2>
    <% using(Html.BeginForm()){ %>
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

    <% for(int i=0;i<Model.Count();i++) { %>
    
        <tr>
            <td>
                <%= Html.Encode(Model[i].Index.IndexID) %>
                <%= Html.HiddenFor(m=>m[i].Index.IndexID )%>
                <%= Html.HiddenFor(m=>m[i].RankingID) %>
                <%= Html.Hidden("Index",i) %>
            </td>
            <td>
                <%= Html.Encode(Model[i].Index.IndexName) %>
                
            </td>
            <td><% if (Model[i].Index.LeafIndex){
                       if (Model[i].Index.ValueType=="N"){%>
                           <%=Html.TextBoxFor(m=>m[i].Score)%>
                       <%}
                       else{%>
                       
                           <%=Html.DropDownListFor(m => m[i].ScoreID, new SelectList(Model[i].ScoreList as IEnumerable,
               "ScoreID", "FixedValue"))%>
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

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

