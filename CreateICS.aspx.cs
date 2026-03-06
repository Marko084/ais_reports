using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AISReports
{
    public partial class CreateICS : System.Web.UI.Page
    {
        #region
        private string calID;
        private string userID;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            userID = Request.QueryString["uid"];
            calID = Request.QueryString["calid"];

            if (!(calID != "0") || !(calID != ""))
                return;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "cms_GetAppointment";

                command.Parameters.AddWithValue("UserID", userID);
                command.Parameters.AddWithValue("DownloadID", calID);

                SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.AppendLine("BEGIN:VCALENDAR");
                stringBuilder.AppendLine("PRODID:-//Claim Progress");
                stringBuilder.AppendLine("METHOD:PUBLISH");
                stringBuilder.AppendLine("BEGIN:VEVENT");

                while (sqlDataReader.Read())
                {
                    stringBuilder.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", Convert.ToDateTime(sqlDataReader["StartDateTime"]).AddHours(5.0)));
                    stringBuilder.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmssZ}", DateTime.UtcNow));
                    stringBuilder.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", Convert.ToDateTime(sqlDataReader["EndDateTime"]).AddHours(5.0)));
                    stringBuilder.AppendLine(string.Format("UID:{0}", Guid.NewGuid()));
                    stringBuilder.AppendLine(string.Format("DESCRIPTION:{0}", sqlDataReader["Notes"].ToString()));
                    stringBuilder.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", sqlDataReader["Notes"].ToString()));
                    stringBuilder.AppendLine(string.Format("SUMMARY;LANGUAGE=en-us:{0}", sqlDataReader["Subject"].ToString()));
                    stringBuilder.AppendLine(string.Format("BEGIN:VALARM"));

                    if (sqlDataReader["Reminder"].ToString() != "")
                        stringBuilder.AppendLine(string.Format("TRIGGER:-PT{0}{1}", ((IEnumerable<string>)sqlDataReader["Reminder"].ToString().Split('|')).First<string>(), ((IEnumerable<string>)sqlDataReader["Reminder"].ToString().Split('|')).Last<string>()));
                    else
                        stringBuilder.AppendLine("TRIGGER:-PT15M");

                    stringBuilder.AppendLine("ACTION:DISPLAY");
                    stringBuilder.AppendLine("DESCRIPTION:Reminder");
                    stringBuilder.AppendLine("END:VALARM");
                    stringBuilder.AppendLine("END:VEVENT");
                    stringBuilder.AppendLine("END:VCALENDAR");
                }

                sqlDataReader.Close();
                sqlDataReader.Dispose();

                string str = "attachment; filename=Calendar.ics";

                Response.ClearContent();
                Response.AddHeader("content-disposition", str);
                Response.ContentType = "text/calendar";
                Response.Write(stringBuilder.ToString());
                Response.End();
            }
        }
    }
}