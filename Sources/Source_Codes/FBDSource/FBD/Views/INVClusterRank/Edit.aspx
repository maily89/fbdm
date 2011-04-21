<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.IndividualClusterRanks>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>
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
                            <%= Html.LabelFor(model => model.RankID) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBoxFor(model => model.RankID, new { @readonly = "true", @disabled = "true" }) %>
                            <%= Html.HiddenFor(model => model.RankID) %>
                        </div>
                   </td>
               </tr>
                <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.Rank) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBoxFor(model => model.Rank) %>
                            <%= Html.ValidationMessageFor(model => model.Rank) %>
                        </div>
                   </td>
               </tr>
            
                <tr>
                    <td>
                        <div class="editor-label">
                            Centroid
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBoxFor(model => model.CentroidX, new { @readonly = "true", @disabled = "true" })%>
                            <%= Html.ValidationMessageFor(model => model.CentroidX) %>
                        </div>
                   </td>
               </tr>
            
                <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.CentroidY) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBoxFor(model => model.CentroidY, new { @readonly = "true", @disabled = "true" })%>
                            <%= Html.ValidationMessageFor(model => model.CentroidY) %>
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


