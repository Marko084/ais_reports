// Decompiled with JetBrains decompiler
// Type: NCDClaimants
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public class NCDClaimants
{
  public string ClaimNumber { get; set; }

  public int CompanyID { get; set; }

  public string QueryType { get; set; }

  public int UserID { get; set; }

  public List<NCDClaimant> Claimants { get; set; }

  public NCDClaimants() => this.Claimants = new List<NCDClaimant>();

  public void Add()
  {
    NCDClaimant ncdClaimant = this.Claimants.First<NCDClaimant>();
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = "cms_InsertClaimants";
      command.Parameters.AddWithValue("CompanyID", (object) ncdClaimant.CompanyID);
      command.Parameters.AddWithValue("ClaimNumber", (object) ncdClaimant.ClaimNumber);
      command.Parameters.AddWithValue("Name", (object) ncdClaimant.Name);
      command.Parameters.AddWithValue("Address", (object) ncdClaimant.Address);
      command.Parameters.AddWithValue("Email", (object) ncdClaimant.Email);
      command.Parameters.AddWithValue("Phone1", (object) ncdClaimant.Phone1);
      command.Parameters.AddWithValue("Phone2", (object) ncdClaimant.Phone2);
      command.Parameters.AddWithValue("SSN", (object) ncdClaimant.SSN);
      command.Parameters.AddWithValue("DOB", (object) ncdClaimant.DOB);
      command.Parameters.AddWithValue("HireDate", (object) ncdClaimant.HireDate);
      command.Parameters.AddWithValue("Gender", (object) ncdClaimant.Gender);
      command.Parameters.AddWithValue("MaritalStatus", (object) ncdClaimant.MaritalStatus);
      command.Parameters.AddWithValue("NbrOfDependents", (object) ncdClaimant.NbrOfDependents);
      command.Parameters.AddWithValue("JobTitle", (object) ncdClaimant.JobTitle);
      command.Parameters.AddWithValue("JobStatus", (object) ncdClaimant.JobStatus);
      command.Parameters.AddWithValue("CurrentWage", (object) ncdClaimant.CurrentWage);
      command.Parameters.AddWithValue("WageType", (object) ncdClaimant.WageType);
      command.Parameters.AddWithValue("ClaimantReserveAmount", (object) ncdClaimant.ClaimantReserveAmount);
      command.Parameters.AddWithValue("ClaimantAllocatedAmount", (object) ncdClaimant.ClaimantAllocatedAmount);
      command.Parameters.AddWithValue("ClaimantUnallocatedAmount", (object) ncdClaimant.ClaimantUnallocatedAmount);
      command.Parameters.AddWithValue("PropertyDamageReserveAmount", (object) ncdClaimant.PropertyDamageReserveAmount);
      command.Parameters.AddWithValue("PropertyDamagePaidAmount", (object) ncdClaimant.PropertyDamagePaidAmount);
      command.Parameters.AddWithValue("PropertyDamageIncurredAmount", (object) ncdClaimant.PropertyDamageIncurredAmount);
      command.Parameters.AddWithValue("BodilyInjuryReserveAmount", (object) ncdClaimant.BodilyInjuryReserveAmount);
      command.Parameters.AddWithValue("BodilyInjuryPaidAmount", (object) ncdClaimant.BodilyInjuryPaidAmount);
      command.Parameters.AddWithValue("BodilyInjuryIncurredAmount", (object) ncdClaimant.BodilyInjuryIncurredAmount);
      command.Parameters.AddWithValue("ExpensesReserveAmount", (object) ncdClaimant.ExpensesReserveAmount);
      command.Parameters.AddWithValue("ExpensesAllocatedAmount", (object) ncdClaimant.ExpensesAllocatedAmount);
      command.Parameters.AddWithValue("ExpensesIncurredAmount", (object) ncdClaimant.ExpensesIncurredAmount);
      command.Parameters.AddWithValue("UpdatedBy", (object) ncdClaimant.UpdatedBy);
      command.Parameters.AddWithValue("UserID", (object) ncdClaimant.UserID);
      command.ExecuteNonQuery();
    }
  }

  public void Update()
  {
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      foreach (NCDClaimant claimant in this.Claimants)
      {
        command.Parameters.Clear();
        command.Parameters.AddWithValue("pkClaimantID", (object) claimant.pkClaimantID);
        if (claimant.Mode.ToLower().Trim() == "delete")
        {
          command.CommandText = "cms_DeleteClaimant";
        }
        else
        {
          if (claimant.Mode.ToLower().Trim() == "add")
            command.CommandText = "cms_InsertClaimants";
          else
            command.CommandText = "cms_UpdateClaimants";
          command.Parameters.AddWithValue("CompanyID", (object) claimant.CompanyID);
          command.Parameters.AddWithValue("ClaimNumber", (object) this.ClaimNumber);
          command.Parameters.AddWithValue("Name", (object) claimant.Name);
          command.Parameters.AddWithValue("Address", (object) claimant.Address);
          command.Parameters.AddWithValue("Email", (object) claimant.Email);
          command.Parameters.AddWithValue("Phone1", (object) claimant.Phone1);
          command.Parameters.AddWithValue("Phone2", (object) claimant.Phone2);
          command.Parameters.AddWithValue("SSN", (object) claimant.SSN);
          command.Parameters.AddWithValue("DOB", (object) claimant.DOB);
          command.Parameters.AddWithValue("HireDate", (object) claimant.HireDate);
          command.Parameters.AddWithValue("Gender", (object) claimant.Gender);
          command.Parameters.AddWithValue("MaritalStatus", (object) claimant.MaritalStatus);
          command.Parameters.AddWithValue("NbrOfDependents", (object) claimant.NbrOfDependents);
          command.Parameters.AddWithValue("JobTitle", (object) claimant.JobTitle);
          command.Parameters.AddWithValue("JobStatus", (object) claimant.JobStatus);
          command.Parameters.AddWithValue("CurrentWage", (object) claimant.CurrentWage);
          command.Parameters.AddWithValue("WageType", (object) claimant.WageType);
          command.Parameters.AddWithValue("ClaimantReserveAmount", (object) claimant.ClaimantReserveAmount);
          command.Parameters.AddWithValue("ClaimantAllocatedAmount", (object) claimant.ClaimantAllocatedAmount);
          command.Parameters.AddWithValue("ClaimantUnallocatedAmount", (object) claimant.ClaimantUnallocatedAmount);
          command.Parameters.AddWithValue("PropertyDamageReserveAmount", (object) claimant.PropertyDamageReserveAmount);
          command.Parameters.AddWithValue("PropertyDamagePaidAmount", (object) claimant.PropertyDamagePaidAmount);
          command.Parameters.AddWithValue("PropertyDamageIncurredAmount", (object) claimant.PropertyDamageIncurredAmount);
          command.Parameters.AddWithValue("BodilyInjuryReserveAmount", (object) claimant.BodilyInjuryReserveAmount);
          command.Parameters.AddWithValue("BodilyInjuryPaidAmount", (object) claimant.BodilyInjuryPaidAmount);
          command.Parameters.AddWithValue("BodilyInjuryIncurredAmount", (object) claimant.BodilyInjuryIncurredAmount);
          command.Parameters.AddWithValue("ExpensesReserveAmount", (object) claimant.ExpensesReserveAmount);
          command.Parameters.AddWithValue("ExpensesAllocatedAmount", (object) claimant.ExpensesAllocatedAmount);
          command.Parameters.AddWithValue("ExpensesIncurredAmount", (object) claimant.ExpensesIncurredAmount);
          command.Parameters.AddWithValue("UpdatedBy", (object) claimant.UpdatedBy);
          command.Parameters.AddWithValue("UserID", (object) claimant.UserID);
        }
        command.ExecuteNonQuery();
      }
    }
  }

  public void Get()
  {
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = "cms_GetClaimants";
      command.Parameters.AddWithValue("ClaimNumber", (object) this.ClaimNumber);
      command.Parameters.AddWithValue("CompanyID", (object) this.CompanyID);
      SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
      while (sqlDataReader.Read())
        this.Claimants.Add(new NCDClaimant()
        {
          pkClaimantID = Convert.ToInt32(sqlDataReader["pkClaimantID"]),
          Address = sqlDataReader["Address"].ToString(),
          BodilyInjuryIncurredAmount = Convert.ToDouble(sqlDataReader["BodilyInjuryIncurredAmount"]),
          BodilyInjuryPaidAmount = Convert.ToDouble(sqlDataReader["BodilyInjuryPaidAmount"]),
          BodilyInjuryReserveAmount = Convert.ToDouble(sqlDataReader["BodilyInjuryReserveAmount"]),
          ClaimantAllocatedAmount = Convert.ToDouble(sqlDataReader["ClaimantAllocatedAmount"]),
          ClaimantUnallocatedAmount = Convert.ToDouble(sqlDataReader["ClaimantUnallocatedAmount"]),
          ClaimNumber = sqlDataReader["ClaimNumber"].ToString(),
          ClaimantReserveAmount = Convert.ToDouble(sqlDataReader["ClaimantReserveAmount"]),
          CompanyID = Convert.ToInt32(sqlDataReader["CompanyID"]),
          Email = sqlDataReader["Email"].ToString(),
          DOB = Convert.ToDateTime(sqlDataReader["DOB"]),
          HireDate = Convert.ToDateTime(sqlDataReader["HireDate"]),
          Gender = sqlDataReader["Gender"].ToString(),
          MaritalStatus = sqlDataReader["MaritalStatus"].ToString(),
          NbrOfDependents = Convert.ToInt32(sqlDataReader["NbrOfDependents"]),
          JobTitle = sqlDataReader["JobTitle"].ToString(),
          JobStatus = sqlDataReader["JobStatus"].ToString(),
          CurrentWage = sqlDataReader["CurrentWage"].ToString(),
          WageType = sqlDataReader["WageType"].ToString(),
          ExpensesAllocatedAmount = Convert.ToDouble(sqlDataReader["ExpensesAllocatedAmount"]),
          ExpensesReserveAmount = Convert.ToDouble(sqlDataReader["ExpensesReserveAmount"]),
          ExpensesIncurredAmount = Convert.ToDouble(sqlDataReader["ExpensesIncurredAmount"]),
          Name = sqlDataReader["Name"].ToString(),
          Phone1 = sqlDataReader["Phone1"].ToString(),
          Phone2 = sqlDataReader["Phone2"].ToString(),
          PropertyDamageIncurredAmount = Convert.ToDouble(sqlDataReader["PropertyDamageIncurredAmount"]),
          PropertyDamagePaidAmount = Convert.ToDouble(sqlDataReader["PropertyDamagePaidAmount"]),
          PropertyDamageReserveAmount = Convert.ToDouble(sqlDataReader["PropertyDamageReserveAmount"]),
          SSN = sqlDataReader["SSN"].ToString(),
          UpdatedBy = sqlDataReader["UpdatedBy"].ToString(),
          UserID = Convert.ToInt32(sqlDataReader["UserID"])
        });
      sqlDataReader.Close();
    }
  }
}
