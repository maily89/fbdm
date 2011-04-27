<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListUser.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.CommonUtilities.Vector>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ListCustomer
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<body>
    <h2>ListCustomer</h2>

<%= Html.Telerik().Grid(Model)
        .Name("Grid")
        .Columns(columns =>
        {
            columns.Bound(o => o.CustomerName).Width(150); ;
            columns.Bound(o => o.x).Width(50).Title("Basic score");
            columns.Bound(o => o.y).Width(50).Title("Colateral score");
            columns.Bound(o => o.RankName).Width(50).Title("Old Rank");
            columns.Bound(o => o.newRankName).Width(50).Title("New Rank");
            columns.Bound(o => o.modifiedDate).Format("{0:dd/MM/yyyy}").Width(120);
        })
        .DataBinding(dataBinding => dataBinding.Ajax().Select("_ListCustomer",
                "INVMining").Enabled(true))
        //{
           // dataBinding.Server().Select("ListCustomer", "INVMining", new { ajax = true });
           
        //})
        .Sortable(sorting => sorting.Enabled(true))
        .Pageable(paging => paging.Enabled(true))
        .Filterable(filtering => filtering.Enabled(true))
        .Groupable(grouping => grouping.Enabled(true))
        .Footer(true)
        .Resizable(resizing => resizing.Columns(true))
%>
</body>

</asp:Content>

