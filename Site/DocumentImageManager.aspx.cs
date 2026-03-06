using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AISReports.Site
{
    public partial class DocumentImageManager : System.Web.UI.Page
    {
        #region
        private string companyID;
        private string categoryName;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            companyID = Request.QueryString["cid"];
            categoryName = Request.QueryString["catname"];

            int num = Page.IsPostBack ? 1 : 0;

            LoadMasterStyleSheets();

            if (categoryName == null)
                return;

            txtGroupName.Text = categoryName;
        }

        protected void btnAdd_Click(object sender, EventArgs e) => AddDocument();

        private string ValidateFileInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();

            bool flag = false;

            lblMessage.Attributes.Remove("class");
            lblMessage.Attributes.Add("class", "ui-state-error ui-corner-all");

            stringBuilder.Append("<ul>");

            if (txtGroupName.Text.Trim().Length == 0)
            {
                stringBuilder.AppendFormat("<li><b>{0}</b> is required.</li>", "Category");
                flag = true;
            }

            if (!fuFile.HasFile)
            {
                stringBuilder.Append("<li>You must select a file to upload.</li>");
                flag = true;
            }

            stringBuilder.Append("</ul>");

            return flag ? stringBuilder.ToString() : "";
        }

        private string GetFileContentType(string extension)
        {
            string fileContentType = "application/octet-stream";

            switch (extension.ToLower())
            {
                case ".doc":
                    fileContentType = "application/vnd.ms-word";
                    break;
                case ".docx":
                    fileContentType = "application/vnd.ms-word";
                    break;
                case ".xls":
                    fileContentType = "application/vnd.ms-excel";
                    break;
                case ".xlsx":
                    fileContentType = "application/vnd.ms-excel";
                    break;
                case ".jpg":
                    fileContentType = "image/jpg";
                    break;
                case ".png":
                    fileContentType = "image/png";
                    break;
                case ".gif":
                    fileContentType = "image/gif";
                    break;
                case ".bmp":
                    fileContentType = "image/bitmap";
                    break;
                case ".pdf":
                    fileContentType = "application/pdf";
                    break;
                case ".ppt":
                    fileContentType = "application/vnd.ms-powerpoint";
                    break;
            }

            return fileContentType;
        }

        private void AddDocument()
        {
            if (ValidateFileInfo().Length == 0)
            {
                try
                {
                    string extension = Path.GetExtension(fuFile.PostedFile.FileName);
                    string fileContentType = GetFileContentType(extension);

                    byte[] buffer = new byte[fuFile.PostedFile.ContentLength];

                    if (txtFileName.Text.Trim().Length == 0)
                        txtFileName.Text = Path.GetFileNameWithoutExtension(fuFile.PostedFile.FileName);

                    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
                    {
                        sqlConnection.Open();

                        SqlCommand command = sqlConnection.CreateCommand();

                        StringBuilder stringBuilder = new StringBuilder();

                        stringBuilder.Append("insert into cms_Documents(CompanyID,UserID,GroupName,GroupDescription,DocumentName,DocumentType,DocumentFile,DocumentExtension) ");
                        stringBuilder.Append("values(@CompanyID,@UserID,@GroupName,@GroupDescription,@DocumentName,@DocumentType,@DocumentFile,@DocumentExtension)");
                        
                        using (Stream inputStream = fuFile.PostedFile.InputStream)
                            inputStream.Read(buffer, 0, buffer.Length);

                        command.CommandType = CommandType.Text;
                        command.CommandText = stringBuilder.ToString();

                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("CompanyID", companyID);
                        command.Parameters.AddWithValue("UserID", 0);
                        command.Parameters.AddWithValue("GroupName", txtGroupName.Text.Trim());
                        command.Parameters.AddWithValue("GroupDescription", "");
                        command.Parameters.AddWithValue("DocumentName", txtFileName.Text.Trim());
                        command.Parameters.AddWithValue("DocumentType", fileContentType);
                        command.Parameters.AddWithValue("DocumentFile", buffer);
                        command.Parameters.AddWithValue("DocumentExtension", extension.Replace(".", ""));

                        command.ExecuteNonQuery();
                    }

                    lblMessage.Text = string.Format("The file named <b>{0}</b> was successfully saved.", txtFileName.Text);
                    lblMessage.Visible = true;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.ToString();
                    lblMessage.Visible = true;
                }
            }
            else
            {
                lblMessage.Text = ValidateFileInfo();
                lblMessage.Visible = true;
            }
        }

        private void LoadMasterStyleSheets()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "cms_GetMasterStyles";

                command.Parameters.AddWithValue("CompanyID", companyID);

                SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (sqlDataReader.Read())
                {
                    string relativeUrl = sqlDataReader["StyleSheetUrl"].ToString();

                    Page.Header.Controls.Add((Control)new Literal()
                    {
                        Text = ("<link href=\"" + Page.ResolveUrl(relativeUrl) + "?\" type=\"text/css\" rel=\"stylesheet\" />")
                    });
                }

                sqlDataReader.Close();
                sqlDataReader.Dispose();
            }
        }
    }
}