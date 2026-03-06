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

namespace AISReports
{
    public partial class DeficientSurveyComments : System.Web.UI.Page
    {
        #region
        private string surveyID;
        private string companyID;
        private string userID;
        private string commentType;
        #endregion

        protected void Page_Init(object sender, EventArgs e) => LoadSecurityContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            surveyID = Request.QueryString["sid"];
            companyID = Request.QueryString["cid"];
            commentType = Request.QueryString["ctype"];

            if (companyID == "10003" || companyID == "10064")
                companyID = "10003,10064";

            LoadComments();
        }

        protected void LoadSecurityContext()
        {
            HttpCookie cookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];

            try
            {
                if (!Request.IsAuthenticated)
                    return;

                userID = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData.Split(';')[3];
            }
            catch  { }
        }

        private void LoadComments()
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                
                stringBuilder.AppendLine("SELECT TOP 500 SurveyID,Comments,CreatedDate,ISNULL((CASE ISNULL(Email,'') WHEN '' THEN (FirstName+' '+LastName) ELSE Email END),'Driver') as UserName ");
                stringBuilder.AppendLine("FROM DeficientSurveyComments ");
                stringBuilder.AppendLine("LEFT OUTER JOIN [User] ON DeficientSurveyComments.UserID=[User].UserID ");
                
                if (surveyID == "NA" && commentType != null)
                    stringBuilder.AppendFormat("WHERE DeficientSurveyComments.CompanyID in ({0}) AND CommentType='{1}' ", companyID, commentType);
                else if (surveyID != "ALL" && commentType != null)
                    stringBuilder.AppendFormat("WHERE SurveyID='{0}' AND DeficientSurveyComments.CompanyID in ({1}) and CommentType='{2}' ", surveyID, companyID, commentType);
                else if (surveyID != "ALL")
                    stringBuilder.AppendFormat("WHERE SurveyID='{0}' AND DeficientSurveyComments.CompanyID in ({1}) ", surveyID, companyID);
                else
                    stringBuilder.AppendFormat("WHERE DeficientSurveyComments.CompanyID in ({0}) ", companyID);
                
                stringBuilder.AppendLine("ORDER BY CreatedDate DESC ");
                
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();

                    SqlCommand command = sqlConnection.CreateCommand();

                    command.CommandType = CommandType.Text;
                    command.CommandText = stringBuilder.ToString();

                    SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                    rpComments.DataSource = sqlDataReader;
                    rpComments.DataBind();

                    sqlDataReader.Dispose();
                }

                if (rpComments.Items.Count == 0)
                    lblNoCommentsFound.Visible = true;
                else
                    lblNoCommentsFound.Visible = false;

            }
            catch (Exception ex)
            {
                lblNoCommentsFound.Text = ex.Message;
            }
        }
    }
}