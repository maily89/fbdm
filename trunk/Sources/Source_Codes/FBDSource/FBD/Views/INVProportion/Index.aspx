<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.INVProportionViewModel>" %>
<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Individual Basic Index Propotion
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>INDIVIDUAL BASIC INDEX PROPOTION</h2>

    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%><br /></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%><br /></p>

    <table>
        <tr>
            <% using (Html.BeginForm())
                { %>
            <td>
                <%= Html.Label("Choose Borrowing Purpose") %>
            </td>
        
            <td>                
                <%= Html.DropDownList("BorrowingPP", new SelectList(Model.BorrowingPPs as IEnumerable, "PurposeID", "Purpose", 
                                                            Model.BorrowingPPID), new { onchange = "this.form.submit();" })%>
            </td>
            <% } %>    
        </tr>
        
        <% using (Html.BeginForm())
            { %>
        <tr>            
            <%= Html.HiddenFor(model => model.BorrowingPPID) %>
            <%= Html.Hidden("NumberOfProportionRows", Model.ProportionRows.Count) %>
            <td>
                <%= Html.Label("Basic Index Proportion") %>
            </td>
            
            <td>
                <table>
                    <tr>
                        <th>Enable</th>
                        <th>Index ID</th>
                        <th>Basic Index</th>
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
            </td>
        </tr>
        <% } %>    
    </table>
</asp:Content>
