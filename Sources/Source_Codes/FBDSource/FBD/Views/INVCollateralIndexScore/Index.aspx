<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.INVCollateralIndexScoreViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	individual collateral index score
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>INDIVIDUAL COLLATERAL INDEX SCORE</h2>
<p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>
    
    <table>
        <% using (Html.BeginForm())
            { %>
        <tr>            
            <td>
                <%= Html.Label("Collateral Index") %>
            </td>
        
            <td>                
                <%= Html.DropDownList("Collateral Index", new SelectList(Model.CollateralIndex as IEnumerable, "IndexID", "IndexName", 
                            Model.CollateralIndexID != null ? Model.CollateralIndexID: null), new { onchange = "this.form.submit();" })%>
            </td>                
        </tr>
       
        
        <% } %>
        
        
        <% using (Html.BeginForm())
            { %>
        <tr>            
            <%= Html.HiddenFor(model => model.CollateralIndexID) %>
            
            
            <%= Html.Hidden("NumberOfScoreRows", Model.ScoreRows.Count) %>
            <td>
                <%= Html.Label("Asssign collateral index") %>
            </td>
            
            <td>
                <table>
                    <tr>
                        <th>Enable</th>
                        <th>Level ID</th>
                        <th>From Value</th>
                        <th>To Value</th>
                        <th>Fixed Value</th>
                    </tr>
                
                    <% for (int i = 0; i < Model.ScoreRows.Count(); i++ )
                       { %>
                    <tr>
                        <%= Html.Hidden("Model.ScoreRows.Index",i) %>
                        <%= Html.HiddenFor(model => model.ScoreRows[i].ScoreID) %>
                        <td>
                            <%= Html.CheckBoxFor(model => model.ScoreRows[i].Checked)%>
                        </td>
                        
                        <td>
                            <%= Model.ScoreRows[i].LevelID %>
                            <%= Html.HiddenFor(model => model.ScoreRows[i].LevelID) %>
                        </td>
                        
                        <td>
                            <%= Html.TextBoxFor(model => model.ScoreRows[i].FromValue) %>
                        </td>
                        
                        <td>
                            <%= Html.TextBoxFor(model => model.ScoreRows[i].ToValue) %>
                        </td>
                        
                        <td>
                            <%= Html.TextBoxFor(model => model.ScoreRows[i].FixedValue) %>
                        </td>
                    </tr>
                    <% } %>
                </table>
            </td>            
        </tr>
        
        <tr>
            <td></td>
            
            <td>
                <input type="submit" name="Save" value="Save" />
            </td>
        </tr>
        <% } %>    
    </table>

</asp:Content>
