using AIS;
using AISReports.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AISReports.UserControls
{
    public partial class NCDEditGrid : NCDUserControlBase
    {
        #region
        private string companyID;
        private List<string> responses = new List<string>();
        private string selectedTableName;
        private string filterByValue = "";
        private List<PickListFieldMap> pickListFieldMap;
        private string hiddenColumns = "ImportID|CompanyID|BatchID|PolicyInfoID";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            companyID = Request.QueryString["cid"];
            selectedTableName = Request.QueryString["t"];

            pChart.Attributes.Add("grid-query", QueryName);
            pChart.Attributes.Add("grid-query-type", QueryType);
            pChart.Attributes.Add("grid-display-fields", DisplayFields);
            pChart.Attributes.Add("grid-type", "edit");

            if (PrimaryKeyField != null && PrimaryKeyField != "")
                pChart.Attributes.Add("primary-key-field", PrimaryKeyField);
            else
                PrimaryKeyField = "";

            LoadChartSettings();

            if (!Page.IsPostBack)
                LoadTables();

            if (litHeaderRow.Text.Trim().Length != 0)
                return;

            LoadColumns();
        }

        private void LoadColumns()
        {
            StringBuilder stringBuilder = new StringBuilder();
            int num = 0;

            litHeaderRow.Text = "";

            string gridHeaderFields = GridHeaderFields;
            char[] chArray = new char[1] { '|' };

            foreach (string str in gridHeaderFields.Split(chArray))
            {
                if (str.Trim().Length > 0)
                {
                    stringBuilder.AppendFormat("<th>{0}</th>", SplitCamelCase(str));
                    ++num;
                }
            }

            litHeaderRow.Text = stringBuilder.ToString();
            litNoData.Text = string.Format("<td colspan='{0}' class='dataTables_empty' >Processing...</td>", (num + 1));
        }

        private void GetFilterValue()
        {
            ListItem byText = ddlTables.Items.FindByText(selectedTableName);

            if (byText == null || byText.Attributes["aria-table-filter"] == null || !(byText.Attributes["aria-table-filter"] != ""))
                return;

            filterByValue = "where " + byText.Attributes["aria-table-filter"].ToString();
        }

        private void LoadChartSettings()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "cms_GetChartSettings";

                command.Parameters.AddWithValue("PageUserControlID", PageUserControlID);
                command.Parameters.AddWithValue("CompanyID", companyID);

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
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "detaillink")
                        pChart.Attributes.Add("detail-link", sqlDataReader["SettingValue"].ToString());
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "edittabletype")
                        pChart.Attributes.Add("data-store-type", sqlDataReader["SettingValue"].ToString());
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "primarykeyfield")
                    {
                        pChart.Attributes.Add("primary-key-field", sqlDataReader["SettingValue"].ToString());
                        PrimaryKeyField = sqlDataReader["SettingValue"].ToString();
                    }
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "editfilterby" && filterByValue == "")
                        filterByValue = sqlDataReader["SettingValue"].ToString().Replace("delete ", "").Replace("drop ", "");
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "showaddbutton")
                    {
                        if (sqlDataReader["SettingValue"].ToString().ToLower() == "true")
                            hypAddRecord.Attributes.Remove("style");
                    }
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "picklistmap")
                    {
                        pickListFieldMap = new List<PickListFieldMap>();

                        string str1 = "";
                        string str2 = "";
                        string str3 = sqlDataReader["SettingValue"].ToString();
                        char[] chArray = new char[1] { '|' };

                        foreach (string str4 in str3.Split(chArray))
                        {
                            if (((IEnumerable<string>)str4.Split('~')).First<string>().ToLower().Trim() != str1.ToLower().Trim())
                            {
                                if (((IEnumerable<string>)str4.Split('~')).First<string>().ToLower().Trim() != "")
                                    str1 = ((IEnumerable<string>)str4.Split('~')).First<string>().ToLower().Trim();
                            }

                            if (str4.Split('~')[1].ToLower().Trim() != str2.ToLower().Trim())
                            {
                                if (str4.Split('~')[1].ToLower().Trim() != "")
                                    str2 = str4.Split('~')[1].ToLower().Trim();
                            }

                            string str5;

                            if (((IEnumerable<string>)str4.Split('~')).Last<string>().ToLower().Trim() == "")
                                str5 = str2;
                            else
                                str5 = ((IEnumerable<string>)str4.Split('~')).Last<string>().ToLower().Trim();

                            pickListFieldMap.Add(new PickListFieldMap()
                            {
                                TableName = str1,
                                FieldName = str2,
                                MapToFieldName = str5
                            });
                        }
                    }
                }
            }
        }

        private void LoadTables()
        {
            StringBuilder stringBuilder = new StringBuilder();

            string str = "%";
            bool flag = false;

            ddlTables.Items.Clear();

            if (pChart.Attributes["data-store-type"] != null)
                str = pChart.Attributes["data-store-type"];

            stringBuilder.AppendFormat("SELECT * FROM [DatabaseEditObjects] WHERE DataStoreType like '{0}%' AND ", str);
            stringBuilder.AppendFormat("UserID in (0,{0}) and ", UserID == null ? "0" : UserID);
            stringBuilder.AppendFormat("CompanyID in(select CompanyID from Company where ParentCompanyID={0} or CompanyID={0}) ", companyID);
            stringBuilder.Append("and ObjectName<>'vw_Lytles_ScheduleBook' and DataStoreType not in ('CallCenter','RatingTracker') ");
            stringBuilder.Append("ORDER BY ObjectDescription ");

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();

                command.CommandType = CommandType.Text;
                command.CommandText = stringBuilder.ToString();

                SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (sqlDataReader.Read())
                {
                    ListItem listItem = new ListItem()
                    {
                        Text = sqlDataReader["ObjectDescription"].ToString(),
                        Value = sqlDataReader["ObjectName"].ToString()
                    };

                    listItem.Attributes.Add("aria-primary-key-field", sqlDataReader["PrimaryKeyField"].ToString());

                    if (sqlDataReader["FilterValue"] != null && sqlDataReader["FilterValue"].ToString() != "")
                        listItem.Attributes.Add("aria-table-filter", sqlDataReader["FilterValue"].ToString());

                    if (selectedTableName != null && selectedTableName.ToLower().Trim() == sqlDataReader["ObjectDescription"].ToString().ToLower().Trim())
                    {
                        listItem.Selected = true;
                        flag = true;
                    }

                    ddlTables.Items.Add(listItem);
                }

                GetFilterValue();

                if (flag)
                {
                    SetTablePrimaryKeyField();

                    if ((str.ToLower() == "policyinfo" || str.ToLower() == "claims" || str.ToLower() == "drivers" || str.ToLower() == "shipments") && str.ToLower() != "invoices")
                        LoadEditFields();
                    else
                        LoadEditFields2();

                    CreateList();
                }
                else
                {
                    if (ddlTables.Items.Count <= 0)
                        return;

                    ddlTables.SelectedIndex = 0;

                    SetTablePrimaryKeyField();

                    if (str.ToLower() != "claims" && str.ToLower() != "invoices")
                        LoadEditFields();
                    else
                        LoadEditFields2();

                    CreateList();
                }
            }
        }

        private void LoadEditFields2()
        {
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();

            bool flag = litHeaderRow.Text.Trim().Length == 0;

            SqlDataReader dr = DAL.ReturnDataReader("admin_GetTableEditFields", "CompanyID,TableName,KeyFieldName", string.Format("{0},{1},{2}", companyID, ddlTables.SelectedValue, PrimaryKeyField));
            
            while (dr.Read())
            {
                string str1 = "";
                string str2 = "";
                if (str1 != "")
                {
                    str2 = string.Format("responseListName{0}", str1.Replace(",", ""));

                    if (!responses.Contains(string.Format("{0}|{1}", str2, str1)))
                        responses.Add(string.Format("{0}|{1}", str2, str1));
                }
                HtmlGenericControl child1 = new HtmlGenericControl("div");
                child1.Attributes.Add("class", "edit-field");

                HtmlGenericControl child2 = new HtmlGenericControl("span");

                HtmlGenericControl child3;

                if (dr["CHARACTER_MAXIMUM_LENGTH"].ToString() != "" && (Convert.ToInt32(dr["CHARACTER_MAXIMUM_LENGTH"].ToString()) > 100 || Convert.ToInt32(dr["CHARACTER_MAXIMUM_LENGTH"].ToString()) == -1))
                {
                    child3 = new HtmlGenericControl("textarea");

                    child3.Attributes.Add("rows", "10");
                    child3.Attributes.Add("cols", "50");
                }
                else if (dr["DATA_TYPE"].ToString().ToLower() == "bit")
                {
                    child3 = new HtmlGenericControl("input");

                    child3.Attributes.Add("type", "checkbox");
                    child3.Attributes.Add("data-field-name", dr["COLUMN_NAME"].ToString());
                    child3.Attributes.Add("data-table-type", ddlTables.SelectedValue);
                }
                else if (dr["DATA_TYPE"].ToString().ToLower() == "varchar")
                {
                    child3 = new HtmlGenericControl("input");

                    child3.Attributes.Add("type", "text");
                    child3.Attributes.Add("data-field-name", dr["COLUMN_NAME"].ToString());
                    child3.Attributes.Add("data-table-type", ddlTables.SelectedValue);
                }
                else
                {
                    child3 = new HtmlGenericControl("input");

                    child3.Attributes.Add("type", "text");
                    child3.Attributes.Add("data-field-name", dr["COLUMN_NAME"].ToString());
                }
                if (pickListFieldMap != null && pickListFieldMap.Count(p => p.FieldName.ToLower() == dr["COLUMN_NAME"].ToString().ToLower()) > 0)
                {
                    PickListFieldMap plfMap = pickListFieldMap.Find(p => p.FieldName.ToLower() == dr["COLUMN_NAME"].ToString().ToLower());

                    child3.Attributes.Add("picklist-field-name", plfMap.MapToFieldName);
                    child3.Attributes.Add("picklist-table-type", plfMap.TableName);
                }

                if (Convert.ToBoolean(dr["is_identity"]))
                    child3.Attributes.Add("aria-identity-column", "true");
                else
                    child3.Attributes.Add("aria-identity-column", "false");

                if (Convert.ToBoolean(dr["is_computed"]))
                    child3.Attributes.Add("aria-computed-column", "true");
                else
                    child3.Attributes.Add("aria-computed-column", "false");

                child2.InnerText = SplitCamelCase(dr["COLUMN_NAME"].ToString());

                child3.ID = string.Format("txt{0}", dr["COLUMN_NAME"].ToString());
                child3.Attributes.Add("class", "response-field");
                child3.Attributes.Add("aria-data-type", dr["DATA_TYPE"].ToString().ToLower());

                if (str2 != "")
                {
                    child3.Attributes.Add("response-list-type", str2);
                    child2.Attributes.Add("class", "question-label");
                }

                child1.Controls.Add(child2);
                child1.Controls.Add(child3);

                if (dr["COLUMN_NAME"].ToString().ToLower() == "importid" || dr["COLUMN_NAME"].ToString().ToLower() == "companyid" || dr["COLUMN_NAME"].ToString().ToLower() == "batchid")
                    child1.Attributes.Add("style", "display:none;");

                if (!Convert.ToBoolean(dr["is_nullable"]) && dr["COLUMN_DEFAULT"].ToString() != "")
                {
                    child1.Attributes.Add("style", "display:none;");
                    child3.Attributes.Add("aria-default-column", "true");

                    if (dr["COLUMN_DEFAULT"].ToString().ToLower().Contains("getdate") || dr["COLUMN_DEFAULT"].ToString().ToLower().Contains("datetime"))
                        child3.Attributes.Add("aria-default-column-value", "gettimestamp");
                }

                phEditFields.Controls.Add(child1);

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

            if (pChart.Attributes["grid-query"].ToLower() == "editgridquery" && companyID == "9999")
                pChart.Attributes["grid-query"] = string.Format("SELECT {0} FROM {1} {2}", stringBuilder2.ToString(), ddlTables.SelectedValue, filterByValue);
            else
                pChart.Attributes["grid-query"] = string.Format("SELECT * FROM {0} {1}", ddlTables.SelectedValue, filterByValue);

            dr.Close();
            dr.Dispose();
        }

        private void LoadEditFields()
        {
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();

            bool flag = litHeaderRow.Text.Trim().Length == 0;

            SqlDataReader sqlDataReader = !(PrimaryKeyField != "") || PrimaryKeyField == null ? DAL.ReturnDataReader("admin_GetTableEditFields", "CompanyID,TableName", string.Format("{0},{1}", companyID, ddlTables.SelectedValue)) : DAL.ReturnDataReader("admin_GetTableEditFields", "CompanyID,TableName,KeyFieldName", string.Format("{0},{1},{2}", companyID, ddlTables.SelectedValue, PrimaryKeyField));
            
            while (sqlDataReader.Read())
            {
                if (!Convert.ToBoolean(sqlDataReader["is_computed"]))
                {
                    HtmlGenericControl child1 = new HtmlGenericControl("div");

                    child1.Attributes.Add("class", "edit-field");

                    HtmlGenericControl child2 = new HtmlGenericControl("span");
                    HtmlGenericControl child3;

                    if (sqlDataReader["CHARACTER_MAXIMUM_LENGTH"].ToString() != "" && (Convert.ToInt32(sqlDataReader["CHARACTER_MAXIMUM_LENGTH"].ToString()) > 505 || Convert.ToInt32(sqlDataReader["CHARACTER_MAXIMUM_LENGTH"].ToString()) == -1))
                    {
                        child3 = new HtmlGenericControl("textarea");

                        child3.Attributes.Add("rows", "10");
                        child3.Attributes.Add("cols", "50");
                    }
                    else if (sqlDataReader["DATA_TYPE"].ToString().ToLower() == "varchar" && sqlDataReader.GetSchemaTable().Columns.Contains("Question") && sqlDataReader["Question"].ToString() == "")
                    {
                        child3 = new HtmlGenericControl("input");

                        child3.Attributes.Add("type", "text");
                        child3.Attributes.Add("data-field-name", sqlDataReader["COLUMN_NAME"].ToString());
                        child3.Attributes.Add("data-table-type", ddlTables.SelectedValue);
                    }
                    else if (sqlDataReader["DATA_TYPE"].ToString().ToLower() == "bit")
                    {
                        child3 = new HtmlGenericControl("input");

                        child3.Attributes.Add("type", "checkbox");
                        child3.Attributes.Add("data-field-name", sqlDataReader["COLUMN_NAME"].ToString());
                        child3.Attributes.Add("data-table-type", ddlTables.SelectedValue);
                    }
                    else if (sqlDataReader["DATA_TYPE"].ToString().ToLower() == "varchar")
                    {
                        child3 = new HtmlGenericControl("input");

                        child3.Attributes.Add("type", "text");
                        child3.Attributes.Add("data-field-name", sqlDataReader["COLUMN_NAME"].ToString());
                        child3.Attributes.Add("data-table-type", ddlTables.SelectedValue);
                    }
                    else
                    {
                        child3 = new HtmlGenericControl("input");

                        child3.Attributes.Add("type", "text");
                    }

                    if (Convert.ToBoolean(sqlDataReader["is_identity"]))
                    {
                        child3.Attributes.Add("aria-identity-column", "true");
                        child3.Attributes.Add("data-field-name", sqlDataReader["COLUMN_NAME"].ToString());
                    }
                    else
                        child3.Attributes.Add("aria-identity-column", "false");

                    if (sqlDataReader.GetSchemaTable().Columns.Contains("Question") && sqlDataReader["Question"].ToString() != "")
                        child2.InnerText = sqlDataReader["Question"].ToString();
                    else
                        child2.InnerText = SplitCamelCase(sqlDataReader["COLUMN_NAME"].ToString());

                    child3.ID = string.Format("txt{0}", sqlDataReader["COLUMN_NAME"].ToString());
                    child3.Attributes.Add("class", "response-field");
                    child3.Attributes.Add("aria-data-type", sqlDataReader["DATA_TYPE"].ToString().ToLower());

                    child1.Controls.Add(child2);
                    child1.Controls.Add(child3);

                    if (sqlDataReader["COLUMN_NAME"].ToString().ToLower() == "importid" || sqlDataReader["COLUMN_NAME"].ToString().ToLower() == "batchid" || Convert.ToBoolean(sqlDataReader["is_identity"]))
                        child1.Attributes.Add("style", "display:none;");
                    else if (sqlDataReader["COLUMN_NAME"].ToString().ToLower() == "companyid" && companyID == "10025" && ddlTables.SelectedValue.ToLower() == "driverinfo")
                    {
                        child3.Attributes.Add("value", "1");
                        child3.Attributes.Add("disabled", "true");
                    }
                    else if (sqlDataReader["COLUMN_NAME"].ToString().ToLower() == "companyid")
                    {
                        child3.Attributes.Add("value", companyID);
                        child1.Attributes.Add("style", "display:none;");
                    }
                    else if (sqlDataReader["COLUMN_NAME"].ToString().ToLower() == "companycode")
                        child3.Attributes.Add("disabled", "true");

                    if (!Convert.ToBoolean(sqlDataReader["is_nullable"]) && sqlDataReader["COLUMN_DEFAULT"].ToString() != "")
                    {
                        child3.Disabled = true;

                        if (sqlDataReader["COLUMN_DEFAULT"].ToString().ToLower().Contains("getdate"))
                            child3.Attributes.Add("value", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss tt"));
                    }

                    phEditFields.Controls.Add(child1);

                    if (flag)
                    {
                        litHeaderRow.Text += string.Format("<th>{0}</th>", SplitCamelCase(sqlDataReader["COLUMN_NAME"].ToString()));

                        if (stringBuilder1.Length == 0)
                        {
                            stringBuilder1.AppendFormat("{0}", SplitCamelCase(sqlDataReader["COLUMN_NAME"].ToString()));
                            stringBuilder2.AppendFormat("{0}", sqlDataReader["COLUMN_NAME"].ToString());
                        }
                        else
                        {
                            stringBuilder1.AppendFormat(",{0}", SplitCamelCase(sqlDataReader["COLUMN_NAME"].ToString()));
                            stringBuilder2.AppendFormat(",{0}", sqlDataReader["COLUMN_NAME"].ToString());
                        }
                    }
                }
            }

            if (pChart.Attributes["grid-query"].ToLower() == "editgridquery")
            {
                if (ddlTables.SelectedItem.Attributes["aria-table-filter"] != null && (filterByValue == null || filterByValue == ""))
                    filterByValue = string.Format("where {0} ", ddlTables.SelectedItem.Attributes["aria-table-filter"].ToString());

                if (companyID == "9999")
                    pChart.Attributes["grid-query"] = string.Format("SELECT {0} FROM {1} {2}", stringBuilder2.ToString(), ddlTables.SelectedValue, filterByValue);
                else if (ddlTables.SelectedValue.Trim().StartsWith("vw"))
                {
                    stringBuilder1.Append(",CompanyCode");
                    litHeaderRow.Text += "<th>CompanyCode</th>";
                    pChart.Attributes["grid-query"] = string.Format("SELECT {0},CompanyCode FROM {1} {2}", stringBuilder2.ToString(), ddlTables.SelectedValue, filterByValue);
                }
                else
                    pChart.Attributes["grid-query"] = string.Format("SELECT {0} FROM {1} {2}", stringBuilder2.ToString() == "" ? "*" : stringBuilder2.ToString(), ddlTables.SelectedValue, filterByValue);
            }
            else
                pChart.Attributes["grid-query"] = string.Format("SELECT * FROM {0} {1}", ddlTables.SelectedValue, filterByValue);

            sqlDataReader.Close();
            sqlDataReader.Dispose();
        }

        private void CreateList()
        {
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();

            stringBuilder1.AppendLine("<script type=\"text/javascript\">");
            stringBuilder1.AppendLine("function getResponseList(listName) { ");

            foreach (string response in responses)
            {
                string str1 = ((IEnumerable<string>)response.Split('|')).First<string>();
                string str2 = ((IEnumerable<string>)response.Split('|')).Last<string>();

                stringBuilder1.Append(string.Format(" var {0}=[ ", str1));

                int num = 0;
                string str3 = str2;
                char[] chArray = new char[1] { ',' };

                foreach (string str4 in str3.Split(chArray))
                {
                    if (num == 0)
                        stringBuilder1.AppendFormat("\"{0}\"", str4);
                    else
                        stringBuilder1.AppendFormat(",\"{0}\"", str4);
                    ++num;
                }

                stringBuilder1.Append(" ]; ");
                stringBuilder1.AppendLine("");
                stringBuilder2.AppendLine(string.Format("if(listName=='{0}') ", str1) + " { return " + str1 + "; }");
            }

            stringBuilder1.AppendLine("");
            stringBuilder1.AppendLine(stringBuilder2.ToString());
            stringBuilder1.AppendLine("} ");
            stringBuilder1.AppendLine("</script>");

            litScript.Text = stringBuilder1.ToString();
        }

        private void SetTablePrimaryKeyField()
        {
            string attribute = ddlTables.Items[ddlTables.SelectedIndex].Attributes["aria-primary-key-field"];

            if (attribute == "")
                attribute = pChart.Attributes["primary-key-field"];
            if (pChart.Attributes["primary-key-field"] != null)
                pChart.Attributes["primary-key-field"] = attribute;
            else
                pChart.Attributes.Add("primary-key-field", attribute);
        }

        private string SplitCamelCase(string str) => Regex.Replace(Regex.Replace(str, "(\\P{Ll})(\\P{Ll}\\p{Ll})", "$1 $2"), "(\\p{Ll})(\\P{Ll})", "$1 $2");
    }
}
