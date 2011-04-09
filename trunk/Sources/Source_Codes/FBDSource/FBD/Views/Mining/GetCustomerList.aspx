<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.CommonUtilities.Vector>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	GetCustomerList
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>GetCustomerList</h2>
<%= Html.Telerik().Grid(Model)
        .Name("Grid")
        .Columns(columns =>
        {
            columns.Bound(o => o.CustomerName).Width(400); ;
            columns.Bound(o => o.x).Width(50).Title("Finacial Score");
            columns.Bound(o => o.y).Width(50).Title("NonFinacial Score");
            columns.Bound(o => o.RankID).Width(50);
            columns.Bound(o => o.modifiedDate).Format("{0:MM/dd/yyyy}").Width(120);
        })
        .DataBinding(dataBinding => 
        {
          //  dataBinding.Server().Select("GetCustomerList", "Grid", new { ajax = true });
            dataBinding.Ajax().Select("_GetCustomerList",
                "Mining").Enabled(true);
        })
        .Sortable(sorting => sorting.Enabled(true))
        .Pageable(paging => paging.Enabled(true))
        .Filterable(filtering => filtering.Enabled(true))
        .Groupable(grouping => grouping.Enabled(true))
        .Footer(true)
        .Resizable(resizing => resizing.Columns(true))
%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

