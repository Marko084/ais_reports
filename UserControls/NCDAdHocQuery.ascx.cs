using AIS;
using AISReports.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AISReports.UserControls
{
    public partial class NCDAdHocQuery : NCDUserControlBase
    {
        #region
        private string companyID;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            companyID = Request.QueryString["cid"];

            LoadSearchFields();

            pChart.Attributes.Add("grid-query", "");
            pChart.Attributes.Add("grid-query-type", "text");
            pChart.Attributes.Add("grid-display-fields", "");
            pChart.Attributes.Add("grid-type", "adhoc");

            litNoData.Text = "<td></td>";

            LoadChartSettings();
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
                    if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "height")
                        pChart.Height = Unit.Pixel(Convert.ToInt32(sqlDataReader["SettingValue"]));
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "detaillink")
                        pChart.Attributes.Add("detail-link", sqlDataReader["SettingValue"].ToString());
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "pdfexport")
                        pChart.Attributes.Add("grid-export-pdf", "true");
                }
            }
        }

        private void LoadSearchFields()
        {
            string str1 = (string)null;
            string str2 = (string)null;

            HtmlGenericControl child1 = (HtmlGenericControl)null;
            HtmlGenericControl child2 = (HtmlGenericControl)null;

            int num = 1;

            SqlDataReader sqlDataReader = (SqlDataReader)DAL.ReturnDataReader("GetAdHocFieldList", "companyID,userID", companyID + "," + UserID);

            while (sqlDataReader.Read())
            {
                if (str2 != sqlDataReader["TableName"].ToString())
                {
                    str2 = sqlDataReader["TableName"].ToString();
                    child2 = new HtmlGenericControl("div");

                    HtmlGenericControl child3 = new HtmlGenericControl("li");
                    HtmlGenericControl child4 = new HtmlGenericControl("a");
                    HtmlGenericControl child5 = new HtmlGenericControl("div");

                    child5.Attributes.Add("id", string.Format("tabs-{0}", num));

                    child4.InnerText = sqlDataReader["TabName"].ToString();
                    child4.Attributes.Add("href", string.Format("#tabs-{0}", num));
                    child4.Attributes.Add("onclick", "javascript:showTabs(href);");

                    child3.Controls.Add((Control)child4);

                    phTabs.Controls.Add((Control)child3);

                    child2.Attributes.Add("class", "accordion");

                    child5.Controls.Add((Control)child2);

                    phFieldGroups.Controls.Add((Control)child5);

                    ++num;

                    str1 = (string)null;
                }

                if (str1 != sqlDataReader["FieldGroupName"].ToString())
                {
                    str1 = sqlDataReader["FieldGroupName"].ToString();

                    HtmlGenericControl child6 = new HtmlGenericControl("h3");

                    child6.InnerHtml = string.Format("<span class='accordion-text'>{0}</span>", str1);

                    child2.Controls.Add((Control)child6);

                    child1 = new HtmlGenericControl("div");

                    child2.Controls.Add((Control)child1);
                }

                HtmlGenericControl child7 = new HtmlGenericControl("input");
                HtmlGenericControl child8 = new HtmlGenericControl("span");

                child8.Attributes.Add("class", "context-group-by");

                child7.Attributes.Add("type", "checkbox");
                child7.Attributes.Add("onclick", "javascript:CheckBoxSelected(this);");
                child7.Attributes.Add("rel", string.Format("{0}.{1}", sqlDataReader["TableName"].ToString(), sqlDataReader["DataFieldName"].ToString()));
                child7.Attributes.Add("field-data-type", sqlDataReader["DataType"].ToString());

                child8.Controls.Add((Control)child7);

                ControlCollection controls = child8.Controls;
                HtmlGenericControl htmlGenericControl = new HtmlGenericControl("span");

                htmlGenericControl.InnerText = sqlDataReader["DisplayName"].ToString() == "" ? SplitCamelCase(sqlDataReader["DataFieldName"].ToString()) : SplitCamelCase(sqlDataReader["DisplayName"].ToString());
                
                HtmlGenericControl child9 = htmlGenericControl;

                controls.Add((Control)child9);

                child1.Controls.Add((Control)child8);
            }
        }

        private string SplitCamelCase(string str) => Regex.Replace(Regex.Replace(str, "(\\P{Ll})(\\P{Ll}\\p{Ll})", "$1 $2"), "(\\p{Ll})(\\P{Ll})", "$1 $2");
    }
}