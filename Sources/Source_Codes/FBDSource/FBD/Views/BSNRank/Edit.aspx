﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.BusinessRanks>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit Business Rank
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Business Rank</h2>

    <% Html.EnableClientValidation(); %>
    <p style="color:Red"><%= TempData["Message"] != null ? TempData["Message"] : "" %></p>
    <br />
    
    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.RankID) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.RankID, new { @readonly = "true", @disabled = "true" })%>
                <%= Html.ValidationMessageFor(model => model.RankID) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.FromValue) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.FromValue, String.Format("{0:F}", Model.FromValue)) %>
                <%= Html.ValidationMessageFor(model => model.FromValue) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.ToValue) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.ToValue, String.Format("{0:F}", Model.ToValue)) %>
                <%= Html.ValidationMessageFor(model => model.ToValue) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Rank) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Rank) %>
                <%= Html.ValidationMessageFor(model => model.Rank) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Evaluation) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Evaluation) %>
                <%= Html.ValidationMessageFor(model => model.Evaluation) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.RiskGroup) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.RiskGroup) %>
                <%= Html.ValidationMessageFor(model => model.RiskGroup) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.DebtGroup) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.DebtGroup) %>
                <%= Html.ValidationMessageFor(model => model.DebtGroup) %>
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

