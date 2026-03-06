using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AISReports.App_Code
{
    public class ChildFieldEvent : IPdfPCellEvent
    {
        protected PdfFormField parent;
        protected PdfFormField kid;
        protected float padding;

        public ChildFieldEvent(PdfFormField parent, PdfFormField kid, float padding)
        {
            this.parent = parent;
            this.kid = kid;
            this.padding = padding;
        }

        public void CellLayout(PdfPCell cell, Rectangle rect, PdfContentByte[] cb)
        {
            this.parent.AddKid(this.kid);
            this.kid.SetWidget(new Rectangle(rect.GetLeft(this.padding), rect.GetBottom(this.padding), rect.GetRight(this.padding), rect.GetTop(this.padding)), PdfAnnotation.HIGHLIGHT_INVERT);
        }
    }
}