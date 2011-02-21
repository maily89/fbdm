<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.BSNScaleScoreViewModel>" %>
<%@ Import Namespace="MVCNestedModels.Controls" %>
<%@ Import Namespace=" MVCControlsToolkit.Core" %>
<%@ Import Namespace=" MVCControlsToolkit.Controls" %>
<%@ Import Namespace=" MVCNestedModels.Models" %>
<%@ Import Namespace=" System.Linq.Expressions" %>
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
        <%= Html.ListBox("CriteriaID",new SelectList(Model.Criteria as IEnumerable,"CriteriaID","CriteriaName",Model!=null?Model.CriteriaID:null),new {onchange="this.form.submit();"})%>
            
    <br /><br />
    <%} %>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

