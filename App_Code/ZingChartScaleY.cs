// Decompiled with JetBrains decompiler
// Type: ZingChartScaleY
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System.Runtime.Serialization;

[DataContract]
public class ZingChartScaleY
{
  [DataMember(EmitDefaultValue = false, Name = "line-color")]
  public string LineColor;
  [DataMember(EmitDefaultValue = false, Name = "line-width")]
  public string LineWidth;
  [DataMember(EmitDefaultValue = false, Name = "label")]
  public ZingChartLabel Label;
  [DataMember(EmitDefaultValue = false, Name = "item")]
  public ZingChartItem Item;
  [DataMember(EmitDefaultValue = false, Name = "guide")]
  public ZingChartGuide Guide;
  [DataMember(EmitDefaultValue = false, Name = "min-value")]
  public string MinimumValue;

  public ZingChartScaleY()
  {
    this.Label = new ZingChartLabel();
    this.Item = new ZingChartItem();
    this.Guide = new ZingChartGuide();
    this.MinimumValue = "0";
  }
}
