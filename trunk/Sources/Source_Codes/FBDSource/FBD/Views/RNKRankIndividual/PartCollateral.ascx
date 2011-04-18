<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<FBD.ViewModels.RNKCollateralRow>>" %>

    <table>
        <tr>
            <th>
                Index ID
            </th>
            <th>
                Index
            </th>
            <th>
                Value
            </th>

            <%if (ViewData["DetailView"] == null)
              { %>
            <th>
            Score
            </th>
            <%} %>
        </tr>

    <% for (int i=0;i<Model.Count;i++) { %>
    
        <tr>
            <% if (Model.ElementAt(i).LeafIndex)
               { %>
            <td>
                <%= Html.Encode(Model.ElementAt(i).Index.IndexID)%>
                <%= Html.HiddenFor(m => m[i].Index.IndexName)%>
                <%= Html.HiddenFor(m => m[i].Index.IndexID)%>
                <%= Html.HiddenFor(m => m[i].Index.ValueType)%>
                <%= Html.HiddenFor(m => m[i].CustomerScoreID)%>
                <%= Html.HiddenFor(m => m[i].LeafIndex)%>
                <%= Html.HiddenFor(m => m[i].RankingID)%>
                <%= Html.HiddenFor(m => m[i].Score)%>
                <%= Html.HiddenFor(m => m[i].ScoreID)%>
                <%= Html.Hidden("Index", i)%>
                
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", Model.ElementAt(i).Index.IndexName))%>
            </td>
            <td>
                <%=(Model[i].Index.ValueType == "N") ? Html.Encode(Model.ElementAt(i).Score) : Html.Encode(Model[i].Value)%>
            </td>

            <%if (ViewData["DetailView"] == null)
              {%>
                          <td>
                <%= Html.Encode(Model.ElementAt(i).CalculatedScore)%>
            </td>

            <%}
               }
               else
               { %>
            
            <td>
            <b class="brownText">
                <%= Html.Encode(Model.ElementAt(i).Index.IndexID)%>
                <%= Html.HiddenFor(m => m[i].Index.IndexName)%>
                <%= Html.HiddenFor(m => m[i].Index.IndexID)%>
                <%= Html.HiddenFor(m => m[i].CustomerScoreID)%>
                <%= Html.HiddenFor(m => m[i].LeafIndex)%>
                <%= Html.HiddenFor(m => m[i].RankingID)%>
                <%= Html.Hidden("Index", i)%>
            </b>
            </td>
            <td>
            <b class="brownText">
                <%= Html.Encode(String.Format("{0:F}", Model.ElementAt(i).Index.IndexName))%>
            </b>    
            </td>
            <td />
            <%if (ViewData["DetailView"] == null)
              {%>
              <td />

            <%}
               } %>
        </tr>
    
    <% } %>

    </table>
    <hr />
    <b>
    Total Collateral Score:<span class="brownText"><%=ViewData["CollateralScore"] %></span><br />
    Collateral Rank: <span class="brownText"><%= ViewData["CollateralRank"] %></span><br />
    </b>



