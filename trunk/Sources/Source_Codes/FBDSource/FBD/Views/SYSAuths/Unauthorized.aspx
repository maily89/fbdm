<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Unauthorized
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>UNAUTHORIZED</h2>
    
    <h3><font style="background-color: Olive; color: White">You dont have permission to user this feature</font></h3>
    <i><b>The system classifies users to guarantee the security requirement. </b></i> <br />
    <i><b>You need to login with account which has enough power. </b></i>
    
</asp:Content>