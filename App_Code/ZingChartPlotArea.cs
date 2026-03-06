// Decompiled with JetBrains decompiler
// Type: ZingChartPlotArea
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System.Runtime.Serialization;

[DataContract]
public class ZingChartPlotArea
{
  [DataMember(EmitDefaultValue = false, Name = "margin")]
  public string Margin;
  [DataMember(EmitDefaultValue = false, Name = "adjust-layout")]
  public string AdjustLayout;

  public ZingChartPlotArea()
  {
    this.Margin = "45 40 90 60";
    this.AdjustLayout = "true";
  }
}
