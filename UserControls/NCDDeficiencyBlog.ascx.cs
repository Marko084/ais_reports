using AISReports.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AISReports.UserControls
{
    public partial class NCDDeficiencyBlog :NCDUserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadColumns();
            LoadChartSettings();

            pChart.Attributes.Add("grid-query", QueryName);
            pChart.Attributes.Add("grid-query-type", QueryType);
            pChart.Attributes.Add("grid-display-fields", DisplayFields.Replace("!", ""));
            pChart.Attributes.Add("grid-type", "blog");
        }

        private void LoadColumns()
        {
            StringBuilder stringBuilder = new StringBuilder();

            int num = 0;
            string displayFields = DisplayFields;
            char[] chArray = new char[1] { '|' };

            foreach (string str in displayFields.Split(chArray))
            {
                stringBuilder.AppendFormat("<th>{0}</th>", SplitCamelCase(str));
                ++num;
            }

            litHeaderRow.Text = stringBuilder.ToString();

            litBody.Text = string.Format("<td colspan='{0}' class='dataTables_empty' >Processing...</td>", (num + 1));
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
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "primarykeyfield")
                        pChart.Attributes.Add("primary-key-field", sqlDataReader["SettingValue"].ToString());
                }
            }
        }

        private string SplitCamelCase(string str) => Regex.Replace(Regex.Replace(str, "(\\P{Ll})(\\P{Ll}\\p{Ll})", "$1 $2"), "(\\p{Ll})(\\P{Ll})", "$1 $2");
    }
}