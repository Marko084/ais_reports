using AISReports.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AISReports.UserControls
{
    public partial class SearchFilter : NCDUserControlBase
    {
        #region Variables
        private string companyID;
        private string pageName;
        private string selectedTableName;
        private bool defaultReportExistsTF;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            companyID = Request.QueryString["cid"];
            selectedTableName = Request.QueryString["t"];

            pageName = Path.GetFileName(Request.PhysicalPath);

            LoadChartSettings();
            LoadFilterList();

            if (!Page.IsPostBack && defaultReportExistsTF)
                txtDisplayDefaultReport.Text = "True";
            else
                txtDisplayDefaultReport.Text = "False";
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (PageUserControlID == 0)
                return;

            LoadChartSettings();
        }

        protected void btnPostBackSearch_Click(object sender, EventArgs e)
        {
        }

        private void LoadChartSettings()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "cms_GetChartSettings";
                command.Parameters.AddWithValue("PageUserControlID", PageUserControlID);
                SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (sqlDataReader.Read())
                {
                    if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "usepostback")
                        btnPostBackSearch.Attributes.Add("use-postback-search", sqlDataReader["SettingValue"].ToString().ToLower());
                }
            }

            string fileName = Path.GetFileName(Request.PhysicalPath);

            if (!(fileName == "ReportingArea.aspx") && !fileName.ToLower().Contains("scorecard") || !Request.RawUrl.ToLower().Contains("/site/"))
                return;

            btnPostBackSearch.Attributes.Add("use-postback-search", "true");
        }

        private void LoadFilterList()
        {
            List<string> stringList = new List<string>();

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "cms_GetSearchFilterFields";
                command.Parameters.AddWithValue("CompanyID", (object)companyID);
                command.Parameters.AddWithValue("PageName", (object)pageName);
                command.Parameters.AddWithValue("UserID", (object)UserID);

                if (!String.IsNullOrEmpty(selectedTableName))
                {
                    command.Parameters.AddWithValue("TableType", selectedTableName);
                }

                SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (sqlDataReader.Read())
                {
                    string str1 = "";
                    string str2 = ((IEnumerable<string>)sqlDataReader["DefaultValue"].ToString().Trim().ToLower().Split('~')).First<string>();

                    if (((IEnumerable<string>)sqlDataReader["DefaultValue"].ToString().Trim().ToLower().Split('~')).Count<string>() > 1)
                        str1 = ((IEnumerable<string>)sqlDataReader["DefaultValue"].ToString().Trim().ToLower().Split('~')).Last<string>();

                    HtmlGenericControl child1 = new HtmlGenericControl("span");
                    TextBox child2 = new TextBox();
                    HtmlGenericControl child3 = new HtmlGenericControl("input");

                    child1.InnerText = sqlDataReader["FieldDescription"].ToString() != "" ? sqlDataReader["FieldDescription"].ToString() : sqlDataReader["FieldName"].ToString();
                    
                    if (sqlDataReader["FieldType"].ToString().ToLower() == "true/false")
                    {
                        phFilterControls.Controls.Add((Control)new HtmlGenericControl("div"));
                        child1.Attributes.Add("class", "truefalse-checkbox");
                    }
                    
                    phFilterControls.Controls.Add((Control)child1);

                    child2.Attributes.Add("data-table-type", sqlDataReader["TableType"].ToString());
                    
                    if (!Convert.ToBoolean(sqlDataReader["VisibleTF"]))
                    {
                        child2.Attributes.Add("report-param-show", "hide");
                        child3.Attributes.Add("report-param-show", "hide");
                    }
                    else
                    {
                        child2.Attributes.Add("report-param-show", sqlDataReader["ReportToDisplay"].ToString() != "" ? sqlDataReader["ReportToDisplay"].ToString() : "none");
                        child3.Attributes.Add("report-param-show", sqlDataReader["ReportToDisplay"].ToString() != "" ? sqlDataReader["ReportToDisplay"].ToString() : "none");
                    }
                    
                    if (sqlDataReader["ReportToDisplay"].ToString() != "")
                    {
                        child2.Attributes.Add("style", "display:none;");
                        child3.Attributes.Add("style", "display:none;");
                        child1.Attributes.Add("style", "display:none;");
                    }
                    
                    if (!Convert.ToBoolean(sqlDataReader["EnabledTF"]))
                        child2.Enabled = false;
                    
                    if (!Convert.ToBoolean(sqlDataReader["VisibleTF"]))
                    {
                        child2.Attributes.Add("style", "display:none;");
                        child3.Attributes.Add("style", "display:none;");
                        child1.Attributes.Add("style", "display:none;");
                    }
                    
                    if (sqlDataReader["DefaultValue"].ToString() != "")
                        child2.Text = sqlDataReader["DefaultValue"].ToString();
                    
                    if (sqlDataReader["FieldType"].ToString().ToLower() == "lookup")
                    {
                        child2.CssClass = "lookup-textbox";
                        child2.ID = string.Format("txt{0}", (object)sqlDataReader["FieldName"].ToString()).ToLower();
                        child2.Attributes.Add("data-field-name", sqlDataReader["FieldName"].ToString());
                        child2.Attributes.Add("data-parameter-name", sqlDataReader["FieldName"].ToString().ToLower().Replace("(", "").Replace(")", ""));
                        
                        phFilterControls.Controls.Add((Control)child2);
                    }
                    else if (sqlDataReader["FieldType"].ToString().ToLower() == "combolookup")
                    {
                        child2.CssClass = "lookup-combobox";

                        child2.ID = string.Format("txt{0}", (object)sqlDataReader["FieldName"].ToString()).ToLower();
                        child2.Attributes.Add("data-field-name", sqlDataReader["FieldName"].ToString());
                        child2.Attributes.Add("data-parameter-name", sqlDataReader["FieldName"].ToString().ToLower().Replace("(", "").Replace(")", ""));
                        
                        phFilterControls.Controls.Add((Control)child2);
                    }
                    else if (sqlDataReader["FieldType"].ToString().ToLower() == "true/false")
                    {
                        child3.Attributes.Add("type", "checkbox");
                        child3.Attributes.Add("class", "truefalse-checkbox");
                        child3.ID = string.Format("txt{0}", (object)sqlDataReader["FieldName"].ToString()).ToLower();
                        child3.Attributes.Add("data-field-name", sqlDataReader["FieldName"].ToString());
                        child3.Attributes.Add("data-parameter-name", sqlDataReader["FieldName"].ToString().ToLower().Replace("(", "").Replace(")", ""));
                        child3.Attributes.Add("onclick", "GetSearchFilterCheckBoxStatuses();");
                        
                        if (str2 == "true" && !Page.IsPostBack)
                            child3.Attributes.Add("checked", str2);

                        HtmlGenericControl child4 = new HtmlGenericControl("label");

                        child4.InnerHtml = "<span><span></span></span>&nbsp;&nbsp;";
                        child4.Attributes.Add("for", "option");

                        phFilterControls.Controls.Add((Control)child3);
                        phFilterControls.Controls.Add((Control)child4);
                    }
                    else if (sqlDataReader["FieldType"].ToString().ToLower() == "date")
                    {
                        child2.CssClass = "date-textbox";
                        DateTime dateTime;

                        if (child1.InnerText.ToLower().Contains("start"))
                        {
                            child2.ID = string.Format("txt{0}", (object)sqlDataReader["FieldName"].ToString()).ToLower().Replace("date", "startdate");
                            child2.Attributes.Add("data-parameter-name", sqlDataReader["FieldName"].ToString().ToLower().Replace("date", "startdate").Replace("(", "").Replace(")", ""));
                            TextBox textBox = child2;
                            string str3;
                            
                            if (str2 == "now")
                            {
                                dateTime = DateTime.Now;
                                str3 = dateTime.ToString("MM/dd/yyyy");
                            }
                            else if (str2 == "year")
                            {
                                dateTime = DateTime.Now;
                                dateTime = dateTime.AddYears(Convert.ToInt32(str1));
                                str3 = dateTime.ToString("MM/dd/yyyy");
                            }
                            else if (str2 == "month")
                            {
                                dateTime = DateTime.Now;
                                dateTime = dateTime.AddMonths(Convert.ToInt32(str1));
                                str3 = dateTime.ToString("MM/dd/yyyy");
                            }
                            else if (str2 == "day")
                            {
                                dateTime = DateTime.Now;
                                dateTime = dateTime.AddDays((double)Convert.ToInt32(str1));
                                str3 = dateTime.ToString("MM/dd/yyyy");
                            }
                            else
                                str3 = str2;
                            textBox.Text = str3;
                        }
                        else if (child1.InnerText.ToLower().Contains("end"))
                        {
                            child2.ID = string.Format("txt{0}", (object)sqlDataReader["FieldName"].ToString()).ToLower().Replace("date", "enddate");
                            child2.Attributes.Add("data-parameter-name", sqlDataReader["FieldName"].ToString().ToLower().Replace("date", "enddate").Replace("(", "").Replace(")", ""));
                           
                            TextBox textBox = child2;
                            string str4;
                           
                            if (str2 == "now")
                            {
                                dateTime = DateTime.Now;
                                str4 = dateTime.ToString("MM/dd/yyyy");
                            }
                            else if (str2 == "year")
                            {
                                dateTime = DateTime.Now;
                                dateTime = dateTime.AddYears(Convert.ToInt32(str1));
                                str4 = dateTime.ToString("MM/dd/yyyy");
                            }
                            else if (str2 == "month")
                            {
                                dateTime = DateTime.Now;
                                dateTime = dateTime.AddMonths(Convert.ToInt32(str1));
                                str4 = dateTime.ToString("MM/dd/yyyy");
                            }
                            else if (str2 == "day")
                            {
                                dateTime = DateTime.Now;
                                dateTime = dateTime.AddDays((double)Convert.ToInt32(str1));
                                str4 = dateTime.ToString("MM/dd/yyyy");
                            }
                            else
                                str4 = str2;
                           
                            textBox.Text = str4;
                        }
                        else
                        {
                            child2.ID = string.Format("txt{0}", (object)sqlDataReader["FieldName"].ToString()).ToLower();
                            child2.Attributes.Add("data-parameter-name", sqlDataReader["FieldName"].ToString().ToLower().Replace("(", "").Replace(")", ""));
                        }
                        
                        child2.Attributes.Add("data-field-name", sqlDataReader["FieldName"].ToString());

                        phFilterControls.Controls.Add((Control)child2);
                    }
                    else if (sqlDataReader["FieldType"].ToString().ToLower() == "text")
                    {
                        child2.CssClass = "freeform-textbox";
                        child2.ID = string.Format("txt{0}", (object)sqlDataReader["FieldName"].ToString()).ToLower();
                        child2.Attributes.Add("data-parameter-name", sqlDataReader["FieldName"].ToString().ToLower().Replace("(", "").Replace(")", ""));
                       
                        phFilterControls.Controls.Add((Control)child2);
                    }
                    
                    if (sqlDataReader["FieldName"].ToString().ToLower() == "reportname" && sqlDataReader["DefaultValue"].ToString() != "")
                        defaultReportExistsTF = true;
                }
            }
        }
    }
}