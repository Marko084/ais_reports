using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AISReports.App_Code
{
    public class HeaderPageEvent : PdfPageEventHelper
    {
        private iTextSharp.text.Font pageHeaderFont = new iTextSharp.text.Font(FontFactory.GetFont("Calibri").BaseFont, 12f, 1, BaseColor.BLACK);
        private string headerTitle;
        private string imgLogoPath;

        public HeaderPageEvent()
        {
        }

        public HeaderPageEvent(string title) => headerTitle = title;

        public HeaderPageEvent(string title, string logoPath)
        {
            headerTitle = title;
            imgLogoPath = logoPath;
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfPTable pdfPtable = new PdfPTable(1);

            pdfPtable.DefaultCell.Border = 0;
            pdfPtable.DefaultCell.BorderColor = BaseColor.WHITE;
            pdfPtable.DefaultCell.Padding = 0.0f;
            pdfPtable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

            PdfPTable table = new PdfPTable(2);

            table.DefaultCell.Border = 0;
            table.DefaultCell.BorderColor = BaseColor.WHITE;
            table.DefaultCell.Padding = 0.0f;

            if (imgLogoPath != null)
            {
                Image instance = Image.GetInstance(imgLogoPath);

                instance.ScalePercent(50f);

                PdfPCell cell = new PdfPCell(instance);

                cell.Border = 0;
                cell.HorizontalAlignment = 2;
                cell.Padding = 0.0f;

                table.AddCell(cell);
            }

            PdfPCell cell1 = new PdfPCell(new Phrase("\n" + headerTitle, pageHeaderFont));

            cell1.HorizontalAlignment = 0;
            cell1.Border = 0;
            cell1.Padding = 0.0f;

            table.AddCell(cell1);

            PdfPCell cell2 = new PdfPCell(table);

            cell2.Border = 0;
            cell2.Padding = 0.0f;

            pdfPtable.AddCell(cell2);

            double num = (double)pdfPtable.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height, writer.DirectContent);
        }
    }

}