﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.SystemUserGroups>" %>
<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit user group
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>EDIT THE USER GROUP</h2>
    
    <% Html.EnableClientValidation(); %>
    <p><%= TempData["Message"] != null ? TempData["Message"] : "" %></p>
    
    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <table>
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.GroupID) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBox("id",Model.GroupID,new {@readonly="true", @disabled="true"}) %>
                        <%= Html.HiddenFor(model => model.GroupID)%>                
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.GroupName) %>
                    </div>
                </td>
                <td>                
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.GroupName) %>
                        <%= Html.ValidationMessageFor(model => model.GroupName) %>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="submit" value="Edit" />
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

