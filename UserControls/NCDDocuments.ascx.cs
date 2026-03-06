using AISReports.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AISReports.UserControls
{
    public partial class NCDDocuments : NCDUserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadChartSettings();
            DisplayLists();
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
                    if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "sectiontitle")
                        lblSectionTitle.Text = sqlDataReader["SettingValue"].ToString();
                }
            }
        }

        private void DisplayLists()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "cms_GetDocuments";

                command.Parameters.AddWithValue("CompanyID", CompanyID);
                command.Parameters.AddWithValue("UserID", UserID);

                SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                HtmlGenericControl child1 = (HtmlGenericControl)null;

                string str = "";

                while (sqlDataReader.Read())
                {
                    if (str != sqlDataReader["GroupName"].ToString())
                    {
                        str = sqlDataReader["GroupName"].ToString();

                        HtmlGenericControl child2 = new HtmlGenericControl("span");
                        HtmlGenericControl child3 = new HtmlGenericControl("div");

                        child1 = new HtmlGenericControl("ul");

                        HtmlGenericControl child4 = new HtmlGenericControl("div");
                        HtmlGenericControl child5 = new HtmlGenericControl("div");

                        child3.Attributes.Add("class", "doc-list-group");

                        child4.Attributes.Add("class", "fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix");

                        child2.Attributes.Add("style", "display:inline-block;padding:3px;");
                        child2.InnerText = str;

                        child5.Attributes.Add("class", "doc-list-body");

                        child4.Controls.Add((Control)child2);

                        child3.Controls.Add((Control)child4);

                        child5.Controls.Add((Control)child1);

                        child3.Controls.Add((Control)child5);

                        phDocuments.Controls.Add((Control)child3);
                    }

                    child1.InnerHtml += string.Format("<li><a href='../GetFile.aspx?id={0}'>{1}</a></li>", sqlDataReader["pkDocumentID"], sqlDataReader["DocumentName"]);
                }

                sqlDataReader.Close();
                sqlDataReader.Dispose();
            }
        }
    }
}