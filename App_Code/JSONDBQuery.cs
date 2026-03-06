using AIS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

public class JSONDBQuery
{
  public string FieldList { get; set; }

  public string TableName { get; set; }

  public string KeyFieldName { get; set; }

  public string KeyFieldValue { get; set; }

  public string QueryType { get; set; }

  public string KeyFieldIsIdentity { get; set; }

  public List<string> FieldListArray { get; set; }

  public string UserID { get; set; }

  private bool UseEncoding(string fList)
  {
    return fList != null && fList.StartsWith("ENC|");
  }

  private string DecodeValue(string val, bool useEnc)
  {
    if (useEnc) return HttpUtility.UrlDecode(val);
    return val;
  }

  private string EncodeValue(object val, bool useEnc)
  {
    string sVal = val == null ? "" : val.ToString();
    if (useEnc) return HttpUtility.UrlEncode(sVal);
    return sVal;
  }

  public JSONDBQuery Add()
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    int num = 0;
    object obj = (object) 0;
    if (this.KeyFieldIsIdentity == null)
      this.KeyFieldIsIdentity = "false";
    stringBuilder1.AppendFormat("INSERT INTO {0} (", (object) this.TableName);
    stringBuilder2.Append("VALUES (");
    
    bool useEnc = UseEncoding(this.FieldList);
    string fList = useEnc ? this.FieldList.Substring(4) : this.FieldList;

    try
    {
      using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
      {
        sqlConnection.Open();
        SqlCommand command = sqlConnection.CreateCommand();
        command.CommandType = CommandType.Text;
        foreach (string str1 in ((IEnumerable<string>) fList.Split('|')).ToList<string>().Where<string>((Func<string, bool>) (f => f != "")))
        {
          string[] parts = str1.Split('~');
          string str2 = parts[0];
          string str3 = DecodeValue(parts.Length > 1 ? parts[1] : "", useEnc);
          
          if (str2.ToLower().Trim() == "companyid" && str3 != "0" && str3 != "" || str2.ToLower().Trim() != "companyid")
          {
            if (num == 0)
            {
              stringBuilder1.AppendFormat("[{0}]", (object) str2);
              stringBuilder2.AppendFormat("@{0}", (object) str2.Replace(" ", "").Replace("/", "").Replace("?", ""));
            }
            else
            {
              stringBuilder1.AppendFormat(",{0}", (object) str2);
              stringBuilder2.AppendFormat(",@{0}", (object) str2.Replace(" ", "").Replace("/", "").Replace("?", ""));
            }
            command.Parameters.AddWithValue(str2.Replace(" ", "").Replace("/", "").Replace("?", ""), (object) str3);
            ++num;
          }
          if (this.KeyFieldName.ToLower() == str2.ToLower())
            this.KeyFieldName = "";
        }
        if (this.KeyFieldName != "" && this.KeyFieldIsIdentity == "false" && this.KeyFieldValue != null && this.KeyFieldValue != "")
        {
          stringBuilder1.AppendFormat(",{0}", (object) this.KeyFieldName);
          stringBuilder2.AppendFormat(",@{0}", (object) this.KeyFieldName);
          command.Parameters.AddWithValue(this.KeyFieldName, (object) this.KeyFieldValue);
        }
        stringBuilder1.Append(") ");
        stringBuilder2.Append(") ");
        stringBuilder1.Append(stringBuilder2.ToString());
        stringBuilder1.AppendLine(" select SCOPE_IDENTITY()");
        command.CommandText = stringBuilder1.ToString();
        obj = command.ExecuteScalar();
      }
    }
    catch (Exception ex)
    {
      DAL.ReturnScalarValue("InsertExceptionLog", "CompanyID,Message,StackTrace,InnerException,Source", "77777,JSONDBQuery: " + ex.ToString() + "," + ex.StackTrace + "," + (ex.InnerException != null ? ex.InnerException.ToString() : "") + "," + ex.Source);
    }
    return new JSONDBQuery()
    {
      TableName = this.TableName,
      QueryType = this.QueryType,
      KeyFieldName = this.KeyFieldName,
      KeyFieldValue = obj == null ? "0" : obj.ToString(),
      FieldList = ""
    };
  }

  public void Update()
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    int num = 0;
    List<string> stringList = new List<string>();
    bool flag = false;

    bool useEnc = UseEncoding(this.FieldList);
    string fList = useEnc ? this.FieldList.Substring(4) : this.FieldList;

    if (this.TableName.Trim().ToLower() == "vw_cartwright_ratingtracker")
    {
      stringBuilder1.Append("UPDATE Cartwright_SurveyResults ");
      stringBuilder1.AppendFormat("SET FirstName='{0}',", (object) this.GetFieldValue("FirstName"));
      stringBuilder1.AppendFormat("LastName='{0}',", (object) this.GetFieldValue("LastName"));
      stringBuilder1.AppendFormat("DeliveryDate='{0}',", (object) this.GetFieldValue("DeliveryDate"));
      stringBuilder1.AppendFormat("HomePhoneNumber='{0}',", (object) this.GetFieldValue("HomePhoneNumber"));
      stringBuilder1.AppendFormat("CellPhoneNumber='{0}',", (object) this.GetFieldValue("CellPhoneNumber"));
      stringBuilder1.AppendFormat("SCAC='{0}',", (object) this.GetFieldValue("SCAC"));
      stringBuilder1.AppendFormat("Email='{0}',", (object) this.GetFieldValue("Email"));
      stringBuilder1.AppendFormat("OriginAgent='{0}',", (object) this.GetFieldValue("OriginAgent"));
      stringBuilder1.AppendFormat("CoordinatorQuestion='{0}',", (object) this.GetFieldValue("CoordinatorScore"));
      stringBuilder1.AppendFormat("DestinationAgent='{0}' ", (object) this.GetFieldValue("DestinationAgent"));
      stringBuilder1.AppendFormat("WHERE GBL='{0}' ", (object) this.KeyFieldValue);
      stringBuilder1.AppendLine("DECLARE @ExistingRecordCount int ");
      stringBuilder1.AppendFormat("select @ExistingRecordCount=count(*) from Cartwright_CSSSurveyResults where GBL='{0}' ", (object) this.KeyFieldValue);
      stringBuilder1.AppendLine("if @ExistingRecordCount>0 ");
      stringBuilder1.AppendLine("begin ");
      stringBuilder1.Append("UPDATE Cartwright_CSSSurveyResults ");
      stringBuilder1.AppendFormat("SET Status='{0}',", (object) this.GetFieldValue("Status"));
      stringBuilder1.AppendFormat("SCAC='{0}' ", (object) this.GetFieldValue("SCAC"));
      stringBuilder1.AppendFormat("WHERE GBL='{0}' ", (object) this.KeyFieldValue);
      stringBuilder1.AppendLine("end ");
      stringBuilder1.AppendLine("else ");
      stringBuilder1.AppendLine("begin ");
      stringBuilder1.AppendFormat("insert into Cartwright_CSSSurveyResults (GBL,Status) values('{0}','{1}') ", (object) this.KeyFieldValue, (object) this.GetFieldValue("Status"));
      stringBuilder1.AppendLine("end ");
      using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
      {
        sqlConnection.Open();
        SqlCommand command = sqlConnection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = stringBuilder1.ToString();
        command.ExecuteNonQuery();
      }
    }
    else
    {
      if (this.TableName.Trim().ToLower().StartsWith("vw_"))
      {
        string str1 = ((IEnumerable<string>) this.TableName.Split('_')).Last<string>();
        string str2 = ((IEnumerable<string>) (((IEnumerable<string>) fList.ToLower().Split('|')).ToList<string>().Find((Predicate<string>) (f => f.Contains("companycode"))) ?? this.TableName.Replace("_" + str1, "").Replace("vw_", "")).Split('~')).Last<string>();
        if (str2 == "nw" || str2 == "demo")
          this.TableName = str1;
        else if (str2.ToLower() == "nw_consolidated")
        {
          object obj = DAL.ReturnScalarValue(string.Format("select companycode from vw_nw_consolidated_surveyresults where cscno='{0}' ", (object) this.KeyFieldValue), true);
          this.TableName = !(obj.ToString().ToLower() == "nw") ? obj.ToString() + "_" + str1 : str1;
        }
        else
          this.TableName = str2 + "_" + str1;
        flag = true;
      }
      StringBuilder stringBuilder2 = new StringBuilder();
      stringBuilder2.Append("select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS inner join sys.columns sc on sc.name=COLUMN_NAME ");
      stringBuilder2.AppendFormat("where object_id in(select object_id from sys.objects where name='{0}') ", (object) this.TableName);
      stringBuilder2.AppendFormat("and TABLE_NAME='{0}' and sc.is_computed<>1 and sc.is_identity<>1 ", (object) this.TableName);
      SqlDataReader sqlDataReader = DAL.ReturnDataReader(stringBuilder2.ToString(), CommandType.Text);
      while (sqlDataReader.Read())
        stringList.Add(sqlDataReader["COLUMN_NAME"].ToString().ToLower());
      sqlDataReader.Close();
      sqlDataReader.Dispose();
      stringBuilder1.AppendFormat("UPDATE {0} SET ", (object) this.TableName);
      using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
      {
        sqlConnection.Open();
        SqlCommand command = sqlConnection.CreateCommand();
        command.CommandType = CommandType.Text;
        foreach (string str3 in ((IEnumerable<string>) fList.Split('|')).ToList<string>().Where<string>((Func<string, bool>) (f => f != "")))
        {
          string[] parts = str3.Split('~');
          string parameterName1 = parts[0];
          string str4 = DecodeValue(parts.Length > 1 ? parts[1] : "", useEnc);
          if (flag)
          {
            string parameterName2 = parameterName1.Replace(" ", "").Replace("/", "").Replace("?", "").Trim();
            if (stringList.Contains(parameterName2.Trim().ToLower()))
            {
              if (num == 0)
                stringBuilder1.AppendFormat("{0}=@{0}", (object) parameterName2);
              else
                stringBuilder1.AppendFormat(",{0}=@{0}", (object) parameterName2);
              command.Parameters.AddWithValue(parameterName2, (object) str4);
              ++num;
            }
          }
          else if (stringList.Contains(parameterName1.Trim().ToLower()))
          {
            if (num == 0)
              stringBuilder1.AppendFormat("{0}=@{0}", (object) parameterName1);
            else
              stringBuilder1.AppendFormat(",{0}=@{0}", (object) parameterName1);
            command.Parameters.AddWithValue(parameterName1, (object) str4);
            ++num;
          }
        }
        if (this.KeyFieldName != "")
        {
          stringBuilder1.AppendFormat(" WHERE {0}=@{1} ", (object) this.KeyFieldName, (object) this.KeyFieldName);
          if (!command.Parameters.Contains(this.KeyFieldName))
            command.Parameters.AddWithValue(this.KeyFieldName, (object) this.KeyFieldValue);
        }
        command.CommandText = stringBuilder1.ToString();
        command.ExecuteNonQuery();
      }
    }
  }

  public void Delete()
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    string fieldList = this.FieldList;
    try
    {
      using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
      {
        sqlConnection.Open();
        SqlCommand command = sqlConnection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = string.Format("select * from {0} where {1}='{2}' for xml path ", (object) this.TableName, (object) this.KeyFieldName, (object) this.KeyFieldValue);
        SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
        while (sqlDataReader.Read())
          fieldList = sqlDataReader[0].ToString();
        sqlDataReader.Close();
      }
    }
    catch
    {
    }

    bool useEnc = UseEncoding(this.FieldList);
    string fList = useEnc ? this.FieldList.Substring(4) : this.FieldList;

    if (this.TableName.Trim().ToLower().StartsWith("vw_"))
    {
      string str1 = ((IEnumerable<string>) this.TableName.Split('_')).Last<string>();
      string str2 = ((IEnumerable<string>) (((IEnumerable<string>) fList.ToLower().Split('|')).ToList<string>().Find((Predicate<string>) (f => f.Contains("companycode"))) ?? this.TableName.Replace("_" + str1, "").Replace("vw_", "")).Split('~')).Last<string>();
      if (str2 == "nw" || str2 == "demo")
        this.TableName = str1;
      else if (str2.ToLower() == "nw_consolidated")
      {
        object obj = DAL.ReturnScalarValue(string.Format("select companycode from vw_nw_consolidated_surveyresults where cscno='{0}' ", (object) this.KeyFieldValue), true);
        this.TableName = !(obj.ToString().ToLower() == "nw") ? obj.ToString() + "_" + str1 : str1;
      }
      else
        this.TableName = str2 + "_" + str1;
    }
    stringBuilder1.AppendFormat("DELETE FROM {0} ", (object) this.TableName);
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.Text;
      stringBuilder1.AppendFormat(" WHERE {0}='{1}' ", (object) this.KeyFieldName, (object) this.KeyFieldValue);
      command.CommandText = stringBuilder1.ToString();
      command.ExecuteNonQuery();
    }
    StringBuilder stringBuilder2 = new StringBuilder();
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.Text;
      stringBuilder2.AppendLine("INSERT INTO cms_DeleteLog(UserID,TableName,KeyFieldName,KeyFieldValue,FieldList) ");
      stringBuilder2.AppendLine("values(@UserID,@TableName,@KeyFieldName,@KeyFieldValue,@FieldList)");
      command.CommandText = stringBuilder2.ToString();
      command.Parameters.AddWithValue("TableName", (object) this.TableName);
      command.Parameters.AddWithValue("KeyFieldName", (object) this.KeyFieldName);
      command.Parameters.AddWithValue("KeyFieldValue", (object) this.KeyFieldValue);
      command.Parameters.AddWithValue("FieldList", (object) fieldList);
      if (this.UserID != null && this.UserID != "")
        command.Parameters.AddWithValue("UserID", (object) this.UserID);
      else
        command.Parameters.AddWithValue("UserID", (object) 0);
      command.ExecuteNonQuery();
    }
  }

  public JSONDBQuery Get()
  {
    StringBuilder stringBuilder = new StringBuilder();
    JSONDBQuery jsondbQuery = new JSONDBQuery()
    {
      TableName = this.TableName,
      QueryType = this.QueryType,
      KeyFieldName = this.KeyFieldName,
      KeyFieldValue = this.KeyFieldValue,
      FieldList = ""
    };

    bool useEnc = UseEncoding(this.FieldList);
    if (useEnc) jsondbQuery.FieldList = "ENC|";

    stringBuilder.Append("SELECT ");
    string fList = useEnc ? this.FieldList.Substring(4) : this.FieldList;
    if (string.IsNullOrEmpty(fList)) return jsondbQuery;
    char[] chArray1 = new char[1]{ '|' };
    foreach (string str1 in fList.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ '~' };
      string str2 = ((IEnumerable<string>) str1.Split(chArray2)).First<string>();
      if (stringBuilder.ToString().Trim().ToUpper() == "SELECT")
        stringBuilder.AppendFormat("[{0}] ", (object) str2);
      else
        stringBuilder.AppendFormat(",[{0}] ", (object) str2);
    }
    stringBuilder.AppendFormat("FROM {0} ", (object) this.TableName);
    stringBuilder.AppendFormat("WHERE {0}='{1}' ", (object) this.KeyFieldName, (object) this.KeyFieldValue);
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.Text;
      command.CommandText = stringBuilder.ToString();
      SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
      while (sqlDataReader.Read())
      {
        for (int ordinal = 0; ordinal < sqlDataReader.FieldCount; ++ordinal)
        {
          string fName = sqlDataReader.GetName(ordinal);
          string fValue = EncodeValue(sqlDataReader[ordinal], useEnc);

          if (jsondbQuery.FieldList.Length == 0 || jsondbQuery.FieldList == "ENC|")
            jsondbQuery.FieldList += string.Format("{0}~{1}", (object) fName, fValue);
          else
            jsondbQuery.FieldList += string.Format("|{0}~{1}", (object) fName, fValue);
        }
        if (!this.TableName.ToLower().Trim().EndsWith("approval"))
          break;
      }
      sqlDataReader.Close();
      sqlDataReader.Dispose();
    }
    return jsondbQuery;
  }

  public void AddList()
  {
    foreach (string fListWithEnc in this.FieldListArray)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = new StringBuilder();
      int num = 0;
      if (this.KeyFieldIsIdentity == null)
        this.KeyFieldIsIdentity = "false";
      stringBuilder1.AppendFormat("INSERT INTO {0} (", (object) this.TableName);
      stringBuilder2.Append("VALUES (");
      
      bool useEnc = UseEncoding(fListWithEnc);
      string fList = useEnc ? fListWithEnc.Substring(4) : fListWithEnc;

      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
        {
          sqlConnection.Open();
          SqlCommand command = sqlConnection.CreateCommand();
          command.CommandType = CommandType.Text;
          foreach (string str1 in ((IEnumerable<string>) fList.Split('|')).ToList<string>().Where<string>((Func<string, bool>) (f => f != "")))
          {
            string[] parts = str1.Split('~');
            string str2 = parts[0];
            string str3 = DecodeValue(parts.Length > 1 ? parts[1] : "", useEnc);

            if (str2.ToLower().Trim() == "companyid" && str3 != "0" && str3 != "" || str2.ToLower().Trim() != "companyid")
            {
              if (num == 0)
              {
                stringBuilder1.AppendFormat("[{0}]", (object) str2);
                stringBuilder2.AppendFormat("@{0}", (object) str2.Replace(" ", "").Replace("/", "").Replace("?", ""));
              }
              else
              {
                stringBuilder1.AppendFormat(",{0}", (object) str2);
                stringBuilder2.AppendFormat(",@{0}", (object) str2.Replace(" ", "").Replace("/", "").Replace("?", ""));
              }
              command.Parameters.AddWithValue(str2.Replace(" ", "").Replace("/", "").Replace("?", ""), (object) str3);
              ++num;
            }
          }
          if (this.KeyFieldName != "" && this.KeyFieldIsIdentity == "false" && this.KeyFieldValue != null && this.KeyFieldValue != "")
          {
            stringBuilder1.AppendFormat(",{0}", (object) this.KeyFieldName);
            stringBuilder2.AppendFormat(",@{0}", (object) this.KeyFieldName);
            command.Parameters.AddWithValue(this.KeyFieldName, (object) this.KeyFieldValue);
          }
          stringBuilder1.Append(") ");
          stringBuilder2.Append(") ");
          stringBuilder1.Append(stringBuilder2.ToString());
          stringBuilder1.AppendLine(" select SCOPE_IDENTITY()");
          command.CommandText = stringBuilder1.ToString();
          command.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        DAL.ReturnScalarValue("InsertExceptionLog", "CompanyID,Message,StackTrace,InnerException,Source", "77777,JSONDBQuery: " + ex.ToString() + "," + ex.StackTrace + "," + (ex.InnerException != null ? ex.InnerException.ToString() : "") + "," + ex.Source);
      }
    }
  }

  private string GetFieldValue(string fieldName) 
  {
      bool useEnc = UseEncoding(this.FieldList);
      string fList = useEnc ? this.FieldList.Substring(4) : this.FieldList;
      string match = ((IEnumerable<string>) fList.Split('|')).ToList<string>().Find((Predicate<string>) (f => f.Contains(fieldName)));
      if (match == null) return "";
      return DecodeValue(match.Split('~')[1], useEnc);
  }
}
