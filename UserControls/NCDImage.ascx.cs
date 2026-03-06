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
    public partial class NCDImage : NCDUserControlBase
    {
        #region
        private string documentUrl;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            documentUrl = !HttpContext.Current.Request.Url.Host.ToLower().Contains("localhost") ? "http://" + HttpContext.Current.Request.Url.Host : "http://localhost/ais";
            LoadSettings();
        }

        private void LoadSettings()
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
                    if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "imagepath")
                    {
                        HtmlGenericControl child1 = new HtmlGenericControl("div");
                        HtmlGenericControl child2 = new HtmlGenericControl("img");

                        child2.Attributes.Add("src", string.Format("{0}\\{1}", documentUrl, sqlDataReader["SettingValue"].ToString().Trim().ToLower()));
                        child1.Controls.Add((Control)child2);

                        phImages.Controls.Add((Control)child1);
                    }
                }
            }
        }
    }
}