using AIS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

public class FusionChartsQuery
{
  private string _clientName = "";
  public int Palette = 3;
  public string BackgroundColor = "#000000";
  public string FontColor = "#ffffff";
  public List<string> chartColorsList = new List<string>()
  {
    "#9400D3",
    "#4B0082",
    "#0000FF",
    "#00FF00",
    "#FFFF00",
    "#FF7F00",
    "#FF0000",
    "#663366",
    "#669933",
    "#660000",
    "#ffff99",
    "#ff9900",
    "#000066",
    "#003300"
  };
  private SqlDataReader dr;
  private string _paramValues;
  private string _paramNames;
  private string _companyId;
  private string _govtid;
  private string _chartType;
  private string _queryName;
  private int _userId;
  private string CompanyName;
  private string _companyCode;
  private string _startDate;
  private string _endDate;
  private string _companyChildId;
  public int UsePlotGradientColor;
  public int ShowPlotBorder;
  public int ShowShadow;

  public int UserId
  {
    get => this._userId;
    set => this._userId = value;
  }

  public string ParamValues
  {
    get => this._paramValues;
    set => this._paramValues = value;
  }

  public string ParamNames
  {
    get => this._paramNames;
    set => this._paramNames = value;
  }

  public string CompanyId
  {
    get => this._companyId;
    set => this._companyId = value;
  }

  public string CompanyChildId
  {
    get => this._companyChildId;
    set => this._companyChildId = value;
  }

  public string CompanyCode
  {
    get => this._companyCode;
    set => this._companyCode = value;
  }

  public string ChartType
  {
    get => this._chartType;
    set => this._chartType = value;
  }

  public string QueryName
  {
    get => this._queryName;
    set => this._queryName = value;
  }

  public string StartDate
  {
    get => this._startDate;
    set => this._startDate = value;
  }

  public string ClientName
  {
    get => this._clientName;
    set => this._clientName = value;
  }

  public string GovtID
  {
    get => this._govtid;
    set => this._govtid = value;
  }

  public string EndDate
  {
    get => this._endDate;
    set => this._endDate = value;
  }

  public string CustomQueryName { get; set; }

  public string ChartTitle { get; set; }

  public string ThemeName { get; set; }

  public string SCAC { get; set; }

  public string View3D { get; set; }

  public string NumberPrefix { get; set; }

  public string GetChartXml()
  {
    this._companyId = this.GetParameterValue("companyId");
    this._startDate = this.GetParameterValue("DeliveryStartDate");
    this._endDate = this.GetParameterValue("DeliveryEndDate");
    this._clientName = this.GetParameterValue("ClientName");
    string parameterValue = this.GetParameterValue("CCode");
    if (parameterValue != "")
      this._companyCode = parameterValue;
    if (this.View3D == null)
      this.View3D = "True";
    this.CompanyName = string.Format("{0}", DAL.ReturnScalarValue("getCompanyName", "companyId", this._companyId));
    if (this.CompanyChildId != "" && this.CompanyChildId != null)
      this.CompanyId = this.CompanyChildId;
    if (this.QueryName.ToUpper() == "TOPDRIVER")
      return this.NW_TopDriverScore();
    if (this.QueryName.ToUpper() == "USEAGAIN")
      return this.UseAgainByCompany();
    if (this.QueryName.ToUpper() == "PERCENTBARCHART")
      return this.PercentBarChart();
    if (this.QueryName.ToUpper() == "TRENDCHART")
      return this.TrendChart();
    if (this.QueryName.ToUpper() == "TRENDAVG")
      return this.CalculateTrendAvg();
    if (this.QueryName.ToUpper() == "COSTPERCLAIM")
      return this.CostPerClaim();
    if (this.QueryName.ToUpper() == "TOTALCOSTCLAIM")
      return this.TotalCostClaim();
    if (this.QueryName.ToUpper() == "STANDARDBARCHART")
      return this.StandardBarChart();
    if (this.QueryName.ToUpper() == "NW_MILITARY_DRIVERSTACKEDCHART")
      return this.NW_Military_DriverStackedChart();
    if (this.QueryName.ToUpper() == "MESA_DRIVERSTACKEDCHART")
      return this.Mesa_DriverStackedChart();
    if (this.QueryName.ToUpper() == "USEAGAINREGION")
    {
      if (this.CompanyCode.StartsWith("Daniels"))
        return this.Daniels_UseAgainRegionByCompany();
      if (this.CompanyCode.StartsWith("DMS"))
        return this.DMS_UseAgainRegionByCompany();
      return this.CompanyCode.StartsWith("NW") ? this.NW_UseAgainRegionByCompany() : this.UseAgainRegionByCompany();
    }
    if (this.QueryName.ToUpper() == "QUESTIONBREAKDOWN" || this.QueryName.ToUpper() == "CALCULATEQUESTIONAVG")
      return this.QuestionBreakdown(false);
    if (this.QueryName.ToUpper() == "QUESTIONBREAKDOWNBYREGION")
    {
      if (this.CompanyCode.StartsWith("Daniels"))
        return this.Daniels_QuestionBreakdown();
      if (this.CompanyCode.StartsWith("DMS"))
        return this.DMS_QuestionBreakdown();
      return this.CompanyCode.StartsWith("NW") ? this.NW_QuestionBreakdownByRegion() : this.QuestionBreakdownByRegion();
    }
    if (this.QueryName.ToUpper() == "QUESTIONBREAKDOWNBYREGIONDASHBOARD")
      return this.QuestionBreakdownByRegionDashboard();
    if (this.QueryName.ToUpper() == "QUESTIONBREAKDOWNHAULINGCODEBYREGIONDASHBOARD")
      return this.QuestionBreakdownHaulingCodeByRegionDashboard();
    if (this.QueryName.ToUpper() == "QUESTIONBREAKDOWNHAULINGCODEBYREGION")
      return this.QuestionBreakdownHaulingCodeByRegion();
    if (this.QueryName.ToUpper() == "QUESTIONBREAKDOWNREGIONBYCOMPANY")
      return this.QuestionBreakdownRegionByCompany();
    if (this.QueryName.ToUpper() == "QUESTIONBREAKDOWNHAULINGCODEREGIONBYCOMPANY")
      return this.QuestionBreakdownHaulingCodeRegionByCompany();
    if (this.QueryName.ToUpper() == "QUESTIONBREAKDOWNREGIONBYCOMPANYDASHBOARD")
      return this.QuestionBreakdownRegionByCompanyDashboard();
    if (this.QueryName.ToUpper() == "QUESTIONBREAKDOWNHAULINGCODEREGIONBYCOMPANYDASHBOARD")
      return this.QuestionBreakdownHaulingCodeRegionByCompanyDashboard();
    if (this.QueryName.ToUpper() == "KPITOTALS")
      return this.KPICodesGraph();
    if (this.QueryName.ToUpper() == "AVGSCOREBYMONTH")
      return this.NW_Military_calculateQuestionAVGByMonth();
    if (this.QueryName.ToUpper() == "AVGSCOREBYBOOKERNO" || this.QueryName.ToUpper() == "AVGSCOREBYBOOKER" || this.QueryName.ToUpper() == "AVGSCOREBYHAULER")
      return this.CompanyCode.ToLower().StartsWith("nw") ? this.NW_Military_calculateQuestionAVGByBookerNo() : this.calculateQuestionAVGByBookerNo();
    if (this.QueryName.ToUpper() == "AVGSCOREBYSCAC")
      return this.GetAVGBySCACCode();
    if (this.QueryName.ToUpper() == "SINGLESERIES")
      return this.GetSingleSeriesChart();
    if (this.QueryName.ToUpper() == "SINGLESERIESCHART")
      return this.GetSingleSeriesChart(true);
    return this.QueryName.ToUpper() == "AVGDAYSTOSETTLE" || this.QueryName.ToUpper() == "AVGAMOUNTPERCLAIM" ? this.GetSingleSeriesChart() : this.GetStandardChartXml();
  }

  private string KPICodesGraph()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine(string.Format("<chart showborder='0' use3dlighting='0' enablesmartlabels='0' startingangle='210' showlabels='0' showpercentvalues='1' showlegend='1' showtooltip='1' decimals='0' usedataplotcolorforlabels='1' theme='{0}'>", (object) this.ThemeName));
    if (this.CustomQueryName != null && this.CustomQueryName != "")
      this.dr = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
    else if (this.CompanyCode == "NW_" && this.CompanyChildId != "1" && this.CompanyChildId != null && this.CompanyChildId != "")
      this.dr = DAL.ReturnDataReader("graph_KPIResults", "companyID,companyChildId,StartDate,EndDate,ClientName,GovtID", this.CompanyId + "," + this.CompanyChildId + "," + this.StartDate + "," + this.EndDate + "," + this.ClientName + "," + this.GovtID);
    else
      this.dr = DAL.ReturnDataReader("graph_KPIResults", this.ParamNames, this.ParamValues);
    while (this.dr.Read())
      stringBuilder.AppendFormat("<set label='{0}' value='{1}'/>", (object) this.dr["KPIDescription"].ToString(), this.dr["TotalCount"]);
    this.dr.Close();
    this.dr.Dispose();
    stringBuilder.AppendLine("</chart>");
    return stringBuilder.ToString();
  }

  private string UseAgainByCompany()
  {
    StringBuilder stringBuilder = new StringBuilder();
    this.dr = this.CustomQueryName == null || !(this.CustomQueryName != "") ? (!(this.CompanyCode == "NW_") || !(this.CompanyChildId != "1") || this.CompanyChildId == null || !(this.CompanyChildId != "") ? DAL.ReturnDataReader(string.Format("{0}useAgainByCompanyId", (object) this.CompanyCode), this.ParamNames, this.ParamValues) : DAL.ReturnDataReader(string.Format("{0}_useAgainByCompanyId", (object) "NW_Group"), this.ParamNames, this.ParamValues)) : DAL.ReturnDataReader(this.CustomQueryName, CommandType.Text);
    double num1 = 0.0;
    double num2 = 0.0;
    while (this.dr.Read())
    {
      num1 = (double) Convert.ToInt32(this.dr["Answer"] == DBNull.Value ? (object) 0 : this.dr["Answer"]);
      num2 = Convert.ToDouble(this.dr["QuestionCount"]) == 0.0 ? 1.0 : Convert.ToDouble(this.dr["QuestionCount"]);
    }
    this.dr.Close();
    this.dr.Dispose();
    double num3 = num1 / num2 * 100.0;
    double num4 = (num2 - num1) / num2 * 100.0;
    string str1 = Convert.ToInt32(this.CompanyId) > 10008 ? num3.ToString("000.00") : num3.ToString("000.00");
    if (str1.Length == 0)
      str1 = "0";
    string str2 = Convert.ToInt32(this.CompanyId) > 10008 ? num4.ToString("000.00") : num4.ToString("000.00");
    if (str2.Length == 0)
      str2 = "0";
    this.CompanyName = BLL.GetCompanyNameById(this.CompanyId);
    stringBuilder.AppendLine(string.Format("<chart showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
    stringBuilder.AppendLine(string.Format("<set label='Yes' value='{0}'/>", (object) str1));
    stringBuilder.AppendLine(string.Format("<set label='No' value='{0}'/>", (object) str2));
    stringBuilder.AppendLine("</chart>");
    return stringBuilder.ToString();
  }

  private string GetStandardChartXml()
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    ArrayList arrayList = new ArrayList();
    string str1 = "";
    string str2 = "";
    string str3 = "";
    string str4 = "";
    List<string> stringList = new List<string>();
    SqlDataReader sqlDataReader = DAL.ReturnDataReader(this.QueryName, this.ParamNames, this.ParamValues);
    for (int ordinal = 0; ordinal < sqlDataReader.FieldCount; ++ordinal)
      stringList.Add(sqlDataReader.GetName(ordinal).ToLower());
    stringBuilder1.AppendLine(string.Format("<chart showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\" [YAXISNAME] [XAXISNAME] >", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
    stringBuilder1.AppendLine("<categories>{0}</categories>");
    while (sqlDataReader.Read())
    {
      if (stringList.Contains("yaxisname"))
        str3 = sqlDataReader["YAxisName"].ToString();
      if (stringList.Contains("xaxisname"))
        str4 = sqlDataReader["XAxisName"].ToString();
      if (stringList.Contains("category") && (str2 == "" || str2 != sqlDataReader["Category"].ToString()) && !stringBuilder2.ToString().ToLower().Contains(sqlDataReader["Category"].ToString().ToLower()))
      {
        stringBuilder2.AppendFormat("<category label=\"{0}\" />", sqlDataReader["Category"]);
        str2 = sqlDataReader["Category"].ToString();
      }
      if (stringList.Contains("dataseries"))
      {
        if (str1 == "")
        {
          str1 = sqlDataReader["DataSeries"].ToString();
          stringBuilder1.AppendFormat("<dataset seriesname='{0}'>", (object) str1);
        }
        else if (str1.ToLower() != sqlDataReader["DataSeries"].ToString().ToLower())
        {
          str1 = sqlDataReader["DataSeries"].ToString();
          stringBuilder1.Append("</dataset>");
          stringBuilder1.AppendFormat("<dataset seriesname='{0}'>", (object) str1);
        }
      }
      stringBuilder1.AppendFormat("<set label='{0}' value='{1}'/>", sqlDataReader["Label"], sqlDataReader["Value"]);
    }
    if (str1 != "")
      stringBuilder1.Append("</dataset>");
    stringBuilder1.Append("</chart>");
    string str5 = stringBuilder2.Length <= 0 ? stringBuilder1.ToString().Replace("<categories>{0}</categories>", "") : string.Format(stringBuilder1.ToString(), (object) stringBuilder2.ToString());
    sqlDataReader.Dispose();
    return str5.Replace("[YAXISNAME]", string.Format("yAxisName='{0}'", (object) str3)).Replace("[XAXISNAME]", string.Format("xAxisName='{0}'", (object) str4));
  }

  private string UseAgainRegionByCompany()
  {
    StringBuilder stringBuilder = new StringBuilder();
    ArrayList arrayList = new ArrayList();
    SqlDataReader sqlDataReader;
    if (this.CustomQueryName != null && this.CustomQueryName != "")
      sqlDataReader = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
    else
      sqlDataReader = DAL.ReturnDataReader(string.Format("{0}useAgainAllRegionsByCompanyId", (object) this.CompanyCode), this.ParamNames, this.ParamValues);
    this.CompanyName = BLL.GetCompanyNameById(this.CompanyId);
    stringBuilder.AppendLine(string.Format("<chart showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
    stringBuilder.AppendFormat("<dataset seriesname='{0}'>", (object) "Yes");
    while (sqlDataReader.Read())
    {
      double int32 = (double) Convert.ToInt32(sqlDataReader["Answer"] == DBNull.Value ? (object) 0 : sqlDataReader["Answer"]);
      double num1 = Convert.ToDouble(sqlDataReader["QuestionCount"]) == 0.0 ? 1.0 : Convert.ToDouble(sqlDataReader["QuestionCount"]);
      double num2 = int32 / num1 * 100.0;
      double num3 = (num1 - int32) / num1 * 100.0;
      string str1 = Convert.ToInt32(this.CompanyId) > 10008 ? num2.ToString("000.00") : num2.ToString("000.00");
      if (str1.Length == 0)
        str1 = "0";
      string str2 = Convert.ToInt32(this.CompanyId) > 10008 ? num3.ToString("000.00") : num3.ToString("000.00");
      if (str2.Length == 0)
        str2 = "0";
      try
      {
        arrayList.Add((object) string.Format("{0}${1}", sqlDataReader["LocationDescription"], (object) str2));
        stringBuilder.AppendFormat("<set label='{0}' value='{1}'/>", sqlDataReader["LocationDescription"], (object) str1);
      }
      catch
      {
      }
    }
    sqlDataReader.Close();
    sqlDataReader.Dispose();
    stringBuilder.AppendLine("</dataset>");
    stringBuilder.AppendFormat("<dataset seriesname='{0}'>", (object) "No");
    foreach (object obj in arrayList)
    {
      string str3 = ((IEnumerable<string>) obj.ToString().Split('$')).First<string>();
      string str4 = ((IEnumerable<string>) obj.ToString().Split('$')).Last<string>();
      stringBuilder.AppendFormat("<set label='{0}' value='{1}'/>", (object) str3, (object) str4);
    }
    stringBuilder.AppendLine("</dataset>");
    stringBuilder.AppendLine("<categories>");
    foreach (object obj in arrayList)
    {
      string str = ((IEnumerable<string>) obj.ToString().Split('$')).First<string>();
      stringBuilder.AppendLine(string.Format("<category label='{0}'/>", (object) str));
    }
    stringBuilder.AppendLine("</categories>");
    stringBuilder.AppendLine("</chart>");
    return stringBuilder.ToString();
  }

  private string QuestionBreakdownRegionByCompany()
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    StringBuilder stringBuilder3 = new StringBuilder();
    ArrayList arrayList = new ArrayList();
    int num1 = 0;
    int num2 = 0;
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    SqlDataReader sqlDataReader;
    if (this.CustomQueryName != null && this.CustomQueryName != "")
      sqlDataReader = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
    else
      sqlDataReader = DAL.ReturnDataReader(string.Format("{0}calculateQuestionBreakdownRegionAVGByCompanyId", (object) this.CompanyCode), this.ParamNames, this.ParamValues);
    this.CompanyName = BLL.GetCompanyNameById(this.CompanyId);
    stringBuilder1.AppendLine(string.Format("<chart xaxisname=\"Locations\" yaxisname=\"Score\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\" placeValuesInside='1' rotateValues='1'>", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
    stringBuilder2.AppendLine("<categories>");
    bool hasRows = sqlDataReader.HasRows;
    while (sqlDataReader.Read())
    {
      int int32_1 = Convert.ToInt32(sqlDataReader["QuestionID"]);
      double int32_2 = (double) Convert.ToInt32(sqlDataReader["Answer"] == DBNull.Value ? (object) 0 : sqlDataReader["Answer"]);
      double num3 = Convert.ToDouble(sqlDataReader["QuestionCount"]) == 0.0 ? 1.0 : Convert.ToDouble(sqlDataReader["QuestionCount"]);
      string str1 = sqlDataReader["QuestionDescription"].ToString();
      string str2 = sqlDataReader["LocationDescription"].ToString();
      double num4 = int32_2 / num3;
      if (!stringBuilder2.ToString().Contains(str2))
        stringBuilder2.AppendLine(string.Format("<category label='{0}' />", (object) str2));
      try
      {
        if (int32_1 != num1)
        {
          if (num1 == 0)
          {
            stringBuilder1.AppendFormat("<dataset seriesname='{0}'>", (object) str1);
          }
          else
          {
            stringBuilder1.AppendLine("</dataset>");
            stringBuilder1.AppendFormat("<dataset seriesname='{0}'>", (object) str1);
          }
          num1 = int32_1;
          ++num2;
        }
        stringBuilder1.AppendFormat("<set label='{0}' value='{0}'/>", (object) num4.ToString("###.00"));
      }
      catch
      {
      }
    }
    sqlDataReader.Close();
    sqlDataReader.Dispose();
    if (hasRows)
      stringBuilder1.AppendLine("</dataset>");
    stringBuilder2.AppendLine("</categories>");
    stringBuilder1.AppendLine(stringBuilder2.ToString());
    stringBuilder1.AppendLine("</chart>");
    return stringBuilder1.ToString();
  }

  private string QuestionBreakdownHaulingCodeRegionByCompany()
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    ArrayList arrayList = new ArrayList();
    int num1 = 0;
    int num2 = 0;
    SqlDataReader sqlDataReader;
    if (this.CustomQueryName != null && this.CustomQueryName != "")
      sqlDataReader = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
    else
      sqlDataReader = DAL.ReturnDataReader(string.Format("{0}calculateQuestionBreakdownHaulingCodeRegionAVGByCompanyId", (object) this.CompanyCode), this.ParamNames, this.ParamValues);
    this.CompanyName = BLL.GetCompanyNameById(this.CompanyId);
    stringBuilder1.AppendLine(string.Format("<chart xaxisname=\"Locations\" yaxisname=\"Score\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\" placeValuesInside='1' rotateValues='1'>", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
    bool hasRows = sqlDataReader.HasRows;
    while (sqlDataReader.Read())
    {
      int int32_1 = Convert.ToInt32(sqlDataReader["QuestionID"]);
      double int32_2 = (double) Convert.ToInt32(sqlDataReader["Answer"] == DBNull.Value ? (object) 0 : sqlDataReader["Answer"]);
      double num3 = Convert.ToDouble(sqlDataReader["QuestionCount"]) == 0.0 ? 1.0 : Convert.ToDouble(sqlDataReader["QuestionCount"]);
      string str = sqlDataReader["QuestionDescription"].ToString();
      double num4 = int32_2 / num3;
      try
      {
        if (int32_1 != num1)
        {
          if (num1 == 0)
          {
            stringBuilder1.AppendFormat("<dataset seriesname='{0}'>", (object) str);
            stringBuilder2.AppendLine(string.Format("<category label='{0}'/>", (object) str));
          }
          else
          {
            stringBuilder1.AppendLine("</dataset>");
            stringBuilder1.AppendFormat("<dataset seriesname='{0}'>", (object) str);
            stringBuilder2.AppendLine(string.Format("<category label='{0}'/>", (object) str));
          }
          num1 = int32_1;
          ++num2;
        }
        stringBuilder1.AppendFormat("<set label='{0}' value='{1}'/>", sqlDataReader["LocationDescription"], (object) num4.ToString("###.00"));
      }
      catch
      {
      }
    }
    sqlDataReader.Close();
    sqlDataReader.Dispose();
    if (hasRows)
      stringBuilder1.AppendLine("</dataset>");
    if (stringBuilder1.Length > 0)
    {
      stringBuilder1.AppendLine("<categories>");
      stringBuilder1.AppendLine(stringBuilder2.ToString());
      stringBuilder1.AppendLine("</categories>");
    }
    stringBuilder1.AppendLine("</chart>");
    return stringBuilder1.ToString();
  }

  private string QuestionBreakdownRegionByCompanyDashboard()
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    ArrayList arrayList = new ArrayList();
    int num1 = 0;
    int num2 = 0;
    SqlDataReader sqlDataReader;
    if (this.CustomQueryName != null && this.CustomQueryName != "")
      sqlDataReader = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
    else
      sqlDataReader = DAL.ReturnDataReader(string.Format("{0}calculateQuestionBreakdownRegionAVGByCompanyId_Dashboard", (object) this.CompanyCode), this.ParamNames, this.ParamValues);
    this.CompanyName = BLL.GetCompanyNameById(this.CompanyId);
    stringBuilder1.AppendLine(string.Format("<chart xaxisname=\"Locations\" yaxisname=\"Score\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\" placeValuesInside='1' rotateValues='1'>", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
    bool hasRows = sqlDataReader.HasRows;
    while (sqlDataReader.Read())
    {
      int int32_1 = Convert.ToInt32(sqlDataReader["QuestionID"]);
      double int32_2 = (double) Convert.ToInt32(sqlDataReader["Answer"] == DBNull.Value ? (object) 0 : sqlDataReader["Answer"]);
      double num3 = Convert.ToDouble(sqlDataReader["QuestionCount"]) == 0.0 ? 1.0 : Convert.ToDouble(sqlDataReader["QuestionCount"]);
      string str = sqlDataReader["QuestionDescription"].ToString();
      double num4 = int32_2 / num3;
      try
      {
        if (int32_1 != num1)
        {
          if (num1 == 0)
          {
            stringBuilder1.AppendFormat("<dataset seriesname='{0}'>", (object) str);
            stringBuilder2.AppendLine(string.Format("<category label='{0}'/>", (object) str));
          }
          else
          {
            stringBuilder1.AppendLine("</dataset>");
            stringBuilder1.AppendFormat("<dataset seriesname='{0}'>", (object) str);
            stringBuilder2.AppendLine(string.Format("<category label='{0}'/>", (object) str));
          }
          num1 = int32_1;
          ++num2;
        }
        stringBuilder1.AppendFormat("<set label='{0}' value='{1}'/>", sqlDataReader["LocationDescription"], (object) num4.ToString("###.00"));
      }
      catch
      {
      }
    }
    sqlDataReader.Close();
    sqlDataReader.Dispose();
    if (hasRows)
      stringBuilder1.AppendLine("</dataset>");
    if (stringBuilder1.Length > 0)
    {
      stringBuilder1.AppendLine("<categories>");
      stringBuilder1.AppendLine(stringBuilder2.ToString());
      stringBuilder1.AppendLine("</categories>");
    }
    stringBuilder1.AppendLine("</chart>");
    return stringBuilder1.ToString();
  }

  private string QuestionBreakdownHaulingCodeRegionByCompanyDashboard()
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    ArrayList arrayList = new ArrayList();
    int num1 = 0;
    int num2 = 0;
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    SqlDataReader sqlDataReader;
    if (this.CustomQueryName != null && this.CustomQueryName != "")
      sqlDataReader = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
    else
      sqlDataReader = DAL.ReturnDataReader(string.Format("{0}calculateQuestionBreakdownHaulingCodeRegionAVGByCompanyIdDashboard", (object) this.CompanyCode), this.ParamNames, this.ParamValues);
    this.CompanyName = BLL.GetCompanyNameById(this.CompanyId);
    stringBuilder1.AppendLine(string.Format("<chart xaxisname=\"Locations\" yaxisname=\"Score\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
    stringBuilder2.AppendLine("<categories>");
    bool hasRows = sqlDataReader.HasRows;
    while (sqlDataReader.Read())
    {
      int int32_1 = Convert.ToInt32(sqlDataReader["QuestionID"]);
      double int32_2 = (double) Convert.ToInt32(sqlDataReader["Answer"] == DBNull.Value ? (object) 0 : sqlDataReader["Answer"]);
      double num3 = Convert.ToDouble(sqlDataReader["QuestionCount"]) == 0.0 ? 1.0 : Convert.ToDouble(sqlDataReader["QuestionCount"]);
      string str1 = sqlDataReader["QuestionDescription"].ToString();
      string str2 = sqlDataReader["LocationDescription"].ToString();
      double num4 = int32_2 / num3;
      if (!stringBuilder2.ToString().Contains(str2))
        stringBuilder2.AppendLine(string.Format("<category label='{0}' />", (object) str2));
      try
      {
        if (int32_1 != num1)
        {
          if (num1 == 0)
          {
            stringBuilder1.AppendFormat("<dataset seriesname='{0}'>", (object) str1);
          }
          else
          {
            stringBuilder1.AppendLine("</dataset>");
            stringBuilder1.AppendFormat("<dataset seriesname='{0}'>", (object) str1);
          }
          num1 = int32_1;
          ++num2;
        }
        stringBuilder1.AppendFormat("<set label='{0}' value='{0}'/>", (object) num4.ToString("###.00"));
      }
      catch
      {
      }
    }
    sqlDataReader.Close();
    sqlDataReader.Dispose();
    if (hasRows)
      stringBuilder1.AppendLine("</dataset>");
    stringBuilder2.AppendLine("</categories>");
    stringBuilder1.AppendLine(stringBuilder2.ToString());
    stringBuilder1.AppendLine("</chart>");
    return stringBuilder1.ToString();
  }

  private string NW_Military_calculateQuestionAVGByMonth()
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    Hashtable hashtable = new Hashtable();
    ArrayList arrayList = new ArrayList();
    SqlDataReader sqlDataReader;
    if (this.CustomQueryName != null && this.CustomQueryName != "")
      sqlDataReader = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
    else
      sqlDataReader = DAL.ReturnDataReader(string.Format("{0}calculateQuestionAVGByMonth", (object) this.CompanyCode), this.ParamNames, this.ParamValues);
    stringBuilder1.AppendLine(string.Format("<chart showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
    stringBuilder1.AppendLine("<dataset seriesame='Avg. Score'>");
    stringBuilder2.AppendLine("<categories>");
    while (sqlDataReader.Read())
    {
      string str = sqlDataReader["MonthYear"].ToString();
      double num = Math.Round(Convert.ToDouble(sqlDataReader["Answer"] == DBNull.Value ? (object) 0 : sqlDataReader["Answer"]), 1);
      if (!stringBuilder2.ToString().Contains(str))
        stringBuilder2.AppendLine(string.Format("<category label='{0}' />", (object) str));
      if (Convert.ToInt32(this.CompanyId) > 10008 || Convert.ToInt32(this.CompanyId) == 10003)
        num = Math.Round(Convert.ToDouble(sqlDataReader["Answer"] == DBNull.Value ? (object) 0 : sqlDataReader["Answer"]), 2);
      arrayList.Add((object) Convert.ToInt32(sqlDataReader["SurveyCount"]));
      stringBuilder1.AppendFormat("<set label='{0}' value='{1}'/>", (object) str, (object) num);
    }
    stringBuilder2.AppendLine("</categories>");
    stringBuilder1.AppendLine("</dataset>");
    stringBuilder1.AppendLine("<dataset seriesname='Survey Count' renderas='line'>");
    for (int index = 0; index < arrayList.Count; ++index)
      stringBuilder1.AppendFormat("<set value='{0}'/>", arrayList[index]);
    stringBuilder1.AppendLine("</dataset>");
    stringBuilder1.AppendLine(stringBuilder2.ToString());
    stringBuilder1.AppendLine("</chart>");
    sqlDataReader.Close();
    sqlDataReader.Dispose();
    return stringBuilder1.ToString();
  }

  private string NW_Military_calculateQuestionAVGByBookerNo()
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    Hashtable hashtable = new Hashtable();
    ArrayList arrayList = new ArrayList();
    SqlDataReader sqlDataReader;
    if (this.CustomQueryName != null && this.CustomQueryName != "")
      sqlDataReader = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
    else
      sqlDataReader = DAL.ReturnDataReader(string.Format("{0}_calculateQuestionAVGByBooker", (object) this.CompanyCode.Trim('_')), this.ParamNames, this.ParamValues);
    stringBuilder1.AppendLine(string.Format("<chart showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
    stringBuilder1.AppendFormat("<dataset seriesname='Avg. Score' renderas='{0}'>", (object) this.ChartType);
    stringBuilder2.AppendLine("<categories>");
    while (sqlDataReader.Read())
    {
      string str = sqlDataReader["BookerNo"].ToString();
      double num = Math.Round(Convert.ToDouble(sqlDataReader["Answer"]), 1);
      if (!stringBuilder2.ToString().Contains(str))
        stringBuilder2.AppendLine(string.Format("<category label='{0}' />", (object) str));
      if (Convert.ToInt32(this.CompanyId) > 10008)
        num = Math.Round(Convert.ToDouble(sqlDataReader["Answer"]), 2);
      arrayList.Add((object) Convert.ToInt32(sqlDataReader["SurveyCount"]));
      stringBuilder1.AppendFormat("<set label='{0}' value='{1}'/>", (object) str, (object) num);
    }
    sqlDataReader.Close();
    sqlDataReader.Dispose();
    stringBuilder1.AppendLine("</dataset>");
    stringBuilder1.AppendLine("<datset seriesname='Survey Count' renderas='Line'>");
    for (int index = 0; index < arrayList.Count; ++index)
      stringBuilder1.AppendFormat("<set value='{0}'/>", arrayList[index]);
    stringBuilder1.AppendLine("</dataset>");
    stringBuilder1.AppendLine(stringBuilder2.ToString());
    stringBuilder1.AppendLine("</chart>");
    return stringBuilder1.ToString();
  }

  private string calculateQuestionAVGByBookerNo()
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    Hashtable hashtable = new Hashtable();
    ArrayList arrayList = new ArrayList();
    SqlDataReader sqlDataReader;
    if (this.CustomQueryName != null && this.CustomQueryName != "")
      sqlDataReader = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
    else
      sqlDataReader = DAL.ReturnDataReader(string.Format("{0}_calculateQuestionAVGByBooker", (object) this.CompanyCode.Trim('_')), this.ParamNames, this.ParamValues);
    stringBuilder1.AppendLine(string.Format("<chart showlegend=\"1\" legendNumColumns=\"0\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
    stringBuilder1.AppendLine("[CATEGORY-SECTION]");
    stringBuilder1.Append("<dataset seriesname='Avg. Score'>");
    stringBuilder2.AppendLine("<categories>");
    while (sqlDataReader.Read())
    {
      string str = sqlDataReader["BookerNo"].ToString();
      double num = Math.Round(Convert.ToDouble(sqlDataReader["Answer"] == DBNull.Value ? (object) 0 : sqlDataReader["Answer"]), 1);
      if (Convert.ToInt32(this.CompanyId) > 10008 || Convert.ToInt32(this.CompanyId) == 10003)
        num = Math.Round(Convert.ToDouble(sqlDataReader["Answer"] == DBNull.Value ? (object) 0 : sqlDataReader["Answer"]), 2);
      arrayList.Add((object) Convert.ToInt32(sqlDataReader["SurveyCount"]));
      stringBuilder2.AppendFormat("<category label='{0}' />", (object) str);
      stringBuilder1.AppendFormat("<set value='{0}'/>", (object) num);
    }
    stringBuilder2.AppendLine("</categories>");
    sqlDataReader.Close();
    sqlDataReader.Dispose();
    stringBuilder1.AppendLine("</dataset>");
    stringBuilder1.AppendLine("<dataset seriesname='Survey Count' RenderAs='Line'>");
    for (int index = 0; index < arrayList.Count; ++index)
      stringBuilder1.AppendFormat("<set value='{0}' />", arrayList[index]);
    stringBuilder1.AppendLine("</dataset>");
    stringBuilder1.AppendLine("</chart>");
    return stringBuilder1.ToString().Replace("[CATEGORY-SECTION]", stringBuilder2.ToString());
  }

  private string GetAVGBySCACCode()
  {
    StringBuilder stringBuilder = new StringBuilder();
    Hashtable hashtable = new Hashtable();
    ArrayList arrayList = new ArrayList();
    SqlDataReader sqlDataReader;
    if (this.CustomQueryName != null && this.CustomQueryName != "")
      sqlDataReader = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
    else
      sqlDataReader = DAL.ReturnDataReader(string.Format("{0}_calculateQuestionAVGBySCAC", (object) this.CompanyCode.Trim('_')), this.ParamNames, this.ParamValues);
    stringBuilder.AppendLine(string.Format("<chart showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
    stringBuilder.AppendFormat("<dataset seriesname='Avg. Score' renderas='{0}'>", (object) this.ChartType);
    while (sqlDataReader.Read())
    {
      string str = sqlDataReader["SCAC"].ToString();
      double num = Math.Round(Convert.ToDouble(sqlDataReader["Answer"] == DBNull.Value ? (object) 0 : sqlDataReader["Answer"]), 1);
      arrayList.Add((object) Convert.ToInt32(sqlDataReader["SurveyCount"]));
      stringBuilder.AppendFormat("<set label='{0}' value='{1}'/>", (object) str, (object) num);
    }
    sqlDataReader.Close();
    sqlDataReader.Dispose();
    stringBuilder.AppendLine("</dataset>");
    stringBuilder.AppendLine("<dataset seriesname='Survey Count' RenderAs='Line'>");
    for (int index = 0; index < arrayList.Count; ++index)
      stringBuilder.AppendFormat("<set value='{0}'/>", arrayList[index]);
    stringBuilder.AppendLine("</dataset>");
    stringBuilder.AppendLine("</chart>");
    return stringBuilder.ToString();
  }

  private string NW_UseAgainRegionByCompany()
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    ArrayList arrayList = new ArrayList();
    SqlDataReader sqlDataReader;
    if (this.CustomQueryName != null && this.CustomQueryName != "")
      sqlDataReader = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
    else
      sqlDataReader = !(this.CompanyCode == "NW_") || !(this.CompanyChildId != "1") || this.CompanyChildId == null || !(this.CompanyChildId != "") ? DAL.ReturnDataReader(string.Format("{0}useAgainAllRegionsByCompanyId", (object) this.CompanyCode), this.ParamNames, this.ParamValues) : DAL.ReturnDataReader(string.Format("{0}_useAgainAllRegionsByCompanyId", (object) "NW_Group"), this.ParamNames, this.ParamValues);
    if (Convert.ToInt32(this.CompanyId) > 10008)
      stringBuilder1.AppendLine(string.Format("<chart xaxisname=\"Locations\" yaxisname=\"Percentage\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
    else
      stringBuilder1.AppendLine(string.Format("<chart xaxisname=\"Regions\" yaxisname=\"Percentage\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
    stringBuilder1.AppendFormat("<dataset seriesname='Yes' renderas='{0}'>", (object) this.ChartType);
    stringBuilder2.AppendLine("<categories>");
    while (sqlDataReader.Read())
    {
      double int32 = (double) Convert.ToInt32(sqlDataReader["Answer"] == DBNull.Value ? (object) 0 : sqlDataReader["Answer"]);
      double num1 = Convert.ToDouble(sqlDataReader["QuestionCount"]) == 0.0 ? 1.0 : Convert.ToDouble(sqlDataReader["QuestionCount"]);
      double num2 = int32 / num1 * 100.0;
      double num3 = (num1 - int32) / num1 * 100.0;
      string str1 = Convert.ToInt32(this.CompanyId) > 10008 || Convert.ToInt32(this.CompanyId) == 1 ? num2.ToString("000.00") : num2.ToString("000.0");
      if (str1.Length == 0)
        str1 = "0";
      string str2 = Convert.ToInt32(this.CompanyId) > 10008 || Convert.ToInt32(this.CompanyId) == 1 ? num3.ToString("000.00") : num3.ToString("000.0");
      if (str2.Length == 0)
        str2 = "0";
      try
      {
        arrayList.Add((object) string.Format("{0}${1}", sqlDataReader["BookerNo"], (object) str2));
        stringBuilder1.AppendFormat("<set label='{0}' value='{1}'/>", sqlDataReader["BookerNo"], (object) str1);
      }
      catch
      {
      }
    }
    sqlDataReader.Close();
    sqlDataReader.Dispose();
    stringBuilder1.AppendLine("</dataset>");
    stringBuilder1.AppendFormat("<dataset seriesname='{0}' RenderAs='{1}'>", (object) "No", (object) this.ChartType);
    foreach (object obj in arrayList)
    {
      string str3 = ((IEnumerable<string>) obj.ToString().Split('$')).First<string>();
      string str4 = ((IEnumerable<string>) obj.ToString().Split('$')).Last<string>();
      stringBuilder1.AppendFormat("<set label='{0}' value='{1}'/>", (object) str3, (object) str4);
      stringBuilder2.AppendLine(string.Format("<category label='{0}' />", (object) str3));
    }
    stringBuilder2.AppendLine("</categories>");
    stringBuilder1.AppendLine("</dataset>");
    stringBuilder1.AppendLine(stringBuilder2.ToString());
    stringBuilder1.AppendLine("</chart>");
    return stringBuilder1.ToString();
  }

  private string DMS_UseAgainRegionByCompany()
  {
    StringBuilder stringBuilder = new StringBuilder();
    SqlDataReader sqlDataReader;
    if (this.CustomQueryName != null && this.CustomQueryName != "")
      sqlDataReader = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
    else
      sqlDataReader = DAL.ReturnDataReader(string.Format("{0}useAgainAllRegionsByCompanyId", (object) this.CompanyCode), this.ParamNames, this.ParamValues);
    double num1 = 0.0;
    double num2 = 0.0;
    while (sqlDataReader.Read())
    {
      num1 = (double) Convert.ToInt32(sqlDataReader["Answer"] == DBNull.Value ? (object) 0 : sqlDataReader["Answer"]);
      num2 = Convert.ToDouble(sqlDataReader["QuestionCount"]) == 0.0 ? 1.0 : Convert.ToDouble(sqlDataReader["QuestionCount"]);
    }
    sqlDataReader.Close();
    sqlDataReader.Dispose();
    double num3 = num1 / num2 * 100.0;
    double num4 = (num2 - num1) / num2 * 100.0;
    string str1 = num3.ToString("000.0");
    if (str1.Length == 0)
      str1 = "0";
    string str2 = num4.ToString("000.0");
    if (str2.Length == 0)
      str2 = "0";
    stringBuilder.AppendLine(string.Format("<chart yaxisname=\"Percentage\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
    stringBuilder.AppendFormat("<set label='{0}' value='{1}'/>", (object) "Yes", (object) str1);
    stringBuilder.AppendFormat("<set label='{0}' value='{1}'/>", (object) "No", (object) str2);
    stringBuilder.AppendLine("</chart>");
    return stringBuilder.ToString();
  }

  private string Daniels_UseAgainRegionByCompany()
  {
    StringBuilder stringBuilder = new StringBuilder();
    SqlDataReader sqlDataReader;
    if (this.CustomQueryName != null && this.CustomQueryName != "")
      sqlDataReader = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
    else
      sqlDataReader = DAL.ReturnDataReader(string.Format("{0}useAgainAllRegionsByCompanyId", (object) this.CompanyCode), this.ParamNames, this.ParamValues);
    double num1 = 0.0;
    double num2 = 0.0;
    while (sqlDataReader.Read())
    {
      num1 = (double) Convert.ToInt32(sqlDataReader["Answer"] == DBNull.Value ? (object) 0 : sqlDataReader["Answer"]);
      num2 = Convert.ToDouble(sqlDataReader["QuestionCount"]) == 0.0 ? 1.0 : Convert.ToDouble(sqlDataReader["QuestionCount"]);
    }
    sqlDataReader.Close();
    sqlDataReader.Dispose();
    double num3 = num1 / num2 * 100.0;
    double num4 = (num2 - num1) / num2 * 100.0;
    string str1 = num3.ToString("000.0");
    if (str1.Length == 0)
      str1 = "0";
    string str2 = num4.ToString("000.0");
    if (str2.Length == 0)
      str2 = "0";
    stringBuilder.AppendLine(string.Format("<chart yaxisname=\"Percentage\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
    stringBuilder.AppendFormat("<set label='{0}' value='{1}'/>", (object) "Yes", (object) str1);
    stringBuilder.AppendFormat("<set label='{0}' value='{1}'/>", (object) "No", (object) str2);
    stringBuilder.AppendLine("</chart>");
    return stringBuilder.ToString();
  }

  private string QuestionBreakdown(bool IsComboChart)
  {
    StringBuilder stringBuilder = new StringBuilder();
    try
    {
      if (!IsComboChart)
        stringBuilder.AppendLine(string.Format("<chart showvalues=\"1\" valuefontbold=\"1\" plotspacepercent=\"10\" yaxisname=\"Avg. Score\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\" placeValuesInside='1' rotateValues='1'>", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
      stringBuilder.AppendFormat("<dataset Name='Avg. Rating' RenderAs='{0}'>", (object) this.ChartType);
      if (this.CustomQueryName != null && this.CustomQueryName != "")
        this.dr = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
      else
        this.dr = !(this.CompanyCode == "NW_") || !(this.CompanyChildId != "1") || this.CompanyChildId == null || !(this.CompanyChildId != "") ? DAL.ReturnDataReader(string.Format("{0}calculateQuestionAVGByCompanyId", (object) this.CompanyCode), this.ParamNames, this.ParamValues) : DAL.ReturnDataReader(string.Format("{0}_calculateQuestionAVGByCompanyId", (object) "NW_Group"), this.ParamNames, this.ParamValues);
      int num1 = this.dr.HasRows ? 1 : 0;
      while (this.dr.Read())
      {
        int int32 = Convert.ToInt32(this.dr["QuestionId"]);
        double num2 = Convert.ToDouble(this.dr["Answer"] == DBNull.Value ? (object) 0 : this.dr["Answer"]);
        double num3 = Convert.ToDouble(this.dr["QuestionCount"]) == 0.0 ? 1.0 : Convert.ToDouble(this.dr["QuestionCount"]);
        double num4 = 0.0;
        num4 = int32 != 999999 ? Math.Round(num2 / num3, 2) : Math.Round(num2, 2);
        int num5;
        switch (int32)
        {
          case 12:
          case 44:
            num5 = 0;
            break;
          case 32:
            num5 = 0;
            break;
          default:
            num5 = int32 != 54 ? 1 : 0;
            break;
        }
        if (num5 != 0)
        {
          string str = this.dr["QuestionDescription"].ToString().Replace("&", "and").Replace("'", "");
          if (str.Length > 30)
            str = str.Substring(0, 30) + "...";
          stringBuilder.AppendFormat("<set label='{0}' value='{1}'/>", (object) str, (object) num4.ToString("#0.00"));
        }
      }
      this.dr.Close();
      this.dr.Dispose();
      stringBuilder.AppendLine("</dataset>");
      if (!IsComboChart)
        stringBuilder.AppendLine("</chart>");
    }
    catch (Exception ex)
    {
      stringBuilder = new StringBuilder();
      stringBuilder.Append(ex.Message);
    }
    return stringBuilder.ToString();
  }

  private string CostPerClaim()
  {
    StringBuilder stringBuilder = new StringBuilder();
    try
    {
      this.dr = DAL.ReturnDataReader(string.Format("{0}CostPerClaim", (object) this.CompanyCode), this.ParamNames, this.ParamValues);
      int num1 = this.dr.HasRows ? 1 : 0;
      string str1 = string.Format("<chart xaxisname=\"[XAXIS]\" yaxisname=\"Avg. Cost Per Claim\" numberprefix=\"$\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor);
      while (this.dr.Read())
      {
        string newValue = this.dr["FieldName"].ToString();
        double num2 = Convert.ToDouble(this.dr["ClaimPaidAvg"]);
        string str2 = this.dr["FieldDescription"].ToString().Replace("&", "and").Replace("'", "");
        if (str2.Length > 30)
          str2 = str2.Substring(0, 30) + "...";
        if (stringBuilder.Length == 0)
          stringBuilder.AppendLine(str1.Replace("[XAXIS]", newValue));
        stringBuilder.AppendFormat("<set label='{0}' value='{1}'/>", (object) str2, (object) Math.Round(num2, 2));
      }
      this.dr.Close();
      this.dr.Dispose();
      stringBuilder.AppendLine("</chart>");
    }
    catch (Exception ex)
    {
      stringBuilder = new StringBuilder();
      stringBuilder.Append(ex.Message);
    }
    return stringBuilder.ToString();
  }

  private string TotalCostClaim()
  {
    StringBuilder stringBuilder = new StringBuilder();
    try
    {
      this.dr = DAL.ReturnDataReader(string.Format("{0}CostPerClaim", (object) this.CompanyCode), this.ParamNames, this.ParamValues);
      int num1 = this.dr.HasRows ? 1 : 0;
      string str1 = string.Format("<chart xaxisname=\"[XAXIS]\" yaxisname=\"Total Claims Cost\" numberprefix=\"$\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\" decimals=\"2\" forcedecimals=\"1\" formatNumberScale=\"0\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor);
      while (this.dr.Read())
      {
        string newValue = this.dr["FieldName"].ToString();
        double num2 = Convert.ToDouble(this.dr["TotalClaimAmount"]);
        string str2 = this.dr["FieldDescription"].ToString().Replace("&", "and").Replace("'", "");
        if (str2.Length > 30)
          str2 = str2.Substring(0, 30) + "...";
        if (stringBuilder.Length == 0)
          stringBuilder.AppendLine(str1.Replace("[XAXIS]", newValue));
        stringBuilder.AppendFormat("<set label='{0}' value='{1}'/>", (object) str2, (object) num2);
      }
      this.dr.Close();
      this.dr.Dispose();
      stringBuilder.AppendLine("</chart>");
    }
    catch (Exception ex)
    {
      stringBuilder = new StringBuilder();
      stringBuilder.Append(ex.Message);
    }
    return stringBuilder.ToString();
  }

  private string CalculateTrendAvg()
  {
    StringBuilder stringBuilder = new StringBuilder();
    try
    {
      this.dr = DAL.ReturnDataReader(string.Format("{0}TrendAvg", (object) this.CompanyCode), this.ParamNames, this.ParamValues);
      int num1 = this.dr.HasRows ? 1 : 0;
      stringBuilder.AppendLine(string.Format("<chart yAxisMaxValue=\"5\" yAxisMinValue=\"0\" yAxisValuesStep=\"0.5\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\" decimals=\"2\" forcedecimals=\"1\" formatNumberScale=\"0\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
      while (this.dr.Read())
      {
        Convert.ToInt32(this.dr["QuestionId"]);
        double num2 = Convert.ToDouble(this.dr["Answer"] == DBNull.Value ? (object) 0 : this.dr["Answer"]);
        double num3 = Convert.ToDouble(this.dr["QuestionCount"]) == 0.0 ? 1.0 : Convert.ToDouble(this.dr["QuestionCount"]);
        double num4 = 0.0;
        num4 = num2 / num3;
        string str = this.dr["QuestionDescription"].ToString().Replace("&", "and").Replace("'", "");
        if (str.Length > 30)
          str = str.Substring(0, 30) + "...";
        stringBuilder.AppendFormat("<set label='{0}' value='{1}'/>", (object) str, (object) num4.ToString("#0.##"));
      }
      this.dr.Close();
      this.dr.Dispose();
      stringBuilder.AppendLine("</chart>");
    }
    catch (Exception ex)
    {
      stringBuilder = new StringBuilder();
      stringBuilder.Append(ex.Message);
    }
    return stringBuilder.ToString();
  }

  private string TrendChart()
  {
    StringBuilder stringBuilder = new StringBuilder();
    try
    {
      this.dr = DAL.ReturnDataReader(string.Format("{0}TrendChart", (object) this.CompanyCode), this.ParamNames, this.ParamValues);
      int num1 = this.dr.HasRows ? 1 : 0;
      stringBuilder.AppendLine(string.Format("<chart yAxisMaxValue=\"100\" yAxisMinValue=\"0\" yAxisValuesStep=\"10\" numbersuffix=\"%\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
      while (this.dr.Read())
      {
        Convert.ToInt32(this.dr["QuestionId"]);
        double num2 = Convert.ToDouble(this.dr["Answer"] == DBNull.Value ? (object) 0 : this.dr["Answer"]);
        double num3 = Convert.ToDouble(this.dr["QuestionCount"]) == 0.0 ? 1.0 : Convert.ToDouble(this.dr["QuestionCount"]);
        double num4 = 0.0;
        num4 = num2 / num3 * 100.0;
        string str = this.dr["QuestionDescription"].ToString().Replace("&", "and").Replace("'", "");
        if (str.Length > 30)
          str = str.Substring(0, 30) + "...";
        stringBuilder.AppendFormat("<set label='{0}' value='{1}'/>", (object) str, (object) num4.ToString("#0.#0"));
      }
      this.dr.Close();
      this.dr.Dispose();
      stringBuilder.AppendLine("</chart>");
    }
    catch (Exception ex)
    {
      stringBuilder = new StringBuilder();
      stringBuilder.Append(ex.Message);
    }
    return stringBuilder.ToString();
  }

  private string PercentBarChart()
  {
    StringBuilder stringBuilder = new StringBuilder();
    try
    {
      this.dr = DAL.ReturnDataReader(string.Format("{0}PercentBarChart", (object) this.CompanyCode), this.ParamNames, this.ParamValues);
      int num = this.dr.HasRows ? 1 : 0;
      stringBuilder.AppendLine(string.Format("<chart xaxisname='Agent' yAxisMaxValue=\"100\" yAxisMinValue=\"0\" yAxisValuesStep=\"10\" numbersuffix=\"%\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
      while (this.dr.Read())
      {
        string str1 = this.dr["value"].ToString();
        string str2 = this.dr["AxisLabel"].ToString().Replace("&", "and").Replace("'", "");
        string str3 = this.dr["ToolTipText"].ToString();
        if (str2.Length > 30)
          str2 = str2.Substring(0, 30) + "...";
        if (str3 != "")
          stringBuilder.AppendFormat("<set label='{0}' value='{1}' tooltext='{2}' />", (object) str2, (object) str1, (object) str3);
        else
          stringBuilder.AppendFormat("<set label='{0}' value='{1}' />", (object) str2, (object) str1);
      }
      this.dr.Close();
      this.dr.Dispose();
      stringBuilder.AppendLine("</chart>");
    }
    catch (Exception ex)
    {
      stringBuilder = new StringBuilder();
      stringBuilder.Append(ex.Message);
    }
    return stringBuilder.ToString();
  }

  private string StandardBarChart()
  {
    StringBuilder stringBuilder = new StringBuilder();
    try
    {
      if (this.CustomQueryName != null && this.CustomQueryName != "")
        this.dr = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
      else
        this.dr = DAL.ReturnDataReader(string.Format("{0}StandardBarChart", (object) this.CompanyCode), this.ParamNames, this.ParamValues);
      int fieldCount = this.dr.FieldCount;
      stringBuilder.AppendLine(string.Format("<chart showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
      stringBuilder.AppendFormat("<dataset Name='Result' renderas='{0}'>", (object) this.ChartType);
      while (this.dr.Read())
      {
        for (int ordinal = 0; ordinal < fieldCount; ++ordinal)
        {
          string name = this.dr.GetName(ordinal);
          string str = this.dr[ordinal].ToString();
          stringBuilder.AppendFormat("<set label='{0}' value='{1}'/>", (object) name, (object) str);
        }
      }
      this.dr.Close();
      this.dr.Dispose();
      stringBuilder.AppendLine("</dataset>");
      stringBuilder.AppendLine("</chart>");
    }
    catch (Exception ex)
    {
      stringBuilder = new StringBuilder();
      stringBuilder.Append(ex.Message);
    }
    return stringBuilder.ToString();
  }

  private string NW_QuestionBreakdown(bool IsComboChart)
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    try
    {
      if (!IsComboChart)
        stringBuilder1.AppendLine(string.Format("<chart showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
      stringBuilder2.AppendLine("<categories>");
      stringBuilder1.AppendFormat("<dataset seriesname='Avg. Rating' renderas='{0}'>", (object) this.ChartType);
      if (this.CustomQueryName != null && this.CustomQueryName != "")
        this.dr = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
      else
        this.dr = !(this.CompanyCode == "NW_") || !(this.CompanyChildId != "1") || this.CompanyChildId == null || !(this.CompanyChildId != "") ? DAL.ReturnDataReader(string.Format("{0}calculateQuestionAVGByCompanyId", (object) this.CompanyCode), this.ParamNames, this.ParamValues) : DAL.ReturnDataReader(string.Format("{0}_calculateQuestionAVGByCompanyId", (object) "NW_Group"), this.ParamNames, this.ParamValues);
      int num1 = this.dr.HasRows ? 1 : 0;
      while (this.dr.Read())
      {
        int int32 = Convert.ToInt32(this.dr["QuestionId"]);
        double num2 = Math.Round(Convert.ToDouble(this.dr["Answer"] == DBNull.Value ? (object) 0 : this.dr["Answer"]) / (Convert.ToDouble(this.dr["QuestionCount"]) == 0.0 ? 1.0 : Convert.ToDouble(this.dr["QuestionCount"])), 1);
        int num3;
        switch (int32)
        {
          case 12:
          case 44:
            num3 = 0;
            break;
          case 32:
            num3 = 0;
            break;
          default:
            num3 = int32 != 54 ? 1 : 0;
            break;
        }
        if (num3 != 0)
        {
          if (!stringBuilder2.ToString().Contains(this.dr["QuestionDescription"].ToString()))
            stringBuilder2.AppendLine(string.Format("<category label='{0}' />", this.dr["QuestionDescription"]));
          stringBuilder1.AppendFormat("<set label='{0}' value='{1}'/>", (object) this.dr["QuestionDescription"].ToString().Replace("&", "and").Replace("'", ""), (object) num2);
        }
      }
      this.dr.Close();
      this.dr.Dispose();
      stringBuilder1.AppendLine("</dataset>");
      stringBuilder2.AppendLine("</categories>");
      stringBuilder1.AppendLine(stringBuilder2.ToString());
      if (!IsComboChart)
        stringBuilder1.AppendLine("</chart>");
    }
    catch (Exception ex)
    {
      stringBuilder1 = new StringBuilder();
      stringBuilder1.Append(ex.Message);
    }
    return stringBuilder1.ToString();
  }

  private string QuestionBreakdownLineGraph(bool IsComboChart, string ComboChartType)
  {
    StringBuilder stringBuilder = new StringBuilder();
    try
    {
      if (!IsComboChart)
        stringBuilder.AppendLine(string.Format("<chart showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
      stringBuilder.AppendFormat("<dataset seriesname='Avg. Rating' RenderAs='{0}'>", (object) ComboChartType);
      if (this.CustomQueryName != null && this.CustomQueryName != "")
        this.dr = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
      else
        this.dr = DAL.ReturnDataReader(string.Format("{0}calculateQuestionAVGByCompanyId", (object) this.CompanyCode), this.ParamNames, this.ParamValues);
      while (this.dr.Read())
      {
        int int32 = Convert.ToInt32(this.dr["QuestionId"]);
        double num1 = Convert.ToDouble(this.dr["Answer"] == DBNull.Value ? (object) 0 : this.dr["Answer"]);
        double num2 = Convert.ToDouble(this.dr["QuestionCount"]) == 0.0 ? 1.0 : Convert.ToDouble(this.dr["QuestionCount"]);
        double num3 = Convert.ToInt32(this.CompanyId) <= 10008 ? Math.Round(num1 / num2, 1) : Math.Round(num1 / num2, 2);
        if (int32 != 12)
          stringBuilder.AppendFormat("<set label='{0}' value='{1}'/>", (object) this.dr["QuestionDescription"].ToString().Replace("&", "and"), (object) num3);
      }
      this.dr.Close();
      this.dr.Dispose();
      stringBuilder.AppendLine("</dataset>");
      if (!IsComboChart)
        stringBuilder.AppendLine("</chart>");
    }
    catch
    {
      stringBuilder = new StringBuilder();
    }
    return stringBuilder.ToString();
  }

  private string NW_QuestionBreakdownByRegion()
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2;
    try
    {
      return this.NW_QuestionBreakdown(false);
    }
    catch (Exception ex)
    {
      stringBuilder2 = new StringBuilder();
      stringBuilder2.Append("Data Relation Error");
    }
    return stringBuilder2.ToString();
  }

  private string QuestionBreakdownByRegion()
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    StringBuilder stringBuilder3 = new StringBuilder();
    try
    {
      string str1 = "";
      string chartType = this.ChartType;
      stringBuilder1.AppendLine(string.Format("<chart yaxisname=\"Score\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
      this.ChartType = "Line";
      stringBuilder2.AppendLine("<categories>");
      stringBuilder1.AppendLine(this.QuestionBreakdownLineGraph(true, "Line"));
      this.ChartType = chartType;
      if (this.CustomQueryName != null && this.CustomQueryName != "")
        this.dr = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
      else
        this.dr = DAL.ReturnDataReader(string.Format("{0}calculateQuestionRegionAVGByCompanyId", (object) this.CompanyCode), this.ParamNames, this.ParamValues);
      bool hasRows = this.dr.HasRows;
      while (this.dr.Read())
      {
        string str2 = this.dr["LocationCode"].ToString();
        double num = Convert.ToDouble(Convert.ToDouble(this.dr["Answer"] == DBNull.Value ? (object) 0 : this.dr["Answer"]) / (Convert.ToDouble(this.dr["QuestionCount"]) == 0.0 ? 1.0 : Convert.ToDouble(this.dr["QuestionCount"])));
        if (str1 != str2)
        {
          if (str1 != "" && str1 != str2)
            stringBuilder3.AppendLine("</dataset>");
          string str3 = this.dr["LocationDescription"].ToString();
          if (str3.Length > 60)
            str3 = str3.Substring(0, 60);
          stringBuilder3.AppendFormat("<dataset seriesname='{0}'>", (object) str3);
          str1 = str2;
        }
        if ((int) this.dr["QuestionId"] != 12 && (int) this.dr["QuestionId"] != 32)
        {
          string str4 = this.dr["QuestionDescription"].ToString().Replace("&", "and");
          if (!stringBuilder2.ToString().Contains(str4))
            stringBuilder2.AppendLine(string.Format("<category label='{0}' />", (object) str4));
          stringBuilder3.AppendFormat("<set value='{0}'/>", (object) num.ToString("00.##"));
        }
      }
      stringBuilder2.AppendLine("</categories>");
      this.dr.Close();
      this.dr.Dispose();
      if (hasRows)
        stringBuilder3.AppendLine("</dataset>");
      stringBuilder1.AppendLine(stringBuilder2.ToString());
      stringBuilder1.AppendLine(stringBuilder3.ToString());
      stringBuilder1.AppendLine("</chart>");
    }
    catch
    {
      stringBuilder1 = new StringBuilder();
      stringBuilder1.Append("Data Relation Error");
    }
    return stringBuilder1.ToString();
  }

  private string QuestionBreakdownByRegionDashboard()
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    StringBuilder stringBuilder3 = new StringBuilder();
    try
    {
      string str1 = "";
      string chartType = this.ChartType;
      if (Convert.ToInt32(this.CompanyId) > 10008 && Convert.ToInt32(this.CompanyId) < 10018)
        stringBuilder1.AppendLine(string.Format("<chart yaxisname=\"Score\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
      else
        stringBuilder1.AppendLine(string.Format("<chart showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
      this.ChartType = "Line";
      stringBuilder3.AppendLine("<categories>");
      stringBuilder2.AppendLine(this.QuestionBreakdownLineGraph(true, "Line"));
      this.ChartType = chartType;
      if (this.CustomQueryName != null && this.CustomQueryName != "")
        this.dr = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
      else
        this.dr = DAL.ReturnDataReader(string.Format("{0}calculateQuestionRegionAVGByCompanyId_Dashboard", (object) this.CompanyCode), this.ParamNames, this.ParamValues);
      bool hasRows = this.dr.HasRows;
      while (this.dr.Read())
      {
        string str2 = this.dr["LocationCode"].ToString();
        double num = Convert.ToDouble(Convert.ToDouble(this.dr["Answer"] == DBNull.Value ? (object) 0 : this.dr["Answer"]) / (Convert.ToDouble(this.dr["QuestionCount"]) == 0.0 ? 1.0 : Convert.ToDouble(this.dr["QuestionCount"])));
        if (str1 != str2)
        {
          if (str1 != "" && str1 != str2)
            stringBuilder2.AppendLine("</dataset>");
          string str3 = this.dr["LocationDescription"].ToString();
          if (str3.Length > 60)
            str3 = str3.Substring(0, 60);
          stringBuilder2.AppendFormat("<dataset seriesname='{0}'>", (object) str3);
          str1 = str2;
        }
        if ((int) this.dr["QuestionId"] != 12 && (int) this.dr["QuestionId"] != 32)
        {
          string str4 = this.dr["QuestionDescription"].ToString().Replace("&", "and");
          if (!stringBuilder3.ToString().Contains(str4))
            stringBuilder3.AppendLine(string.Format("<category label='{0}' />", (object) str4));
          if (Convert.ToInt32(this.CompanyId) > 10008 || Convert.ToInt32(this.CompanyId) == 10003)
            stringBuilder2.AppendFormat("<set label='{0}' value='{1}'/>", (object) str4, (object) num.ToString("00.#0"));
          else
            stringBuilder2.AppendFormat("<set label='{0}' value='{1}'/>", (object) str4, (object) num.ToString("00.#0"));
        }
      }
      stringBuilder3.AppendLine("</categories>");
      this.dr.Close();
      this.dr.Dispose();
      if (hasRows)
        stringBuilder2.AppendLine("</dataset>");
      stringBuilder1.AppendLine(stringBuilder3.ToString());
      stringBuilder1.AppendLine(stringBuilder2.ToString());
      stringBuilder1.AppendLine("</chart>");
    }
    catch
    {
      stringBuilder1 = new StringBuilder();
      stringBuilder1.Append("Data Relation Error");
    }
    return stringBuilder1.ToString();
  }

  private string QuestionBreakdownHaulingCodeByRegion()
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    try
    {
      string str1 = "";
      string chartType = this.ChartType;
      if (Convert.ToInt32(this.CompanyId) > 10008 && Convert.ToInt32(this.CompanyId) < 10018)
        stringBuilder1.AppendLine(string.Format("<chart yaxisname=\"Score\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
      else
        stringBuilder1.AppendLine(string.Format("<chart showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
      this.ChartType = "Line";
      stringBuilder1.AppendLine(this.QuestionBreakdownLineGraph(true, "Line"));
      this.ChartType = chartType;
      if (this.CustomQueryName != null && this.CustomQueryName != "")
        this.dr = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
      else
        this.dr = DAL.ReturnDataReader(string.Format("{0}calculateQuestionHaulingRegionAVGByCompanyId", (object) this.CompanyCode), this.ParamNames, this.ParamValues);
      bool hasRows = this.dr.HasRows;
      stringBuilder2.AppendLine("<categories>");
      while (this.dr.Read())
      {
        string str2 = this.dr["LocationCode"].ToString();
        double num = Convert.ToDouble(Convert.ToDouble(this.dr["Answer"] == DBNull.Value ? (object) 0 : this.dr["Answer"]) / (Convert.ToDouble(this.dr["QuestionCount"]) == 0.0 ? 1.0 : Convert.ToDouble(this.dr["QuestionCount"])));
        if (!stringBuilder2.ToString().Contains(this.dr["QuestionDescription"].ToString()))
          stringBuilder2.AppendLine(string.Format("<category label='{0}' />", (object) this.dr["QuestionDescription"].ToString()));
        if (str1 != str2)
        {
          if (str1 != "" && str1 != str2)
            stringBuilder1.AppendLine("</dataset>");
          string str3 = this.dr["LocationDescription"].ToString();
          if (str3.Length > 60)
            str3 = str3.Substring(0, 60);
          stringBuilder1.AppendFormat("<dataset seriesname='{0}'>", (object) str3);
          str1 = str2;
        }
        if ((int) this.dr["QuestionId"] != 12 && (int) this.dr["QuestionId"] != 32)
        {
          string str4 = this.dr["QuestionDescription"].ToString().Replace("&", "and");
          if (Convert.ToInt32(this.CompanyId) > 10008 || Convert.ToInt32(this.CompanyId) == 10003)
            stringBuilder1.AppendFormat("<set label='{0}' value='{1}'/>", (object) str4, (object) num.ToString("00.#0"));
          else
            stringBuilder1.AppendFormat("<set label='{0}' value='{1}'/>", (object) str4, (object) num.ToString("00.#0"));
        }
      }
      this.dr.Close();
      this.dr.Dispose();
      if (hasRows)
        stringBuilder1.AppendLine("</dataset>");
      stringBuilder2.AppendLine("</categories>");
      stringBuilder1.AppendLine(stringBuilder2.ToString());
      stringBuilder1.AppendLine("</chart>");
    }
    catch
    {
      stringBuilder1 = new StringBuilder();
      stringBuilder1.Append("Data Relation Error");
    }
    return stringBuilder1.ToString();
  }

  private string QuestionBreakdownHaulingCodeByRegionDashboard()
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    try
    {
      string str1 = "";
      string chartType = this.ChartType;
      StringBuilder stringBuilder2 = new StringBuilder();
      StringBuilder stringBuilder3 = new StringBuilder();
      if (Convert.ToInt32(this.CompanyId) > 10008 && Convert.ToInt32(this.CompanyId) < 10018)
        stringBuilder1.AppendLine(string.Format("<chart yaxisname=\"Score\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
      else
        stringBuilder1.AppendLine(string.Format("<chart showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
      this.ChartType = "Line";
      stringBuilder3.AppendLine("<categories>");
      stringBuilder2.AppendLine(this.QuestionBreakdownLineGraph(true, "Line"));
      this.ChartType = chartType;
      if (this.CustomQueryName != null && this.CustomQueryName != "")
        this.dr = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
      else
        this.dr = DAL.ReturnDataReader(string.Format("{0}calculateQuestionHaulingRegionAVGByCompanyId_Dashboard", (object) this.CompanyCode), this.ParamNames, this.ParamValues);
      bool hasRows = this.dr.HasRows;
      while (this.dr.Read())
      {
        string str2 = this.dr["LocationCode"].ToString();
        double num = Convert.ToDouble(Convert.ToDouble(this.dr["Answer"] == DBNull.Value ? (object) 0 : this.dr["Answer"]) / (Convert.ToDouble(this.dr["QuestionCount"]) == 0.0 ? 1.0 : Convert.ToDouble(this.dr["QuestionCount"])));
        if (str1 != str2)
        {
          if (str1 != "" && str1 != str2)
            stringBuilder2.AppendLine("</dataset>");
          string str3 = this.dr["LocationDescription"].ToString();
          if (str3.Length > 60)
            str3 = str3.Substring(0, 60);
          stringBuilder2.AppendFormat("<dataset seriesname='{0}'>", (object) str3);
          str1 = str2;
        }
        if ((int) this.dr["QuestionId"] != 12 && (int) this.dr["QuestionId"] != 32)
        {
          string str4 = this.dr["QuestionDescription"].ToString().Replace("&", "and");
          if (!stringBuilder3.ToString().Contains(str4))
            stringBuilder3.AppendLine(string.Format("<category label='{0}' />", (object) str4));
          if (Convert.ToInt32(this.CompanyId) > 10008 || Convert.ToInt32(this.CompanyId) == 10003)
            stringBuilder2.AppendFormat("<set value='{0}'/>", (object) num.ToString("00.#0"));
          else
            stringBuilder2.AppendFormat("<set value='{0}'/>", (object) num.ToString("00.#0"));
        }
      }
      stringBuilder3.AppendLine("</categories>");
      this.dr.Close();
      this.dr.Dispose();
      if (hasRows)
        stringBuilder2.AppendLine("</dataset>");
      stringBuilder1.AppendLine(stringBuilder3.ToString());
      stringBuilder1.AppendLine(stringBuilder2.ToString());
      stringBuilder1.AppendLine("</chart>");
    }
    catch
    {
      stringBuilder1 = new StringBuilder();
      stringBuilder1.Append("Data Relation Error");
    }
    return stringBuilder1.ToString();
  }

  private string Daniels_QuestionBreakdown()
  {
    StringBuilder stringBuilder = new StringBuilder();
    try
    {
      if (this.CustomQueryName != null && this.CustomQueryName != "")
        this.dr = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
      else
        this.dr = DAL.ReturnDataReader(string.Format("{0}calculateQuestionRegionAVGByCompanyId", (object) this.CompanyCode), this.ParamNames, this.ParamValues);
      stringBuilder.AppendLine(string.Format("<chart xaxisname=\"Companies\" yaxisname=\"Score\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
      while (this.dr.Read())
      {
        double num = Convert.ToDouble(Convert.ToDouble(this.dr["Answer"] == DBNull.Value ? (object) 0 : this.dr["Answer"]) / (Convert.ToDouble(this.dr["QuestionCount"]) == 0.0 ? 1.0 : Convert.ToDouble(this.dr["QuestionCount"])));
        if ((int) this.dr["QuestionId"] != 44)
          stringBuilder.AppendFormat("<set label='{0}' value='{1}'/>", (object) this.dr["QuestionDescription"].ToString().Replace("&", "and"), (object) num.ToString("00.0"));
      }
      this.dr.Close();
      this.dr.Dispose();
      stringBuilder.AppendLine("</chart>");
    }
    catch (Exception ex)
    {
      stringBuilder = new StringBuilder();
      stringBuilder.Append(ex.Message);
    }
    return stringBuilder.ToString();
  }

  private string DMS_QuestionBreakdown()
  {
    StringBuilder stringBuilder = new StringBuilder();
    try
    {
      if (this.CustomQueryName != null && this.CustomQueryName != "")
        this.dr = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
      else
        this.dr = DAL.ReturnDataReader(string.Format("{0}calculateQuestionRegionAVGByCompanyId", (object) this.CompanyCode), this.ParamNames, this.ParamValues);
      stringBuilder.AppendLine(string.Format("<chart xaxisname=\"Companies\" yaxisname=\"Score\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
      while (this.dr.Read())
      {
        double num = Convert.ToDouble(Convert.ToDouble(this.dr["Answer"] == DBNull.Value ? (object) 0 : this.dr["Answer"]) / (Convert.ToDouble(this.dr["QuestionCount"]) == 0.0 ? 1.0 : Convert.ToDouble(this.dr["QuestionCount"])));
        if ((int) this.dr["QuestionId"] != 54)
          stringBuilder.AppendFormat("<set label='{0}' value='{1}'/>", (object) this.dr["QuestionDescription"].ToString().Replace("&", "and").Replace("'", ""), (object) num.ToString("00.0"));
      }
      this.dr.Close();
      this.dr.Dispose();
      stringBuilder.AppendLine("</chart>");
    }
    catch (Exception ex)
    {
      stringBuilder = new StringBuilder();
      stringBuilder.Append(ex.Message);
    }
    return stringBuilder.ToString();
  }

  public string NW_TopDriverScore()
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (this.CustomQueryName != null && this.CustomQueryName != "")
      this.dr = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
    else
      this.dr = DAL.ReturnDataReader(string.Format("{0}DriverScoreChart", (object) "NW_"));
    stringBuilder.AppendLine(string.Format("<chart xaxisname=\"Companies\" yaxisname=\"Score\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
    stringBuilder.AppendLine("<dataset seriesname='Driver Scores'>");
    while (this.dr.Read())
      stringBuilder.AppendFormat("<set label='{0}' value='{1}'/>", this.dr["DriverName"], (object) Math.Round(Convert.ToDecimal(this.dr["DriverRating"]), 2, MidpointRounding.AwayFromZero));
    stringBuilder.AppendLine("</dataset></chart>");
    this.dr.Close();
    this.dr.Dispose();
    return stringBuilder.ToString();
  }

  public string NW_Military_DriverStackedChart()
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    StringBuilder stringBuilder3 = new StringBuilder();
    StringBuilder stringBuilder4 = new StringBuilder();
    this.dr = DAL.ReturnDataReader("NW_Military_GetDriverScoreChart", this.ParamNames, this.ParamValues);
    stringBuilder1.AppendLine(string.Format("<chart yaxisname=\"Questions\" xaxisname=\"Drivers\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
    while (this.dr.Read())
    {
      if (stringBuilder2.Length == 0)
      {
        stringBuilder2.AppendFormat("<dataset seriesname='All Drivers'>");
        stringBuilder3.AppendFormat("<dataset seriesname='AMVL Drivers'>");
        stringBuilder4.AppendFormat("<dataset seriesname='Other Drivers'>");
      }
      stringBuilder2.AppendLine(string.Format("<set label='{0}' value='{1}' />", (object) this.dr["TotalConsolidatedLabel"].ToString().Replace("&", "and").Replace(":", ""), this.dr["TotalConsolidatedScores"]));
      stringBuilder3.AppendLine(string.Format("<set label='{0}' value='{1}' />", (object) this.dr["TotalConsolidatedLabel"].ToString().Replace("&", "and").Replace(":", ""), this.dr["AMVLDrivers"]));
      stringBuilder4.AppendLine(string.Format("<set label='{0}' value='{1}' />", (object) this.dr["TotalConsolidatedLabel"].ToString().Replace("&", "and").Replace(":", ""), this.dr["AllDrivers"]));
    }
    if (stringBuilder2.Length > 0)
    {
      stringBuilder2.AppendLine("</dataset>");
      stringBuilder3.AppendLine("</dataset>");
      stringBuilder4.AppendLine("</dataset>");
    }
    stringBuilder1.AppendLine(stringBuilder2.ToString());
    stringBuilder1.AppendLine(stringBuilder3.ToString());
    stringBuilder1.AppendLine(stringBuilder4.ToString());
    stringBuilder1.AppendLine("</chart>");
    this.dr.Close();
    this.dr.Dispose();
    return stringBuilder1.ToString();
  }

  public string Mesa_DriverStackedChart()
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    StringBuilder stringBuilder3 = new StringBuilder();
    StringBuilder stringBuilder4 = new StringBuilder();
    this.dr = DAL.ReturnDataReader("Mesa_GetDriverScoreChart", this.ParamNames, this.ParamValues);
    stringBuilder1.AppendLine(string.Format("<chart yaxisname=\"Questions\" xaxisname=\"Drivers\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
    while (this.dr.Read())
    {
      if (stringBuilder2.Length == 0)
      {
        stringBuilder2.AppendFormat("<dataset seriesname='All Drivers'>");
        stringBuilder3.AppendFormat("<dataset seriesname='Mesa Drivers'>");
        stringBuilder4.AppendFormat("<dataset seriesname='Other Drivers'>");
      }
      stringBuilder2.AppendLine(string.Format("<set label='{0}' value='{1}' />", (object) this.dr["TotalConsolidatedLabel"].ToString().Replace("&", "and").Replace(":", ""), this.dr["TotalConsolidatedScores"]));
      stringBuilder3.AppendLine(string.Format("<set label='{0}' value='{1}' />", (object) this.dr["TotalConsolidatedLabel"].ToString().Replace("&", "and").Replace(":", ""), this.dr["MesaDrivers"]));
      stringBuilder4.AppendLine(string.Format("<set label='{0}' value='{1}' />", (object) this.dr["TotalConsolidatedLabel"].ToString().Replace("&", "and").Replace(":", ""), this.dr["AllDrivers"]));
    }
    if (stringBuilder2.Length > 0)
    {
      stringBuilder2.AppendLine("</dataset>");
      stringBuilder3.AppendLine("</dataset>");
      stringBuilder4.AppendLine("</dataset>");
    }
    stringBuilder1.AppendLine(stringBuilder2.ToString());
    stringBuilder1.AppendLine(stringBuilder3.ToString());
    stringBuilder1.AppendLine(stringBuilder4.ToString());
    stringBuilder1.AppendLine("</chart>");
    this.dr.Close();
    this.dr.Dispose();
    return stringBuilder1.ToString();
  }

  public string GetSingleSeriesChart()
  {
    StringBuilder stringBuilder = new StringBuilder();
    this.dr = DAL.ReturnDataReader("GetChartProperties", "CompanyID,ChartCode", string.Format("{0},{1}", (object) this.CompanyId, (object) this.CustomQueryName));
    while (this.dr.Read())
      stringBuilder.AppendLine(string.Format("<chart yaxisname=\"" + this.dr["YAxisLabel"].ToString() + "\" xaxisname=\"" + this.dr["XAxisLabel"].ToString() + "\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
    this.dr.Close();
    this.dr.Dispose();
    this.dr = DAL.ReturnCustomQueryDataReader("GetCustomQueryResult", "CompanyID|QueryName|ParamNames|ParamValues", string.Format("{0}|{1}|{2}|{3}", (object) this.CompanyId, (object) this.CustomQueryName, (object) this.ParamNames, (object) this.ParamValues));
    while (this.dr.Read())
      stringBuilder.AppendLine(string.Format("<set label='{0}' value='{1}' />", this.dr["AxisLabel"], this.dr["YValue"]));
    this.dr.Close();
    this.dr.Dispose();
    stringBuilder.AppendLine("</chart>");
    return stringBuilder.ToString();
  }

  public string GetSingleSeriesChart(bool standardChartTF)
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    int num = 0;
    bool flag1 = false;
    bool flag2 = false;
    string str1 = (string) null;
    string str2 = (string) null;
    this.dr = DAL.ReturnDataReader(string.Format("{0}SingleSeriesChart", (object) this.CompanyCode), this.ParamNames, this.ParamValues);
    while (this.dr.Read())
    {
      if (num == 0)
      {
        if (this.FieldNameExistsTF(this.dr, "YAxisTitle"))
          str2 = this.dr["YAxisTitle"].ToString();
        if (this.FieldNameExistsTF(this.dr, "XAxisTitle"))
          str1 = this.dr["XAxisTitle"].ToString();
        stringBuilder1.AppendLine(string.Format("<chart yaxisname=\"" + (str2 == null ? "" : str2) + "\" xaxisname=\"" + (str1 == null ? "" : str1) + "\" showlegend=\"1\" legendshadow=\"0\" legendborderalpha=\"0\" theme=\"{0}\" useplotgradientcolor=\"{1}\" showplotborder=\"{2}\" showshadow=\"{3}\" bgcolor=\"{4}\" palette=\"{5}\" basefontcolor=\"{6}\">", (object) this.ThemeName, (object) this.UsePlotGradientColor, (object) this.ShowPlotBorder, (object) this.ShowShadow, (object) this.BackgroundColor, (object) this.Palette, (object) this.FontColor));
        if (this.FieldNameExistsTF(this.dr, "MultiSeries"))
        {
          flag2 = true;
          stringBuilder1.AppendLine(string.Format("<dataset seriesname='Agent' renderas='{0}'>", (object) this.ChartType));
        }
        else
          stringBuilder1.AppendLine(string.Format("<dataset seriesname='Ranking' renderas='{0}'>", (object) this.ChartType));
        if (this.FieldNameExistsTF(this.dr, "LinePoint") && stringBuilder2.ToString().Length == 0)
        {
          flag1 = true;
          stringBuilder2.AppendLine("<dataset seriesname='Shipment Count' renderas='Line'>");
        }
        stringBuilder1.AppendLine("");
      }
      if (flag1)
        stringBuilder2.AppendLine(string.Format("<set label='{0}' tooltext='{0}, {1}' value='{1}' />", this.dr["AxisLabel"], this.dr["LinePoint"]));
      if (flag2)
        stringBuilder1.AppendLine(string.Format("<set label='{0}' tooltext='{0}, {1}' value='{1}' />", this.dr["AxisLabel"], this.dr["value"]));
      else
        stringBuilder1.AppendLine(string.Format("<set label='{0}' tooltext='{0}, {1}' value='{1}' />", this.dr["AxisLabel"], this.dr["value"]));
      ++num;
    }
    this.dr.Close();
    this.dr.Dispose();
    stringBuilder1.AppendLine("</dataset>");
    if (flag1)
    {
      stringBuilder2.AppendLine("</dataset>");
      stringBuilder1.AppendLine(stringBuilder2.ToString());
    }
    stringBuilder1.AppendLine("</chart>");
    return stringBuilder1.ToString();
  }

  public string GetChartXaml()
  {
    string chartXaml = "";
    this.dr = DAL.ReturnDataReader(this.QueryName, this.ParamNames + ",ChartType", this.ParamValues + "," + this.ChartType);
    while (this.dr.Read())
      chartXaml = this.dr["ChartXaml"].ToString();
    this.dr.Close();
    this.dr.Dispose();
    return chartXaml;
  }

  public string GetParameterValue(string parameterName)
  {
    string[] strArray1 = this.ParamNames.Split(',');
    bool flag = false;
    int index = 0;
    foreach (string str in strArray1)
    {
      if (str.Trim().ToLower() == parameterName.Trim().ToLower())
      {
        flag = true;
        break;
      }
      ++index;
    }
    string[] strArray2 = this.ParamValues.Split(',');
    return flag ? strArray2[index] : "";
  }

  private bool FieldNameExistsTF(SqlDataReader dr, string fieldName)
  {
    for (int ordinal = 0; ordinal < dr.FieldCount; ++ordinal)
    {
      if (dr.GetName(ordinal).ToLower().Trim() == fieldName.ToLower().Trim())
        return true;
    }
    return false;
  }
}
