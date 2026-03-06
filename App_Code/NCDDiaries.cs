// Decompiled with JetBrains decompiler
// Type: NCDDiaries
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public class NCDDiaries
{
  public string ClaimNumber { get; set; }

  public int CompanyID { get; set; }

  public string QueryType { get; set; }

  public int UserID { get; set; }

  public List<NCDDiary> Diaries { get; set; }

  public NCDDiaries() => this.Diaries = new List<NCDDiary>();

  public void Add()
  {
    NCDDiary ncdDiary = this.Diaries.First<NCDDiary>();
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = "cms_InsertDiaries";
      command.Parameters.AddWithValue("CompanyID", (object) ncdDiary.CompanyID);
      command.Parameters.AddWithValue("ClaimNumber", (object) ncdDiary.ClaimNumber);
      command.Parameters.AddWithValue("ReportDate", (object) ncdDiary.ReportDate);
      command.Parameters.AddWithValue("NextReportDate", (object) ncdDiary.NextReportDate);
      command.Parameters.AddWithValue("DiaryType", (object) ncdDiary.DiaryType);
      command.Parameters.AddWithValue("Comments", (object) ncdDiary.Comments);
      command.Parameters.AddWithValue("CompletedTF", (object) ncdDiary.CompletedTF);
      command.Parameters.AddWithValue("UpdatedBy", (object) ncdDiary.UpdatedBy);
      command.Parameters.AddWithValue("UserID", (object) ncdDiary.UserID);
      if (ncdDiary.AttachmentFile != null)
      {
        command.Parameters.AddWithValue("AttachmentFile", (object) ((IEnumerable<string>) ncdDiary.AttachmentFile.Split(new string[1]
        {
          "base64,"
        }, StringSplitOptions.None)).Last<string>());
        command.Parameters.AddWithValue("AttachmentFileName", (object) ncdDiary.AttachmentFileName);
      }
      command.ExecuteNonQuery();
    }
  }

  public void Update()
  {
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      foreach (NCDDiary diary in this.Diaries)
      {
        command.Parameters.Clear();
        command.Parameters.AddWithValue("pkDiaryID", (object) diary.pkDiaryID);
        if (diary.Mode.ToLower().Trim() == "delete")
        {
          command.CommandText = "cms_DeleteDiary";
        }
        else
        {
          if (diary.Mode.ToLower().Trim() == "add")
            command.CommandText = "cms_InsertDiaries";
          else
            command.CommandText = "cms_UpdateDiaries";
          command.Parameters.AddWithValue("CompanyID", (object) diary.CompanyID);
          command.Parameters.AddWithValue("ClaimNumber", (object) diary.ClaimNumber);
          command.Parameters.AddWithValue("ReportDate", (object) diary.ReportDate);
          command.Parameters.AddWithValue("NextReportDate", (object) diary.NextReportDate);
          command.Parameters.AddWithValue("DiaryType", (object) diary.DiaryType);
          command.Parameters.AddWithValue("Comments", (object) diary.Comments);
          command.Parameters.AddWithValue("CompletedTF", (object) diary.CompletedTF);
          command.Parameters.AddWithValue("UpdatedBy", (object) diary.UpdatedBy);
          command.Parameters.AddWithValue("UserID", (object) diary.UserID);
          if (diary.AttachmentFile != null)
          {
            command.Parameters.AddWithValue("AttachmentFile", (object) ((IEnumerable<string>) diary.AttachmentFile.Split(new string[1]
            {
              "base64,"
            }, StringSplitOptions.None)).Last<string>());
            command.Parameters.AddWithValue("AttachmentFileName", (object) diary.AttachmentFileName);
          }
        }
        command.ExecuteNonQuery();
      }
    }
  }

  public void Get()
  {
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = "cms_GetDiaries";
      command.Parameters.AddWithValue("ClaimNumber", (object) this.ClaimNumber);
      command.Parameters.AddWithValue("CompanyID", (object) this.CompanyID);
      SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
      while (sqlDataReader.Read())
        this.Diaries.Add(new NCDDiary()
        {
          pkDiaryID = Convert.ToInt32(sqlDataReader["pkDiaryID"]),
          CompanyID = Convert.ToInt32(sqlDataReader["CompanyID"]),
          ClaimNumber = sqlDataReader["ClaimNumber"].ToString(),
          ReportDate = Convert.ToDateTime(sqlDataReader["ReportDate"]),
          NextReportDate = Convert.ToDateTime(sqlDataReader["NextReportDate"]),
          DiaryType = sqlDataReader["DiaryType"].ToString(),
          Comments = sqlDataReader["Comments"].ToString(),
          CompletedTF = Convert.ToBoolean(sqlDataReader["CompletedTF"]),
          CompletedDate = Convert.ToDateTime(sqlDataReader["CompletedDate"] == DBNull.Value ? (object) "1/1/1900" : sqlDataReader["CompletedDate"]),
          UpdatedBy = sqlDataReader["UpdatedBy"].ToString(),
          UserID = Convert.ToInt32(sqlDataReader["UserID"]),
          AttachmentFileName = sqlDataReader["AttachmentFileName"].ToString(),
          DownloadUrl = sqlDataReader["DownloadUrl"].ToString()
        });
      sqlDataReader.Close();
    }
  }
}
