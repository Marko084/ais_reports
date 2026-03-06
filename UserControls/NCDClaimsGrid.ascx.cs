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
    public partial class NCDClaimsGrid : NCDUserControlBase
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
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            companyID = Request.QueryString["cid"];
            selectedTableName = Request.QueryString["t"];

            pChart.Attributes.Add("grid-query", QueryName);
            pChart.Attributes.Add("grid-query-type", QueryType);
            pChart.Attributes.Add("grid-display-fields", DisplayFields);
            pChart.Attributes.Add("grid-type", "claims");
            pChart.Attributes.Add("adjustor-approval-module", "false");
            pChart.Attributes.Add("gm-approval-module", "false");
            pChart.Attributes.Add("dynamic-query", "false");

            if (PrimaryKeyField != null && PrimaryKeyField != "")
                pChart.Attributes.Add("primary-key-field", PrimaryKeyField);
            else
                PrimaryKeyField = "";

            GetFieldGroupLists();
            LoadChartSettings();

            if (useAdjustorClaimsApprovalModuleTF || useGMClaimsApprovalModuleTF)
                LoadClaimsApprovalFields();

            if (!Page.IsPostBack)
            {
                LoadTables();
                LoadFieldMaps();
            }

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
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "multipicklistmap")
                        pChart.Attributes.Add("multipicklistmap", sqlDataReader["SettingValue"].ToString());
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
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "useadjustorclaimsapprovalmodule")
                    {
                        pChart.Attributes["adjustor-approval-module"] = sqlDataReader["SettingValue"].ToString().ToLower();
                        useAdjustorClaimsApprovalModuleTF = true;
                    }
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "usegmclaimsapprovalmodule")
                    {
                        pChart.Attributes["gm-approval-module"] = sqlDataReader["SettingValue"].ToString().ToLower();
                        useGMClaimsApprovalModuleTF = true;
                    }
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "claimscheckrequestkeyfield")
                        pChart.Attributes.Add("checkrequest-key-field", sqlDataReader["SettingValue"].ToString());
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "detaillink")
                        pChart.Attributes.Add("detail-link", sqlDataReader["SettingValue"].ToString());
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "surveyprimarykeyfield")
                        pChart.Attributes.Add("survey-primary-key-field", sqlDataReader["SettingValue"].ToString());
                    else if (sqlDataReader["SettingName"].ToString().ToLower() == "gridreadonly")
                        pChart.Attributes["grid-read-only"] = sqlDataReader["SettingValue"].ToString().ToLower();
                    else if (sqlDataReader["SettingName"].ToString().Trim().ToLower() == "documentsiteurl")
                        pChart.Attributes.Add("document-site-url", sqlDataReader["SettingValue"].ToString().ToLower());
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

            stringBuilder.AppendFormat("select * from cms_FieldMaps where UserControlName='NCDClaims.ascx' and CompanyID={0} ", companyID);

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

            if (pChart.Attributes["data-store-type"] != null)
                str = pChart.Attributes["data-store-type"];

            stringBuilder.AppendFormat("SELECT * FROM [DatabaseEditObjects] WHERE DataStoreType like '{0}%' AND ", str);

            if (CompanyID == "10000")
                stringBuilder.AppendFormat("CompanyID in(select CompanyID from Company where CompanyID={0}) ", companyID);
            else
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
        }

        private void LoadClaimsApprovalFields()
        {
            StringBuilder stringBuilder1 = new StringBuilder();

            HtmlGenericControl child1 = (HtmlGenericControl)null;
            HtmlGenericControl child2 = (HtmlGenericControl)null;

            bool flag = false;
            string str1 = DAL.ReturnScalarValue("select CompanyCode from Company where CompanyID=" + companyID, true).ToString();

            if (str1 == "ARS")
                str1 = "ARS_Consolidated";

            SqlDataReader dr = DAL.ReturnDataReader("admin_GetTableEditFields", "CompanyID,TableName,KeyFieldName,UseAdjustorClaimsApprovalModule,UseGMClaimsApprovalModule,UserControlName,ShowDetailTF", string.Format("{0},{1},{2},{3},{4},NCDClaims.ascx,{5}", companyID, (str1 + "_Claims_Approval"), PrimaryKeyField, (useAdjustorClaimsApprovalModuleTF ? 1 : 0), useGMClaimsApprovalModuleTF, showSurveyDetailLinkTF));
           
            while (dr.Read())
            {
                string str2 = "";
                string str3 = "";

                HtmlSelect child3 = (HtmlSelect)null;

                if (str2 != "")
                {
                    str3 = string.Format("responseListName{0}", str2.Replace(",", ""));

                    if (!responses.Contains(string.Format("{0}|{1}", str3, str2)))
                        responses.Add(string.Format("{0}|{1}", str3, str2));
                }

                HtmlGenericControl child4 = new HtmlGenericControl("div");

                child4.Attributes.Add("class", "edit-field");

                HtmlGenericControl child5 = new HtmlGenericControl("span");

                if (dr["COLUMN_NAME"].ToString().ToLower() == "company" || dr["COLUMN_NAME"].ToString().ToLower().StartsWith("authorizedby") || dr["COLUMN_NAME"].ToString().ToLower() == "chargetoaccount" || dr["COLUMN_NAME"].ToString().ToLower() == "drivername")
                {
                    SqlDataReader sqlDataReader1 = (SqlDataReader)null;

                    if (dr["COLUMN_NAME"].ToString().ToLower() == "company")
                        sqlDataReader1 = (SqlDataReader)DAL.ReturnDataReader("GetPickList", "CompanyID,UserID,FieldName,UsePickList", companyID + "," + UserID + ",ClaimsGMLocation,1");
                    else if (dr["COLUMN_NAME"].ToString().ToLower().StartsWith("authorizedby"))
                        sqlDataReader1 = (SqlDataReader)DAL.ReturnDataReader("GetPickList", "CompanyID,UserID,FieldName,UsePickList", companyID + "," + UserID + ",GMName,1");
                    
                    if (dr["COLUMN_NAME"].ToString().ToLower() == "authorizedby")
                        child5.InnerText = "Check Request Authorized By";
                    else if (dr["COLUMN_NAME"].ToString().ToLower() == "authorizedby2")
                        child5.InnerText = "Check Request Authorized By 2";
                    else
                        child5.InnerText = SplitCamelCase(dr["COLUMN_NAME"].ToString());

                    child4.Controls.Add(child5);

                    if (dr["COLUMN_NAME"].ToString().ToLower() == "chargetoaccount")
                    {
                        string str4 = "GLCode|GLCodeType|ClaimsGMLocation";

                        HtmlGenericControl child6 = new HtmlGenericControl("div");

                        child6.Attributes.Add("style", "display:inline-block;");

                        StringBuilder stringBuilder2 = new StringBuilder();

                        stringBuilder2.AppendLine("<script type=\"text/javascript\">" + Environment.NewLine);
                        stringBuilder2.AppendLine("function getPickList(p) {");
                        stringBuilder2.AppendLine("var pickList; ");

                        string str5 = str4;
                        char[] chArray = new char[1] { '|' };

                        foreach (string str6 in str5.Split(chArray))
                        {
                            SqlDataReader sqlDataReader2 = (SqlDataReader)DAL.ReturnDataReader("GetPickList", "CompanyID,UserID,FieldName,UsePickList", companyID + "," + UserID + "," + str6 + ",1");
                            HtmlGenericControl child7 = new HtmlGenericControl("input");

                            child7.Attributes.Add("type", "text");

                            if (str6.Trim().ToLower() == "claimsgmlocation")
                                child7.Attributes.Add("style", "width:163px !important;");
                            else if (str6.Trim().ToLower() == "glcode")
                                child7.Attributes.Add("style", "width:55px !important;");
                            else
                                child7.Attributes.Add("style", "width:120px !important;");

                            child7.Attributes.Add("class", string.Format("aria-charge-to-fields aria-pick-list {0}", str6.ToLower()));
                            child7.Attributes.Add("aria-pick-list-name", str6.Trim().ToLower() + "Tags");
                            child7.Attributes.Add("id", "ddl" + dr["COLUMN_NAME"].ToString());

                            child3 = new HtmlSelect();

                            stringBuilder2.AppendLine(string.Format("if (p=='{0}') ", (str6.Trim().ToLower() + "Tags")));
                            stringBuilder2.AppendLine("{ ");
                            stringBuilder2.Append("return [");

                            string str7 = "";

                            while (sqlDataReader2.Read())
                                str7 = !(str7 == "") ? str7 + ",\"" + sqlDataReader2["Value"].ToString() + "\"" : "\"" + sqlDataReader2["Value"].ToString() + "\"";
                            
                            stringBuilder2.AppendLine(string.Format("{0}]; ", str7));
                            stringBuilder2.AppendLine("};");

                            sqlDataReader2.Close();

                            if (str6.Trim().ToLower() == "claimsgmlocation")
                            {
                                HtmlGenericControl child8 = new HtmlGenericControl("input");

                                child8.Attributes.Add("type", "text");
                                child8.Attributes.Add("id", "ddlChargeToAccount");
                                child8.Attributes.Add("style", "width:60px !important;");
                                child8.Attributes.Add("class", "aria-charge-to-fields gmclaimamount");
                                child8.Attributes.Add("aria-data-type", "currency");
                                child6.Controls.Add(child8);
                            }

                            child6.Controls.Add(child7);
                        }

                        stringBuilder2.AppendLine("return pickList; ");
                        stringBuilder2.AppendLine("} ");
                        stringBuilder2.AppendLine("</script>");

                        litScript.Text += stringBuilder2.ToString();

                        HtmlGenericControl child9 = new HtmlGenericControl("span");

                        child9.Attributes.Add("class", "aria-charge-to-fields gm-approval-indicator");
                        child9.Attributes.Add("aria-approval-indicator", "true");
                        child9.Attributes.Add("style", "width:20px;height:20px;color:transparent;");
                        child9.Attributes.Add("id", "ddlChargeToAccount");
                        child9.InnerText = "N";

                        child6.Controls.Add(child9);

                        HtmlGenericControl child10 = new HtmlGenericControl("a");

                        child10.InnerText = "Add";
                        child10.Attributes.Add("id", "lnkChargeToAdd");
                        child10.Attributes.Add("class", "add-charge-to-button");
                        child10.Attributes.Add("href", "#");
                        child10.Attributes.Add("style", "padding-left:7px;");
                        child10.Attributes.Add("onclick", "javascript:AddChargeToAccountRow(this);");

                        child6.Controls.Add(child10);

                        child4.Controls.Add(child6);
                    }
                    else if (dr["COLUMN_NAME"].ToString().ToLower() == "drivername")
                    {
                        child3 = new HtmlSelect();

                        HtmlGenericControl child11 = new HtmlGenericControl("div");

                        child11.Attributes.Add("style", "display:inline-block;");

                        HtmlGenericControl child12 = new HtmlGenericControl("input");

                        child12.Attributes.Add("type", "text");
                        child12.Attributes.Add("data-field-name", dr["COLUMN_NAME"].ToString());
                        child12.Attributes.Add("data-table-type", "CustomDrivers");
                        child12.Attributes.Add("aria-identity-column", "false");
                        child12.Attributes.Add("aria-computed-column", "false");
                        child12.Attributes.Add("id", string.Format("txt{0}", dr["COLUMN_NAME"].ToString()));
                        child12.Attributes.Add("class", "lookup-combobox aria-driver-fields");
                        child12.Attributes.Add("aria-data-type", dr["DATA_TYPE"].ToString().ToLower());

                        child11.Controls.Add(child12);

                        HtmlGenericControl child13 = new HtmlGenericControl("input");

                        child13.Attributes.Add("type", "text");
                        child13.Attributes.Add("data-field-name", dr["COLUMN_NAME"].ToString());
                        child13.Attributes.Add("data-table-type", ddlTables.SelectedValue);
                        child13.Attributes.Add("aria-identity-column", "false");
                        child13.Attributes.Add("aria-computed-column", "false");
                        child13.Attributes.Add("id", string.Format("txt{0}", dr["COLUMN_NAME"].ToString()));
                        child13.Attributes.Add("class", "aria-driver-fields aria-pick-list");
                        child13.Attributes.Add("aria-pick-list-name", "claimsgmlocationTags");
                        child13.Attributes.Add("aria-data-type", dr["DATA_TYPE"].ToString().ToLower());

                        child11.Controls.Add(child13);

                        HtmlGenericControl child14 = new HtmlGenericControl("a");

                        child14.InnerText = "Add";
                        child14.Attributes.Add("id", "lnkAddDriver");
                        child14.Attributes.Add("class", "add-driver");
                        child14.Attributes.Add("href", "#");
                        child14.Attributes.Add("style", "padding-left:7px;");
                        child14.Attributes.Add("onclick", "javascript:AddDriverRow(this);");

                        child11.Controls.Add(child14);

                        child4.Controls.Add(child11);
                    }
                    else
                    {
                        child3 = new HtmlSelect();

                        child3.Items.Add("");
                        child3.Attributes.Add("id", "ddl" + dr["COLUMN_NAME"].ToString());
                        child3.Attributes.Add("class", "ddl-combobox");

                        while (sqlDataReader1.Read())
                            child3.Items.Add(sqlDataReader1["Value"].ToString());

                        sqlDataReader1.Close();

                        child4.Controls.Add(child3);
                    }

                    phClaimsApproval.Controls.Add(child4);
                }

                if (child3 == null)
                {
                    HtmlGenericControl child15;

                    if (dr["CHARACTER_MAXIMUM_LENGTH"].ToString() != "" && (Convert.ToInt32(dr["CHARACTER_MAXIMUM_LENGTH"].ToString()) > 100 || Convert.ToInt32(dr["CHARACTER_MAXIMUM_LENGTH"].ToString()) == -1))
                    {
                        child15 = new HtmlGenericControl("textarea");

                        child15.Attributes.Add("rows", "10");
                        child15.Attributes.Add("cols", "50");
                    }
                    else if (dr["DATA_TYPE"].ToString().ToLower() == "varchar")
                    {
                        child15 = new HtmlGenericControl("input");

                        child15.Attributes.Add("type", "text");
                        child15.Attributes.Add("data-field-name", dr["COLUMN_NAME"].ToString());
                        child15.Attributes.Add("data-table-type", ddlTables.SelectedValue);
                    }
                    else
                    {
                        child15 = new HtmlGenericControl("input");

                        child15.Attributes.Add("type", "text");
                        child15.Attributes.Add("data-field-name", dr["COLUMN_NAME"].ToString());
                    }

                    if (pickListFieldMap != null && child15 != null && pickListFieldMap.Count(p => p.FieldName.ToLower() == dr["COLUMN_NAME"].ToString().ToLower()) > 0)
                    {
                        ClaimsPickListFieldMap cplfMap = pickListFieldMap.Find(p => p.FieldName.ToLower() == dr["COLUMN_NAME"].ToString().ToLower());
                        child15.Attributes.Add("picklist-field-name", cplfMap.MapToFieldName);
                        child15.Attributes.Add("picklist-table-type", cplfMap.TableName);
                    }

                    if (Convert.ToBoolean(dr["is_identity"]))
                        child15.Attributes.Add("aria-identity-column", "true");
                    else
                        child15.Attributes.Add("aria-identity-column", "false");

                    if (Convert.ToBoolean(dr["is_computed"]))
                        child15.Attributes.Add("aria-computed-column", "true");
                    else
                        child15.Attributes.Add("aria-computed-column", "false");

                    child5.InnerText = SplitCamelCase(dr["COLUMN_NAME"].ToString());

                    child15.ID = string.Format("txt{0}", dr["COLUMN_NAME"].ToString());
                    child15.Attributes.Add("class", "response-field");
                    child15.Attributes.Add("aria-data-type", dr["DATA_TYPE"].ToString().ToLower());

                    if (str3 != "")
                    {
                        child15.Attributes.Add("response-list-type", str3);
                        child5.Attributes.Add("class", "question-label");
                    }

                    child4.Controls.Add(child5);
                    child4.Controls.Add(child15);

                    FieldGroupList plfMap = fieldGroupList.Find(g => g.FieldName.ToLower() == dr["COLUMN_NAME"].ToString().ToLower());

                    if (plfMap != null)
                    {
                        child4.Attributes.Add("aria-group-name", plfMap.GroupName);

                        if (!plfMap.GroupDivAddedTF)
                        {
                            child1 = new HtmlGenericControl("div");

                            child1.Attributes.Add("class", "accordion");
                            child1.Attributes.Add("aria-group-section", plfMap.GroupName);

                            child2 = new HtmlGenericControl("div");

                            HtmlGenericControl child16 = new HtmlGenericControl("h3");

                            child16.InnerHtml = string.Format("<span class='accordion-text'>{0}</span><img class='data-indicator' src=''/>", plfMap.GroupName);

                            child1.Controls.Add(child16);

                            phEditFields.Controls.Add(child1);

                            foreach (FieldGroupList fieldGroup in fieldGroupList)
                            {
                                if (fieldGroup.GroupName == plfMap.GroupName)
                                    fieldGroup.GroupDivAddedTF = true;
                            }
                        }

                        child2.Controls.Add(child4);

                        child1.Controls.Add(child2);
                    }

                    if (dr["COLUMN_NAME"].ToString().ToLower() == "importid" || dr["COLUMN_NAME"].ToString().ToLower() == "companyid" || dr["COLUMN_NAME"].ToString().ToLower() == "batchid" || dr["COLUMN_NAME"].ToString().ToLower() == "approvedbygm")
                        child4.Attributes.Add("style", "display:none;");

                    if (Convert.ToBoolean(dr["is_indicator"]))
                    {
                        child4.Attributes.Add("style", "display:none;");
                        child15.Attributes.Add("aria-indicator-column", "true");
                    }

                    if (fieldGroupList == null)
                        phClaimsApproval.Controls.Add(child4);

                    if (flag)
                    {
                        litHeaderRow.Text += string.Format("<th>{0}</th>", SplitCamelCase(dr["COLUMN_NAME"].ToString()));

                        if (stringBuilder1.Length == 0)
                            stringBuilder1.AppendFormat("{0}", SplitCamelCase(dr["COLUMN_NAME"].ToString()));
                        else
                            stringBuilder1.AppendFormat(",{0}", SplitCamelCase(dr["COLUMN_NAME"].ToString()));
                    }
                }
            }

            if (pChart.Attributes["grid-query"].ToLower() == "editgridquery")
                pChart.Attributes["grid-query"] = string.Format("SELECT * FROM {0} ", ddlTables.SelectedValue);

            dr.Close();
            dr.Dispose();
        }

        private void LoadEditFields2()
        {
            StringBuilder stringBuilder = new StringBuilder();

            HtmlGenericControl child1 = (HtmlGenericControl)null;
            HtmlGenericControl child2 = (HtmlGenericControl)null;

            bool flag = litHeaderRow.Text.Trim().Length == 0;

            SqlDataReader dr = (SqlDataReader)DAL.ReturnDataReader("admin_GetTableEditFields", "CompanyID,TableName,KeyFieldName,UseAdjustorClaimsApprovalModule,UseGMClaimsApprovalModule,UserControlName,ShowDetailTF", string.Format("{0},{1},{2},{3},{4},NCDClaims.ascx,{5}", companyID, ddlTables.SelectedValue, PrimaryKeyField, (useAdjustorClaimsApprovalModuleTF ? 1 : 0), useGMClaimsApprovalModuleTF, showSurveyDetailLinkTF));
            
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

                HtmlGenericControl child3 = new HtmlGenericControl("div");

                child3.Attributes.Add("class", "edit-field");

                HtmlGenericControl child4 = new HtmlGenericControl("span");
                HtmlGenericControl child5;

                if (dr["CHARACTER_MAXIMUM_LENGTH"].ToString() != "" && (Convert.ToInt32(dr["CHARACTER_MAXIMUM_LENGTH"].ToString()) > 100 || Convert.ToInt32(dr["CHARACTER_MAXIMUM_LENGTH"].ToString()) == -1))
                {
                    child5 = new HtmlGenericControl("textarea");

                    child5.Attributes.Add("rows", "10");
                    child5.Attributes.Add("cols", "50");
                }
                else if (dr["DATA_TYPE"].ToString().ToLower() == "varchar")
                {
                    child5 = new HtmlGenericControl("input");

                    child5.Attributes.Add("type", "text");
                    child5.Attributes.Add("data-field-name", dr["COLUMN_NAME"].ToString());
                    child5.Attributes.Add("data-table-type", ddlTables.SelectedValue);
                }
                else
                {
                    child5 = new HtmlGenericControl("input");

                    child5.Attributes.Add("type", "text");
                    child5.Attributes.Add("data-field-name", dr["COLUMN_NAME"].ToString());
                }
                if (pickListFieldMap != null && pickListFieldMap.Count(p => p.FieldName.ToLower() == dr["COLUMN_NAME"].ToString().ToLower()) > 0)
                {
                    ClaimsPickListFieldMap plfMap = pickListFieldMap.Find(p => p.FieldName.ToLower() == dr["COLUMN_NAME"].ToString().ToLower());

                    child5.Attributes.Add("picklist-field-name", plfMap.MapToFieldName);
                    child5.Attributes.Add("picklist-table-type", plfMap.TableName);
                }

                if (Convert.ToBoolean(dr["is_identity"]))
                    child5.Attributes.Add("aria-identity-column", "true");
                else
                    child5.Attributes.Add("aria-identity-column", "false");

                if (Convert.ToBoolean(dr["is_computed"]))
                    child5.Attributes.Add("aria-computed-column", "true");
                else
                    child5.Attributes.Add("aria-computed-column", "false");

                child4.InnerText = SplitCamelCase(dr["COLUMN_NAME"].ToString());

                child5.ID = string.Format("txt{0}", dr["COLUMN_NAME"].ToString());
                child5.Attributes.Add("class", "response-field");

                child5.Attributes.Add("aria-data-type", dr["DATA_TYPE"].ToString().ToLower());

                if (str2 != "")
                {
                    child5.Attributes.Add("response-list-type", str2);
                    child4.Attributes.Add("class", "question-label");
                }

                child3.Controls.Add(child4);
                child3.Controls.Add(child5);

                FieldGroupList fgList = fieldGroupList.Find(g => g.FieldName.ToLower() == dr["COLUMN_NAME"].ToString().ToLower());

                if (fgList != null)
                {
                    child3.Attributes.Add("aria-group-name", fgList.GroupName);

                    if (!fgList.GroupDivAddedTF)
                    {
                        child1 = new HtmlGenericControl("div");
                        child1.Attributes.Add("class", "accordion");
                        child1.Attributes.Add("aria-group-section", fgList.GroupName);

                        child2 = new HtmlGenericControl("div");

                        HtmlGenericControl child6 = new HtmlGenericControl("h3");

                        child6.InnerHtml = string.Format("<span class='accordion-text'>{0}</span><img class='data-indicator' src=''/>", fgList.GroupName);
                        
                        child1.Controls.Add(child6);

                        phEditFields.Controls.Add(child1);

                        foreach (FieldGroupList fieldGroup in fieldGroupList)
                        {
                            if (fieldGroup.GroupName == fieldGroup.GroupName)
                                fieldGroup.GroupDivAddedTF = true;
                        }
                    }

                    child2.Controls.Add(child3);

                    child1.Controls.Add(child2);
                }

                if (dr["COLUMN_NAME"].ToString().ToLower() == "importid" || dr["COLUMN_NAME"].ToString().ToLower() == "companyid" || dr["COLUMN_NAME"].ToString().ToLower() == "batchid")
                    child3.Attributes.Add("style", "display:none;");

                if (Convert.ToBoolean(dr["is_indicator"]))
                {
                    child3.Attributes.Add("style", "display:none;");
                    child5.Attributes.Add("aria-indicator-column", "true");
                }

                if (fieldGroupList == null)
                    phEditFields.Controls.Add(child3);

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

            stringBuilder.Append("select GroupName,DataFieldName from cms_FieldGroups fg ");
            stringBuilder.Append("inner join cms_FieldList fl on pkFieldGroupID = fkFieldGroupID ");
            stringBuilder.AppendFormat("where UserID in (0,{0}) and CompanyID={1} and UserControlName='NCDClaims.ascx' ", UserID, companyID);
            stringBuilder.Append("order by fg.SortOrder,fl.SortOrder ");

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