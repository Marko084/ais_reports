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
    public partial class NCDMap : NCDUserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pChart.Attributes.Add("grid-query", QueryName);
            pChart.Attributes.Add("grid-query-type", QueryType);
            pChart.Attributes.Add("grid-display-fields", DisplayFields.Replace("!", ""));
            pChart.Attributes.Add("grid-type", "map");

            LoadChartSettings();
        }

        private void LoadChartSettings()
        {
            lblChartTitle.Text = ChartTitle;

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
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "charttitle")
                    {
                        pChart.Attributes.Add("chart-title", sqlDataReader["SettingValue"].ToString());
                        lblChartTitle.Text = sqlDataReader["SettingValue"].ToString();
                    }
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "charttypebuttons")
                        pChart.Attributes.Add("chart-types", sqlDataReader["SettingValue"].ToString().Trim());
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "enabledrilldown")
                        pChart.Attributes.Add("chart-drilldown", sqlDataReader["SettingValue"].ToString());
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "charttheme")
                        pChart.Attributes.Add("chart-theme", sqlDataReader["SettingValue"].ToString());
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "customquery")
                        pChart.Attributes.Add("chart-custom-query", sqlDataReader["SettingValue"].ToString());
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "chartparameters")
                        pChart.Attributes.Add("chart-parameters", sqlDataReader["SettingValue"].ToString());
                }
            }
        }
    }
}