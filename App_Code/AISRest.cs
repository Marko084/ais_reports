// Decompiled with JetBrains decompiler
// Type: AISRest
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using AIS;
using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using System.ServiceModel.Activation;

[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class AISRest : IAISRest
{
  private DriverScoreCard driverScoreCard = new DriverScoreCard();
  private ZingChartData zcData = new ZingChartData();
  private HighChartData hcData = new HighChartData();
  private AVLCharterSurveyResultsModel avlCharterSurveyResults = new AVLCharterSurveyResultsModel();
  private AVLSurveyResultsModel avlSurveyResults = new AVLSurveyResultsModel();
  private AVLAtlasVanlinesSurveyResultsModel avlAtlasVanlinesSurveyResults = new AVLAtlasVanlinesSurveyResultsModel();
  private AVLAiresSurveyResultsModel avlAiresSurveyResults = new AVLAiresSurveyResultsModel();
  private AVLRMWSurveyResultsModel avlRMWSurveyResults = new AVLRMWSurveyResultsModel();
  private AVLMMISurveyResultsModel avlMMISurveyResults = new AVLMMISurveyResultsModel();

  public HighChartData GetHighChartData(
    string query_name,
    string pname,
    string pvalue,
    string theme,
    string chart_type)
  {
    return this.hcData.Get(query_name, pname, pvalue, theme, chart_type);
  }

  public DriverScoreCard GetDriverScoreCard(string email) => this.driverScoreCard.Get(email);

  public DriverScoreCard GetDemoDriverScoreCard(string email) => this.driverScoreCard.Get(email, "10000");

  public string GetCartusAvailRegionStates(string region)
  {
    string availRegionStates = "";
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.Text;
      command.CommandText = "select stuff ((select ','+[State] from Cartus_Avail_RH_StateRegion where region='" + region + "' for xml path('')), 1, 1, '')";
      SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
      while (sqlDataReader.Read())
        availRegionStates = sqlDataReader[0].ToString();
    }
    return availRegionStates;
  }

  public string GetTableHeaderDescription(string tableHeader)
  {
    string str = tableHeader.Trim().ToLower().Replace("-", "").Replace(" ", "").Replace("'", "");
    if (str == "bookedinregion")
      return "Number of shipments booked by regional network agent";
    if (str == "selfhauled")
      return "Number of shipments booker = hauler";
    if (str == "hauledoutofnetwork")
      return "Number of shipments Atlas to haul";
    if (str == "accepted")
      return "Percentage of shipments accepted from Load Board";
    if (str == "pushed")
      return "Percentage of shipments pushed from Load Board";
    if (str == "sitevisits")
      return "Number of QC visits completed";
    if (str == "csr")
      return "Average Score PME Q1";
    if (str == "oa")
      return "Average Score PME Q2";
    if (str == "da")
      return "Average Score PME Q3";
    if (str == "pvo")
      return "Average Score PME Q4";
    if (str == "overall")
      return "Average Score PME Q5";
    if (str == "totalpmesreturned")
      return "Total number of post move evaluations returned";
    return str == "averagebyregion" ? "Average of all scores" : str;
  }

  public ZingChartData GetChartData() => this.zcData.Get();

  public ZingChartData GetZingChartData(
    string query_name,
    string pname,
    string pvalue,
    string theme,
    string chart_type)
  {
    return this.zcData.Get(query_name, pname, pvalue, theme, chart_type);
  }

  public UserLog PostUserLog(string username)
  {
    UserLog userLog = new UserLog();
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.Text;
      command.CommandText = string.Format("insert into UserHistory(UserName,LastAccessedDate) Values('{0}','{1}')", (object) username, (object) DateTime.Now);
      command.ExecuteNonQuery();
      userLog.Message = "Usage Date Saved.";
    }
    return userLog;
  }

  public List<AVLCharterSurveyResultsModel> GetAVLCharterSurveyData(
    string username,
    string pwd)
  {
    if (new SoapAuth()
    {
      UserName = username,
      Password = pwd
    }.IsValid())
      return this.avlCharterSurveyResults.GetSurveyData();
    throw new Exception("Access Denied. Please contact Audit and Infomration Services (AIS) if you've recieved this message.");
  }

  public List<AVLSurveyResultsModel> GetAVLSurveyData(
    string username,
    string pwd)
  {
    if (new SoapAuth()
    {
      UserName = username,
      Password = pwd
    }.IsValid())
      return this.avlSurveyResults.GetSurveyData();
    throw new Exception("Access Denied. Please contact Audit and Infomration Services (AIS) if you've recieved this message.");
  }

  public List<AVLAiresSurveyResultsModel> GetAVLAiresSurveyData(
    string username,
    string pwd)
  {
    if (new SoapAuth()
    {
      UserName = username,
      Password = pwd
    }.IsValid())
      return this.avlAiresSurveyResults.GetSurveyData();
    throw new Exception("Access Denied. Please contact Audit and Infomration Services (AIS) if you've recieved this message.");
  }

  public List<AVLAtlasVanlinesSurveyResultsModel> GetAVLAtlasVanlinesSurveyData(
    string username,
    string pwd)
  {
    if (new SoapAuth()
    {
      UserName = username,
      Password = pwd
    }.IsValid())
      return this.avlAtlasVanlinesSurveyResults.GetSurveyData();
    throw new Exception("Access Denied. Please contact Audit and Infomration Services (AIS) if you've recieved this message.");
  }

  public List<AVLMMISurveyResultsModel> GetAVLMMISurveyData(
    string username,
    string pwd)
  {
    if (new SoapAuth()
    {
      UserName = username,
      Password = pwd
    }.IsValid())
      return this.avlMMISurveyResults.GetSurveyData();
    throw new Exception("Access Denied. Please contact Audit and Infomration Services (AIS) if you've recieved this message.");
  }

  public List<AVLRMWSurveyResultsModel> GetAVLRMWSurveyData(
    string username,
    string pwd)
  {
    if (new SoapAuth()
    {
      UserName = username,
      Password = pwd
    }.IsValid())
      return this.avlRMWSurveyResults.GetSurveyData();
    throw new Exception("Access Denied. Please contact Audit and Infomration Services (AIS) if you've recieved this message.");
  }

  public List<NWConsolidatedSurveyResultsModel> GetNWSurveyData(
    string nextpageurl,
    string username,
    string pwd)
  {
    if (new SoapAuth()
    {
      UserName = username,
      Password = pwd
    }.IsValid())
      return new NWConsolidatedSurveyResultsModel().GetSurveyData(nextpageurl);
    throw new Exception("Access Denied. Please contact Audit and Infomration Services (AIS) if you've recieved this message in error.");
  }
}
