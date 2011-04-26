<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ListUser.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBD.CommonUtilities.Vector>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Cluster
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Cluster</h2>
<%    
        System.Web.UI.DataVisualization.Charting.Chart Chart2 = new System.Web.UI.DataVisualization.Charting.Chart();
        Chart2.Width = 1000;
        Chart2.Height = 400;
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
            // Set axis title
            Chart2.ChartAreas["Series 1"].AxisX.Title = "Collateral Index Score";
            Chart2.ChartAreas["Series 1"].AxisY.Title = "Basic Index Score";
            
            Chart2.ChartAreas["Series 1"].Area3DStyle.Enable3D = true;
            //Chart2.Series["Default"]["DrawingStyle"]="
            Chart2.ChartAreas["Series 1"].Area3DStyle.Inclination = 10;
            Chart2.ChartAreas["Series 1"].Area3DStyle.IsClustered = true;


            //Work with legends
            Chart2.Legends.Add("Default");
            Chart2.Legends["Default"].CustomItems.Clear();

            // Add new custom legend item
            //Chart2.Legends["Default"].CustomItems.Add(new LegendItem("LegendItem", System.Drawing.Color.Red, ""));
            //Chart2.Legends["Default"].CustomItems[0].Cells.Add(new LegendCell(LegendCellType.Text, "Central", System.Drawing.ContentAlignment.MiddleLeft));
            //Chart2.Legends["Default"].CustomItems[0].Cells[0].Text = "Central";

            List<FBD.Models.IndividualClusterRanks> cln = (List<FBD.Models.IndividualClusterRanks>)ViewData["clusterName"];
            int index = 1;
            for (int i = 0; i < int.Parse(ViewData["cluster"].ToString()); i++)
            {   //Create series and config them
                Chart2.Series.Add(i.ToString());
                Chart2.Series[i.ToString()].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Point;
                Chart2.Series[i.ToString()].MarkerSize = 10;
                Chart2.Series[i.ToString()].MarkerStyle = MarkerStyle.Circle;
                Chart2.Series[i.ToString()]["PointWidth"] = "0.2";
                //add link to legend
                Chart2.Series[i.ToString()].LegendMapAreaAttributes = "onclick=\"javascript:loadCustomer(#SER);\"";
                    
                // add points to series    
                List<FBD.CommonUtilities.Vector> listResult = (List<FBD.CommonUtilities.Vector>)ViewData[i.ToString()];
                int numberMember = listResult.Count;
                for (int k = 0; k < numberMember; k++)
                {
                    Chart2.Series[i.ToString()].Points.AddXY(listResult[k].x, listResult[k].y);
                    //Chart2.Series[i.ToString()]["LineTension"] = "2.0";

                    //Chart2.Series[i.ToString()].IsValueShownAsLabel = true;
                    //Chart2.Series[i.ToString()]["DrawingStyle"] = "Cylinder";
                    //Chart2.Series[i.ToString()].XAxisType = AxisType.Primary;

                    Chart2.Series[i.ToString()].Points[k].ToolTip = listResult[k].CustomerName.ToString() + " : (" + listResult[k].x + "-"+listResult[k].y+")";
                    index++;
                }
                //get info from this chart
                //DataPoint MaxPoint = Chart2.Series[i.ToString()].Points.FindMaxByValue();
                //MaxPoint.IsValueShownAsLabel = true;
                //DataPoint minPoint = Chart2.Series[i.ToString()].Points.FindMinByValue();
                ////minPoint.IsValueShownAsLabel = true;

                int num = Chart2.Series[i.ToString()].Points.Count() ;
                //legend text
                Chart2.Series[i.ToString()].LegendText = "Cluster "+ cln[i].Rank.ToString()  + " : " + num.ToString() + " members";
                //Legend tooltip
                //Chart2.Series[i.ToString()].LegendToolTip = "Min: " + minPoint.YValues[0] + " - Max: " + MaxPoint.YValues[0];
                //Chart2.Series[i.ToString()].PostBackValue

            }
            //add centroid List

            Chart2.Series.Add("centroidList");
            Chart2.Series["centroidList"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Point;
            Chart2.Series["centroidList"].Color = System.Drawing.Color.Red;
            Chart2.Series["centroidList"].MarkerSize = 15;
            Chart2.Series["centroidList"].MarkerStyle = MarkerStyle.Star5;
            Chart2.Series["centroidList"]["PointWidth"] = "0.2";
            // add points to series    
            List<FBD.CommonUtilities.Vector> centroidList = (List<FBD.CommonUtilities.Vector>)ViewData["centroidList"];
            
            int numberCentroid = centroidList.Count;
            for (int k = 0; k < numberCentroid; k++)
            {
                Chart2.Series["centroidList"].Points.AddXY(centroidList[k].x, centroidList[k].y);
                //Chart2.Series[i.ToString()]["LineTension"] = "2.0";

                //Chart2.Series[i.ToString()].IsValueShownAsLabel = true;
                //Chart2.Series[i.ToString()]["DrawingStyle"] = "Cylinder";
                //Chart2.Series[i.ToString()].XAxisType = AxisType.Primary;
                Chart2.Series["centroidList"].Points[k].ToolTip = "(" + Math.Round(centroidList[k].x *100)/10 + "-" +Math.Round(centroidList[k].y*100)/10 + ")";
                Chart2.Series["centroidList"].MapAreaAttributes = "onclick=\"javascript:loadCustomer(#INDEX);\"";
            }
        }
        else
        {
            Title t = new Title("Number of customer is not enough for mining", Docking.Top, new System.Drawing.Font("Trebuchet MS", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
            Chart2.Titles.Add(t);
        }
    //onclick 
        //foreach (Series series in Chart2.Series)
        //{
        //    //series.MapAreaAttributes =
        //    //    "onclick=\"javascript:alert('Mouse click event captured in the series! Series Name: #SER, Point Index: #INDEX');\"";
        //    //series.LegendMapAreaAttributes =
        //    //    "onclick=\"javascript:alert('Mouse click event captured in the legend! Series: #SER, Total Values: #TOTAL{C}');\"";

        //    //series.MapAreaAttributes =
        //    //    "onclick=\"javascript:loadCustomer(#SER);\"";
            
        //} 
    
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

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
    function ShowWaitIcon() {
        document.getElementById("cusomterResponse").innerHTML = "<img src='/Content/images/wait.gif' />" ;
    }
    function loadCustomer(x) {
        //alert(x);
        document.getElementById("list").innerHTML = "<img src='/Content/images/loading.gif' />";
        $("#list").load('/INVMining/ListCustomer/' + x, new function() { window.scrollBy(800, 500) });
        
    }
    </script>
</asp:Content>