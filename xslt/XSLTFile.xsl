<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:dt="urn:schemas-microsoft-com:datatypes" xmlns:user="urn:my-scripts">
  <xsl:output method="text" />
  <xsl:template match="Employee">
    <xsl:text>{\rtf1\fs22</xsl:text>
    <!--Print the Header Row-->
    <xsl:text>\trowd\cellx2500\cellx3000\cellx3800\cellx6800\cellx9144\intbl\keepn\ql\b Name\cell Age\cell Sex\cell Address\cell Department\cell</xsl:text>
    <xsl:text>\b0\ql0\intbl\row</xsl:text>
   
    <!--Print Employee Records-->
    <xsl:apply-templates select="Record" />
    <xsl:text>}</xsl:text>
  </xsl:template>
  
  <xsl:template match="Record">
    <xsl:text>\trowd\cellx2500\cellx3000\cellx3800\cellx6800\cellx9144\intbl\keepn\ql </xsl:text>
    
    <xsl:value-of select="EmpName" />
      <xsl:text>\cell </xsl:text>

    <xsl:value-of select="EmpAge" />
        <xsl:text>\cell </xsl:text>

    <xsl:if test="EmpSex='M'">

      <xsl:text>Male</xsl:text>

      <xsl:text>\cell </xsl:text>

    </xsl:if>

    <xsl:if test="EmpSex='F'">

      <xsl:text>Female</xsl:text>

      <xsl:text>\cell </xsl:text>

    </xsl:if>

    <xsl:value-of select="EmpAddress" />

    <xsl:text>\cell </xsl:text>

    <!--Print Employee Department-->

    <xsl:apply-templates select="Department" />

    <xsl:text>\cell</xsl:text>

    <xsl:text>\intbl\row</xsl:text>

  </xsl:template>

  <xsl:template match="Department">

    <xsl:value-of select="DeptName" />

  </xsl:template>

</xsl:stylesheet>



