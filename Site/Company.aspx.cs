using AISReports.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AISReports.Site
{
    public partial class Company : NCDPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Page_Init(object sender, EventArgs e) => LoadUserControls();

        private void LoadUserControls()
        {
            string str = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData.Split(';')[3];
            string companyId = Master.CompanyId;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "cms_GetUserControls";

                command.Parameters.AddWithValue("CompanyID", companyId);
                command.Parameters.AddWithValue("UserID", str);
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

                            child.UserID = Master.UserId;
                            child.CompanyID = Master.CompanyId;
                            foundControl.Controls.Add((Control)child);
                        }
                    }
                }

                sqlDataReader.Close();
                sqlDataReader.Dispose();
                sqlConnection.Close();
            }
        }
    }
}