// Decompiled with JetBrains decompiler
// Type: CalendarAppointment
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public class CalendarAppointment
{
  public int CalendarAppointmentID { get; set; }

  public string UserID { get; set; }

  public string DownloadID { get; set; }

  public string Subject { get; set; }

  public string Location { get; set; }

  public string StartDate { get; set; }

  public string EndDate { get; set; }

  public string Notes { get; set; }

  public string Reminder { get; set; }

  public void Add()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine("INSERT INTO cms_Appointments (UserID,DownloadID,Subject,Location,StartDateTime,EndDateTime,Notes,Reminder) ");
    stringBuilder.AppendLine("VALUES (@UserID,@DownloadID,@Subject,@Location,@StartDateTime,@EndDateTime,@Notes,@Reminder)");
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.Text;
      command.CommandText = stringBuilder.ToString();
      command.Parameters.AddWithValue("UserID", (object) this.UserID);
      command.Parameters.AddWithValue("DownloadID", (object) this.DownloadID);
      command.Parameters.AddWithValue("Subject", (object) this.Subject);
      command.Parameters.AddWithValue("Location", (object) this.Location);
      command.Parameters.AddWithValue("StartDateTime", (object) this.StartDate);
      command.Parameters.AddWithValue("EndDateTime", (object) this.EndDate);
      command.Parameters.AddWithValue("Notes", (object) this.Notes);
      command.Parameters.AddWithValue("Reminder", (object) this.Reminder);
      command.ExecuteNonQuery();
    }
  }
}
