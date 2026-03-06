using AISReports.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AISReports.UserControls
{
    public partial class NCDFusionChart : NCDUserControlBase
    {
        #region Variables
        private int _chartWidth = 300;
        private int _chartHeight = 200;
        private string _queryName;
        private string _dataUri;
        private bool _enableDrillDown;
        private string _drillDownURL = "";
        private string companyName = "";
        private int companyId;
        private TextInfo camelCaseConverter;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            camelCaseConverter = Thread.CurrentThread.CurrentCulture.TextInfo;
            pChart.Attributes.Add("chart-query", QueryName);
            pChart.Attributes.Add("chart-query-type", QueryType);

            LoadChartSettings();
        }

        private string GetChartTitle(string queryName)
        {
            if (queryName.ToUpper() == "KPITOTALS")
                return "KPI Codes Breakdown";

            if (queryName.ToUpper() == "TOPDRIVER")
                return "Visifire 3D Column Chart";

            if (queryName.ToUpper() == "USEAGAIN")
                return string.Format("Use {0} Again?", (object)companyName);

            if (queryName.ToUpper() == "NW_MILITARY_DRIVERSTACKEDCHART" || queryName.ToUpper() == "MESA_DRIVERSTACKEDCHART")
                return "Driver Score Chart";

            if (queryName.ToUpper() == "USEAGAINREGION")
                return string.Format("Use {0} Again?", (object)companyName);

            if (queryName.ToUpper() == "QUESTIONBREAKDOWN" || queryName.ToUpper() == "QUESTIONBREAKDOWNBYREGION")
                return "Questions Breakdown";

            if (queryName.ToUpper() == "QUESTIONBREAKDOWNBYREGIONDASHBOARD")
                return companyId > 10008 && companyId < 10018 ? "Average Score by Booker (of All Survey Questions)" : "Questions Breakdown By Region";

            if (queryName.ToUpper() == "QUESTIONBREAKDOWNHAULINGCODEBYREGIONDASHBOARD")
                return companyId > 10008 && companyId < 10018 ? "Average Score by Hauler (of All Survey Questions)" : "Questions Breakdown By Region";

            if (queryName.ToUpper() == "QUESTIONBREAKDOWNHAULINGCODEBYREGION")
                return companyId > 10008 && companyId < 10018 ? "Average Score by Hauler (of All Survey Questions)" : "Questions Breakdown By Region";

            if (queryName.ToUpper() == "QUESTIONBREAKDOWNREGIONBYCOMPANY" || queryName.ToUpper() == "QUESTIONBREAKDOWNHAULINGCODEREGIONBYCOMPANY" || queryName.ToUpper() == "QUESTIONBREAKDOWNREGIONBYCOMPANYDASHBOARD" || queryName.ToUpper() == "QUESTIONBREAKDOWNHAULINGCODEREGIONBYCOMPANYDASHBOARD")
                return "Question Breakdown by Location";

            if (queryName.ToUpper() == "AVGSCOREBYMONTH")
                return "Avg. Survey Score by Delivery Month";

            if (queryName.ToUpper() == "AVGSCOREBYBOOKERNO")
                return "Avg. Survey Score By Booker No.";

            return queryName.ToUpper() == "AVGSCOREBYSCAC" ? "Avg. Survey Score By SCAC Code" : "Chart";
        }

        private void SetChartTypeButtons(string buttonList)
        {
            PlaceHolder1.Controls.Clear();

            HtmlGenericControl child1 = new HtmlGenericControl("div");
            child1.Attributes.Add("class", "chart-button-section");

            string str1 = buttonList;
            char[] chArray = new char[1] { '|' };

            foreach (string str2 in str1.Split(chArray))
            {
                HtmlGenericControl child2 = new HtmlGenericControl("span");

                child2.Attributes.Add("class", string.Format("chart-button-icon chart-button-{0}", (object)str2.ToLower().Trim()));
                child2.Attributes.Add("onclick", string.Format("javascript:SetFusionChartType('{0}',this);", (object)pChart.ClientID));
                child2.Attributes.Add("title", camelCaseConverter.ToTitleCase(str2));
                child1.Controls.Add((Control)child2);
            }

            pChart.Attributes.Add("chart-type-selected", buttonList.Split('|')[0]);
            PlaceHolder1.Controls.Add((Control)child1);
        }

        private void LoadChartSettings()
        {
            lblChartTitle.Text = ChartTitle;

            if (ChartTypes != null)
                SetChartTypeButtons(ChartTypes);

            pChart.Height = Unit.Pixel(Convert.ToInt32(ChartHeight));
            pChart.Attributes.Add("chart-types", ChartTypes);
            pChart.Attributes.Add("chart-query", QueryName);

            if (ChartDrillDown != null && ChartDrillDown != "")
                pChart.Attributes.Add("chart-drilldown", ChartDrillDown);

            if (QueryParameters != "" && QueryParameters != "")
                pChart.Attributes.Add("aria-query-param", QueryParameters);

            if (ChartSettings != null)
            {
                string chartSettings = ChartSettings;
                char[] chArray = new char[1] { '|' };

                foreach (string str in chartSettings.Split(chArray))
                {
                    if (((IEnumerable<string>)str.Split('~')).First<string>().ToLower() == "height")
                    {
                        pChart.Height = Unit.Pixel(Convert.ToInt32(((IEnumerable<string>)str.Split('~')).Last<string>()));
                        pChart.Attributes.Add("chart-height", ((IEnumerable<string>)str.Split('~')).Last<string>());
                    }
                    else if (((IEnumerable<string>)str.Split('~')).First<string>().ToLower() == "width")
                    {
                        pChart.Width = Unit.Pixel(Convert.ToInt32(((IEnumerable<string>)str.Split('~')).Last<string>()));
                        pChart.Attributes.Add("chart-width", ((IEnumerable<string>)str.Split('~')).Last<string>());
                    }
                    else if (((IEnumerable<string>)str.Split('~')).First<string>().ToLower() == "charttitle")
                    {
                        pChart.Attributes.Add("chart-title", ((IEnumerable<string>)str.Split('~')).Last<string>());
                        lblChartTitle.Text = ((IEnumerable<string>)str.Split('~')).Last<string>();
                    }
                    else if (((IEnumerable<string>)str.Split('~')).First<string>().ToLower() == "charttypes")
                    {
                        SetChartTypeButtons(((IEnumerable<string>)str.Split('~')).Last<string>().Replace("^", "|"));
                        pChart.Attributes.Add("chart-types", ((IEnumerable<string>)str.Split('~')).Last<string>().Replace("^", "|"));
                    }
                    else if (((IEnumerable<string>)str.Split('~')).First<string>().ToLower() == "enabledrilldown")
                        pChart.Attributes.Add("chart-drilldown", ((IEnumerable<string>)str.Split('~')).Last<string>());
                    else if (((IEnumerable<string>)str.Split('~')).First<string>().ToLower() == "charttheme")
                        pChart.Attributes.Add("chart-theme", ((IEnumerable<string>)str.Split('~')).Last<string>());
                    else if (((IEnumerable<string>)str.Split('~')).First<string>().ToLower() == "customquery")
                        pChart.Attributes.Add("chart-custom-query", ((IEnumerable<string>)str.Split('~')).Last<string>());
                    else if (((IEnumerable<string>)str.Split('~')).First<string>().ToLower() == "chartparameters")
                        pChart.Attributes.Add("chart-parameters", ((IEnumerable<string>)str.Split('~')).Last<string>());
                    else if (((IEnumerable<string>)str.Split('~')).First<string>().ToLower() == "chartsettings")
                        pChart.Attributes.Add("chart-settings", ((IEnumerable<string>)str.Split('~')).Last<string>());
                    else if (((IEnumerable<string>)str.Split('~')).First<string>().ToLower() == "companycodeoverride")
                        pChart.Attributes.Add("chart-ccodeoverride", ((IEnumerable<string>)str.Split('~')).Last<string>());
                }
            }
            else
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();

                    SqlCommand command = sqlConnection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "cms_GetChartSettings";

                    command.Parameters.AddWithValue("PageUserControlID", (object)PageUserControlID);
                    SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                    while (sqlDataReader.Read())
                    {
                        if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "height")
                            pChart.Height = Unit.Pixel(Convert.ToInt32(sqlDataReader["SettingValue"]));
                        else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "charttitle")
                        {
                            pChart.Attributes.Add("chart-title", sqlDataReader["SettingValue"].ToString());
                            lblChartTitle.Text = sqlDataReader["SettingValue"].ToString();
                        }
                        else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "charttypebuttons")
                        {
                            SetChartTypeButtons(sqlDataReader["SettingValue"].ToString().Trim());
                            pChart.Attributes.Add("chart-types", sqlDataReader["SettingValue"].ToString().Trim());
                        }
                        else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "enabledrilldown")
                            pChart.Attributes.Add("chart-drilldown", sqlDataReader["SettingValue"].ToString());
                        else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "charttheme")
                            pChart.Attributes.Add("chart-theme", sqlDataReader["SettingValue"].ToString());
                        else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "customquery")
                            pChart.Attributes.Add("chart-custom-query", sqlDataReader["SettingValue"].ToString());
                        else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "chartparameters")
                            pChart.Attributes.Add("chart-parameters", sqlDataReader["SettingValue"].ToString());
                    }
                }
            }
        }
    }
}