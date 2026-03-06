// Decompiled with JetBrains decompiler
// Type: ZingChartLegend
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System.Runtime.Serialization;

[DataContract]
public class ZingChartLegend
{
  [DataMember(EmitDefaultValue = false, Name = "margin")]
  public string Margin;
  [DataMember(EmitDefaultValue = false, Name = "toggle-action")]
  public string ToggleAction;
  [DataMember(EmitDefaultValue = false, Name = "shadow")]
  public string Shadow;
  [DataMember(EmitDefaultValue = false, Name = "border-radius")]
  public string BorderRadius;
  [DataMember(EmitDefaultValue = false, Name = "background-color")]
  public string BackgroundColor;
  [DataMember(EmitDefaultValue = false, Name = "border-color")]
  public string BorderColor;
  [DataMember(EmitDefaultValue = false, Name = "layout")]
  public string Layout;
  [DataMember(EmitDefaultValue = false, Name = "font-color")]
  public string Color;

  public ZingChartLegend()
  {
    this.Margin = "auto auto 1% auto";
    this.ToggleAction = "remove";
    this.Shadow = "false";
    this.BorderRadius = "4";
    this.BackgroundColor = "#fff";
    this.BorderColor = "#fff";
    this.Layout = "float";
    this.Color = "#fff";
  }
}
