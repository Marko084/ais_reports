using AISReports.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AISReports.UserControls
{
    public partial class NCDImageDocuments : NCDUserControlBase
    {
        #region
        private string companyID;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            companyID = Request.QueryString["cid"];
            int num = Page.IsPostBack ? 1 : 0;
        }

    }
}