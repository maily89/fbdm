<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.BSNLineViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>
    <%= TempData["Message"]!=null?TempData["Message"]:"" %><br />
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
            Industry
            </div>
            <div class="editor-field">
                <%= Html.DropDownList("IndustryID", new SelectList(Model.BusinessIndustries as IEnumerable, 
"IndustryID", "IndustryName", Model!=null?Model.IndustryID:null)) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.BusinessLines.LineName) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.BusinessLines.LineName) %>
                <%= Html.ValidationMessageFor(model => model.BusinessLines.LineName) %>
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

