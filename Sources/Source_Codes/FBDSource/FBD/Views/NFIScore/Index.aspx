<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.NFIScoreViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Non-Financial Index Score
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MANAGE NON-FINANCIAL INDEX SCORE</h2>

    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>

    <table>
        <% using (Html.BeginForm())
            { %>
        <tr>            
            <td>
                <%= Html.Label("Business Industry") %>
            </td>
        
            <td>                
                <%= Html.DropDownList("Industry", new SelectList(Model.Industries as IEnumerable, "IndustryID", "IndustryName", 
                            Model.IndustryID != null ? Model.IndustryID : null), new { onchange = "this.form.submit();" })%>
            </td>                
        </tr>
        
        <tr>            
            <td>
                <%= Html.Label("Non-Financial index") %>
            </td>
        
            <td>                
                <%= Html.ListBox("NonFinancialIndex",new SelectList(Model.NonFinancialIndexes as IEnumerable,"IndexID","IndexName",
                                    Model.IndexID != null ? Model.IndexID : null),new {onchange="this.form.submit();",
                                                    style="width:400px"})%>
            </td>                
        </tr>
        <% } %>
        
        
        <% using (Html.BeginForm())
            { %>
        <tr>            
            <%= Html.HiddenFor(model => model.IndustryID) %>
            <%= Html.HiddenFor(model => model.IndexID) %>
            
            <%= Html.Hidden("NumberOfScoreRows", Model.ScoreRows.Count) %>
            <td>
                <%= Html.Label("Non-Financial Index Score") %>
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