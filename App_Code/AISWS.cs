using AIS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Web.Services;

[WebService(Namespace = "http://aismgt.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public class AISWS : WebService
{
  [WebMethod]
  public string HelloWorld() => "Hello World";

  [WebMethod]
  [ScriptMethod]
  public string[] GetInvoiceNbrList(string prefixText, int count, string contextKey)
  {
    List<string> stringList = new List<string>(count);
    try
    {
      if (count == 0)
        count = 15;

      if (prefixText.Trim().Equals(""))
        return new string[0];

      string str1 = contextKey.Split('|').GetValue(0).ToString();
      string str2 = contextKey.Split('|').GetValue(1).ToString();

      SqlDataReader sqlDataReader = DAL.ReturnDataReader("getInvoiceNbrList", "companyId,prefix,count,userId", string.Format("{0},{1},{2},{3}", (object) str1, (object) prefixText, (object) count, (object) str2));
     
            while (sqlDataReader.Read())
        stringList.Add(string.Format("\"{0}\"", (object) sqlDataReader["InvoiceNumber"].ToString()));
      sqlDataReader.Close();
      sqlDataReader.Dispose();
    }
    catch (Exception ex)
    {
      return new string[1]{ ex.ToString() };
    }
    return stringList.ToArray();
  }

  [WebMethod]
  [ScriptMethod]
  public string[] GetTransfereeList(string prefixText, int count, string contextKey)
  {
    List<string> stringList = new List<string>(count);
    try
    {
      if (count == 0)
        count = 15;
      if (prefixText.Trim().Equals(""))
        return new string[0];
      string str1 = contextKey.Split('|').GetValue(0).ToString();
      string str2 = contextKey.Split('|').GetValue(1).ToString();
      SqlDataReader sqlDataReader = DAL.ReturnDataReader("getTransfereeNames", "companyId,prefix,count,userId", string.Format("{0},{1},{2},{3}", (object) str1, (object) prefixText, (object) count, (object) str2));
      int num = 0;
      while (sqlDataReader.Read())
      {
        stringList.Add(string.Format("\"{0}\"", (object) sqlDataReader["transfereename"].ToString()));
        ++num;
        if (num > 15)
          break;
      }
      sqlDataReader.Close();
      sqlDataReader.Dispose();
    }
    catch (Exception ex)
    {
      return new string[1]{ ex.ToString() };
    }
    return stringList.ToArray();
  }

  [ScriptMethod]
  [WebMethod]
  public string[] GetCSCNoList(string prefixText, int count, string contextKey)
  {
    List<string> stringList = new List<string>(count);
    try
    {
      if (count == 0)
        count = 15;
      if (prefixText.Trim().Equals(""))
        return new string[0];
      string str1 = contextKey.Split('|').GetValue(0).ToString();
      string str2 = contextKey.Split('|').GetValue(1).ToString();
      SqlDataReader sqlDataReader = DAL.ReturnDataReader("getCSCNoList", "companyId,prefix,count,userId", string.Format("{0},{1},{2},{3}", (object) str1, (object) prefixText, (object) count, (object) str2));
      while (sqlDataReader.Read())
        stringList.Add(string.Format("\"{0}\"", (object) sqlDataReader[0].ToString()));
      sqlDataReader.Close();
      sqlDataReader.Dispose();
    }
    catch (Exception ex)
    {
      return new string[1]{ ex.ToString() };
    }
    return stringList.ToArray();
  }

  [WebMethod]
  [ScriptMethod]
  public string[] GetDriverList(string prefixText, int count, string contextKey)
  {
    List<string> stringList = new List<string>(count);
    try
    {
      if (count == 0)
        count = 15;
      if (prefixText.Trim().Equals(""))
        return new string[0];
      string str1 = contextKey.Split('|').GetValue(0).ToString();
      string str2 = contextKey.Split('|').GetValue(1).ToString();
      SqlDataReader sqlDataReader = DAL.ReturnDataReader("getDriverList", "companyId,prefix,count,userId", string.Format("{0},{1},{2},{3}", (object) str1, (object) prefixText, (object) count, (object) str2));
      while (sqlDataReader.Read())
      {
        double result;
        double.TryParse(sqlDataReader[0].ToString(), out result);
        if (result > 0.0)
          stringList.Add("\"" + sqlDataReader[0].ToString() + "\"");
        else
          stringList.Add(sqlDataReader[0].ToString());
      }
      sqlDataReader.Close();
      sqlDataReader.Dispose();
    }
    catch (Exception ex)
    {
      return new string[1]{ ex.ToString() };
    }
    return stringList.ToArray();
  }

  [ScriptMethod]
  [WebMethod]
  public string[] GetLocationCodeList(string prefixText, int count, string contextKey)
  {
    List<string> stringList = new List<string>(count);
    try
    {
      if (count == 0)
        count = 15;
      if (prefixText.Trim().Equals(""))
        return new string[0];
      string str1 = contextKey.Split('|').GetValue(0).ToString();
      string str2 = contextKey.Split('|').GetValue(1).ToString();
      SqlDataReader sqlDataReader = DAL.ReturnDataReader("getLocationCodeList", "companyId,prefix,count,userId", string.Format("{0},{1},{2},{3}", (object) str1, (object) prefixText, (object) count, (object) str2));
      while (sqlDataReader.Read())
        stringList.Add(sqlDataReader["LocationCode"].ToString());
      sqlDataReader.Close();
      sqlDataReader.Dispose();
    }
    catch (Exception ex)
    {
      return new string[1]{ ex.ToString() };
    }
    return stringList.ToArray();
  }

  [ScriptMethod]
  [WebMethod]
  public string[] GetSCACList(string prefixText, int count, string contextKey)
  {
    List<string> stringList = new List<string>(count);
    try
    {
      if (count == 0)
        count = 15;
      if (prefixText.Trim().Equals(""))
        return new string[0];
      string str1 = contextKey.Split('|').GetValue(0).ToString();
      string str2 = contextKey.Split('|').GetValue(1).ToString();
      SqlDataReader sqlDataReader = DAL.ReturnDataReader(string.Format("{0}_getSCACList", (object) contextKey.Split('|').GetValue(2).ToString()), "companyId,prefix,userId", string.Format("{0},{1},{2}", (object) str1, (object) prefixText, (object) str2));
      while (sqlDataReader.Read())
        stringList.Add(sqlDataReader["SCAC"].ToString());
      sqlDataReader.Close();
      sqlDataReader.Dispose();
    }
    catch (Exception ex)
    {
      return new string[1]{ ex.ToString() };
    }
    return stringList.ToArray();
  }

  [ScriptMethod]
  [WebMethod]
  public string[] GetCSRList(string prefixText, int count, string contextKey)
  {
    List<string> stringList = new List<string>(count);
    try
    {
      if (count == 0)
        count = 15;
      if (prefixText.Trim().Equals(""))
        return new string[0];
      string str1 = contextKey.Split('|').GetValue(0).ToString();
      string str2 = contextKey.Split('|').GetValue(1).ToString();
      SqlDataReader sqlDataReader = DAL.ReturnDataReader("getCSRList", "companyId,prefix,count,userId", string.Format("{0},{1},{2},{3}", (object) str1, (object) prefixText, (object) count, (object) str2));
      while (sqlDataReader.Read())
        stringList.Add(sqlDataReader[0].ToString());
      sqlDataReader.Close();
      sqlDataReader.Dispose();
    }
    catch (Exception ex)
    {
      return new string[1]{ ex.ToString() };
    }
    return stringList.ToArray();
  }

  [ScriptMethod]
  [WebMethod]
  public string[] GetAgentOriginList(string prefixText, int count, string contextKey)
  {
    List<string> stringList = new List<string>(count);
    try
    {
      if (count == 0)
        count = 15;
      if (prefixText.Trim().Equals(""))
        return new string[0];
      string str1 = contextKey.Split('|').GetValue(0).ToString();
      string str2 = contextKey.Split('|').GetValue(1).ToString();
      SqlDataReader sqlDataReader = DAL.ReturnDataReader("getAgentOriginList", "companyId,prefix,count,userId", string.Format("{0},{1},{2},{3}", (object) str1, (object) prefixText, (object) count, (object) str2));
      while (sqlDataReader.Read())
        stringList.Add(string.Format("\"{0}\"", (object) sqlDataReader[0].ToString()));
      sqlDataReader.Close();
      sqlDataReader.Dispose();
    }
    catch (Exception ex)
    {
      return new string[1]{ ex.ToString() };
    }
    return stringList.ToArray();
  }

  [ScriptMethod]
  [WebMethod]
  public string[] GetAgentDestinationList(string prefixText, int count, string contextKey)
  {
    List<string> stringList = new List<string>(count);
    try
    {
      if (count == 0)
        count = 15;
      if (prefixText.Trim().Equals(""))
        return new string[0];
      string str1 = contextKey.Split('|').GetValue(0).ToString();
      string str2 = contextKey.Split('|').GetValue(1).ToString();
      SqlDataReader sqlDataReader = DAL.ReturnDataReader("getAgentDestinationList", "companyId,prefix,count,userId", string.Format("{0},{1},{2},{3}", (object) str1, (object) prefixText, (object) count, (object) str2));
      while (sqlDataReader.Read())
        stringList.Add(string.Format("\"{0}\"", (object) sqlDataReader[0].ToString()));
      sqlDataReader.Close();
      sqlDataReader.Dispose();
    }
    catch (Exception ex)
    {
      return new string[1]{ ex.ToString() };
    }
    return stringList.ToArray();
  }

  [ScriptMethod]
  [WebMethod]
  public string[] GetClientNamesList(string prefixText, int count, string contextKey)
  {
    List<string> stringList = new List<string>(count);
    try
    {
      if (count == 0)
        count = 15;
      if (prefixText.Trim().Equals(""))
        return new string[0];
      string str1 = contextKey.Split('|').GetValue(0).ToString();
      string str2 = contextKey.Split('|').GetValue(1).ToString();
      SqlDataReader sqlDataReader = DAL.ReturnDataReader("getClientNamesList", "companyId,prefix,count,userId", string.Format("{0},{1},{2},{3}", (object) str1, (object) prefixText, (object) count, (object) str2));
      while (sqlDataReader.Read())
        stringList.Add(sqlDataReader[0].ToString());
      sqlDataReader.Close();
      sqlDataReader.Dispose();
    }
    catch (Exception ex)
    {
      return new string[1]{ ex.ToString() };
    }
    return stringList.ToArray();
  }

  [WebMethod]
  [ScriptMethod]
  public string[] GetLookupList(string prefixText, int count, string contextKey)
  {
    List<string> stringList = new List<string>(count);
    try
    {
      if (count == 0)
        count = 15;
      if (prefixText.Trim().Equals(""))
        return new string[0];
      string str1 = contextKey.Split('|').GetValue(0).ToString();
      string str2 = contextKey.Split('|').GetValue(1).ToString();
      string str3 = contextKey.Split('|').GetValue(2).ToString();
      string str4 = (string) null;
      if (contextKey.Split('|').Length > 3)
        str4 = contextKey.Split('|').GetValue(3).ToString();
      SqlDataReader sqlDataReader = DAL.ReturnDataReader(nameof (GetLookupList), "companyId,fieldName,prefix,count,userId,TableName", string.Format("{0},{1},{2},{3},{4},{5}", (object) str1, (object) str3, (object) prefixText, (object) count, (object) str2, (object) str4));
      while (sqlDataReader.Read())
        stringList.Add(string.Format("\"{0}\"", (object) sqlDataReader[0].ToString()));
      sqlDataReader.Close();
      sqlDataReader.Dispose();
    }
    catch (Exception ex)
    {
      return new string[1]{ ex.ToString() };
    }
    return stringList.ToArray();
  }

  [WebMethod]
  public void AddSurveyComment(SurveyComment NewSurveyComment) => NewSurveyComment.Add();

  [WebMethod]
  public void SetCommentAsRead(SurveyComment NewSurveyComment) => NewSurveyComment.SetCommentAsRead();

  [WebMethod]
  public void SetCheckRequestCommentsAsRead(SurveyComment NewSurveyComment) => NewSurveyComment.SetCheckRequestCommentsAsRead();

  [WebMethod]
  public void AddCalendarAppointment(CalendarAppointment NewCalendarAppointment) => NewCalendarAppointment.Add();

  [WebMethod]
  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
  public void AddSavedQuery(SavedQuery SavedQuery) => SavedQuery.Add();

  [WebMethod]
  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
  public void UpdateSavedQuery(SavedQuery SavedQuery) => SavedQuery.Update();

  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
  [WebMethod]
  public void DeleteSavedQuery(SavedQuery SavedQuery) => SavedQuery.Delete();

  [WebMethod]
  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
  public global::JSONDBQuery JSONDBQuery(global::JSONDBQuery NewJSONDBQuery)
  {
    global::JSONDBQuery jsondbQuery = new global::JSONDBQuery();
    if (NewJSONDBQuery.QueryType.ToLower() == "add")
      jsondbQuery = NewJSONDBQuery.Add();
    else if (NewJSONDBQuery.QueryType.ToLower() == "save")
      NewJSONDBQuery.Update();
    else if (NewJSONDBQuery.QueryType.ToLower() == "update")
      NewJSONDBQuery.Update();
    else if (NewJSONDBQuery.QueryType.ToLower() == "delete")
      NewJSONDBQuery.Delete();
    else if (NewJSONDBQuery.QueryType.ToLower() == "edit")
      jsondbQuery = NewJSONDBQuery.Get();
    else if (NewJSONDBQuery.QueryType.ToLower() == "get")
      jsondbQuery = NewJSONDBQuery.Get();
    else if (NewJSONDBQuery.QueryType.ToLower() == "addlist")
      NewJSONDBQuery.AddList();
    return jsondbQuery;
  }

  [WebMethod]
  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
  public NCDClaimants NCDClaimantQuery(NCDClaimants NewNCDClaimantQuery)
  {
    NCDClaimants ncdClaimants = NewNCDClaimantQuery;
    if (NewNCDClaimantQuery.QueryType.ToLower() == "add")
      NewNCDClaimantQuery.Add();
    else if (NewNCDClaimantQuery.QueryType.ToLower() == "update")
      NewNCDClaimantQuery.Update();
    else if (NewNCDClaimantQuery.QueryType.ToLower() == "get")
      NewNCDClaimantQuery.Get();
    return ncdClaimants;
  }

  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
  [WebMethod]
  public NCDDiaries NCDDiaryQuery(NCDDiaries NewNCDDiaryQuery)
  {
    NCDDiaries ncdDiaries = NewNCDDiaryQuery;
    if (NewNCDDiaryQuery.QueryType.ToLower() == "add")
      NewNCDDiaryQuery.Add();
    else if (NewNCDDiaryQuery.QueryType.ToLower() == "update")
      NewNCDDiaryQuery.Update();
    else if (NewNCDDiaryQuery.QueryType.ToLower() == "get")
      NewNCDDiaryQuery.Get();
    return ncdDiaries;
  }

  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
  [WebMethod]
  public NCDEmailMessage SendEmailMessage(NCDEmailMessage NewNCDMailMessage)
  {
    NCDEmailMessage ncdEmailMessage = new NCDEmailMessage();
    NewNCDMailMessage.Send();
    return ncdEmailMessage;
  }

  [WebMethod]
  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
  public NCDDocument NCDDocumentQuery(NCDDocument NewNCDDocumentQuery)
  {
    NCDDocument ncdDocument = new NCDDocument();
    if (NewNCDDocumentQuery.QueryType == "add")
      NewNCDDocumentQuery.Add();
    else if (NewNCDDocumentQuery.QueryType == "update")
      NewNCDDocumentQuery.Update();
    else if (NewNCDDocumentQuery.QueryType == "delete")
      NewNCDDocumentQuery.Delete();
    return ncdDocument;
  }

  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
  [WebMethod]
  public NCDClaimCheck NCDClaimCheckQuery(NCDClaimCheck NewNCDClaimCheckQuery)
  {
    if (NewNCDClaimCheckQuery.QueryType == "add")
      NewNCDClaimCheckQuery.Add();
    else if (NewNCDClaimCheckQuery.QueryType == "get")
      NewNCDClaimCheckQuery.Get();
    else if (NewNCDClaimCheckQuery.QueryType == "update")
      NewNCDClaimCheckQuery.Update();
    else if (NewNCDClaimCheckQuery.QueryType == "delete")
      NewNCDClaimCheckQuery.Delete();
    return NewNCDClaimCheckQuery;
  }

  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
  [WebMethod]
  public NCDUsers NCDUsersQuery(NCDUsers NewNCDUsersQuery)
  {
    if (NewNCDUsersQuery.QueryType == "getbyusertype")
      NewNCDUsersQuery.GetUsersByType();
    else if (NewNCDUsersQuery.QueryType == "getuserdetail")
      NewNCDUsersQuery.GetUserDetail();
    else if (NewNCDUsersQuery.QueryType == "updateuser")
      NewNCDUsersQuery.UpdateUser();
    return NewNCDUsersQuery;
  }

  [WebMethod]
  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
  public NCDSmsSender NCDSmsSenderQuery(NCDSmsSender NewNCDSmsSender)
  {
    NewNCDSmsSender.SendMessage();
    return NewNCDSmsSender;
  }

  [WebMethod]
  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
  public NCDCSSWeeklyMetrics NCDCSSWeeklyMetricQuery(
    NCDCSSWeeklyMetrics NewNCDCSSWeeklyMetricQuery)
  {
    NCDCSSWeeklyMetrics ncdcssWeeklyMetrics = NewNCDCSSWeeklyMetricQuery;
    if (NewNCDCSSWeeklyMetricQuery.QueryType.ToLower() == "add")
      NewNCDCSSWeeklyMetricQuery.Add();
    else if (NewNCDCSSWeeklyMetricQuery.QueryType.ToLower() == "update")
      NewNCDCSSWeeklyMetricQuery.Update();
    else if (NewNCDCSSWeeklyMetricQuery.QueryType.ToLower() == "get")
      NewNCDCSSWeeklyMetricQuery.Get();
    return ncdcssWeeklyMetrics;
  }

  [WebMethod]
  public string GetGridData(GridData NewGridData) => NewGridData.Search();

  public string GetJsonData(DataQuery NewDataQuery) => NewDataQuery.GetResultJsonString();

  [WebMethod]
  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
  public bool DeleteUser(string userid)
  {
    try
    {
      DAL.ReturnScalarValue("admin_DeleteUser", "UserID", userid);
      return true;
    }
    catch (Exception ex)
    {
      return false;
    }
  }
}
