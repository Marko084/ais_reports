<%@ WebHandler Language="C#" Class="FusionChartXmlHandler" %>

using System;
using System.Web;
using System.Web.Security;

public class FusionChartXmlHandler : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/xml";

        FormsIdentity fi = (FormsIdentity)HttpContext.Current.User.Identity;
        FormsAuthenticationTicket ticket = fi.Ticket;

        string[] userData = ticket.UserData.Split(';');
        string chartType = GetChartType(context.Request["chart_type"]);
        string queryName = context.Request["query_name"];
        string companyId = userData[2];
        string queryParamNames = context.Request["pname"];
        string queryParamValues = context.Request["pvalue"];
        string chartTitle = context.Request["ctitle"];
        string customQueryName = context.Request["cquery"];
        string themeName = context.Request["theme"];
        string view3D = context.Request["view3d"];
        string numericPrefix = context.Request["npfx"];

        FusionChartsQuery fcq = new FusionChartsQuery();

        fcq.ChartType = GetChartType(chartType);
        fcq.CompanyId = companyId;
        fcq.ParamNames = queryParamNames;
        fcq.ParamValues = queryParamValues;
        fcq.QueryName = queryName;
        fcq.ChartTitle = chartTitle;
        fcq.CustomQueryName = customQueryName;
        fcq.ThemeName = themeName;
        fcq.View3D = view3D;
        fcq.CompanyCode = (userData[11] == "" ? userData[5] + "_" : userData[11] + "_");
        fcq.UserId = 0;
        fcq.NumberPrefix = (numericPrefix == null ? "" : numericPrefix);

        string result = fcq.GetChartXml();

        context.Response.Write(result);
    }

    private string GetChartType(string cType)
    {
        string result = "bar2d";

        if (cType.ToLower() == "column")
            return "column2d";
        else if (cType.ToLower() == "pie")
            return "pie2d";
        else if (cType.ToLower() == "doughnut")
            return "doughnut2d";
        else if (cType.ToLower() == "pie")
            return "pie2d";
        else if (cType.ToLower() == "line")
            return "line";
        else if (cType.ToLower() == "area")
            return "area2d";
        else if (cType.ToLower() == "bubble")
            return "bubble";

        return result;
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}