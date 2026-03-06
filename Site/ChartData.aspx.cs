using AIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AISReports.Site
{
    public partial class ChartData : System.Web.UI.Page
    {
        #region
        private string chartType = "3d pie";
        private string queryName;
        private string companyId;
        private string queryParamNames;
        private string queryParamValues;
        private string chartTitle;
        private string customQueryName;
        private string themeName;
        private string view3D;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["chart_type"] != null)
                chartType = Request.QueryString["chart_type"].ToString();

            if (Request.QueryString["cquery"] != null)
                customQueryName = Request.QueryString["cquery"].ToString();

            if (Request.QueryString["query_name"] != null)
                queryName = Request.QueryString["query_name"].ToString();

            if (Request.QueryString["pname"] != null)
                queryParamNames = Request.QueryString["pname"].ToString();

            if (Request.QueryString["pvalue"] != null)
                queryParamValues = Request.QueryString["pvalue"].ToString();

            if (Request.QueryString["view3d"] != null)
                view3D = Request.QueryString["view3d"];

            if (Request.QueryString["theme"] != null)
                themeName = Request.QueryString["theme"];

            if (Request.QueryString["ctitle"] != null)
                chartTitle = Request.QueryString["ctitle"];

            StringBuilder sb = new StringBuilder();

            if (queryName != null)
                GetData(sb);

            Response.ClearHeaders();
            Response.ContentType = "text/xml";
            Response.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }

        private void GetData(StringBuilder sb)
        {
            string[] strArray = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData.Split(';');
            companyId = strArray[2];

            string chartXml = new ChartQuery()
            {
                ChartType = chartType,
                CompanyId = companyId,
                ParamNames = queryParamNames,
                ParamValues = queryParamValues,
                QueryName = queryName,
                ChartTitle = chartTitle,
                CustomQueryName = customQueryName,
                ThemeName = themeName,
                View3D = view3D,
                CompanyCode = (strArray[11] == "" ? strArray[5] + "_" : strArray[11] + "_"),
                UserId = 0
            }.GetChartXml();

            sb.Append(chartXml);
        }
    }
}