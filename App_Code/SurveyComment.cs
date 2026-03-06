// Decompiled with JetBrains decompiler
// Type: SurveyComment
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public class SurveyComment
{
  public string UserID { get; set; }

  public string Comments { get; set; }

  public string SurveyID { get; set; }

  public string CompanyID { get; set; }

  public string CommentType { get; set; }

  public string ParentID { get; set; }

  public string UserType { get; set; }

  public string CommentGUID { get; set; }

  public void Add()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine("INSERT INTO DeficientSurveyComments (CompanyID,SurveyID,Comments,UserID,CommentType,ParentID,UserType,CommentGUID) ");
    stringBuilder.AppendLine("VALUES (@CompanyID,@SurveyID,@Comments,@UserID,@CommentType,@ParentID,@UserType,@CommentGUID) ");
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.Text;
      command.CommandText = stringBuilder.ToString();
      command.Parameters.AddWithValue("CompanyID", (object) this.CompanyID);
      command.Parameters.AddWithValue("SurveyID", (object) this.SurveyID);
      command.Parameters.AddWithValue("UserID", (object) this.UserID);
      command.Parameters.AddWithValue("Comments", (object) this.Comments);
      command.Parameters.AddWithValue("CommentType", (object) this.CommentType);
      command.Parameters.AddWithValue("ParentID", (object) this.ParentID);
      command.Parameters.AddWithValue("UserType", (object) this.UserType);
      command.Parameters.AddWithValue("CommentGUID", this.CommentGUID == null ? (object) "" : (object) this.CommentGUID);
      command.ExecuteNonQuery();
    }
  }

  public void SetCommentAsRead()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine("UPDATE DeficientSurveyComments ");
    stringBuilder.AppendLine(string.Format("SET ViewedTF=1 WHERE CompanyID in ({0}) and SurveyID=@SurveyID and ParentID=@ParentID and UserType=@UserType ", (object) this.CompanyID));
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.Text;
      command.CommandText = stringBuilder.ToString();
      command.Parameters.AddWithValue("SurveyID", (object) this.SurveyID);
      command.Parameters.AddWithValue("ParentID", (object) this.ParentID);
      command.Parameters.AddWithValue("UserType", (object) this.UserType);
      command.ExecuteNonQuery();
    }
  }

  public void SetCheckRequestCommentsAsRead()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine("UPDATE DeficientSurveyComments ");
    stringBuilder.AppendLine(string.Format("SET ViewedTF=1 WHERE CompanyID in ({0}) and CommentType='CheckRequest' and ParentID=@ParentID and UserType=@UserType ", (object) this.CompanyID));
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.Text;
      command.CommandText = stringBuilder.ToString();
      command.Parameters.AddWithValue("ParentID", (object) this.ParentID);
      command.Parameters.AddWithValue("UserType", (object) this.UserType);
      command.ExecuteNonQuery();
    }
  }
}
