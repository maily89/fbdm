<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.RNKBusinessRankingViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add</h2>
    <% Html.EnableClientValidation(); %>
    
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>


    <% using (Html.BeginForm("Add", "RNKRankBusiness"))
       {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            <table>
            <tr>
                <td>
                    <div class="editor-label">
                        CIF Number
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.CIF, new { @readonly = "true", @disabled = "true" })%>
                    </div>
               </td>
           </tr>
            <tr>
                <td>
                    <div class="editor-label">
                        <%= FBD.CommonUtilities.Constants.SYSTEM_REPORTING_PERIOD %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.PeriodID, new { @readonly = "true"})%>
                    </div>
               </td>
           </tr>
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.BusinessRanking.CreditDepartment) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.BusinessRanking.CreditDepartment) %>
                        <%= Html.ValidationMessageFor(model => model.BusinessRanking.CreditDepartment) %>
                    </div>
               </td>
           </tr>
            
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.BusinessRanking.TaxCode) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.BusinessRanking.TaxCode) %>
                        <%= Html.ValidationMessageFor(model => model.BusinessRanking.TaxCode) %>
                    </div>
               </td>
           </tr>
            
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.BusinessRanking.CustomerGroup) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.BusinessRanking.CustomerGroup) %>
                        <%= Html.ValidationMessageFor(model => model.BusinessRanking.CustomerGroup) %>
                    </div>
               </td>
           </tr>
            <tr>
                <td>
                    <div class="editor-label">
                        <%=FBD.CommonUtilities.Constants.BUSINESS_INDUSTRY %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.DropDownList("IndustryID", new SelectList(FBD.ViewModels.RNKRankingViewModel.BusinessIndustries as IEnumerable, 
"IndustryID", "IndustryName", Model!=null?Model.IndustryID:null)) %>
                    </div>
               </td>
           </tr>
           <tr>
                <td>
                    <div class="editor-label">
                        <%=FBD.CommonUtilities.Constants.BUSINESS_TYPE %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.DropDownList("TypeID", new SelectList(FBD.ViewModels.RNKRankingViewModel.BusinessTypes as IEnumerable, 
"TypeID", "TypeName", Model!=null?Model.TypeID:null)) %>
                    </div>
               </td>
           </tr>
            <tr>
                <td>
                    <div class="editor-label">
                        Audited Status
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.DropDownListFor(m=> m.BusinessRanking.AuditedStatus,FBD.CommonUtilities.DropDownHelper.AuditedType) %>
                    </div>
               </td>
           </tr>
            
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.BusinessRanking.TotalDebt) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.BusinessRanking.TotalDebt) %>
                        <%= Html.ValidationMessageFor(model => model.BusinessRanking.TotalDebt) %>
                    </div>
               </td>
           </tr>
            <tr>
                <td>
                    <div class="editor-label">
                        Customer Type
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.DropDownList("CustomerTypeID", new SelectList(FBD.ViewModels.RNKRankingViewModel.CustomerType as IEnumerable, 
"TypeID", "TypeName", Model!=null?Model.CustomerTypeID:null),"Customer Type") %>
                    </div>
               </td>
           </tr>
            <tr>
                <td>
                    <div class="editor-label">
                        User:
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        Unknown
                    </div>
               </td>
           </tr>
            <tr>
                <td>
                    <div class="editor-label">
                        Date Modified
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(m=>m.BusinessRanking.DateModified) %>
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

