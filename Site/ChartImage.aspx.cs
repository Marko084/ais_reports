using AIS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace AISReports.Site
{
    public partial class ChartImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string parameterNames = "";
            string parameterValues = "";
            string str1 = Request.QueryString["pl"];
            string str2 = Request.QueryString["ctitle"];
            string storedProcedureName = Request.QueryString["qn"];
            string str3 = Request.QueryString["axisyinterval"];

            Chart1.Titles.First<Title>().Text = str2;

            if (str3 != null)
                Chart1.ChartAreas["ChartArea1"].AxisY.Interval = Convert.ToDouble(str3);

            string str4 = str1;
            char[] chArray = new char[1] { '|' };

            foreach (string str5 in str4.Split(chArray))
            {
                if (parameterNames.Length == 0)
                {
                    parameterNames = ((IEnumerable<string>)str5.Split('~')).First<string>();
                    parameterValues = ((IEnumerable<string>)str5.Split('~')).Last<string>();
                }
                else
                {
                    parameterNames += string.Format(",{0}", (object)((IEnumerable<string>)str5.Split('~')).First<string>());
                    parameterValues += string.Format(",{0}", (object)((IEnumerable<string>)str5.Split('~')).Last<string>());
                }
            }

            SqlDataReader sqlDataReader = DAL.ReturnDataReader(storedProcedureName, parameterNames, parameterValues);

            Series series = Chart1.Series["Series1"];

            while (sqlDataReader.Read())
            {
                DataPoint dataPoint = new DataPoint();

                dataPoint.AxisLabel = sqlDataReader["AxisLabel"].ToString();
                dataPoint.YValues = new List<double>() {Convert.ToDouble(sqlDataReader["YValue"]) }.ToArray();

                try
                {
                    dataPoint.ToolTip = sqlDataReader["ToolTip"].ToString();
                }
                catch
                {
                }

                series.Points.Add(dataPoint);
            }

            sqlDataReader.Close();
            sqlDataReader.Dispose();
        }
    }
}