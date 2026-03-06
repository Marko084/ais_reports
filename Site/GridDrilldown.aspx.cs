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

namespace AISReports.Site
{
    public partial class GridDrilldown : System.Web.UI.Page
    {
        #region
        private string title;
        private string query;
        private string companyID;
        private string userID;
        private string queryType;
        private string keyFieldValue;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            title = Request.QueryString["t"];
            query = Request.QueryString["q"];
            queryType = Request.QueryString["qt"];
            companyID = Request.QueryString["cid"];
            userID = Request.QueryString["uid"];
            keyFieldValue = Request.QueryString["kf"];

            if (title != null)
                lblTitle.Text = title;

            LoadMasterStyleSheets();

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();

                if (queryType.ToLower() == "storedproc")
                    command.CommandType = CommandType.StoredProcedure;
                else
                    command.CommandType = CommandType.Text;

                command.CommandText = query;

                command.Parameters.AddWithValue("KeyFIeldValue", keyFieldValue);
                command.Parameters.AddWithValue("CompanyID", companyID);
                command.Parameters.AddWithValue("UserID", userID);

                DataSet dataSet = new DataSet();

                new SqlDataAdapter(command).Fill(dataSet);

                GridView1.DataSource = dataSet;
                GridView1.DataBind();

                if (dataSet.Tables[0].Rows.Count == 0)
                    lblMessage.Text = "No data was found.";
                else
                    lblMessage.Text = "";
            }
        }

        private void LoadMasterStyleSheets()
        {
            bool flag = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "cms_GetMasterStyles";

                command.Parameters.AddWithValue("CompanyID", companyID);

                SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (sqlDataReader.Read())
                {
                    CssAdder.AddCss(sqlDataReader["StyleSheetUrl"].ToString(), Page);

                    if (sqlDataReader["StyleSheetUrl"].ToString().ToLower().Contains("menu.css"))
                        flag = true;
                }

                if (!flag)
                    CssAdder.AddCss("../css/menu.css", Page);

                sqlDataReader.Close();
                sqlDataReader.Dispose();
            }
        }
    }
}