<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.RNKBusinessRankingViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= ViewData["Edit"]!=null?"Edit Business Info":"Add Business Info" %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= ViewData["Edit"]!=null?"Edit Business Info":"Add Business Info" %></h2>
    <% Html.EnableClientValidation(); %>
    
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>
    
    <%if (ViewData["Edit"]==null){ %>
    <%Html.RenderPartial("BusinessStep",FBD.CommonUtilities.Constants.BusinessRankStep.General); %>
    <%} %>
    <%Html.RenderPartial("CustomerInfo", Model.CustomerInfo);%>
    <% using (Html.BeginForm(ViewData["Edit"] != null ? "EditInfo" : "Add", "RNKRankBusiness"))
       {%>
        <%= Html.ValidationSummary(false) %>

            <table>
            
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.HiddenFor(m=>m.IsNew) %>
                        <%= Html.HiddenFor(model => model.PeriodID)%>
                        <%= Html.HiddenFor(model=>model.CustomerID) %>
                        <%= Html.HiddenFor(model=>model.BusinessRanking.ID) %>
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
                        Loan Term
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.DropDownList("LoanID", new SelectList(FBD.ViewModels.RNKRankingViewModel.LoanTerm as IEnumerable, 
"LoanTermID", "LoanTermName", Model!=null?Model.LoanID:null)) %>
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
"TypeID", "TypeName", Model!=null?Model.CustomerTypeID:null)) %>
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
                       <%= Html.TextBoxFor(model => model.BusinessRanking.UserID,new{@readonly="true"})%>

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
                        <%= Html.TextBoxFor(m => m.BusinessRanking.DateModified, new { @readonly = "true" })%>
                    </div>
               </td>
           </tr>
        </table>
        <hr />
        <hr />
        <table>
	    <tr>
	    <%if (ViewData["Edit"] != null)
       { %>
	    <td><input value="Save" type="submit" name="Save"/></td>
	    <td><input type='button' onclick="window.location.href='<%= Url.Action("DetailGeneral", new {id = ViewData["RankID"] } ) %>';" value="Cancel" /></td>
	    <%} %>
	    <% if (ViewData["Edit"] == null)
        { %>
        <td><input value="Save only" type="submit" name="Save"/></td>
	    <td><input value="Save then process next" type="submit" name="SaveNext"/></td>
	    
	    <td><input type='button' onclick="window.location.href='<%= Url.Action("Index") %>';" value="Cancel" /></td>
	    <%}
            %>
	    </tr>
	    </table>
	    <br />
	    <% if (ViewData["Edit"] == null)
        {%>
	    <b>*Save only:</b> Information will be saved and stop process<br />
	    <b>*Save then process next:</b> Information will be saved and continue process<br />
	    <b>*Cancel:</b> Discard the ranking process
	    <%} %>

    <% } %>


</asp:Content>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>

