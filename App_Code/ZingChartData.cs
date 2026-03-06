// Decompiled with JetBrains decompiler
// Type: ZingChartData
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using AIS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

[DataContract]
[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
public class ZingChartData
{
  [DataMember(EmitDefaultValue = false, Name = "type")]
  public string Type;
  [DataMember(EmitDefaultValue = false, Name = "x")]
  public string X;
  [DataMember(EmitDefaultValue = false, Name = "y")]
  public string Y;
  [DataMember(EmitDefaultValue = false, Name = "backgroundColor")]
  public string BackgroundColor;
  [DataMember(EmitDefaultValue = false, Name = "plot")]
  public ZingChartPlot Plot;
  [DataMember(EmitDefaultValue = false, Name = "series")]
  public List<ZingChartSeriesData> Series;
  [DataMember(EmitDefaultValue = false, Name = "scale-x")]
  public ZingChartScaleX ScaleX;
  [DataMember(EmitDefaultValue = false, Name = "scale-y")]
  public ZingChartScaleY ScaleY;
  [DataMember(EmitDefaultValue = false, Name = "errormessage")]
  public string ErrorMessage;
  [DataMember(EmitDefaultValue = false, Name = "plotarea")]
  public ZingChartPlotArea PlotArea;

  public ZingChartData()
  {
    this.Plot = new ZingChartPlot();
    this.Series = new List<ZingChartSeriesData>();
    this.ScaleX = new ZingChartScaleX();
    this.ScaleY = new ZingChartScaleY();
    this.PlotArea = new ZingChartPlotArea();
  }

  public ZingChartData Get()
  {
    List<double> doubleList = new List<double>()
    {
      35.0,
      42.0,
      67.0,
      89.0,
      25.0,
      34.0,
      67.0,
      85.0
    };
    ZingChartData zingChartData = new ZingChartData();
    zingChartData.ErrorMessage = "";
    try
    {
      zingChartData.Type = "bar";
      zingChartData.BackgroundColor = "#333";
      zingChartData.Plot.BorderRadius = "5px";
      zingChartData.ScaleX.LineColor = "white";
      zingChartData.ScaleX.Label.Text = "Graph Color";
      zingChartData.ScaleX.Label.FontSize = "18px";
      zingChartData.ScaleX.Label.Color = "white";
      zingChartData.ScaleX.Item.Color = "#fff";
      zingChartData.ScaleY.LineColor = "white";
      zingChartData.ScaleY.Label.Text = "Graph Color";
      zingChartData.ScaleY.Label.FontSize = "18px";
      zingChartData.ScaleY.Label.Color = "white";
      zingChartData.ScaleY.Item.Color = "#fff";
      zingChartData.Series.Add(new ZingChartSeriesData()
      {
        Values = doubleList,
        Text = new List<string>() { "Test 1" }
      });
    }
    catch (Exception ex)
    {
      zingChartData = new ZingChartData();
      zingChartData.ErrorMessage = ex.ToString();
    }
    return zingChartData;
  }

  public ZingChartData Get(
    string query_name,
    string pname,
    string pvalue,
    string theme,
    string chart_type)
  {
    List<double> doubleList = new List<double>()
    {
      35.0,
      42.0,
      67.0,
      89.0,
      25.0,
      34.0,
      67.0,
      85.0
    };
    ZingChartData zingChartData = new ZingChartData();
    zingChartData.ErrorMessage = "";
    try
    {
      zingChartData.Type = chart_type.ToLower();
      zingChartData.BackgroundColor = "#333";
      zingChartData.Plot.BorderRadius = "5px";
      if (chart_type.ToLower() == "doughnut")
      {
        zingChartData.Type = "pie";
        zingChartData.Plot.Slice = "35%";
      }
      zingChartData.ScaleX.LineColor = "white";
      zingChartData.ScaleX.Label.FontSize = "12px";
      zingChartData.ScaleX.Label.Color = "white";
      zingChartData.ScaleX.Item.Color = "#fff";
      zingChartData.ScaleY.LineColor = "white";
      zingChartData.ScaleY.Label.FontSize = "12px";
      zingChartData.ScaleY.Label.Color = "white";
      zingChartData.ScaleY.Item.Color = "#fff";
      ZingChartSeriesData zingChartSeriesData = new ZingChartSeriesData();
      List<ZingChartSeriesData> queryData = this.GetQueryData(query_name, pname, pvalue);
      zingChartData.ScaleX.Labels = queryData.SelectMany<ZingChartSeriesData, string>((Func<ZingChartSeriesData, IEnumerable<string>>) (sd => (IEnumerable<string>) sd.Text)).ToList<string>();
      zingChartData.Series.AddRange((IEnumerable<ZingChartSeriesData>) queryData);
    }
    catch (Exception ex)
    {
      zingChartData = new ZingChartData();
      zingChartData.ErrorMessage = ex.ToString();
    }
    return zingChartData;
  }

  private List<ZingChartSeriesData> GetQueryData(
    string queryName,
    string paramNames,
    string paramValues)
  {
    StringBuilder stringBuilder = new StringBuilder();
    List<ZingChartSeriesData> queryData = new List<ZingChartSeriesData>();
    ZingChartSeriesData zingChartSeriesData = new ZingChartSeriesData();
    int index = ((IEnumerable<string>) paramNames.ToLower().Split(',')).ToList<string>().IndexOf("companyid");
    string companyId = ((IEnumerable<string>) paramValues.Split(',')).ToList<string>()[index];
    string companyCodeById = BLL.GetCompanyCodeById(companyId);
    string storedProcedureName = this.GetStoredProcedureName(queryName);
    stringBuilder.Append("select isnull((select OrganizationCode from Organization where OrganizationID ");
    stringBuilder.AppendFormat("in(select OrganizationID from company where CompanyID={0})),'N/A') as OrganizationCode ", (object) companyId);
    string str = Convert.ToString(DAL.ReturnScalarValue(stringBuilder.ToString(), true));
    SqlDataReader sqlDataReader = DAL.ReturnDataReader(string.Format("{0}_{1}", str == "N/A" ? (object) companyCodeById : (object) str, (object) storedProcedureName), paramNames, paramValues);
    while (sqlDataReader.Read())
    {
      double num = (double) Convert.ToInt32(sqlDataReader["Answer"]) / (double) Convert.ToInt32(sqlDataReader["QuestionCount"]);
      zingChartSeriesData.Text.Add(sqlDataReader["QuestionDescription"].ToString().Trim());
      zingChartSeriesData.Values.Add(Math.Round(num, 2));
    }
    queryData.Add(zingChartSeriesData);
    sqlDataReader.Close();
    sqlDataReader.Dispose();
    return queryData;
  }

  private string GetStoredProcedureName(string queryName)
  {
    if (queryName.ToLower() == "useagain")
      return "UseAgainByCompanyID";
    if (queryName.ToLower() == "useagainregion")
      return "useAgainAllRegionsByCompanyId";
    if (queryName.ToLower() == "questionbreakdown")
      return "calculateQuestionAVGByCompanyId";
    if (queryName.ToLower() == "questionbreakdownbyregion")
      return "calculateQuestionRegionAVGByCompanyId";
    if (queryName.ToLower() == "questionbreakdownhaulingcodebyregiondashboard")
      return "calculateQuestionHaulingRegionAVGByCompanyId_Dashboard";
    return queryName.ToLower() == "questionbreakdownregionbycompany" ? "calculateQuestionBreakdownRegionAVGByCompanyId" : queryName;
  }
}
