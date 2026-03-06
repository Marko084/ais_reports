using System.Web.UI.HtmlControls;

namespace AIS
{
  public class FieldGroupList
  {
    public string FieldName { get; set; }

    public string GroupName { get; set; }

    public int SortOrder { get; set; }

    public bool GroupDivAddedTF { get; set; }

    public HtmlGenericControl GroupHtmlDiv { get; set; }
  }
}
