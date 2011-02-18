<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.SystemReportingPeriods>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit system reporting period
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>EDIT SYSTEM REPORTING PERIOD</h2>
    <% Html.EnableClientValidation(); %>
    <p><%=TempData["Message"] != null ? TempData["Message"]: ""%></p>
    
    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
            
            <table>
            <tr>
                <td>
                    <div class="editor-label">
                    <%= Html.LabelFor(model => model.PeriodID) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                    <%= Html.TextBoxFor(model => model.PeriodID)%>
                    <%= Html.ValidationMessageFor(model => model.PeriodID) %>
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
                        <%= Html.EditorFor(model => model.FromDate, String.Format("{0:g}", Model.FromDate)) %>
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
                        <%= Html.EditorFor(model => model.ToDate, String.Format("{0:g}", Model.ToDate)) %>
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
            </table>
            
            <p>
                <input type="submit" value="Save" />
                <input type='button' onclick="window.location.href='<%= Url.Action("Index") %>';" value="Cancel" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

