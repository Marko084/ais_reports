// Decompiled with JetBrains decompiler
// Type: ChecksDataSet
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

[HelpKeyword("vs.data.DataSet")]
[ToolboxItem(true)]
[XmlRoot("ChecksDataSet")]
[DesignerCategory("code")]
[XmlSchemaProvider("GetTypedDataSetSchema")]
[Serializable]
public class ChecksDataSet : DataSet
{
  private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
  private ChecksDataSet.cms_GetClaimsChecksDataTable tablecms_GetClaimsChecks;

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DebuggerNonUserCode]
  public ChecksDataSet()
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
  protected ChecksDataSet(SerializationInfo info, StreamingContext context)
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
        if (dataSet.Tables[nameof (cms_GetClaimsChecks)] != null)
          base.Tables.Add((DataTable) new ChecksDataSet.cms_GetClaimsChecksDataTable(dataSet.Tables[nameof (cms_GetClaimsChecks)]));
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

  [Browsable(false)]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DebuggerNonUserCode]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public ChecksDataSet.cms_GetClaimsChecksDataTable cms_GetClaimsChecks => this.tablecms_GetClaimsChecks;

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DebuggerNonUserCode]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DebuggerNonUserCode]
  public override DataSet Clone()
  {
    ChecksDataSet checksDataSet = (ChecksDataSet) base.Clone();
    checksDataSet.InitVars();
    checksDataSet.SchemaSerializationMode = this.SchemaSerializationMode;
    return (DataSet) checksDataSet;
  }

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DebuggerNonUserCode]
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
      if (dataSet.Tables["cms_GetClaimsChecks"] != null)
        base.Tables.Add((DataTable) new ChecksDataSet.cms_GetClaimsChecksDataTable(dataSet.Tables["cms_GetClaimsChecks"]));
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

  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  internal void InitVars() => this.InitVars(true);

  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  internal void InitVars(bool initTable)
  {
    this.tablecms_GetClaimsChecks = (ChecksDataSet.cms_GetClaimsChecksDataTable) base.Tables["cms_GetClaimsChecks"];
    if (!initTable || this.tablecms_GetClaimsChecks == null)
      return;
    this.tablecms_GetClaimsChecks.InitVars();
  }

  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  private void InitClass()
  {
    this.DataSetName = nameof (ChecksDataSet);
    this.Prefix = "";
    this.Namespace = "http://tempuri.org/ChecksDataSet.xsd";
    this.EnforceConstraints = true;
    this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
    this.tablecms_GetClaimsChecks = new ChecksDataSet.cms_GetClaimsChecksDataTable();
    base.Tables.Add((DataTable) this.tablecms_GetClaimsChecks);
  }

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DebuggerNonUserCode]
  private bool ShouldSerializecms_GetClaimsChecks() => false;

  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  private void SchemaChanged(object sender, CollectionChangeEventArgs e)
  {
    if (e.Action != CollectionChangeAction.Remove)
      return;
    this.InitVars();
  }

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DebuggerNonUserCode]
  public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
  {
    ChecksDataSet checksDataSet = new ChecksDataSet();
    XmlSchemaComplexType typedDataSetSchema = new XmlSchemaComplexType();
    XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
    xmlSchemaSequence.Items.Add((XmlSchemaObject) new XmlSchemaAny()
    {
      Namespace = checksDataSet.Namespace
    });
    typedDataSetSchema.Particle = (XmlSchemaParticle) xmlSchemaSequence;
    XmlSchema schemaSerializable = checksDataSet.GetSchemaSerializable();
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
  public delegate void cms_GetClaimsChecksRowChangeEventHandler(
    object sender,
    ChecksDataSet.cms_GetClaimsChecksRowChangeEvent e);

  [XmlSchemaProvider("GetTypedTableSchema")]
  [Serializable]
  public class cms_GetClaimsChecksDataTable : TypedTableBase<ChecksDataSet.cms_GetClaimsChecksRow>
  {
    private DataColumn columnpkClaimCheckID;
    private DataColumn columnUserID;
    private DataColumn columnPaymentDate;
    private DataColumn columnClaimNumber;
    private DataColumn columnInjuredName;
    private DataColumn columnPayableTo;
    private DataColumn columnAmount;
    private DataColumn columnExpenseAccount;
    private DataColumn columnAssignedTo;
    private DataColumn columnMemo;
    private DataColumn columnRecurringPayment;
    private DataColumn columnIssueCheck;
    private DataColumn columnHoldCheck;
    private DataColumn columnAmountText;
    private DataColumn columnUpdateDateTime;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public cms_GetClaimsChecksDataTable()
    {
      this.TableName = "cms_GetClaimsChecks";
      this.BeginInit();
      this.InitClass();
      this.EndInit();
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    internal cms_GetClaimsChecksDataTable(DataTable table)
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
    protected cms_GetClaimsChecksDataTable(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.InitVars();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn pkClaimCheckIDColumn => this.columnpkClaimCheckID;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn UserIDColumn => this.columnUserID;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn PaymentDateColumn => this.columnPaymentDate;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn ClaimNumberColumn => this.columnClaimNumber;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn InjuredNameColumn => this.columnInjuredName;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
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

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn MemoColumn => this.columnMemo;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn RecurringPaymentColumn => this.columnRecurringPayment;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn IssueCheckColumn => this.columnIssueCheck;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn HoldCheckColumn => this.columnHoldCheck;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn AmountTextColumn => this.columnAmountText;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn UpdateDateTimeColumn => this.columnUpdateDateTime;

    [Browsable(false)]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public int Count => this.Rows.Count;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public ChecksDataSet.cms_GetClaimsChecksRow this[int index] => (ChecksDataSet.cms_GetClaimsChecksRow) this.Rows[index];

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public event ChecksDataSet.cms_GetClaimsChecksRowChangeEventHandler cms_GetClaimsChecksRowChanging;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public event ChecksDataSet.cms_GetClaimsChecksRowChangeEventHandler cms_GetClaimsChecksRowChanged;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public event ChecksDataSet.cms_GetClaimsChecksRowChangeEventHandler cms_GetClaimsChecksRowDeleting;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public event ChecksDataSet.cms_GetClaimsChecksRowChangeEventHandler cms_GetClaimsChecksRowDeleted;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void Addcms_GetClaimsChecksRow(ChecksDataSet.cms_GetClaimsChecksRow row) => this.Rows.Add((DataRow) row);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public ChecksDataSet.cms_GetClaimsChecksRow Addcms_GetClaimsChecksRow(
      int UserID,
      DateTime PaymentDate,
      string ClaimNumber,
      string InjuredName,
      string PayableTo,
      Decimal Amount,
      string ExpenseAccount,
      string AssignedTo,
      string Memo,
      bool RecurringPayment,
      bool IssueCheck,
      bool HoldCheck,
      string AmountText,
      DateTime UpdateDateTime)
    {
      ChecksDataSet.cms_GetClaimsChecksRow row = (ChecksDataSet.cms_GetClaimsChecksRow) this.NewRow();
      object[] objArray = new object[15]
      {
        null,
        (object) UserID,
        (object) PaymentDate,
        (object) ClaimNumber,
        (object) InjuredName,
        (object) PayableTo,
        (object) Amount,
        (object) ExpenseAccount,
        (object) AssignedTo,
        (object) Memo,
        (object) RecurringPayment,
        (object) IssueCheck,
        (object) HoldCheck,
        (object) AmountText,
        (object) UpdateDateTime
      };
      row.ItemArray = objArray;
      this.Rows.Add((DataRow) row);
      return row;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public ChecksDataSet.cms_GetClaimsChecksRow FindBypkClaimCheckID(int pkClaimCheckID) => (ChecksDataSet.cms_GetClaimsChecksRow) this.Rows.Find(new object[1]
    {
      (object) pkClaimCheckID
    });

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public override DataTable Clone()
    {
      ChecksDataSet.cms_GetClaimsChecksDataTable claimsChecksDataTable = (ChecksDataSet.cms_GetClaimsChecksDataTable) base.Clone();
      claimsChecksDataTable.InitVars();
      return (DataTable) claimsChecksDataTable;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override DataTable CreateInstance() => (DataTable) new ChecksDataSet.cms_GetClaimsChecksDataTable();

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    internal void InitVars()
    {
      this.columnpkClaimCheckID = this.Columns["pkClaimCheckID"];
      this.columnUserID = this.Columns["UserID"];
      this.columnPaymentDate = this.Columns["PaymentDate"];
      this.columnClaimNumber = this.Columns["ClaimNumber"];
      this.columnInjuredName = this.Columns["InjuredName"];
      this.columnPayableTo = this.Columns["PayableTo"];
      this.columnAmount = this.Columns["Amount"];
      this.columnExpenseAccount = this.Columns["ExpenseAccount"];
      this.columnAssignedTo = this.Columns["AssignedTo"];
      this.columnMemo = this.Columns["Memo"];
      this.columnRecurringPayment = this.Columns["RecurringPayment"];
      this.columnIssueCheck = this.Columns["IssueCheck"];
      this.columnHoldCheck = this.Columns["HoldCheck"];
      this.columnAmountText = this.Columns["AmountText"];
      this.columnUpdateDateTime = this.Columns["UpdateDateTime"];
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    private void InitClass()
    {
      this.columnpkClaimCheckID = new DataColumn("pkClaimCheckID", typeof (int), (string) null, MappingType.Element);
      this.Columns.Add(this.columnpkClaimCheckID);
      this.columnUserID = new DataColumn("UserID", typeof (int), (string) null, MappingType.Element);
      this.Columns.Add(this.columnUserID);
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
      this.columnRecurringPayment = new DataColumn("RecurringPayment", typeof (bool), (string) null, MappingType.Element);
      this.Columns.Add(this.columnRecurringPayment);
      this.columnIssueCheck = new DataColumn("IssueCheck", typeof (bool), (string) null, MappingType.Element);
      this.Columns.Add(this.columnIssueCheck);
      this.columnHoldCheck = new DataColumn("HoldCheck", typeof (bool), (string) null, MappingType.Element);
      this.Columns.Add(this.columnHoldCheck);
      this.columnAmountText = new DataColumn("AmountText", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnAmountText);
      this.columnUpdateDateTime = new DataColumn("UpdateDateTime", typeof (DateTime), (string) null, MappingType.Element);
      this.Columns.Add(this.columnUpdateDateTime);
      this.Constraints.Add((Constraint) new UniqueConstraint("Constraint1", new DataColumn[1]
      {
        this.columnpkClaimCheckID
      }, true));
      this.columnpkClaimCheckID.AutoIncrement = true;
      this.columnpkClaimCheckID.AllowDBNull = false;
      this.columnpkClaimCheckID.ReadOnly = true;
      this.columnpkClaimCheckID.Unique = true;
      this.columnClaimNumber.MaxLength = 99;
      this.columnInjuredName.MaxLength = 99;
      this.columnPayableTo.MaxLength = 99;
      this.columnExpenseAccount.MaxLength = 30;
      this.columnAssignedTo.MaxLength = 99;
      this.columnMemo.MaxLength = 100;
      this.columnAmountText.MaxLength = (int) byte.MaxValue;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public ChecksDataSet.cms_GetClaimsChecksRow Newcms_GetClaimsChecksRow() => (ChecksDataSet.cms_GetClaimsChecksRow) this.NewRow();

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => (DataRow) new ChecksDataSet.cms_GetClaimsChecksRow(builder);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override Type GetRowType() => typeof (ChecksDataSet.cms_GetClaimsChecksRow);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override void OnRowChanged(DataRowChangeEventArgs e)
    {
      base.OnRowChanged(e);
      if (this.cms_GetClaimsChecksRowChanged == null)
        return;
      this.cms_GetClaimsChecksRowChanged((object) this, new ChecksDataSet.cms_GetClaimsChecksRowChangeEvent((ChecksDataSet.cms_GetClaimsChecksRow) e.Row, e.Action));
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override void OnRowChanging(DataRowChangeEventArgs e)
    {
      base.OnRowChanging(e);
      if (this.cms_GetClaimsChecksRowChanging == null)
        return;
      this.cms_GetClaimsChecksRowChanging((object) this, new ChecksDataSet.cms_GetClaimsChecksRowChangeEvent((ChecksDataSet.cms_GetClaimsChecksRow) e.Row, e.Action));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override void OnRowDeleted(DataRowChangeEventArgs e)
    {
      base.OnRowDeleted(e);
      if (this.cms_GetClaimsChecksRowDeleted == null)
        return;
      this.cms_GetClaimsChecksRowDeleted((object) this, new ChecksDataSet.cms_GetClaimsChecksRowChangeEvent((ChecksDataSet.cms_GetClaimsChecksRow) e.Row, e.Action));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override void OnRowDeleting(DataRowChangeEventArgs e)
    {
      base.OnRowDeleting(e);
      if (this.cms_GetClaimsChecksRowDeleting == null)
        return;
      this.cms_GetClaimsChecksRowDeleting((object) this, new ChecksDataSet.cms_GetClaimsChecksRowChangeEvent((ChecksDataSet.cms_GetClaimsChecksRow) e.Row, e.Action));
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void Removecms_GetClaimsChecksRow(ChecksDataSet.cms_GetClaimsChecksRow row) => this.Rows.Remove((DataRow) row);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
    {
      XmlSchemaComplexType typedTableSchema = new XmlSchemaComplexType();
      XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
      ChecksDataSet checksDataSet = new ChecksDataSet();
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
        FixedValue = checksDataSet.Namespace
      });
      typedTableSchema.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
      {
        Name = "tableTypeName",
        FixedValue = nameof (cms_GetClaimsChecksDataTable)
      });
      typedTableSchema.Particle = (XmlSchemaParticle) xmlSchemaSequence;
      XmlSchema schemaSerializable = checksDataSet.GetSchemaSerializable();
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

  public class cms_GetClaimsChecksRow : DataRow
  {
    private ChecksDataSet.cms_GetClaimsChecksDataTable tablecms_GetClaimsChecks;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    internal cms_GetClaimsChecksRow(DataRowBuilder rb)
      : base(rb)
    {
      this.tablecms_GetClaimsChecks = (ChecksDataSet.cms_GetClaimsChecksDataTable) this.Table;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public int pkClaimCheckID
    {
      get => (int) this[this.tablecms_GetClaimsChecks.pkClaimCheckIDColumn];
      set => this[this.tablecms_GetClaimsChecks.pkClaimCheckIDColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public int UserID
    {
      get
      {
        try
        {
          return (int) this[this.tablecms_GetClaimsChecks.UserIDColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'UserID' in table 'cms_GetClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_GetClaimsChecks.UserIDColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DateTime PaymentDate
    {
      get
      {
        try
        {
          return (DateTime) this[this.tablecms_GetClaimsChecks.PaymentDateColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'PaymentDate' in table 'cms_GetClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_GetClaimsChecks.PaymentDateColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string ClaimNumber
    {
      get
      {
        try
        {
          return (string) this[this.tablecms_GetClaimsChecks.ClaimNumberColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ClaimNumber' in table 'cms_GetClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_GetClaimsChecks.ClaimNumberColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string InjuredName
    {
      get
      {
        try
        {
          return (string) this[this.tablecms_GetClaimsChecks.InjuredNameColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'InjuredName' in table 'cms_GetClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_GetClaimsChecks.InjuredNameColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string PayableTo
    {
      get
      {
        try
        {
          return (string) this[this.tablecms_GetClaimsChecks.PayableToColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'PayableTo' in table 'cms_GetClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_GetClaimsChecks.PayableToColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public Decimal Amount
    {
      get
      {
        try
        {
          return (Decimal) this[this.tablecms_GetClaimsChecks.AmountColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'Amount' in table 'cms_GetClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_GetClaimsChecks.AmountColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string ExpenseAccount
    {
      get
      {
        try
        {
          return (string) this[this.tablecms_GetClaimsChecks.ExpenseAccountColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ExpenseAccount' in table 'cms_GetClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_GetClaimsChecks.ExpenseAccountColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string AssignedTo
    {
      get
      {
        try
        {
          return (string) this[this.tablecms_GetClaimsChecks.AssignedToColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'AssignedTo' in table 'cms_GetClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_GetClaimsChecks.AssignedToColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string Memo
    {
      get
      {
        try
        {
          return (string) this[this.tablecms_GetClaimsChecks.MemoColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'Memo' in table 'cms_GetClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_GetClaimsChecks.MemoColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool RecurringPayment
    {
      get
      {
        try
        {
          return (bool) this[this.tablecms_GetClaimsChecks.RecurringPaymentColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'RecurringPayment' in table 'cms_GetClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_GetClaimsChecks.RecurringPaymentColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IssueCheck
    {
      get
      {
        try
        {
          return (bool) this[this.tablecms_GetClaimsChecks.IssueCheckColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'IssueCheck' in table 'cms_GetClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_GetClaimsChecks.IssueCheckColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool HoldCheck
    {
      get
      {
        try
        {
          return (bool) this[this.tablecms_GetClaimsChecks.HoldCheckColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'HoldCheck' in table 'cms_GetClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_GetClaimsChecks.HoldCheckColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string AmountText
    {
      get
      {
        try
        {
          return (string) this[this.tablecms_GetClaimsChecks.AmountTextColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'AmountText' in table 'cms_GetClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_GetClaimsChecks.AmountTextColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DateTime UpdateDateTime
    {
      get
      {
        try
        {
          return (DateTime) this[this.tablecms_GetClaimsChecks.UpdateDateTimeColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'UpdateDateTime' in table 'cms_GetClaimsChecks' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tablecms_GetClaimsChecks.UpdateDateTimeColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsUserIDNull() => this.IsNull(this.tablecms_GetClaimsChecks.UserIDColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetUserIDNull() => this[this.tablecms_GetClaimsChecks.UserIDColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsPaymentDateNull() => this.IsNull(this.tablecms_GetClaimsChecks.PaymentDateColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetPaymentDateNull() => this[this.tablecms_GetClaimsChecks.PaymentDateColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsClaimNumberNull() => this.IsNull(this.tablecms_GetClaimsChecks.ClaimNumberColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetClaimNumberNull() => this[this.tablecms_GetClaimsChecks.ClaimNumberColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsInjuredNameNull() => this.IsNull(this.tablecms_GetClaimsChecks.InjuredNameColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetInjuredNameNull() => this[this.tablecms_GetClaimsChecks.InjuredNameColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsPayableToNull() => this.IsNull(this.tablecms_GetClaimsChecks.PayableToColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetPayableToNull() => this[this.tablecms_GetClaimsChecks.PayableToColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsAmountNull() => this.IsNull(this.tablecms_GetClaimsChecks.AmountColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetAmountNull() => this[this.tablecms_GetClaimsChecks.AmountColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsExpenseAccountNull() => this.IsNull(this.tablecms_GetClaimsChecks.ExpenseAccountColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetExpenseAccountNull() => this[this.tablecms_GetClaimsChecks.ExpenseAccountColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsAssignedToNull() => this.IsNull(this.tablecms_GetClaimsChecks.AssignedToColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetAssignedToNull() => this[this.tablecms_GetClaimsChecks.AssignedToColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsMemoNull() => this.IsNull(this.tablecms_GetClaimsChecks.MemoColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetMemoNull() => this[this.tablecms_GetClaimsChecks.MemoColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsRecurringPaymentNull() => this.IsNull(this.tablecms_GetClaimsChecks.RecurringPaymentColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetRecurringPaymentNull() => this[this.tablecms_GetClaimsChecks.RecurringPaymentColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsIssueCheckNull() => this.IsNull(this.tablecms_GetClaimsChecks.IssueCheckColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetIssueCheckNull() => this[this.tablecms_GetClaimsChecks.IssueCheckColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsHoldCheckNull() => this.IsNull(this.tablecms_GetClaimsChecks.HoldCheckColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetHoldCheckNull() => this[this.tablecms_GetClaimsChecks.HoldCheckColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsAmountTextNull() => this.IsNull(this.tablecms_GetClaimsChecks.AmountTextColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetAmountTextNull() => this[this.tablecms_GetClaimsChecks.AmountTextColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsUpdateDateTimeNull() => this.IsNull(this.tablecms_GetClaimsChecks.UpdateDateTimeColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetUpdateDateTimeNull() => this[this.tablecms_GetClaimsChecks.UpdateDateTimeColumn] = Convert.DBNull;
  }

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  public class cms_GetClaimsChecksRowChangeEvent : EventArgs
  {
    private ChecksDataSet.cms_GetClaimsChecksRow eventRow;
    private DataRowAction eventAction;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public cms_GetClaimsChecksRowChangeEvent(
      ChecksDataSet.cms_GetClaimsChecksRow row,
      DataRowAction action)
    {
      this.eventRow = row;
      this.eventAction = action;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public ChecksDataSet.cms_GetClaimsChecksRow Row => this.eventRow;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataRowAction Action => this.eventAction;
  }
}
