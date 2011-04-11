<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<FBD.ViewModels.RNKScaleRow>>" %>

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
            <th>
                Score
            </th>
        </tr>

    <% for(int i=0;i<Model.Count;i++) { %>
    
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
                <%= Html.Encode(Model.ElementAt(i).Value)%>
                <%= Html.HiddenFor(m=>m[i].Value) %>
            </td>
            <td>
                <%= Html.Encode(Model.ElementAt(i).Score)%>
            </td>
        </tr>
    
    <% } %>

    </table>
    <hr/>
    <b>
    Total Scale Score:<span class="brownText"><%=ViewData["ScaleScore"] %></span><br />
    Scale:<span class="brownText"><%= ViewData["Scale"] %></span>
    </b>
	<hr/>

