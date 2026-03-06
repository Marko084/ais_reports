using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace AISReports.App_Code
{
    public class NCDUserControlBase : UserControl
    {
        public string QueryName { get; set; }

        public string QueryType { get; set; }

        public string DisplayFields { get; set; }

        public string ChartType { get; set; }

        public int PageUserControlID { get; set; }

        public string PrimaryKeyField { get; set; }

        public string GridHeaderFields { get; set; }

        public string UserID { get; set; }

        public string CompanyID { get; set; }

        public string ChartTitle { get; set; }

        public string ChartTypes { get; set; }

        public string ChartDrillDown { get; set; }

        public string ChartHeight { get; set; }

        public string ChartWidth { get; set; }

        public string QueryParameters { get; set; }

        public string ChartSettings { get; set; }

        public string CurrentPageName { get; set; }

        public string UserLevel { get; set; }

        public string UserName { get; set; }

        public string GetUniqueID()
        {
            string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] array = new char[12];
            Random random = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = text[random.Next(text.Length)];
            }

            return new string(array);
        }
    }
}
