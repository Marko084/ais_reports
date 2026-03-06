using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AISReports
{
    public partial class _default : System.Web.UI.MasterPage
    {
        #region Variables
        private string _companyId = "0";
        private string _companyChildId = "0";
        private string _userId = "0";
        private string _companyName;
        private string _userLevel;
        private string _userName;
        private string[] userData;
        private string _companyCode;
        #endregion

        #region Properties
        public string CompanyId
        {
            get => this._companyId;
            set => this._companyId = value;
        }

        public string CompanyChildId => this._companyChildId;

        public string CompanyCode
        {
            get => this._companyCode;
            set => this._companyCode = value;
        }

        public string UserId
        {
            get => this._userId;
            set => this._userId = value;
        }

        public string CompanyName => this._companyName;

        public string UserName => this._userName;

        public string UserLevel => this._userLevel;

        public string PageTitle
        {
            get => this.Page.Title;
            set => this.Page.Title = value;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ContentPlaceHolder1_Init(object sender, EventArgs e) => this.LoadSecurityContext();

        protected void LoadSecurityContext()
        {
            HttpCookie cookie = this.Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            _companyChildId = this.Request.QueryString["cid"];

            try
            {
                if (!this.Request.IsAuthenticated)
                    return;

                userData = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData.Split(';');

                _userName = this.userData[0];
                _companyId = this.userData[2];
                _userId = this.userData[3];
                _companyName = this.userData[1];
                _userLevel = this.userData[4];
                _companyCode = userData[5];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LoadMenu()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetMenuByCompanyID";
                command.Parameters.AddWithValue("CompanyID", (object)this.CompanyChildId);
                SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (sqlDataReader.Read());

                sqlDataReader.Dispose();
            }
        }
    }
}