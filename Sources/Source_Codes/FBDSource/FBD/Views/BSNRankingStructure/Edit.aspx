<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.BusinessRankingStructure>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit Ranking Structure
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Ranking Structure</h2>
    <% Html.EnableClientValidation(); %>
    
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>
    

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(false) %>
        
        <fieldset>
            <legend>Fields</legend>
            <table>
                <tr>
                    <td>
                        <div class="editor-label">
                            <%= Html.LabelFor(model => model.IndexType) %>
                            <%= Html.HiddenFor(model => model.ID) %>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.TextBox("IndexTypeText",Model.IndexType==FBD.CommonUtilities.Constants.RNK_STRUCTURE_FINANCIAL_INDEX?"Financial Index":"NonFinancial Index",new {@readonly="true"}) %>
                            <%= Html.HiddenFor(m=>m.IndexType) %>
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
                            <%= Html.TextBox("AuditStatusText", Model.AuditedStatus == FBD.CommonUtilities.Constants.RNK_STRUCTURE_AUDITED ? "Audited" : "Not Audited", new { @readonly = "true" })%>
                            <%=Html.HiddenFor(m=>m.AuditedStatus) %>
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

