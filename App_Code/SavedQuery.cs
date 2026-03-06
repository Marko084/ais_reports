// Decompiled with JetBrains decompiler
// Type: SavedQuery
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public class SavedQuery
{
  public int pkSavedQueryID { get; set; }

  public string CompanyID { get; set; }

  public string UserID { get; set; }

  public string QueryName { get; set; }

  public string QueryFields { get; set; }

  public string QueryTables { get; set; }

  public string QueryWhere { get; set; }

  public string TabName { get; set; }

  public void Add()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine("INSERT INTO cms_SavedQueries (CompanyID,UserID,QueryName,QueryFields,QueryTables,QueryWhere,TabName,UpdatedDate,UpdatedBy) ");
    stringBuilder.AppendLine("VALUES (@CompanyID,@UserID,@QueryName,@QueryFields,@QueryTables,@QueryWhere,@TabName,@UpdatedDate,@UpdatedBy)");
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.Text;
      command.CommandText = stringBuilder.ToString();
      command.Parameters.AddWithValue("CompanyID", (object) this.CompanyID);
      command.Parameters.AddWithValue("UserID", (object) this.UserID);
      command.Parameters.AddWithValue("QueryName", (object) this.QueryName);
      command.Parameters.AddWithValue("QueryFields", (object) this.QueryFields);
      command.Parameters.AddWithValue("QueryTables", (object) this.QueryTables);
      command.Parameters.AddWithValue("QueryWhere", (object) this.QueryWhere);
      command.Parameters.AddWithValue("TabName", (object) this.TabName);
      command.Parameters.AddWithValue("UpdatedDate", (object) DateTime.Now);
      command.Parameters.AddWithValue("UpdatedBy", (object) this.UserID);
      command.ExecuteNonQuery();
    }
  }

  public void Delete()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine("DELETE FROM cms_SavedQueries WHERE QueryName=@QueryName and UserID=@UserID ");
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.Text;
      command.CommandText = stringBuilder.ToString();
      command.Parameters.AddWithValue("QueryName", (object) this.QueryName);
      command.Parameters.AddWithValue("UserID", (object) this.UserID);
      command.ExecuteNonQuery();
    }
  }

  public void Update()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine("UPDATE cms_SavedQueries ");
    stringBuilder.AppendLine("SET QueryFields=@QueryFields,QueryTables=@QueryTables,QueryWhere=@QueryWhere,UpdatedBy=@UpdatedBy,UpdatedDate=@UpdatedDate,@TabName ");
    stringBuilder.AppendLine("WHERE QueryName=@QueryName and UserID=@UserID ");
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.Text;
      command.CommandText = stringBuilder.ToString();
      command.Parameters.AddWithValue("CompanyID", (object) this.CompanyID);
      command.Parameters.AddWithValue("UserID", (object) this.UserID);
      command.Parameters.AddWithValue("QueryName", (object) this.QueryName);
      command.Parameters.AddWithValue("QueryFields", (object) this.QueryFields);
      command.Parameters.AddWithValue("QueryTables", (object) this.QueryTables);
      command.Parameters.AddWithValue("QueryWhere", (object) this.QueryWhere);
      command.Parameters.AddWithValue("TabName", (object) this.TabName);
      command.Parameters.AddWithValue("UpdatedDate", (object) DateTime.Now);
      command.Parameters.AddWithValue("UpdatedBy", (object) this.UserID);
      command.ExecuteNonQuery();
    }
  }
}
