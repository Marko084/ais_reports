<%@ WebHandler Language="C#" Class="ListHandler" %>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.Text;
using System.Web;
using Newtonsoft.Json;

public class ListHandler : IHttpHandler {

    public void ProcessRequest (HttpContext context) {

        string queryType = context.Request["qt"];

        if (queryType.ToLower() == "oldlookup")
            LookupList(context);
        if (queryType.ToLower() == "lookup")
            GetLookupList(context);
        else if (queryType.ToLower() == "storedproc" ||
                 queryType.ToLower() == "text" ||
                 queryType.ToLower() == "custom")
            GetGridDataResult(context);
        else if (queryType.ToLower() == "savedquery")
            GetSavedQuery(context);
        else if (queryType.ToLower() == "pick")
            GetPickList(context);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

    private void GetGridDataResult(HttpContext context)
    {
        string queryParamList = context.Request.QueryString["pl"];
        string queryName = context.Request.QueryString["qn"];
        string queryType = context.Request["qt"];
        string filterList = context.Request["ftr"];
        string fieldList = context.Request["fld"];
        string tableList = context.Request["tl"];
        string controlType = context.Request["ct"];
        string gridType = context.Request["gt"];
        string dynamicQuery = context.Request["dq"];

        try
        {
            DataQuery dQuery = new DataQuery();

            if(queryParamList ==null)
                queryParamList="";

            dQuery.QueryParamList = queryParamList;

            if (context.Request["query"] != null)
                dQuery.Query = context.Request["query"].ToString();
            else
                dQuery.Query = queryName;

            dQuery.QueryType =queryType;

            if (dQuery.Query != null)
            {
                var evaluateQuery = dQuery.Query.ToLower();

                if (evaluateQuery.Contains("schema.") ||
                    (evaluateQuery.Contains("login") && !evaluateQuery.Contains("userloginhistory")) ||
                    evaluateQuery.Contains("[user]"))
                    context.Response.Redirect("Login.aspx"); // throw new UnauthorizedAccessException();
            }

            if (gridType == "blog")
                fieldList = "NbrOfResponses|Detail|Add|Comments|" + fieldList;

            dQuery.FieldList = fieldList;
            dQuery.FilterList = filterList;

            if(dQuery.Query.ToLower()=="select * from cms_scaccodes")
            {
                dQuery.Query = "select ImportID,CompanyID,SCAC,Threshold from cms_scaccodes";
            }

            context.Response.Clear();
            context.Response.ContentType = "application/json";

            if(controlType !=null && controlType.ToLower()=="grid")
                context.Response.Write(dQuery.GetGridResultJsonString());
            else if(controlType=="clist")
                context.Response.Write(dQuery.GetResultJsonObjectString());
            else
                context.Response.Write(dQuery.GetResultJsonString());
        }
        catch (Exception ex)
        {
            AIS.DAL.ReturnScalarValue("InsertExceptionLog", "CompanyID,Message,StackTrace,InnerException,Source", "77777" + "," + ex.ToString() + "," + ex.StackTrace + "," + (ex.InnerException != null ? ex.InnerException.ToString() : "") + "," + ex.Source);
            context.Response.Write("{"+String.Format("\"error\":\"{0}\"", ex.ToString())+"}");
        }

    }

    private void GetLookupList(HttpContext context)
    {
        string companyID = context.Request["cid"];
        string fieldName = context.Request["fn"];
        string tableName = context.Request["tn"];
        string searchTerm = context.Request["term"];
        string userID = context.Request["uid"];
        string consolidatedTF = context.Request["consolidated"];
        string reportType = context.Request["rt"];
        string tabName = context.Request["tabname"];

        context.Response.Clear();
        context.Response.ContentType = "application/json";

        LookupItemCollection itemCol = new LookupItemCollection();

        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
        {
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetLookupList";
            cmd.Parameters.AddWithValue("CompanyID", companyID);
            cmd.Parameters.AddWithValue("UserID", userID);
            cmd.Parameters.AddWithValue("FieldName", fieldName);

            if (consolidatedTF != null && consolidatedTF != "")
                cmd.Parameters.AddWithValue("ConsolidatedTF", consolidatedTF);

            if(searchTerm !=null)
                cmd.Parameters.AddWithValue("Prefix", searchTerm);

            if(tableName !=null)
                cmd.Parameters.AddWithValue("TableName", tableName);

            if (reportType != null)
                cmd.Parameters.AddWithValue("ReportID", reportType);

            if (tabName != null && tabName != "")
                cmd.Parameters.AddWithValue("TabName",tabName);

            if (tableName != "undefined")
            {
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    itemCol.Add(new LookupItem { label = dr["Value"].ToString(), value = dr["Value"].ToString() });
                }

                dr.Dispose();
            }
        }

        string json = JsonConvert.SerializeObject(itemCol);
        json = json.Substring(0, json.Length - 1).Replace("{\"LookupList\":", "");

        context.Response.Write(json);

    }

    private void GetPickList(HttpContext context)
    {
        string companyID = context.Request["cid"];
        string fieldName = context.Request["fn"];
        string tableName = context.Request["tn"];
        string searchTerm = context.Request["term"];
        string returnType = context.Request["rt"];
        string searchFieldName = context.Request["sfn"];
        string searchFieldValue = context.Request["sfv"];
        string userID = context.Request["uid"];
        string usePickList = context.Request["pl"];

        context.Response.Clear();
        context.Response.ContentType = "application/json";

        LookupItemCollection itemCol = new LookupItemCollection();

        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
        {
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetPickList";
            cmd.Parameters.AddWithValue("CompanyID", companyID);
            cmd.Parameters.AddWithValue("UserID", userID);
            cmd.Parameters.AddWithValue("FieldName", fieldName);

            if(searchFieldName !=null)
                cmd.Parameters.AddWithValue("SearchFieldName", searchFieldName);

            if (searchFieldValue != null)
                cmd.Parameters.AddWithValue("SearchFieldValue", searchFieldValue);

            if (returnType != null)
                cmd.Parameters.AddWithValue("ReturnType", returnType);

            if (searchTerm != null)
                cmd.Parameters.AddWithValue("Prefix", searchTerm);

            if (tableName != null)
                cmd.Parameters.AddWithValue("TableName", tableName);

            if (usePickList != null)
                cmd.Parameters.AddWithValue("UsePickList", 1);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                itemCol.Add(new LookupItem { label = dr["Value"].ToString(), value = dr["Value"].ToString() });
            }

            dr.Dispose();
        }

        string json = JsonConvert.SerializeObject(itemCol);
        json = json.Substring(0, json.Length - 1).Replace("{\"LookupList\":", "");

        context.Response.Write(json);

    }

    private void LookupList(HttpContext context)
    {
        string companyID = context.Request["cid"];
        string fieldName = context.Request["fn"];
        string tableName = context.Request["tn"];
        string searchTerm = context.Request["term"];
        string userID = context.Request["uid"];

        context.Response.Clear();
        context.Response.ContentType = "application/json";

        LookupItemCollection itemCol = new LookupItemCollection();

        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
        {
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetLookupList";
            cmd.Parameters.AddWithValue("CompanyID", companyID);
            cmd.Parameters.AddWithValue("UserID", userID);
            cmd.Parameters.AddWithValue("FieldName", fieldName);

            if (searchTerm != null)
                cmd.Parameters.AddWithValue("Prefix", searchTerm);

            if (tableName != null)
                cmd.Parameters.AddWithValue("TableName", tableName);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                itemCol.Add(new LookupItem { label = dr["Value"].ToString(), value = dr["Value"].ToString() });
            }

            dr.Dispose();
        }

        string json = JsonConvert.SerializeObject(itemCol);
        json = json.Substring(0, json.Length - 1).Replace("{\"LookupList\":", "");

        context.Response.Write(json);

    }

    private void GetSavedQuery(HttpContext context)
    {
        string companyID = context.Request["cid"];
        string queryName = context.Request["qn"];
        string userID = context.Request["uid"];
        string tabName = context.Request["tabname"];
        StringBuilder sbQuery = new StringBuilder();

        context.Response.Clear();
        context.Response.ContentType = "application/json";

        AdvancedQueryCollection itemCol = new AdvancedQueryCollection();

        sbQuery.Append("SELECT pkSavedQueryID,QueryName,QueryFields,QueryTables,QueryWhere ");
        sbQuery.AppendLine("FROM cms_SavedQueries ");
        sbQuery.AppendLine("WHERE CompanyID=@CompanyID AND (UserID=0 OR UserID=@UserID) ");

        if (queryName != null && queryName != "")
            sbQuery.Append(" AND QueryName=@QueryName ");

        if (tabName != null && tabName != "")
            sbQuery.Append("AND TabName=@TabName");

        sbQuery.AppendLine(" ORDER BY QueryName ");

        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
        {
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sbQuery.ToString();
            cmd.Parameters.AddWithValue("CompanyID", companyID);
            cmd.Parameters.AddWithValue("UserID", userID);

            if(queryName !=null && queryName !="")
                cmd.Parameters.AddWithValue("QueryName", queryName);

            if (tabName != null && tabName != "")
                cmd.Parameters.AddWithValue("TabName",tabName);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                itemCol.Add(new AdvancedQuery
                {
                    queryfields = dr["QueryFields"].ToString(),
                    querytables=dr["QueryTables"].ToString(),
                    querywhere = dr["QueryWhere"].ToString(),
                    queryid=dr["pkSavedQueryID"].ToString(),
                    queryname=dr["QueryName"].ToString()
                });
            }

            dr.Dispose();
        }

        //context.Response.Write(sbQuery.ToString());
        string json = JsonConvert.SerializeObject(itemCol);

        if(json.Length>2)
            json = json.Substring(0, json.Length - 1).Replace("{\"AdvancedQueryList\":", "").Replace("{\"SavedQueryList\":","");

        context.Response.Write(json);

    }

}

[DataContract]
public class LookupItemCollection
{
    [DataMember]
    public List<LookupItem> LookupList { get; set; }

    public void Add(LookupItem lookupItem)
    {
        if (LookupList == null)
            LookupList = new List<LookupItem>();

        LookupList.Add(lookupItem);
    }
}

[DataContract]
public class LookupItem
{
    [DataMember]
    public string label {get; set;}
    [DataMember]
    public string value {get; set;}

}

[DataContract]
public class AdvancedQuery
{
    [DataMember]
    public string queryid { get; set; }
    [DataMember]
    public string queryname { get; set; }
    [DataMember]
    public string queryfields { get; set; }
    [DataMember]
    public string querytables { get; set; }
    [DataMember]
    public string querywhere { get; set; }
}

[DataContract]
public class AdvancedQueryCollection
{
    [DataMember]
    public List<AdvancedQuery> SavedQueryList { get; set; }

    public void Add(AdvancedQuery item)
    {
        if (SavedQueryList == null)
            SavedQueryList = new List<AdvancedQuery>();

        SavedQueryList.Add(item);
    }
}