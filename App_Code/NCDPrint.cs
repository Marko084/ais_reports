// Decompiled with JetBrains decompiler
// Type: NCDPrint
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using SelectPdf;

public class NCDPrint
{
  public string HtmlContent { get; set; }

  public void GetPDF() => new HtmlToPdf()
  {
    Options = {
      PdfPageSize = PdfPageSize.A4,
      PdfPageOrientation = PdfPageOrientation.Portrait
    }
  }.ConvertHtmlString(this.HtmlContent).Save();
}
