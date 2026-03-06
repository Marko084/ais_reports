// Decompiled with JetBrains decompiler
// Type: DriverScoreCardDetails
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System.Runtime.Serialization;

[DataContract]
public class DriverScoreCardDetails
{
  [DataMember]
  public string AtlanticClientNameSecondary;
  [DataMember]
  public string TransfereeName;
  [DataMember]
  public string RegistrationNumber;
  [DataMember]
  public string CounselorName;
  [DataMember]
  public string CounselorScore;
  [DataMember]
  public string PackerScore;
  [DataMember]
  public string DriverScore;
  [DataMember]
  public string EstimatorScore;
  [DataMember]
  public string OverallScore;
  [DataMember]
  public string Comments;
}
