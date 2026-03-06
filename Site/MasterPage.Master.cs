using AIS;
using AISReports.App_Code;
using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AISReports.Site
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        #region Variables
        private string _companyId = "0";
        private string _userId = "0";
        private string _companyName;
        private string _userLevel;
        private string _userName;
        private string[] userData;
        private string _companyCode;
        private string pageName;
        #endregion

        #region Properties
        public string CompanyId
        {
            get => _companyId;
            set => _companyId = value;
        }

        public string CompanyCode
        {
            get => _companyCode;
            set => _companyCode = value;
        }

        public string UserId
        {
            get => _userId;
            set => _userId = value;
        }

        public string CompanyName => _companyName;

        public string UserName => _userName;

        public string UserLevel => _userLevel;

        public string PageTitle
        {
            get => Page.Title;
            set => Page.Title = value;
        }

        public string CurrentPageName => pageName;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadStyleSheets();

            if (Page.IsPostBack)
                return;

            LoadMenu();

            lblCID.Text = Request.QueryString["cid"];
            lblUID.Text = _userId;
            lblUType.Text = _userLevel;

            if (lblCID.Text == "")
                lblCID.Text = _companyId;

            object obj = DAL.ReturnScalarValue(string.Format("select ConsolidatedClientTF from Company where companyId={0}", lblCID.Text), true);

            lblConsolidated.Text = obj != DBNull.Value ? Convert.ToInt32(obj).ToString() : "0";
            LoadCustomSettings();

        }

        protected void ContentPlaceHolder1_Init(object sender, EventArgs e)
        {
            pageName = Path.GetFileName(Request.PhysicalPath);
            LoadSecurityContext();
            LoadMasterStyleSheets();
        }

        private void LoadMenu()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetMenuItemsUser";
                command.Parameters.AddWithValue("CompanyID", (object)CompanyId);
                command.Parameters.AddWithValue("UserID", (object)_userId);

                SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                litMenu.Text = "";

                while (sqlDataReader.Read())
                    litMenu.Text += sqlDataReader[0].ToString();

                sqlDataReader.Close();
                sqlDataReader.Dispose();
            }
        }

        protected void LoadSecurityContext()
        {
            HttpCookie cookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            string str = Request.QueryString["cid"];

            try
            {
                if (Request.IsAuthenticated)
                {
                    userData = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData.Split(';');

                    _userName = userData[0];
                    _companyId = userData[2];
                    _userId = userData[3];
                    _companyName = userData[1];
                    _userLevel = userData[4];
                    _companyCode = userData[5];
                    lblUserInfo.Text = !(userData[12] == "1") ? string.Format("Welcome {0}", (object)userData[0]) : string.Format("<b>IMPERSONATION MODE</b><br/>Welcome {0}", (object)userData[0]);
                    lblCompanyName.Text = _companyName;
                }
                else
                    lblUserInfo.Text = "";

                if (str == null || !(str != ""))
                    return;

                _companyId = BLL.UserAccessToPageTF(_userId, str) ? str : throw new Exception("This account does not have access to this page.  If this is not correct please contact AIS.");
                _companyName = BLL.GetCompanyNameById(str);

                lblCompanyName.Text = _companyName;

                if (!_companyCode.ToLower().Contains("convergent"))
                    return;

                lblCompanyName.Text = DateTime.Now.ToString("MMMM d, yyyy hh:mm tt");

                GetMenuItemName();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void LoadStyleSheets()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "cms_GetPageLayout";
                command.Parameters.AddWithValue("CompanyID", (object)CompanyId);
                command.Parameters.AddWithValue("PageName", (object)pageName);
                command.Parameters.AddWithValue("UserID", (object)UserId);

                SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                bool flag = false;

                if (!sqlDataReader.HasRows)
                {
                    string str = "../css/1-column-center-layout.css";

                    foreach (Control control in Page.Header.Controls)
                    {
                        if (control is HtmlLink && (control as HtmlLink).Attributes["href"] == str)
                            flag = true;
                    }

                    if (!flag)
                    {
                        HtmlLink child = new HtmlLink();
                        child.Attributes.Add("href", str + "?" + GenerateID());
                        child.Attributes.Add("type", "text/css");
                        child.Attributes.Add("rel", "stylesheet");
                        Page.Header.Controls.Add((Control)child);
                    }
                }

                while (sqlDataReader.Read())
                {
                    string str = sqlDataReader["StyleUrl"].ToString();

                    foreach (Control control in Page.Header.Controls)
                    {
                        if (control is HtmlLink && (control as HtmlLink).Attributes["href"] == str)
                            flag = true;
                    }

                    if (!flag)
                    {
                        HtmlLink child = new HtmlLink();
                        child.Attributes.Add("href", str + "?" + GenerateID());
                        child.Attributes.Add("type", "text/css");
                        child.Attributes.Add("rel", "stylesheet");
                        Page.Header.Controls.Add((Control)child);
                    }
                }

                sqlDataReader.Close();
                sqlDataReader.Dispose();
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
                command.Parameters.AddWithValue("CompanyID", (object)CompanyId);
                SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (sqlDataReader.Read())
                {
                   CssAdder.AddCss(sqlDataReader["StyleSheetUrl"].ToString(), Page);
                    if (sqlDataReader["StyleSheetUrl"].ToString().ToLower().Contains("menu.css"))
                        flag = true;
                    else
                        lblChartStyle.Text = !sqlDataReader["StyleSheetUrl"].ToString().ToLower().Contains("convergent") ? "ais" : "convergent";
                }
                if (!flag)
                    CssAdder.AddCss("../css/menu.css", Page);
                sqlDataReader.Close();
                sqlDataReader.Dispose();
            }
        }

        private void LoadCustomSettings()
        {
            SqlDataReader sqlDataReader = (SqlDataReader)DAL.ReturnDataReader(string.Format("select * from CompanySettings where CompanyID={0}", (object)_companyId), CommandType.Text);
            
            if (_companyCode.ToLower().StartsWith("convergent"))
            {
                while (sqlDataReader.Read())
                {
                    if (sqlDataReader["SettingName"].ToString().ToLower() == "sitelogourl")
                        imgLogo.ImageUrl = sqlDataReader["SettingValue"].ToString();
                    else if (sqlDataReader["SettingName"].ToString().ToLower() == "sitetitletext")
                        lblHeaderTextRight.Text = sqlDataReader["SettingValue"].ToString();
                    else if (sqlDataReader["SettingName"].ToString().ToLower() == "sitesubtitletext")
                        lblSubHeaderTextRight.Text = sqlDataReader["SettingValue"].ToString();
                }

                lblHeaderText.Text = "";
            }
            else
            {
                while (sqlDataReader.Read())
                {
                    if (sqlDataReader["SettingName"].ToString().ToLower() == "sitelogourl")
                        imgLogo.ImageUrl = sqlDataReader["SettingValue"].ToString();
                    else if (sqlDataReader["SettingName"].ToString().ToLower() == "sitetitletext")
                        lblHeaderText.Text = sqlDataReader["SettingValue"].ToString();
                    else if (sqlDataReader["SettingName"].ToString().ToLower() == "sitesubtitletext")
                        lblSubHeaderText.Text = sqlDataReader["SettingValue"].ToString();
                }
            }

            if (!sqlDataReader.IsClosed)
                sqlDataReader.Close();

            sqlDataReader.Dispose();
        }

        public string GenerateID()
        {
            string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] chArray = new char[7];
            Random random = new Random();
            for (int index = 0; index < chArray.Length; ++index)
                chArray[index] = str[random.Next(str.Length)];
            return new string(chArray);
        }

        public string GetMenuItemName()
        {
            string menuItemName = "";
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "cms_GetSelectMenuItemName";
                command.Parameters.AddWithValue("PageName", CurrentPageName);
                command.Parameters.AddWithValue("CompanyID", CompanyId);
                command.Parameters.AddWithValue("UserID", UserId);

                SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (sqlDataReader.Read())
                    lblSelectedMenuItem.Text = sqlDataReader["MenuItemDescription"].ToString().ToUpper();

                sqlDataReader.Close();
                sqlDataReader.Dispose();
            }
            return menuItemName;
        }
    }

}