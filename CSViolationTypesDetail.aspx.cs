using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AISReports
{
    public partial class CSViolationTypesDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtPVOName.Text = Request.QueryString["pvo"];
            txtAgent.Text = Request.QueryString["agent"];
            txtMonthEndStartDate.Text = Request.QueryString["startDate"];
            txtMonthEndEndDate.Text = Request.QueryString["endDate"];
            txtPointType.Text = Request.QueryString["pType"];
            txtViolationSection.Text = Request.QueryString["vSection"];

            if (Request.QueryString["cid"] != null)
                txtCompanyID.Text = Request.QueryString["cid"];
            else if (Request.QueryString["companyID"] != null)
                txtCompanyID.Text = Request.QueryString["companyID"];

            if (Request.QueryString["uid"] != null)
                txtUserID.Text = Request.QueryString["uid"];
            else if (Request.QueryString["userID"] != null)
                txtUserID.Text = Request.QueryString["uid"];

            lblMessage.Text = "";
        }
    }
}