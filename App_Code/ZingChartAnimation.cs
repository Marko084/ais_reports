// Decompiled with JetBrains decompiler
// Type: ZingChartAnimation
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System.Runtime.Serialization;

[DataContract]
public class ZingChartAnimation
{
  [DataMember(EmitDefaultValue = false, Name = "effect")]
  public string Effect;
  [DataMember(EmitDefaultValue = false, Name = "speed")]
  public string Speed;
  [DataMember(EmitDefaultValue = false, Name = "method")]
  public string Method;
  [DataMember(EmitDefaultValue = false, Name = "sequence")]
  public string Sequence;
}
