<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.Models.SystemUsers>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Login Success
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><font style="color: Blue"> HAVE A NICE WORKING DAY, <%=Model.FullName %> </font></h2>
    <br />
        
    <b>User ID          : <font style="color: Brown"><%=Model.UserID %></font></b>
    <br />
    <b>Full Name        : <font style="color: Brown"><%=Model.FullName %></font></b>
    <br />
    <b>Credit Department: <font style="color: Brown"><%=Model.CreditDepartment %></font></b>
    <br />
    <b>In Group         : <font style="color: Brown"><%=Model.SystemUserGroups.GroupName %></font></b>
    <br />
    <b>At Branch        : <font style="color: Brown"><%=Model.SystemBranches.BranchName %></font></b>
    <br />
    <b><%= Html.ActionLink("Want to change your password?", "ChangePassword") %></b>

</asp:Content>
