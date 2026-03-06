// Decompiled with JetBrains decompiler
// Type: NCDClaimCheck
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public class NCDClaimCheck
{
  public int pkClaimCheckID { get; set; }

  public DateTime PaymentDate { get; set; }

  public string ClaimNumber { get; set; }

  public string InjuredName { get; set; }

  public string PayableTo { get; set; }

  public double Amount { get; set; }

  public string AmountText { get; set; }

  public string ExpenseAccount { get; set; }

  public int CheckNumber { get; set; }

  public string Memo { get; set; }

  public bool RecurringPayment { get; set; }

  public string PaymentInterval { get; set; }

  public bool IssueCheck { get; set; }

  public bool HoldCheck { get; set; }

  public int UserID { get; set; }

  public string RemittanceComments { get; set; }

  public string QueryType { get; set; }

  public void GetPaymentHistory()
  {
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = "cms_GetClaimsCheck";
      command.Parameters.AddWithValue("pkClaimCheckID", (object) this.pkClaimCheckID);
      SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
      while (sqlDataReader.Read())
      {
        this.PaymentDate = Convert.ToDateTime(sqlDataReader["PaymentDate"]);
        this.Amount = Convert.ToDouble(sqlDataReader["Amount"]);
        this.CheckNumber = Convert.ToInt32(sqlDataReader["CheckNumber"].ToString());
        this.ClaimNumber = sqlDataReader["ClaimNumber"].ToString();
        this.ExpenseAccount = sqlDataReader["ExpenseAccount"].ToString();
        this.HoldCheck = Convert.ToBoolean(sqlDataReader["HoldCheck"]);
        this.InjuredName = sqlDataReader["InjuredName"].ToString();
        this.IssueCheck = Convert.ToBoolean(sqlDataReader["IssueCheck"]);
        this.Memo = sqlDataReader["Memo"].ToString();
        this.PaymentInterval = sqlDataReader["PaymentInterval"].ToString();
        this.PayableTo = sqlDataReader["PayableTo"].ToString();
        this.RecurringPayment = Convert.ToBoolean(sqlDataReader["RecurringPayment"]);
        this.AmountText = this.ConvertNumberToCurrencyText(Convert.ToDecimal(this.Amount));
        this.RemittanceComments = sqlDataReader["RemittanceComments"].ToString();
      }
    }
  }

  public void Get()
  {
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = "cms_GetClaimsCheck";
      command.Parameters.AddWithValue("pkClaimCheckID", (object) this.pkClaimCheckID);
      SqlDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
      while (sqlDataReader.Read())
      {
        this.PaymentDate = Convert.ToDateTime(sqlDataReader["PaymentDate"]);
        this.Amount = Convert.ToDouble(sqlDataReader["Amount"]);
        this.CheckNumber = 0;
        this.ClaimNumber = sqlDataReader["ClaimNumber"].ToString();
        this.ExpenseAccount = sqlDataReader["ExpenseAccount"].ToString();
        this.HoldCheck = Convert.ToBoolean(sqlDataReader["HoldCheck"]);
        this.InjuredName = sqlDataReader["InjuredName"].ToString();
        this.IssueCheck = Convert.ToBoolean(sqlDataReader["IssueCheck"]);
        this.Memo = sqlDataReader["Memo"].ToString();
        this.PayableTo = sqlDataReader["PayableTo"].ToString();
        this.RecurringPayment = Convert.ToBoolean(sqlDataReader["RecurringPayment"]);
        this.PaymentInterval = sqlDataReader["PaymentInterval"].ToString();
        this.AmountText = this.ConvertNumberToCurrencyText(Convert.ToDecimal(this.Amount));
        this.RemittanceComments = sqlDataReader["RemittanceComments"].ToString();
      }
    }
  }

  public void Add()
  {
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = "cms_InsertClaimsCheck";
      command.Parameters.AddWithValue("PaymentDate", (object) this.PaymentDate);
      command.Parameters.AddWithValue("ClaimNumber", (object) this.ClaimNumber);
      command.Parameters.AddWithValue("InjuredName", (object) this.InjuredName);
      command.Parameters.AddWithValue("PayableTo", (object) this.PayableTo);
      command.Parameters.AddWithValue("Amount", (object) this.Amount);
      command.Parameters.AddWithValue("ExpenseAccount", (object) this.ExpenseAccount);
      command.Parameters.AddWithValue("Memo", (object) this.Memo);
      command.Parameters.AddWithValue("RecurringPayment", (object) this.RecurringPayment);
      command.Parameters.AddWithValue("PaymentInterval", (object) this.PaymentInterval);
      command.Parameters.AddWithValue("IssueCheck", (object) this.IssueCheck);
      command.Parameters.AddWithValue("HoldCheck", (object) this.HoldCheck);
      command.Parameters.AddWithValue("UserID", (object) this.UserID);
      command.Parameters.AddWithValue("AmountText", (object) this.ConvertNumberToCurrencyText(Convert.ToDecimal(this.Amount)));
      command.Parameters.AddWithValue("RemittanceComments", (object) this.RemittanceComments);
      command.ExecuteNonQuery();
    }
  }

  public void Update()
  {
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = "cms_UpdateClaimsCheck";
      command.Parameters.AddWithValue("pkClaimCheckID", (object) this.pkClaimCheckID);
      command.Parameters.AddWithValue("PaymentDate", (object) this.PaymentDate);
      command.Parameters.AddWithValue("ClaimNumber", (object) this.ClaimNumber);
      command.Parameters.AddWithValue("InjuredName", (object) this.InjuredName);
      command.Parameters.AddWithValue("PayableTo", (object) this.PayableTo);
      command.Parameters.AddWithValue("Amount", (object) this.Amount);
      command.Parameters.AddWithValue("ExpenseAccount", (object) this.ExpenseAccount);
      command.Parameters.AddWithValue("Memo", (object) this.Memo);
      command.Parameters.AddWithValue("RecurringPayment", (object) (this.RecurringPayment ? 1 : 0));
      command.Parameters.AddWithValue("PaymentInterval", (object) this.PaymentInterval);
      command.Parameters.AddWithValue("IssueCheck", (object) (this.IssueCheck ? 1 : 0));
      command.Parameters.AddWithValue("HoldCheck", (object) (this.HoldCheck ? 1 : 0));
      command.Parameters.AddWithValue("UserID", (object) this.UserID);
      command.Parameters.AddWithValue("AmountText", (object) this.ConvertNumberToCurrencyText(Convert.ToDecimal(this.Amount)));
      command.Parameters.AddWithValue("RemittanceComments", (object) this.RemittanceComments);
      command.ExecuteNonQuery();
    }
  }

  public void Delete()
  {
    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["reports_aismgtConnectionString"].ConnectionString))
    {
      sqlConnection.Open();
      SqlCommand command = sqlConnection.CreateCommand();
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = "cms_DeleteClaimsCheck";
      command.Parameters.AddWithValue("pkClaimCheckID", (object) this.pkClaimCheckID);
      command.ExecuteNonQuery();
    }
  }

  public string ConvertNumberToCurrencyText(Decimal number)
  {
    number = Decimal.Round(number, 2, MidpointRounding.AwayFromZero);
    string empty = string.Empty;
    string[] strArray = number.ToString().Split('.');
    long number1 = long.Parse(strArray[0]);
    string text1 = this.NumberToText(number1);
    string str = (number1 == 0L ? "No" : text1) + (number1 == 1L ? " Dollar and " : " Dollars and ");
    string currencyText;
    if (strArray.Length > 1)
    {
      long number2 = long.Parse(strArray[1].Length == 1 ? strArray[1] + "0" : strArray[1]);
      string text2 = this.NumberToText(number2);
      currencyText = str + (number2 == 0L ? " No" : text2) + (number2 == 1L ? " Cent" : " Cents");
    }
    else
      currencyText = str + "No Cents";
    return currencyText;
  }

  private string NumberToText(long number)
  {
    StringBuilder stringBuilder = new StringBuilder();
    string[] strArray1 = new string[3]
    {
      "Thousand ",
      "Million ",
      "Billion "
    };
    string[] strArray2 = new string[8]
    {
      "Twenty",
      "Thirty",
      "Forty",
      "Fifty",
      "Sixty",
      "Seventy",
      "Eighty",
      "Ninety"
    };
    string[] strArray3 = new string[19]
    {
      "One",
      "Two",
      "Three",
      "Four",
      "Five",
      "Six",
      "Seven",
      "Eight",
      "Nine",
      "Ten",
      "Eleven",
      "Twelve",
      "Thirteen",
      "Fourteen",
      "Fifteen",
      "Sixteen",
      "Seventeen",
      "Eighteen",
      "Nineteen"
    };
    if (number == 0L)
      return "Zero";
    if (number < 0L)
    {
      stringBuilder.Append("Negative ");
      number = -number;
    }
    long[] numArray = new long[4];
    int num1 = 0;
    for (; number > 0L; number /= 1000L)
      numArray[num1++] = number % 1000L;
    for (int index = 3; index >= 0; --index)
    {
      long num2 = numArray[index];
      if (num2 >= 100L)
      {
        stringBuilder.Append(strArray3[num2 / 100L - 1L] + " Hundred ");
        num2 %= 100L;
        if (num2 == 0L && index > 0)
          stringBuilder.Append(strArray1[index - 1]);
      }
      if (num2 >= 20L)
      {
        if ((ulong) num2 % 10UL > 0UL)
          stringBuilder.Append(strArray2[num2 / 10L - 2L] + " " + strArray3[num2 % 10L - 1L] + " ");
        else
          stringBuilder.Append(strArray2[num2 / 10L - 2L] + " ");
      }
      else if (num2 > 0L)
        stringBuilder.Append(strArray3[num2 - 1L] + " ");
      if (num2 != 0L && index > 0)
        stringBuilder.Append(strArray1[index - 1]);
    }
    return stringBuilder.ToString().Trim();
  }
}
