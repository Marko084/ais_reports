// Decompiled with JetBrains decompiler
// Type: _reports_aismgtDataSet
// Assembly: App_Code, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 25328527-F322-457A-95EE-BA6D9FC6EA72
// Assembly location: C:\inetpub\wwwroot\AIS\bin\App_Code.dll

using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

[DesignerCategory("code")]
[HelpKeyword("vs.data.DataSet")]
[ToolboxItem(true)]
[XmlSchemaProvider("GetTypedDataSetSchema")]
[XmlRoot("_reports_aismgtDataSet")]
[Serializable]
public class _reports_aismgtDataSet : DataSet
{
  private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
  private _reports_aismgtDataSet.cms_PrintClaimsChecksDataTable tablecms_PrintClaimsChecks;

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DebuggerNonUserCode]
  public _reports_aismgtDataSet()
  {
    this.BeginInit();
    this.InitClass();
    CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
    base.Tables.CollectionChanged += changeEventHandler;
    base.Relations.CollectionChanged += changeEventHandler;
    this.EndInit();
  }

  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  protected _reports_aismgtDataSet(SerializationInfo info, StreamingContext context)
    : base(info, context, false)
  {
    if (this.IsBinarySerialized(info, context))
    {
      this.InitVars(false);
      CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
      this.Tables.CollectionChanged += changeEventHandler;
      this.Relations.CollectionChanged += changeEventHandler;
    }
    else
    {
      string s = (string) info.GetValue("XmlSchema", typeof (string));
      if (this.DetermineSchemaSerializationMode(info, context) == SchemaSerializationMode.IncludeSchema)
      {
        DataSet dataSet = new DataSet();
        dataSet.ReadXmlSchema((XmlReader) new XmlTextReader((TextReader) new StringReader(s)));
        if (dataSet.Tables[nameof (cms_PrintClaimsChecks)] != null)
          base.Tables.Add((DataTable) new _reports_aismgtDataSet.cms_PrintClaimsChecksDataTable(dataSet.Tables[nameof (cms_PrintClaimsChecks)]));
        this.DataSetName = dataSet.DataSetName;
        this.Prefix = dataSet.Prefix;
        this.Namespace = dataSet.Namespace;
        this.Locale = dataSet.Locale;
        this.CaseSensitive = dataSet.CaseSensitive;
        this.EnforceConstraints = dataSet.EnforceConstraints;
        this.Merge(dataSet, false, MissingSchemaAction.Add);
        this.InitVars();
      }
      else
        this.ReadXmlSchema((XmlReader) new XmlTextReader((TextReader) new StringReader(s)));
      this.GetSerializationData(info, context);
      CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
      base.Tables.CollectionChanged += changeEventHandler;
      this.Relations.CollectionChanged += changeEventHandler;
    }
  }

  [DebuggerNonUserCode]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  public _reports_aismgtDataSet.cms_PrintClaimsChecksDataTable cms_PrintClaimsChecks => this.tablecms_PrintClaimsChecks;

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [Browsable(true)]
  public override SchemaSerializationMode SchemaSerializationMode
  {
    get => this._schemaSerializationMode;
    set => this._schemaSerializationMode = value;
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  public new DataTableCollection Tables => base.Tables;

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  public new DataRelationCollection Relations => base.Relations;

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DebuggerNonUserCode]
  protected override void InitializeDerivedDataSet()
  {
    this.BeginInit();
    this.InitClass();
    this.EndInit();
  }

  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  public override DataSet Clone()
  {
    _reports_aismgtDataSet reportsAismgtDataSet = (_reports_aismgtDataSet) base.Clone();
    reportsAismgtDataSet.InitVars();
    reportsAismgtDataSet.SchemaSerializationMode = this.SchemaSerializationMode;
    return (DataSet) reportsAismgtDataSet;
  }

  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  protected override bool ShouldSerializeTables() => false;

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DebuggerNonUserCode]
  protected override bool ShouldSerializeRelations() => false;

  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  protected override void ReadXmlSerializable(XmlReader reader)
  {
    if (this.DetermineSchemaSerializationMode(reader) == SchemaSerializationMode.IncludeSchema)
    {
      this.Reset();
      DataSet dataSet = new DataSet();
      int num = (int) dataSet.ReadXml(reader);
      if (dataSet.Tables["cms_PrintClaimsChecks"] != null)
        base.Tables.Add((DataTable) new _reports_aismgtDataSet.cms_PrintClaimsChecksDataTable(dataSet.Tables["cms_PrintClaimsChecks"]));
      this.DataSetName = dataSet.DataSetName;
      this.Prefix = dataSet.Prefix;
      this.Namespace = dataSet.Namespace;
      this.Locale = dataSet.Locale;
      this.CaseSensitive = dataSet.CaseSensitive;
      this.EnforceConstraints = dataSet.EnforceConstraints;
      this.Merge(dataSet, false, MissingSchemaAction.Add);
      this.InitVars();
    }
    else
    {
      int num = (int) this.ReadXml(reader);
      this.InitVars();
    }
  }

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DebuggerNonUserCode]
  protected override XmlSchema GetSchemaSerializable()
  {
    MemoryStream memoryStream = new MemoryStream();
    this.WriteXmlSchema((XmlWriter) new XmlTextWriter((Stream) memoryStream, (Encoding) null));
    memoryStream.Position = 0L;
    return XmlSchema.Read((XmlReader) new XmlTextReader((Stream) memoryStream), (ValidationEventHandler) null);
  }

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DebuggerNonUserCode]
  internal void InitVars() => this.InitVars(true);

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DebuggerNonUserCode]
  internal void InitVars(bool initTable)
  {
    this.tablecms_PrintClaimsChecks = (_reports_aismgtDataSet.cms_PrintClaimsChecksDataTable) base.Tables["cms_PrintClaimsChecks"];
    if (!initTable || this.tablecms_PrintClaimsChecks == null)
      return;
    this.tablecms_PrintClaimsChecks.InitVars();
  }

  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  private void InitClass()
  {
    this.DataSetName = "reports-aismgtDataSet";
    this.Prefix = "";
    this.Namespace = "http://tempuri.org/reports-aismgtDataSet1.xsd";
    this.EnforceConstraints = true;
    this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
    this.tablecms_PrintClaimsChecks = new _reports_aismgtDataSet.cms_PrintClaimsChecksDataTable();
    base.Tables.Add((DataTable) this.tablecms_PrintClaimsChecks);
  }

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DebuggerNonUserCode]
  private bool ShouldSerializecms_PrintClaimsChecks() => false;

  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  private void SchemaChanged(object sender, CollectionChangeEventArgs e)
  {
    if (e.Action != CollectionChangeAction.Remove)
      return;
    this.InitVars();
  }

  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
  {
    _reports_aismgtDataSet reportsAismgtDataSet = new _reports_aismgtDataSet();
    XmlSchemaComplexType typedDataSetSchema = new XmlSchemaComplexType();
    XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
    xmlSchemaSequence.Items.Add((XmlSchemaObject) new XmlSchemaAny()
    {
      Namespace = reportsAismgtDataSet.Namespace
    });
    typedDataSetSchema.Particle = (XmlSchemaParticle) xmlSchemaSequence;
    XmlSchema schemaSerializable = reportsAismgtDataSet.GetSchemaSerializable();
    if (xs.Contains(schemaSerializable.TargetNamespace))
    {
      MemoryStream memoryStream1 = new MemoryStream();
      MemoryStream memoryStream2 = new MemoryStream();
      try
      {
        schemaSerializable.Write((Stream) memoryStream1);
        foreach (XmlSchema schema in (IEnumerable) xs.Schemas(schemaSerializable.TargetNamespace))
        {
          memoryStream2.SetLength(0L);
          schema.Write((Stream) memoryStream2);
          if (memoryStream1.Length == memoryStream2.Length)
          {
            memoryStream1.Position = 0L;
            memoryStream2.Position = 0L;
            do
              ;
            while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
            if (memoryStream1.Position == memoryStream1.Length)
              return typedDataSetSchema;
          }
        }
      }
      finally
      {
        memoryStream1?.Close();
        memoryStream2?.Close();
      }
    }
    xs.Add(schemaSerializable);
    return typedDataSetSchema;
  }

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  public delegate void cms_PrintClaimsChecksRowChangeEventHandler(
    object sender,
    _reports_aismgtDataSet.cms_PrintClaimsChecksRowChangeEvent e);

  [XmlSchemaProvider("GetTypedTableSchema")]
  [Serializable]
  public class cms_PrintClaimsChecksDataTable : 
    TypedTableBase<_reports_aismgtDataSet.cms_PrintClaimsChecksRow>
  {
    private DataColumn columnpkClaimCheckHistoryID;
    private DataColumn columnCompanyID;
    private DataColumn columnUserID;
    private DataColumn columnCheckNumber;
    private DataColumn columnPaymentDate;
    private DataColumn columnClaimNumber;
    private DataColumn columnInjuredName;
    private DataColumn columnPayableTo;
    private DataColumn columnAmount;
    private DataColumn columnExpenseAccount;
    private DataColumn columnAssignedTo;
    private DataColumn columnMemo;
    private DataColumn columnAddress;
    private DataColumn columnRemittanceComments;
    private DataColumn columnRecurringPayment;
    private DataColumn columnPaymentInterval;
    private DataColumn columnAmountText;
    private DataColumn columnUpdateDate;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public cms_PrintClaimsChecksDataTable()
    {
      this.TableName = "cms_PrintClaimsChecks";
      this.BeginInit();
      this.InitClass();
      this.EndInit();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    internal cms_PrintClaimsChecksDataTable(DataTable table)
    {
      this.TableName = table.TableName;
      if (table.CaseSensitive != table.DataSet.CaseSensitive)
        this.CaseSensitive = table.CaseSensitive;
      if (table.Locale.ToString() != table.DataSet.Locale.ToString())
        this.Locale = table.Locale;
      if (table.Namespace != table.DataSet.Namespace)
        this.Namespace = table.Namespace;
      this.Prefix = table.Prefix;
      this.MinimumCapacity = table.MinimumCapacity;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected cms_PrintClaimsChecksDataTable(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.InitVars();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn pkClaimCheckHistoryIDColumn => this.columnpkClaimCheckHistoryID;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn CompanyIDColumn => this.columnCompanyID;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn UserIDColumn => this.columnUserID;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn CheckNumberColumn => this.columnCheckNumber;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn PaymentDateColumn => this.columnPaymentDate;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn ClaimNumberColumn => this.columnClaimNumber;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn InjuredNameColumn => this.columnInjuredName;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn PayableToColumn => this.columnPayableTo;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn AmountColumn => this.columnAmount;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn ExpenseAccountColumn => this.columnExpenseAccount;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn AssignedToColumn => this.columnAssignedTo;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn MemoColumn => this.columnMemo;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn AddressColumn => this.columnAddress;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn RemittanceCommentsColumn => this.columnRemittanceComments;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn RecurringPaymentColumn => this.columnRecurringPayment;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn PaymentIntervalColumn => this.columnPaymentInterval;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn AmountTextColumn => this.columnAmountText;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn UpdateDateColumn => this.columnUpdateDate;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [Browsable(false)]
    public int Count => this.Rows.Count;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public _reports_aismgtDataSet.cms_PrintClaimsChecksRow this[int index] => (_reports_aismgtDataSet.cms_PrintClaimsChecksRow) this.Rows[index];

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public event _reports_aismgtDataSet.cms_PrintClaimsChecksRowChangeEventHandler cms_PrintClaimsChecksRowChanging;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public event _reports_aismgtDataSet.cms_PrintClaimsChecksRowChangeEventHandler cms_PrintClaimsChecksRowChanged;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public event _reports_aismgtDataSet.cms_PrintClaimsChecksRowChangeEventHandler cms_PrintClaimsChecksRowDeleting;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public event _reports_aismgtDataSet.cms_PrintClaimsChecksRowChangeEventHandler cms_PrintClaimsChecksRowDeleted;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void Addcms_PrintClaimsChecksRow(
      _reports_aismgtDataSet.cms_PrintClaimsChecksRow row)
    {
      this.Rows.Add((DataRow) row);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public _reports_aismgtDataSet.cms_PrintClaimsChecksRow Addcms_PrintClaimsChecksRow(
      int CompanyID,
      int UserID,
      int CheckNumber,
      DateTime PaymentDate,
      string ClaimNumber,
      string InjuredName,
      string PayableTo,
      Decimal Amount,
      string ExpenseAccount,
      string AssignedTo,
      string Memo,
      string Address,
      string RemittanceComments,
      bool RecurringPayment,
      int PaymentInterval,
      string AmountText,
      DateTime UpdateDate)
    {
      _reports_aismgtDataSet.cms_PrintClaimsChecksRow row = (_reports_aismgtDataSet.cms_PrintClaimsChecksRow) this.NewRow();
      object[] objArray = new object[18]
      {
        null,
        (object) CompanyID,
        (object) UserID,
        (object) CheckNumber,
        (object) PaymentDate,
        (object) ClaimNumber,
        (object) InjuredName,
        (object) PayableTo,
        (object) Amount,
        (object) ExpenseAccount,
        (object) AssignedTo,
        (object) Memo,
        (object) Address,
        (object) RemittanceComments,
        (object) RecurringPayment,
        (object) PaymentInterval,
        (object) AmountText,
        (object) UpdateDate
      };
      row.ItemArray = objArray;
      this.Rows.Add((DataRow) row);
      return row;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public _reports_aismgtDataSet.cms_PrintClaimsChecksRow FindBypkClaimCheckHistoryID(
      int pkClaimCheckHistoryID)
    {
      return (_reports_aismgtDataSet.cms_PrintClaimsChecksRow) this.Rows.Find(new object[1]
      {
        (object) pkClaimCheckHistoryID
      });
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public override DataTable Clone()
    {
      _reports_aismgtDataSet.cms_PrintClaimsChecksDataTable claimsChecksDataTable = (_reports_aismgtDataSet.cms_PrintClaimsChecksDataTable) base.Clone();
      claimsChecksDataTable.InitVars();
      return (DataTable) claimsChecksDataTable;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override DataTable CreateInstance() => (DataTable) new _reports_aismgtDataSet.cms_PrintClaimsChecksDataTable();

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    internal void InitVars()
    {
      this.columnpkClaimCheckHistoryID = this.Columns["pkClaimCheckHistoryID"];
      this.columnCompanyID = this.Columns["CompanyID"];
      this.columnUserID = this.Columns["UserID"];
      this.columnCheckNumber = this.Columns["CheckNumber"];
      this.columnPaymentDate = this.Columns["PaymentDate"];
      this.columnClaimNumber = this.Columns["ClaimNumber"];
      this.columnInjuredName = this.Columns["InjuredName"];
      this.columnPayableTo = this.Columns["PayableTo"];
      this.columnAmount = this.Columns["Amount"];
      this.columnExpenseAccount = this.Columns["ExpenseAccount"];
      this.columnAssignedTo = this.Columns["AssignedTo"];
      this.columnMemo = this.Columns["Memo"];
      this.columnAddress = this.Columns["Address"];
      this.columnRemittanceComments = this.Columns["RemittanceComments"];
      this.columnRecurringPayment = this.Columns["RecurringPayment"];
      this.columnPaymentInterval = this.Columns["PaymentInterval"];
      this.columnAmountText = this.Columns["AmountText"];
      this.columnUpdateDate = this.Columns["UpdateDate"];
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private void InitClass()
    {
      this.columnpkClaimCheckHistoryID = new DataColumn("pkClaimCheckHistoryID", typeof (int), (string) null, MappingType.Element);
      this.Columns.Add(this.columnpkClaimCheckHistoryID);
      this.columnCompanyID = new DataColumn("CompanyID", typeof (int), (string) null, MappingType.Element);
      this.Columns.Add(this.columnCompanyID);
      this.columnUserID = new DataColumn("UserID", typeof (int), (string) null, MappingType.Element);
      this.Columns.Add(this.columnUserID);
      this.columnCheckNumber = new DataColumn("CheckNumber", typeof (int), (string) null, MappingType.Element);
      this.Columns.Add(this.columnCheckNumber);
      this.columnPaymentDate = new DataColumn("PaymentDate", typeof (DateTime), (string) null, MappingType.Element);
      this.Columns.Add(this.columnPaymentDate);
      this.columnClaimNumber = new DataColumn("ClaimNumber", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnClaimNumber);
      this.columnInjuredName = new DataColumn("InjuredName", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnInjuredName);
      this.columnPayableTo = new DataColumn("PayableTo", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnPayableTo);
      this.columnAmount = new DataColumn("Amount", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnAmount);
      this.columnExpenseAccount = new DataColumn("ExpenseAccount", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnExpenseAccount);
      this.columnAssignedTo = new DataColumn("AssignedTo", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnAssignedTo);
      this.columnMemo = new DataColumn("Memo", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnMemo);
      this.columnAddress = new DataColumn("Address", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnAddress);
      this.columnRemittanceComments = new DataColumn("RemittanceComments", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnRemittanceComments);
      this.columnRecurringPayment = new DataColumn("RecurringPayment", typeof (bool), (string) null, MappingType.Element);
      this.Columns.Add(this.columnRecurringPayment);
      this.columnPaymentInterval = new DataColumn("PaymentInterval", typeof (int), (string) null, MappingType.Element);
      this.Columns.Add(this.columnPaymentInterval);
      this.columnAmountText = new DataColumn("AmountText", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnAmountText);
      this.columnUpdateDate = new DataColumn("UpdateDate", typeof (DateTime), (string) null, MappingType.Element);
      this.Columns.Add(this.columnUpdateDate);
      this.Constraints.Add((Constraint) new UniqueConstraint("Constraint1", new DataColumn[1]
      {
        this.columnpkClaimCheckHistoryID
      }, true));
      this.columnpkClaimCheckHistoryID.AutoIncrement = true;
      this.columnpkClaimCheckHistoryID.AllowDBNull = false;
      this.columnpkClaimCheckHistoryID.ReadOnly = true;
      this.columnpkClaimCheckHistoryID.Unique = true;
      this.columnCheckNumber.ReadOnly = true;
      this.columnClaimNumber.MaxLength = 99;
      this.columnInjuredName.MaxLength = 99;
      this.columnPayableTo.MaxLength = 99;
      this.columnExpenseAccount.MaxLength = 30;
      this.columnAssignedTo.MaxLength = 99;
      this.columnMemo.MaxLength = 100;
      this.columnAddress.MaxLength = (int) byte.MaxValue;
      this.columnRemittanceComments.MaxLength = 500;
      this.columnAmountText.MaxLength = (int) byte.MaxValue;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public _reports_aismgtDataSet.cms_PrintClaimsChecksRow Newcms_PrintClaimsChecksRow() => (_reports_aismgtDataSet.cms_PrintClaimsChecksRow) this.NewRow();

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => (DataRow) new _reports_aismgtDataSet.cms_PrintClaimsChecksRow(builder);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override Type GetRowType() => typeof (_reports_aismgtDataSet.cms_PrintClaimsChecksRow);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override void OnRowChanged(DataRowChangeEventArgs e)
    {
      base.OnRowChanged(e);
      if (this.cms_PrintClaimsChecksRowChanged == null)
        return;
      this.cms_PrintClaimsChecksRowChanged((object) this, new _reports_aismgtDataSet.cms_PrintClaimsChecksRowChangeEvent((_reports_aismgtDataSet.cms_PrintClaimsChecksRow) e.Row, e.Action));
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override void OnRowChanging(DataRowChangeEventArgs e)
    {
      base.OnRowChanging(e);
      if (this.cms_PrintClaimsChecksRowChanging == null)
        return;
      this.cms_PrintClaimsChecksRowChanging((object) this, new _reports_aismgtDataSet.cms_PrintClaimsChecksRowChangeEvent((_reports_aismgtDataSet.cms_PrintClaimsChecksRow) e.Row, e.Action));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override void OnRowDeleted(DataRowChangeEventArgs e)
    {
      base.OnRowDeleted(e);
      if (this.cms_PrintClaimsChecksRowDeleted == null)
        return;
      this.cms_PrintClaimsChecksRowDeleted((object) this, new _reports_aismgtDataSet.cms_PrintClaimsChecksRowChangeEvent((_reports_aismgtDataSet.cms_PrintClaimsChecksRow) e.Row, e.Action));
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override void OnRowDeleting(DataRowChangeEventArgs e)
    {
      base.OnRowDeleting(e);
      if (this.cms_PrintClaimsChecksRowDeleting == null)
        return;
      this.cms_PrintClaimsChecksRowDeleting((object) this, new _reports_aismgtDataSet.cms_PrintClaimsChecksRowChangeEvent((_reports_aismgtDataSet.cms_PrintClaimsChecksRow) e.Row, e.Action));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void Removecms_PrintClaimsChecksRow(
      _reports_aismgtDataSet.cms_PrintClaimsChecksRow row)
    {
      this.Rows.Remove((DataRow) row);
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
    {
      XmlSchemaComplexType typedTableSchema = new XmlSchemaComplexType();
      XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
      _reports_aismgtDataSet reportsAismgtDataSet = new _reports_aismgtDataSet();
      XmlSchemaAny xmlSchemaAny1 = new XmlSchemaAny();
      xmlSchemaAny1.Namespace = "http://www.w3.org/2001/XMLSchema";
      xmlSchemaAny1.MinOccurs = 0M;
      xmlSchemaAny1.MaxOccurs = Decimal.MaxValue;
      xmlSchemaAny1.ProcessContents = XmlSchemaContentProcessing.Lax;
      xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny1);
      XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
      xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
      xmlSchemaAny2.MinOccurs = 1M;
      xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
      xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny2);
      typedTableSchema.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
      {
        Name = "namespace",
        FixedValue = reportsAismgtDataSet.Namespace
      });
      typedTableSchema.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
      {
        Name = "tableTypeName",
        FixedValue = nameof (cms_PrintClaimsChecksDataTable)
      });
      typedTableSchema.Particle = (XmlSchemaParticle) xmlSchemaSequence;
      XmlSchema schemaSerializable = reportsAismgtDataSet.GetSchemaSerializable();
      if (xs.Contains(schemaSerializable.TargetNamespace))
      {
        MemoryStream memoryStream1 = new MemoryStream();
        MemoryStream memoryStream2 = new MemoryStream();
        try
        {
          schemaSerializable.Write((Stream) memoryStream1);
          foreach (XmlSchema schema in (IEnumerable) xs.Schemas(schemaSerializable.TargetNamespace))
          {
            memoryStream2.SetLength(0L);
            schema.Write((Stream) memoryStream2);
            if (memoryStream1.Length == memoryStream2.Length)
            {
              memoryStream1.Position = 0L;
              memoryStream2.Position = 0L;
              do
                ;
              while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
              if (memoryStream1.Position == memoryStream1.Length)
                return typedTableSchema;
            }
          }
        }
        finally
        {
          memoryStream1?.Close();
          memoryStream2?.Close();
        }
      }
      xs.Add(schemaSerializable);
      return typedTableSchema;
    }
  }

  public class cms_PrintClaimsChecksRow : DataRow
  {
    private _reports_aismgtDataSet.cms_PrintClaimsChecksDataTable tablecms_PrintClaimsChecks;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    internal cms_PrintClaimsChecksRow(DataRowBuilder rb)
      : base(rb)
    {
      this.tablecms_PrintClaimsChecks = (_reports_aismgtDataSet.cms_PrintClaimsChecksDataTable) this.Table;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public int pkClaimCheckHistoryID
    {
      get => (int) this[this.tablecms_PrintClaimsChecks.pkClaimCheckHistoryIDColumn];
      set => this[this.tablecms_PrintClaimsChecks.pkClaimCheckHistoryIDColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public int CompanyID
    {
      get
      {
        try
        {
          return (int) this[this.tablecms_PrintClaimsChecks.CompanyIDColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'CompanyID' in table 'cms_PrintClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_PrintClaimsChecks.CompanyIDColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public int UserID
    {
      get
      {
        try
        {
          return (int) this[this.tablecms_PrintClaimsChecks.UserIDColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'UserID' in table 'cms_PrintClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_PrintClaimsChecks.UserIDColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public int CheckNumber
    {
      get
      {
        try
        {
          return (int) this[this.tablecms_PrintClaimsChecks.CheckNumberColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'CheckNumber' in table 'cms_PrintClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_PrintClaimsChecks.CheckNumberColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DateTime PaymentDate
    {
      get
      {
        try
        {
          return (DateTime) this[this.tablecms_PrintClaimsChecks.PaymentDateColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'PaymentDate' in table 'cms_PrintClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_PrintClaimsChecks.PaymentDateColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string ClaimNumber
    {
      get
      {
        try
        {
          return (string) this[this.tablecms_PrintClaimsChecks.ClaimNumberColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ClaimNumber' in table 'cms_PrintClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_PrintClaimsChecks.ClaimNumberColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string InjuredName
    {
      get
      {
        try
        {
          return (string) this[this.tablecms_PrintClaimsChecks.InjuredNameColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'InjuredName' in table 'cms_PrintClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_PrintClaimsChecks.InjuredNameColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string PayableTo
    {
      get
      {
        try
        {
          return (string) this[this.tablecms_PrintClaimsChecks.PayableToColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'PayableTo' in table 'cms_PrintClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_PrintClaimsChecks.PayableToColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public Decimal Amount
    {
      get
      {
        try
        {
          return (Decimal) this[this.tablecms_PrintClaimsChecks.AmountColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'Amount' in table 'cms_PrintClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_PrintClaimsChecks.AmountColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string ExpenseAccount
    {
      get
      {
        try
        {
          return (string) this[this.tablecms_PrintClaimsChecks.ExpenseAccountColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ExpenseAccount' in table 'cms_PrintClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_PrintClaimsChecks.ExpenseAccountColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string AssignedTo
    {
      get
      {
        try
        {
          return (string) this[this.tablecms_PrintClaimsChecks.AssignedToColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'AssignedTo' in table 'cms_PrintClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_PrintClaimsChecks.AssignedToColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string Memo
    {
      get
      {
        try
        {
          return (string) this[this.tablecms_PrintClaimsChecks.MemoColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'Memo' in table 'cms_PrintClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_PrintClaimsChecks.MemoColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string Address
    {
      get
      {
        try
        {
          return (string) this[this.tablecms_PrintClaimsChecks.AddressColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'Address' in table 'cms_PrintClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_PrintClaimsChecks.AddressColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string RemittanceComments
    {
      get
      {
        try
        {
          return (string) this[this.tablecms_PrintClaimsChecks.RemittanceCommentsColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'RemittanceComments' in table 'cms_PrintClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_PrintClaimsChecks.RemittanceCommentsColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool RecurringPayment
    {
      get
      {
        try
        {
          return (bool) this[this.tablecms_PrintClaimsChecks.RecurringPaymentColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'RecurringPayment' in table 'cms_PrintClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_PrintClaimsChecks.RecurringPaymentColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public int PaymentInterval
    {
      get
      {
        try
        {
          return (int) this[this.tablecms_PrintClaimsChecks.PaymentIntervalColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'PaymentInterval' in table 'cms_PrintClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_PrintClaimsChecks.PaymentIntervalColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string AmountText
    {
      get
      {
        try
        {
          return (string) this[this.tablecms_PrintClaimsChecks.AmountTextColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'AmountText' in table 'cms_PrintClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_PrintClaimsChecks.AmountTextColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DateTime UpdateDate
    {
      get
      {
        try
        {
          return (DateTime) this[this.tablecms_PrintClaimsChecks.UpdateDateColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'UpdateDate' in table 'cms_PrintClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_PrintClaimsChecks.UpdateDateColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsCompanyIDNull() => this.IsNull(this.tablecms_PrintClaimsChecks.CompanyIDColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetCompanyIDNull() => this[this.tablecms_PrintClaimsChecks.CompanyIDColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsUserIDNull() => this.IsNull(this.tablecms_PrintClaimsChecks.UserIDColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetUserIDNull() => this[this.tablecms_PrintClaimsChecks.UserIDColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsCheckNumberNull() => this.IsNull(this.tablecms_PrintClaimsChecks.CheckNumberColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetCheckNumberNull() => this[this.tablecms_PrintClaimsChecks.CheckNumberColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsPaymentDateNull() => this.IsNull(this.tablecms_PrintClaimsChecks.PaymentDateColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetPaymentDateNull() => this[this.tablecms_PrintClaimsChecks.PaymentDateColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsClaimNumberNull() => this.IsNull(this.tablecms_PrintClaimsChecks.ClaimNumberColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetClaimNumberNull() => this[this.tablecms_PrintClaimsChecks.ClaimNumberColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsInjuredNameNull() => this.IsNull(this.tablecms_PrintClaimsChecks.InjuredNameColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetInjuredNameNull() => this[this.tablecms_PrintClaimsChecks.InjuredNameColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsPayableToNull() => this.IsNull(this.tablecms_PrintClaimsChecks.PayableToColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetPayableToNull() => this[this.tablecms_PrintClaimsChecks.PayableToColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsAmountNull() => this.IsNull(this.tablecms_PrintClaimsChecks.AmountColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetAmountNull() => this[this.tablecms_PrintClaimsChecks.AmountColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsExpenseAccountNull() => this.IsNull(this.tablecms_PrintClaimsChecks.ExpenseAccountColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetExpenseAccountNull() => this[this.tablecms_PrintClaimsChecks.ExpenseAccountColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsAssignedToNull() => this.IsNull(this.tablecms_PrintClaimsChecks.AssignedToColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetAssignedToNull() => this[this.tablecms_PrintClaimsChecks.AssignedToColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsMemoNull() => this.IsNull(this.tablecms_PrintClaimsChecks.MemoColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetMemoNull() => this[this.tablecms_PrintClaimsChecks.MemoColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsAddressNull() => this.IsNull(this.tablecms_PrintClaimsChecks.AddressColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetAddressNull() => this[this.tablecms_PrintClaimsChecks.AddressColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsRemittanceCommentsNull() => this.IsNull(this.tablecms_PrintClaimsChecks.RemittanceCommentsColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetRemittanceCommentsNull() => this[this.tablecms_PrintClaimsChecks.RemittanceCommentsColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsRecurringPaymentNull() => this.IsNull(this.tablecms_PrintClaimsChecks.RecurringPaymentColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetRecurringPaymentNull() => this[this.tablecms_PrintClaimsChecks.RecurringPaymentColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsPaymentIntervalNull() => this.IsNull(this.tablecms_PrintClaimsChecks.PaymentIntervalColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetPaymentIntervalNull() => this[this.tablecms_PrintClaimsChecks.PaymentIntervalColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsAmountTextNull() => this.IsNull(this.tablecms_PrintClaimsChecks.AmountTextColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetAmountTextNull() => this[this.tablecms_PrintClaimsChecks.AmountTextColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsUpdateDateNull() => this.IsNull(this.tablecms_PrintClaimsChecks.UpdateDateColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetUpdateDateNull() => this[this.tablecms_PrintClaimsChecks.UpdateDateColumn] = Convert.DBNull;
  }

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  public class cms_PrintClaimsChecksRowChangeEvent : EventArgs
  {
    private _reports_aismgtDataSet.cms_PrintClaimsChecksRow eventRow;
    private DataRowAction eventAction;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public cms_PrintClaimsChecksRowChangeEvent(
      _reports_aismgtDataSet.cms_PrintClaimsChecksRow row,
      DataRowAction action)
    {
      this.eventRow = row;
      this.eventAction = action;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public _reports_aismgtDataSet.cms_PrintClaimsChecksRow Row => this.eventRow;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataRowAction Action => this.eventAction;
  }
}
