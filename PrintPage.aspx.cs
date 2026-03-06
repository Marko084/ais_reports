using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AIS;
using AISReports.App_Code;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Text;

namespace AISReports
{
    public partial class PrintPage : System.Web.UI.Page
    {
        #region
        private iTextSharp.text.Font tableCellFont = new iTextSharp.text.Font(FontFactory.GetFont("Calibri").BaseFont, 8f, 0, BaseColor.BLACK);
        private iTextSharp.text.Font tableCellHeaderFont = new iTextSharp.text.Font(FontFactory.GetFont("Calibri").BaseFont, 8f, 1, BaseColor.BLACK);
        private iTextSharp.text.Font tableDayCellFont = new iTextSharp.text.Font(FontFactory.GetFont("Calibri").BaseFont, 6f, 0, BaseColor.BLACK);
        private iTextSharp.text.Font tableDayCellHeaderFont = new iTextSharp.text.Font(FontFactory.GetFont("Calibri").BaseFont, 6f, 1, BaseColor.BLACK);
        private BaseColor yellowColor = new BaseColor((int)byte.MaxValue, (int)byte.MaxValue, 0);
        private BaseColor cyanColor = new BaseColor(0, 229, 238);
        private BaseColor redColor = new BaseColor((int)byte.MaxValue, 0, 0);
        private BaseColor whiteColor = new BaseColor((int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);
        private BaseColor pinkColor = new BaseColor((int)byte.MaxValue, 0, (int)byte.MaxValue);
        private MemoryStream stream;
        private PdfWriter writer;
        private Document doc;
        private string tableName;
        private string keyName;
        private string keyValue;
        private string companyID;
        private string currentDate;
        private string queryType;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            string data = Request.QueryString["pdata"];
            keyName = Request.QueryString["kn"];
            keyValue = Request.QueryString["kv"];
            tableName = Request.QueryString["tn"];
            companyID = Request.QueryString["cid"];
            currentDate = Request.QueryString["cdate"];
            queryType = Request.QueryString["qtype"];

            if (data != null)
                PrintEditClaimsData(data);
            else if (currentDate != null)
                PrintLytlesWorkbookByDay();
            else if (queryType == "newhire")
                PrintNewHire();
            else
                PrintOnePage();
        }

        private void PrintEditClaimsData(string data)
        {
            doc = new Document(PageSize.A4);
            stream = new MemoryStream();
            writer = PdfWriter.GetInstance(doc, (Stream)stream);

            doc.Open();

            PdfPTable pdfPtable = new PdfPTable(2);

            pdfPtable.HeaderRows = 0;
            pdfPtable.DefaultCell.Border = 0;
            pdfPtable.DefaultCell.BorderColor = BaseColor.WHITE;
            pdfPtable.DefaultCell.Padding = 0.0f;

            string str1 = data;
            char[] chArray = new char[1] { '|' };

            foreach (string str2 in str1.Split(chArray))
            {
                string str3 = ((IEnumerable<string>)str2.Split('~')).First<string>();
                string str4 = ((IEnumerable<string>)str2.Split('~')).Last<string>();

                PdfPCell cell1 = new PdfPCell();

                cell1.Border = 0;
                cell1.Padding = 0.0f;
                cell1.AddElement((IElement)new Phrase(str3, tableCellFont));

                pdfPtable.AddCell(cell1);

                PdfPCell cell2 = new PdfPCell();

                cell2.Border = 0;
                cell2.Padding = 0.0f;
                cell2.AddElement((IElement)new Phrase(str4, tableCellFont));

                pdfPtable.AddCell(cell2);
            }

            doc.Add((IElement)pdfPtable);
            doc.Close();

            string str5 = string.Format("attachment; filename={0}_{1}.pdf", "ClaimsScreenPrint", DateTime.Now.ToString("yyyyMMddhhmmss"));

            Response.ClearContent();
            Response.AddHeader("content-disposition", str5);
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(stream.ToArray());
            Response.End();
        }

        private void PrintCheckRequest()
        {
            string str1 = Request.QueryString["crkfld"];
            string str2 = Request.QueryString["crid"];
            string str3 = Request.QueryString["tbl"];

            doc = new Document(PageSize.A4);
            stream = new MemoryStream();
            writer = PdfWriter.GetInstance(doc, (Stream)stream);

            doc.Open();

            PdfPTable pdfPtable1 = new PdfPTable(3);
            PdfPTable pdfPtable2 = new PdfPTable(2);
            PdfPTable pdfPtable3 = new PdfPTable(2);
            PdfPTable pdfPtable4 = new PdfPTable(2);
            PdfPTable pdfPtable5 = new PdfPTable(2);

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("select * from {0} where {1}={2} ", str3, str1, str2);

            DAL.ReturnDataReader(stringBuilder.ToString(), CommandType.Text);
        }

        private void PrintOnePage()
        {
            doc = new Document(PageSize.A4);
            stream = new MemoryStream();
            writer = PdfWriter.GetInstance(doc, (Stream)stream);

            writer.PageEvent = (IPdfPageEvent)new HeaderPageEvent("Lytle's Workbook Item", HttpContext.Current.Server.MapPath("~\\images\\lytles-logo.png"));

            string str1 = "RegNumber|Name|Account|OfficeAssigned|PKStartDate|PKEndDate|LDStartDate|LDEndDate|DELStartDate|DELEndDate|ShipmentDelivered|CSRContactedTransfereePK|CSRContactedTransfereeLD|CSRContactedTransfereeDEL|Cancelled|PickupAndDelivery|Type|DateSpread|Weight|MoveCoordinator|OriginDrivers|DestinationDrivers|OriginHelpers|DestinationHelpers|Details|ContactedTransfereeNotes";
            doc.Open();

            PdfPTable pdfPtable = new PdfPTable(2);

            pdfPtable.HeaderRows = 0;
            pdfPtable.DefaultCell.Border = 0;
            pdfPtable.DefaultCell.BorderColor = BaseColor.WHITE;
            pdfPtable.DefaultCell.Padding = 0.0f;

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("select * from {0} where {1}={2} ", tableName, keyName, keyValue);

            SqlDataReader sqlDataReader = DAL.ReturnDataReader(stringBuilder.ToString(), CommandType.Text);

            while (sqlDataReader.Read())
            {
                string str2 = str1;
                char[] chArray = new char[1] { '|' };

                foreach (string str3 in str2.Split(chArray))
                {
                    string str4 = SplitCamelCase(str3);
                    string str5 = sqlDataReader[str3].ToString();

                    if (str3.ToUpper() != "IMPORTID")
                    {
                        int ordinal = sqlDataReader.GetOrdinal(str3);

                        if (sqlDataReader.GetDataTypeName(ordinal) == "datetime")
                            str5 = string.Format("{0:MM/dd/yyyy}", sqlDataReader[str3]);

                        PdfPCell cell1 = new PdfPCell();

                        cell1.Border = 0;
                        cell1.Padding = 0.0f;
                        cell1.AddElement((IElement)new Phrase(str4, tableCellHeaderFont));

                        pdfPtable.AddCell(cell1);

                        PdfPCell cell2 = new PdfPCell();

                        cell2.Border = 0;
                        cell2.Padding = 0.0f;
                        cell2.AddElement((IElement)new Phrase(str5, tableCellFont));

                        pdfPtable.AddCell(cell2);
                    }
                }
            }

            doc.Add((IElement)pdfPtable);
            doc.Close();

            string str6 = string.Format("attachment; filename={0}_{1}.pdf", "Doc", DateTime.Now.ToString("yyyyMMddhhmmss"));

            Response.ClearContent();
            Response.AddHeader("content-disposition", str6);
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(stream.ToArray());
            Response.End();
        }

        private void PrintNewHire()
        {
            doc = new Document(PageSize.A4);
            stream = new MemoryStream();
            writer = PdfWriter.GetInstance(doc, (Stream)stream);

            writer.PageEvent = (IPdfPageEvent)new HeaderPageEvent("Employee Information", HttpContext.Current.Server.MapPath("~\\images\\atlantic_logo_sm.png"));

            PdfFormField empty = PdfFormField.CreateEmpty(writer);

            empty.FieldName = "root";

            doc.Open();

            PdfPTable pdfPtable = new PdfPTable(2);

            pdfPtable.HeaderRows = 0;
            pdfPtable.DefaultCell.Border = 0;
            pdfPtable.DefaultCell.BorderColor = BaseColor.WHITE;
            pdfPtable.DefaultCell.Padding = 0.0f;

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("select * from {0} where {1}={2} ", tableName, keyName, keyValue);

            SqlDataReader sqlDataReader = DAL.ReturnDataReader(stringBuilder.ToString(), CommandType.Text);

            while (sqlDataReader.Read())
            {
                int fieldCount = sqlDataReader.FieldCount;

                for (int ordinal1 = 0; ordinal1 < fieldCount; ++ordinal1)
                {
                    string name = sqlDataReader.GetName(ordinal1);
                    string str1 = SplitCamelCase(name);
                    string str2 = sqlDataReader[name].ToString();

                    if (name.ToUpper() != "IMPORTID" && name.ToUpper() != "BATCHID" && name.ToUpper() != "COMPANYID")
                    {
                        int ordinal2 = sqlDataReader.GetOrdinal(name);

                        if (sqlDataReader.GetDataTypeName(ordinal2) == "datetime")
                            str2 = string.Format("{0:MM/dd/yyyy}", sqlDataReader[name]);

                        PdfPCell cell1 = new PdfPCell();

                        cell1.Border = 0;
                        cell1.Padding = 0.0f;
                        cell1.AddElement((IElement)new Phrase(str1, tableCellHeaderFont));

                        pdfPtable.AddCell(cell1);

                        PdfPCell cell2 = new PdfPCell();

                        cell2.Border = 0;
                        cell2.Padding = 0.0f;
                        cell2.AddElement((IElement)new Phrase(str2, tableCellFont));

                        pdfPtable.AddCell(cell2);
                    }
                }
            }

            doc.Add((IElement)pdfPtable);
            writer.AddAnnotation((PdfAnnotation)empty);
            doc.Close();

            string str = string.Format("attachment; filename={0}_{1}.pdf", "Doc", DateTime.Now.ToString("yyyyMMddhhmmss"));

            Response.ClearContent();
            Response.AddHeader("content-disposition", str);
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(stream.ToArray());
            Response.End();
        }

        private void PrintLytlesWorkbookByDay()
        {
            doc = new Document(PageSize.A4.Rotate(), 10f, 10f, 40f, 10f);
            stream = new MemoryStream();
            writer = PdfWriter.GetInstance(doc, (Stream)stream);

            writer.PageEvent = (IPdfPageEvent)new HeaderPageEvent(string.Format("Workbook {0}", Convert.ToDateTime(currentDate).ToString("ddd MMM dd")));
            
            PdfFormField empty = PdfFormField.CreateEmpty(writer);

            empty.FieldName = "root";

            string str1 = "CSRContactedTransfereePK|CSRContactedTransfereeLD|CSRContactedTransfereeDEL|Account|Name|PickupAndDelivery|Type|DateSpread|Weight|MoveCoordinator|OriginDrivers|DestinationDrivers|OriginHelpers|DestinationHelpers|Cancelled|ShipmentDelivered";
            
            doc.Open();

            PdfPTable pdfPtable = new PdfPTable(((IEnumerable<string>)str1.Split('|')).Count<string>());

            pdfPtable.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
            pdfPtable.HeaderRows = 0;
            pdfPtable.DefaultCell.Padding = 0.0f;

            float[] relativeWidths = new float[15]
            {
              50f,
              50f,
              50f,
              150f,
              150f,
              250f,
              50f,
              75f,
              100f,
              100f,
              100f,
              100f,
              100f,
              60f,
              60f
            };

            pdfPtable.SetWidths(relativeWidths);

            string str2 = str1;
            char[] chArray1 = new char[1] { '|' };

            foreach (string str3 in str2.Split(chArray1))
            {
                PdfPCell cell = new PdfPCell();

                cell.Padding = 2f;
                cell.HorizontalAlignment = 1;

                if (str3.ToLower().Contains("csrcontacted"))
                    cell.AddElement((IElement)new Phrase(SplitCamelCase(str3.Replace("CSRContactedTransferee", "CSRCntdTransf")), tableDayCellHeaderFont));
                else
                    cell.AddElement((IElement)new Phrase(SplitCamelCase(str3), tableDayCellHeaderFont));

                pdfPtable.AddCell(cell);
            }

            SqlDataReader sqlDataReader = DAL.ReturnDataReader("Lytles_GetWorkbook", "CompanyID,UserID,CurrentDate", string.Format("{0},0,{1}", companyID, currentDate));
            
            bool flag = true;

            while (sqlDataReader.Read())
            {
                string officeNumber = sqlDataReader["officeAssigned"].ToString();

                if (!flag)
                {
                    string str4 = str1;
                    char[] chArray2 = new char[1] { '|' };

                    foreach (string name in str4.Split(chArray2))
                    {
                        string str5 = sqlDataReader[name].ToString();
                        int ordinal = sqlDataReader.GetOrdinal(name);

                        if (sqlDataReader.GetDataTypeName(ordinal) == "bit")
                            str5 = !(str5.ToLower() == "true") ? (!(str5.ToLower() == "false") ? "No" : "No") : "Yes";

                        PdfPCell cell = new PdfPCell();

                        cell.Padding = 0.5f;
                        cell.AddElement((IElement)new Phrase(str5, tableDayCellFont));
                        cell.BackgroundColor = GetBaseColorByOffice(officeNumber);

                        pdfPtable.AddCell(cell);
                    }
                }

                flag = false;
            }

            doc.Add((IElement)pdfPtable);
            writer.AddAnnotation((PdfAnnotation)empty);
            doc.Close();

            string str6 = string.Format("attachment; filename={0}_{1}.pdf", "Doc", DateTime.Now.ToString("yyyyMMddhhmmss"));

            Response.ClearContent();
            Response.AddHeader("content-disposition", str6);
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(stream.ToArray());
            Response.End();
        }

        private BaseColor GetBaseColorByOffice(string officeNumber)
        {
            if (officeNumber == "9999")
                return cyanColor;
            if (officeNumber == "385")
                return redColor;
            if (officeNumber == "1385")
                return pinkColor;
            return officeNumber == "1305" ? yellowColor : whiteColor;
        }

        private string SplitCamelCase(string str)
        {
            try
            {
                return Regex.Replace(Regex.Replace(str, "(\\P{Ll})(\\P{Ll}\\p{Ll})", "$1 $2"), "(\\p{Ll})(\\P{Ll})", "$1 $2");
            }
            catch (Exception ex)
            {
            }
            return str;
        }
    }
}