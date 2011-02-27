<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.BusinessFinancialIndexLevels>" %>
<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit financial index level
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>EDIT THE FINANCIAL INDEX LEVEL</h2>
    
    <% Html.EnableClientValidation(); %>
    <p style="color:Red"><%= TempData["Message"] != null ? TempData["Message"] : "" %></p>
    <br />
    
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
                            <%= Html.TextBox("id", Model.LevelID, new { @readonly = "true", @disabled = "true" })%>
                            <%= Html.HiddenFor(model => model.LevelID)%>
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
                        <input type="submit" value="Save" />
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
