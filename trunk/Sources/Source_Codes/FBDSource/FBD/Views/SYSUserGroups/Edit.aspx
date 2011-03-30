<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.SystemUserGroups>" %>
<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit user group
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>EDIT USER GROUP</h2>
    
    <% Html.EnableClientValidation(); %>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%><br /></p>
    
    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Group information</legend>
            
            <table>
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.GroupID) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.GroupID, new { @readonly = "true", @disabled = "true" }) %>                        
                        <%= Html.HiddenFor(model => model.GroupID)%>                
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.GroupName) %>
                    </div>
                </td>
                <td>                
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.GroupName) %>
                        <%= Html.ValidationMessageFor(model => model.GroupName) %>
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

