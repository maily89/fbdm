<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.SYSUsersViewModel>" %>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add new user
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ADD NEW USER</h2>
    <% Html.EnableClientValidation(); %>
    
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%><br /></p>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>User information</legend>
            <table>
            
            <tr>
                <td>
                
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.SystemUsers.UserID) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.SystemUsers.UserID) %>
                        <%= Html.ValidationMessageFor(model => model.SystemUsers.UserID) %>
                    </div>
               </td>
           </tr>
            
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.SystemUsers.FullName) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.SystemUsers.FullName) %>
                        <%= Html.ValidationMessageFor(model => model.SystemUsers.FullName) %>
                    </div>
               </td>
           </tr>
            
            <tr>
                <td>
                
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.BranchID) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.DropDownList("BranchID", new SelectList(Model.SystemBranches as IEnumerable,
                            "BranchID", "BranchName", Model.BranchID))%>
                    </div>
               </td>
           </tr>
            
            <tr>
                <td>
                
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.GroupID) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.DropDownList("GroupID", new SelectList(Model.SystemUserGroups as IEnumerable,
                            "GroupID", "GroupName", Model.GroupID))%>
                    </div>
               </td>
           </tr>
            
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.SystemUsers.Password)%>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.SystemUsers.Password, new { @readonly = "true", @disabled = "true" })%>
                    </div>
               </td>
           </tr>
            
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.SystemUsers.Status) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.SystemUsers.Status)%>
                        <%= Html.ValidationMessageFor(model => model.SystemUsers.Status)%>
                    </div>
               </td>
           </tr>
            
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.SystemUsers.CreditDepartment) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.SystemUsers.CreditDepartment) %>
                        <%= Html.ValidationMessageFor(model => model.SystemUsers.CreditDepartment) %>
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
