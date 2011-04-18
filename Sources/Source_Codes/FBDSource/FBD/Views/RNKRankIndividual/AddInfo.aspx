<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.RNKIndividualViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= ViewData["Edit"]!=null?"Edit ":"Add " %>Customer Ranking
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= ViewData["Edit"]!=null?"Edit":"Add " %>Customer Ranking</h2>
    <% Html.EnableClientValidation(); %>
    
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>
    <% if (ViewData["Edit"] == null)
       { %>
    <%Html.RenderPartial("IndividualStep", FBD.CommonUtilities.Constants.IndividualRankStep.General); %>
    <%} %>
    <%Html.RenderPartial("CustomerInfo", Model.CustomerInfo);%>

    <% using (Html.BeginForm("AddInfo","RNKRankIndividual"))
       {%>
        <%= Html.ValidationSummary(false) %>
        <% if (ViewData["Edit"]!=null){ %>
        <%= Html.Hidden("Edit", ViewData["Edit"])%>
           <%} %>
        <%= Html.Hidden("rankID",ViewData["RankID"]) %>
        <fieldset>
            <legend>Fields</legend>
            <table>            
            <tr>
                <td>
                    <div class="editor-label">
     
                        <%= Html.LabelFor(model => model.IndividualRanking.CreditDepartment) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.IndividualRanking.CreditDepartment)%>
                        <%= Html.HiddenFor(model=>model.IndividualRanking.ID)%>
                        <%= Html.HiddenFor(model => model.Date) %>
                        <%= Html.HiddenFor(model => model.CIF)%>
                        <%= Html.HiddenFor(model => model.CustomerID) %>
                        <%= Html.ValidationMessageFor(model => model.IndividualRanking.CreditDepartment)%>
                    </div>
               </td>
           </tr>
            <tr>
                <td>
                    <div class="editor-label">
                        Purpose
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.DropDownList("PurposeID", new SelectList(FBD.ViewModels.RNKRankingViewModel.BorrowingPurpose as IEnumerable,
                            "PurposeID", "Purpose", Model != null ? Model.PurposeID : null))%>
                    </div>
               </td>
           </tr>
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.IndividualRanking.TotalDebt)%>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.IndividualRanking.TotalDebt)%>
                        <%= Html.ValidationMessageFor(model => model.IndividualRanking.TotalDebt)%>
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
                        <%= Html.DropDownList("LoanTermID", new SelectList(FBD.ViewModels.RNKRankingViewModel.LoanTerm as IEnumerable, 
"LoanTermID", "LoanTermName", Model!=null?Model.LoanTermID:null)) %>
                    </div>
               </td>
           </tr>
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.IndividualRanking.UserID)%>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.IndividualRanking.UserID,new{@readonly="true"})%>
                    </div>
               </td>
           </tr>
            
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.IndividualRanking.DateModified)%>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.IndividualRanking.DateModified,new{@readonly="true"})%>
                        <%= Html.ValidationMessageFor(model => model.IndividualRanking.DateModified)%>
                    </div>
               </td>
           </tr>
            <tr>
                <td>
                    <input type="submit" value="Save" />
                </td>
                <td>
                    <%if (ViewData["Edit"]==null){ %>
                        <input type='button' onclick="window.location.href='<%= Url.Action("Index") %>';" value="Cancel" />
                    <%}else { %>
                        <input type='button' onclick="window.location.href='<%= Url.Action("DetailGeneral", new {id = ViewData["RankID"] }) %>';" value="Cancel" />
                    <% }%>
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

