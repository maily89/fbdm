<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.BusinessTypes>" %>

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
            
            <table>
            
               <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.TypeID) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBoxFor(model => model.TypeID, new { @readonly = "true", @disabled = "true" }) %>
                            <%= Html.HiddenFor(model => model.TypeID) %>
                        </div>
                   </td>
               </tr>
                <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.TypeName) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBoxFor(model => model.TypeName) %>
                            <%= Html.ValidationMessageFor(model => model.TypeName) %>
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

