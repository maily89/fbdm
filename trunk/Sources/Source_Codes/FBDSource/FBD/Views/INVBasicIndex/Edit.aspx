﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.IndividualBasicIndex>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>
    <% Html.EnableClientValidation(); %>
    
    <p class="message"><%= TempData["Message"] != null ? TempData["Message"] : "" %></p>
    

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
            <table>
            
               <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.IndexID) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBoxFor(model => model.IndexID, new { @readonly = "true", @disabled = "true" }) %>
                            <%= Html.HiddenFor(model => model.IndexID) %>
                        </div>
                   </td>
               </tr>
                <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.IndexName) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBoxFor(model => model.IndexName) %>
                            <%= Html.ValidationMessageFor(model => model.IndexName) %>
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
                        </div>
                   </td>
               </tr>
            
                <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.ValueType) %>
                        </div>
                    </td>
                     <td>
                    <div class="editor-field">
                        <%= Html.DropDownListFor(model => model.ValueType, new SelectList(new List<String>{"N", "C"}, "N")) %>
                    </div>
                </td>
               </tr>
            
                <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.LeafIndex) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.CheckBoxFor(model => model.LeafIndex) %>
                        </div>
                    </td>
               </tr>
            
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

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

