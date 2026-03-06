// Decompiled with JetBrains decompiler
// Type: IAISRest
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

[ServiceContract]
public interface IAISRest
{
  [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/getdriverscorecard/{email}")]
  [OperationContract]
  DriverScoreCard GetDriverScoreCard(string email);

  [OperationContract]
  [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/getdemodriverscorecard/{email}")]
  DriverScoreCard GetDemoDriverScoreCard(string email);

  [OperationContract]
  [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/getcartusavailregionstates/{region}")]
  string GetCartusAvailRegionStates(string region);

  [OperationContract]
  [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/gettableheaderdescription/{tableheader}")]
  string GetTableHeaderDescription(string tableheader);

  [OperationContract]
  [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/getchartdata/")]
  ZingChartData GetChartData();

  [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/getzingchartdata?query_name={query_name}&pname={pname}&pvalue={pvalue}&theme={theme}&chart_type={chart_type}")]
  [OperationContract]
  ZingChartData GetZingChartData(
    string query_name,
    string pname,
    string pvalue,
    string theme,
    string chart_type);

  [OperationContract]
  [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/gethighchartdata?query_name={query_name}&pname={pname}&pvalue={pvalue}&theme={theme}&chart_type={chart_type}")]
  HighChartData GetHighChartData(
    string query_name,
    string pname,
    string pvalue,
    string theme,
    string chart_type);

  [OperationContract]
  [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "/logusage/{username}")]
  UserLog PostUserLog(string username);

  [OperationContract]
  [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/getavlchartersurveydata?uid={username}&pwd={pwd}")]
  List<AVLCharterSurveyResultsModel> GetAVLCharterSurveyData(
    string username,
    string pwd);

  [OperationContract]
  [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/getavlatlasvanlinessurveydata?uid={username}&pwd={pwd}")]
  List<AVLAtlasVanlinesSurveyResultsModel> GetAVLAtlasVanlinesSurveyData(
    string username,
    string pwd);

  [OperationContract]
  [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/getavlairessurveydata?uid={username}&pwd={pwd}")]
  List<AVLAiresSurveyResultsModel> GetAVLAiresSurveyData(
    string username,
    string pwd);

  [OperationContract]
  [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/getavlmmisurveydata?uid={username}&pwd={pwd}")]
  List<AVLMMISurveyResultsModel> GetAVLMMISurveyData(
    string username,
    string pwd);

  [OperationContract]
  [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/getavlrmwsurveydata?uid={username}&pwd={pwd}")]
  List<AVLRMWSurveyResultsModel> GetAVLRMWSurveyData(
    string username,
    string pwd);

  [OperationContract]
  [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/getavlsurveydata?uid={username}&pwd={pwd}")]
  List<AVLSurveyResultsModel> GetAVLSurveyData(string username, string pwd);

  [OperationContract]
  [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/getnwsurveydata/{nextpageurl}?uid={username}&pwd={pwd}")]
  List<NWConsolidatedSurveyResultsModel> GetNWSurveyData(
    string nextpageurl,
    string username,
    string pwd);
}
