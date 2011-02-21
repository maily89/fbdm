<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.BusinessRankingStructure>" %>

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
                            <%= Html.LabelFor(model => model.ID) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBoxFor(model => model.ID, new { @readonly = "true", @disabled = "true" }) %>
                            <%= Html.HiddenFor(model => model.ID) %>
                        </div>
                   </td>
               </tr>
                <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.IndexType) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBoxFor(model => model.IndexType) %>
                            <%= Html.ValidationMessageFor(model => model.IndexType) %>
                        </div>
                   </td>
               </tr>
            
                <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.AuditedStatus) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBoxFor(model => model.AuditedStatus) %>
                            <%= Html.ValidationMessageFor(model => model.AuditedStatus) %>
                        </div>
                   </td>
               </tr>
            
                <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.Percentage) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBoxFor(model => model.Percentage) %>
                            <%= Html.ValidationMessageFor(model => model.Percentage) %>
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

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>

