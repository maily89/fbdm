<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.BusinessTypes>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>
    <% Html.EnableClientValidation(); %>
    <p><%= TempData["Message"] != null ? TempData["Message"] : "" %></p>
    
    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.TypeID) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.TypeID, new { @readonly = "true", @disabled = "true" })%>
                <%= Html.ValidationMessageFor(model => model.TypeID) %>
                <%= Html.HiddenFor(model => model.TypeID)%>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.TypeName) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.TypeName) %>
                <%= Html.ValidationMessageFor(model => model.TypeName) %>
            </div>
            
            <p>
                <input type="submit" value="Edit" />
                <input type='button' onclick="window.location.href='<%= Url.Action("Index") %>';" value="Cancel" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

