// Decompiled with JetBrains decompiler
// Type: ASPNetChartMaker
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using AIS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

public class ASPNetChartMaker
{
  private Chart chart;

  public string ParameterList { get; set; }

  public string ChartTitle { get; set; }

  public string QueryName { get; set; }

  public string AxisYInterval { get; set; }

  public string QueryUrl { get; set; }

  public string AxisYMaximum { get; set; }

  public string GetImageUrl()
  {
    this.chart = new Chart();
    this.chart.Height = Unit.Pixel(400);
    this.chart.Width = Unit.Pixel(600);
    this.chart.Palette = ChartColorPalette.SemiTransparent;
    this.chart.RenderType = RenderType.BinaryStreaming;
    this.chart.ImageType = ChartImageType.Png;
    this.chart.BackColor = Color.Silver;
    this.chart.BackSecondaryColor = Color.White;
    this.chart.BorderlineDashStyle = ChartDashStyle.Solid;
    this.chart.BackGradientStyle = GradientStyle.TopBottom;
    this.chart.BorderlineWidth = 1;
    this.chart.BorderlineColor = Color.Black;
    this.chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
    this.GetPropertiesFromUrl();
    if (this.ChartTitle != null && this.ChartTitle.Trim().Length > 0)
      this.AddChartTitle();
    this.AddChartArea();
    this.AddChartSeries();
    this.PopulateChart();
    using (MemoryStream imageStream = new MemoryStream())
    {
      this.chart.SaveImage((Stream) imageStream, ChartImageFormat.Png);
      return "data:image/png;base64," + Convert.ToBase64String(imageStream.ToArray(), 0, imageStream.ToArray().Length);
    }
  }

  private void PopulateChart()
  {
    string parameterNames = "";
    string parameterValues = "";
    string parameterList = this.ParameterList;
    char[] chArray = new char[1]{ '|' };
    foreach (string str in parameterList.Split(chArray))
    {
      if (parameterNames.Length == 0)
      {
        parameterNames = ((IEnumerable<string>) str.Split('~')).First<string>();
        parameterValues = ((IEnumerable<string>) str.Split('~')).Last<string>();
      }
      else
      {
        parameterNames += string.Format(",{0}", (object) ((IEnumerable<string>) str.Split('~')).First<string>());
        parameterValues += string.Format(",{0}", (object) ((IEnumerable<string>) str.Split('~')).Last<string>());
      }
    }
    SqlDataReader sqlDataReader = DAL.ReturnDataReader(this.QueryName, parameterNames, parameterValues);
    Series byName = this.chart.Series.FindByName("Series1");
    while (sqlDataReader.Read())
    {
      DataPoint dataPoint = new DataPoint();
      dataPoint.AxisLabel = sqlDataReader["AxisLabel"].ToString();
      dataPoint.YValues = new List<double>()
      {
        Convert.ToDouble(sqlDataReader["YValue"])
      }.ToArray();
      try
      {
        dataPoint.ToolTip = sqlDataReader["ToolTip"].ToString();
      }
      catch
      {
      }
      byName.Points.Add(dataPoint);
    }
    sqlDataReader.Close();
    sqlDataReader.Dispose();
  }

  private void AddChartTitle()
  {
    if (this.chart.Titles.Count == 0)
      this.chart.Titles.Add(new Title()
      {
        Text = this.ChartTitle,
        ShadowColor = Color.Transparent,
        ShadowOffset = 0,
        ForeColor = Color.FromArgb(26, 59, 105),
        Font = new Font("Trebuchet MS", 18.25f, FontStyle.Bold)
      });
    else
      this.chart.Titles.First<Title>().Text = this.ChartTitle;
  }

  private void AddChartArea()
  {
    int num = this.chart.ChartAreas.Count<ChartArea>() + 1;
    ChartArea chartArea1 = new ChartArea();
    chartArea1.Name = string.Format("ChartArea{0}", (object) num);
    chartArea1.BorderColor = Color.Black;
    chartArea1.BorderDashStyle = ChartDashStyle.Solid;
    chartArea1.BackSecondaryColor = Color.White;
    chartArea1.ShadowColor = Color.Transparent;
    chartArea1.BackColor = Color.White;
    ChartArea chartArea2 = chartArea1;
    if (!string.IsNullOrEmpty(this.AxisYMaximum))
      chartArea2.AxisY.Maximum = Convert.ToDouble(this.AxisYMaximum);
    if (!string.IsNullOrEmpty(this.AxisYInterval))
      chartArea2.AxisY.Interval = Convert.ToDouble(this.AxisYInterval);
    else
      this.AxisYInterval = "10";
    ChartArea3DStyle chartArea3Dstyle = new ChartArea3DStyle()
    {
      Rotation = 10,
      Perspective = 10,
      Enable3D = false,
      Inclination = 0,
      IsRightAngleAxes = false,
      IsClustered = false,
      WallWidth = 0
    };
    Axis axis1 = new Axis()
    {
      LineColor = Color.FromArgb(64, 64, 64, 64),
      Interval = Convert.ToDouble(this.AxisYInterval),
      IsStartedFromZero = true
    };
    axis1.LabelStyle = new LabelStyle()
    {
      Font = new Font("Trebuchet MS", 8.25f, FontStyle.Bold)
    };
    axis1.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
    axis1.Title = "Avg. Score";
    axis1.TitleFont = new Font("Trebuchet MS", 14.25f, FontStyle.Bold);
    axis1.TitleForeColor = Color.FromArgb(26, 59, 105);
    Axis axis2 = new Axis()
    {
      LineColor = Color.FromArgb(64, 64, 64, 64),
      Interval = 1.0
    };
    axis2.LabelStyle = new LabelStyle()
    {
      Font = new Font("Trebuchet MS", 8.25f, FontStyle.Bold),
      Interval = 1.0
    };
    axis2.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
    axis2.Title = "Month";
    axis2.TitleFont = new Font("Trebuchet MS", 14.25f, FontStyle.Bold);
    axis2.TitleForeColor = Color.FromArgb(26, 59, 105);
    chartArea2.Area3DStyle = chartArea3Dstyle;
    chartArea2.AxisY = axis1;
    chartArea2.AxisX = axis2;
    this.chart.ChartAreas.Add(chartArea2);
  }

  private void AddChartSeries()
  {
    int num = this.chart.Series.Count + 1;
    Series series = new Series();
    series.Name = string.Format("Series{0}", (object) num);
    series.ChartType = SeriesChartType.Line;
    series.MarkerStyle = MarkerStyle.Circle;
    series.Label = "#VALY";
    series.MarkerSize = 12;
    this.chart.Series.Add(series);
  }

  private void GetPropertiesFromUrl()
  {
    if (this.QueryUrl == null || this.QueryUrl.Trim().Length <= 0)
      return;
    this.ParameterList = HttpUtility.ParseQueryString(this.QueryUrl).Get("pl");
    this.ChartTitle = HttpUtility.ParseQueryString(this.QueryUrl).Get("ctitle");
    this.QueryName = HttpUtility.ParseQueryString(this.QueryUrl).Get("qn");
    this.AxisYInterval = HttpUtility.ParseQueryString(this.QueryUrl).Get("axisyinterval");
    if (!this.QueryUrl.ToLower().Contains("axisymaximum"))
      return;
    this.AxisYMaximum = HttpUtility.ParseQueryString(this.QueryUrl).Get("axisymaximum");
  }
}
