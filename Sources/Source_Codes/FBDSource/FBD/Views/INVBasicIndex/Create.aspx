<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.IndividualBasicIndex>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create New Individual Basic Index</h2>
    <% Html.EnableClientValidation(); %>
    
   <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>
    
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
                        <%= Html.TextBoxFor(model => model.IndexID) %>
                        <%= Html.ValidationMessageFor(model => model.IndexID) %>
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
                    <input type="submit" value="Add" />
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

