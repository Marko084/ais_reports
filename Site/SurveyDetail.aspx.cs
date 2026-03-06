using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AISReports.Site
{
    public partial class SurveyDetail : System.Web.UI.Page
    {
        #region
        private string surveyID;
        private string companyId;
        private string companyCode;
        private string viewType;
        private string keyFieldName;
        private string consolidated;
        private bool showZerosTF;
        private bool useActualTF;
        private string formType;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
           surveyID =Request.QueryString["id"];
           companyId =Request.QueryString["cid"];
           viewType =Request.QueryString["viewType"];
           keyFieldName =Request.QueryString["pkfn"];
           consolidated =Request.QueryString["consolidated"];

           formType = "Standard";

            if (Request.QueryString["sz"] != null)
               showZerosTF = Convert.ToBoolean(Request.QueryString["sz"]);
            if (Request.QueryString["ua"] != null)
               useActualTF = Convert.ToBoolean(Request.QueryString["ua"]);
            if (Request.QueryString["ft"] != null)
               formType =Request.QueryString["ft"];

           LoadForm();
        }

        private void LoadForm()
        {
            if (viewType != null)
               viewType += "_";
            else
               viewType = "";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ais_GetSurveyDetails";

                command.Parameters.AddWithValue("CompanyID", companyId);
                command.Parameters.AddWithValue("SurveyID", surveyID);
                command.Parameters.AddWithValue("DataViewType", viewType);
                command.Parameters.AddWithValue("ConsolidatedTF", consolidated);
                command.Parameters.AddWithValue("ShowZerosTF", showZerosTF);
                command.Parameters.AddWithValue("UseActualTF", useActualTF);
                command.Parameters.AddWithValue("FormType", formType);

                if (keyFieldName != null &&keyFieldName != "")
                    command.Parameters.AddWithValue("PrimaryKeyField", keyFieldName);

                SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (sqlDataReader.Read())
                   litSurveyDetail.Text = sqlDataReader[0].ToString();

                if (litSurveyDetail.Text.Trim().Length != 0)
                    return;

               litSurveyDetail.Text = "<h1>No Survey Detail Found.</h1>";
            }
        }
    }
}