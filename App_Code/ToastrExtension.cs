using System.Web.UI;

namespace AIS
{
  public static class ToastrExtension
  {
    public static void ShowToastr(this Page page, string message, string title, string type = "info") => page.ClientScript.RegisterStartupScript(page.GetType(), "toastr_message", string.Format("window.parent.CloseEditDialog('{0}','{1}','{2}');", (object) type.ToLower(), (object) message, (object) title), true);
  }
}
