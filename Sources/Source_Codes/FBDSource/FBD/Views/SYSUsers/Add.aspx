<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.SYSUsersViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add user
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ADD NEW A USER</h2>
    <% Html.EnableClientValidation(); %>
    
    <p class="message"><%= TempData["Message"] != null ? TempData["Message"] : "" %></p>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
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
                        <%= Html.LabelFor(model => model.GroupID) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.DropDownList("Group", new SelectList(Model.SystemUserGroups as IEnumerable,
                            "GroupID", "GroupName", Model != null ? Model.GroupID : null))%>
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
                        <%= Html.DropDownList("Branch", new SelectList(Model.SystemBranches as IEnumerable,
                            "BranchID", "BranchName", Model != null ? Model.BranchID : null))%>
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
                        <%= Html.LabelFor(model => model.SystemUsers.Password) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.SystemUsers.Password) %>
                        <%= Html.ValidationMessageFor(model => model.SystemUsers.Password) %>
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

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

