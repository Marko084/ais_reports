<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/">
  <xsl:text><!DOCTYPE ITEXT SYSTEM "itext.dtd"></xsl:text>
  <itext>
    <table columns="4" width="100%" align="Center" cellpadding="1.0" cellspacing="1.0" widths="25" borderwidth="1.0" left="true" right="true" top="true" bottom="true" red="255" green="255" blue="255">
      <xsl:for-each select="//Data">
        <row>
          <cell>
            <paragraph leading="18.0" font="unknown" align="Default">
              <xsl:value-of select="DriverName"/>
            </paragraph>
          </cell>
          <cell>
            <paragraph leading="18.0" font="unknown" align="Default">
              <xsl:value-of select="CSCNo"/>
            </paragraph>
          </cell>
          <cell>
            <paragraph leading="18.0" font="unknown" align="Default">
              <xsl:value-of select="TransfereeName"/>
            </paragraph>
          </cell>
          <cell>
            <paragraph leading="18.0" font="unknown" align="Default">
              <xsl:value-of select="AccountName"/>
            </paragraph>
          </cell>
        </row>
      </xsl:for-each>
    </table>
  </itext>
  </xsl:template>

</xsl:stylesheet>

