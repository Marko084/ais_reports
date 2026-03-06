using AIS;
using AISReports.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AISReports.UserControls
{
    public partial class NCDReportingCtrl : NCDUserControlBase
    {
        private bool camelCaseHeaderTitle = true;
        protected Literal litReportStyle;
        protected Label lblControlTitle;
        protected HyperLink hypExportToExcel;
        protected HyperLink hypExportToPDF;
        protected Label lblReportTitle;
        protected Label lblReportSubTitle;
        protected Label lblReportParameters;
        protected Image imgLogo1;
        protected Label lblHeaderText;
        protected GridView GridView1;
        protected GridView GridView2;
        protected GridView GridView3;
        protected TextBox txtQueryParams;
        protected TextBox txtQueryName;
        protected Label lblMessage;
        private string queryName;
        private string layoutType;
        private string lossRunReportHeaderText = "Claim Number|Date Received|Loss Date|LAG|Insured Name|Claimant|Injury Codes|Status|Type|Subro|Legal|Location|Stat|Result";
        private string workersCompSubHeaderText = "Indemnity|Medical|Expenses|Incurred";
        private string autoLiabilitySubHeaderText = "Property Damage|Bodily injury|Expenses|Incurred";

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}