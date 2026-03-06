// Decompiled with JetBrains decompiler
// Type: ZingChartLabel
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System.Runtime.Serialization;

[DataContract]
public class ZingChartLabel
{
  [DataMember(EmitDefaultValue = false, Name = "text")]
  public string Text;
  [DataMember(EmitDefaultValue = false, Name = "color")]
  public string Color;
  [DataMember(EmitDefaultValue = false, Name = "font-size")]
  public string FontSize;
}
