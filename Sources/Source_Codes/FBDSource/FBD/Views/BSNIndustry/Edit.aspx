<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.BusinessIndustries>" %>
<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>
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
                <%= Html.LabelFor(model => model.IndustryID) %>
            </div>
            
            <div class="editor-field">
                <%= Html.TextBox("id",Model.IndustryID,new {@readonly="true", @disabled="true"}) %>
                <%= Html.HiddenFor(model => model.IndustryID)%>
                
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.IndustryName) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.IndustryName) %>
                <%= Html.ValidationMessageFor(model => model.IndustryName) %>
            </div>
            
            
            <input type="submit" value="Edit" />
            
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>
