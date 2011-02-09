<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.BusinessIndustries>" %>
<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add</h2>
    <% Html.EnableClientValidation(); %>
    <p><%= Model!=null?Model.Error:"" %></p>
    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.IndustryID) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.IndustryID) %>
                <%= Html.ValidationMessageFor(model => model.IndustryID) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.IndustryName) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.IndustryName) %>
                <%= Html.ValidationMessageFor(model => model.IndustryName) %>
            </div>
            
            
            <input type="submit" value="Create" />
            
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

