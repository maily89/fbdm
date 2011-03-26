<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.SYSLoginModel>" %>
<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Login
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Login</h2>
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
                            <%= Html.TextBoxFor(model => model.UserID) %>
                            <%= Html.ValidationMessageFor(model => model.UserID) %>
                        </div>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.Password) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBoxFor(model => model.Password) %>
                            <%= Html.ValidationMessageFor(model => model.Password) %>
                        </div>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <input type="submit" value="Login" />
                    </td>
                    <td>
                        <input type='button' onclick="window.location.href='<%= Url.Action("Index", "Home") %>';" value="Cancel" />
                    </td>
                </tr>
            </table>
        </fieldset>

    <% } %>
</asp:Content>
