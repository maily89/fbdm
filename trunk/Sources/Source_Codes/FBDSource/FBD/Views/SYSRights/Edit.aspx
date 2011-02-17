<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.SystemRights>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.RightID) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.RightID) %>
                <%= Html.ValidationMessageFor(model => model.RightID) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.RightName) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.RightName) %>
                <%= Html.ValidationMessageFor(model => model.RightName) %>
            </div>
            
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

