using AIS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AISReports
{
    //Google Charts Implementation

    public partial class Chart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str1 = Request.QueryString["cid"];
            string str2 = Request.QueryString["fvalue"];
            string parameterNames = Request.QueryString["qparm"];
            string parameterValues = Request.QueryString["qval"];
            string str3 = Request.QueryString["ctitle"];
            string str4 = Request.QueryString["location"];

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("<script type='text/javascript'> ");
            stringBuilder.AppendLine("google.load(\"visualization\", \"1\", { packages: [\"corechart\"] }); ");
            stringBuilder.AppendLine("google.setOnLoadCallback(drawChart); ");
            stringBuilder.AppendLine("function drawChart() { ");
            stringBuilder.AppendLine("var data = google.visualization.arrayToDataTable([ ");

            DataTable table = ((DataSet)DAL.ReturnDataSet("GetWorldClassCommitmentData", parameterNames, parameterValues, "WCC", "WCC")).Tables[0];

            DataView defaultView = table.DefaultView;

            stringBuilder.Append(" [");

            for (int index = 0; index < table.Columns.Count; ++index)
            {
                if (table.Columns[index].ColumnName != "Criteria")
                {
                    if (index == table.Columns.Count - 1)
                        stringBuilder.AppendFormat("'{0}'", table.Columns[index].ColumnName);
                    else
                        stringBuilder.AppendFormat("'{0}',", table.Columns[index].ColumnName);
                }
            }

            stringBuilder.AppendLine("], ");

            defaultView.RowFilter = string.Format("Criteria='{0}'", ((IEnumerable<string>)str2.Split(',')).Last<string>());

            for (int index1 = 0; index1 < defaultView.Count; ++index1)
            {
                stringBuilder.Append(" [");

                for (int index2 = 0; index2 < table.Columns.Count; ++index2)
                {
                    if (table.Columns[index2].ColumnName != "Criteria")
                    {
                        if (index2 == table.Columns.Count - 1)
                            stringBuilder.AppendFormat("{0}", ValidateData(defaultView.ToTable().Rows[index1][index2].ToString()));
                        else if (table.Columns[index2].ColumnName == "WCCDate")
                            stringBuilder.AppendFormat("'{0}',", defaultView.ToTable().Rows[index1][index2].ToString());
                        else
                            stringBuilder.AppendFormat("{0},", ValidateData(defaultView.ToTable().Rows[index1][index2].ToString()));
                    }
                }

                if (index1 == defaultView.Count - 1)
                    stringBuilder.AppendLine("] ");
                else
                    stringBuilder.AppendLine("], ");
            }

            stringBuilder.Append("]); ");
            stringBuilder.Append("var options = { ");
            stringBuilder.AppendFormat("title: '{0} - {1}', ", ((IEnumerable<string>)str2.Split(',')).Last<string>(), str3);
            stringBuilder.Append("bar: { groupWidth: '100'}, ");
            stringBuilder.Append("legend: {textStyle: {color: '#000000'} }, ");
            stringBuilder.Append("vAxis: { title: 'Quarter', titleTextStyle: { color: '#000000' }, gridlines: { color: '#000000' }, baselineColor: '#000000', textStyle: {color:'#000000'} }, ");
            stringBuilder.Append(" hAxis: {title: 'Score', titleTextStyle: {color: '#000000'}, gridlines: {color: '#000000'}, baselineColor : '#000000',textStyle: {color:'#000000'} }, ");
            stringBuilder.Append("backgroundColor: {stroke:'#FFFFFF',strokeWidth: '#000000', fill: '#ffffff'}, ");
            stringBuilder.Append("is3D: true ");
            stringBuilder.Append(" };");
            stringBuilder.Append("var chart = new google.visualization.BarChart(document.getElementById('chart_div')); ");
            stringBuilder.Append("chart.draw(data, options); ");
            stringBuilder.Append("}; ");
            stringBuilder.Append("</script>");

            litJS.Text = stringBuilder.ToString();
        }

        private string GetQuarter(DataTable dt, string result)
        {
            DateTime dateTime = Convert.ToDateTime(result);

            foreach (DataRow row in (InternalDataCollectionBase)dt.Rows)
            {
                if (dateTime.Month >= Convert.ToInt32(row["StartMonth"]) && dateTime.Month <= Convert.ToInt32(row["EndMonth"]))
                    return row["Quarter"].ToString();
            }

            return "";
        }

        private string ValidateData(string value)
        {
            double result = 0.0;

            return double.TryParse(value, out result) ? value.Trim() : "0";
        }

        protected void Page_Init(object sender, EventArgs e)
        {
        }
    }
}