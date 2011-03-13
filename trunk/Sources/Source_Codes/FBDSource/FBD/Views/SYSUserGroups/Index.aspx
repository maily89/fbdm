﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.Models.SystemUserGroups>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	User Group
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MANAGING USER GROUPS</h2>
    
    <%= TempData["Message"]!=null?TempData["Message"]:"" %>
    
    <h3>
        <%= Html.ActionLink("Add New User Group", "Add") %>
    </h3>
    <table> 
        <tr>
            <th></th>
            <th>Group ID</th>
            <th>Group Name</th>
        </tr>
        <% foreach (var item in Model)  { %>
        
            <tr>
                <td>
                    <%= Html.ActionLink("Edit", "Edit", new { id = item.GroupID })%> 
                    <%= Html.ActionLink("Delete", "Delete", new { id = item.GroupID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete "+item.GroupName+"?');" })%>
                </td>
                <td>
                    <%= Html.Encode(item.GroupID) %>
                </td>
                <td>
                    <%= Html.Encode(item.GroupName) %>
                </td>
            </tr>
        <% } %>
    </table>

    <h3>
        <%= Html.ActionLink("Add New User Group", "Add") %>
    </h3>

</asp:Content>


