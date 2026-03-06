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
    public partial class NCDEmail : NCDUserControlBase
    {
        #region
        private string companyID;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            companyID = Request.QueryString["cid"];

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
                command.Parameters.AddWithValue("CompanyID", companyID);

                SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (sqlDataReader.Read())
                {
                    if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "fromaddress")
                        txtFromAddress.Text = sqlDataReader["SettingValue"].ToString();
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "toaddresslistquery")
                        pChart.Attributes.Add("to-address-list-query", sqlDataReader["SettingValue"].ToString());
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "encrpytedlink")
                        pChart.Attributes.Add("encrypted-link", sqlDataReader["SettingValue"].ToString());
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "controltitle")
                        pChart.Attributes.Add("control-title", sqlDataReader["SettingValue"].ToString());
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "emailbody")
                        lblEmailBody.Text = sqlDataReader["SettingValue"].ToString();
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "emailsubject")
                        pChart.Attributes.Add("email-subject", sqlDataReader["SettingValue"].ToString());
                }

                sqlDataReader.Close();
                sqlDataReader.Dispose();
            }

            if (!(lblEmailBody.Text == ""))
                return;

            lblEmailBody.Text = "A claim has been filed on a shipment that you may have been responsible on.  This is your notification: replying to this email is your only opportunity to respond ( no phone calls ).  You must reply within 10 business days." + Environment.NewLine + Environment.NewLine + "Thank you, Atlantic Relocation Claims Department.";
        }
    }
}