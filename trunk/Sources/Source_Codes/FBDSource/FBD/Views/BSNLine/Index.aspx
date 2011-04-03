﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.BSNLineIndexViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Business Lines
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>LIST OF BUSINESS LINES</h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%></p>
    Industry List <br />
    <% using (Html.BeginForm())
       { %>
    <%= Html.DropDownList("IndustryID", new SelectList(Model.Industries as IEnumerable,
                "IndustryID", "IndustryName", Model != null ? Model.IndustryID : null), "Select Industry", new { onchange = "this.form.submit();" })%>
    <br /><br />
    <%} %>
    Lines for <%= Model.IndustryName %>
    
    <table>
        <tr>
            <th></th>
            <th>Line ID</th>
            <th>Line Name</th>
        </tr>

    <%  if (Model.Lines != null)
        {
            foreach (var item in Model.Lines)
            { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new {  id=item.LineID  })%> |
                <%= Html.ActionLink("Delete", "Delete", new { id = item.LineID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete line " + item.LineName + "?');" })%>
            </td>
            <td>
                <%= Html.Encode(item.LineID)%>
            </td>
            <td>
                <%= Html.Encode(item.LineName)%>
            </td>
        </tr>
    
    <% }
        } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Add") %>
    </p>

</asp:Content>


