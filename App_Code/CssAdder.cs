using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AISReports.App_Code
{
    public class CssAdder
    {
        public static void AddCss(string path, Page page)
        {
            string str1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] chArray = new char[12];
            Random random = new Random();
            for (int index = 0; index < chArray.Length; ++index)
                chArray[index] = str1[random.Next(str1.Length)];
            string str2 = new string(chArray);
            Literal child = new Literal()
            {
                Text = "<link href=\"" + page.ResolveUrl(path) + "?" + str2 + "\" type=\"text/css\" rel=\"stylesheet\" />"
            };
            page.Header.Controls.Add((Control)child);
        }
    }
}