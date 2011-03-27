<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.SystemReportingPeriods>" %>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit reporting period
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>EDIT REPORTING PERIOD</h2>
    <% Html.EnableClientValidation(); %>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%><br /></p>
    
    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Reporting period information</legend>
            
            <table>
            <tr>
                <td>
                    <div class="editor-label">
                    <%= Html.LabelFor(model => model.PeriodID) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                    <%= Html.TextBoxFor(model => model.PeriodID, new { @readonly = "true", @disabled = "true" })%>
                    <%= Html.ValidationMessageFor(model => model.PeriodID)%>
            </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.PeriodName) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.PeriodName)%>
                        <%= Html.ValidationMessageFor(model => model.PeriodName) %>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.FromDate) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.Telerik().DatePickerFor(model => model.FromDate).Format("dd-MMM-yyyy")%>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.ToDate) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.Telerik().DatePickerFor(model => model.ToDate).Format("dd-MMM-yyyy")%>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.Active) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.CheckBoxFor(model => model.Active) %>
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
