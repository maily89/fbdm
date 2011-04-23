<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/nullSite.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	NextStep
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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
            if (ViewData["done"] != null)
            {
                t = new Title("DONE", Docking.Top, new System.Drawing.Font("Trebuchet MS", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
            }
            Chart2.Titles.Add(t);
            Chart2.BackGradientStyle = GradientStyle.TopBottom;
            Chart2.BackColor = System.Drawing.Color.WhiteSmoke;
            Chart2.BackSecondaryColor = System.Drawing.Color.FromArgb(211, 223, 240);
            Chart2.BorderlineWidth = 1;
            Chart2.BorderColor = System.Drawing.Color.White;

            Chart2.ChartAreas.Add("Series 1");
            Chart2.ChartAreas["Series 1"].BackGradientStyle = GradientStyle.TopBottom;
            Chart2.ChartAreas["Series 1"].BackColor = System.Drawing.Color.White;
            Chart2.ChartAreas["Series 1"].BackSecondaryColor = System.Drawing.Color.FromArgb(211, 223, 240);
            Chart2.ChartAreas["Series 1"].BorderDashStyle = ChartDashStyle.Dash;

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
            if(ViewData["sumDistance"]!=null)
            Chart2.Legends["Default"].CustomItems[0].Cells[0].Text = ViewData["sumDistance"].ToString();


            
            //System.Drawing.Color[] color = { System.Drawing.Color.Red, System.Drawing.Color.Orange, System.Drawing.Color.Yellow, System.Drawing.Color.Green, System.Drawing.Color.Indigo, System.Drawing.Color.Blue, System.Drawing.Color.Violet};
            
            for (int i = 0; i < int.Parse(ViewData["cluster"].ToString()); i++)
            {   //Create series and config them
                Chart2.Series.Add(i.ToString());
                Chart2.Series[i.ToString()].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Point;
                Chart2.Series[i.ToString()].MarkerSize = 10;
                Chart2.Series[i.ToString()].MarkerStyle = MarkerStyle.Circle;
                Chart2.Series[i.ToString()]["PointWidth"] = "0.2";
              //  if(i<color.Length)
               // Chart2.Series[i.ToString()].Color = color[i];
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

                    Chart2.Series[i.ToString()].Points[k].ToolTip = listResult[k].CustomerName.ToString() + " : (" + listResult[k].x + "-" + listResult[k].y + ")";
                    
                }
                //get info from this chart
                DataPoint MaxPoint = Chart2.Series[i.ToString()].Points.FindMaxByValue();
                //MaxPoint.IsValueShownAsLabel = true;
                DataPoint minPoint = Chart2.Series[i.ToString()].Points.FindMinByValue();
                //minPoint.IsValueShownAsLabel = true;

                int num = Chart2.Series[i.ToString()].Points.Count() + 1;
                //legend text

               Chart2.Series[i.ToString()].LegendText = "Group " + (i + 1).ToString() + " : " + num.ToString() + " members";
                //Legend tooltip
                Chart2.Series[i.ToString()].LegendToolTip = "Min: " + minPoint.YValues[0] + " - Max: " + MaxPoint.YValues[0];

            }
            //add oldCentroid List

            Chart2.Series.Add("oldCentroid");
            Chart2.Series["oldCentroid"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Point;
            Chart2.Series["oldCentroid"].Color = System.Drawing.Color.IndianRed;
            Chart2.Series["oldCentroid"].MarkerSize = 15;
            Chart2.Series["oldCentroid"].MarkerStyle = MarkerStyle.Star5;
            Chart2.Series["oldCentroid"]["PointWidth"] = "0.2";
            // add points to series    
            List<FBD.CommonUtilities.Vector> centroidList = (List<FBD.CommonUtilities.Vector>)ViewData["oldCentroid"];
            int numberCentroid = (centroidList.Count > 1) ? centroidList.Count : 0; 
            for (int k = 0; k < numberCentroid; k++)
            {
                Chart2.Series["oldCentroid"].Points.AddXY(centroidList[k].x, centroidList[k].y);
                //Chart2.Series[i.ToString()]["LineTension"] = "2.0";
                //Chart2.Series[i.ToString()].IsValueShownAsLabel = true;
                //Chart2.Series[i.ToString()]["DrawingStyle"] = "Cylinder";
                //Chart2.Series[i.ToString()].XAxisType = AxisType.Primary;
                Chart2.Series["oldCentroid"].Points[k].ToolTip = "(" + Math.Round(centroidList[k].x * 100) / 10 + "-" + Math.Round(centroidList[k].y * 100) / 10 + ")";
            }

            //add NewCentroid List

            Chart2.Series.Add("newCentroid");
            Chart2.Series["newCentroid"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Point;
            Chart2.Series["newCentroid"].Color = System.Drawing.Color.Red;
            Chart2.Series["newCentroid"].MarkerSize = 15;
            Chart2.Series["newCentroid"].MarkerStyle = MarkerStyle.Star5;
            Chart2.Series["newCentroid"]["PointWidth"] = "0.2";
            // add points to series    
            List<FBD.CommonUtilities.Vector> newCentroid = (List<FBD.CommonUtilities.Vector>)ViewData["newCentroid"];
            //int numNewCentroid = (newCentroid.Count > 1) ? newCentroid.Count : 0;
            for (int k = 0; k < numberCentroid; k++)
            {
                Chart2.Series["newCentroid"].Points.AddXY(newCentroid[k].x, newCentroid[k].y);
                //Chart2.Series[i.ToString()]["LineTension"] = "2.0";
                //Chart2.Series[i.ToString()].IsValueShownAsLabel = true;
                //Chart2.Series[i.ToString()]["DrawingStyle"] = "Cylinder";
                //Chart2.Series[i.ToString()].XAxisType = AxisType.Primary;
                Chart2.Series["newCentroid"].Points[k].ToolTip = "(" + Math.Round(newCentroid[k].x * 100) / 10 + "-" +Math.Round(newCentroid[k].y*100)/10 + ")";
            }
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
    
   
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
