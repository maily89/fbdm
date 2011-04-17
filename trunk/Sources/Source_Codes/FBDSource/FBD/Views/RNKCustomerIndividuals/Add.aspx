<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.RNKCustomerIndividualsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add New Individual Customer</h2>
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
                        <%= Html.LabelFor(model => model.CustomerIndividual.CIF) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.CustomerIndividual.CIF)%>
                        <%= Html.ValidationMessageFor(model => model.CustomerIndividual.CIF)%>
                    </div>
               </td>
           </tr>
           <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.CustomerIndividual.CustomerName) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.CustomerIndividual.CustomerName)%>
                        <%= Html.ValidationMessageFor(model => model.CustomerIndividual.CustomerName)%>
                    </div>
               </td>
           </tr>
           <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.SystemBranches) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.DropDownList("BranchID", new SelectList(Model.SystemBranches as IEnumerable, 
"BranchID", "BranchName", Model!=null?Model.BranchID:null)) %>                    </div>
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

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>

