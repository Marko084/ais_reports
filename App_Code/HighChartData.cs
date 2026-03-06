// Decompiled with JetBrains decompiler
// Type: HighChartData
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

[DataContract]
[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
public class HighChartData
{
  [DataMember(EmitDefaultValue = false, Name = "chart")]
  public HighChartData.HighChartType ChartType;
  [DataMember(EmitDefaultValue = false, Name = "title")]
  public HighChartData.HighChartTitle ChartTitle;
  [DataMember(EmitDefaultValue = false, Name = "xAxis")]
  public HighChartData.HighChartXAxis ChartXAxis;
  [DataMember(EmitDefaultValue = false, Name = "yAxis")]
  public HighChartData.HighChartYAxis ChartYAxis;
  [DataMember(EmitDefaultValue = false, Name = "tooltip")]
  public HighChartData.HighChartTooltip ChartTooltip;
  [DataMember(EmitDefaultValue = false, Name = "series")]
  public List<HighChartData.HighChartSeries> ChartSeriesData;
  [DataMember(EmitDefaultValue = false, Name = "plotOptions")]
  public HighChartData.HighChartPlotOptions ChartPlotOptions;

  public HighChartData()
  {
    this.ChartType = new HighChartData.HighChartType();
    this.ChartTitle = new HighChartData.HighChartTitle();
    this.ChartXAxis = new HighChartData.HighChartXAxis();
    this.ChartYAxis = new HighChartData.HighChartYAxis();
    this.ChartTooltip = new HighChartData.HighChartTooltip();
    this.ChartSeriesData = new List<HighChartData.HighChartSeries>();
  }

  public HighChartData Get() => new HighChartData()
  {
    ChartType = {
      Type = "column"
    },
    ChartTitle = {
      TitleText = "Chart Test"
    }
  };

  public HighChartData Get(
    string query_name,
    string pname,
    string pvalue,
    string theme,
    string chart_type)
  {
    return new HighChartsBuilder()
    {
      ChartTheme = theme,
      ChartType = chart_type.ToLower(),
      ParamNames = pname,
      ParamValues = pvalue,
      QueryName = query_name
    }.Create();
  }

  [DataContract]
  public class HighChartType
  {
    [DataMember(EmitDefaultValue = false, Name = "type")]
    public string Type;
  }

  [DataContract]
  public class HighChartTitle
  {
    [DataMember(EmitDefaultValue = false, Name = "text")]
    public string TitleText;

    public HighChartTitle() => this.TitleText = "";
  }

  [DataContract]
  public class HighChartXAxis
  {
    [DataMember(EmitDefaultValue = false, Name = "categories")]
    public List<string> Categories;
    [DataMember(EmitDefaultValue = false, Name = "crosshair")]
    public bool Crosshair;
    [DataMember(EmitDefaultValue = false, Name = "title")]
    public HighChartData.HighChartTitle ChartTitle;

    public HighChartXAxis()
    {
      this.Crosshair = true;
      this.Categories = new List<string>();
      this.ChartTitle = new HighChartData.HighChartTitle();
    }
  }

  [DataContract]
  public class HighChartYAxis
  {
    [DataMember(EmitDefaultValue = false, Name = "min")]
    public int Minimum;
    [DataMember(EmitDefaultValue = false, Name = "title")]
    public HighChartData.HighChartTitle ChartTitle;

    public HighChartYAxis() => this.ChartTitle = new HighChartData.HighChartTitle();
  }

  [DataContract]
  public class HighChartTooltip
  {
    [DataMember(EmitDefaultValue = false, Name = "shared")]
    public bool Shared;
    [DataMember(EmitDefaultValue = false, Name = "useHTML")]
    public bool UseHTML;
    [DataMember(EmitDefaultValue = false, Name = "headerFormat")]
    public string HeaderFormat;
    [DataMember(EmitDefaultValue = false, Name = "pointFormat")]
    public string PointFormat;
    [DataMember(EmitDefaultValue = false, Name = "footerFormat")]
    public string FooterFormat;
  }

  [DataContract]
  public class HighChartSeries
  {
    [DataMember(EmitDefaultValue = false, Name = "name")]
    public string DataSeriesName;
    [DataMember(EmitDefaultValue = false, Name = "colorByPoint")]
    public bool ColorByPoint;
    [DataMember(EmitDefaultValue = false, Name = "data1")]
    public List<double> DataPointValueList;
    [DataMember(EmitDefaultValue = false, Name = "data")]
    public List<HighChartData.DataPointItem> DataPointYValueList;

    public void AddDataPoint(HighChartData.DataPointItem dpi)
    {
      if (this.DataPointYValueList == null)
        this.DataPointYValueList = new List<HighChartData.DataPointItem>();
      this.DataPointYValueList.Add(dpi);
    }

    public void AddDataPoint(double dpi)
    {
      if (this.DataPointValueList == null)
        this.DataPointValueList = new List<double>();
      this.DataPointValueList.Add(dpi);
    }
  }

  [DataContract]
  public class DataPointItem
  {
    [DataMember(EmitDefaultValue = false, Name = "name")]
    public string PointName;
    [DataMember(EmitDefaultValue = false, Name = "y")]
    public double? DataYPoint;
    [DataMember(EmitDefaultValue = false, Name = "sliced")]
    public bool Sliced;
    [DataMember(EmitDefaultValue = false, Name = "selected")]
    public bool Selected;
  }

  [DataContract]
  public class HighChartPlotOptions
  {
    [DataMember(EmitDefaultValue = false, Name = "bar")]
    public HighChartData.HighChartPlotOptionChartType BarChart;
    [DataMember(EmitDefaultValue = false, Name = "column")]
    public HighChartData.HighChartPlotOptionChartType ColumnChart;
    [DataMember(EmitDefaultValue = false, Name = "pie")]
    public HighChartData.HighChartPlotOptionChartType PieChart;
  }

  [DataContract]
  public class HighChartPlotOptionChartType
  {
    [DataMember(EmitDefaultValue = false, Name = "dataLabels")]
    public HighChartData.HighChartPlotOptionDataLabel DataLabels;

    public HighChartPlotOptionChartType() => this.DataLabels = new HighChartData.HighChartPlotOptionDataLabel();
  }

  public class HighChartPlotOptionDataLabel
  {
    [DataMember(EmitDefaultValue = false, Name = "enabled")]
    public bool enabled;

    public HighChartPlotOptionDataLabel() => this.enabled = true;
  }
}
