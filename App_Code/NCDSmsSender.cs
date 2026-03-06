// Decompiled with JetBrains decompiler
// Type: NCDSmsSender
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Twilio;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;

public class NCDSmsSender
{
  private string authToken;
  private string accountSSID;

  public int CompanyID { get; set; }

  public string PhoneNumber { get; set; }

  public int BatchID { get; set; }

  public string SurveyID { get; set; }

  public string QueryType { get; set; }

  public string SurveyType { get; set; }

  public string PrimaryKeyFieldName { get; set; }

  public NCDSmsSender()
  {
    this.authToken = ConfigurationManager.AppSettings["TwilioAccontAuthToken"];
    this.accountSSID = ConfigurationManager.AppSettings["TwilioAccountSSID"];
  }

  public bool SendMessage()
  {
    bool flag = false;
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = "GetNotificationList";
      SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
      while (sqlDataReader.Read())
      {
        if (Convert.ToBoolean(sqlDataReader["AutoSmsTF"]) && Convert.ToInt32(sqlDataReader["CompanyID"]) == this.CompanyID && Convert.ToBoolean(sqlDataReader["ActiveTF"]))
        {
          if (this.SurveyID != null && this.SurveyID != "")
            this.SendNotification(sqlDataReader["queryName"].ToString());
          else
            this.SendNotifications(sqlDataReader["queryName"].ToString());
        }
      }
    }
    return flag;
  }

  private void SendNotification(string queryName)
  {
    TwilioClient.Init(this.accountSSID, this.authToken);
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = queryName;
      command.Parameters.AddWithValue("CompanyID", (object) this.CompanyID);
      command.Parameters.AddWithValue("BatchID", (object) this.BatchID);
      command.Parameters.AddWithValue("PhoneNumber", (object) this.PhoneNumber);
      command.Parameters.AddWithValue("SurveyID", (object) this.SurveyID);
      command.Parameters.AddWithValue("SurveyType", (object) this.SurveyType);
      SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
      while (sqlDataReader.Read())
      {
        sqlDataReader["PhoneNumber"].ToString().Replace("+", "").Replace("-", "").Replace(" ", "").Trim();
        string str1 = string.Format("{0}\n\n{1}", sqlDataReader["Body"], sqlDataReader["MediaUrl"]);
        string uriString = sqlDataReader["ImageUrl"].ToString();
        if (uriString != null && uriString != "")
        {
          Twilio.Types.PhoneNumber phoneNumber1 = new Twilio.Types.PhoneNumber(sqlDataReader["FromAddress"].ToString());
          Twilio.Types.PhoneNumber phoneNumber2 = new Twilio.Types.PhoneNumber(sqlDataReader["PhoneNumber"].ToString());
          Twilio.Types.PhoneNumber phoneNumber3 = phoneNumber1;
          string str2 = str1;
          List<Uri> uriList = new List<Uri>();
          uriList.Add(new Uri(uriString));
          Decimal? nullable1 = new Decimal?();
          bool? nullable2 = new bool?();
          MessageResource.Create(phoneNumber2, (string) null, phoneNumber3, (string) null, str2, uriList, (Uri) null, (string) null, nullable1, nullable2, null);
        }
        else
        {
          Twilio.Types.PhoneNumber phoneNumber = new Twilio.Types.PhoneNumber(sqlDataReader["FromAddress"].ToString());
          MessageResource.Create(new Twilio.Types.PhoneNumber(sqlDataReader["PhoneNumber"].ToString()), (string) null, phoneNumber, (string) null, str1, (List<Uri>) null, (Uri) null, (string) null, new Decimal?(), new bool?(), null);
        }
        this.LogNotifications();
      }
    }
  }

  private void SendNotifications(string queryName)
  {
    TwilioClient.Init(this.accountSSID, this.authToken);
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = queryName;
      command.Parameters.AddWithValue("CompanyID", (object) this.CompanyID);
      command.Parameters.AddWithValue("BatchID", (object) this.BatchID);
      command.Parameters.AddWithValue("PhoneNumber", (object) this.PhoneNumber);
      SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
      while (sqlDataReader.Read())
      {
        try
        {
          MessageResource.Create(new Twilio.Types.PhoneNumber("+" + sqlDataReader["PhoneNumber"].ToString().Replace("+", "").Replace("-", "").Replace(" ", "").Trim()), (string) null, new Twilio.Types.PhoneNumber(sqlDataReader["FromAddress"].ToString()), (string) null, "Test", (List<Uri>) null, (Uri) null, (string) null, new Decimal?(), new bool?(), null);
        }
        catch (Exception ex)
        {
        }
      }
    }
  }

  private void LogNotifications()
  {
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.Text;
      command.CommandText = "insert into cms_AutoSmsTracker(CompanyID,PhoneNumber,KeyFieldValue,KeyFieldName,EmailSentCount,SurveyType) values(@CompanyID,@PhoneNumber,@KeyFieldValue,@KeyFieldName,@EmailSentCount,@SurveyType) ";
      command.Parameters.AddWithValue("CompanyID", (object) this.CompanyID);
      command.Parameters.AddWithValue("PhoneNumber", (object) this.PhoneNumber);
      command.Parameters.AddWithValue("KeyFieldValue", (object) this.SurveyID);
      command.Parameters.AddWithValue("KeyFieldName", (object) this.PrimaryKeyFieldName);
      command.Parameters.AddWithValue("EmailSentCount", (object) 1);
      command.Parameters.AddWithValue("SurveyType", (object) this.SurveyType);
      command.ExecuteNonQuery();
    }
  }
}
