using AISReports.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AISReports.UserControls
{
    public partial class NCDLytlesWorkbookGrid : NCDUserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DisplayFields))
            {
                DisplayFields = "ImportID|CSRContactedTransfereePK|CSRContactedTransfereeLD|CSRContactedTransfereeDEL|Account|Name|PickupAndDelivery|Type|DateSpread|Weight|Details|OriginDrivers|OriginHelpers|DestinationDrivers|DestinationHelpers|Cancelled|OfficeAssigned|ShipmentDelivered";
            }

            pChart.Attributes.Add("grid-display-fields", DisplayFields.Replace("!", ""));

            // ADD THIS — matches exact column names from Lytles_Schedulebook table
            pChart.Attributes.Add("database-fields",
                "ImportID~ImportID|" +
                "OfficeAssigned~OfficeAssigned|" +
                "Account~Account|" +
                "PPWK~PPWK|" +
                "Trailer~Trailer|" +
                "RegNumber~RegNumber|" +
                "Name~Name|" +
                "EmailAddress~EmailAddress|" +
                "PickupLocation~PickupLocation|" +
                "DeliveryLocation~DeliveryLocation|" +
                "Weight~Weight|" +
                "Details~Details|" +
                "ContactedTransfereeNotes~ContactedTransfereeNotes|" +
                "MoveAgent~MoveAgent|" +
                "OriginDriverNames~OriginDriverNames|" +
                "DestinationDriverNames~DestinationDriverNames|" +
                "OriginHelperNames~OriginHelperNames|" +
                "DestinationHelperNames~DestinationHelperNames|" +
                "PKStartDate~PKStartDate|" +
                "PKEndDate~PKEndDate|" +
                "LDStartDate~LDStartDate|" +
                "LDEndDate~LDEndDate|" +
                "DELStartDate~DELStartDate|" +
                "DELEndDate~DELEndDate|" +
                "Cancelled~Cancelled|" +
                "ShipmentDelivered~ShipmentDelivered|" +
                "CSRContactedTransfereePK~CSRContactedTransfereePK|" +
                "CSRContactedTransfereeLD~CSRContactedTransfereeLD|" +
                "CSRContactedTransfereeDEL~CSRContactedTransfereeDEL");

            if (!string.IsNullOrEmpty(QueryName))
                pChart.Attributes.Add("grid-query", QueryName);
            else
                pChart.Attributes.Add("grid-query", "Lytles_GetWorkbook");

            if (!string.IsNullOrEmpty(QueryType))
                pChart.Attributes.Add("grid-query-type", QueryType);
            else
                pChart.Attributes.Add("grid-query-type", "storedproc");
        }
    }
}