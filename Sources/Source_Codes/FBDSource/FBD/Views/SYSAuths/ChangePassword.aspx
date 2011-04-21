<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.SYSChangePassModel>" %>
<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Change Password
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Change Password</h2>
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
                            <%= Html.LabelFor(model => model.UserID) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBox("id", Model.UserID, new { @readonly = "true", @disabled = "true" })%>
                            <%= Html.HiddenFor(model => model.UserID) %>
                        </div>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.OldPassword) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.PasswordFor(model => model.OldPassword)%>
                            <%= Html.ValidationMessageFor(model => model.OldPassword) %>
                        </div>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.NewPassword) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.PasswordFor(model => model.NewPassword)%>
                            <%= Html.ValidationMessageFor(model => model.NewPassword) %>
                        </div>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.ConfirmNewPassword) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.PasswordFor(model => model.ConfirmNewPassword)%>
                            <%= Html.ValidationMessageFor(model => model.ConfirmNewPassword) %>
                        </div>
                    </td>
                </tr>
                
                <tr>
                    <td></td>
                    
                    <td>
                        <input type="submit" value="Save" />
                        <input type='button' onclick="window.location.href='<%= Url.Action("LoginSuccess") %>';" value="Cancel" />
                    </td>
                </tr>
            </table>
        </fieldset>

    <% } %>

</asp:Content>