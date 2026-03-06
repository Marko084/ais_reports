using AIS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AISReports
{
    public partial class DriverSafetyPointsDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtPVOName.Text = Request.QueryString["pvo"];
            txtAgent.Text = Request.QueryString["agent"];
            txtEnteredStartDate.Text = Request.QueryString["startDate"];
            txtEnteredEndDate.Text = Request.QueryString["endDate"];
            txtViolationType.Text = Request.QueryString["vType"];
            txtCompanyID.Text = Request.QueryString["cid"];
            txtUserID.Text = Request.QueryString["uid"];

            LoadSummaryColumns();
            LoadDetailColumns();

            lblMessage.Text = "";
        }

        private void LoadSummaryColumns()
        {
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();

            SqlDataReader sqlDataReader = DAL.ReturnDataReader(string.Format("SELECT * FROM DisplayFields WHERE CompanyID={0} AND PageName='{1}' AND DisplayTF=1 AND PageSection='Summary' ORDER BY SortOrder", txtCompanyID.Text, Path.GetFileName(Request.CurrentExecutionFilePath)), CommandType.Text);
            
            while (sqlDataReader.Read())
            {
                stringBuilder1.AppendFormat("<th style='font-size:12px;'>{0}</th>", sqlDataReader["FieldName"]);

                if (stringBuilder2.Length == 0)
                    stringBuilder2.AppendFormat("{0}", sqlDataReader["FieldName"]);
                else
                    stringBuilder2.AppendFormat("|{0}", sqlDataReader["FieldName"]);
            }

            pChart.Attributes.Add("grid-display-fields", stringBuilder2.ToString());

            litHeaderRow.Text = stringBuilder1.ToString();
            litNoData.Text = string.Format("<td colspan='{0}' class='dataTables_empty' >Processing...</td>", (stringBuilder2.ToString().Split('|').Length + 1));
        }

        private void LoadDetailColumns()
        {
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();

            SqlDataReader sqlDataReader = DAL.ReturnDataReader(string.Format("SELECT * FROM DisplayFields WHERE CompanyID={0} AND PageName='{1}' AND DisplayTF=1 AND PageSection='Detail' ORDER BY SortOrder", txtCompanyID.Text, Path.GetFileName(Request.CurrentExecutionFilePath)), CommandType.Text);
            
            while (sqlDataReader.Read())
            {
                stringBuilder1.AppendFormat("<th style='font-size:12px;'>{0}</th>", sqlDataReader["FieldName"]);

                if (stringBuilder2.Length == 0)
                    stringBuilder2.AppendFormat("{0}", sqlDataReader["FieldName"]);
                else
                    stringBuilder2.AppendFormat("|{0}", sqlDataReader["FieldName"]);
            }

            pChart2.Attributes.Add("grid-display-fields", stringBuilder2.ToString());

            litDetailHeaderRow.Text = stringBuilder1.ToString();
            litDetailNoData.Text = string.Format("<td colspan='{0}' class='dataTables_empty' >Processing...</td>", (stringBuilder2.ToString().Split('|').Length + 1));
        }
    }
}