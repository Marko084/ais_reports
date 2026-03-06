// Decompiled with JetBrains decompiler
// Type: IAISData
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System.ServiceModel;

[ServiceContract]
public interface IAISData
{
  [OperationContract]
  void DoWork();
}
