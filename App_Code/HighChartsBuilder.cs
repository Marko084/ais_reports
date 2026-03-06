// Decompiled with JetBrains decompiler
// Type: HighChartsBuilder
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using AIS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public class HighChartsBuilder
{
  private string companyCode;
  private string companyID;

  public string QueryName { get; set; }

  public string ParamNames { get; set; }

  public string ParamValues { get; set; }

  public string ChartTheme { get; set; }

  public string ChartType { get; set; }

  public HighChartData Create()
  {
    HighChartData highChartData = new HighChartData();
    this.companyID = this.GetParameterValue("companyid");
    this.companyCode = BLL.GetCompanyCodeById(this.companyID);
    if (this.QueryName.ToUpper() == "QUESTIONBREAKDOWN")
      highChartData = this.GetQuestionBreakdownChartData();
    return highChartData;
  }

  private HighChartData GetQuestionBreakdownChartData()
  {
    HighChartData breakdownChartData = new HighChartData();
    SqlDataReader sqlDataReader = DAL.ReturnDataReader(string.Format("{0}_calculateQuestionAVGByCompanyId", (object) this.companyCode), this.ParamNames, this.ParamValues);
    bool flag = sqlDataReader.GetSchemaTable().AsEnumerable().Any<DataRow>((Func<DataRow, bool>) (c => c["ColumnName"] == (object) "Score" || c["ColumnName"] == (object) "AvgScore"));
    breakdownChartData.ChartTitle.TitleText = "";
    breakdownChartData.ChartXAxis.Categories.Add("Question Breakdown");
    breakdownChartData.ChartYAxis.ChartTitle.TitleText = "Score";
    while (sqlDataReader.Read())
    {
      double dpi;
      if (flag)
      {
        dpi = Convert.ToDouble(((IEnumerable<string>) sqlDataReader["Score"].ToString().Split('/')).First<string>());
      }
      else
      {
        Convert.ToInt32(sqlDataReader["QuestionId"]);
        dpi = Math.Round(Convert.ToDouble(sqlDataReader["Answer"] == DBNull.Value ? (object) 0 : sqlDataReader["Answer"]) / (Convert.ToDouble(sqlDataReader["QuestionCount"]) == 0.0 ? 1.0 : Convert.ToDouble(sqlDataReader["QuestionCount"])), 2);
      }
      HighChartData.HighChartPlotOptions chartPlotOptions = new HighChartData.HighChartPlotOptions();
      HighChartData.HighChartSeries highChartSeries = new HighChartData.HighChartSeries();
      highChartSeries.DataSeriesName = sqlDataReader["QuestionDescription"].ToString();
      breakdownChartData.ChartType.Type = this.ChartType.ToLower();
      breakdownChartData.ChartPlotOptions = chartPlotOptions;
      if (this.ChartType.ToLower() == "bar" || this.ChartType.ToLower() == "column")
      {
        chartPlotOptions.BarChart = new HighChartData.HighChartPlotOptionChartType();
        highChartSeries.AddDataPoint(dpi);
      }
      else if (this.ChartType.ToLower() == "pie")
      {
        chartPlotOptions.PieChart = new HighChartData.HighChartPlotOptionChartType();
        highChartSeries.AddDataPoint(new HighChartData.DataPointItem()
        {
          DataYPoint = new double?(dpi),
          PointName = sqlDataReader["QuestionDescription"].ToString()
        });
        highChartSeries.ColorByPoint = true;
      }
      breakdownChartData.ChartSeriesData.Add(highChartSeries);
    }
    return breakdownChartData;
  }

  public string GetParameterValue(string parameterName) => ((IEnumerable<string>) this.ParamValues.Split(',')).ToList<string>().ElementAt<string>(((IEnumerable<string>) this.ParamNames.ToLower().Split(',')).ToList<string>().IndexOf(parameterName.ToLower())).ToString();
}
