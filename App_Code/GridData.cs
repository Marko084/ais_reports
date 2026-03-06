// Decompiled with JetBrains decompiler
// Type: GridData
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public class GridData
{
  public string UserID { get; set; }

  public string CompanyID { get; set; }

  public string FieldList { get; set; }

  public string TableName { get; set; }

  public string Filter { get; set; }

  public string AdvancedFilter { get; set; }

  public string OrderBy { get; set; }

  public string Search()
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    int num = 0;
    string[] strArray = this.FieldList.Split('|');
    if (this.Filter.Length > 0)
      stringBuilder1.AppendFormat("SELECT {0} FROM {1} ", (object) string.Join(",", strArray), (object) this.TableName);
    else
      stringBuilder1.AppendFormat("SELECT TOP 500 {0} FROM {1} ", (object) this.TableName);
    if (this.Filter.Trim().Length > 0)
    {
      stringBuilder1.Append("WHERE ");
      foreach (string str in strArray)
      {
        if (num == 0)
          stringBuilder1.AppendFormat("{0} LIKE '%{1}%' ", (object) str, (object) this.Filter.Trim());
        else
          stringBuilder1.AppendFormat("OR {0} LIKE '%{1}%' ", (object) str, (object) this.Filter.Trim());
        ++num;
      }
    }
    if (this.OrderBy.Length > 0)
      stringBuilder1.AppendFormat("ORDER BY {0} ", (object) this.OrderBy);
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.Text;
      command.CommandText = stringBuilder1.ToString();
      SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
      while (sqlDataReader.Read())
      {
        foreach (string str in strArray)
          ;
      }
      sqlDataReader.Dispose();
    }
    return stringBuilder1.ToString();
  }
}
