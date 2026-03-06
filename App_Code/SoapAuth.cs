// Decompiled with JetBrains decompiler
// Type: AIS.SoapAuth
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services.Protocols;

namespace AIS
{
  public class SoapAuth : SoapHeader
  {
    public string UserName { get; set; }

    public string Password { get; set; }

    public bool IsValid()
    {
      int num = -99;
      using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
      {
        sqlConnection.Open();
        SqlCommand command = sqlConnection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = string.Format("select * from [Login] where username='{0}' and password='{1}' and IsActive='True' ", (object) this.UserName, (object) this.Password);
        SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
        while (sqlDataReader.Read())
          num = Convert.ToInt32(sqlDataReader["UserID"]);
      }
      return num != -99;
    }
  }
}
