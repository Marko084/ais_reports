// Decompiled with JetBrains decompiler
// Type: ZingChartSeriesData
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

[DataContract]
public class ZingChartSeriesData
{
  [DataMember(EmitDefaultValue = false, Name = "values")]
  public List<double> Values;
  [DataMember(EmitDefaultValue = false, Name = "text")]
  public List<string> Text;

  public ZingChartSeriesData()
  {
    this.Values = new List<double>();
    this.Text = new List<string>();
  }
}
