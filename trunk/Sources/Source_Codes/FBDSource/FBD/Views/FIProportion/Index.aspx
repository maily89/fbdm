<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.ViewModels.FIProportionViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Financial Index Proportion
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MANAGING FINANCIAL INDEX PROPORTION</h2>
    
    <%= TempData["Message"]!=null?TempData["Message"]:"" %><br />

    <table>
        <tr>
            <td>
                <%= Html.Label("Choose an industry") %>
            </td>
        
            <td>
                <% using (Html.BeginForm())
                   { %>
                <%= Html.DropDownList("IndustryID", new SelectList(Model.Industries as IEnumerable,
                            "IndustryID", "IndustryName", Model.IndustryID))%>
            </td>
                
        </tr>
        
        <tr>
            <td>
                <%= Html.Label("Financial Index Proportion") %>
            </td>
            
            <td>
                <table>
                    <tr>
                        <th>Enable</th>
                        <th>Index ID</th>
                        <th>Financial Index</th>
                        <th>Proportion</th>
                    </tr>
                
                    <% for (int i = 0; i < Model.ProportionRows.Count(); i++ )
                       { %>
                    <tr>
                        <td>
                            <% if (Model.ProportionRows[i].LeafIndex == true)
                               { %>
                            <%= Html.CheckBoxFor(model => model.ProportionRows[i].Checked)%>
                            <% }
                               else
                               {%>
                                +
                            <% } %>
                        </td>
                        
                        <td>
                            <%= Model.ProportionRows[i].IndexID %>
                        </td>
                        
                        <td>
                            <%= Model.ProportionRows[i].IndexName %>
                        </td>
                        
                        <td>
                            <%= Html.TextBoxFor(model => model.ProportionRows[i].Proportion) %>
                        </td>
                    </tr>
                    <% } %>
                </table>
            </td>
        </tr>
        
        <tr>
            <td></td>
            
            <td>
                <input type="submit" value="Save Changes" />
            </td>
        </tr>
        <%} %>
    </table>
</asp:Content>
