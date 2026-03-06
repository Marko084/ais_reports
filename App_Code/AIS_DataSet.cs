// Decompiled with JetBrains decompiler
// Type: AIS_DataSet
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

[XmlSchemaProvider("GetTypedDataSetSchema")]
[DesignerCategory("code")]
[XmlRoot("AIS_DataSet")]
[HelpKeyword("vs.data.DataSet")]
[ToolboxItem(true)]
[Serializable]
public class AIS_DataSet : DataSet
{
  private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
  private AIS_DataSet.Demo_InvoicesDataTable tableDemo_Invoices;

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DebuggerNonUserCode]
  public AIS_DataSet()
  {
    this.BeginInit();
    this.InitClass();
    CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
    base.Tables.CollectionChanged += changeEventHandler;
    base.Relations.CollectionChanged += changeEventHandler;
    this.EndInit();
  }

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DebuggerNonUserCode]
  protected AIS_DataSet(SerializationInfo info, StreamingContext context)
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
        if (dataSet.Tables[nameof (Demo_Invoices)] != null)
          base.Tables.Add((DataTable) new AIS_DataSet.Demo_InvoicesDataTable(dataSet.Tables[nameof (Demo_Invoices)]));
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

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [DebuggerNonUserCode]
  [Browsable(false)]
  public AIS_DataSet.Demo_InvoicesDataTable Demo_Invoices => this.tableDemo_Invoices;

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  public override SchemaSerializationMode SchemaSerializationMode
  {
    get => this._schemaSerializationMode;
    set => this._schemaSerializationMode = value;
  }

  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new DataTableCollection Tables => base.Tables;

  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
    AIS_DataSet aisDataSet = (AIS_DataSet) base.Clone();
    aisDataSet.InitVars();
    aisDataSet.SchemaSerializationMode = this.SchemaSerializationMode;
    return (DataSet) aisDataSet;
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
      if (dataSet.Tables["Demo_Invoices"] != null)
        base.Tables.Add((DataTable) new AIS_DataSet.Demo_InvoicesDataTable(dataSet.Tables["Demo_Invoices"]));
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

  [DebuggerNonUserCode]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  internal void InitVars(bool initTable)
  {
    this.tableDemo_Invoices = (AIS_DataSet.Demo_InvoicesDataTable) base.Tables["Demo_Invoices"];
    if (!initTable || this.tableDemo_Invoices == null)
      return;
    this.tableDemo_Invoices.InitVars();
  }

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DebuggerNonUserCode]
  private void InitClass()
  {
    this.DataSetName = nameof (AIS_DataSet);
    this.Prefix = "";
    this.Namespace = "http://tempuri.org/AIS_DataSet.xsd";
    this.EnforceConstraints = true;
    this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
    this.tableDemo_Invoices = new AIS_DataSet.Demo_InvoicesDataTable();
    base.Tables.Add((DataTable) this.tableDemo_Invoices);
  }

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  [DebuggerNonUserCode]
  private bool ShouldSerializeDemo_Invoices() => false;

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
    AIS_DataSet aisDataSet = new AIS_DataSet();
    XmlSchemaComplexType typedDataSetSchema = new XmlSchemaComplexType();
    XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
    xmlSchemaSequence.Items.Add((XmlSchemaObject) new XmlSchemaAny()
    {
      Namespace = aisDataSet.Namespace
    });
    typedDataSetSchema.Particle = (XmlSchemaParticle) xmlSchemaSequence;
    XmlSchema schemaSerializable = aisDataSet.GetSchemaSerializable();
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
  public delegate void Demo_InvoicesRowChangeEventHandler(
    object sender,
    AIS_DataSet.Demo_InvoicesRowChangeEvent e);

  [XmlSchemaProvider("GetTypedTableSchema")]
  [Serializable]
  public class Demo_InvoicesDataTable : TypedTableBase<AIS_DataSet.Demo_InvoicesRow>
  {
    private DataColumn columnInvoiceID;
    private DataColumn columnCompanyID;
    private DataColumn columnAISClientCode;
    private DataColumn columnAISClientName;
    private DataColumn columnCorporateAccountName;
    private DataColumn columnVanLine;
    private DataColumn columnShipmentType;
    private DataColumn columnSelfHaul;
    private DataColumn columnCarrierInvoiceNumber;
    private DataColumn columnCarrierShipmentNumber;
    private DataColumn columnTransfereeLastName;
    private DataColumn columnTransfereeFirstName;
    private DataColumn columnShipmentLoadDate;
    private DataColumn columnShipmentDeliveryDate;
    private DataColumn columnShipmentOriginCity;
    private DataColumn columnShipmentOriginState;
    private DataColumn columnShipmentDestinationCity;
    private DataColumn columnShipmentDestinationState;
    private DataColumn columnShipmentEstimatedWeight;
    private DataColumn columnShipmentActualWeight;
    private DataColumn columnShipmentMiles;
    private DataColumn columnLineHaul;
    private DataColumn columnFuel;
    private DataColumn columnIRR;
    private DataColumn columnOriginServiceCharge;
    private DataColumn columnDestinationServiceCharge;
    private DataColumn columnContainer;
    private DataColumn columnPacking;
    private DataColumn columnUnpacking;
    private DataColumn columnOther;
    private DataColumn columnShuttleIndicator;
    private DataColumn columnShuttleOrigin;
    private DataColumn columnShuttleDestination;
    private DataColumn columnThirdPartyChargesOrigin;
    private DataColumn columnThirdPartyChargesDestination;
    private DataColumn columnDebrisPickUp;
    private DataColumn columnExtraLabor;
    private DataColumn columnExtraDelivery;
    private DataColumn columnValuation;
    private DataColumn columnMiscellaneous;
    private DataColumn columnStorageIndicator;
    private DataColumn columnSITDrayageInAmount;
    private DataColumn columnSITDeliveryOutAmount;
    private DataColumn columnStorageInDate;
    private DataColumn columnStorageOutDate;
    private DataColumn columnNumberOfStorageDays;
    private DataColumn columnPermStorage;
    private DataColumn columnCarHauling;
    private DataColumn columnBookerCode;
    private DataColumn columnHaulerCode;
    private DataColumn columnOriginAgentCode;
    private DataColumn columnDestinationAgentCode;
    private DataColumn columnBatchID;
    private DataColumn columnClaimDateFiled;
    private DataColumn columnClaimDateSettled;
    private DataColumn columnClaimAmountFiled;
    private DataColumn columnClaimAmountSettled;
    private DataColumn columnTotalInvoiceAmount;
    private DataColumn columnCreatedDate;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public Demo_InvoicesDataTable()
    {
      this.TableName = "Demo_Invoices";
      this.BeginInit();
      this.InitClass();
      this.EndInit();
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    internal Demo_InvoicesDataTable(DataTable table)
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
    protected Demo_InvoicesDataTable(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.InitVars();
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn InvoiceIDColumn => this.columnInvoiceID;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn CompanyIDColumn => this.columnCompanyID;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn AISClientCodeColumn => this.columnAISClientCode;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn AISClientNameColumn => this.columnAISClientName;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn CorporateAccountNameColumn => this.columnCorporateAccountName;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn VanLineColumn => this.columnVanLine;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn ShipmentTypeColumn => this.columnShipmentType;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn SelfHaulColumn => this.columnSelfHaul;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn CarrierInvoiceNumberColumn => this.columnCarrierInvoiceNumber;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn CarrierShipmentNumberColumn => this.columnCarrierShipmentNumber;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn TransfereeLastNameColumn => this.columnTransfereeLastName;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn TransfereeFirstNameColumn => this.columnTransfereeFirstName;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn ShipmentLoadDateColumn => this.columnShipmentLoadDate;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn ShipmentDeliveryDateColumn => this.columnShipmentDeliveryDate;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn ShipmentOriginCityColumn => this.columnShipmentOriginCity;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn ShipmentOriginStateColumn => this.columnShipmentOriginState;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn ShipmentDestinationCityColumn => this.columnShipmentDestinationCity;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn ShipmentDestinationStateColumn => this.columnShipmentDestinationState;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn ShipmentEstimatedWeightColumn => this.columnShipmentEstimatedWeight;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn ShipmentActualWeightColumn => this.columnShipmentActualWeight;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn ShipmentMilesColumn => this.columnShipmentMiles;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn LineHaulColumn => this.columnLineHaul;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn FuelColumn => this.columnFuel;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn IRRColumn => this.columnIRR;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn OriginServiceChargeColumn => this.columnOriginServiceCharge;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn DestinationServiceChargeColumn => this.columnDestinationServiceCharge;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn ContainerColumn => this.columnContainer;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn PackingColumn => this.columnPacking;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn UnpackingColumn => this.columnUnpacking;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn OtherColumn => this.columnOther;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn ShuttleIndicatorColumn => this.columnShuttleIndicator;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn ShuttleOriginColumn => this.columnShuttleOrigin;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn ShuttleDestinationColumn => this.columnShuttleDestination;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn ThirdPartyChargesOriginColumn => this.columnThirdPartyChargesOrigin;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn ThirdPartyChargesDestinationColumn => this.columnThirdPartyChargesDestination;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn DebrisPickUpColumn => this.columnDebrisPickUp;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn ExtraLaborColumn => this.columnExtraLabor;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn ExtraDeliveryColumn => this.columnExtraDelivery;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn ValuationColumn => this.columnValuation;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn MiscellaneousColumn => this.columnMiscellaneous;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn StorageIndicatorColumn => this.columnStorageIndicator;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn SITDrayageInAmountColumn => this.columnSITDrayageInAmount;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn SITDeliveryOutAmountColumn => this.columnSITDeliveryOutAmount;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn StorageInDateColumn => this.columnStorageInDate;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn StorageOutDateColumn => this.columnStorageOutDate;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn NumberOfStorageDaysColumn => this.columnNumberOfStorageDays;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn PermStorageColumn => this.columnPermStorage;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn CarHaulingColumn => this.columnCarHauling;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn BookerCodeColumn => this.columnBookerCode;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn HaulerCodeColumn => this.columnHaulerCode;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn OriginAgentCodeColumn => this.columnOriginAgentCode;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn DestinationAgentCodeColumn => this.columnDestinationAgentCode;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn BatchIDColumn => this.columnBatchID;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn ClaimDateFiledColumn => this.columnClaimDateFiled;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn ClaimDateSettledColumn => this.columnClaimDateSettled;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn ClaimAmountFiledColumn => this.columnClaimAmountFiled;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataColumn ClaimAmountSettledColumn => this.columnClaimAmountSettled;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn TotalInvoiceAmountColumn => this.columnTotalInvoiceAmount;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DataColumn CreatedDateColumn => this.columnCreatedDate;

    [DebuggerNonUserCode]
    [Browsable(false)]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public int Count => this.Rows.Count;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public AIS_DataSet.Demo_InvoicesRow this[int index] => (AIS_DataSet.Demo_InvoicesRow) this.Rows[index];

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public event AIS_DataSet.Demo_InvoicesRowChangeEventHandler Demo_InvoicesRowChanging;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public event AIS_DataSet.Demo_InvoicesRowChangeEventHandler Demo_InvoicesRowChanged;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public event AIS_DataSet.Demo_InvoicesRowChangeEventHandler Demo_InvoicesRowDeleting;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public event AIS_DataSet.Demo_InvoicesRowChangeEventHandler Demo_InvoicesRowDeleted;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void AddDemo_InvoicesRow(AIS_DataSet.Demo_InvoicesRow row) => this.Rows.Add((DataRow) row);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public AIS_DataSet.Demo_InvoicesRow AddDemo_InvoicesRow(
      int CompanyID,
      string AISClientCode,
      string AISClientName,
      string CorporateAccountName,
      string VanLine,
      string ShipmentType,
      string SelfHaul,
      string CarrierInvoiceNumber,
      string CarrierShipmentNumber,
      string TransfereeLastName,
      string TransfereeFirstName,
      DateTime ShipmentLoadDate,
      DateTime ShipmentDeliveryDate,
      string ShipmentOriginCity,
      string ShipmentOriginState,
      string ShipmentDestinationCity,
      string ShipmentDestinationState,
      Decimal ShipmentEstimatedWeight,
      Decimal ShipmentActualWeight,
      Decimal ShipmentMiles,
      Decimal LineHaul,
      Decimal Fuel,
      Decimal IRR,
      Decimal OriginServiceCharge,
      Decimal DestinationServiceCharge,
      Decimal Container,
      Decimal Packing,
      Decimal Unpacking,
      Decimal Other,
      string ShuttleIndicator,
      Decimal ShuttleOrigin,
      Decimal ShuttleDestination,
      Decimal ThirdPartyChargesOrigin,
      Decimal ThirdPartyChargesDestination,
      Decimal DebrisPickUp,
      Decimal ExtraLabor,
      Decimal ExtraDelivery,
      Decimal Valuation,
      Decimal Miscellaneous,
      string StorageIndicator,
      Decimal SITDrayageInAmount,
      Decimal SITDeliveryOutAmount,
      DateTime StorageInDate,
      DateTime StorageOutDate,
      string NumberOfStorageDays,
      Decimal PermStorage,
      Decimal CarHauling,
      string BookerCode,
      string HaulerCode,
      string OriginAgentCode,
      string DestinationAgentCode,
      int BatchID,
      DateTime ClaimDateFiled,
      DateTime ClaimDateSettled,
      Decimal ClaimAmountFiled,
      Decimal ClaimAmountSettled,
      Decimal TotalInvoiceAmount,
      DateTime CreatedDate)
    {
      AIS_DataSet.Demo_InvoicesRow row = (AIS_DataSet.Demo_InvoicesRow) this.NewRow();
      object[] objArray = new object[59]
      {
        null,
        (object) CompanyID,
        (object) AISClientCode,
        (object) AISClientName,
        (object) CorporateAccountName,
        (object) VanLine,
        (object) ShipmentType,
        (object) SelfHaul,
        (object) CarrierInvoiceNumber,
        (object) CarrierShipmentNumber,
        (object) TransfereeLastName,
        (object) TransfereeFirstName,
        (object) ShipmentLoadDate,
        (object) ShipmentDeliveryDate,
        (object) ShipmentOriginCity,
        (object) ShipmentOriginState,
        (object) ShipmentDestinationCity,
        (object) ShipmentDestinationState,
        (object) ShipmentEstimatedWeight,
        (object) ShipmentActualWeight,
        (object) ShipmentMiles,
        (object) LineHaul,
        (object) Fuel,
        (object) IRR,
        (object) OriginServiceCharge,
        (object) DestinationServiceCharge,
        (object) Container,
        (object) Packing,
        (object) Unpacking,
        (object) Other,
        (object) ShuttleIndicator,
        (object) ShuttleOrigin,
        (object) ShuttleDestination,
        (object) ThirdPartyChargesOrigin,
        (object) ThirdPartyChargesDestination,
        (object) DebrisPickUp,
        (object) ExtraLabor,
        (object) ExtraDelivery,
        (object) Valuation,
        (object) Miscellaneous,
        (object) StorageIndicator,
        (object) SITDrayageInAmount,
        (object) SITDeliveryOutAmount,
        (object) StorageInDate,
        (object) StorageOutDate,
        (object) NumberOfStorageDays,
        (object) PermStorage,
        (object) CarHauling,
        (object) BookerCode,
        (object) HaulerCode,
        (object) OriginAgentCode,
        (object) DestinationAgentCode,
        (object) BatchID,
        (object) ClaimDateFiled,
        (object) ClaimDateSettled,
        (object) ClaimAmountFiled,
        (object) ClaimAmountSettled,
        (object) TotalInvoiceAmount,
        (object) CreatedDate
      };
      row.ItemArray = objArray;
      this.Rows.Add((DataRow) row);
      return row;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public override DataTable Clone()
    {
      AIS_DataSet.Demo_InvoicesDataTable invoicesDataTable = (AIS_DataSet.Demo_InvoicesDataTable) base.Clone();
      invoicesDataTable.InitVars();
      return (DataTable) invoicesDataTable;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override DataTable CreateInstance() => (DataTable) new AIS_DataSet.Demo_InvoicesDataTable();

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    internal void InitVars()
    {
      this.columnInvoiceID = this.Columns["InvoiceID"];
      this.columnCompanyID = this.Columns["CompanyID"];
      this.columnAISClientCode = this.Columns["AISClientCode"];
      this.columnAISClientName = this.Columns["AISClientName"];
      this.columnCorporateAccountName = this.Columns["CorporateAccountName"];
      this.columnVanLine = this.Columns["VanLine"];
      this.columnShipmentType = this.Columns["ShipmentType"];
      this.columnSelfHaul = this.Columns["SelfHaul"];
      this.columnCarrierInvoiceNumber = this.Columns["CarrierInvoiceNumber"];
      this.columnCarrierShipmentNumber = this.Columns["CarrierShipmentNumber"];
      this.columnTransfereeLastName = this.Columns["TransfereeLastName"];
      this.columnTransfereeFirstName = this.Columns["TransfereeFirstName"];
      this.columnShipmentLoadDate = this.Columns["ShipmentLoadDate"];
      this.columnShipmentDeliveryDate = this.Columns["ShipmentDeliveryDate"];
      this.columnShipmentOriginCity = this.Columns["ShipmentOriginCity"];
      this.columnShipmentOriginState = this.Columns["ShipmentOriginState"];
      this.columnShipmentDestinationCity = this.Columns["ShipmentDestinationCity"];
      this.columnShipmentDestinationState = this.Columns["ShipmentDestinationState"];
      this.columnShipmentEstimatedWeight = this.Columns["ShipmentEstimatedWeight"];
      this.columnShipmentActualWeight = this.Columns["ShipmentActualWeight"];
      this.columnShipmentMiles = this.Columns["ShipmentMiles"];
      this.columnLineHaul = this.Columns["LineHaul"];
      this.columnFuel = this.Columns["Fuel"];
      this.columnIRR = this.Columns["IRR"];
      this.columnOriginServiceCharge = this.Columns["OriginServiceCharge"];
      this.columnDestinationServiceCharge = this.Columns["DestinationServiceCharge"];
      this.columnContainer = this.Columns["Container"];
      this.columnPacking = this.Columns["Packing"];
      this.columnUnpacking = this.Columns["Unpacking"];
      this.columnOther = this.Columns["Other"];
      this.columnShuttleIndicator = this.Columns["ShuttleIndicator"];
      this.columnShuttleOrigin = this.Columns["ShuttleOrigin"];
      this.columnShuttleDestination = this.Columns["ShuttleDestination"];
      this.columnThirdPartyChargesOrigin = this.Columns["ThirdPartyChargesOrigin"];
      this.columnThirdPartyChargesDestination = this.Columns["ThirdPartyChargesDestination"];
      this.columnDebrisPickUp = this.Columns["DebrisPickUp"];
      this.columnExtraLabor = this.Columns["ExtraLabor"];
      this.columnExtraDelivery = this.Columns["ExtraDelivery"];
      this.columnValuation = this.Columns["Valuation"];
      this.columnMiscellaneous = this.Columns["Miscellaneous"];
      this.columnStorageIndicator = this.Columns["StorageIndicator"];
      this.columnSITDrayageInAmount = this.Columns["SITDrayageInAmount"];
      this.columnSITDeliveryOutAmount = this.Columns["SITDeliveryOutAmount"];
      this.columnStorageInDate = this.Columns["StorageInDate"];
      this.columnStorageOutDate = this.Columns["StorageOutDate"];
      this.columnNumberOfStorageDays = this.Columns["NumberOfStorageDays"];
      this.columnPermStorage = this.Columns["PermStorage"];
      this.columnCarHauling = this.Columns["CarHauling"];
      this.columnBookerCode = this.Columns["BookerCode"];
      this.columnHaulerCode = this.Columns["HaulerCode"];
      this.columnOriginAgentCode = this.Columns["OriginAgentCode"];
      this.columnDestinationAgentCode = this.Columns["DestinationAgentCode"];
      this.columnBatchID = this.Columns["BatchID"];
      this.columnClaimDateFiled = this.Columns["ClaimDateFiled"];
      this.columnClaimDateSettled = this.Columns["ClaimDateSettled"];
      this.columnClaimAmountFiled = this.Columns["ClaimAmountFiled"];
      this.columnClaimAmountSettled = this.Columns["ClaimAmountSettled"];
      this.columnTotalInvoiceAmount = this.Columns["TotalInvoiceAmount"];
      this.columnCreatedDate = this.Columns["CreatedDate"];
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    private void InitClass()
    {
      this.columnInvoiceID = new DataColumn("InvoiceID", typeof (int), (string) null, MappingType.Element);
      this.Columns.Add(this.columnInvoiceID);
      this.columnCompanyID = new DataColumn("CompanyID", typeof (int), (string) null, MappingType.Element);
      this.Columns.Add(this.columnCompanyID);
      this.columnAISClientCode = new DataColumn("AISClientCode", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnAISClientCode);
      this.columnAISClientName = new DataColumn("AISClientName", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnAISClientName);
      this.columnCorporateAccountName = new DataColumn("CorporateAccountName", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnCorporateAccountName);
      this.columnVanLine = new DataColumn("VanLine", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnVanLine);
      this.columnShipmentType = new DataColumn("ShipmentType", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnShipmentType);
      this.columnSelfHaul = new DataColumn("SelfHaul", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnSelfHaul);
      this.columnCarrierInvoiceNumber = new DataColumn("CarrierInvoiceNumber", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnCarrierInvoiceNumber);
      this.columnCarrierShipmentNumber = new DataColumn("CarrierShipmentNumber", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnCarrierShipmentNumber);
      this.columnTransfereeLastName = new DataColumn("TransfereeLastName", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnTransfereeLastName);
      this.columnTransfereeFirstName = new DataColumn("TransfereeFirstName", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnTransfereeFirstName);
      this.columnShipmentLoadDate = new DataColumn("ShipmentLoadDate", typeof (DateTime), (string) null, MappingType.Element);
      this.Columns.Add(this.columnShipmentLoadDate);
      this.columnShipmentDeliveryDate = new DataColumn("ShipmentDeliveryDate", typeof (DateTime), (string) null, MappingType.Element);
      this.Columns.Add(this.columnShipmentDeliveryDate);
      this.columnShipmentOriginCity = new DataColumn("ShipmentOriginCity", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnShipmentOriginCity);
      this.columnShipmentOriginState = new DataColumn("ShipmentOriginState", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnShipmentOriginState);
      this.columnShipmentDestinationCity = new DataColumn("ShipmentDestinationCity", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnShipmentDestinationCity);
      this.columnShipmentDestinationState = new DataColumn("ShipmentDestinationState", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnShipmentDestinationState);
      this.columnShipmentEstimatedWeight = new DataColumn("ShipmentEstimatedWeight", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnShipmentEstimatedWeight);
      this.columnShipmentActualWeight = new DataColumn("ShipmentActualWeight", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnShipmentActualWeight);
      this.columnShipmentMiles = new DataColumn("ShipmentMiles", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnShipmentMiles);
      this.columnLineHaul = new DataColumn("LineHaul", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnLineHaul);
      this.columnFuel = new DataColumn("Fuel", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnFuel);
      this.columnIRR = new DataColumn("IRR", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnIRR);
      this.columnOriginServiceCharge = new DataColumn("OriginServiceCharge", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnOriginServiceCharge);
      this.columnDestinationServiceCharge = new DataColumn("DestinationServiceCharge", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnDestinationServiceCharge);
      this.columnContainer = new DataColumn("Container", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnContainer);
      this.columnPacking = new DataColumn("Packing", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnPacking);
      this.columnUnpacking = new DataColumn("Unpacking", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnUnpacking);
      this.columnOther = new DataColumn("Other", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnOther);
      this.columnShuttleIndicator = new DataColumn("ShuttleIndicator", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnShuttleIndicator);
      this.columnShuttleOrigin = new DataColumn("ShuttleOrigin", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnShuttleOrigin);
      this.columnShuttleDestination = new DataColumn("ShuttleDestination", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnShuttleDestination);
      this.columnThirdPartyChargesOrigin = new DataColumn("ThirdPartyChargesOrigin", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnThirdPartyChargesOrigin);
      this.columnThirdPartyChargesDestination = new DataColumn("ThirdPartyChargesDestination", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnThirdPartyChargesDestination);
      this.columnDebrisPickUp = new DataColumn("DebrisPickUp", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnDebrisPickUp);
      this.columnExtraLabor = new DataColumn("ExtraLabor", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnExtraLabor);
      this.columnExtraDelivery = new DataColumn("ExtraDelivery", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnExtraDelivery);
      this.columnValuation = new DataColumn("Valuation", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnValuation);
      this.columnMiscellaneous = new DataColumn("Miscellaneous", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnMiscellaneous);
      this.columnStorageIndicator = new DataColumn("StorageIndicator", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnStorageIndicator);
      this.columnSITDrayageInAmount = new DataColumn("SITDrayageInAmount", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnSITDrayageInAmount);
      this.columnSITDeliveryOutAmount = new DataColumn("SITDeliveryOutAmount", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnSITDeliveryOutAmount);
      this.columnStorageInDate = new DataColumn("StorageInDate", typeof (DateTime), (string) null, MappingType.Element);
      this.Columns.Add(this.columnStorageInDate);
      this.columnStorageOutDate = new DataColumn("StorageOutDate", typeof (DateTime), (string) null, MappingType.Element);
      this.Columns.Add(this.columnStorageOutDate);
      this.columnNumberOfStorageDays = new DataColumn("NumberOfStorageDays", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnNumberOfStorageDays);
      this.columnPermStorage = new DataColumn("PermStorage", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnPermStorage);
      this.columnCarHauling = new DataColumn("CarHauling", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnCarHauling);
      this.columnBookerCode = new DataColumn("BookerCode", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnBookerCode);
      this.columnHaulerCode = new DataColumn("HaulerCode", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnHaulerCode);
      this.columnOriginAgentCode = new DataColumn("OriginAgentCode", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnOriginAgentCode);
      this.columnDestinationAgentCode = new DataColumn("DestinationAgentCode", typeof (string), (string) null, MappingType.Element);
      this.Columns.Add(this.columnDestinationAgentCode);
      this.columnBatchID = new DataColumn("BatchID", typeof (int), (string) null, MappingType.Element);
      this.Columns.Add(this.columnBatchID);
      this.columnClaimDateFiled = new DataColumn("ClaimDateFiled", typeof (DateTime), (string) null, MappingType.Element);
      this.Columns.Add(this.columnClaimDateFiled);
      this.columnClaimDateSettled = new DataColumn("ClaimDateSettled", typeof (DateTime), (string) null, MappingType.Element);
      this.Columns.Add(this.columnClaimDateSettled);
      this.columnClaimAmountFiled = new DataColumn("ClaimAmountFiled", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnClaimAmountFiled);
      this.columnClaimAmountSettled = new DataColumn("ClaimAmountSettled", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnClaimAmountSettled);
      this.columnTotalInvoiceAmount = new DataColumn("TotalInvoiceAmount", typeof (Decimal), (string) null, MappingType.Element);
      this.Columns.Add(this.columnTotalInvoiceAmount);
      this.columnCreatedDate = new DataColumn("CreatedDate", typeof (DateTime), (string) null, MappingType.Element);
      this.Columns.Add(this.columnCreatedDate);
      this.columnInvoiceID.AutoIncrement = true;
      this.columnInvoiceID.AutoIncrementSeed = -1L;
      this.columnInvoiceID.AutoIncrementStep = -1L;
      this.columnInvoiceID.AllowDBNull = false;
      this.columnInvoiceID.ReadOnly = true;
      this.columnAISClientCode.MaxLength = 50;
      this.columnAISClientName.MaxLength = 50;
      this.columnCorporateAccountName.MaxLength = 50;
      this.columnVanLine.MaxLength = 50;
      this.columnShipmentType.MaxLength = 50;
      this.columnSelfHaul.MaxLength = 50;
      this.columnCarrierInvoiceNumber.MaxLength = 50;
      this.columnCarrierShipmentNumber.MaxLength = 50;
      this.columnTransfereeLastName.MaxLength = 50;
      this.columnTransfereeFirstName.MaxLength = 50;
      this.columnShipmentOriginCity.MaxLength = 50;
      this.columnShipmentOriginState.MaxLength = 50;
      this.columnShipmentDestinationCity.MaxLength = 50;
      this.columnShipmentDestinationState.MaxLength = 50;
      this.columnShuttleIndicator.MaxLength = 50;
      this.columnStorageIndicator.MaxLength = 50;
      this.columnNumberOfStorageDays.MaxLength = 50;
      this.columnBookerCode.MaxLength = 10;
      this.columnHaulerCode.MaxLength = 10;
      this.columnOriginAgentCode.MaxLength = 10;
      this.columnDestinationAgentCode.MaxLength = 10;
      this.columnCreatedDate.AllowDBNull = false;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public AIS_DataSet.Demo_InvoicesRow NewDemo_InvoicesRow() => (AIS_DataSet.Demo_InvoicesRow) this.NewRow();

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => (DataRow) new AIS_DataSet.Demo_InvoicesRow(builder);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override Type GetRowType() => typeof (AIS_DataSet.Demo_InvoicesRow);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override void OnRowChanged(DataRowChangeEventArgs e)
    {
      base.OnRowChanged(e);
      if (this.Demo_InvoicesRowChanged == null)
        return;
      this.Demo_InvoicesRowChanged((object) this, new AIS_DataSet.Demo_InvoicesRowChangeEvent((AIS_DataSet.Demo_InvoicesRow) e.Row, e.Action));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override void OnRowChanging(DataRowChangeEventArgs e)
    {
      base.OnRowChanging(e);
      if (this.Demo_InvoicesRowChanging == null)
        return;
      this.Demo_InvoicesRowChanging((object) this, new AIS_DataSet.Demo_InvoicesRowChangeEvent((AIS_DataSet.Demo_InvoicesRow) e.Row, e.Action));
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override void OnRowDeleted(DataRowChangeEventArgs e)
    {
      base.OnRowDeleted(e);
      if (this.Demo_InvoicesRowDeleted == null)
        return;
      this.Demo_InvoicesRowDeleted((object) this, new AIS_DataSet.Demo_InvoicesRowChangeEvent((AIS_DataSet.Demo_InvoicesRow) e.Row, e.Action));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override void OnRowDeleting(DataRowChangeEventArgs e)
    {
      base.OnRowDeleting(e);
      if (this.Demo_InvoicesRowDeleting == null)
        return;
      this.Demo_InvoicesRowDeleting((object) this, new AIS_DataSet.Demo_InvoicesRowChangeEvent((AIS_DataSet.Demo_InvoicesRow) e.Row, e.Action));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void RemoveDemo_InvoicesRow(AIS_DataSet.Demo_InvoicesRow row) => this.Rows.Remove((DataRow) row);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
    {
      XmlSchemaComplexType typedTableSchema = new XmlSchemaComplexType();
      XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
      AIS_DataSet aisDataSet = new AIS_DataSet();
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
        FixedValue = aisDataSet.Namespace
      });
      typedTableSchema.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
      {
        Name = "tableTypeName",
        FixedValue = nameof (Demo_InvoicesDataTable)
      });
      typedTableSchema.Particle = (XmlSchemaParticle) xmlSchemaSequence;
      XmlSchema schemaSerializable = aisDataSet.GetSchemaSerializable();
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

  public class Demo_InvoicesRow : DataRow
  {
    private AIS_DataSet.Demo_InvoicesDataTable tableDemo_Invoices;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    internal Demo_InvoicesRow(DataRowBuilder rb)
      : base(rb)
    {
      this.tableDemo_Invoices = (AIS_DataSet.Demo_InvoicesDataTable) this.Table;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public int InvoiceID
    {
      get => (int) this[this.tableDemo_Invoices.InvoiceIDColumn];
      set => this[this.tableDemo_Invoices.InvoiceIDColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public int CompanyID
    {
      get
      {
        try
        {
          return (int) this[this.tableDemo_Invoices.CompanyIDColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'CompanyID' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.CompanyIDColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string AISClientCode
    {
      get
      {
        try
        {
          return (string) this[this.tableDemo_Invoices.AISClientCodeColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'AISClientCode' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.AISClientCodeColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string AISClientName
    {
      get
      {
        try
        {
          return (string) this[this.tableDemo_Invoices.AISClientNameColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'AISClientName' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.AISClientNameColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string CorporateAccountName
    {
      get
      {
        try
        {
          return (string) this[this.tableDemo_Invoices.CorporateAccountNameColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'CorporateAccountName' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.CorporateAccountNameColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string VanLine
    {
      get
      {
        try
        {
          return (string) this[this.tableDemo_Invoices.VanLineColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'VanLine' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.VanLineColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string ShipmentType
    {
      get
      {
        try
        {
          return (string) this[this.tableDemo_Invoices.ShipmentTypeColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ShipmentType' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ShipmentTypeColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string SelfHaul
    {
      get
      {
        try
        {
          return (string) this[this.tableDemo_Invoices.SelfHaulColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'SelfHaul' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.SelfHaulColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string CarrierInvoiceNumber
    {
      get
      {
        try
        {
          return (string) this[this.tableDemo_Invoices.CarrierInvoiceNumberColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'CarrierInvoiceNumber' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.CarrierInvoiceNumberColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string CarrierShipmentNumber
    {
      get
      {
        try
        {
          return (string) this[this.tableDemo_Invoices.CarrierShipmentNumberColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'CarrierShipmentNumber' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.CarrierShipmentNumberColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string TransfereeLastName
    {
      get
      {
        try
        {
          return (string) this[this.tableDemo_Invoices.TransfereeLastNameColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'TransfereeLastName' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.TransfereeLastNameColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string TransfereeFirstName
    {
      get
      {
        try
        {
          return (string) this[this.tableDemo_Invoices.TransfereeFirstNameColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'TransfereeFirstName' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.TransfereeFirstNameColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DateTime ShipmentLoadDate
    {
      get
      {
        try
        {
          return (DateTime) this[this.tableDemo_Invoices.ShipmentLoadDateColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ShipmentLoadDate' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ShipmentLoadDateColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DateTime ShipmentDeliveryDate
    {
      get
      {
        try
        {
          return (DateTime) this[this.tableDemo_Invoices.ShipmentDeliveryDateColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ShipmentDeliveryDate' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ShipmentDeliveryDateColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string ShipmentOriginCity
    {
      get
      {
        try
        {
          return (string) this[this.tableDemo_Invoices.ShipmentOriginCityColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ShipmentOriginCity' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ShipmentOriginCityColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string ShipmentOriginState
    {
      get
      {
        try
        {
          return (string) this[this.tableDemo_Invoices.ShipmentOriginStateColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ShipmentOriginState' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ShipmentOriginStateColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string ShipmentDestinationCity
    {
      get
      {
        try
        {
          return (string) this[this.tableDemo_Invoices.ShipmentDestinationCityColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ShipmentDestinationCity' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ShipmentDestinationCityColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string ShipmentDestinationState
    {
      get
      {
        try
        {
          return (string) this[this.tableDemo_Invoices.ShipmentDestinationStateColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ShipmentDestinationState' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ShipmentDestinationStateColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public Decimal ShipmentEstimatedWeight
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.ShipmentEstimatedWeightColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ShipmentEstimatedWeight' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ShipmentEstimatedWeightColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public Decimal ShipmentActualWeight
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.ShipmentActualWeightColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ShipmentActualWeight' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ShipmentActualWeightColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public Decimal ShipmentMiles
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.ShipmentMilesColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ShipmentMiles' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ShipmentMilesColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public Decimal LineHaul
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.LineHaulColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'LineHaul' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.LineHaulColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public Decimal Fuel
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.FuelColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'Fuel' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.FuelColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public Decimal IRR
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.IRRColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'IRR' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.IRRColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public Decimal OriginServiceCharge
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.OriginServiceChargeColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'OriginServiceCharge' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.OriginServiceChargeColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public Decimal DestinationServiceCharge
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.DestinationServiceChargeColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'DestinationServiceCharge' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.DestinationServiceChargeColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public Decimal Container
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.ContainerColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'Container' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ContainerColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public Decimal Packing
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.PackingColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'Packing' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.PackingColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public Decimal Unpacking
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.UnpackingColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'Unpacking' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.UnpackingColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public Decimal Other
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.OtherColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'Other' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.OtherColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string ShuttleIndicator
    {
      get
      {
        try
        {
          return (string) this[this.tableDemo_Invoices.ShuttleIndicatorColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ShuttleIndicator' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ShuttleIndicatorColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public Decimal ShuttleOrigin
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.ShuttleOriginColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ShuttleOrigin' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ShuttleOriginColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public Decimal ShuttleDestination
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.ShuttleDestinationColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ShuttleDestination' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ShuttleDestinationColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public Decimal ThirdPartyChargesOrigin
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.ThirdPartyChargesOriginColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ThirdPartyChargesOrigin' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ThirdPartyChargesOriginColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public Decimal ThirdPartyChargesDestination
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.ThirdPartyChargesDestinationColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ThirdPartyChargesDestination' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ThirdPartyChargesDestinationColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public Decimal DebrisPickUp
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.DebrisPickUpColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'DebrisPickUp' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.DebrisPickUpColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public Decimal ExtraLabor
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.ExtraLaborColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ExtraLabor' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ExtraLaborColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public Decimal ExtraDelivery
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.ExtraDeliveryColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ExtraDelivery' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ExtraDeliveryColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public Decimal Valuation
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.ValuationColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'Valuation' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ValuationColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public Decimal Miscellaneous
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.MiscellaneousColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'Miscellaneous' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.MiscellaneousColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string StorageIndicator
    {
      get
      {
        try
        {
          return (string) this[this.tableDemo_Invoices.StorageIndicatorColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'StorageIndicator' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.StorageIndicatorColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public Decimal SITDrayageInAmount
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.SITDrayageInAmountColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'SITDrayageInAmount' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.SITDrayageInAmountColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public Decimal SITDeliveryOutAmount
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.SITDeliveryOutAmountColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'SITDeliveryOutAmount' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.SITDeliveryOutAmountColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DateTime StorageInDate
    {
      get
      {
        try
        {
          return (DateTime) this[this.tableDemo_Invoices.StorageInDateColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'StorageInDate' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.StorageInDateColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DateTime StorageOutDate
    {
      get
      {
        try
        {
          return (DateTime) this[this.tableDemo_Invoices.StorageOutDateColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'StorageOutDate' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.StorageOutDateColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string NumberOfStorageDays
    {
      get
      {
        try
        {
          return (string) this[this.tableDemo_Invoices.NumberOfStorageDaysColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'NumberOfStorageDays' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.NumberOfStorageDaysColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public Decimal PermStorage
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.PermStorageColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'PermStorage' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.PermStorageColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public Decimal CarHauling
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.CarHaulingColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'CarHauling' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.CarHaulingColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string BookerCode
    {
      get
      {
        try
        {
          return (string) this[this.tableDemo_Invoices.BookerCodeColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'BookerCode' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.BookerCodeColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string HaulerCode
    {
      get
      {
        try
        {
          return (string) this[this.tableDemo_Invoices.HaulerCodeColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'HaulerCode' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.HaulerCodeColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public string OriginAgentCode
    {
      get
      {
        try
        {
          return (string) this[this.tableDemo_Invoices.OriginAgentCodeColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'OriginAgentCode' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.OriginAgentCodeColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public string DestinationAgentCode
    {
      get
      {
        try
        {
          return (string) this[this.tableDemo_Invoices.DestinationAgentCodeColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'DestinationAgentCode' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.DestinationAgentCodeColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public int BatchID
    {
      get
      {
        try
        {
          return (int) this[this.tableDemo_Invoices.BatchIDColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'BatchID' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.BatchIDColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DateTime ClaimDateFiled
    {
      get
      {
        try
        {
          return (DateTime) this[this.tableDemo_Invoices.ClaimDateFiledColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ClaimDateFiled' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ClaimDateFiledColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DateTime ClaimDateSettled
    {
      get
      {
        try
        {
          return (DateTime) this[this.tableDemo_Invoices.ClaimDateSettledColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ClaimDateSettled' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ClaimDateSettledColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public Decimal ClaimAmountFiled
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.ClaimAmountFiledColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ClaimAmountFiled' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ClaimAmountFiledColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public Decimal ClaimAmountSettled
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.ClaimAmountSettledColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'ClaimAmountSettled' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.ClaimAmountSettledColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public Decimal TotalInvoiceAmount
    {
      get
      {
        try
        {
          return (Decimal) this[this.tableDemo_Invoices.TotalInvoiceAmountColumn];
        }
        catch (InvalidCastException ex)
        {
          throw new StrongTypingException("The value for column 'TotalInvoiceAmount' in table 'Demo_Invoices' is DBNull.", (Exception) ex);
        }
      }
      set => this[this.tableDemo_Invoices.TotalInvoiceAmountColumn] = (object) value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public DateTime CreatedDate
    {
      get => (DateTime) this[this.tableDemo_Invoices.CreatedDateColumn];
      set => this[this.tableDemo_Invoices.CreatedDateColumn] = (object) value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsCompanyIDNull() => this.IsNull(this.tableDemo_Invoices.CompanyIDColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetCompanyIDNull() => this[this.tableDemo_Invoices.CompanyIDColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsAISClientCodeNull() => this.IsNull(this.tableDemo_Invoices.AISClientCodeColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetAISClientCodeNull() => this[this.tableDemo_Invoices.AISClientCodeColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsAISClientNameNull() => this.IsNull(this.tableDemo_Invoices.AISClientNameColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetAISClientNameNull() => this[this.tableDemo_Invoices.AISClientNameColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsCorporateAccountNameNull() => this.IsNull(this.tableDemo_Invoices.CorporateAccountNameColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetCorporateAccountNameNull() => this[this.tableDemo_Invoices.CorporateAccountNameColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsVanLineNull() => this.IsNull(this.tableDemo_Invoices.VanLineColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetVanLineNull() => this[this.tableDemo_Invoices.VanLineColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsShipmentTypeNull() => this.IsNull(this.tableDemo_Invoices.ShipmentTypeColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetShipmentTypeNull() => this[this.tableDemo_Invoices.ShipmentTypeColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsSelfHaulNull() => this.IsNull(this.tableDemo_Invoices.SelfHaulColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetSelfHaulNull() => this[this.tableDemo_Invoices.SelfHaulColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsCarrierInvoiceNumberNull() => this.IsNull(this.tableDemo_Invoices.CarrierInvoiceNumberColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetCarrierInvoiceNumberNull() => this[this.tableDemo_Invoices.CarrierInvoiceNumberColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsCarrierShipmentNumberNull() => this.IsNull(this.tableDemo_Invoices.CarrierShipmentNumberColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetCarrierShipmentNumberNull() => this[this.tableDemo_Invoices.CarrierShipmentNumberColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsTransfereeLastNameNull() => this.IsNull(this.tableDemo_Invoices.TransfereeLastNameColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetTransfereeLastNameNull() => this[this.tableDemo_Invoices.TransfereeLastNameColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsTransfereeFirstNameNull() => this.IsNull(this.tableDemo_Invoices.TransfereeFirstNameColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetTransfereeFirstNameNull() => this[this.tableDemo_Invoices.TransfereeFirstNameColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsShipmentLoadDateNull() => this.IsNull(this.tableDemo_Invoices.ShipmentLoadDateColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetShipmentLoadDateNull() => this[this.tableDemo_Invoices.ShipmentLoadDateColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsShipmentDeliveryDateNull() => this.IsNull(this.tableDemo_Invoices.ShipmentDeliveryDateColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetShipmentDeliveryDateNull() => this[this.tableDemo_Invoices.ShipmentDeliveryDateColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsShipmentOriginCityNull() => this.IsNull(this.tableDemo_Invoices.ShipmentOriginCityColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetShipmentOriginCityNull() => this[this.tableDemo_Invoices.ShipmentOriginCityColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsShipmentOriginStateNull() => this.IsNull(this.tableDemo_Invoices.ShipmentOriginStateColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetShipmentOriginStateNull() => this[this.tableDemo_Invoices.ShipmentOriginStateColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsShipmentDestinationCityNull() => this.IsNull(this.tableDemo_Invoices.ShipmentDestinationCityColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetShipmentDestinationCityNull() => this[this.tableDemo_Invoices.ShipmentDestinationCityColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsShipmentDestinationStateNull() => this.IsNull(this.tableDemo_Invoices.ShipmentDestinationStateColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetShipmentDestinationStateNull() => this[this.tableDemo_Invoices.ShipmentDestinationStateColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsShipmentEstimatedWeightNull() => this.IsNull(this.tableDemo_Invoices.ShipmentEstimatedWeightColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetShipmentEstimatedWeightNull() => this[this.tableDemo_Invoices.ShipmentEstimatedWeightColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsShipmentActualWeightNull() => this.IsNull(this.tableDemo_Invoices.ShipmentActualWeightColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetShipmentActualWeightNull() => this[this.tableDemo_Invoices.ShipmentActualWeightColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsShipmentMilesNull() => this.IsNull(this.tableDemo_Invoices.ShipmentMilesColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetShipmentMilesNull() => this[this.tableDemo_Invoices.ShipmentMilesColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsLineHaulNull() => this.IsNull(this.tableDemo_Invoices.LineHaulColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetLineHaulNull() => this[this.tableDemo_Invoices.LineHaulColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsFuelNull() => this.IsNull(this.tableDemo_Invoices.FuelColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetFuelNull() => this[this.tableDemo_Invoices.FuelColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsIRRNull() => this.IsNull(this.tableDemo_Invoices.IRRColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetIRRNull() => this[this.tableDemo_Invoices.IRRColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsOriginServiceChargeNull() => this.IsNull(this.tableDemo_Invoices.OriginServiceChargeColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetOriginServiceChargeNull() => this[this.tableDemo_Invoices.OriginServiceChargeColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsDestinationServiceChargeNull() => this.IsNull(this.tableDemo_Invoices.DestinationServiceChargeColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetDestinationServiceChargeNull() => this[this.tableDemo_Invoices.DestinationServiceChargeColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsContainerNull() => this.IsNull(this.tableDemo_Invoices.ContainerColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetContainerNull() => this[this.tableDemo_Invoices.ContainerColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsPackingNull() => this.IsNull(this.tableDemo_Invoices.PackingColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetPackingNull() => this[this.tableDemo_Invoices.PackingColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsUnpackingNull() => this.IsNull(this.tableDemo_Invoices.UnpackingColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetUnpackingNull() => this[this.tableDemo_Invoices.UnpackingColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsOtherNull() => this.IsNull(this.tableDemo_Invoices.OtherColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetOtherNull() => this[this.tableDemo_Invoices.OtherColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsShuttleIndicatorNull() => this.IsNull(this.tableDemo_Invoices.ShuttleIndicatorColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetShuttleIndicatorNull() => this[this.tableDemo_Invoices.ShuttleIndicatorColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsShuttleOriginNull() => this.IsNull(this.tableDemo_Invoices.ShuttleOriginColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetShuttleOriginNull() => this[this.tableDemo_Invoices.ShuttleOriginColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsShuttleDestinationNull() => this.IsNull(this.tableDemo_Invoices.ShuttleDestinationColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetShuttleDestinationNull() => this[this.tableDemo_Invoices.ShuttleDestinationColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsThirdPartyChargesOriginNull() => this.IsNull(this.tableDemo_Invoices.ThirdPartyChargesOriginColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetThirdPartyChargesOriginNull() => this[this.tableDemo_Invoices.ThirdPartyChargesOriginColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsThirdPartyChargesDestinationNull() => this.IsNull(this.tableDemo_Invoices.ThirdPartyChargesDestinationColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetThirdPartyChargesDestinationNull() => this[this.tableDemo_Invoices.ThirdPartyChargesDestinationColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsDebrisPickUpNull() => this.IsNull(this.tableDemo_Invoices.DebrisPickUpColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetDebrisPickUpNull() => this[this.tableDemo_Invoices.DebrisPickUpColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsExtraLaborNull() => this.IsNull(this.tableDemo_Invoices.ExtraLaborColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetExtraLaborNull() => this[this.tableDemo_Invoices.ExtraLaborColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsExtraDeliveryNull() => this.IsNull(this.tableDemo_Invoices.ExtraDeliveryColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetExtraDeliveryNull() => this[this.tableDemo_Invoices.ExtraDeliveryColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsValuationNull() => this.IsNull(this.tableDemo_Invoices.ValuationColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetValuationNull() => this[this.tableDemo_Invoices.ValuationColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsMiscellaneousNull() => this.IsNull(this.tableDemo_Invoices.MiscellaneousColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetMiscellaneousNull() => this[this.tableDemo_Invoices.MiscellaneousColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsStorageIndicatorNull() => this.IsNull(this.tableDemo_Invoices.StorageIndicatorColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetStorageIndicatorNull() => this[this.tableDemo_Invoices.StorageIndicatorColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsSITDrayageInAmountNull() => this.IsNull(this.tableDemo_Invoices.SITDrayageInAmountColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetSITDrayageInAmountNull() => this[this.tableDemo_Invoices.SITDrayageInAmountColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsSITDeliveryOutAmountNull() => this.IsNull(this.tableDemo_Invoices.SITDeliveryOutAmountColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetSITDeliveryOutAmountNull() => this[this.tableDemo_Invoices.SITDeliveryOutAmountColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsStorageInDateNull() => this.IsNull(this.tableDemo_Invoices.StorageInDateColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetStorageInDateNull() => this[this.tableDemo_Invoices.StorageInDateColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsStorageOutDateNull() => this.IsNull(this.tableDemo_Invoices.StorageOutDateColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetStorageOutDateNull() => this[this.tableDemo_Invoices.StorageOutDateColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsNumberOfStorageDaysNull() => this.IsNull(this.tableDemo_Invoices.NumberOfStorageDaysColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetNumberOfStorageDaysNull() => this[this.tableDemo_Invoices.NumberOfStorageDaysColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsPermStorageNull() => this.IsNull(this.tableDemo_Invoices.PermStorageColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetPermStorageNull() => this[this.tableDemo_Invoices.PermStorageColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsCarHaulingNull() => this.IsNull(this.tableDemo_Invoices.CarHaulingColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetCarHaulingNull() => this[this.tableDemo_Invoices.CarHaulingColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsBookerCodeNull() => this.IsNull(this.tableDemo_Invoices.BookerCodeColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetBookerCodeNull() => this[this.tableDemo_Invoices.BookerCodeColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsHaulerCodeNull() => this.IsNull(this.tableDemo_Invoices.HaulerCodeColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetHaulerCodeNull() => this[this.tableDemo_Invoices.HaulerCodeColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsOriginAgentCodeNull() => this.IsNull(this.tableDemo_Invoices.OriginAgentCodeColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetOriginAgentCodeNull() => this[this.tableDemo_Invoices.OriginAgentCodeColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsDestinationAgentCodeNull() => this.IsNull(this.tableDemo_Invoices.DestinationAgentCodeColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetDestinationAgentCodeNull() => this[this.tableDemo_Invoices.DestinationAgentCodeColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsBatchIDNull() => this.IsNull(this.tableDemo_Invoices.BatchIDColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetBatchIDNull() => this[this.tableDemo_Invoices.BatchIDColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsClaimDateFiledNull() => this.IsNull(this.tableDemo_Invoices.ClaimDateFiledColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetClaimDateFiledNull() => this[this.tableDemo_Invoices.ClaimDateFiledColumn] = Convert.DBNull;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool IsClaimDateSettledNull() => this.IsNull(this.tableDemo_Invoices.ClaimDateSettledColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetClaimDateSettledNull() => this[this.tableDemo_Invoices.ClaimDateSettledColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsClaimAmountFiledNull() => this.IsNull(this.tableDemo_Invoices.ClaimAmountFiledColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetClaimAmountFiledNull() => this[this.tableDemo_Invoices.ClaimAmountFiledColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsClaimAmountSettledNull() => this.IsNull(this.tableDemo_Invoices.ClaimAmountSettledColumn);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public void SetClaimAmountSettledNull() => this[this.tableDemo_Invoices.ClaimAmountSettledColumn] = Convert.DBNull;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool IsTotalInvoiceAmountNull() => this.IsNull(this.tableDemo_Invoices.TotalInvoiceAmountColumn);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void SetTotalInvoiceAmountNull() => this[this.tableDemo_Invoices.TotalInvoiceAmountColumn] = Convert.DBNull;
  }

  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
  public class Demo_InvoicesRowChangeEvent : EventArgs
  {
    private AIS_DataSet.Demo_InvoicesRow eventRow;
    private DataRowAction eventAction;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public Demo_InvoicesRowChangeEvent(AIS_DataSet.Demo_InvoicesRow row, DataRowAction action)
    {
      this.eventRow = row;
      this.eventAction = action;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public AIS_DataSet.Demo_InvoicesRow Row => this.eventRow;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public DataRowAction Action => this.eventAction;
  }
}
