<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.INVCollateralIndexScoreViewModel>" %>
<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
<script src="/Scripts/Tooltip.js" type="text/javascript"></script>

<style type="text/css">
/*<![CDATA[*/

#foo {
     position:absolute;
     width:230px;
     border:solid 1px #000000;
     background:#5C87B2;
     color:Yellow;
     padding:5px;
     font-family:verdana;
     font-size:10px;
     text-align:justify;
     display:none;
 }
/*//]]>*/
</style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Individual Collateral Index Score
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
                        
                        <td  onkeypress="toolTip(<%=(i+1)*30+300 %>,700)">
                            <%= Html.TextBoxFor(model => model.ScoreRows[i].FixedValue)%>
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
<div id="foo"></div>
</asp:Content>
