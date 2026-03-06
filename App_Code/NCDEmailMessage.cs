// Decompiled with JetBrains decompiler
// Type: NCDEmailMessage
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;

public class NCDEmailMessage
{
  public string FromAddress { get; set; }

  public string ToAddress { get; set; }

  public string Subject { get; set; }

  public string Message { get; set; }

  public string CCAddress { get; set; }

  public string BCCAddress { get; set; }

  public string AttachmentFile { get; set; }

  public string AttachmentFileName { get; set; }

  public void Send()
  {
    MailMessage message = new MailMessage();
    message.To.Add(this.ToAddress);
    message.From = new MailAddress(this.FromAddress);
    message.Subject = this.Subject;
    message.Body = this.Message;
    message.IsBodyHtml = true;
    if (this.AttachmentFile != null)
    {
      string s = ((IEnumerable<string>) this.AttachmentFile.Split(new string[1]
      {
        "base64,"
      }, StringSplitOptions.None)).Last<string>();
      ContentType contentType = new ContentType()
      {
        MediaType = "application/octet-stream",
        Name = this.AttachmentFileName
      };
      message.Attachments.Add(new Attachment((Stream) new MemoryStream(Convert.FromBase64String(s)), contentType));
    }
    if (this.CCAddress != null && this.CCAddress.Trim().Length > 0)
    {
      string ccAddress = this.CCAddress;
      char[] chArray = new char[1]{ '|' };
      foreach (string addresses in ccAddress.Split(chArray))
        message.CC.Add(addresses);
    }
    if (this.BCCAddress != null && this.BCCAddress.Trim().Length > 0)
    {
      string bccAddress = this.BCCAddress;
      char[] chArray = new char[1]{ '|' };
      foreach (string addresses in bccAddress.Split(chArray))
        message.Bcc.Add(addresses);
    }
    new SmtpClient()
    {
      DeliveryMethod = SmtpDeliveryMethod.Network
    }.Send(message);
  }
}
