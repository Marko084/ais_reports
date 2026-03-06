using AIS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AISReports.Site
{
    public partial class LytlesWorkbookEdit : System.Web.UI.Page
    {
        #region
        private string editID;
        private string editedBy;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            editID = Request.QueryString["id"];
            editedBy = Request.QueryString["editedby"];

            if (Page.IsPostBack)
                return;

            LoadControls();
            ClearControls();

            if (!string.IsNullOrEmpty(editID))
                LoadData(editID);
            else
                editID = "0";

            btnPrintJob.Attributes.Add("rel", editID);
            hdnEditedBy.Value = editedBy;
        }

        protected void btnSaveJob_Click(object sender, EventArgs e)
        {
            List<string> list1 = OriginDriverNames.Items.Cast<ListItem>().Where<ListItem>((Func<ListItem, bool>)(l => l.Selected)).Select<ListItem, string>((Func<ListItem, string>)(i => i.Value)).ToList<string>();
            List<string> list2 = DestinationDriverNames.Items.Cast<ListItem>().Where<ListItem>((Func<ListItem, bool>)(l => l.Selected)).Select<ListItem, string>((Func<ListItem, string>)(i => i.Value)).ToList<string>();
            List<string> list3 = OriginHelperNames.Items.Cast<ListItem>().Where<ListItem>((Func<ListItem, bool>)(l => l.Selected)).Select<ListItem, string>((Func<ListItem, string>)(i => i.Value)).ToList<string>();
            List<string> list4 = DestinationHelperNames.Items.Cast<ListItem>().Where<ListItem>((Func<ListItem, bool>)(l => l.Selected)).Select<ListItem, string>((Func<ListItem, string>)(i => i.Value)).ToList<string>();
            
            ContactedTransfereeNotes.Text = hdnContactedTransfereeNotes.Value;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Lytles_UpdateWorkbook";

                command.Parameters.AddWithValue("@ImportID", editID);
                command.Parameters.AddWithValue("@PKStartDate", PKStartDate.Text);
                command.Parameters.AddWithValue("@PKEndDate", PKEndDate.Text);
                command.Parameters.AddWithValue("@LDStartDate", LDStartDate.Text);
                command.Parameters.AddWithValue("@LDEndDate", LDEndDate.Text);
                command.Parameters.AddWithValue("@DELStartDate", DELStartDate.Text);
                command.Parameters.AddWithValue("@DELEndDate", DELEndDate.Text);
                command.Parameters.AddWithValue("@OfficeAssigned", Offices.Text);
                command.Parameters.AddWithValue("@PPWK", PPWKStatuses.Text);
                command.Parameters.AddWithValue("@RegNumber", RegNumber.Text.Trim());
                command.Parameters.AddWithValue("@Account", Accounts.Text);
                command.Parameters.AddWithValue("@Name", Name.Text.Trim());
                command.Parameters.AddWithValue("@PickupLocation", PickupLocation.Text.Trim());
                command.Parameters.AddWithValue("@DeliveryLocation", DeliveryLocation.Text.Trim());
                command.Parameters.AddWithValue("@Weight", Weight.Text);
                command.Parameters.AddWithValue("@Trailer", Trailers.Text);
                command.Parameters.AddWithValue("@Details", Details.Text);
                command.Parameters.AddWithValue("@OriginDriverNames", string.Join(",", (IEnumerable<string>)list1));
                command.Parameters.AddWithValue("@OriginHelperNames", string.Join(",", (IEnumerable<string>)list3));
                command.Parameters.AddWithValue("@MoveAgent", MoveAgents.SelectedValue);
                command.Parameters.AddWithValue("@Cancelled", Cancelled.Checked);
                command.Parameters.AddWithValue("@DestinationDriverNames", string.Join(",", (IEnumerable<string>)list2));
                command.Parameters.AddWithValue("@DestinationHelperNames", string.Join(",", (IEnumerable<string>)list4));
                command.Parameters.AddWithValue("@CSRContactedTransfereePK", CSRContactedTransfereePK.Checked);
                command.Parameters.AddWithValue("@ShipmentDelivered", ShipmentDelivered.Checked);
                command.Parameters.AddWithValue("@EmailAddress", EmailAddress.Text.Trim());
                command.Parameters.AddWithValue("@CSRContactedTransfereeLD", CSRContactedTransfereeLD.Checked);
                command.Parameters.AddWithValue("@CSRContactedTransfereeDEL", CSRContactedTransfereeDEL.Checked);
                command.Parameters.AddWithValue("@ContactedTransfereeNotes", ContactedTransfereeNotes.Text);
                command.Parameters.AddWithValue("@EditedBy", editedBy);
                command.ExecuteNonQuery();
            }

            Page.ShowToastr("Job Updated Successfully", "Update Job", "success");
        }

        protected void btnClearJob_Click(object sender, EventArgs e)
        {
            ClearControls();
            LoadControls();
        }

        protected void btnDeleteJob_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> list1 = OriginDriverNames.Items.Cast<ListItem>().Where<ListItem>((Func<ListItem, bool>)(l => l.Selected)).Select<ListItem, string>((Func<ListItem, string>)(i => i.Value)).ToList<string>();
                List<string> list2 = DestinationDriverNames.Items.Cast<ListItem>().Where<ListItem>((Func<ListItem, bool>)(l => l.Selected)).Select<ListItem, string>((Func<ListItem, string>)(i => i.Value)).ToList<string>();
                List<string> list3 = OriginHelperNames.Items.Cast<ListItem>().Where<ListItem>((Func<ListItem, bool>)(l => l.Selected)).Select<ListItem, string>((Func<ListItem, string>)(i => i.Value)).ToList<string>();
                List<string> list4 = DestinationHelperNames.Items.Cast<ListItem>().Where<ListItem>((Func<ListItem, bool>)(l => l.Selected)).Select<ListItem, string>((Func<ListItem, string>)(i => i.Value)).ToList<string>();
                
                ContactedTransfereeNotes.Text = hdnContactedTransfereeNotes.Value;
                Cancelled.Checked = true;

                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();

                    SqlCommand command = sqlConnection.CreateCommand();

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Lytles_UpdateWorkbook";

                    command.Parameters.AddWithValue("@ImportID", editID);
                    command.Parameters.AddWithValue("@PKStartDate", PKStartDate.Text);
                    command.Parameters.AddWithValue("@PKEndDate", PKEndDate.Text);
                    command.Parameters.AddWithValue("@LDStartDate", LDStartDate.Text);
                    command.Parameters.AddWithValue("@LDEndDate", LDEndDate.Text);
                    command.Parameters.AddWithValue("@DELStartDate", DELStartDate.Text);
                    command.Parameters.AddWithValue("@DELEndDate", DELEndDate.Text);
                    command.Parameters.AddWithValue("@OfficeAssigned", Offices.Text);
                    command.Parameters.AddWithValue("@PPWK", PPWKStatuses.Text);
                    command.Parameters.AddWithValue("@RegNumber", RegNumber.Text.Trim());
                    command.Parameters.AddWithValue("@Account", Accounts.Text);
                    command.Parameters.AddWithValue("@Name", Name.Text.Trim());
                    command.Parameters.AddWithValue("@PickupLocation", PickupLocation.Text.Trim());
                    command.Parameters.AddWithValue("@DeliveryLocation", DeliveryLocation.Text.Trim());
                    command.Parameters.AddWithValue("@Weight", Weight.Text);
                    command.Parameters.AddWithValue("@Trailer", Trailers.Text);
                    command.Parameters.AddWithValue("@Details", Details.Text);
                    command.Parameters.AddWithValue("@OriginDriverNames", string.Join(",", (IEnumerable<string>)list1));
                    command.Parameters.AddWithValue("@OriginHelperNames", string.Join(",", (IEnumerable<string>)list3));
                    command.Parameters.AddWithValue("@MoveAgent", MoveAgents.Text);
                    command.Parameters.AddWithValue("@Cancelled", "True");
                    command.Parameters.AddWithValue("@DestinationDriverNames", string.Join(",", (IEnumerable<string>)list2));
                    command.Parameters.AddWithValue("@DestinationHelperNames", string.Join(",", (IEnumerable<string>)list4));
                    command.Parameters.AddWithValue("@CSRContactedTransfereePK", CSRContactedTransfereePK.Checked);
                    command.Parameters.AddWithValue("@ShipmentDelivered", ShipmentDelivered.Checked);
                    command.Parameters.AddWithValue("@EmailAddress", EmailAddress.Text.Trim());
                    command.Parameters.AddWithValue("@CSRContactedTransfereeLD", CSRContactedTransfereeLD.Checked);
                    command.Parameters.AddWithValue("@CSRContactedTransfereeDEL", CSRContactedTransfereeDEL.Checked);
                    command.Parameters.AddWithValue("@ContactedTransfereeNotes", ContactedTransfereeNotes.Text);
                    command.Parameters.AddWithValue("@EditedBy", editedBy);

                    command.ExecuteNonQuery();

                    Page.ShowToastr("Job Updated Successfully", "Delete Job", "success");
                }
            }
            catch (Exception ex)
            {
                Page.ShowToastr(ex.Message, "Delete Job", "error");
            }
        }

        private void LoadControls()
        {
            Accounts.Items.Clear();
            MoveAgents.Items.Clear();
            Trailers.Items.Clear();
            Offices.Items.Clear();
            OriginDriverNames.Items.Clear();
            DestinationDriverNames.Items.Clear();
            OriginHelperNames.Items.Clear();
            DestinationHelperNames.Items.Clear();

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Lytles_GetWorkbookLists";

                SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (sqlDataReader.Read())
                {
                    string str = sqlDataReader["Name"].ToString().Trim();
                    string lower = sqlDataReader["TableName"].ToString().ToLower();

                    if (lower == "accounts")
                        Accounts.Items.Add(new ListItem()
                        {
                            Text = str,
                            Value = str
                        });
                    else if (lower == "offices")
                        Offices.Items.Add(new ListItem()
                        {
                            Text = str,
                            Value = str
                        });
                    else if (lower == "moveagents")
                        MoveAgents.Items.Add(new ListItem()
                        {
                            Text = str,
                            Value = str
                        });
                    else if (lower == "drivers")
                    {
                        OriginDriverNames.Items.Add(new ListItem()
                        {
                            Text = str,
                            Value = str
                        });
                        DestinationDriverNames.Items.Add(new ListItem()
                        {
                            Text = str,
                            Value = str
                        });
                    }
                    else if (lower == "helpers")
                    {
                        OriginHelperNames.Items.Add(new ListItem()
                        {
                            Text = str,
                            Value = str
                        });
                        DestinationHelperNames.Items.Add(new ListItem()
                        {
                            Text = str,
                            Value = str
                        });
                    }
                    else if (lower == "trailers")
                        Trailers.Items.Add(new ListItem() 
                        {
                            Text = str,
                            Value = str
                        });
                }
            }
        }

        private void ClearControls()
        {
            RegNumber.Text = "";
            Name.Text = "";
            EmailAddress.Text = "";
            PickupLocation.Text = "";
            DeliveryLocation.Text = "";
            ShipmentDelivered.Checked = false;
            Cancelled.Checked = false;
            Details.Text = "";
            Weight.Text = "";
            ContactedTransfereeNotes.Text = "";
            CSRContactedTransfereeDEL.Checked = false;
            CSRContactedTransfereeLD.Checked = false;
            CSRContactedTransfereePK.Checked = false;
            PKStartDate.Text = "";
            PKEndDate.Text = "";
            LDStartDate.Text = "";
            LDEndDate.Text = "";
            DELStartDate.Text = "";
            DELEndDate.Text = "";
        }

        private void LoadData(string editID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand command = sqlConnection.CreateCommand();

                command.CommandText = string.Format("select * from Lytles_ScheduleBook where ImportID={0}", editID);

                SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (sqlDataReader.Read())
                {
                    RegNumber.Text = sqlDataReader["RegNumber"].ToString();
                    Name.Text = sqlDataReader["Name"].ToString();
                    EmailAddress.Text = sqlDataReader["EmailAddress"].ToString();
                    PickupLocation.Text = sqlDataReader["PickupLocation"].ToString();
                    DeliveryLocation.Text = sqlDataReader["DeliveryLocation"].ToString();
                    Details.Text = sqlDataReader["Details"].ToString();
                    Weight.Text = sqlDataReader["Weight"].ToString();
                    ContactedTransfereeNotes.Text = sqlDataReader["ContactedTransfereeNotes"].ToString();
                    hdnContactedTransfereeNotes.Value = ContactedTransfereeNotes.Text;

                    TextBox pkStartDate = PKStartDate;
                    DateTime dateTime = Convert.ToDateTime(Convert.IsDBNull(sqlDataReader["PKStartDate"]) ? "1/1/1900" : sqlDataReader["PKStartDate"]);

                    string str1 = dateTime.ToString("yyyy-MM-dd");

                    pkStartDate.Text = str1;

                    TextBox pkEndDate = PKEndDate;

                    dateTime = Convert.ToDateTime(Convert.IsDBNull(sqlDataReader["PKEndDate"]) ? "1/1/1900" : sqlDataReader["PKEndDate"]);

                    string str2 = dateTime.ToString("yyyy-MM-dd");
                    pkEndDate.Text = str2;

                    TextBox ldStartDate = LDStartDate;
                    dateTime = Convert.ToDateTime(Convert.IsDBNull(sqlDataReader["LDStartDate"]) ? "1/1/1900" : sqlDataReader["LDStartDate"]);

                    string str3 = dateTime.ToString("yyyy-MM-dd");
                    ldStartDate.Text = str3;

                    TextBox ldEndDate = LDEndDate;
                    dateTime = Convert.ToDateTime(Convert.IsDBNull(sqlDataReader["LDEndDate"]) ? "1/1/1900" : sqlDataReader["LDEndDate"]);

                    string str4 = dateTime.ToString("yyyy-MM-dd");
                    ldEndDate.Text = str4;

                    TextBox delStartDate = DELStartDate;
                    dateTime = Convert.ToDateTime(Convert.IsDBNull(sqlDataReader["DELStartDate"]) ? "1/1/1900" : sqlDataReader["DELStartDate"]);

                    string str5 = dateTime.ToString("yyyy-MM-dd");
                    delStartDate.Text = str5;

                    TextBox delEndDate = DELEndDate;
                    dateTime = Convert.ToDateTime(Convert.IsDBNull(sqlDataReader["DELEndDate"]) ? "1/1/1900" : sqlDataReader["DELEndDate"]);

                    string str6 = dateTime.ToString("yyyy-MM-dd");
                    delEndDate.Text = str6;

                    CSRContactedTransfereeDEL.Checked = Convert.ToBoolean(Convert.IsDBNull(sqlDataReader["CSRContactedTransfereeDEL"]) ? false : sqlDataReader["CSRContactedTransfereeDEL"]);
                    CSRContactedTransfereeLD.Checked = Convert.ToBoolean(Convert.IsDBNull(sqlDataReader["CSRContactedTransfereeLD"]) ? false : sqlDataReader["CSRContactedTransfereeLD"]);
                    CSRContactedTransfereePK.Checked = Convert.ToBoolean(Convert.IsDBNull(sqlDataReader["CSRContactedTransfereePK"]) ? false : sqlDataReader["CSRContactedTransfereePK"]);
                    ShipmentDelivered.Checked = Convert.ToBoolean(Convert.IsDBNull(sqlDataReader["ShipmentDelivered"]) ? false : sqlDataReader["ShipmentDelivered"]);
                    Cancelled.Checked = Convert.ToBoolean(Convert.IsDBNull(sqlDataReader["Cancelled"]) ? false : sqlDataReader["Cancelled"]);

                    ListItem byValue1 = PPWKStatuses.Items.FindByValue(sqlDataReader["PPWK"].ToString());

                    if (byValue1 != null)
                        byValue1.Selected = true;

                    ListItem byValue2 = Accounts.Items.FindByValue(sqlDataReader["Account"].ToString());

                    if (byValue2 != null)
                        byValue2.Selected = true;

                    ListItem byValue3 = Offices.Items.FindByValue(sqlDataReader["OfficeAssigned"].ToString());

                    if (byValue3 != null)
                        byValue3.Selected = true;

                    ListItem byValue4 = Trailers.Items.FindByValue(sqlDataReader["Trailer"].ToString());

                    if (byValue4 != null)
                        byValue4.Selected = true;

                    ListItem byValue5 = MoveAgents.Items.FindByValue(sqlDataReader["MoveAgent"].ToString());

                    if (byValue5 != null)
                        byValue5.Selected = true;

                    string str7 = sqlDataReader["OriginDriverNames"].ToString();
                    string str8 = sqlDataReader["DestinationDriverNames"].ToString();
                    string str9 = sqlDataReader["OriginHelperNames"].ToString();
                    string str10 = sqlDataReader["DestinationHelperNames"].ToString();
                    string str11 = str7;

                    char[] chArray1 = new char[1] { ',' };

                    foreach (string str12 in str11.Split(chArray1))
                    {
                        ListItem byValue6 = OriginDriverNames.Items.FindByValue(str12.Trim());

                        if (byValue6 != null)
                            byValue6.Selected = true;
                    }

                    string str13 = str8;
                    char[] chArray2 = new char[1] { ',' };

                    foreach (string str14 in str13.Split(chArray2))
                    {
                        ListItem byValue7 = DestinationDriverNames.Items.FindByValue(str14.Trim());

                        if (byValue7 != null)
                            byValue7.Selected = true;
                    }

                    string str15 = str9;
                    char[] chArray3 = new char[1] { ',' };

                    foreach (string str16 in str15.Split(chArray3))
                    {
                        ListItem byValue8 = OriginHelperNames.Items.FindByValue(str16.Trim());

                        if (byValue8 != null)
                            byValue8.Selected = true;
                    }

                    string str17 = str10;
                    char[] chArray4 = new char[1] { ',' };

                    foreach (string str18 in str17.Split(chArray4))
                    {
                        ListItem byValue9 = DestinationHelperNames.Items.FindByValue(str18.Trim());

                        if (byValue9 != null)
                            byValue9.Selected = true;
                    }
                }

                sqlDataReader.Close();
            }
        }
    }
}