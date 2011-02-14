<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.SystemReportingPeriods>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add</h2>
    <% Html.EnableClientValidation(); %>
    <p><%= TempData["Message"] != null ? TempData["Message"]: "" %></p>
    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.PeriodID) %>
            </div>
            
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.PeriodID)%>
                <%= Html.ValidationMessageFor(model => model.PeriodID) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.PeriodName) %>
            </div>
            
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.PeriodName)%>
                <%= Html.ValidationMessageFor(model => model.PeriodName) %>
            </div>
            
            <div class="editor-label">
                From Date
            </div>
                
            <div class="editor-field">
                
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.ToDate) %>
            </div>
            
            <div class="editor-field">
                <%= Html.LabelFor(model => model.ToDate) %>
                <%= Html.ValidationMessageFor(model => model.ToDate) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Active) %>
            </div>
            
            <div class="editor-field">
                <%= Html.LabelFor(model => model.Active) %>
                <%= Html.ValidationMessageFor(model => model.Active) %>
            </div>
            
                <input type="submit" value="Add" />
                <input type="button" onclick="window.location.href='<%= Url.Action("Index") %>';" value="Cancel" />
                
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

