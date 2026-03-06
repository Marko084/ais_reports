using AIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AISReports.Site
{
    public partial class KPIDetails : System.Web.UI.Page
    {
        #region
        private string startdate;
        private string enddate;
        private string companyID;
        private string kpiDescription;
        private string clientName;
        private string scac;
        private string userID;
        private string transfereeName;
        private string driverName;
        private string loadStartDate;
        private string loadEndDate;
        private string userLevel;
        private string shipmentNumber;
        private string fieldName;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblKPIDetail.Text = Request.QueryString["kpi"];
                startdate = Request.QueryString["DeliveryStartDate"] == null ? Request.QueryString["startStartDate"] : Request.QueryString["DeliveryStartDate"];
                enddate = Request.QueryString["DeliveryEndDate"] == null ? Request.QueryString["startendDate"] : Request.QueryString["DeliveryEndDate"];
                startdate = Request.QueryString["CompletionStartDate"] == null ? Request.QueryString["startStartDate"] : Request.QueryString["CompletionStartDate"];
                enddate = Request.QueryString["CompletionEndDate"] == null ? Request.QueryString["startendDate"] : Request.QueryString["CompletionEndDate"];
                companyID = Request.QueryString["companyID"];
                kpiDescription = Request.QueryString["kpi"];
                clientName = Request.QueryString["clientName"] == null ? "%" : Request.QueryString["clientName"];
                scac = Request.QueryString["scac"] == null ? "%" : Request.QueryString["scac"];
                userID = Request.QueryString["UserID"];
                userLevel = Request.QueryString["userlevel"] == null ? "" : Request.QueryString["userlevel"];
                driverName = Request.QueryString["drivername"] == null ? "%" : Request.QueryString["drivername"];
                loadStartDate = Request.QueryString["loadstartdate"] == null ? "1/1/1900" : Request.QueryString["loadstartdate"];
                loadEndDate = Request.QueryString["loadenddate"] == null ? "12/31/9999" : Request.QueryString["loadenddate"];
                transfereeName = Request.QueryString["transfereename"] == null ? "" : Request.QueryString["transfereename"];
                shipmentNumber = Request.QueryString["shipmentnumber"] == null ? "" : Request.QueryString["shipmentnumber"];
                fieldName = Request.QueryString["fieldname"] == null ? "" : Request.QueryString["fieldname"];

                if (Request.QueryString["responsetoquestion2"] != null)
                    clientName = Request.QueryString["responsetoquestion2"];

                StringBuilder stringBuilder = new StringBuilder();

                litKPIDetail.Text = DAL.ReturnScalarValue("getKPIDetails", "CompanyID,UserID,DeliveryStartDate,DeliveryEndDate,KPIDescription,ClientName,SortBy,SortOrder,SCAC,UseCMSTF,UserLevel,DriverName,LoadStartDate,LoadEndDate,TransfereeName,ShipmentNumber,FieldName", companyID + "," + userID + "," + startdate + "," + enddate + "," + kpiDescription + "," + clientName + "," + txtColumnSort.Text + "," + txtSortOrder.Text + "," + scac + ",1," + userLevel + "," + driverName + "," + loadStartDate + "," + loadEndDate + "," + transfereeName + "," + shipmentNumber + "," + fieldName).ToString().Replace("&lt;", "<").Replace("&gt;", ">");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
        }
    }
}