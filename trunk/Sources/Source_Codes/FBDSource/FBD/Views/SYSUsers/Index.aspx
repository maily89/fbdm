<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.SYSUsersIndexViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	System User
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MANAGING USERS</h2>
    
    <%= TempData["Message"]!=null?TempData["Message"]:"" %>
    
    <br />
        Branches List 
    <br />
    <% using (Html.BeginForm())%>
    <% { %>
            <%--<%= Html.DropDownList("GroupID", new SelectList(Model.GroupID as IEnumerable, "GroupID", "GroupName", 
                            Model != null ? Model.GroupID : null), "Select Group", new { onchange = "this.form.submit();" })%>
            <br /><br />--%>
            <%= Html.DropDownList("BranchID", new SelectList(Model.Branches as IEnumerable, "BranchID", "BranchName", 
                            Model != null ? Model.BranchID : null), "Select Branch", new { onchange = "this.form.submit();" })%>                
            <br /><br />
    <% } %>
    
    Lines for <%= Model.BranchName %>
    <h3>
        <%= Html.ActionLink("Add New User", "Add") %>
    </h3>
    <table>
        <tr>
            <th></th>
            <th>User ID</th>
            <th>Group</th>
            <th>Branch</th>
            <th>Full Name</th>
            <th>Password</th>
            <th>Status</th>
            <th>Credit Department</th>
            
        </tr>

    <% if (Model.Users != null)
       {    
            foreach (var item in Model.Users) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.UserID }) %> |
                <%= Html.ActionLink("Delete", "Delete", new { id=item.UserID })%>
            </td>
            <td>
                <%= Html.Encode(item.UserID) %>
            </td>
            <td>
                <%= Html.Encode(item.SystemUserGroups.GroupName) %>
            </td>
            <td>
                <%= Html.Encode(item.SystemBranches.BranchName) %>
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
    <% } 
    } %>

    </table>

    <p>
        <%= Html.ActionLink("Add New User", "Add") %>
    </p>

</asp:Content>


