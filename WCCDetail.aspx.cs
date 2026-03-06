using AIS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AISReports
{
    public partial class WCCDetail : System.Web.UI.Page
    {
        #region
        private StringBuilder sbParams;
        private StringBuilder sbValues;
        private string startMonth;
        private string endMonth;
        private string companyId;
        private string year;
        private string quarter;
        private string location;
        private List<string> standardsExceptions;
        private int deltaScoreColumnIndex = -1;
        private int currentColumnIndex;
        private string chartUrl = "../Chart.aspx?cid={0}&ctype=bar&vaxis=quarter&haxis=score&qtype=getwwc&qparm={1}&qval={2}&filter=Critieria&fvalue={3}&location={4}&ctitle=Convention Year {5}";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            year = Request.QueryString["year"];
            companyId = Request.QueryString["cid"];
            quarter = Request.QueryString["quarter"];
            location = Request.QueryString["location"];
            standardsExceptions = new List<string>()
                                        {
                                          "HAULING CLAIMS",
                                          "PACKING CLAIMS",
                                          "SAFETY CSA PTS",
                                          "WAREHOUSE CLAIMS",
                                          "SAFETY POINTS AVERAGE"
                                        };

            GetData();
        }

        private void GetData()
        {
            ClearData();
            LoadParameters();

            ddlCriteriaList.Items.Clear();

            lblYear.Text = year;

            DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo();

            DataSet dataSet = (DataSet)DAL.ReturnDataSet("GetWorldClassCommitmentData", sbParams.ToString(), sbValues.ToString(), "WCC", "WCC");

            if (dataSet.Tables.Count <= 0)
                return;

            DataTable table1 = dataSet.Tables[1];
            DataTable table2 = dataSet.Tables[0];
            DataTable table3 = dataSet.Tables[2];

            int int32 = Convert.ToInt32(year);

            for (int index = 0; index < table1.Rows.Count; ++index)
            {
                DataView defaultView = table2.DefaultView;

                defaultView.RowFilter = string.Format("WCCDate='{0}' ", table1.Rows[index]["Quarter"].ToString());

                string str = string.Format("{0}  {1} - {2} 1 to {3} {4}", table1.Rows[index]["Quarter"], int32, dateTimeFormatInfo.GetMonthName(Convert.ToInt32(table1.Rows[index]["StartMonth"])).ToString(), dateTimeFormatInfo.GetMonthName(Convert.ToInt32(table1.Rows[index]["EndMonth"])).ToString(), DateTime.DaysInMonth(Convert.ToInt32(int32), Convert.ToInt32(table1.Rows[index]["EndMonth"])));
                
                Convert.ToInt32(table1.Rows[index]["EndMonth"]);

                lblFilter.Text += string.Format("{0} {1}", defaultView.RowFilter, defaultView.Count);

                if (defaultView.Count > 0)
                {
                    if (GridView1.Rows.Count == 0)
                    {
                        GridViewLabel1.Text = str;
                        GridView1.DataSource = defaultView;
                        GridView1.DataBind();

                        currentColumnIndex = 0;
                    }
                    else if (GridView2.Rows.Count == 0)
                    {
                        GridViewLabel2.Text = str;
                        GridView2.DataSource = defaultView;
                        GridView2.DataBind();

                        currentColumnIndex = 0;
                    }
                    else if (GridView3.Rows.Count == 0)
                    {
                        GridViewLabel3.Text = str;
                        GridView3.DataSource = defaultView;
                        GridView3.DataBind();

                        currentColumnIndex = 0;
                    }
                    else if (GridView4.Rows.Count == 0)
                    {
                        GridViewLabel4.Text = str;
                        GridView4.DataSource = defaultView;
                        GridView4.DataBind();

                        currentColumnIndex = 0;
                    }
                }
            }

            lblUploadDate.Text = "Data displayed is through 2/29/2016";

            frmChart.Attributes.Remove("src");
            frmChart.Attributes.Add("src", string.Format(chartUrl, companyId, sbParams.ToString(), sbValues.ToString(), ddlCriteriaList.SelectedValue, location, year));
        }

        private void ClearData()
        {
            GridViewLabel1.Text = "";
            GridViewLabel2.Text = "";
            GridViewLabel3.Text = "";
            GridViewLabel4.Text = "";

            GridView1.DataSource = null;
            GridView1.DataBind();

            GridView2.DataSource = null;
            GridView2.DataBind();

            GridView3.DataSource = null;
            GridView3.DataBind();

            GridView4.DataSource = null;
            GridView4.DataBind();

            currentColumnIndex = 0;
        }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    cell.Width = Unit.Pixel(75);
                    cell.Text = cell.Text.PadLeft(4, '0');
                    cell.HorizontalAlign = HorizontalAlign.Center;

                    if (cell.Text.Contains("Conv. YTD Cumulative vs Prior Conv. YTD"))
                    {
                        deltaScoreColumnIndex = currentColumnIndex;

                        cell.Attributes.Add("style", "background-color:yellow;color:#000;background-image:none !important;");
                    }
                    else if (cell.Text == location)
                    {
                        TableCell tableCell = cell;

                        tableCell.Text = tableCell.Text + Environment.NewLine + "Current Quarter to Date";
                    }

                    ++currentColumnIndex;
                }
            }

            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            ListItem listItem = new ListItem(e.Row.Cells[0].Text, e.Row.Cells[0].Text);

            int num = 0;
            string upper = e.Row.Cells[0].Text.Trim().ToUpper();

            if (!ddlCriteriaList.Items.Contains(listItem))
            {
                LoadParameters();

                listItem.Attributes.Add("aria-chart-url", string.Format(chartUrl, companyId, sbParams.ToString(), sbValues.ToString(), e.Row.Cells[0].Text, location, year));
                
                ddlCriteriaList.Items.Add(listItem);
            }

            DataRowView dataItem = (DataRowView)e.Row.DataItem;

            foreach (TableCell cell in e.Row.Cells)
            {
                double result = 0.0;

                cell.Width = Unit.Pixel(75);

                if (double.TryParse(cell.Text, out result))
                {
                    cell.Text = result.ToString("##0.00");
                    cell.HorizontalAlign = HorizontalAlign.Right;
                }

                if (num > 1)
                {
                    if (standardsExceptions.Contains(upper))
                    {
                        if (num == deltaScoreColumnIndex)
                        {
                            cell.BackColor = Color.Yellow;

                            if (Convert.ToDouble(dataItem[deltaScoreColumnIndex].ToString()) > 0.0)
                            {
                                cell.Attributes.Add("style", "color:red;");
                                cell.Text = "-" + cell.Text;
                            }
                            else
                            {
                                cell.Attributes.Remove("style");
                                cell.Text = cell.Text.Replace("-", "");
                            }
                        }
                        else if (result > Convert.ToDouble(dataItem["Atlas Standard"].ToString()))
                            cell.Attributes.Add("style", "color:red;");
                    }
                    else if (num == deltaScoreColumnIndex)
                    {
                        cell.BackColor = Color.Yellow;

                        if (Convert.ToDouble(dataItem[deltaScoreColumnIndex].ToString()) < 0.0)
                            cell.Attributes.Add("style", "color:red;");
                        else
                            cell.Attributes.Remove("style");
                    }
                    else if (result < Convert.ToDouble(dataItem["Atlas Standard"].ToString()))
                        cell.Attributes.Add("style", "color:red;");
                    else
                        cell.Attributes.Remove("style");
                }

                ++num;
            }
        }

        private void LoadParameters()
        {
            sbParams = new StringBuilder();
            sbValues = new StringBuilder();

            sbParams.Append("companyId");

            sbValues.AppendFormat("{0}", companyId);

            string[] strArray = quarter.Split('-');

            int num = DateTime.DaysInMonth(Convert.ToInt32(year), Convert.ToInt32(strArray[1]));

            startMonth = string.Format("{0}/1/{1}", strArray[0], (Convert.ToInt32(strArray[0]) > Convert.ToInt32(strArray[1]) ? Convert.ToInt32(year) - 1 : Convert.ToInt32(year)));
            endMonth = string.Format("{0}/{1}/{2}", strArray[1], num, year);

            sbParams.Append(",WCCStartDate");
            sbValues.AppendFormat(",{0}", startMonth);
            sbParams.Append(",WCCEndDate");
            sbValues.AppendFormat(",{0}", endMonth);
            sbParams.Append(",LocationCode");
            sbValues.AppendFormat(",{0}", location);
        }
    }
}