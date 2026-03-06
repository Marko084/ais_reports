using AIS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AISReports
{
    public partial class Login : System.Web.UI.Page
    {
        #region Variables
        private Hashtable hshLogin;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_LoggingIn(object sender, LoginCancelEventArgs e) => this.ProcessLogin(this.Login1.UserName, this.Login1.Password, (string)null, "0");

        private void ProcessLogin(string userName, string password, string url, string impersonateUser)
        {
            try
            {
                this.lblMessage.Text = "";

                if (!this.ValidateLogin(userName, password, impersonateUser))
                    return;

                if (this.Login1.RememberMeSet)
                    FormsAuthentication.SetAuthCookie(userName, true, "AIS");
                else
                    FormsAuthentication.SetAuthCookie(userName, false);

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(180.0), false, this.hshLogin[(object)"userinfo"].ToString());
                
                string str1 = FormsAuthentication.Encrypt(ticket);
                string str2 = ticket.UserData.Split(';').GetValue(5).ToString();
                string str3 = ticket.UserData.Split(';').GetValue(2).ToString();
                string str4 = ticket.UserData.Split(';').GetValue(6).ToString();
                string str5 = ticket.UserData.Split(';').GetValue(10).ToString();

                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, str1);

                if (Login1.RememberMeSet)
                    cookie.Expires = DateTime.Now.AddYears(3);

                Response.Cookies.Add(cookie);

                object obj1 = DAL.ReturnScalarValue(string.Format("select UseCMSTF from Company where companyId={0}", str3), true);
                object obj2 = DAL.ReturnScalarValue(string.Format("select ConsolidatedClientTF from Company where companyId={0}", str3), true);
                
                if (Convert.ToBoolean(obj1) && str5.ToLower().Contains("nw_consolidated"))
                    Response.Redirect(string.Format(str5.ToLower(), str3), true);
                else if (!Convert.ToBoolean(obj2) && str5.ToLower().Contains("?cid="))
                    Response.Redirect(string.Format("{0}", str5), true);
                else if (!Convert.ToBoolean(obj2))
                    Response.Redirect(string.Format("{0}?cid={1}", str5, str3), true);
                else if (Convert.ToBoolean(obj1) && str5.ToLower().Contains("dashboard.aspx?cid=10102"))
                   Response.Redirect(str5.ToLower(), true);
                else if (Convert.ToBoolean(obj1) && str5.ToLower().Contains("/"))
                    Response.Redirect(string.Format(str5.ToLower().Replace("dashboard", "company"), str3), true);
                else if (url != null && url != "")
                    Response.Redirect(url, true);
                else if (str4 == "")
                    Response.Redirect(string.Format(FormsAuthentication.DefaultUrl, str2, str3), true);
                else
                    Response.Redirect(string.Format(FormsAuthentication.DefaultUrl, str2, str3).Replace("Dashboard", "Company"), true);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private bool ValidateLogin()
        {
            hshLogin = BLL.GetLoginInfo(Login1.UserName, Login1.Password);

            return hshLogin.Count > 0;
        }

        private bool ValidateLogin(string userName, string password)
        {
            hshLogin = BLL.GetLoginInfo(userName, password);
            return hshLogin.Count > 0;
        }

        private bool ValidateLogin(string userName, string password, string impersonateUser)
        {
            hshLogin = GetLoginInfo(userName, password, impersonateUser);
            return hshLogin.Count > 0;
        }

        private void LoadSecurityContext()
        {
            HttpContext current = HttpContext.Current;
            string formsCookieName = FormsAuthentication.FormsCookieName;
            HttpCookie cookie = current.Request.Cookies[formsCookieName];

            try
            {
                if (!current.Request.IsAuthenticated)
                    return;

                string[] strArray = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData.Split(';');

                string str1 = strArray[8];
                string str2 = strArray[9];
                string str3 = strArray[5];
                string str4 = strArray[2];
                string str5 = strArray[6];

                string str6 = Request.QueryString["url"];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private Hashtable GetLoginInfo(string username, string password, string impersonateUser)
        {
            Hashtable loginInfo = new Hashtable();

            SqlDataReader sqlDataReader = DAL.ReturnDataReader("getLogin", "username,password,impersonateUser", string.Format("{0},{1},{2}",username, password,impersonateUser));
            
            if (!sqlDataReader.IsClosed)
            {
                sqlDataReader.Read();
                
                if (sqlDataReader.HasRows)
                {
                    loginInfo.Add(nameof(username), username);
                    loginInfo.Add("userinfo", string.Format("{0} {1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13}", sqlDataReader["FirstName"], sqlDataReader["LastName"], sqlDataReader["companyname"], sqlDataReader["companyid"], sqlDataReader["userid"], sqlDataReader["UserTypeName"], sqlDataReader["CompanyCode"], sqlDataReader["OrganizationName"], sqlDataReader["OrganizationID"], username, password, sqlDataReader["LandingPage"].ToString(), sqlDataReader["OrganizationCode"].ToString(), impersonateUser));
                }

                sqlDataReader.Close();
                sqlDataReader.Dispose();
            }

            return loginInfo;
        }
    }
}