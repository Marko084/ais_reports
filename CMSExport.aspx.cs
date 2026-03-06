using AIS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AISReports
{
    public partial class CMSExport : System.Web.UI.Page
    {
        #region
        private string queryParams;
        private string exportType;
        private string exportName;
        private string styleName;
        private StringWriter sw;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            queryParams = Request.QueryString["query"];
            exportType = Request.QueryString["extype"];
            exportName = Request.QueryString["exName"];
            styleName = Request.QueryString["sName"];

            if (exportType.ToLower() == "excel")
                ExportToExcel();
            else if (exportType.ToLower() == "pdf")
            {
                ExportToPdf();
            }
            else
            {
                if (!(exportType.ToLower() == "htmltopdf"))
                    return;

                ConvertHtmlToPdf();
            }
        }

        private string GetParamValueByName(string paramName)
        {
            char[] chArray = new char[1] { '|' };

            foreach (string str1 in queryParams.Split(chArray))
            {
                string str2 = HttpUtility.UrlDecode(str1);
                string str3 = ((IEnumerable<string>)str2.Split('~')).First<string>();

                string paramValueByName = ((IEnumerable<string>)str2.Split('~')).Last<string>();

                if (str3.ToLower().Trim() == paramName.ToLower().Trim())
                    return paramValueByName;
            }

            return "";
        }

        private string FormatData(string value)
        {
            value = value.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", "").Replace("N/A", "").Replace("sectionheader|", "").Replace("sectionfooter|", "");
            
            if (value.Contains("\""))
                value = value.Replace("\"", "'");

            if (value.Contains(","))
                value = string.Format("\"{0}\"", value);

            if (value.Contains(Environment.NewLine))
                value = value.Replace(Environment.NewLine, "");

            return Regex.Replace(value, "[^\\u0020-\\u007E]", string.Empty);
        }

        private void BuildExcelFile(SqlDataReader dr)
        {
            for (int ordinal = 0; ordinal < dr.FieldCount; ++ordinal)
            {
                if (ordinal == 0)
                    sw.Write(SplitCamelCase(dr.GetName(ordinal).ToString().Replace("_", " ")));
                else
                    sw.Write("," + SplitCamelCase(dr.GetName(ordinal).ToString().Replace("_", " ")));
            }

            sw.WriteLine();

            while (dr.Read())
            {
                bool flag = false;

                for (int ordinal = 0; ordinal < dr.FieldCount; ++ordinal)
                {
                    if (ordinal == 0 && dr[ordinal].ToString().ToLower() == "pagebreak")
                    {
                        flag = true;
                        break;
                    }

                    if (ordinal == 0)
                        sw.Write(FormatData(dr[ordinal].ToString()));
                    else
                        sw.Write("," + FormatData(dr[ordinal].ToString()));
                }

                if (!flag)
                    sw.WriteLine();
            }

            if (!dr.NextResult())
                return;

            BuildExcelFile(dr);
        }

        private string PopulateReportParameters(string parameters)
        {
            TextInfo textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
            
            bool flag = false;
            string str1 = "";
            char[] chArray = new char[1] { '|' };

            foreach (string str2 in queryParams.Split(chArray))
            {
                string str3 = HttpUtility.UrlDecode(str2);
                string str4 = ((IEnumerable<string>)str3.Split('~')).First<string>();
                string newValue = ((IEnumerable<string>)str3.Split('~')).Last<string>();

                if (parameters.ToLower().Contains("[fieldstartdate]") && str4.Trim().ToLower().EndsWith("startdate") && newValue.Trim().Length > 0)
                {
                    parameters = parameters.ToLower().Replace("[fieldstartdate]", newValue).Replace("[label]", str4.Trim().ToLower().Replace("startdate", ""));
                    flag = true;
                }
                else if (parameters.ToLower().Contains("[fieldenddate]") && str4.Trim().ToLower().EndsWith("enddate") && newValue.Trim().Length > 0)
                {
                    parameters = parameters.ToLower().Replace("[fieldenddate]", newValue);
                    flag = true;
                }
                else if (parameters.ToLower().Contains(string.Format("[{0}]", str4)))
                {
                    parameters = parameters.ToLower().Replace(string.Format("[{0}]", str4), newValue);
                    flag = true;
                }
                else if (parameters.ToLower().Contains("[parameters]") && newValue.Trim().Length > 0 && !str4.Trim().ToLower().EndsWith("startdate") && !str4.Trim().ToLower().EndsWith("enddate"))
                {
                    str1 += parameters.ToLower().Replace("[parameters]", string.Format("{0} {1}\r\n", str4, newValue));
                    flag = true;
                    parameters = str1;
                }
            }

            if (!flag)
                parameters = "";

            return Regex.Replace(textInfo.ToTitleCase(textInfo.ToLower(parameters)).Replace("Bookingagentcode", "BookingAgentCode").Replace("Haulingagentcode", "HaulingAgentCode").Replace("Surveyentered", "Survey Entered").Replace("Bookercode", "BookerCode").Replace("Haulercode", "HaulerCode"), "(\\B[A-Z])", " $1").Replace("Csr", "CSR").Replace("Scac", "SCAC").Replace("Created ", "Survey ");
        }

        private void ExportToExcel()
        {
            string paramValueByName1 = GetParamValueByName("queryName");
            string paramValueByName2 = GetParamValueByName("companyID");
            string paramValueByName3 = GetParamValueByName("userID");

            SqlDataReader sqlDataReader = DAL.ReturnDataReader(string.Format("select QueryName,ReportDescription,ReportTitle,ReportSubTitle,ReportParameters from Reports where CompanyID={0} and QueryName='{1}' and UserID in(0,{2}) ", paramValueByName2, paramValueByName1, paramValueByName3), CommandType.Text);
            
            sw = new StringWriter();

            while (sqlDataReader.Read())
            {
                sw.WriteLine(sqlDataReader["ReportDescription"].ToString());
                sw.WriteLine(PopulateReportParameters(sqlDataReader["ReportSubTitle"].ToString()));
                sw.WriteLine(PopulateReportParameters(sqlDataReader["ReportParameters"].ToString()));
                sw.WriteLine();
            }

            sqlDataReader.Close();
            sqlDataReader.Dispose();

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 120;
                command.CommandText = paramValueByName1;

                char[] chArray = new char[1] { '|' };

                foreach (string str1 in queryParams.Split(chArray))
                {
                    string str2 = HttpUtility.UrlDecode(str1);
                    string parameterName = ((IEnumerable<string>)str2.Split('~')).First<string>();
                    string str3 = ((IEnumerable<string>)str2.Split('~')).Last<string>();

                    if (str3.ToLower().Trim() != paramValueByName1.ToLower().Trim() && str3.Trim() != "")
                        command.Parameters.AddWithValue(parameterName, str3);
                }

                SqlDataReader dr = command.ExecuteReader();

                BuildExcelFile(dr);

                dr.Close();
                dr.Dispose();
            }

            string str = string.Format("attachment; filename={0}.csv", exportName);

            Response.ClearContent();
            Response.AddHeader("content-disposition", str);
            Response.ContentType = "application/ms-excel";
            Response.Write(sw.ToString());
            Response.End();
        }

        private void ExportToPdf()
        {
            string paramValueByName1 = GetParamValueByName("queryName");
            string paramValueByName2 = GetParamValueByName("userID");

            DataSet dataSet = new DataSet();

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = paramValueByName1;

                char[] chArray = new char[1] { '|' };

                foreach (string str1 in queryParams.Split(chArray))
                {
                    string str2 = HttpUtility.UrlDecode(str1);
                    string parameterName = ((IEnumerable<string>)str2.Split('~')).First<string>();
                    string str3 = ((IEnumerable<string>)str2.Split('~')).Last<string>();

                    if (str3.ToLower().Trim() != paramValueByName1.ToLower().Trim() && str3.Trim() != "")
                        command.Parameters.AddWithValue(parameterName, str3);
                }

                new SqlDataAdapter(command).Fill(dataSet);

                foreach (DataTable table in (InternalDataCollectionBase)dataSet.Tables)
                {
                    for (int index = 0; index < table.Columns.Count; ++index)
                        table.Columns[index].ColumnName = SplitCamelCase(table.Columns[index].ColumnName.Replace("_", " "));
                }
            }

            string paramValueByName3 = GetParamValueByName("companyID");

            NCDReportGenerator ncdReportGenerator = new NCDReportGenerator();

            ncdReportGenerator.ReportData = dataSet;

            SqlDataReader sqlDataReader = DAL.ReturnDataReader(string.Format("select QueryName,ReportDescription,ReportTitle,ReportSubTitle,ReportParameters from Reports where CompanyID={0} and QueryName='{1}' and UserID in(0,{2}) ", paramValueByName3, paramValueByName1, paramValueByName2), CommandType.Text);
            
            while (sqlDataReader.Read())
            {
                ncdReportGenerator.ReportTitle = sqlDataReader["ReportDescription"].ToString();
                ncdReportGenerator.ReportSubTitle = PopulateReportParameters(sqlDataReader["ReportSubTitle"].ToString());
                ncdReportGenerator.ReportParameters = PopulateReportParameters(sqlDataReader["ReportParameters"].ToString());
                ncdReportGenerator.CompanyID = paramValueByName3;
            }

            sqlDataReader.Close();
            sqlDataReader.Dispose();

            if (exportName.ToLower() == "scorecard" && paramValueByName3 != "10108")
                ncdReportGenerator.GenerateScoreCardReport();
            else if (exportName.ToLower() == "scorecard" && paramValueByName3 == "10108")
                ncdReportGenerator.GenerateScoreCardReport("green-gray-largefont");
            else if (exportName.ToLower() == "reports")
                ncdReportGenerator.GenerateStandardReport();

            string str = string.Format("attachment; filename={0}_{1}.pdf", exportName, DateTime.Now.ToString("yyyyMMddhhmmss"));

            Response.ClearContent();
            Response.AddHeader("content-disposition", str);
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(ncdReportGenerator.ReportStream.ToArray());
            Response.End();
        }

        private void ConvertHtmlToPdf()
        {
        }

        private string SplitCamelCase(string str) => Regex.Replace(Regex.Replace(str, "(\\P{Ll})(\\P{Ll}\\p{Ll})", "$1 $2"), "(\\p{Ll})(\\P{Ll})", "$1 $2");
    }
}