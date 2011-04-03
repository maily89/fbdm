<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.NFIProportionViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	NFI Proportion By Business Industry
</asp:Content>
<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/CheckProportion.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>NON-FINANCIAL INDEX PROPORTION BY BUSINESS INDUSTRIES</h2>

    <% Html.EnableClientValidation(); %>    
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>

    <table>
        <tr>
            <% using (Html.BeginForm())
                { %>
            <td>
                <%= Html.Label("Choose an industry") %>
            </td>
        
            <td>                
                <%= Html.DropDownList("Industry", new SelectList(Model.Industries as IEnumerable, "IndustryID", "IndustryName", 
                                                            Model.IndustryID), new { onchange = "this.form.submit();" })%>
            </td>
            <% } %>    
        </tr>
        
        <% using (Html.BeginForm())
            { %>
        
        <tr>            
            <%= Html.HiddenFor(model => model.IndustryID) %>
            <%= Html.Hidden("NumberOfProportionRows", Model.ProportionRows.Count) %>
            <td>
                <%= Html.Label("Non-Financial Index Proportion") %>
            </td>
            
            <td>
                <table>
                    <tr>
                        <th>Enable</th>
                        <th>Index ID</th>
                        <th>Non-Financial Index</th>
                        <th>Proportion</th>
                    </tr>
                
                    <% for (int i = 0; i < Model.ProportionRows.Count(); i++ )
                       { %>
                    <tr>
                        <%= Html.Hidden("Model.ProportionRows.Index",i) %>
                        <%= Html.HiddenFor(model => model.ProportionRows[i].ProportionID) %>
                        <td>                            
                            <% if (Model.ProportionRows[i].LeafIndex == true)
                               { %>
                            <%= Html.CheckBoxFor(model => model.ProportionRows[i].Checked)%>
                            <% }
                               else
                               {%>
                                +
                            <% } %>
                        </td>
                        
                        <td>
                            <%= Model.ProportionRows[i].IndexID %>
                            <%= Html.HiddenFor(model => model.ProportionRows[i].IndexID) %>
                        </td>
                        
                        <td>
                            <%= Model.ProportionRows[i].IndexName %>
                            <%= Html.HiddenFor(model => model.ProportionRows[i].IndexName) %>
                        </td>
                        
                        <td>
                            <% if (Model.ProportionRows[i].LeafIndex == false)
                               { %>
                                    100%
                            <% }
                               else
                               {%>
                                <%= Html.TextBoxFor(model => model.ProportionRows[i].Proportion) %> %
                            <% } %>
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
                <input type="button" value="Check Total Proportion" onclick="javascript:alert(CheckTotalNFIProportionByIndustry());" />
            </td>
        </tr>
        <% } %>    
    </table>
</asp:Content>
                        
