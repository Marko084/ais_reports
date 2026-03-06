using AIS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AISReports
{
    public partial class GetFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str1 = Request.QueryString["id"];
            string str2 = "";
            string str3 = "";
            string str4 = "";

            byte[] buffer = (byte[])null;

            SqlDataReader sqlDataReader = (SqlDataReader)DAL.ReturnDataReader(string.Format("select * from cms_Documents where pkDocumentID={0}", str1), CommandType.Text);
            
            while (sqlDataReader.Read())
            {
                str2 = sqlDataReader["DocumentName"].ToString();
                str3 = sqlDataReader["DocumentExtension"].ToString();
                str4 = sqlDataReader["DocumentType"].ToString();

                buffer = (byte[])sqlDataReader["DocumentFile"];
            }

            sqlDataReader.Close();
            sqlDataReader.Dispose();

            Response.AddHeader("Content-type", str4);
            Response.AddHeader("Content-Disposition", "attachment; filename=" + string.Format("{0}.{1}", str2, str3));
            Response.BinaryWrite(buffer);
            Response.Flush();
            Response.End();
        }
    }
}