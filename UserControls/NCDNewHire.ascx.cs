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
    public partial class NCDNewHire : NCDUserControlBase
    {
        #region
        private string companyID;
        private List<string> responses = new List<string>();
        private string selectedTableName;
        private string filterByValue = "";
        private List<ClaimsPickListFieldMap> pickListFieldMap;
        private List<FieldGroupList> fieldGroupList;
        private bool useAdjustorClaimsApprovalModuleTF;
        private bool useGMClaimsApprovalModuleTF;
        private bool showSurveyDetailLinkTF;
        private string defaultTableName;
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            companyID = Request.QueryString["cid"];
            selectedTableName = Request.QueryString["t"];

            pChart.Attributes.Add("grid-query", QueryName);
            pChart.Attributes.Add("grid-query-type", QueryType);
            pChart.Attributes.Add("grid-display-fields", DisplayFields);
            pChart.Attributes.Add("grid-type", "newhire");
            pChart.Attributes.Add("dynamic-query", "false");

            if (PrimaryKeyField != null && PrimaryKeyField != "")
                pChart.Attributes.Add("primary-key-field", PrimaryKeyField);
            else
                PrimaryKeyField = "";

            GetFieldGroupLists();
            LoadChartSettings();

            if (!Page.IsPostBack)
            {
                LoadTables();
                LoadFieldMaps();
            }

            if (litHeaderRow.Text.Trim().Length == 0)
                LoadColumns();

            txtSendToAddress.Text = Convert.ToString(DAL.ReturnScalarValue("ais_GetWorkFlowEmail", "CompanyID,UserID", companyID + "," + UserID));
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

        private void LoadChartSettings()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;

                command.CommandText = "cms_GetChartSettings";
                command.Parameters.AddWithValue("PageUserControlID", PageUserControlID);

                SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (sqlDataReader.Read())
                {
                    if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "height")
                        pChart.Height = Unit.Pixel(Convert.ToInt32(sqlDataReader["SettingValue"]));
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "defaulttablename")
                        defaultTableName = sqlDataReader["SettingValue"].ToString();
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "charttitle")
                    {
                        pChart.Attributes.Add("chart-title", sqlDataReader["SettingValue"].ToString());
                        lblChartTitle.Text = sqlDataReader["SettingValue"].ToString();
                    }
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "detaillink")
                    {
                        pChart.Attributes.Add("detail-link", sqlDataReader["SettingValue"].ToString());
                        showSurveyDetailLinkTF = true;
                    }
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "edittabletype")
                        pChart.Attributes.Add("data-store-type", sqlDataReader["SettingValue"].ToString());
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "primarykeyfield")
                    {
                        pChart.Attributes.Add("primary-key-field", sqlDataReader["SettingValue"].ToString());
                        PrimaryKeyField = sqlDataReader["SettingValue"].ToString();
                    }
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "editfilterby")
                        filterByValue = sqlDataReader["SettingValue"].ToString().Replace("delete ", "").Replace("drop ", "");
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "showaddbutton")
                    {
                        if (sqlDataReader["SettingValue"].ToString().ToLower() == "true")
                            hypAddRecord.Attributes.Remove("style");
                    }
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "showdocumentimagebutton")
                    {
                        if (sqlDataReader["SettingValue"].ToString().ToLower() == "true")
                            hypViewDocuments.Attributes.Remove("style");
                    }
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "showcreateicsbutton")
                        pChart.Attributes.Add("createics", sqlDataReader["SettingValue"].ToString().ToLower());
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "picklistmap")
                    {
                        pickListFieldMap = new List<ClaimsPickListFieldMap>();

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

                            pickListFieldMap.Add(new ClaimsPickListFieldMap()
                            {
                                TableName = str1,
                                FieldName = str2,
                                MapToFieldName = str5
                            });
                        }
                    }
                    else if (sqlDataReader["SettingName"].ToString().ToLower() == "gridreadonly")
                        pChart.Attributes["grid-read-only"] = sqlDataReader["SettingValue"].ToString().ToLower();
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "dynamicquery")
                        pChart.Attributes["dynamic-query"] = sqlDataReader["SettingValue"].ToString().ToLower();
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "showemaillink")
                        pChart.Attributes.Add("show-email-link", sqlDataReader["SettingValue"].ToString().ToLower());
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "sendtofield")
                        pChart.Attributes.Add("send-to-field", sqlDataReader["SettingValue"].ToString());
                }
            }
        }

        private void LoadFieldMaps()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("select * from cms_FieldMaps where UserControlName='NCDNewHire.ascx' and CompanyID={0} ", companyID);

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();

                command.CommandType = CommandType.Text;
                command.CommandText = stringBuilder.ToString();

                SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (sqlDataReader.Read())
                {
                    txtFieldMapList.Attributes.Add("aria-map-table-name", sqlDataReader["SourceMapTableName"].ToString());
                    txtFieldMapList.Attributes.Add("aria-map-key-field", sqlDataReader["KeyFieldName"].ToString());
                    txtFieldMapList.Text = sqlDataReader["MapFieldList"].ToString();
                }

                sqlDataReader.Close();
                sqlDataReader.Dispose();
            }
        }

        private void LoadTables()
        {
            StringBuilder stringBuilder = new StringBuilder();

            string str = "%";
            bool flag = false;

            ddlTables.Items.Clear();

            if (defaultTableName != null)
            {
                ListItem listItem = new ListItem()
                {
                    Text = defaultTableName,
                    Value = defaultTableName
                };

                listItem.Attributes.Add("aria-primary-key-field", PrimaryKeyField);
                listItem.Selected = true;

                flag = true;

                ddlTables.Items.Add(listItem);
            }
            else
            {
                if (pChart.Attributes["data-store-type"] != null)
                    str = pChart.Attributes["data-store-type"];

                stringBuilder.AppendFormat("SELECT * FROM [DatabaseEditObjects] WHERE DataStoreType like '{0}%' AND ", str);
                stringBuilder.AppendFormat("CompanyID in(select CompanyID from Company where ParentCompanyID={0} or CompanyID={0}) ", companyID);
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

                        if (selectedTableName != null && selectedTableName.ToLower().Trim() == sqlDataReader["ObjectName"].ToString().ToLower().Trim())
                        {
                            listItem.Selected = true;
                            flag = true;
                        }

                        ddlTables.Items.Add(listItem);
                    }
                }
            }

            if (flag)
            {
                SetTablePrimaryKeyField();

                if ((str.ToLower() == "claims" || str.ToLower() == "drivers") && str.ToLower() != "invoices")
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

        private void LoadEditFields2()
        {
            StringBuilder stringBuilder = new StringBuilder();

            HtmlGenericControl child1 = (HtmlGenericControl)null;
            HtmlGenericControl child2 = (HtmlGenericControl)null;

            bool flag = litHeaderRow.Text.Trim().Length == 0;

            SqlDataReader dr = (SqlDataReader)DAL.ReturnDataReader("admin_GetTableEditFields", "CompanyID,TableName,KeyFieldName", string.Format("{0},{1},{2}", companyID, ddlTables.SelectedValue, PrimaryKeyField));
            
            while (dr.Read())
            {
                HtmlSelect child3 = (HtmlSelect)null;
                HtmlGenericControl child4 = (HtmlGenericControl)null;

                string str1 = "";
                string str2 = "";

                if (str1 != "")
                {
                    str2 = string.Format("responseListName{0}", str1.Replace(",", ""));

                    if (!responses.Contains(string.Format("{0}|{1}", str2, str1)))
                        responses.Add(string.Format("{0}|{1}", str2, str1));
                }

                HtmlGenericControl child5 = new HtmlGenericControl("div");

                child5.Attributes.Add("class", "edit-field");

                HtmlGenericControl child6 = new HtmlGenericControl("span");

                if (dr["COLUMN_NAME"].ToString() == "BranchLocation" || dr["COLUMN_NAME"].ToString() == "PayType" || dr["COLUMN_NAME"].ToString() == "GuaranteedPeriod" || dr["COLUMN_NAME"].ToString() == "PayRate")
                {
                    child3 = new HtmlSelect();

                    child3.Attributes.Add("type", "text");
                    child3.Attributes.Add("data-field-name", dr["COLUMN_NAME"].ToString());
                    child3.Attributes.Add("data-table-type", ddlTables.SelectedValue);
                    child3.ID = string.Format("ddl{0}", dr["COLUMN_NAME"].ToString());
                    child3.Attributes.Add("aria-data-type", dr["DATA_TYPE"].ToString().ToLower());
                    child3.Attributes.Add("aria-computed-column", "false");
                    child3.Attributes.Add("aria-identity-column", "false");
                }
                else if (dr["CHARACTER_MAXIMUM_LENGTH"].ToString() != "" && (Convert.ToInt32(dr["CHARACTER_MAXIMUM_LENGTH"].ToString()) > 100 || Convert.ToInt32(dr["CHARACTER_MAXIMUM_LENGTH"].ToString()) == -1))
                {
                    child4 = new HtmlGenericControl("textarea");

                    child4.Attributes.Add("rows", "10");
                    child4.Attributes.Add("cols", "50");
                }
                else if (dr["DATA_TYPE"].ToString().ToLower() == "varchar")
                {
                    child4 = new HtmlGenericControl("input");

                    child4.Attributes.Add("type", "text");
                    child4.Attributes.Add("data-field-name", dr["COLUMN_NAME"].ToString());
                    child4.Attributes.Add("data-table-type", ddlTables.SelectedValue);
                }
                else if (dr["DATA_TYPE"].ToString().ToLower() == "bit")
                {
                    child4 = new HtmlGenericControl("input");

                    child4.Attributes.Add("type", "checkbox");
                    child4.Attributes.Add("data-field-name", dr["COLUMN_NAME"].ToString());
                    child4.Attributes.Add("data-table-type", ddlTables.SelectedValue);
                }
                else
                {
                    child4 = new HtmlGenericControl("input");

                    child4.Attributes.Add("type", "text");
                    child4.Attributes.Add("data-field-name", dr["COLUMN_NAME"].ToString());
                }

                if (pickListFieldMap != null && pickListFieldMap.Count(p => p.FieldName.ToLower() == dr["COLUMN_NAME"].ToString().ToLower()) > 0)
                {
                    ClaimsPickListFieldMap plfMap = pickListFieldMap.Find(p => p.FieldName.ToLower() == dr["COLUMN_NAME"].ToString().ToLower());
                    child4.Attributes.Add("picklist-field-name", plfMap.MapToFieldName);
                    child4.Attributes.Add("picklist-table-type", plfMap.TableName);
                }

                if (Convert.ToBoolean(dr["is_identity"]))
                    child4.Attributes.Add("aria-identity-column", "true");
                else
                    child4?.Attributes.Add("aria-identity-column", "false");

                if (Convert.ToBoolean(dr["is_computed"]) && child4 != null)
                {
                    child4.Attributes.Add("aria-computed-column", "true");
                    child4.Attributes.Add("disabled", "disabled");
                }
                else
                    child4?.Attributes.Add("aria-computed-column", "false");

                child6.InnerText = SplitCamelCase(dr["COLUMN_NAME"].ToString());

                if (child4 != null)
                {
                    child4.ID = string.Format("txt{0}", dr["COLUMN_NAME"].ToString());
                    child4.Attributes.Add("class", "response-field");
                    child4.Attributes.Add("aria-data-type", dr["DATA_TYPE"].ToString().ToLower());
                }

                if (dr["COLUMN_NAME"].ToString().ToLower() == "createddate" || dr["COLUMN_NAME"].ToString().ToLower() == "lastupdateddate" || dr["COLUMN_NAME"].ToString().ToLower() == "createdby" || dr["COLUMN_NAME"].ToString().ToLower() == "lastupdatedby")
                    child4.Attributes.Add("disabled", "disabled");

                if (UserLevel.ToLower() == "manager" && child4 != null)
                {
                    if (dr["COLUMN_NAME"].ToString().ToLower() == "payrate" || dr["COLUMN_NAME"].ToString().ToLower() == "paytype" || dr["COLUMN_NAME"].ToString() == "PayrollAdminCompletedBy" || dr["COLUMN_NAME"].ToString() == "PayrollEnteredDate" || dr["COLUMN_NAME"].ToString() == "GMApprovalName" || dr["COLUMN_NAME"].ToString() == "GMApprovalDate" || dr["COLUMN_NAME"].ToString() == "VerificationAdminName" || dr["COLUMN_NAME"].ToString() == "VerificationAdminDate" || dr["COLUMN_NAME"].ToString() == "ExecutiveApprovalName" || dr["COLUMN_NAME"].ToString() == "ExecutiveApprovalDate" || dr["COLUMN_NAME"].ToString() == "NegativeDrugResultReceived" || dr["COLUMN_NAME"].ToString() == "DrugTestingClinic")
                        child4.Attributes.Add("disabled", "disabled");
                }
                else if (UserLevel.ToLower() == "gm" && child4 != null)
                {
                    if (dr["COLUMN_NAME"].ToString().ToLower() == "payrate" || dr["COLUMN_NAME"].ToString().ToLower() == "paytype" || dr["COLUMN_NAME"].ToString() == "PayrollAdminCompletedBy" || dr["COLUMN_NAME"].ToString() == "PayrollEnteredDate" || dr["COLUMN_NAME"].ToString() == "VerificationAdminName" || dr["COLUMN_NAME"].ToString() == "VerificationAdminDate" || dr["COLUMN_NAME"].ToString() == "ExecutiveApprovalName" || dr["COLUMN_NAME"].ToString() == "ExecutiveApprovalDate" || dr["COLUMN_NAME"].ToString() == "GMApprovalName" || dr["COLUMN_NAME"].ToString() == "NegativeDrugResultReceived" || dr["COLUMN_NAME"].ToString() == "DrugTestingClinic")
                        child4.Attributes.Add("disabled", "disabled");
                }
                else if (UserLevel.ToLower() == "corp" && child4 != null)
                {
                    if (dr["COLUMN_NAME"].ToString() == "PayrollAdminCompletedBy" || dr["COLUMN_NAME"].ToString() == "PayrollEnteredDate" || dr["COLUMN_NAME"].ToString() == "GMApprovalName" || dr["COLUMN_NAME"].ToString() == "GMApprovalDate" || dr["COLUMN_NAME"].ToString() == "ExecutiveApprovalName" || dr["COLUMN_NAME"].ToString() == "ExecutiveApprovalDate" || dr["COLUMN_NAME"].ToString() == "VerificationAdminName")
                        child4.Attributes.Add("disabled", "disabled");

                    if (dr["COLUMN_NAME"].ToString().ToLower() == "payrate" || dr["COLUMN_NAME"].ToString().ToLower() == "paytype")
                        child5.Attributes.Add("style", "display:none;");
                }
                else if (UserLevel.ToLower() == "exec" && child4 != null)
                {
                    if (dr["COLUMN_NAME"].ToString().ToLower() == "payrate" || dr["COLUMN_NAME"].ToString().ToLower() == "paytype" || dr["COLUMN_NAME"].ToString() == "PayrollAdminCompletedBy" || dr["COLUMN_NAME"].ToString() == "PayrollEnteredDate" || dr["COLUMN_NAME"].ToString() == "GMApprovalName" || dr["COLUMN_NAME"].ToString() == "GMApprovalDate" || dr["COLUMN_NAME"].ToString() == "VerificationAdminName" || dr["COLUMN_NAME"].ToString() == "VerificationAdminDate" || dr["COLUMN_NAME"].ToString() == "ExecutiveApprovalName" || dr["COLUMN_NAME"].ToString() == "NegativeDrugResultReceived" || dr["COLUMN_NAME"].ToString() == "DrugTestingClinic")
                        child4.Attributes.Add("disabled", "disabled");
                }
                else if (UserLevel.ToLower() == "finance" && child4 != null && (dr["COLUMN_NAME"].ToString() == "GMApprovalName" || dr["COLUMN_NAME"].ToString() == "GMApprovalDate" || dr["COLUMN_NAME"].ToString() == "VerificationAdminName" || dr["COLUMN_NAME"].ToString() == "VerificationAdminDate" || dr["COLUMN_NAME"].ToString() == "ExecutiveApprovalName" || dr["COLUMN_NAME"].ToString() == "ExecutiveApprovalDate" || dr["COLUMN_NAME"].ToString() == "PayrollAdminCompletedBy" || dr["COLUMN_NAME"].ToString() == "NegativeDrugResultReceived" || dr["COLUMN_NAME"].ToString() == "DrugTestingClinic"))
                    child4.Attributes.Add("disabled", "disabled");

                if (str2 != "")
                {
                    child4.Attributes.Add("response-list-type", str2);
                    child6.Attributes.Add("class", "question-label");
                }

                child5.Controls.Add(child6);

                if (child3 != null)
                    child5.Controls.Add(child3);
                else
                    child5.Controls.Add(child4);

                FieldGroupList fgList = fieldGroupList.Find(g => g.FieldName.ToLower() == dr["COLUMN_NAME"].ToString().ToLower());

                if (fgList != null)
                {
                    child5.Attributes.Add("aria-group-name", fgList.GroupName);
                    if (!fgList.GroupDivAddedTF)
                    {
                        child1 = new HtmlGenericControl("div");

                        child1.Attributes.Add("class", "accordion");
                        child1.Attributes.Add("aria-group-section", fgList.GroupName);

                        child2 = new HtmlGenericControl("div");

                        HtmlGenericControl child7 = new HtmlGenericControl("h3");

                        child7.InnerHtml = string.Format("<span class='accordion-text'>{0}</span><img class='data-indicator' src=''/>", fgList.GroupName);
                        
                        child1.Controls.Add(child7);

                        phEditFields.Controls.Add(child1);

                        foreach (FieldGroupList fieldGroup in fieldGroupList)
                        {
                            if (fieldGroup.GroupName == fieldGroup.GroupName)
                                fieldGroup.GroupDivAddedTF = true;
                        }
                    }

                    child2.Controls.Add(child5);
                    child1.Controls.Add(child2);
                }

                if (dr["COLUMN_NAME"].ToString().ToLower() == "importid" || dr["COLUMN_NAME"].ToString().ToLower() == "companyid" || dr["COLUMN_NAME"].ToString().ToLower() == "batchid")
                    child5.Attributes.Add("style", "display:none;");
                if (Convert.ToBoolean(dr["is_indicator"]))
                {
                    child5.Attributes.Add("style", "display:none;");
                    child4.Attributes.Add("aria-indicator-column", "true");
                }
                if (dr["COLUMN_NAME"].ToString().ToLower() == "originatoremail")
                {
                    child4.Attributes.Add("disabled", "disabled");
                    txtOriginatorEmail.Text = Convert.ToString(DAL.ReturnScalarValue(string.Format("select Email from [user] where UserID={0}", UserID), true));
                }
                if (fieldGroupList == null)
                    phEditFields.Controls.Add(child5);

                if (flag)
                {
                    litHeaderRow.Text += string.Format("<th>{0}</th>", SplitCamelCase(dr["COLUMN_NAME"].ToString()));

                    if (stringBuilder.Length == 0)
                        stringBuilder.AppendFormat("{0}", SplitCamelCase(dr["COLUMN_NAME"].ToString()));
                    else
                        stringBuilder.AppendFormat(",{0}", SplitCamelCase(dr["COLUMN_NAME"].ToString()));
                }
            }

            if (pChart.Attributes["grid-query"].ToLower() == "editgridquery")
                pChart.Attributes["grid-query"] = string.Format("SELECT * FROM {0} ", ddlTables.SelectedValue);

            dr.Close();
            dr.Dispose();
        }

        private void LoadEditFields()
        {
            StringBuilder stringBuilder = new StringBuilder();

            bool flag = litHeaderRow.Text.Trim().Length == 0;

            SqlDataReader dr = !(PrimaryKeyField != "") || PrimaryKeyField == null ? (SqlDataReader)DAL.ReturnDataReader("admin_GetTableEditFields", "CompanyID,TableName", string.Format("{0},{1}", companyID, ddlTables.SelectedValue)) : (SqlDataReader)DAL.ReturnDataReader("admin_GetTableEditFields", "CompanyID,TableName,KeyFieldName", string.Format("{0},{1},{2}", companyID, ddlTables.SelectedValue, PrimaryKeyField));
           
            while (dr.Read())
            {
                if (!Convert.ToBoolean(dr["is_computed"]))
                {
                    HtmlGenericControl child1 = new HtmlGenericControl("div");

                    child1.Attributes.Add("class", "edit-field");

                    HtmlGenericControl child2 = new HtmlGenericControl("span");
                    HtmlGenericControl child3;

                    if (dr["CHARACTER_MAXIMUM_LENGTH"].ToString() != "" && (Convert.ToInt32(dr["CHARACTER_MAXIMUM_LENGTH"].ToString()) > 505 || Convert.ToInt32(dr["CHARACTER_MAXIMUM_LENGTH"].ToString()) == -1))
                    {
                        child3 = new HtmlGenericControl("textarea");

                        child3.Attributes.Add("rows", "10");
                        child3.Attributes.Add("cols", "50");
                    }
                    else if (dr["DATA_TYPE"].ToString().ToLower() == "varchar" && dr.GetSchemaTable().Columns.Contains("Question") && dr["Question"].ToString() == "")
                    {
                        child3 = new HtmlGenericControl("input");

                        child3.Attributes.Add("type", "text");
                        child3.Attributes.Add("data-field-name", dr["COLUMN_NAME"].ToString());
                        child3.Attributes.Add("data-table-type", ddlTables.SelectedValue);
                    }
                    else if (dr["DATA_TYPE"].ToString().ToLower() == "bit")
                    {
                        child3 = new HtmlGenericControl("input");

                        child3.Attributes.Add("type", "checkbox");
                        child3.Attributes.Add("data-field-name", dr["COLUMN_NAME"].ToString());
                        child3.Attributes.Add("data-table-type", ddlTables.SelectedValue);
                    }
                    else
                    {
                        child3 = new HtmlGenericControl("input");

                        child3.Attributes.Add("type", "text");
                    }
                    if (Convert.ToBoolean(dr["is_identity"]))
                    {
                        child3.Attributes.Add("aria-identity-column", "true");
                        child3.Attributes.Add("data-field-name", dr["COLUMN_NAME"].ToString());
                    }
                    else
                        child3.Attributes.Add("aria-identity-column", "false");

                    if (dr.GetSchemaTable().Columns.Contains("Question") && dr["Question"].ToString() != "")
                        child2.InnerText = dr["Question"].ToString();
                    else
                        child2.InnerText = SplitCamelCase(dr["COLUMN_NAME"].ToString());

                    child3.ID = string.Format("txt{0}", dr["COLUMN_NAME"].ToString());
                    child3.Attributes.Add("class", "response-field");
                    child3.Attributes.Add("aria-data-type", dr["DATA_TYPE"].ToString().ToLower());

                    child1.Controls.Add(child2);
                    child1.Controls.Add(child3);

                    FieldGroupList fgList = fieldGroupList.Find(g => g.FieldName.ToLower() == dr["COLUMN_NAME"].ToString().ToLower());

                    if (fgList != null)
                        child1.Attributes.Add("aria-group-name", fgList.GroupName);

                    if (dr["COLUMN_NAME"].ToString().ToLower() == "importid" || dr["COLUMN_NAME"].ToString().ToLower() == "batchid" || Convert.ToBoolean(dr["is_identity"]))
                        child1.Attributes.Add("style", "display:none;");
                    else if (dr["COLUMN_NAME"].ToString().ToLower() == "companyid" && companyID == "10025" && ddlTables.SelectedValue.ToLower() == "driverinfo")
                    {
                        child3.Attributes.Add("value", "1");
                        child3.Attributes.Add("disabled", "true");
                    }
                    else if (dr["COLUMN_NAME"].ToString().ToLower() == "companyid")
                    {
                        child3.Attributes.Add("value", companyID);
                        child1.Attributes.Add("style", "display:none;");
                    }
                    else if (dr["COLUMN_NAME"].ToString().ToLower() == "companycode")
                        child3.Attributes.Add("disabled", "true");

                    if (Convert.ToBoolean(dr["is_indicator"]))
                    {
                        child1.Attributes.Add("style", "display:none;");
                        child3.Attributes.Add("aria-indicator-column", "true");
                    }

                    phEditFields.Controls.Add(child1);

                    if (flag)
                    {
                        litHeaderRow.Text += string.Format("<th>{0}</th>", SplitCamelCase(dr["COLUMN_NAME"].ToString()));

                        if (stringBuilder.Length == 0)
                            stringBuilder.AppendFormat("{0}", SplitCamelCase(dr["COLUMN_NAME"].ToString()));
                        else
                            stringBuilder.AppendFormat(",{0}", SplitCamelCase(dr["COLUMN_NAME"].ToString()));
                    }
                }
            }

            dr.Close();
            dr.Dispose();

            if (!(pChart.Attributes["grid-query"].ToLower() == "editgridquery"))
                return;

            pChart.Attributes["grid-query"] = string.Format("SELECT * FROM {0} {1}", ddlTables.SelectedValue, filterByValue);
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

            litScript.Text += stringBuilder1.ToString();
        }

        private void GetFieldGroupLists()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("select GroupName,DataFieldName from cms_FieldGroups ");
            stringBuilder.Append("inner join cms_FieldList on pkFieldGroupID = fkFieldGroupID ");
            stringBuilder.AppendFormat("where UserID in (0,{0}) and CompanyID={1} and UserControlName='NCDClaims.ascx' ", UserID, companyID);
            stringBuilder.Append("order by cms_FieldGroups.SortOrder ");

            fieldGroupList = new List<FieldGroupList>();

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();

                command.CommandType = CommandType.Text;
                command.CommandText = stringBuilder.ToString();

                SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (sqlDataReader.Read())
                    fieldGroupList.Add(new FieldGroupList()
                    {
                        FieldName = sqlDataReader["DataFieldName"].ToString(),
                        GroupName = sqlDataReader["GroupName"].ToString()
                    });

                sqlDataReader.Close();
                sqlDataReader.Dispose();
            }
        }

        private void SetTablePrimaryKeyField()
        {
            string attribute = ddlTables.Items[ddlTables.SelectedIndex].Attributes["aria-primary-key-field"];

            if (pChart.Attributes["primary-key-field"] != null)
                pChart.Attributes["primary-key-field"] = attribute;
            else
                pChart.Attributes.Add("primary-key-field", attribute);
        }

        private string SplitCamelCase(string str) => Regex.Replace(Regex.Replace(str, "(\\P{Ll})(\\P{Ll}\\p{Ll})", "$1 $2"), "(\\p{Ll})(\\P{Ll})", "$1 $2");
    }
}