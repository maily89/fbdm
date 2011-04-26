<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListUser.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.CommonUtilities.Vector>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	CustomerList
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>CustomerList</h2>
    

<%= Html.Telerik().Grid(Model)
        .Name("Grid")
        .Columns(columns =>
        {
            columns.Bound(o => o.CustomerName).Width(400); ;
            columns.Bound(o => o.x).Width(50).Title("Total Score Score");
            columns.Bound(o => o.RankName).Width(50).Title("Old Rank");
            columns.Bound(o => o.newRankName).Width(50).Title("New Rank");
            columns.Bound(o => o.modifiedDate).Format("{0:dd/MM/yyyy}").Width(120);
        })
        .DataBinding(dataBinding => 
        {
           // dataBinding.Server().Select("GetCustomerList", "BSNMining", new { ajax = true });
            dataBinding.Ajax().Select("_GetCustomerList",
                "BSNMining").Enabled(true);
        })
        .Sortable(sorting => sorting.Enabled(true))
        .Pageable(paging => paging.Enabled(true))
        .Filterable(filtering => filtering.Enabled(true))
        .Groupable(grouping => grouping.Enabled(true))
        .Footer(true)
        .Resizable(resizing => resizing.Columns(true))
%>

</asp:Content>

