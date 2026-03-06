// Decompiled with JetBrains decompiler
// Type: NCDDocument
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

public class NCDDocument
{
  public int pkDocumentID { get; set; }

  public int CompanyID { get; set; }

  public int UserID { get; set; }

  public string GroupName { get; set; }

  public string GroupDescription { get; set; }

  public string DocumentName { get; set; }

  public string DocumentFile { get; set; }

  public string QueryType { get; set; }

  public void Add()
  {
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = "cms_InsertDocument";
      command.Parameters.AddWithValue("CompanyID", (object) this.CompanyID);
      command.Parameters.AddWithValue("UserID", (object) this.UserID);
      command.Parameters.AddWithValue("GroupName", (object) this.GroupName);
      command.Parameters.AddWithValue("GroupDescription", (object) this.GroupDescription);
      command.Parameters.AddWithValue("DocumentName", (object) Path.GetFileNameWithoutExtension(this.DocumentName));
      command.Parameters.AddWithValue("DocumentType", (object) this.GetMimeType());
      command.Parameters.AddWithValue("DocumentExtension", (object) Path.GetExtension(this.DocumentName).ToLower().Replace(".", ""));
      command.Parameters.AddWithValue("DocumentFile", (object) ((IEnumerable<string>) this.DocumentFile.Split(new string[1]
      {
        "base64,"
      }, StringSplitOptions.None)).Last<string>());
      command.ExecuteNonQuery();
    }
  }

  public void Update()
  {
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = "cms_UpdateDocument";
      command.Parameters.AddWithValue("pkDocumentID", (object) this.pkDocumentID);
      command.Parameters.AddWithValue("CompanyID", (object) this.CompanyID);
      command.Parameters.AddWithValue("UserID", (object) this.UserID);
      command.Parameters.AddWithValue("GroupName", (object) this.GroupName);
      command.Parameters.AddWithValue("GroupDescription", (object) this.GroupDescription);
      command.Parameters.AddWithValue("DocumentName", (object) Path.GetFileNameWithoutExtension(this.DocumentName));
      command.Parameters.AddWithValue("DocumentType", (object) this.GetMimeType());
      command.Parameters.AddWithValue("DocumentExtension", (object) Path.GetExtension(this.DocumentName).ToLower().Replace(".", ""));
      command.Parameters.AddWithValue("DocumentFile", (object) ((IEnumerable<string>) this.DocumentFile.Split(new string[1]
      {
        "base64,"
      }, StringSplitOptions.None)).Last<string>());
      command.ExecuteNonQuery();
    }
  }

  public void Delete()
  {
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = "cms_DeleteDocument";
      command.Parameters.AddWithValue("pkDocumentID", (object) this.pkDocumentID);
      command.ExecuteNonQuery();
    }
  }

  private string GetMimeType()
  {
    string mimeType = "application/octet-stream";
    try
    {
      string str = Path.GetExtension(this.DocumentName).ToLower().Replace(".", "");
      if (str == "docx")
        mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
      else if (str == "doc")
        mimeType = "application/msword";
      else if (str == "xls")
        mimeType = "application/vnd.ms-excel";
      else if (str == "xlsx")
        mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
      else if (str == "html" || str == "htm")
        mimeType = "text/html";
      else if (str == "png")
        mimeType = "image/png";
      else if (str == "jpg" || str == "jpeg")
        mimeType = "image/jpeg";
      else if (str == "bmp")
        mimeType = "image/bmp";
      else if (str == "mpeg" || str == "mp4")
        mimeType = "video/mpeg";
      else if (str == "pptx")
        mimeType = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
      else if (str == "ppt")
        mimeType = "application/vnd.ms-powerpoint";
      else if (str == "tiff")
        mimeType = "image/tiff";
      else if (str == "txt")
        mimeType = "text/plain";
      else if (str == "pdf")
        mimeType = "application/pdf";
    }
    catch
    {
    }
    return mimeType;
  }
}
