// Decompiled with JetBrains decompiler
// Type: DriverScoreCard
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.ServiceModel;

[DataContract]
[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
public class DriverScoreCard
{
  [DataMember]
  public string EstimatorAverageScore;
  [DataMember]
  public string CounselorAverageScore;
  [DataMember]
  public string DriverAverageScore;
  [DataMember]
  public string PackerAverageScore;
  [DataMember]
  public string OverallAverageScore;
  [DataMember]
  public string PercentageOf5sReceived;
  [DataMember]
  public string PercentageOf4sReceived;
  [DataMember]
  public string PercentageOf3sReceived;
  [DataMember]
  public string PercentageOf2sReceived;
  [DataMember]
  public string PercentageOf1sReceived;
  [DataMember]
  public string FirstName;
  [DataMember]
  public string LastName;
  [DataMember]
  public string DriverNumber;
  [DataMember]
  private List<DriverScoreCardDetails> Details;

  public DriverScoreCard() => this.Details = new List<DriverScoreCardDetails>();

  public DriverScoreCard Get(string email)
  {
    DriverScoreCard driverScoreCard = new DriverScoreCard();
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = "ARS_GetDriverScoreCard";
      command.Parameters.AddWithValue("CompanyID", (object) 10003);
      command.Parameters.AddWithValue("ConsolidatedQuestions", (object) 1);
      command.Parameters.AddWithValue("DeliveryStartDate", (object) DateTime.Today.AddYears(-1));
      command.Parameters.AddWithValue("DeliveryEndDate", (object) DateTime.Today);
      command.Parameters.AddWithValue("EmailAddress", (object) email);
      SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
      while (sqlDataReader.Read())
      {
        if (sqlDataReader["TotalConsolidatedLabel"].ToString().ToLower().Contains("estimator consolidated score"))
          driverScoreCard.EstimatorAverageScore = sqlDataReader["TotalConsolidatedScores"].ToString();
        else if (sqlDataReader["TotalConsolidatedLabel"].ToString().ToLower().Contains("counselor consolidated score"))
          driverScoreCard.CounselorAverageScore = sqlDataReader["TotalConsolidatedScores"].ToString();
        else if (sqlDataReader["TotalConsolidatedLabel"].ToString().ToLower().Contains("driver consolidated score"))
          driverScoreCard.DriverAverageScore = sqlDataReader["TotalConsolidatedScores"].ToString();
        else if (sqlDataReader["TotalConsolidatedLabel"].ToString().ToLower().Contains("packer consolidated score"))
          driverScoreCard.PackerAverageScore = sqlDataReader["TotalConsolidatedScores"].ToString();
        else if (sqlDataReader["TotalConsolidatedLabel"].ToString().ToLower().Contains("overall consolidated score"))
          driverScoreCard.OverallAverageScore = sqlDataReader["TotalConsolidatedScores"].ToString();
        if (sqlDataReader["NumberOfScoresReceivedLabel"].ToString().ToLower().Contains("percentage of 5"))
          driverScoreCard.PercentageOf5sReceived = sqlDataReader["NumberOfScoresReceivedInTotal"].ToString();
        else if (sqlDataReader["NumberOfScoresReceivedLabel"].ToString().ToLower().Contains("percentage of 4"))
          driverScoreCard.PercentageOf4sReceived = sqlDataReader["NumberOfScoresReceivedInTotal"].ToString();
        else if (sqlDataReader["NumberOfScoresReceivedLabel"].ToString().ToLower().Contains("percentage of 3"))
          driverScoreCard.PercentageOf3sReceived = sqlDataReader["NumberOfScoresReceivedInTotal"].ToString();
        else if (sqlDataReader["NumberOfScoresReceivedLabel"].ToString().ToLower().Contains("percentage of 2"))
          driverScoreCard.PercentageOf2sReceived = sqlDataReader["NumberOfScoresReceivedInTotal"].ToString();
        else if (sqlDataReader["NumberOfScoresReceivedLabel"].ToString().ToLower().Contains("percentage of 1"))
          driverScoreCard.PercentageOf1sReceived = sqlDataReader["NumberOfScoresReceivedInTotal"].ToString();
      }
      sqlDataReader.NextResult();
      while (sqlDataReader.Read())
        driverScoreCard.Details.Add(new DriverScoreCardDetails()
        {
          AtlanticClientNameSecondary = sqlDataReader["AtlanticClientNameSecondary"].ToString(),
          Comments = sqlDataReader["Comments"].ToString(),
          CounselorName = sqlDataReader["CounselorName"].ToString(),
          CounselorScore = sqlDataReader["ConsolidatedClientResponseforCounselor"].ToString(),
          PackerScore = sqlDataReader["ConsolidatedClientResponseforPacker"].ToString(),
          DriverScore = sqlDataReader["ConsolidatedClientResponseforDriver"].ToString(),
          EstimatorScore = sqlDataReader["ConsolidatedClientResponseforEstimator"].ToString(),
          OverallScore = sqlDataReader["ConsolidatedClientResponseforOverallScore"].ToString(),
          RegistrationNumber = sqlDataReader["RegistrationNumber"].ToString(),
          TransfereeName = sqlDataReader["TransfereeName"].ToString()
        });
      sqlDataReader.NextResult();
      while (sqlDataReader.Read())
      {
        driverScoreCard.FirstName = sqlDataReader["FirstName"].ToString();
        driverScoreCard.LastName = sqlDataReader["LastName"].ToString();
        driverScoreCard.DriverNumber = sqlDataReader["DriverNumber"].ToString();
      }
    }
    return driverScoreCard;
  }

  public DriverScoreCard Get(string email, string companyID)
  {
    DriverScoreCard driverScoreCard = new DriverScoreCard();
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = "Demo_GetDriverScoreCardDemo";
      command.Parameters.AddWithValue("CompanyID", (object) companyID);
      command.Parameters.AddWithValue("ConsolidatedQuestions", (object) 1);
      command.Parameters.AddWithValue("DeliveryStartDate", (object) DateTime.Today.AddYears(-1));
      command.Parameters.AddWithValue("DeliveryEndDate", (object) DateTime.Today);
      command.Parameters.AddWithValue("EmailAddress", (object) email);
      SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
      while (sqlDataReader.Read())
      {
        if (sqlDataReader["TotalConsolidatedLabel"].ToString().ToLower().Contains("estimator consolidated score"))
          driverScoreCard.EstimatorAverageScore = sqlDataReader["TotalConsolidatedScores"].ToString();
        else if (sqlDataReader["TotalConsolidatedLabel"].ToString().ToLower().Contains("counselor consolidated score"))
          driverScoreCard.CounselorAverageScore = sqlDataReader["TotalConsolidatedScores"].ToString();
        else if (sqlDataReader["TotalConsolidatedLabel"].ToString().ToLower().Contains("driver consolidated score"))
          driverScoreCard.DriverAverageScore = sqlDataReader["TotalConsolidatedScores"].ToString();
        else if (sqlDataReader["TotalConsolidatedLabel"].ToString().ToLower().Contains("packer consolidated score"))
          driverScoreCard.PackerAverageScore = sqlDataReader["TotalConsolidatedScores"].ToString();
        else if (sqlDataReader["TotalConsolidatedLabel"].ToString().ToLower().Contains("overall consolidated score"))
          driverScoreCard.OverallAverageScore = sqlDataReader["TotalConsolidatedScores"].ToString();
        if (sqlDataReader["NumberOfScoresReceivedLabel"].ToString().ToLower().Contains("percentage of 5"))
          driverScoreCard.PercentageOf5sReceived = sqlDataReader["NumberOfScoresReceivedInTotal"].ToString();
        else if (sqlDataReader["NumberOfScoresReceivedLabel"].ToString().ToLower().Contains("percentage of 4"))
          driverScoreCard.PercentageOf4sReceived = sqlDataReader["NumberOfScoresReceivedInTotal"].ToString();
        else if (sqlDataReader["NumberOfScoresReceivedLabel"].ToString().ToLower().Contains("percentage of 3"))
          driverScoreCard.PercentageOf3sReceived = sqlDataReader["NumberOfScoresReceivedInTotal"].ToString();
        else if (sqlDataReader["NumberOfScoresReceivedLabel"].ToString().ToLower().Contains("percentage of 2"))
          driverScoreCard.PercentageOf2sReceived = sqlDataReader["NumberOfScoresReceivedInTotal"].ToString();
        else if (sqlDataReader["NumberOfScoresReceivedLabel"].ToString().ToLower().Contains("percentage of 1"))
          driverScoreCard.PercentageOf1sReceived = sqlDataReader["NumberOfScoresReceivedInTotal"].ToString();
      }
      sqlDataReader.NextResult();
      while (sqlDataReader.Read())
        driverScoreCard.Details.Add(new DriverScoreCardDetails()
        {
          AtlanticClientNameSecondary = sqlDataReader["AtlanticClientNameSecondary"].ToString(),
          Comments = sqlDataReader["Comments"].ToString(),
          CounselorName = sqlDataReader["CounselorName"].ToString(),
          CounselorScore = sqlDataReader["ConsolidatedClientResponseforCounselor"].ToString(),
          PackerScore = sqlDataReader["ConsolidatedClientResponseforPacker"].ToString(),
          DriverScore = sqlDataReader["ConsolidatedClientResponseforDriver"].ToString(),
          EstimatorScore = sqlDataReader["ConsolidatedClientResponseforEstimator"].ToString(),
          OverallScore = sqlDataReader["ConsolidatedClientResponseforOverallScore"].ToString(),
          RegistrationNumber = sqlDataReader["RegistrationNumber"].ToString(),
          TransfereeName = sqlDataReader["TransfereeName"].ToString()
        });
      sqlDataReader.NextResult();
      while (sqlDataReader.Read())
      {
        driverScoreCard.FirstName = sqlDataReader["FirstName"].ToString();
        driverScoreCard.LastName = sqlDataReader["LastName"].ToString();
        driverScoreCard.DriverNumber = sqlDataReader["DriverNumber"].ToString();
      }
    }
    return driverScoreCard;
  }
}
