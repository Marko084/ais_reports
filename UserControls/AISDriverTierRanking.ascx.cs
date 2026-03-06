using AIS;
using AISReports.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AISReports.UserControls
{
    public partial class AISDriverTierRanking : NCDUserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadChartSettings();
            GenerateReport();
        }


        private void GenerateReport()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ARS_DriverTierTrackingReport";
            }
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
                    if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "reportstyle")
                        litReportStyle.Text = string.Format("<link rel=\"stylesheet\" href=\"../css/reports/{0}.css?{1}\" type=\"text/css\" media=\"screen\" />", sqlDataReader["SettingValue"].ToString(), GetUniqueID());
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "controltitle")
                        lblControlTitle.Text = sqlDataReader["SettingValue"].ToString();
                }
            }
        }
    }
}