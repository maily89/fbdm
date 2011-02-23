<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.SYSUserGroupsRightsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Managing user group rights
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MANAGING USER GROUP RIGHTS</h2>
    <%= TempData["Message"]!=null?TempData["Message"]:"" %><br />
    User Group list <br />
    <% using (Html.BeginForm())
       { %>
    <%= Html.DropDownList("GroupID", new SelectList(Model.UserGroups as IEnumerable,
                "GroupID", "GroupName", Model != null ? Model.GroupID : null), "Select User Group", new { onchange = "this.form.submit();" })%>
    <br /><br />
    <%} %>
    List Rights of <%=Model.GroupName %>>
    
    <table>
        <tr>
            <th>Enable</th>
            <th>Right ID</th>
            <th>Right Name</th>
        </tr>

    <% if (Model.Rights != null)
       {
          foreach (var item in Model.Rights)
          { %>
                <tr>
                    <td>
                    <%
                      //  if () %>
                        <%//= Html.CheckBox() %>
                    </td>
                    <td>
                        <%= Html.Encode(item.RightID) %>
                    </td>
                    <td>
                        <%= Html.Encode(item.RightName) %>
                    </td>
                </tr>
            
            <%} %>
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

