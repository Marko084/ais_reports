using AISReports.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AISReports.UserControls
{
    public partial class NCDCompanyButtons : NCDUserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();

            string[] strArray = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData.Split(';');
            string str1 = strArray[3];
            string str2 = strArray[2];

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "cms_GetCompanyButtons";

                command.Parameters.AddWithValue("CompanyID", str2);
                command.Parameters.AddWithValue("UserID", str1);

                SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (sqlDataReader.Read())
                {
                    string str3 = sqlDataReader["LandingPage"].ToString();

                    stringBuilder.AppendLine("<div class='siteIconStyle'>");
                    stringBuilder.AppendFormat("<a href='../{0}?cid={1}'>", ((IEnumerable<string>)str3.Split('?')).First<string>(), sqlDataReader["CompanyID"].ToString());
                    stringBuilder.AppendLine("<img src='../images/SITEICON.png' height='64' width='64' border='0' />");
                    stringBuilder.AppendLine("<br/>");
                    stringBuilder.AppendFormat("<span>{0}</span>", sqlDataReader["CompanyDescription"].ToString());
                    stringBuilder.AppendLine("</a>");
                    stringBuilder.AppendLine("</div>");
                }

                sqlDataReader.Dispose();
                sqlConnection.Close();
            }
            
            litCompanyLinks.Text = stringBuilder.ToString();
        }
    }
}