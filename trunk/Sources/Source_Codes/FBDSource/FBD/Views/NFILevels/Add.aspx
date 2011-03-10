<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.BusinessNonFinancialIndexLevels>" %>
<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add non-financial index level
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ADD NEW A NON-FINANCIAL INDEX LEVEL</h2>
    
    <% Html.EnableClientValidation(); %>    
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            <table>
                <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.LevelID) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBoxFor(model => model.LevelID) %>
                            <%= Html.ValidationMessageFor(model => model.LevelID) %>
                        </div>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.Score) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBoxFor(model => model.Score)%>
                            <%= Html.ValidationMessageFor(model => model.Score)%>
                        </div>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <input type="submit" value="Add" />
                    </td>
                    <td>
                        <input type='button' onclick="window.location.href='<%= Url.Action("Index") %>';" value="Cancel" />
                    </td>
                </tr>
            </table>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>
