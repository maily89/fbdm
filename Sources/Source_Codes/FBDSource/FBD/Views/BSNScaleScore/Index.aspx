<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.BSNScaleScoreViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
    <%= TempData["Message"]!=null?TempData["Message"]:"" %><br />
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
    <%= Html.Telerik().Grid(Model.ScaleScore)
        .Name("ScaleScore")
        .DataKeys(datakeys => datakeys.Add(s => s.ScoreID))
        .ToolBar(commands => commands.Insert())
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
            columns.Bound(o => o.FromValue);
            columns.Bound(o => o.ToValue);
            columns.Bound(o => o.Score);
            columns.Command(c => c
                    .Edit());
            columns.Command(c => c.Delete());
        })
        .Pageable()
        .Sortable()
%>
    <%} %>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

