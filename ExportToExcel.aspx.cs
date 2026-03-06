using AIS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Security.Cryptography;
using System.Xml;
using OfficeOpenXml;
using OfficeOpenXml.Encryption;
using OfficeOpenXml.Packaging;
using OfficeOpenXml.Table;
using OfficeOpenXml.Utils;


namespace AISReports
{
    public partial class ExportToExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string storedProcedureName = Request.QueryString["query"];
            string str1 = "GridExport";
            string parameterNames = Request.QueryString["qpn"] == null ? Session["QUERY_PARAMETERS"].ToString() : Request.QueryString["qpn"];
            string parameterValues = Request.QueryString["qpv"] == null ? Session["QUERY_VALUES"].ToString() : Request.QueryString["qpv"];

            if (Request.QueryString["fname"] != null)
                str1 = Request.QueryString["fname"];

            DataSet dataSet = DAL.ReturnDataSet(storedProcedureName, parameterNames, parameterValues, "SurveyResults", "SurveyResult");

            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                DataTable table = dataSet.Tables[0];

                lblMessage.Text = "Data Exported.";

                string str2 = "attachment; filename=" + str1 + ".xlsx";

                Response.Clear();
                Response.Charset = "";
                Response.ContentEncoding = Encoding.UTF8;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", str2);

                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                    excelWorksheet.Cells["A1"].LoadFromDataTable((DataTable)table, true, TableStyles.None);
                    excelWorksheet.Cells[excelWorksheet.Dimension.Address].AutoFitColumns();

                    Response.BinaryWrite(excelPackage.GetAsByteArray());
                }

                Response.Flush();
                Response.End();
            }
            else
                lblMessage.Text = "Nothing to Export.";
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            foreach (TableCell cell in e.Row.Cells)
            {
                DateTime result = DateTime.MinValue;
                DateTime.TryParse(cell.Text, out result);
                if (cell.Text.Trim().StartsWith("0") && result == DateTime.MinValue)
                    cell.Text += "&nbsp;";
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
    }
}
