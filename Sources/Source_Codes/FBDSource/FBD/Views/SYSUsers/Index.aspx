<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.SYSUsersIndexViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	User
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MANAGING USERS</h2>
    
    <%= TempData["Message"]!=null?TempData["Message"]:"" %>
    <h3>
        <%= Html.ActionLink("Add New User", "Add") %>
    </h3>
    <br />
        User Groups List 
    <br />
    <% using (Html.BeginForm())%>
    <% { %>
            <%= Html.DropDownList("GroupID", new SelectList(Model.GroupID as IEnumerable, "GroupID", "GroupName", 
                            Model != null ? Model.GroupID : null), "Select Group", new { onchange = "this.form.submit();" })%>
            <br /><br />
            <%= Html.DropDownList("BranchID", new SelectList(Model.BranchID as IEnumerable, "BranchID", "BranchName", 
                            Model != null ? Model.BranchID : null), "Select Branch", new { onchange = "this.form.submit();" })%>                
            <br /><br />
    <% } %>
    
    Users for <%= Model.GroupID %>
    <h3>
        <%= Html.ActionLink("Add New User", "Add") %>
    </h3>
    <table>
        <tr>
            <th></th>
            <th>
                UserID
            </th>
            <th>
                FullName
            </th>
            <th>
                Password
            </th>
            <th>
                Status
            </th>
            <th>
                CreditDepartment
            </th>
        </tr>

    <% if (Model.SystemUsers != null)
       {    
            foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.UserID }) %> |
                <%= Html.ActionLink("Delete", "Delete", new { id=item.UserID })%>
            </td>
            <td>
                <%= Html.Encode(item.UserID) %>
            </td>
            <td>
                <%= Html.Encode(item.FullName) %>
            </td>
            <td>
                <%= Html.Encode(item.Password) %>
            </td>
            <td>
                <%= Html.Encode(item.Status) %>
            </td>
            <td>
                <%= Html.Encode(item.CreditDepartment) %>
            </td>
        </tr>
    <% } %>
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Add New", "Add") %>
    </p>

</asp:Content>


