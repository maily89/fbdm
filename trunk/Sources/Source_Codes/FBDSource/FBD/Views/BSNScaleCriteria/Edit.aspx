﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.BusinessScaleCriteria>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit Business Scale Criteria
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Business Scale Criteria</h2>
    <% Html.EnableClientValidation(); %>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>
    <br />
    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
            
            <table>
            
               <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.CriteriaID) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBoxFor(model => model.CriteriaID, new { @readonly = "true", @disabled = "true" }) %>
                            <%= Html.HiddenFor(model => model.CriteriaID) %>
                        </div>
                   </td>
               </tr>
                <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.CriteriaName) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBoxFor(model => model.CriteriaName) %>
                            <%= Html.ValidationMessageFor(model => model.CriteriaName) %>
                        </div>
                   </td>
               </tr>
            
                <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.Unit) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBoxFor(model => model.Unit) %>
                            <%= Html.ValidationMessageFor(model => model.Unit) %>
                        </div>
                   </td>
               </tr>
            
                <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.Formula) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBoxFor(model => model.Formula) %>
                            <%= Html.ValidationMessageFor(model => model.Formula) %>
                            <%= Html.HiddenFor(m => m.ValueType)%>
                        </div>
                   </td>
               </tr>
            
                <%--<tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.ValueType) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                        <% Model.ValueType = "N"; %>
                            <%= Html.HiddenFor(m => m.ValueType)%>
                        </div>
                   </td>
               </tr>--%>
            
             <tr>
                <td>
                    <input type="submit" value="Save" />
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

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>

