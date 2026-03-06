// Decompiled with JetBrains decompiler
// Type: AISData
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;

[ScriptService]
[WebService(Namespace = "http://aismgt.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class AISData : WebService
{
  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
  [WebMethod]
  public string GetDriverScoreCard(string emailAddress)
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    StringBuilder stringBuilder3 = new StringBuilder();
    StringBuilder stringBuilder4 = new StringBuilder();
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
      command.Parameters.AddWithValue("EmailAddress", (object) emailAddress);
      SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
      while (sqlDataReader.Read())
      {
        stringBuilder1.AppendLine(string.Format("\"{0}\": \"{1}\",", sqlDataReader["TotalConsolidatedLabel"], sqlDataReader["TotalConsolidatedScores"]));
        stringBuilder2.AppendLine(string.Format("\"{0}\": \"{1}\",", sqlDataReader["NumberOfScoresReceivedLabel"], sqlDataReader["NumberOfScoresReceivedInTotal"]));
      }
      sqlDataReader.NextResult();
      while (sqlDataReader.Read())
      {
        stringBuilder3.AppendLine("{");
        stringBuilder3.AppendLine(string.Format("\"Atlantic Client Name Secondary\": \"{0}\",", sqlDataReader["AtlanticClientNameSecondary"]));
        stringBuilder3.AppendLine(string.Format("\"Transferee Name\": \"{0}\",", sqlDataReader["TransfereeName"]));
        stringBuilder3.AppendLine(string.Format("\"Registration Number\": \"{0}\",", sqlDataReader["RegistrationNumber"]));
        stringBuilder3.AppendLine(string.Format("\"Counselor Name\": \"{0}\",", sqlDataReader["CounselorName"]));
        stringBuilder3.AppendLine(string.Format("\"Counselor Score\": \"{0}\",", sqlDataReader["ConsolidatedClientResponseforCounselor"]));
        stringBuilder3.AppendLine(string.Format("\"Packer Score\": \"{0}\",", sqlDataReader["ConsolidatedClientResponseforPacker"]));
        stringBuilder3.AppendLine(string.Format("\"Driver Score\": \"{0}\",", sqlDataReader["ConsolidatedClientResponseforDriver"]));
        stringBuilder3.AppendLine(string.Format("\"Estimator Score \": \"{0}\",", sqlDataReader["ConsolidatedClientResponseforEstimator"]));
        stringBuilder3.AppendLine(string.Format("\"Overall Score\": \"{0}\",", sqlDataReader["ConsolidatedClientResponseforOverallScore"]));
        stringBuilder3.AppendLine(string.Format("\"Comments\": \"{0}\"", sqlDataReader["Comments"]));
        stringBuilder3.AppendLine("},");
      }
      stringBuilder3.Remove(stringBuilder3.Length - 1, 1);
      stringBuilder4.AppendLine("{");
      stringBuilder4.Append(stringBuilder1.ToString());
      stringBuilder4.Append(stringBuilder2.ToString());
      stringBuilder4.AppendLine("\"DriverDetail\": [");
      stringBuilder4.Append(stringBuilder3.ToString());
      stringBuilder4.AppendLine("]");
      stringBuilder4.AppendLine("}");
    }
    stringBuilder4.ToString();
    return stringBuilder4.ToString();
  }
}
