<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
							  xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
							  xmlns:xs="http://www.w3.org/2001/XMLSchema"  
						      xmlns:msdata="urn:schemas-microsoft-com:xml-msdata"  
						      xmlns:rd="http://schemas.microsoft.com/SQLServer/reporting/reportdesigner"  
				              xmlns="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition">
    
	<xsl:param name="ReportID"></xsl:param>
	<xsl:param name="ReportTitle"></xsl:param>
	<xsl:param name="DataSetName"></xsl:param>
	<xsl:param name="DataSourceID"></xsl:param>
	<xsl:param name="DataSourceName">aisConnectionString</xsl:param>
	<xsl:param name="ReportWidth">8.5</xsl:param>
	<xsl:param name="ReportHeight">11</xsl:param>
	<xsl:param name="Query"></xsl:param>
	<xsl:param name="TableName"></xsl:param>
	<xsl:param name="CompanyCode"></xsl:param>
	<xsl:param name="QueryType"></xsl:param>
	<xsl:variable name="mvarName" select="/xs:schema/@Name"/>
	<xsl:variable name="mvarFontSize">8pt</xsl:variable>
	<xsl:variable name="mvarFontWeight">500</xsl:variable>
	<xsl:variable name="mvarFontWeightBold">700</xsl:variable>
	<xsl:output method="xml" indent="yes" encoding="utf-8" />
    <xsl:template match="xs:sequence">
		<Report xmlns:rd="http://schemas.microsoft.com/SQLServer/reporting/reportdesigner" xmlns="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition">
			<DataSources>
				<DataSource Name="{$DataSourceName}">
					<rd:DataSourceID>
						<xsl:value-of select="$DataSourceID" />
					</rd:DataSourceID>
					<ConnectionProperties>
						<DataProvider />
						<ConnectString />
					</ConnectionProperties>
				</DataSource>
			</DataSources>
			<DataSets>
				<DataSet Name="{$DataSetName}">
					<Fields>
						<xsl:apply-templates select="xs:element" mode="Field">
						</xsl:apply-templates>
					</Fields>
					<Query>
						<DataSourceName>
							<xsl:value-of select="$DataSourceName" />
						</DataSourceName>
						<CommandText>
							<xsl:value-of select="$Query" />
						</CommandText>
						<rd:UseGenericDesigner>true</rd:UseGenericDesigner>
					</Query>
					<rd:DataSetInfo>
						<rd:DataSetName>AIS_DataSet</rd:DataSetName>
						<rd:TableName>Demo_Invoices</rd:TableName>
						<rd:TableAdapterName>Demo_InvoicesTableAdapter</rd:TableAdapterName>
						<rd:TableAdapterFillMethod>Fill</rd:TableAdapterFillMethod>
						<rd:TableAdapterGetDataMethod>GetData</rd:TableAdapterGetDataMethod>
					</rd:DataSetInfo>
				</DataSet>
			</DataSets>
			<rd:ReportID>
				<xsl:value-of select="$ReportID" />
			</rd:ReportID>
			<InteractiveHeight>11in</InteractiveHeight>
			<rd:DrawGrid>true</rd:DrawGrid>
			<InteractiveWidth>17in</InteractiveWidth>
			<rd:SnapToGrid>true</rd:SnapToGrid>
			<RightMargin>1in</RightMargin>
			<LeftMargin>1in</LeftMargin>
			<PageHeader>
				<PrintOnFirstPage>true</PrintOnFirstPage>
				<ReportItems>
					<Textbox Name="txtPageHeader">
						<rd:DefaultName>txtPageHeader</rd:DefaultName>
						<Style>
							<FontFamily>Tahoma</FontFamily>
							<FontSize>18pt</FontSize>
							<PaddingLeft>2pt</PaddingLeft>
							<PaddingRight>2pt</PaddingRight>
							<PaddingTop>2pt</PaddingTop>
							<PaddingBottom>2pt</PaddingBottom>
						</Style>
						<CanGrow>true</CanGrow>
						<Height>0.33in</Height>
						<Value>
							<xsl:value-of select="$ReportTitle" />
						</Value>
					</Textbox>
				</ReportItems>
				<Height>0.375in</Height>
				<PrintOnLastPage>true</PrintOnLastPage>
			</PageHeader>
			<BottomMargin>1in</BottomMargin>
			<Width>
				<xsl:value-of select="$ReportWidth" />
				<xsl:text>in</xsl:text>
			</Width>
			<Body>
				<Height>
					<xsl:value-of select="$ReportHeight" />
					<xsl:text>in</xsl:text>
				</Height>
				<ColumnSpacing>0.5in</ColumnSpacing>
				<ReportItems>
					<Table Name="table1">
						<DataSetName>
							<xsl:value-of select="$DataSetName" />
						</DataSetName>
						<Top>0.5in</Top>
						<Height>0.50in</Height>
						<Header>
							<TableRows>
								<TableRow>
									<Height>0.25in</Height>
									<TableCells>
										<xsl:apply-templates select="xs:element" mode="HeaderTableCell" />
										<xsl:if test="$QueryType='XXX'">
										<TableCell>
											<ReportItems>
												<Textbox Name="textboxDetail">
													<rd:DefaultName>textboxDetail</rd:DefaultName>
													<Value>Detail</Value>
													<CanGrow>true</CanGrow>
													<ZIndex>7</ZIndex>
													<Style>
														<TextAlign>Center</TextAlign>
														<PaddingLeft>2pt</PaddingLeft>
														<PaddingBottom>2pt</PaddingBottom>
														<PaddingRight>2pt</PaddingRight>
														<PaddingTop>2pt</PaddingTop>
														<FontSize>
															<xsl:value-of select="$mvarFontSize"/>
														</FontSize>
														<FontWeight>
															<xsl:value-of select="$mvarFontWeightBold"/>
														</FontWeight>
														<BackgroundColor>Navy</BackgroundColor>
														<Color>#ffffff</Color>
														<BorderColor>
															<Default>#ffffff</Default>
														</BorderColor>
														<BorderStyle>
															<Default>Solid</Default>
														</BorderStyle>
													</Style>
												</Textbox>
											</ReportItems>
										</TableCell>
										</xsl:if>
									</TableCells>
								</TableRow>
							</TableRows>
						</Header>
						<Details>
							<TableRows>
								<TableRow>
									<Height>0.25in</Height>
									<TableCells>
										<xsl:apply-templates select="xs:element" mode="DetailTableCell">
										</xsl:apply-templates>
										<xsl:if test="$QueryType='XXX'">
											<TableCell>
												<ReportItems>
													<Textbox Name="DetailLink">
														<rd:DefaultName>DetailLink</rd:DefaultName>
														<Action>
															<Hyperlink>
																http://reports.aismgt.com<xsl:text>/</xsl:text><xsl:value-of select="$CompanyCode"/><xsl:text>/</xsl:text>SurveyDetail.aspx?cscno<xsl:text>=</xsl:text>=Fields!CSCNo.Value
															</Hyperlink>
														</Action>
														<Value>Details</Value>
														<CanGrow>true</CanGrow>
														<ZIndex>7</ZIndex>
														<Style>
															<TextAlign>Left</TextAlign>
															<PaddingLeft>2pt</PaddingLeft>
															<PaddingBottom>2pt</PaddingBottom>
															<PaddingRight>2pt</PaddingRight>
															<PaddingTop>2pt</PaddingTop>
															<FontSize>
																<xsl:value-of select="$mvarFontSize"/>
															</FontSize>
															<FontWeight>
																<xsl:value-of select="$mvarFontWeight"/>
															</FontWeight>
															<BackgroundColor>#e0e0e0</BackgroundColor>
															<Color>#000000</Color>
															<BorderColor>
																<Default>#ffffff</Default>
															</BorderColor>
															<BorderStyle>
																<Default>Solid</Default>
															</BorderStyle>
														</Style>
													</Textbox>
												</ReportItems>
											</TableCell>
										</xsl:if>
									</TableCells>
								</TableRow>
							</TableRows>
						</Details>
						<TableColumns>
							<xsl:apply-templates select="xs:element" mode="TableColumn">
							</xsl:apply-templates>
							<xsl:if test="$QueryType='XXX'">
								<TableColumn>
									<Width>1.5in</Width>
								</TableColumn>
							</xsl:if>
						</TableColumns>
					</Table>
				</ReportItems>
			</Body>
		</Report>
    </xsl:template>

	<xsl:template match="xs:element" mode="Field">
		<xsl:variable name="varFieldName">
			<xsl:value-of select="@name" />
		</xsl:variable>

		<xsl:variable name="varDataType">
			<xsl:choose>
				<xsl:when test="@type='xs:int'">System.Int32</xsl:when>
				<xsl:when test="@type='xs:string'">System.String</xsl:when>
				<xsl:when test="@type='xs:dateTime'">System.DateTime</xsl:when>
				<xsl:when test="@type='xs:boolean'">System.Boolean</xsl:when>
				<xsl:when test="@type='xs:decimal'">System.Decimal</xsl:when>
				<xsl:when test="@type='xs:double'">System.Double</xsl:when>
			</xsl:choose>
		</xsl:variable>

		<Field Name="{$varFieldName}">
			<rd:TypeName>
				<xsl:value-of select="$varDataType"/>
			</rd:TypeName>
			<DataField>
				<xsl:value-of select="$varFieldName"/>
			</DataField>
		</Field>
	</xsl:template>

	<xsl:template match="xs:element" mode="DetailTableCell">
		<xsl:variable name="varFieldName">
			<xsl:value-of select="@name" />
		</xsl:variable>

		<TableCell>
			<ReportItems>
				<Textbox Name="{$varFieldName}">
					<rd:DefaultName>
						<xsl:value-of select="$varFieldName"/>
					</rd:DefaultName>
					<Value>=Fields!<xsl:value-of select="$varFieldName"/>.Value
					</Value>
					<CanGrow>true</CanGrow>
					<ZIndex>7</ZIndex>
					<Style>
						<TextAlign>Left</TextAlign>
						<PaddingLeft>2pt</PaddingLeft>
						<PaddingBottom>2pt</PaddingBottom>
						<PaddingRight>2pt</PaddingRight>
						<PaddingTop>2pt</PaddingTop>
						<FontSize>
							<xsl:value-of select="$mvarFontSize"/>
						</FontSize>
						<FontWeight>
							<xsl:value-of select="$mvarFontWeight"/>
						</FontWeight>
						<BackgroundColor>#e0e0e0</BackgroundColor>
						<Color>#000000</Color>
						<BorderColor>
							<Default>#ffffff</Default>
						</BorderColor>
						<BorderStyle>
							<Default>Solid</Default>
						</BorderStyle>
					</Style>
				</Textbox>
			</ReportItems>
		</TableCell>
	</xsl:template>

	<xsl:template match="xs:element" mode="TableColumn">
		<TableColumn>
			<Width>1.5in</Width>
		</TableColumn>
	</xsl:template>

	<xsl:template match="xs:element" mode="HeaderTableCell">
		<xsl:variable name="varFieldName">
			<xsl:value-of select="@name" />
		</xsl:variable>
		<TableCell>
			<ReportItems>
				<Textbox Name="textbox{position()}">
					<rd:DefaultName>textbox<xsl:value-of select="position()"/></rd:DefaultName>
					<Value>
						<xsl:value-of select="$varFieldName"/>
					</Value>
					<CanGrow>true</CanGrow>
					<ZIndex>7</ZIndex>
					<Style>
						<TextAlign>Center</TextAlign>
						<PaddingLeft>2pt</PaddingLeft>
						<PaddingBottom>2pt</PaddingBottom>
						<PaddingRight>2pt</PaddingRight>
						<PaddingTop>2pt</PaddingTop>
						<FontSize>
							<xsl:value-of select="$mvarFontSize"/>
						</FontSize>
						<FontWeight>
							<xsl:value-of select="$mvarFontWeightBold"/>
						</FontWeight>
						<BackgroundColor>Navy</BackgroundColor>
						<Color>#ffffff</Color>
						<BorderColor>
							<Default>#ffffff</Default>
						</BorderColor>
						<BorderStyle>
							<Default>Solid</Default>
						</BorderStyle>
					</Style>
				</Textbox>
			</ReportItems>
		</TableCell>
	</xsl:template>

</xsl:stylesheet>
