// Decompiled with JetBrains decompiler
// Type: CanvasJSData
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System.Runtime.Serialization;
using System.ServiceModel;

[DataContract]
[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
public class CanvasJSData
{
  public CanvasJSData GetChartData(
    string queryName,
    string pname,
    string pvalue,
    string theme,
    string chart_type)
  {
    return new CanvasJSData();
  }
}
