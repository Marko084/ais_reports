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
    public partial class GeoMap : NCDPageBase
    {
        protected void Page_Load(object sender, EventArgs e) => LoadUserControls();

        private void LoadUserControls()
        {
            SearchFilter1.UserID = Master.UserId;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "cms_GetUserControls";

                command.Parameters.AddWithValue("CompanyID", Master.CompanyId);
                command.Parameters.AddWithValue("UserID", Master.UserId);
                command.Parameters.AddWithValue("PageName", Master.CurrentPageName);

                SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (sqlDataReader.Read())
                {
                    ControlFinder<PlaceHolder> controlFinder = new ControlFinder<PlaceHolder>();
                    controlFinder.FindChildControlsRecursive((Control)Page);

                    foreach (PlaceHolder foundControl in controlFinder.FoundControls)
                    {
                        if (foundControl.ID.ToLower() == sqlDataReader["PageContentSectionName"].ToString().ToLower())
                        {
                            NCDUserControlBase child = (NCDUserControlBase)LoadControl(sqlDataReader["UserControlUrl"].ToString());

                            child.QueryName = sqlDataReader["QueryName"].ToString();
                            child.QueryType = sqlDataReader["QueryType"].ToString();
                            child.DisplayFields = sqlDataReader["DisplayFields"].ToString().Replace("!", "");
                            child.GridHeaderFields = sqlDataReader["GridDisplayHeaders"].ToString();
                            child.PageUserControlID = Convert.ToInt32(sqlDataReader["pkPageControlID"]);
                            child.ChartSettings = sqlDataReader["ChartSettings"].ToString();
                            child.UserID = Master.UserId;

                            if (sqlDataReader["DisplayFields"].ToString().Contains("!"))
                            {
                                string str1 = sqlDataReader["DisplayFields"].ToString();
                                char[] chArray = new char[1] { '|' };

                                foreach (string str2 in str1.Split(chArray))
                                {
                                    if (str2.Contains("!"))
                                    {
                                        child.PrimaryKeyField = str2.Replace("!", "");
                                        break;
                                    }
                                }
                            }

                            foundControl.Controls.Add((Control)child);
                        }
                    }

                    if (sqlDataReader["PageContentSectionName"].ToString().ToLower() == "sitepage")
                        litScript.Text += string.Format("<script type=\"text/javascript\" src=\"{0}?{1}\"></script>", sqlDataReader["UserControlUrl"].ToString(), GetUniqueID().ToLower());
                }

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlConnection.Close();
            }
        }
    }
}