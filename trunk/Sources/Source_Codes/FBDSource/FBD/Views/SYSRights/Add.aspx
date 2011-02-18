<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.SystemRights>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add new System Right
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ADD NEW SYSTEM RIGHT</h2>
    <% Html.EnableClientValidation(); %>
    <p><%= TempData["Message"] != null ? TempData["Message"]: "" %></p>
    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            <table>
                <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.RightID) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBoxFor(model => model.RightID) %>
                            <%= Html.ValidationMessageFor(model => model.RightID) %>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.RightName) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBoxFor(model => model.RightName) %>
                            <%= Html.ValidationMessageFor(model => model.RightName) %>
                        </div>
                    </td>
                </tr>
            </table>      
            
            <p>
                <input type="submit" value="Save" />
                <input type='button' onclick="window.location.href='<%= Url.Action("Index") %>';" value="Cancel" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>
