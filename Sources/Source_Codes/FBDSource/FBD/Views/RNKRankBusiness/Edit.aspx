<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<int>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Customer Business</h2>
    <table>
    <tbody>
        <tr>
        <td><%= Html.ActionLink("Edit Info", "EditInfo", new { id=Model })%></td>
        </tr>
        <tr>
        <td><%= Html.ActionLink("Edit Scale Info", "EditScale", new { id = Model })%></td>
        </tr>
        <tr>
        <td><%= Html.ActionLink("Edit Financial Index", "AddFinancialScore", new { id = Model })%></td>
        </tr>
        <tr>
        <td><%= Html.ActionLink("Edit Non-Financial Index Score", "EditNonFinancialIndex", new { id = Model })%></td>
        </tr>
    </tbody>
    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

</asp:Content>
