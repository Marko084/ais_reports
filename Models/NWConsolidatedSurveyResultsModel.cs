using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Models
{
    [DataContract]
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class NWConsolidatedSurveyResultsModel
    {

        [DataMember]
        public string CompanyCode { get; set; }
        [DataMember]
        public string CompanyName { get; set; }
        [DataMember]
        public DateTime DeliveryDate { get; set; }
        [DataMember]
        public DateTime CompletionDate { get; set; }
        [DataMember]
        public DateTime LoadDate { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public string CSCNo { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string OriginAgent { get; set; }
        [DataMember]
        public string DestinationAgent { get; set; }
        [DataMember]
        public string DriverName { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string CustomerServiceRep { get; set; }
        [DataMember]
        public string NationalAcctNo { get; set; }
        [DataMember]
        public string LocationCode { get; set; }
        [DataMember]
        public string HaulerCode { get; set; }
        [DataMember]
        public string StorageCode { get; set; }
        [DataMember]
        public string BookerNo { get; set; }
        [DataMember]
        public string OriginCity { get; set; }
        [DataMember]
        public string OriginState { get; set; }
        [DataMember]
        public string DestinationCity { get; set; }
        [DataMember]
        public string DestinationState { get; set; }
        [DataMember]
        public string KPICode { get; set; }
        [DataMember]
        public string FreightIndicator { get; set; }
        [DataMember]
        public string ParentCompanyName { get; set; }
        [DataMember]
        public string ParentIDNumber { get; set; }
        [DataMember]
        public decimal? ConsolidatedClientResponseforSurveyor { get; set; }
        [DataMember]
        public decimal? ConsolidatedClientResponseforCSR { get; set; }
        [DataMember]
        public decimal? ConsolidatedClientResponseforDriveratOrigin { get; set; }
        [DataMember]
        public decimal? ConsolidatedClientResponseforPackingCrew { get; set; }
        [DataMember]
        public decimal? ConsolidatedClientResponseforLoadingCrew { get; set; }
        [DataMember]
        public decimal? ConsolidatedClientResponseforDriveratDestination { get; set; }
        [DataMember]
        public decimal? ConsolidatedClientResponseforUnpackingandUnloadingCrew { get; set; }
        [DataMember]
        public string ConsolidatedClientResponseforCrewUniformatOrigin { get; set; }
        [DataMember]
        public string ConsolidatedClientResponseforDriverUniformatOrigin { get; set; }
        [DataMember]
        public string ConsolidatedClientResponseforCrewUniformatDestination { get; set; }
        [DataMember]
        public string ConsolidatedClientResponseforDriverUniformatDestination { get; set; }
        [DataMember]
        public string ConsolidatedClientResponseforClaims { get; set; }
        [DataMember]
        public decimal? ConsolidatedClientResponseforOverallMovingExperience { get; set; }
        [DataMember]
        public string ConsolidatedClientResponseforUseNWAgain { get; set; }
        [DataMember]
        public string Comments { get; set; }
        [DataMember]
        public string CompanySite { get; set; }
        [DataMember]
        public decimal? TotalScores { get; set; }
        [DataMember]
        public int? QuestionCount { get; set; }

        #region Methods

        public List<NWConsolidatedSurveyResultsModel> GetSurveyData(string pageNumber)
        {
            List<NWConsolidatedSurveyResultsModel> results = new List<NWConsolidatedSurveyResultsModel>();

            StringBuilder sb = new StringBuilder();

            var pageSize = "200";
           
            sb.AppendLine("select * from ");
            sb.AppendLine("(select row_number() over (order by ImportID) as RowNum, * from vw_NW_Consolidated_DriverRating) as RowConstrainedResult ");
            sb.AppendFormat("where RowNum >= ({0}*{1}-{1}+1) and RowNum < ({0}*{1}) order by RowNum ",pageNumber,pageSize);

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sb.ToString(); // "select * from vw_NW_Consolidated_DriverRating";

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    var model = new NWConsolidatedSurveyResultsModel();

                    foreach (PropertyInfo prop in model.GetType().GetProperties())
                    {
                        var propType = prop.PropertyType.Name;

                        try
                        {
                            if (prop.Name.EndsWith("Date"))
                                prop.SetValue(model, Convert.ToDateTime(dr[prop.Name]).ToString("MM-dd-yyyy HH:mm:ss"));
                            else if (propType == "Int32")
                                prop.SetValue(model, Convert.ToInt32(dr[prop.Name]));
                            else if (propType == "Double")
                                prop.SetValue(model, Convert.ToDouble(dr[prop.Name]));
                            else if (propType == "Decimal")
                                prop.SetValue(model, Convert.ToDecimal(dr[prop.Name]));
                            else if (propType == "Boolean")
                                prop.SetValue(model, Convert.ToBoolean(dr[prop.Name]));
                            else
                                prop.SetValue(model, dr[prop.Name].ToString());
                        }
                        catch { }
                    }

                    results.Add(model);
                }
            }

            if(results.Count==0)
            {
                results = null;
            }

            return results;
        }
        #endregion
    }
}
