<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.SystemBranches>" %>
<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add branch
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ADD NEW BRANCH</h2>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            <table>
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.BranchID) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.BranchID) %>
                        <%= Html.ValidationMessageFor(model => model.BranchID) %>
                    </div>
                </td>
            </tr>
            
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.BranchName) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.BranchName) %>
                        <%= Html.ValidationMessageFor(model => model.BranchName) %>
                    </div>
                </td>
            </tr>
            
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.Active) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.CheckBoxFor(model => model.Active) %>
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


