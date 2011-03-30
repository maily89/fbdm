<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<int>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Individual Customer</h2>
    <table>
    <tbody>
        <tr>
        <td><%= Html.ActionLink("Edit Info", "EditInfo", new { id=Model })%></td>
        </tr>
        <tr>
        <td><%= Html.ActionLink("Edit Basic Index", "EditBasicScore", new { id = Model })%></td>
        </tr>
        <tr>
        <td><%= Html.ActionLink("Edit Collateral Index", "EditCollateralScore", new { id = Model })%></td>
        </tr>
    </tbody>
    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

</asp:Content>
