// Decompiled with JetBrains decompiler
// Type: NCDDiary
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System;

public class NCDDiary
{
  public int pkDiaryID { get; set; }

  public int CompanyID { get; set; }

  public string ClaimNumber { get; set; }

  public DateTime ReportDate { get; set; }

  public DateTime NextReportDate { get; set; }

  public string DiaryType { get; set; }

  public string Comments { get; set; }

  public string Mode { get; set; }

  public bool CompletedTF { get; set; }

  public string UpdatedBy { get; set; }

  public int UserID { get; set; }

  public string AttachmentFile { get; set; }

  public string AttachmentFileName { get; set; }

  public string DownloadUrl { get; set; }

  public DateTime CompletedDate { get; set; }
}
