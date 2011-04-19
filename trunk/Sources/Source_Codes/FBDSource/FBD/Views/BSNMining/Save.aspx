<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBD.CommonUtilities.Vector>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Index</h2>
    <%
        
        System.Web.UI.DataVisualization.Charting.Chart Chart2 = new System.Web.UI.DataVisualization.Charting.Chart();
        Chart2.Width = 800;
        Chart2.Height = 500;
        Chart2.RenderType = RenderType.ImageTag;
        
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

            Chart2.ChartAreas["Series 1"].Area3DStyle.Enable3D = true;
            //Chart2.Series["Default"]["DrawingStyle"]="
            Chart2.ChartAreas["Series 1"].Area3DStyle.Inclination = 10;
            Chart2.ChartAreas["Series 1"].Area3DStyle.IsClustered = true;


            //Work with legends
            Chart2.Legends.Add("Default");
            Chart2.Legends["Default"].CustomItems.Clear();

            // Add new custom legend item
            Chart2.Legends["Default"].CustomItems.Add(new LegendItem("LegendItem", System.Drawing.Color.Red, ""));
            Chart2.Legends["Default"].CustomItems[0].Cells.Add(new LegendCell(LegendCellType.Text, "Central", System.Drawing.ContentAlignment.MiddleLeft));
            Chart2.Legends["Default"].CustomItems[0].Cells[0].Text = "Central";

            int index = 1;
            for (int i = 0; i < int.Parse(ViewData["cluster"].ToString()); i++)
            {   //Create series and config them
                Chart2.Series.Add(i.ToString());
                Chart2.Series[i.ToString()].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.SplineArea;
                Chart2.Series[i.ToString()].MarkerSize = 10;
                Chart2.Series[i.ToString()]["PointWidth"] = "0.2";
                // add points to series    
                Random r = new Random();
                List<FBD.CommonUtilities.Vector> listResult = (List<FBD.CommonUtilities.Vector>)ViewData[i.ToString()];
                int numberMember = listResult.Count;
                for (int k = 0; k < numberMember; k++)
                {
                    Chart2.Series[i.ToString()].Points.AddXY(index, listResult[k].x);
                    Chart2.Series[i.ToString()]["LineTension"] = "2.0";

                    //Chart2.Series[i.ToString()].IsValueShownAsLabel = true;
                    //Chart2.Series[i.ToString()]["DrawingStyle"] = "Cylinder";
                    //Chart2.Series[i.ToString()].XAxisType = AxisType.Primary;

                    Chart2.Series[i.ToString()].Points[k].ToolTip = listResult[k].CustomerName.ToString() + " : " + listResult[k].x;
                    index++;
                }
                //get info from this chart
                DataPoint MaxPoint = Chart2.Series[i.ToString()].Points.FindMaxByValue();
                MaxPoint.IsValueShownAsLabel = true;
                DataPoint minPoint = Chart2.Series[i.ToString()].Points.FindMinByValue();
                //minPoint.IsValueShownAsLabel = true;

                int num = Chart2.Series[i.ToString()].Points.Count() + 1;
                //legend text

                Chart2.Series[i.ToString()].LegendText = "Group " + (i + 1).ToString() + " : " + num.ToString() + " members";
                //Legend tooltip
                Chart2.Series[i.ToString()].LegendToolTip = "Min: " + minPoint.YValues[0] + " - Max: " + MaxPoint.YValues[0];

            
        }
        
        // Chart2.Legends.Add("Legend1");
        // Render chart control
        Chart2.Page = this;
        HtmlTextWriter writer = new HtmlTextWriter(Page.Response.Output);
        Chart2.RenderControl(writer);

    %>
            <asp:Label runat="server" Text="DONE" ForeColor=Blue/>

   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
