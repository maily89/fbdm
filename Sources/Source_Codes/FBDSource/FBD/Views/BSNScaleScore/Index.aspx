<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.BSNScaleScoreViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Business Scale Score
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>BUSINESS SCALE SCORING</h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>
    <% using (Html.BeginForm())
       { %>
        <%= Html.DropDownList("IndustryID", new SelectList(Model.Industry as IEnumerable,
                "IndustryID", "IndustryName", Model != null ? Model.IndustryID : null), "Select Industry", new { onchange = "this.form.submit();" })%>
        <br />
        <br />
        <%= Html.ListBox("CriteriaID",new SelectList(Model.Criteria as IEnumerable,"CriteriaID","CriteriaName",Model!=null?Model.CriteriaID:null),new {onchange="this.form.submit();",style="width:500px"})%>
        <br />
        <br />
    <%} %>
    <% using (Html.BeginForm()) {%>
    <%= Html.Telerik().Grid<FBD.ViewModels.BSNScaleScoreRow>(Model.ScaleScore)
        .Name("ScaleScore")
        .DataKeys(datakeys => datakeys.Add(s => s.ScoreID))
        .ToolBar(commands => commands.Insert().HtmlAttributes(new {@style=(string.IsNullOrEmpty(Model.IndustryID) 
                || string.IsNullOrEmpty(Model.CriteriaID)?"display:none":"")}))
        .DataBinding(dataBinding => dataBinding
            .Ajax()
            //Home.Index render the grid initially
                .Select("IndexAjax", "BSNScaleScore", new { IndustryID = Model.IndustryID, CriteriaID = Model.CriteriaID })
                                .Insert("Insert", "BSNScaleScore", new { IndustryID = Model.IndustryID, CriteriaID = Model.CriteriaID })
                                .Update("Update", "BSNScaleScore", new { IndustryID = Model.IndustryID, CriteriaID = Model.CriteriaID })
                                .Delete("Delete", "BSNScaleScore", new { IndustryID = Model.IndustryID, CriteriaID = Model.CriteriaID })
        )
        .Columns(columns =>
        {
            columns.Bound(o => o.FromValue).Width(100);
            columns.Bound(o => o.ToValue).Width(100);
            columns.Bound(o => o.Score).Width(100);
            columns.Command(commands =>
            {
                commands.Edit();
                    commands.Delete();
            }).Width(200)
            ;
       
        })
        .Pageable()
        .Sortable()
%>
    <%} %>


</asp:Content>

<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent"  runat="server">
<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script> 
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script> 
</asp:Content>


