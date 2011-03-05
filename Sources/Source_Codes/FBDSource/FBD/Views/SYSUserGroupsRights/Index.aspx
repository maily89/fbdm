<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.SYSUserGroupsRightsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Managing user group rights
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MANAGING USER GROUP RIGHTS</h2>
    
    <p class="scc-message"><%= TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.SCC_MESSAGE] : ""%><br /></p>
    <p class="err-message"><%= TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] != null ? TempData[FBD.CommonUtilities.Constants.ERR_MESSAGE] : ""%><br /></p>
        
    <table>
        <tr>
        <%using (Html.BeginForm())
          {%>
           <td> <%= Html.Label("Choose a Group")%></td>
           <td> <%= Html.DropDownList("UserGroup", new SelectList(Model.LstUserGroups as IEnumerable, "GroupID", "GroupName", Model.GroupID), new { onchange = "this.form.submit();" })%></td>
       <%} %>
       </tr>
       
       <% using (Html.BeginForm())
            { %>
        <tr>            
            <%= Html.HiddenFor(model => model.GroupID) %>
            <%= Html.Hidden("NumberOfRightRows", Model.LstGroupRightRows.Count) %>
            <td>
                <%= Html.Label("System Deauthentication") %>
            </td>
           <td>
                <table>
                    <tr>
                        <th>Enable</th>
                        <th>Right ID</th>
                        <th>Right Name</th>
                    </tr>
                    
                    <% for (int i = 0; i< Model.LstGroupRightRows.Count(); i++){ %>
                    <tr>
                        <%= Html.Hidden("Model.LstGroupRightRows.Index", i) %>
                        <%= Html.HiddenFor(model => model.LstGroupRightRows[i].GroupRightID) %>
                        
                        <td>
                            <%= Html.CheckBoxFor(model => model.LstGroupRightRows[i].Checked)%>
                        </td>
                        <td>
                            <%= Model.LstGroupRightRows[i].RightID %>
                            <%= Html.HiddenFor(model => model.LstGroupRightRows[i].RightID) %>
                        </td>
                        <td>
                            <%= Model.LstGroupRightRows[i].RightName %>
                            <%= Html.HiddenFor(model => model.LstGroupRightRows[i].RightName) %>
                        </td>
                    </tr>
                    <%} %>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
            
            <td>
                <input type="submit" name="Save" value="Save" />
            </td>
        </tr>
        <%} %>        
    </table>

</asp:Content>

