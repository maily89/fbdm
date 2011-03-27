<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.SYSUsersIndexViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Managing System users
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MANAGING SYSTEM USERS</h2>
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%><br /></p>
    
    <%using (Html.BeginForm())
            {%>
            <%= Html.Label("Choose a Branch")%>
            <%= Html.DropDownList("BranchID", new SelectList(Model.Branches as IEnumerable, "BranchID", "BranchName",
                            Model != null ? Model.BranchID : null),"Select Branch", new { onchange = "this.form.submit();" })%>
            <%} %>
            <br />
        <p>
        <%= Html.ActionLink("Add new user", "Add") %>
    </p>
    List of users in <%=Model.BranchName %>:         
    <table>
        
        <tr>
            <th></th>
            <th>
                User ID
            </th>
            <th>
                Full name
            </th>
            <th>
                Branch
            </th>
            <th>
                Group
            </th>
            <th>
                Status
            </th>
            <th>
                Credit department
            </th>
        </tr>

    <% foreach (var item in Model.Users) {
            %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.UserID}) %> |
                <%= Html.ActionLink("Delete", "Delete", new { id = item.UserID }, new { onclick = "javascript:return confirm('Are you sure you wish to delete the user " + item.FullName + "?');" })%>
            </td>
            <td>
                <%= Html.Encode(item.UserID) %>
            </td>
            <td>
                <%= Html.Encode(item.FullName) %>
            </td>
            <td>
                <%=Html.Encode(item.SystemBranches.BranchName) %>
            </td>
            <td>
                <%=Html.Encode(item.SystemUserGroups.GroupName) %>
            </td>
            <td>
                <%=Html.Encode(item.Status) %>
            </td>
            <td>
                <%=Html.Encode(item.CreditDepartment)%>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Add new user", "Add") %>
    </p>

</asp:Content>
