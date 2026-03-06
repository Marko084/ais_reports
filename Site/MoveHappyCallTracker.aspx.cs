using AIS;
using AISReports.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AISReports.Site
{
    public partial class MoveHappyCallTracker : NCDPageBase
    {
        #region
        private string filterByValue = "1=1";
        private List<string> responses = new List<string>();
        private string companyID;
        private string primaryKeyField;
        private string selectedTableName;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            selectedTableName = Request.QueryString["t"];
            companyID = "10122";

            if (!Page.IsPostBack)
                LoadTables();

            if (!(selectedTableName != "") || selectedTableName == null)
                return;

            LoadEditFields();
            CreateList();
        }

        private void LoadTables()
        {
            SqlDataReader sqlDataReader = DAL.ReturnDataReader("select * from DatabaseEditObjects where DataStoreType='CallCenter' and CompanyID=" + companyID + " order by ObjectDescription", CommandType.Text);
           
            while (sqlDataReader.Read())
            {
                ListItem listItem = new ListItem()
                {
                    Text = sqlDataReader["ObjectDescription"].ToString(),
                    Value = sqlDataReader["ObjectName"].ToString()
                };

                listItem.Attributes.Add("aria-primary-key-field", sqlDataReader["PrimaryKeyField"].ToString());
                listItem.Attributes.Add("aria-company-id", sqlDataReader["CompanyID"].ToString());

                if (sqlDataReader["FilterValue"] != null && sqlDataReader["FilterValue"].ToString() != "")
                    listItem.Attributes.Add("aria-table-filter", sqlDataReader["FilterValue"].ToString());

                ddlTables.Items.Add(listItem);

                selectedTableName = sqlDataReader["ObjectDescription"].ToString();

                listItem.Selected = true;
            }
        }

        private string GetFilterValue()
        {
            string filterValue = "";

            ListItem byText = ddlTables.Items.FindByText(selectedTableName);

            if (byText != null && byText.Attributes["aria-table-filter"] != null && byText.Attributes["aria-table-filter"] != "" && byText.Attributes["aria-table-filter"] != null)
                filterValue = byText.Attributes["aria-table-filter"].ToString();

            return filterValue;
        }

        private void LoadEditFields()
        {
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();

            List<FieldGroupList> source = new List<FieldGroupList>();

            bool flag = litHeaderRow.Text.Trim().Length == 0;

            SqlDataReader dr = DAL.ReturnDataReader("admin_GetTableEditFields", "CompanyID,TableName,DataStoreType", string.Format("{0},{1},CallCenter", companyID, ddlTables.SelectedValue));
            
            while (dr.Read())
            {
                if (dr["Question"].ToString() != "" && dr["Question"].ToString() != null && dr["Question"].ToString() != "Comments" && responses.Count<string>((Func<string, bool>)(r => r.ToLower().Contains(dr["ScoreValue"].ToString().Replace(",", "").Replace(" ", "")))) == 0)
                {
                    string str1 = string.Format("responseListName{0}", dr["ScoreValue"].ToString().Replace(",", "").Replace(" ", ""));
                    string str2 = dr["AnswerDescription"].ToString();

                    if (!responses.Contains(string.Format("{0}|{1}", str1, str2)))
                        responses.Add(string.Format("{0}|{1}^{2}", str1, str2, dr["ScoreValue"].ToString()));
                }

                if (source.Count<FieldGroupList>((Func<FieldGroupList, bool>)(g => g.GroupName == dr["Description"].ToString())) == 0 && dr["Description"].ToString() != "")
                    source.Add(new FieldGroupList()
                    {
                        GroupName = dr["Description"].ToString(),
                        GroupHtmlDiv = new HtmlGenericControl("div")
                    });

                if (dr["COLUMN_NAME"].ToString() == "Status" || dr["COLUMN_NAME"].ToString() == "Contacted")
                {
                    HtmlGenericControl child1 = new HtmlGenericControl("select");
                    HtmlGenericControl child2 = new HtmlGenericControl("span");
                    HtmlGenericControl child3 = new HtmlGenericControl("div");

                    child3.Attributes.Add("class", "edit-field");

                    child2.InnerText = SplitCamelCase(dr["COLUMN_NAME"].ToString());

                    child1.ID = string.Format("cmb{0}", dr["COLUMN_NAME"].ToString());
                    child1.Attributes.Add("type", "text");
                    child1.Attributes.Add("data-table-type", ddlTables.SelectedValue);
                    child1.Attributes.Add("class", "response-field");
                    child1.Attributes.Add("aria-data-type", dr["DATA_TYPE"].ToString().ToLower());
                    child1.Attributes.Add("data-field-name", dr["COLUMN_NAME"].ToString());
                    child1.Attributes.Add("aria-identity-column", "false");

                    child3.Controls.Add(child2);
                    child3.Controls.Add(child1);

                    source.Find(g => g.GroupName == dr["Description"].ToString()).GroupHtmlDiv.Controls.Add(child3);

                    if (flag)
                    {
                        litHeaderRow.Text += string.Format("<th>{0}</th>", SplitCamelCase(dr["COLUMN_NAME"].ToString()));

                        if (stringBuilder1.Length == 0)
                        {
                            stringBuilder1.AppendFormat("{0}", SplitCamelCase(dr["COLUMN_NAME"].ToString()));
                            stringBuilder2.AppendFormat("{0}", dr["COLUMN_NAME"].ToString());
                        }
                        else
                        {
                            stringBuilder1.AppendFormat(",{0}", SplitCamelCase(dr["COLUMN_NAME"].ToString()));
                            stringBuilder2.AppendFormat(",{0}", dr["COLUMN_NAME"].ToString());
                        }
                    }
                }
                else if (!Convert.ToBoolean(dr["is_computed"]))
                {
                    HtmlGenericControl child4 = new HtmlGenericControl("div");

                    child4.Attributes.Add("class", "edit-field");

                    HtmlGenericControl child5 = new HtmlGenericControl("span");
                    HtmlGenericControl child6;

                    if (dr["CHARACTER_MAXIMUM_LENGTH"].ToString() != "" && (Convert.ToInt32(dr["CHARACTER_MAXIMUM_LENGTH"].ToString()) > 505 || Convert.ToInt32(dr["CHARACTER_MAXIMUM_LENGTH"].ToString()) == -1))
                    {
                        child6 = new HtmlGenericControl("textarea");

                        child6.Attributes.Add("rows", "5");
                        child6.Attributes.Add("cols", "50");
                    }
                    else if (dr["DATA_TYPE"].ToString().ToLower() == "varchar" && dr.GetSchemaTable().Columns.Contains("Question") && dr["Question"].ToString() == "")
                    {
                        child6 = new HtmlGenericControl("input");

                        child6.Attributes.Add("type", "text");
                        child6.Attributes.Add("data-table-type", ddlTables.SelectedValue);
                    }
                    else if (dr["DATA_TYPE"].ToString().ToLower() == "varchar")
                    {
                        child6 = new HtmlGenericControl("input");

                        child6.Attributes.Add("type", "text");
                        child6.Attributes.Add("data-table-type", ddlTables.SelectedValue);
                    }
                    else if (dr["DATA_TYPE"].ToString().ToLower() == "bit")
                    {
                        child6 = new HtmlGenericControl("input");

                        child6.Attributes.Add("type", "checkbox");
                    }
                    else
                    {
                        child6 = new HtmlGenericControl("input");

                        child6.Attributes.Add("type", "text");
                    }
                    if (Convert.ToBoolean(dr["is_identity"]))
                    {
                        child6.Attributes.Add("aria-identity-column", "true");
                        child6.Attributes.Add("data-field-name", dr["COLUMN_NAME"].ToString());
                    }
                    else
                        child6.Attributes.Add("aria-identity-column", "false");

                    if (dr["Question"].ToString() != "")
                    {
                        child5.InnerText = dr["Question"].ToString();
                        child4.Attributes["class"] += " survey-question";
                    }
                    else
                        child5.InnerText = SplitCamelCase(dr["COLUMN_NAME"].ToString());

                    child6.ID = string.Format("txt{0}", dr["COLUMN_NAME"].ToString());
                    child6.Attributes.Add("class", "response-field");
                    child6.Attributes.Add("aria-data-type", dr["DATA_TYPE"].ToString().ToLower());
                    child6.Attributes.Add("data-field-name", dr["COLUMN_NAME"].ToString());

                    child4.Controls.Add(child5);
                    child4.Controls.Add(child6);

                    if (dr["COLUMN_NAME"].ToString().ToLower() == "importid" || dr["COLUMN_NAME"].ToString().ToLower() == "batchid" || Convert.ToBoolean(dr["is_identity"]))
                        child4.Attributes.Add("style", "display:none;");
                    else if (dr["COLUMN_NAME"].ToString().ToLower() == "companyid")
                    {
                        child6.Attributes.Add("value", companyID);
                        child4.Attributes.Add("style", "display:none;");
                    }
                    else if (dr["COLUMN_NAME"].ToString().ToLower() == "companycode")
                        child6.Attributes.Add("disabled", "true");

                    if (!Convert.ToBoolean(dr["is_nullable"]) && dr["COLUMN_DEFAULT"].ToString() != "")
                    {
                        child6.Disabled = true;
                        if (dr["COLUMN_DEFAULT"].ToString().ToLower().Contains("getdate"))
                            child6.Attributes.Add("value", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss tt"));
                    }

                    source.Find((Predicate<FieldGroupList>)(g => g.GroupName == dr["Description"].ToString())).GroupHtmlDiv.Controls.Add(child4);

                    if (dr["Question"].ToString() != "" && dr["Question"].ToString() != "Comments")
                    {
                        child6.Attributes.Add("response-list-type", string.Format("responseListName{0}", dr["ScoreValue"].ToString().Replace(",", "").Replace(" ", "")));
                        child5.Attributes.Add("class", "question-label");
                    }

                    if (flag)
                    {
                        litHeaderRow.Text += string.Format("<th>{0}</th>", SplitCamelCase(dr["COLUMN_NAME"].ToString()));
                        if (stringBuilder1.Length == 0)
                        {
                            stringBuilder1.AppendFormat("{0}", SplitCamelCase(dr["COLUMN_NAME"].ToString()));
                            stringBuilder2.AppendFormat("{0}", dr["COLUMN_NAME"].ToString());
                        }
                        else
                        {
                            stringBuilder1.AppendFormat(",{0}", SplitCamelCase(dr["COLUMN_NAME"].ToString()));
                            stringBuilder2.AppendFormat(",{0}", dr["COLUMN_NAME"].ToString());
                        }
                    }
                }
            }

            foreach (FieldGroupList fieldGroupList in source)
            {
                HtmlGenericControl child7 = new HtmlGenericControl("fieldset");
                HtmlGenericControl child8 = new HtmlGenericControl("legend");

                child8.InnerText = SplitCamelCase(fieldGroupList.GroupName);

                child7.Controls.Add(child8);

                if (fieldGroupList.GroupName == "HiddenField")
                    child7.Attributes.Add("style", "display:none;");

                if (fieldGroupList.GroupName.ToUpper() == "QUESTIONS")
                {
                    HtmlGenericControl child9 = new HtmlGenericControl("div");
                    HtmlGenericControl child10 = new HtmlGenericControl("span");

                    child10.Attributes.Add("class", "question-label total-score-label");

                    child9.Attributes.Add("class", "edit-field survey-question");

                    child10.InnerText = "Total Score:";

                    child9.Controls.Add(child10);

                    HtmlGenericControl child11 = new HtmlGenericControl("span");

                    child11.Attributes.Add("class", "response-score-value");
                    child11.Attributes.Add("id", "totalQuestionScore");

                    child9.Controls.Add(child11);

                    fieldGroupList.GroupHtmlDiv.Controls.Add(child9);
                }

                child7.Controls.Add(fieldGroupList.GroupHtmlDiv);

                phEditFields.Controls.Add(child7);
            }

            if (GetFilterValue() != "")
                filterByValue = GetFilterValue();

            pChart.Attributes["grid-query"] = string.Format("select {0} from {1} where {2} order by ImportID desc", stringBuilder2.ToString() == "" ? "*" : stringBuilder2.ToString(), ddlTables.SelectedValue, filterByValue);
            
            dr.Close();
            dr.Dispose();
        }

        private void CreateList()
        {
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            StringBuilder stringBuilder3 = new StringBuilder();

            stringBuilder1.AppendLine("<script type=\"text/javascript\">");
            stringBuilder1.AppendLine("function getResponseList(listName) { ");
            stringBuilder3.AppendLine("function getResponseScore(listName,responseDesc) {");

            foreach (string response in responses)
            {
                string str1 = ((IEnumerable<string>)response.Split('|')).First<string>();
                string str2 = ((IEnumerable<string>)((IEnumerable<string>)response.Split('|')).Last<string>().Split('^')).First<string>();
                string[] strArray = ((IEnumerable<string>)((IEnumerable<string>)response.Split('|')).Last<string>().Split('^')).Last<string>().Split(',');

                stringBuilder1.Append(string.Format(" var {0}=[ ", str1));

                int index = 0;
                string str3 = str2;
                char[] chArray = new char[1] { ',' };

                foreach (string str4 in str3.Split(chArray))
                {
                    string str5 = strArray[index];

                    if (index == 0)
                        stringBuilder1.Append("{");
                    else
                        stringBuilder1.Append(",{");

                    stringBuilder1.AppendFormat("label:\"{0}\", value :\"{1}\"", str4, str5);
                    stringBuilder1.Append("}");

                    ++index;
                }

                stringBuilder1.Append(" ]; ");
                stringBuilder1.AppendLine("");

                stringBuilder3.AppendLine(string.Format("if(listName=='{0}') ", str1) + " { return " + str1 + "; }");

                stringBuilder2.AppendLine(string.Format("if(listName=='{0}') ", str1) + " { return " + str1 + "; }");
            }

            stringBuilder1.AppendLine("");
            stringBuilder1.AppendLine(stringBuilder2.ToString());
            stringBuilder1.AppendLine("} ");
            stringBuilder1.AppendLine("</script>");

            litScript.Text = stringBuilder1.ToString();
        }

        private string SplitCamelCase(string str) => Regex.Replace(Regex.Replace(str, "(\\P{Ll})(\\P{Ll}\\p{Ll})", "$1 $2"), "(\\p{Ll})(\\P{Ll})", "$1 $2");
    }
}