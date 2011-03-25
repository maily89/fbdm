<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.INVSummaryRankViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add</h2>
    <% Html.EnableClientValidation(); %>
    
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>
    
    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            <table>
           <%-- <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.summaryRanks.ID) %>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.summaryRanks.ID) %>
                        <%= Html.ValidationMessageFor(model => model.summaryRanks.ID) %>
                    </div>
               </td>
           </tr>--%>
           
          
           
           <tr>            
                <td>
                    <%= Html.Label("Basic Rank") %>
                </td>
            
                <td>                
                    <%= Html.DropDownList("basicRankID", new SelectList(Model.basicRanks as IEnumerable, "RankID", "Rank", 
                                Model.basicRankID))%>
                </td>                
           </tr>
       
            <tr>            
                <td>
                    <%= Html.Label("Collateral Rank") %>
                </td>
            
                <td>                
                    <%= Html.DropDownList("collateralRankID", new SelectList(Model.collateralRanks as IEnumerable, "RankID", "Rank",Model.collateralRankID))%>
                </td>                
           </tr>
           
            <tr>
                <td>
                    <div class="editor-label">
                        <%= Html.LabelFor(model => model.Evaluation)%>
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.TextBoxFor(model => model.Evaluation) %>
                        <%= Html.ValidationMessageFor(model => model.Evaluation) %>
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

