using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;

public class DataQuery
{
    public string Query { get; set; }

    public string FieldList { get; set; }

    public string QueryType { get; set; }

    public string QueryParamList { get; set; }

    public string FilterList { get; set; }

    public string GetGridResultJsonString()
    {
        int iTotalRecords = 0;
        string sEcho = HttpContext.Current.Request["sEcho"] ?? (HttpContext.Current.Request["draw"] ?? "1");
        List<List<string>> aaData = new List<List<string>>();

        bool useFieldList = !string.IsNullOrEmpty(this.FieldList) && this.FieldList.Trim() != "*" && this.FieldList.Trim().ToLower() != "undefined"; ;

        using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
        {
            sqlConnection.Open();
            SqlCommand command = sqlConnection.CreateCommand();
            command.CommandTimeout = 600;

            if (this.QueryType != null && this.QueryType.ToLower().Trim() == "storedproc")
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = this.Query;
                if (!string.IsNullOrEmpty(this.QueryParamList))
                {
                    foreach (string param in this.QueryParamList.Split('|'))
                    {
                        string[] parts = param.Split('~');
                        if (parts.Length == 2 && parts[1].Trim().Length > 0)
                            command.Parameters.AddWithValue(parts[0], parts[1].Trim());
                    }
                }
            }
            else if (this.QueryType != null && this.QueryType.ToLower().Trim() == "custom")
            {
                string paramNames = "";
                string paramValues = "";
                string companyId = "";
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetCustomQueryResult";
                foreach (string param in this.QueryParamList.Split('|'))
                {
                    string[] parts = param.Split('~');
                    if (parts.Length == 2)
                    {
                        if (parts[0].ToLower().Trim() == "companyid") companyId = parts[1];
                        else if (parts[1].Trim() != "")
                        {
                            paramNames += (paramNames == "" ? "" : ",") + parts[0];
                            paramValues += (paramValues == "" ? "" : ",") + parts[1];
                        }
                    }
                }
                command.Parameters.AddWithValue("CompanyID", companyId);
                command.Parameters.AddWithValue("QueryName", this.Query);
                command.Parameters.AddWithValue("ParamNames", paramNames);
                command.Parameters.AddWithValue("ParamValues", paramValues);
            }
            else
            {
                if (this.QueryType.ToLower() == "text" && string.IsNullOrEmpty(this.Query))
                {
                    return JsonConvert.SerializeObject(new { sEcho = sEcho, iTotalRecords = 0, iTotalDisplayRecords = 0, aaData = new string[0][] });
                }
                command.CommandType = CommandType.Text;
                command.CommandText = this.Query;
            }

            using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (dr.Read())
                {
                    List<string> row = new List<string> { "" }; 
                    if (useFieldList)
                    {
                        foreach (string field in this.FieldList.Split('|').Where(f => !string.IsNullOrEmpty(f) && f.Trim().ToLower() != "undefined"))
                        {
                            row.Add(this.FormatData(dr, field));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            row.Add(this.FormatData(dr, i));
                        }
                    }
                    aaData.Add(row);
                    iTotalRecords++;
                }
            }
        }

        return JsonConvert.SerializeObject(new {
            sEcho = sEcho,
            iTotalRecords = iTotalRecords,
            iTotalDisplayRecords = iTotalRecords,
            aaData = aaData
        });
    }

    public string GetResultJsonObjectString()
    {
        List<List<object>> result = new List<List<object>>();
        bool useFieldList = !string.IsNullOrEmpty(this.FieldList) && this.FieldList.Trim() != "*" && this.FieldList.Trim().ToLower() != "undefined"; ;

        using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
        {
            sqlConnection.Open();
            SqlCommand command = sqlConnection.CreateCommand();
            command.CommandTimeout = 600;

            if (this.QueryType != null && this.QueryType.ToLower().Trim() == "storedproc")
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = this.Query;
                if (!string.IsNullOrEmpty(this.QueryParamList))
                {
                    foreach (string param in this.QueryParamList.Split('|'))
                    {
                        string[] parts = param.Split('~');
                        if (parts.Length == 2 && parts[1].Trim().Length > 0)
                            command.Parameters.AddWithValue(parts[0], parts[1].Trim());
                    }
                }
            }
            else if (this.QueryType != null && this.QueryType.ToLower().Trim() == "custom")
            {
                string paramNames = "";
                string paramValues = "";
                string companyId = "";
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetCustomQueryResult";
                foreach (string param in this.QueryParamList.Split('|'))
                {
                    string[] parts = param.Split('~');
                    if (parts.Length == 2)
                    {
                        if (parts[0].ToLower().Trim() == "companyid") companyId = parts[1];
                        else if (parts[1].Trim() != "")
                        {
                            paramNames += (paramNames == "" ? "" : ",") + parts[0];
                            paramValues += (paramValues == "" ? "" : ",") + parts[1];
                        }
                    }
                }
                command.Parameters.AddWithValue("CompanyID", companyId);
                command.Parameters.AddWithValue("QueryName", this.Query);
                command.Parameters.AddWithValue("ParamNames", paramNames);
                command.Parameters.AddWithValue("ParamValues", paramValues);
            }
            else
            {
                command.CommandType = CommandType.Text;
                command.CommandText = this.Query;
            }

            using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (dr.Read())
                {
                    List<object> row = new List<object>();
                    if (useFieldList)
                    {
                        foreach (string field in this.FieldList.Split('|').Where(f => !string.IsNullOrEmpty(f) && f.Trim().ToLower() != "undefined"))
                        {
                            row.Add(this.FormatDataRaw(dr, field));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            row.Add(this.FormatDataRaw(dr, i));
                        }
                    }
                    result.Add(row);
                }
            }
        }
        return JsonConvert.SerializeObject(result);
    }

    public string GetResultJsonString()
    {
        List<List<string>> aaData = new List<List<string>>();
        bool useFieldList = !string.IsNullOrEmpty(this.FieldList) && this.FieldList.Trim() != "*" && this.FieldList.Trim().ToLower() != "undefined"; ;

        using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
        {
            sqlConnection.Open();
            SqlCommand command = sqlConnection.CreateCommand();
            command.CommandTimeout = 600;

            if (this.QueryType != null && this.QueryType.ToLower().Trim() == "storedproc")
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = this.Query;
                if (!string.IsNullOrEmpty(this.QueryParamList))
                {
                    foreach (string param in this.QueryParamList.Split('|'))
                    {
                        string[] parts = param.Split('~');
                        if (parts.Length == 2 && parts[1].Trim().Length > 0)
                            command.Parameters.AddWithValue(parts[0], parts[1].Trim());
                    }
                }
            }
            else if (this.QueryType != null && this.QueryType.ToLower().Trim() == "custom")
            {
                string paramNames = "";
                string paramValues = "";
                string companyId = "";
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetCustomQueryResult";
                foreach (string param in this.QueryParamList.Split('|'))
                {
                    string[] parts = param.Split('~');
                    if (parts.Length == 2)
                    {
                        if (parts[0].ToLower().Trim() == "companyid") companyId = parts[1];
                        else if (parts[1].Trim() != "")
                        {
                            paramNames += (paramNames == "" ? "" : ",") + parts[0];
                            paramValues += (paramValues == "" ? "" : ",") + parts[1];
                        }
                    }
                }
                command.Parameters.AddWithValue("CompanyID", companyId);
                command.Parameters.AddWithValue("QueryName", this.Query);
                command.Parameters.AddWithValue("ParamNames", paramNames);
                command.Parameters.AddWithValue("ParamValues", paramValues);
            }
            else
            {
                command.CommandType = CommandType.Text;
                command.CommandText = this.Query;
            }

            using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (dr.Read())
                {
                    List<string> row = new List<string>();
                    if (useFieldList)
                    {
                        foreach (string field in this.FieldList.Split('|').Where(f => !string.IsNullOrEmpty(f) && f.Trim().ToLower() != "undefined"))
                        {
                            row.Add(this.FormatData(dr, field));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            row.Add(this.FormatData(dr, i));
                        }
                    }
                    aaData.Add(row);
                }
            }
        }
        return JsonConvert.SerializeObject(aaData);
    }

    private string FormatData(SqlDataReader dr, string field)
    {
        try
        {
            object val = dr[field];
            if (val == null || val == DBNull.Value) return "";
            string str = val.ToString();

        if (field.ToLower().EndsWith("date") && !string.IsNullOrEmpty(str))
        {
            if (DateTime.TryParse(str, out DateTime dt))
            {
                if (dt.Year == 1900 || dt.Year == 1971 || (dt.Month == 12 && dt.Day == 30 && dt.Year == 1899))
                    return "";
                return dt.ToString("MM/dd/yyyy");
            }
        }

        str = str.Replace("\"", "'")
                 .Replace("\\", "&#92;")
                 .Replace("ÿ", "");
        
        return Regex.Replace(str, "[^\\u0020-\\u007E]", string.Empty);
        }
        catch { return ""; }
    }

    private string FormatData(SqlDataReader dr, int fieldIndex)
    {
        object val = dr[fieldIndex];
        if (val == null || val == DBNull.Value) return "";
        string str = val.ToString();

        if (dr.GetFieldType(fieldIndex).Name == "DateTime" && !string.IsNullOrEmpty(str))
        {
            if (DateTime.TryParse(str, out DateTime dt))
            {
                if (dt.Year == 1900 || dt.Year == 1971 || (dt.Month == 12 && dt.Day == 30 && dt.Year == 1899))
                    return "";
                return dt.ToString("MM/dd/yyyy");
            }
        }

        str = str.Replace("\"", "'")
                 .Replace("\\", "&#92;")
                 .Replace("ÿ", "");
        
        return Regex.Replace(str, "[^\\u0020-\\u007E]", string.Empty);
    }

    private object FormatDataRaw(SqlDataReader dr, string field)
    {
        object val = dr[field];
        if (val == null || val == DBNull.Value) return null;
        if (this.IsNumeric(val.ToString())) return val;
        return this.FormatData(dr, field);
    }

    private object FormatDataRaw(SqlDataReader dr, int index)
    {
        object val = dr[index];
        if (val == null || val == DBNull.Value) return null;
        if (this.IsNumeric(val.ToString())) return val;
        return this.FormatData(dr, index);
    }

    private bool IsNumeric(string value) => int.TryParse(value, out int _) || float.TryParse(value, out float _) || Decimal.TryParse(value, out Decimal _);
}
