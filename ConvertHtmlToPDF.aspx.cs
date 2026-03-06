using NReco.PdfGenerator;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AISReports
{
    public partial class ConvertHtmlToPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = Request.QueryString["rid"];
            string empty = string.Empty;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();

                command.CommandType = CommandType.Text;
                command.CommandText = string.Format("select ReportHtml from cms_ReportRequests where pkReportRequestID={0}", str);

                empty = command.ExecuteScalar().ToString();
            }

            HtmlToPdfConverter htmlToPdfConverter = new HtmlToPdfConverter();

            MemoryStream output = new MemoryStream();

            htmlToPdfConverter.GeneratePdf(empty.ToString(), (string)null, (Stream)output);

            output.Close();

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AppendHeader("content-disposition", string.Format("attachment;filename=Report-{0}.pdf", DateTime.Now.ToString("yyyyMMddhhmmss")));
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(output.ToArray());
            Response.End();
        }
    }
}