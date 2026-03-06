// Decompiled with JetBrains decompiler
// Type: ZingChartGuide
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System.Runtime.Serialization;

[DataContract]
public class ZingChartGuide
{
  [DataMember(EmitDefaultValue = false, Name = "line-color")]
  public string LineColor;
  [DataMember(EmitDefaultValue = false, Name = "line-style")]
  public string LineStyle;
  [DataMember(EmitDefaultValue = false, Name = "line-width")]
  public string LineWidth;
  [DataMember(EmitDefaultValue = false, Name = "alpha")]
  public string Alpha;

  public ZingChartGuide()
  {
    this.LineColor = "#333";
    this.LineStyle = "solid";
    this.LineWidth = "1";
    this.Alpha = "0.5";
  }
}
