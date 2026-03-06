// Decompiled with JetBrains decompiler
// Type: DataSet1
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
[DesignerCategory("code")]
[XmlSchemaProvider("GetTypedDataSetSchema")]
[XmlRoot("DataSet1")]
[Serializable]
public class DataSet1 : DataSet
{
  private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
  private DataSet1.SurveyResultsDataTable tableSurveyResults;

  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  public DataSet1()
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
  protected DataSet1(SerializationInfo info, StreamingContext context)
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
        if (dataSet.Tables[nameof (SurveyResults)] != null)
          base.Tables.Add((DataTable) new DataSet1.SurveyResultsDataTable(dataSet.Tables[nameof (SurveyResults)]));
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

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [Browsable(false)]
  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  public DataSet1.SurveyResultsDataTable SurveyResults => this.tableSurveyResults;

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DebuggerNonUserCode]
  [Browsable(true)]
  public override SchemaSerializationMode SchemaSerializationMode
  {
    get => this._schemaSerializationMode;
    set => this._schemaSerializationMode = value;
  }

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerNonUserCode]
  public new DataTableCollection Tables => base.Tables;

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DebuggerNonUserCode]
  public new DataRelationCollection Relations => base.Relations;

  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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
    DataSet1 dataSet1 = (DataSet1) base.Clone();
    dataSet1.InitVars();
    dataSet1.SchemaSerializationMode = this.SchemaSerializationMode;
    return (DataSet) dataSet1;
  }

  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  protected override bool ShouldSerializeTables() => false;

  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  protected override bool ShouldSerializeRelations() => false;

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DebuggerNonUserCode]
  protected override void ReadXmlSerializable(XmlReader reader)
  {
    if (this.DetermineSchemaSerializationMode(reader) == SchemaSerializationMode.IncludeSchema)
    {
      this.Reset();
      DataSet dataSet = new DataSet();
      int num = (int) dataSet.ReadXml(reader);
      if (dataSet.Tables["SurveyResults"] != null)
        base.Tables.Add((DataTable) new DataSet1.SurveyResultsDataTable(dataSet.Tables["SurveyResults"]));
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
    this.tableSurveyResults = (DataSet1.SurveyResultsDataTable) base.Tables["SurveyResults"];
    if (!initTable || this.tableSurveyResults == null)
      return;
    this.tableSurveyResults.InitVars();
  }

  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  private void InitClass()
  {
    this.DataSetName = nameof (DataSet1);
    this.Prefix = "";
    this.Namespace = "http://tempuri.org/DataSet1.xsd";
    this.EnforceConstraints = true;
    this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
    this.tableSurveyResults = new DataSet1.SurveyResultsDataTable();
    base.Tables.Add((DataTable) this.tableSurveyResults);
  }

  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  private bool ShouldSerializeSurveyResults() => false;

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DebuggerNonUserCode]
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
    DataSet1 dataSet1 = new DataSet1();
    XmlSchemaComplexType typedDataSetSchema = new XmlSchemaComplexType();
    XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
    xmlSchemaSequence.Items.Add((XmlSchemaObject) new XmlSchemaAny()
    {
      Namespace = dataSet1.Namespace
    });
    typedDataSetSchema.Particle = (XmlSchemaParticle) xmlSchemaSequence;
    XmlSchema schemaSerializable = dataSet1.GetSchemaSerializable();
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
  public delegate void SurveyResultsRowChangeEventHandler(
    object sender,
    DataSet1.SurveyResultsRowChangeEvent e);

  [XmlSchemaProvider("GetTypedTableSchema")]
  [Serializable]
  public class SurveyResultsDataTable : TypedTableBase<DataSet1.SurveyResultsRow>
  {
    private DataColumn columnImportId;
    private DataColumn columnCompanyId;
    private DataColumn columnDeliveryDate;
    private DataColumn columnCompletionDate;
    private DataColumn columnCSCNo;
    private DataColumn columnLastName;
    private DataColumn columnFirstName;
    private DataColumn columnOriginAgent;
    private DataColumn columnDestinationAgent;
    private DataColumn columnDriverName;
    private DataColumn columnClientName;
    private DataColumn columnCustomerServiceRep;
    private DataColumn columnNationalAcctNo;
    private DataColumn columnLocationCode;
    private DataColumn columnBookerNo;
    private DataColumn columnQuestion;
    private DataColumn columnAnswer;
    private DataColumn columnQuestionId;
    private DataColumn columnShipOriginCity;
    private DataColumn columnShipOriginState;
    private DataColumn columnShipDestinationCity;
    private DataColumn columnShipDestinationState;
    private DataColumn columnCreatedDate;
    private DataColumn columnBatchId;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public SurveyResultsDataTable()
    {
      this.TableName = "SurveyResults";
      this.BeginInit();
      this.InitClass();
      this.EndInit();
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    internal SurveyResultsDataTable(DataTable table)
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
    protected SurveyResultsDataTable(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.InitVars();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn ImportIdColumn => this.columnImportId;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn CompanyIdColumn => this.columnCompanyId;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn DeliveryDateColumn => this.columnDeliveryDate;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn CompletionDateColumn => this.columnCompletionDate;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn CSCNoColumn => this.columnCSCNo;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn LastNameColumn => this.columnLastName;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn FirstNameColumn => this.columnFirstName;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn OriginAgentColumn => this.columnOriginAgent;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn DestinationAgentColumn => this.columnDestinationAgent;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn DriverNameColumn => this.columnDriverName;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn ClientNameColumn => this.columnClientName;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn CustomerServiceRepColumn => this.columnCustomerServiceRep;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn NationalAcctNoColumn => this.columnNationalAcctNo;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn LocationCodeColumn => this.columnLocationCode;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn BookerNoColumn => this.columnBookerNo;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn QuestionColumn => this.columnQuestion;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn AnswerColumn => this.columnAnswer;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn QuestionIdColumn => this.columnQuestionId;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn ShipOriginCityColumn => this.columnShipOriginCity;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn ShipOriginStateColumn => this.columnShipOriginState;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn ShipDestinationCityColumn => this.columnShipDestinationCity;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn ShipDestinationStateColumn => this.columnShipDestinationState;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn CreatedDateColumn => this.columnCreatedDate;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn BatchIdColumn => this.columnBatchId;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    [Browsable(false)]
    public int Count => this.Rows.Count;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataSet1.SurveyResultsRow this[int index] => (DataSet1.SurveyResultsRow) this.Rows[index];

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public event DataSet1.SurveyResultsRowChangeEventHandler SurveyResultsRowChanging;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public event DataSet1.SurveyResultsRowChangeEventHandler SurveyResultsRowChanged;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public event DataSet1.SurveyResultsRowChangeEventHandler SurveyResultsRowDeleting;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public event DataSet1.SurveyResultsRowChangeEventHandler SurveyResultsRowDeleted;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void AddSurveyResultsRow(DataSet1.SurveyResultsRow row) => this.Rows.Add((DataRow) row);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataSet1.SurveyResultsRow AddSurveyResultsRow(
      int CompanyId,
      DateTime DeliveryDate,
      DateTime CompletionDate,
      string CSCNo,
      string LastName,
      string FirstName,
      string OriginAgent,
      string DestinationAgent,
      string DriverName,
      string ClientName,
      string CustomerServiceRep,
      string NationalAcctNo,
      string LocationCode,
      string BookerNo,
      string Question,
      string Answer,
      int QuestionId,
      string ShipOriginCity,
      string ShipOriginState,
      string ShipDestinationCity,
      string ShipDestinationState,
      DateTime CreatedDate,
      int BatchId)
    {
      DataSet1.SurveyResultsRow row = (DataSet1.SurveyResultsRow) this.NewRow();
      object[] objArray = new object[24]
      {
        null,
        (object) CompanyId,
        (object) DeliveryDate,
        (object) CompletionDate,
        (object) CSCNo,
        (object) LastName,
        (object) FirstName,
        (object) OriginAgent,
        (object) DestinationAgent,
        (object) DriverName,
        (object) ClientName,
        (object) CustomerServiceRep,
        (object) NationalAcctNo,
        (object) LocationCode,
        (object) BookerNo,
        (object) Question,
        (object) Answer,
        (object) QuestionId,
        (object) ShipOriginCity,
        (object) ShipOriginState,
        (object) ShipDestinationCity,
        (object) ShipDestinationState,
        (object) CreatedDate,
        (object) BatchId
      };
      row.ItemArray = objArray;
      this.Rows.Add((DataRow) row);
      return row;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataSet1.SurveyResultsRow FindByImportId(int ImportId) => (DataSet1.SurveyResultsRow) this.Rows.Find(new object[1]
    {
      (object) ImportId
    });

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public override DataTable Clone()
    {
      DataSet1.SurveyResultsDataTable resultsDataTable = (DataSet1.SurveyResultsDataTable) base.Clone();
      resultsDataTable.InitVars();
      return (DataTable) resultsDataTable;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override DataTable CreateInstance() => (DataTable) new DataSet1.SurveyResultsDataTable();

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    internal void InitVars()
    {
      this.columnImportId = this.Columns["ImportId"];
      this.columnCompanyId = this.Columns["CompanyId"];
      this.columnDeliveryDate = this.Columns["DeliveryDate"];
      this.columnCompletionDate = this.Columns["CompletionDate"];
      this.columnCSCNo = this.Columns["CSCNo"];
      this.columnLastName = this.Columns["LastName"];
      this.columnFirstName = this.Columns["FirstName"];
      this.columnOriginAgent = this.Columns["OriginAgent"];
      this.columnDestinationAgent = this.Columns["DestinationAgent"];
      this.columnDriverName = this.Columns["DriverName"];
      this.columnClientName = this.Columns["ClientName"];
      this.columnCustomerServiceRep = this.Columns["CustomerServiceRep"];
      this.columnNationalAcctNo = this.Columns["NationalAcctNo"];
      this.columnLocationCode = this.Columns["LocationCode"];
      this.columnBookerNo = this.Columns["BookerNo"];
      this.columnQuestion = this.Columns["Question"];
      this.columnAnswer = this.Columns["Answer"];
      this.columnQuestionId = this.Columns["QuestionId"];
      this.columnShipOriginCity = this.Columns["ShipOriginCity"];
      this.columnShipOriginState = this.Columns["ShipOriginState"];
      this.columnShipDestinationCity = this.Columns["ShipDestinationCity"];
      this.columnShipDestinationState = this.Columns["ShipDestinationState"];
      this.columnCreatedDate = this.Columns["CreatedDate"];
      this.columnBatchId = this.Columns["BatchId"];
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    private void InitClass()
    {
      this.columnImportId = new DataColumn("ImportId", typeof (int), (string) null, MappingType.Element);
      this.Columns.Add(this.columnImportId);
      this.columnCompanyId = new DataColumn("CompanyId", typeof (int), (string) null, MappingType.Element);
      this.Columns.Add(this.columnCompanyId);
      this.columnDeliveryDate = new DataColumn("DeliveryDate", typeof (DateTime), (string) null, MappingType.Element);
      this.Columns.Add(this.columnDeliveryDate);
      this.columnCompletionDate = new DataColumn("CompletionDate", typeof (DateTime), (string) null, MappingType.Element);
      this.Columns.Add(this.columnCompletionDate);
      this.columnCSCNo = new DataColumn("CSCNo", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnCSCNo);
      this.columnLastName = new DataColumn("LastName", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnLastName);
      this.columnFirstName = new DataColumn("FirstName", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnFirstName);
      this.columnOriginAgent = new DataColumn("OriginAgent", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnOriginAgent);
      this.columnDestinationAgent = new DataColumn("DestinationAgent", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnDestinationAgent);
      this.columnDriverName = new DataColumn("DriverName", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnDriverName);
      this.columnClientName = new DataColumn("ClientName", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnClientName);
      this.columnCustomerServiceRep = new DataColumn("CustomerServiceRep", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnCustomerServiceRep);
      this.columnNationalAcctNo = new DataColumn("NationalAcctNo", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnNationalAcctNo);
      this.columnLocationCode = new DataColumn("LocationCode", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnLocationCode);
      this.columnBookerNo = new DataColumn("BookerNo", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnBookerNo);
      this.columnQuestion = new DataColumn("Question", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnQuestion);
      this.columnAnswer = new DataColumn("Answer", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnAnswer);
      this.columnQuestionId = new DataColumn("QuestionId", typeof (int), (string) null, MappingType.Element);
      this.Columns.Add(this.columnQuestionId);
      this.columnShipOriginCity = new DataColumn("ShipOriginCity", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnShipOriginCity);
      this.columnShipOriginState = new DataColumn("ShipOriginState", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnShipOriginState);
      this.columnShipDestinationCity = new DataColumn("ShipDestinationCity", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnShipDestinationCity);
      this.columnShipDestinationState = new DataColumn("ShipDestinationState", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnShipDestinationState);
      this.columnCreatedDate = new DataColumn("CreatedDate", typeof (DateTime), (string) null, MappingType.Element);
      this.Columns.Add(this.columnCreatedDate);
      this.columnBatchId = new DataColumn("BatchId", typeof (int), (string) null, MappingType.Element);
      this.Columns.Add(this.columnBatchId);
      this.Constraints.Add((Constraint) new UniqueConstraint("Constraint1", new DataColumn[1]
      {
        this.columnImportId
      }, true));
      this.columnImportId.AutoIncrement = true;
      this.columnImportId.AllowDBNull = false;
      this.columnImportId.ReadOnly = true;
      this.columnImportId.Unique = true;
      this.columnCSCNo.MaxLength = (int) byte.MaxValue;
      this.columnLastName.MaxLength = (int) byte.MaxValue;
      this.columnFirstName.MaxLength = (int) byte.MaxValue;
      this.columnOriginAgent.MaxLength = (int) byte.MaxValue;
      this.columnDestinationAgent.MaxLength = (int) byte.MaxValue;
      this.columnDriverName.MaxLength = (int) byte.MaxValue;
      this.columnClientName.MaxLength = (int) byte.MaxValue;
      this.columnCustomerServiceRep.MaxLength = (int) byte.MaxValue;
      this.columnNationalAcctNo.MaxLength = (int) byte.MaxValue;
      this.columnLocationCode.MaxLength = (int) byte.MaxValue;
      this.columnBookerNo.MaxLength = (int) byte.MaxValue;
      this.columnQuestion.MaxLength = (int) byte.MaxValue;
      this.columnAnswer.MaxLength = 512;
      this.columnShipOriginCity.MaxLength = 50;
      this.columnShipOriginState.MaxLength = 5;
      this.columnShipDestinationCity.MaxLength = 50;
      this.columnShipDestinationState.MaxLength = 5;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataSet1.SurveyResultsRow NewSurveyResultsRow() => (DataSet1.SurveyResultsRow) this.NewRow();

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => (DataRow) new DataSet1.SurveyResultsRow(builder);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override Type GetRowType() => typeof (DataSet1.SurveyResultsRow);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override void OnRowChanged(DataRowChangeEventArgs e)
    {
      base.OnRowChanged(e);
      if (this.SurveyResultsRowChanged == null)
        return;
      this.SurveyResultsRowChanged((object) this, new DataSet1.SurveyResultsRowChangeEvent((DataSet1.SurveyResultsRow) e.Row, e.Action));
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override void OnRowChanging(DataRowChangeEventArgs e)
    {
      base.OnRowChanging(e);
      if (this.SurveyResultsRowChanging == null)
        return;
      this.SurveyResultsRowChanging((object) this, new DataSet1.SurveyResultsRowChangeEvent((DataSet1.SurveyResultsRow) e.Row, e.Action));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override void OnRowDeleted(DataRowChangeEventArgs e)
    {
      base.OnRowDeleted(e);
      if (this.SurveyResultsRowDeleted == null)
        return;
      this.SurveyResultsRowDeleted((object) this, new DataSet1.SurveyResultsRowChangeEvent((DataSet1.SurveyResultsRow) e.Row, e.Action));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override void OnRowDeleting(DataRowChangeEventArgs e)
    {
      base.OnRowDeleting(e);
      if (this.SurveyResultsRowDeleting == null)
        return;
      this.SurveyResultsRowDeleting((object) this, new DataSet1.SurveyResultsRowChangeEvent((DataSet1.SurveyResultsRow) e.Row, e.Action));
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void RemoveSurveyResultsRow(DataSet1.SurveyResultsRow row) => this.Rows.Remove((DataRow) row);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
    {
      XmlSchemaComplexType typedTableSchema = new XmlSchemaComplexType();
      XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
      DataSet1 dataSet1 = new DataSet1();
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
        FixedValue = dataSet1.Namespace
      });
      typedTableSchema.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
      {
        Name = "tableTypeName",
        FixedValue = nameof (SurveyResultsDataTable)
      });
      typedTableSchema.Particle = (XmlSchemaParticle) xmlSchemaSequence;
      XmlSchema schemaSerializable = dataSet1.GetSchemaSerializable();
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

  public class SurveyResultsRow : DataRow
  {
    private DataSet1.SurveyResultsDataTable tableSurveyResults;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    internal SurveyResultsRow(DataRowBuilder rb)
      : base(rb)
    {
      this.tableSurveyResults = (DataSet1.SurveyResultsDataTable) this.Table;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public int ImportId
    {
      get => (int) this[this.tableSurveyResults.ImportIdColumn];
      set => this[this.tableSurveyResults.ImportIdColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public int CompanyId
    {
      get
      {
        try
        {
          return (int) this[this.tableSurveyResults.CompanyIdColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'CompanyId' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.CompanyIdColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DateTime DeliveryDate
    {
      get
      {
        try
        {
          return (DateTime) this[this.tableSurveyResults.DeliveryDateColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'DeliveryDate' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.DeliveryDateColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DateTime CompletionDate
    {
      get
      {
        try
        {
          return (DateTime) this[this.tableSurveyResults.CompletionDateColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'CompletionDate' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.CompletionDateColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string CSCNo
    {
      get
      {
        try
        {
          return (string) this[this.tableSurveyResults.CSCNoColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'CSCNo' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.CSCNoColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string LastName
    {
      get
      {
        try
        {
          return (string) this[this.tableSurveyResults.LastNameColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'LastName' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.LastNameColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string FirstName
    {
      get
      {
        try
        {
          return (string) this[this.tableSurveyResults.FirstNameColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'FirstName' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.FirstNameColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string OriginAgent
    {
      get
      {
        try
        {
          return (string) this[this.tableSurveyResults.OriginAgentColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'OriginAgent' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.OriginAgentColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string DestinationAgent
    {
      get
      {
        try
        {
          return (string) this[this.tableSurveyResults.DestinationAgentColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'DestinationAgent' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.DestinationAgentColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string DriverName
    {
      get
      {
        try
        {
          return (string) this[this.tableSurveyResults.DriverNameColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'DriverName' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.DriverNameColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string ClientName
    {
      get
      {
        try
        {
          return (string) this[this.tableSurveyResults.ClientNameColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ClientName' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.ClientNameColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string CustomerServiceRep
    {
      get
      {
        try
        {
          return (string) this[this.tableSurveyResults.CustomerServiceRepColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'CustomerServiceRep' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.CustomerServiceRepColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string NationalAcctNo
    {
      get
      {
        try
        {
          return (string) this[this.tableSurveyResults.NationalAcctNoColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'NationalAcctNo' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.NationalAcctNoColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string LocationCode
    {
      get
      {
        try
        {
          return (string) this[this.tableSurveyResults.LocationCodeColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'LocationCode' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.LocationCodeColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string BookerNo
    {
      get
      {
        try
        {
          return (string) this[this.tableSurveyResults.BookerNoColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'BookerNo' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.BookerNoColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string Question
    {
      get
      {
        try
        {
          return (string) this[this.tableSurveyResults.QuestionColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'Question' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.QuestionColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string Answer
    {
      get
      {
        try
        {
          return (string) this[this.tableSurveyResults.AnswerColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'Answer' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.AnswerColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public int QuestionId
    {
      get
      {
        try
        {
          return (int) this[this.tableSurveyResults.QuestionIdColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'QuestionId' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.QuestionIdColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string ShipOriginCity
    {
      get
      {
        try
        {
          return (string) this[this.tableSurveyResults.ShipOriginCityColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ShipOriginCity' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.ShipOriginCityColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string ShipOriginState
    {
      get
      {
        try
        {
          return (string) this[this.tableSurveyResults.ShipOriginStateColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ShipOriginState' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.ShipOriginStateColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string ShipDestinationCity
    {
      get
      {
        try
        {
          return (string) this[this.tableSurveyResults.ShipDestinationCityColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ShipDestinationCity' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.ShipDestinationCityColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string ShipDestinationState
    {
      get
      {
        try
        {
          return (string) this[this.tableSurveyResults.ShipDestinationStateColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ShipDestinationState' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.ShipDestinationStateColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DateTime CreatedDate
    {
      get
      {
        try
        {
          return (DateTime) this[this.tableSurveyResults.CreatedDateColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'CreatedDate' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.CreatedDateColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public int BatchId
    {
      get
      {
        try
        {
          return (int) this[this.tableSurveyResults.BatchIdColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'BatchId' in table 'SurveyResults' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableSurveyResults.BatchIdColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsCompanyIdNull() => this.IsNull(this.tableSurveyResults.CompanyIdColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetCompanyIdNull() => this[this.tableSurveyResults.CompanyIdColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsDeliveryDateNull() => this.IsNull(this.tableSurveyResults.DeliveryDateColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetDeliveryDateNull() => this[this.tableSurveyResults.DeliveryDateColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsCompletionDateNull() => this.IsNull(this.tableSurveyResults.CompletionDateColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetCompletionDateNull() => this[this.tableSurveyResults.CompletionDateColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsCSCNoNull() => this.IsNull(this.tableSurveyResults.CSCNoColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetCSCNoNull() => this[this.tableSurveyResults.CSCNoColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsLastNameNull() => this.IsNull(this.tableSurveyResults.LastNameColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetLastNameNull() => this[this.tableSurveyResults.LastNameColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsFirstNameNull() => this.IsNull(this.tableSurveyResults.FirstNameColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetFirstNameNull() => this[this.tableSurveyResults.FirstNameColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsOriginAgentNull() => this.IsNull(this.tableSurveyResults.OriginAgentColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetOriginAgentNull() => this[this.tableSurveyResults.OriginAgentColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsDestinationAgentNull() => this.IsNull(this.tableSurveyResults.DestinationAgentColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetDestinationAgentNull() => this[this.tableSurveyResults.DestinationAgentColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsDriverNameNull() => this.IsNull(this.tableSurveyResults.DriverNameColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetDriverNameNull() => this[this.tableSurveyResults.DriverNameColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsClientNameNull() => this.IsNull(this.tableSurveyResults.ClientNameColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetClientNameNull() => this[this.tableSurveyResults.ClientNameColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsCustomerServiceRepNull() => this.IsNull(this.tableSurveyResults.CustomerServiceRepColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetCustomerServiceRepNull() => this[this.tableSurveyResults.CustomerServiceRepColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsNationalAcctNoNull() => this.IsNull(this.tableSurveyResults.NationalAcctNoColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetNationalAcctNoNull() => this[this.tableSurveyResults.NationalAcctNoColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsLocationCodeNull() => this.IsNull(this.tableSurveyResults.LocationCodeColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetLocationCodeNull() => this[this.tableSurveyResults.LocationCodeColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsBookerNoNull() => this.IsNull(this.tableSurveyResults.BookerNoColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetBookerNoNull() => this[this.tableSurveyResults.BookerNoColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsQuestionNull() => this.IsNull(this.tableSurveyResults.QuestionColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetQuestionNull() => this[this.tableSurveyResults.QuestionColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsAnswerNull() => this.IsNull(this.tableSurveyResults.AnswerColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetAnswerNull() => this[this.tableSurveyResults.AnswerColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsQuestionIdNull() => this.IsNull(this.tableSurveyResults.QuestionIdColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetQuestionIdNull() => this[this.tableSurveyResults.QuestionIdColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsShipOriginCityNull() => this.IsNull(this.tableSurveyResults.ShipOriginCityColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetShipOriginCityNull() => this[this.tableSurveyResults.ShipOriginCityColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsShipOriginStateNull() => this.IsNull(this.tableSurveyResults.ShipOriginStateColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetShipOriginStateNull() => this[this.tableSurveyResults.ShipOriginStateColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsShipDestinationCityNull() => this.IsNull(this.tableSurveyResults.ShipDestinationCityColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetShipDestinationCityNull() => this[this.tableSurveyResults.ShipDestinationCityColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsShipDestinationStateNull() => this.IsNull(this.tableSurveyResults.ShipDestinationStateColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetShipDestinationStateNull() => this[this.tableSurveyResults.ShipDestinationStateColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsCreatedDateNull() => this.IsNull(this.tableSurveyResults.CreatedDateColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetCreatedDateNull() => this[this.tableSurveyResults.CreatedDateColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsBatchIdNull() => this.IsNull(this.tableSurveyResults.BatchIdColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetBatchIdNull() => this[this.tableSurveyResults.BatchIdColumn] = Convert.DBNull;
  }

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  public class SurveyResultsRowChangeEvent : EventArgs
  {
    private DataSet1.SurveyResultsRow eventRow;
    private DataRowAction eventAction;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public SurveyResultsRowChangeEvent(DataSet1.SurveyResultsRow row, DataRowAction action)
    {
      this.eventRow = row;
      this.eventAction = action;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataSet1.SurveyResultsRow Row => this.eventRow;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataRowAction Action => this.eventAction;
  }
}
