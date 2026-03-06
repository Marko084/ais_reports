// Decompiled with JetBrains decompiler
// Type: NCDCSSWeeklyMetrics
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public class NCDCSSWeeklyMetrics
{
  public int CompanyID { get; set; }

  public string QueryType { get; set; }

  public int UserID { get; set; }

  public List<NCDCSSWeeklyMetric> Metrics { get; set; }

  public string Mode { get; set; }

  public NCDCSSWeeklyMetrics() => this.Metrics = new List<NCDCSSWeeklyMetric>();

  public void Add()
  {
    NCDCSSWeeklyMetric ncdcssWeeklyMetric = this.Metrics.First<NCDCSSWeeklyMetric>();
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = "cms_InsertCSSWeeklyMetric";
      command.Parameters.AddWithValue("CompanyID", (object) ncdcssWeeklyMetric.CompanyID);
      command.Parameters.AddWithValue("DataFieldName", (object) ncdcssWeeklyMetric.DataFieldName);
      command.Parameters.AddWithValue("DataFieldValue", (object) ncdcssWeeklyMetric.DataFieldValue);
      command.Parameters.AddWithValue("MetricName", (object) ncdcssWeeklyMetric.MetricName);
      command.Parameters.AddWithValue("Cycle", (object) ncdcssWeeklyMetric.Cycle);
      command.Parameters.AddWithValue("Target", (object) ncdcssWeeklyMetric.Target);
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
      foreach (NCDCSSWeeklyMetric metric in this.Metrics)
      {
        command.Parameters.Clear();
        command.Parameters.AddWithValue("MetricID", (object) metric.MetricID);
        if (this.Mode.ToLower().Trim() == "delete")
        {
          command.CommandText = "cms_DeleteCSSWeeklyMetric";
        }
        else
        {
          if (this.Mode.ToLower().Trim() == "add")
            command.CommandText = "cms_InsertCSSWeeklyMetric";
          else
            command.CommandText = "cms_UpdateCSSWeeklyMetric";
          command.Parameters.AddWithValue("CompanyID", (object) metric.CompanyID);
          command.Parameters.AddWithValue("DataFieldName", (object) metric.DataFieldName);
          command.Parameters.AddWithValue("DataFieldValue", (object) metric.DataFieldValue);
          command.Parameters.AddWithValue("MetricName", (object) metric.MetricName);
          command.Parameters.AddWithValue("Target", (object) metric.Target);
          command.Parameters.AddWithValue("MetricID", (object) metric.MetricID);
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
      command.CommandText = "cms_GetCSSWeeklyMetrics";
      command.Parameters.AddWithValue("CompanyID", (object) this.CompanyID);
      SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
      while (sqlDataReader.Read())
      {
        int fieldCount = sqlDataReader.FieldCount;
        NCDCSSWeeklyMetric ncdcssWeeklyMetric = new NCDCSSWeeklyMetric();
        for (int ordinal = 0; ordinal < fieldCount; ++ordinal)
        {
          string name = sqlDataReader.GetName(ordinal);
          ncdcssWeeklyMetric.GetType().GetProperty(name).SetValue((object) ncdcssWeeklyMetric, sqlDataReader[ordinal] == DBNull.Value ? (object) null : sqlDataReader[ordinal]);
        }
        this.Metrics.Add(ncdcssWeeklyMetric);
      }
      sqlDataReader.Close();
    }
  }
}
