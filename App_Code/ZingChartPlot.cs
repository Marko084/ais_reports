// Decompiled with JetBrains decompiler
// Type: ZingChartPlot
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

[DataContract]
public class ZingChartPlot
{
  [DataMember(EmitDefaultValue = false, Name = "border-radius")]
  public string BorderRadius;
  [DataMember(EmitDefaultValue = false, Name = "slice")]
  public string Slice;
  [DataMember(EmitDefaultValue = false, Name = "animation")]
  public ZingChartAnimation Animation;
  [DataMember(EmitDefaultValue = false, Name = "styles")]
  public List<string> Styles;

  public ZingChartPlot()
  {
    this.Animation = new ZingChartAnimation()
    {
      Effect = "2",
      Method = "5",
      Sequence = "1",
      Speed = "1000"
    };
    this.Styles = new List<string>()
    {
      "skyblue",
      "red",
      "orange",
      "yellow",
      "green",
      "purple",
      "brown",
      "navy",
      "tomato",
      "tan",
      "gold",
      "lime",
      "darkviolet",
      "khaki",
      "lightsteelblue",
      "maroon",
      "orangered",
      "wheat",
      "olive",
      "orchid",
      "sienna"
    };
  }
}
