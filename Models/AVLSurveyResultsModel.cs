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
    public class AVLSurveyResultsModel
    {
        #region Properties
        [DataMember]
        public string OrderNumber { get; set; } = String.Empty;

        [DataMember]
        public string RegistrationNumber { get; set; } = String.Empty;

        [DataMember]
        public string GBL { get; set; } = String.Empty;

        [DataMember]
        public string CSCNo { get; set; } = String.Empty;

        [DataMember]
        public string LastName { get; set; } = String.Empty;

        [DataMember]
        public string FirstName { get; set; } = String.Empty;

        [DataMember]
        public string TransfereeName { get; set; } = String.Empty;

        [DataMember]
        public string OriginAgent { get; set; } = String.Empty;

        [DataMember]
        public string DestinationAgent { get; set; } = String.Empty;

        [DataMember]
        public string DriverName { get; set; } = String.Empty;

        [DataMember]
        public string ClientName { get; set; } = String.Empty;

        [DataMember]
        public string CustomerServiceRep { get; set; } = String.Empty;

        [DataMember]
        public string NationalAcctNo { get; set; } = String.Empty;

        [DataMember]
        public string LocationCode { get; set; } = String.Empty;

        [DataMember]
        public string HaulerCode { get; set; } = String.Empty;

        [DataMember]
        public string StorageCode { get; set; } = String.Empty;

        [DataMember]
        public string BookerNo { get; set; } = String.Empty;

        [DataMember]
        public string ShipOriginCity { get; set; } = String.Empty;

        [DataMember]
        public string ShipOriginState { get; set; } = String.Empty;

        [DataMember]
        public string ShipDestinationCity { get; set; } = String.Empty;

        [DataMember]
        public string ShipDestinationState { get; set; } = String.Empty;

        [DataMember]
        public string DeliveryDate { get; set; } = String.Empty;

        [DataMember]
        public string CompletionDate { get; set; } = String.Empty;

        [DataMember]
        public string LoadDate { get; set; } = String.Empty;

        [DataMember]
        public string CreatedDate { get; set; } = String.Empty;

        [DataMember]
        public string KPICode { get; set; } = String.Empty;

        [DataMember]
        public string GovernmentBillOfLading { get; set; } = String.Empty;

        [DataMember]
        public string FreightIndicator { get; set; } = String.Empty;

        [DataMember]
        public string CellPhoneNumber { get; set; } = String.Empty;

        [DataMember]
        public string PhoneNumber { get; set; } = String.Empty;

        [DataMember]
        public string SITAgent { get; set; } = String.Empty;

        [DataMember]
        public string CSRScore { get; set; } = String.Empty;

        [DataMember]
        public string SurveyorScore { get; set; } = String.Empty;

        [DataMember]
        public string DriverAtOriginScore { get; set; } = String.Empty;

        [DataMember]
        public string DriverAtDestinationScore { get; set; } = String.Empty;

        [DataMember]
        public string PackingCrewScore { get; set; } = String.Empty;

        [DataMember]
        public string LoadingCrewScore { get; set; } = String.Empty;

        [DataMember]
        public string UnpackingAndUnloadingScore { get; set; } = String.Empty;

        [DataMember]
        public string OriginCrewUniform { get; set; } = String.Empty;

        [DataMember]
        public string OriginDriverUniform { get; set; } = String.Empty;

        [DataMember]
        public string DestinationDriverUniform { get; set; } = String.Empty;

        [DataMember]
        public string DestinationCrewUniform { get; set; } = String.Empty;

        [DataMember]
        public string Claims { get; set; } = String.Empty;

        [DataMember]
        public string UseAgain { get; set; } = String.Empty;

        [DataMember]
        public string OverallScore { get; set; } = String.Empty;

        [DataMember]
        public string Comments { get; set; } = String.Empty;

        [DataMember]
        public string Email { get; set; } = String.Empty;

        [DataMember]
        public string SCAC { get; set; } = String.Empty;

        [DataMember]
        public string PackAgent { get; set; } = String.Empty;

        [DataMember]
        public string PickupAgent { get; set; } = String.Empty;

        [DataMember]
        public string DriverNumber { get; set; } = String.Empty;
        #endregion

        #region Methods

        public List<AVLSurveyResultsModel> GetSurveyData()
        {
            List<AVLSurveyResultsModel> results = new List<AVLSurveyResultsModel>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from AVL_SurveyResults where Status='Spoke To' ";

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    var model = new AVLSurveyResultsModel();

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
