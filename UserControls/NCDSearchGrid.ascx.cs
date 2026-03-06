using AISReports.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace AISReports.UserControls
{
    public partial class NCDSearchGrid : NCDUserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadColumns();
            LoadChartSettings();

            pChart.Attributes.Add("grid-query", QueryName);
            pChart.Attributes.Add("grid-query-type", QueryType);
            pChart.Attributes.Add("grid-display-fields", DisplayFields.Replace("!", ""));
            pChart.Attributes.Add("grid-type", "display");
        }

        private void LoadColumns()
        {
            StringBuilder stringBuilder = new StringBuilder();

            int num = 0;

            if (GridHeaderFields != null && GridHeaderFields.Trim().Length > 0)
            {
                string gridHeaderFields = GridHeaderFields;
                char[] chArray = new char[1] { '|' };

                foreach (string str in gridHeaderFields.Split(chArray))
                {
                    stringBuilder.AppendFormat("<th>{0}</th>", str);
                    ++num;
                }
            }
            else
            {
                string displayFields = this.DisplayFields;
                char[] chArray = new char[1] { '|' };

                foreach (string str in displayFields.Split(chArray))
                {
                    stringBuilder.AppendFormat("<th>{0}</th>", str);
                    ++num;
                }
            }

            litHeaderRow.Text = stringBuilder.ToString();
            litNoData.Text = string.Format("<td colspan='{0}' class='dataTables_empty' >Processing...</td>", (num + 1));
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
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "charttitle")
                    {
                        pChart.Attributes.Add("chart-title", sqlDataReader["SettingValue"].ToString());
                        lblChartTitle.Text = sqlDataReader["SettingValue"].ToString();
                    }
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "displaygridformat")
                        pChart.Attributes.Add("display-grid-format", sqlDataReader["SettingValue"].ToString().ToLower());
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "chartparameters")
                        pChart.Attributes.Add("chart-parameters", sqlDataReader["SettingValue"].ToString().ToLower());
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "showexportbutton")
                        pChart.Attributes["show-export-button"] = sqlDataReader["SettingValue"].ToString().ToLower();
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "displaygridfilter")
                        pChart.Attributes["display-grid-filter"] = sqlDataReader["SettingValue"].ToString().ToLower();
                }
            }
        }
    }
}