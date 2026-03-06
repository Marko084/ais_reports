using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace AISReports.App_Code
{
    public class NCDPageBase : Page
    {
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