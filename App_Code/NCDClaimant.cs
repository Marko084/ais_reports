// Decompiled with JetBrains decompiler
// Type: NCDClaimant
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System;

public class NCDClaimant
{
  public int pkClaimantID { get; set; }

  public int CompanyID { get; set; }

  public string ClaimNumber { get; set; }

  public string Name { get; set; }

  public string Address { get; set; }

  public string Email { get; set; }

  public string Phone1 { get; set; }

  public string Phone2 { get; set; }

  public string SSN { get; set; }

  public DateTime DOB { get; set; }

  public DateTime HireDate { get; set; }

  public string Gender { get; set; }

  public string MaritalStatus { get; set; }

  public int NbrOfDependents { get; set; }

  public string JobTitle { get; set; }

  public string JobStatus { get; set; }

  public string CurrentWage { get; set; }

  public string WageType { get; set; }

  public double ClaimantReserveAmount { get; set; }

  public double ClaimantAllocatedAmount { get; set; }

  public double ClaimantUnallocatedAmount { get; set; }

  public double PropertyDamageReserveAmount { get; set; }

  public double PropertyDamagePaidAmount { get; set; }

  public double PropertyDamageIncurredAmount { get; set; }

  public double BodilyInjuryReserveAmount { get; set; }

  public double BodilyInjuryPaidAmount { get; set; }

  public double BodilyInjuryIncurredAmount { get; set; }

  public double ExpensesReserveAmount { get; set; }

  public double ExpensesAllocatedAmount { get; set; }

  public double ExpensesIncurredAmount { get; set; }

  public string Mode { get; set; }

  public string UpdatedBy { get; set; }

  public int UserID { get; set; }
}
