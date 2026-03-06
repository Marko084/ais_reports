// Decompiled with JetBrains decompiler
// Type: NCDUsers
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class NCDUsers
{
  public int CompanyID { get; set; }

  public int UserID { get; set; }

  public string UserType { get; set; }

  public string QueryType { get; set; }

  public string ClaimNumber { get; set; }

  public List<NCDUser> Users { get; set; }

  public NCDUsers() => this.Users = new List<NCDUser>();

  public void GetUserDetail()
  {
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = "admin_GetUserType";
      command.Parameters.AddWithValue("CompanyID", (object) this.CompanyID);
      command.Parameters.AddWithValue("UserID", (object) this.UserID);
      SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
      while (sqlDataReader.Read())
        this.Users.Add(new NCDUser()
        {
          UserID = Convert.ToInt32(sqlDataReader["UserID"]),
          CompanyID = Convert.ToInt32(sqlDataReader["CompanyID"]),
          FirstName = sqlDataReader["FirstName"].ToString(),
          LastName = sqlDataReader["LastName"].ToString(),
          Email = sqlDataReader["Email"].ToString(),
          UserName = sqlDataReader["UserName"].ToString(),
          UserType = sqlDataReader["UserType"].ToString()
        });
      sqlDataReader.Close();
    }
  }

  public void GetUsersByType()
  {
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = "admin_GetUsersByType";
      command.Parameters.AddWithValue("CompanyID", (object) this.CompanyID);
      command.Parameters.AddWithValue("UserType", (object) this.UserType);
      SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
      while (sqlDataReader.Read())
        this.Users.Add(new NCDUser()
        {
          UserID = Convert.ToInt32(sqlDataReader["UserID"]),
          CompanyID = Convert.ToInt32(sqlDataReader["CompanyID"]),
          FirstName = sqlDataReader["FirstName"].ToString(),
          LastName = sqlDataReader["LastName"].ToString(),
          Email = sqlDataReader["Email"].ToString(),
          UserName = sqlDataReader["UserName"].ToString(),
          UserType = sqlDataReader["Name"].ToString()
        });
      sqlDataReader.Close();
    }
  }

  public void UpdateUser()
  {
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = "cms_UpdateConvergentUsers";
      command.Parameters.AddWithValue("UserID", (object) this.UserID);
      command.Parameters.AddWithValue("CompanyID", (object) this.CompanyID);
      command.Parameters.AddWithValue("ClaimNumber", (object) this.ClaimNumber);
    }
  }
}
