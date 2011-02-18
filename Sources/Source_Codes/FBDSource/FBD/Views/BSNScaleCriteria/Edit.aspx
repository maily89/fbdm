<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.BusinessScaleCriteria>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit Business Scale Criteria
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Business Scale Criteria</h2>
    <% Html.EnableClientValidation(); %>
    <p style="color:Red"><%= TempData["Message"] != null ? TempData["Message"] : "" %></p>
    <br />
    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.CriteriaID) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.CriteriaID,new {@readonly="true", @disabled="true"}) %>
                <%= Html.ValidationMessageFor(model => model.CriteriaID) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.CriteriaName) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.CriteriaName) %>
                <%= Html.ValidationMessageFor(model => model.CriteriaName) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Unit) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Unit) %>
                <%= Html.ValidationMessageFor(model => model.Unit) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Formula) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Formula) %>
                <%= Html.ValidationMessageFor(model => model.Formula) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.ValueType) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.ValueType) %>
                <%= Html.ValidationMessageFor(model => model.ValueType) %>
            </div>
            
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

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

