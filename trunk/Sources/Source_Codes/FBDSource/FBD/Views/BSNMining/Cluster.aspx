<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListUser.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.CommonUtilities.Vector>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Cluster
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Cluster</h2>
    <%  
        System.Web.UI.DataVisualization.Charting.Chart Chart2 = new System.Web.UI.DataVisualization.Charting.Chart();
        Chart2.Width = 800;
        Chart2.Height = 500;
        Chart2.RenderType = RenderType.ImageTag;
        if (!"0".Equals(ViewData["cluster"].ToString()))
        {
            //customize chart display:
            Chart2.Palette = ChartColorPalette.Bright;
            Title t = new Title("cluster into " + ViewData["cluster"].ToString() + " group", Docking.Top, new System.Drawing.Font("Trebuchet MS", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
            Chart2.Titles.Add(t);
            Chart2.BackGradientStyle = GradientStyle.TopBottom;
            Chart2.BackColor = System.Drawing.Color.White;
            Chart2.BackSecondaryColor = System.Drawing.Color.FromArgb(211, 223, 240);
            Chart2.BorderlineWidth = 1;
            Chart2.BorderColor = System.Drawing.Color.White;

            Chart2.ChartAreas.Add("Series 1");
            Chart2.ChartAreas["Series 1"].BackGradientStyle = GradientStyle.TopBottom;
            Chart2.ChartAreas["Series 1"].BackColor = System.Drawing.Color.White;
            Chart2.ChartAreas["Series 1"].BackSecondaryColor = System.Drawing.Color.FromArgb(211, 223, 240);
            Chart2.ChartAreas["Series 1"].BorderDashStyle = ChartDashStyle.Solid;
            Chart2.ChartAreas["Series 1"].AxisY.Title = "Total Score";
            Chart2.ChartAreas["Series 1"].Area3DStyle.Enable3D = true;
            //Chart2.Series["Default"]["DrawingStyle"]="
            Chart2.ChartAreas["Series 1"].Area3DStyle.Inclination = 10;
            Chart2.ChartAreas["Series 1"].Area3DStyle.IsClustered = true;

            //Work with legends
            Chart2.Legends.Add("Default");
            Chart2.Legends["Default"].CustomItems.Clear();
            
             //Add new custom legend item
            //Chart2.Legends["Default"].CustomItems.Add(new LegendItem("LegendItem", System.Drawing.Color.Red, ""));
            //Chart2.Legends["Default"].CustomItems[0].Cells.Add(new LegendCell(LegendCellType.Text, "Central", System.Drawing.ContentAlignment.MiddleLeft));
            //Chart2.Legends["Default"].CustomItems[0].Cells[0].Text = "Central";
            System.Drawing.Color[] colors = {System.Drawing.Color.Red,System.Drawing.Color.Orange,System.Drawing.Color.Yellow,System.Drawing.Color.Green,System.Drawing.Color.DeepSkyBlue,System.Drawing.Color.Indigo,System.Drawing.Color.Violet};
            int index = 1;
            List<FBD.Models.BusinessClusterRanks> cln = (List<FBD.Models.BusinessClusterRanks>)ViewData["clusterName"];
            for (int i = 0; i < int.Parse(ViewData["cluster"].ToString()); i++)
            {   
                //Create series and config them
                Chart2.Series.Add(i.ToString());
                Chart2.Series[i.ToString()].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.SplineArea;
                Chart2.Series[i.ToString()].MarkerSize = 10;
                Chart2.Series[i.ToString()]["PointWidth"] = "0.2";
                if(i<colors.Length)
                    Chart2.Series[i.ToString()].Color = colors[i];
                //add onclick
                Chart2.Series[i.ToString()].LegendMapAreaAttributes =
                       "onclick=\"javascript:loadCustomer(#SER);\"";
                // add points to series 
                List<FBD.CommonUtilities.Vector> listResult = (List<FBD.CommonUtilities.Vector>)ViewData[i.ToString()];
                int numberMember = listResult.Count;
                for (int k = 0; k < numberMember; k++)
                {
                    Chart2.Series[i.ToString()].Points.AddXY(index, listResult[k].x);
                    Chart2.Series[i.ToString()]["LineTension"] = "2.0";

                    Chart2.Series[i.ToString()].Points[k].ToolTip = listResult[k].CustomerName.ToString() + " : " + listResult[k].x;
                    index++;
                }
                //get info from this chart
                if (numberMember > 0)
                {
                    DataPoint MaxPoint = Chart2.Series[i.ToString()].Points.FindMaxByValue();
                    MaxPoint.IsValueShownAsLabel = true;
                    DataPoint minPoint = Chart2.Series[i.ToString()].Points.FindMinByValue();
                    //minPoint.IsValueShownAsLabel = true;
                    int num = Chart2.Series[i.ToString()].Points.Count();
                    
                    //legend text
                    Chart2.Series[i.ToString()].LegendText = "Cluster " + cln[i].Rank + " : " + num.ToString() + " members";
                    //Legend tooltip
                    Chart2.Series[i.ToString()].LegendToolTip = "Min: " + minPoint.YValues[0] + " - Max: " + MaxPoint.YValues[0];
                }
            }
            //add centroid List
            //Chart2.Series.Add("centroidList");
            //Chart2.Series["centroidList"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Point;
            //Chart2.Series["centroidList"].Color = System.Drawing.Color.Red;
            //Chart2.Series["centroidList"].MarkerSize = 10;
            //Chart2.Series["centroidList"]["PointWidth"] = "0.2";
            // add points to series    

        }
        else
        {
            Title t = new Title("Number of customer is not enough for mining", Docking.Top, new System.Drawing.Font("Trebuchet MS", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
            Chart2.Titles.Add(t);
        }
        // Chart2.Legends.Add("Legend1");
        // Render chart control
      
        Chart2.Page = this;

        HtmlTextWriter writer = new HtmlTextWriter(Page.Response.Output);
        Chart2.RenderControl(writer);

    %>
    <div id="cusomterResponse" class="done">
        <% if (!"0".Equals(ViewData["cluster"].ToString()))
               using (Ajax.BeginForm("Save", new AjaxOptions { UpdateTargetId = "cusomterResponse" }))
               {%>
        <input type="submit" value="Save" onclick="ShowWaitIcon()" />
        <%}%>
    </div>
    <div id="list">
    </div>

</asp:Content>
<asp:Content ID="Script" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript">
    function ShowWaitIcon() {
        document.getElementById("cusomterResponse").innerHTML = "<img src='../../Content/images/wait.gif' />" ;
    }
    function loadCustomer(x) {

        document.getElementById("list").innerHTML = "<img src='/Content/images/loading.gif' />";
        $("#list").load('/BSNMining/GetCustomerList/' + x, new function() { window.scrollTo(0, 1000) });
    }
    </script>
</asp:Content>
