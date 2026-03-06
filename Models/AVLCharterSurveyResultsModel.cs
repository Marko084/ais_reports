using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Models
{
    [DataContract]
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class AVLCharterSurveyResultsModel
    {
        #region Properties
        [DataMember]
        public string CompanyCode { get; set; } = String.Empty;

        [DataMember]
        public string CSCNo { get; set; } = String.Empty;

        [DataMember]
        public string OrderNumber { get; set; } = String.Empty;

        [DataMember]
        public string GovernmentBillOfLading { get; set; } = String.Empty;

        [DataMember]
        public string DeliveryDate { get; set; } = String.Empty;

        [DataMember]
        public string ClientName { get; set; } = String.Empty;

        [DataMember]
        public string TransfereeName { get; set; } = String.Empty;

        [DataMember]
        public string OriginAgent { get; set; } = String.Empty;

        [DataMember]
        public string DestinationAgent { get; set; } = String.Empty;

        [DataMember]
        public string DriverName { get; set; } = String.Empty;

        [DataMember]
        public string NationalAcctNo { get; set; } = String.Empty;

        [DataMember]
        public string BookerNo { get; set; } = String.Empty;

        [DataMember]
        public string CustomerServiceRep { get; set; } = String.Empty;

        [DataMember]
        public string StorageCode { get; set; } = String.Empty;

        [DataMember]
        public string FirstName { get; set; } = String.Empty;

        [DataMember]
        public string LastName { get; set; } = String.Empty;

        [DataMember]
        public string HaulerCode { get; set; } = String.Empty;

        [DataMember]
        public string ShipOriginCity { get; set; } = String.Empty;

        [DataMember]
        public string ShipOriginState { get; set; } = String.Empty;

        [DataMember]
        public string ShipDestinationCity { get; set; } = String.Empty;

        [DataMember]
        public string ShipDestinationState { get; set; } = String.Empty;

        [DataMember]
        public string CompletionDate { get; set; } = String.Empty;

        [DataMember]
        public string KPICode { get; set; } = String.Empty;

        [DataMember]
        public string LoadDate { get; set; } = String.Empty;

        [DataMember]
        public string Email { get; set; } = String.Empty;

        [DataMember]
        public string PackAgent { get; set; } = String.Empty;

        [DataMember]
        public string PickupAgent { get; set; } = String.Empty;

        [DataMember]
        public string RegistrationNumber { get; set; } = String.Empty;

        [DataMember]
        public string SCAC { get; set; } = String.Empty;

        [DataMember]
        public string GovernmentID { get; set; } = String.Empty;

        [DataMember]
        public string CreatedDate { get; set; } = String.Empty;

        [DataMember]
        public string FreightIndicator { get; set; } = String.Empty;

        [DataMember]
        public string PhoneNumber { get; set; } = String.Empty;

        [DataMember]
        public string Score { get; set; } = String.Empty;

        [DataMember]
        public string PackingResponse { get; set; } = String.Empty;

        [DataMember]
        public string LoadingResponse { get; set; } = String.Empty;

        [DataMember]
        public string TimelinessResponse { get; set; } = String.Empty;

        [DataMember]
        public string UnloadingResponse { get; set; } = String.Empty;

        [DataMember]
        public string DeliveryResponse { get; set; } = String.Empty;

        [DataMember]
        public string OverallResponse { get; set; } = String.Empty;

        [DataMember]
        public string CoordinatorResponsivenessResponse { get; set; } = String.Empty;

        [DataMember]
        public string FileClaim { get; set; } = String.Empty;

        [DataMember]
        public int PackingScore { get; set; } = Int32.MinValue;

        [DataMember]
        public int LoadingScore { get; set; } = Int32.MinValue;

        [DataMember]
        public int TimelinessScore { get; set; } = Int32.MinValue;

        [DataMember]
        public int UnloadingScore { get; set; } = Int32.MinValue;

        [DataMember]
        public int DeliveryScore { get; set; } = Int32.MinValue;

        [DataMember]
        public int OverallScore { get; set; } = Int32.MinValue;

        [DataMember]
        public int CoordinatorResponsivenessScore { get; set; } = Int32.MinValue;

        [DataMember]
        public string Comments { get; set; } = String.Empty;

        #endregion

        #region Methods

        public List<AVLCharterSurveyResultsModel> GetSurveyData()
        {
            List<AVLCharterSurveyResultsModel> results = new List<AVLCharterSurveyResultsModel>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from vw_AVL_Charter_SurveyResults";

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    var model = new AVLCharterSurveyResultsModel();

                    foreach (PropertyInfo prop in model.GetType().GetProperties())
                    {
                        var propType = prop.PropertyType.Name;

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

                    results.Add(model);
                }
            }

            return results;
        }
        #endregion
    }
}
