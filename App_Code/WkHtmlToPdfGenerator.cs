// Decompiled with JetBrains decompiler
// Type: WkHtmlToPdfGenerator
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.Security;

public class WkHtmlToPdfGenerator : IPdfGenerator
{
  private const int maxFileSize = 32768;
  private const int maxWaitTime = 60000;
  private string userName;
  private string password;

  public void ConvertToPdf(string url, Stream outputStream)
  {
    using (Process process = this.CreateProcess())
    {
      string str = " - ";
      process.StartInfo.Arguments = url + "  " + str;
      process.Start();
      this.WriteToOutput(process, outputStream);
      process.WaitForExit(60000);
    }
  }

  private void WriteToOutput(Process proc, Stream outputStream)
  {
    byte[] buffer = new byte[32768];
    while (true)
    {
      int count = proc.StandardOutput.BaseStream.Read(buffer, 0, buffer.Length);
      if (count > 0)
        outputStream.Write(buffer, 0, count);
      else
        break;
    }
  }

  private Process CreateProcess()
  {
    this.LoadSecurityContext();
    string str1 = HttpContext.Current.Server.MapPath("~/cgi-bin");
    string str2 = str1 + "\\wkhtmltopdf.exe ";
    return new Process()
    {
      StartInfo = {
        CreateNoWindow = true,
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        RedirectStandardInput = true,
        UseShellExecute = false,
        FileName = str2,
        WorkingDirectory = str1
      }
    };
  }

  private void LoadSecurityContext()
  {
    HttpContext current = HttpContext.Current;
    string formsCookieName = FormsAuthentication.FormsCookieName;
    HttpCookie cookie = current.Request.Cookies[formsCookieName];
    try
    {
      if (!current.Request.IsAuthenticated)
        return;
      string[] strArray = ((FormsIdentity) HttpContext.Current.User.Identity).Ticket.UserData.Split(';');
      this.userName = strArray[8];
      this.password = strArray[9];
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
}
