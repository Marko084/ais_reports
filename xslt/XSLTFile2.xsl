<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/">
  <html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:word' xmlns='http://www.w3.org/TR/REC-html40'>
    <head>
      <title>Time</title>
      <!--[if gte mso 9]><xml><w:WordDocument><w:View>Print</w:View><w:Zoom>90</w:Zoom><w:DoNotOptimizeForBrowser/></w:WordDocument></xml><![endif]-->
      <style>
        <!-- /* Style Definitions */ @page Section1   {size:6.0in 11.0in; 
               margin:1.0in 1.25in 1.0in 1.25in; mso-header-margin:.5in;
               mso-footer-margin:.5in; mso-paper-source:0;} div.Section1
                 {page:Section1;font-size:12.0pt;font-family:'Arial'}
               @list l0 {mso-list-id:367726253; mso-list-type:hybrid; 
               mso-list-template-ids:1573942220 -2102384016 67698691 67698693 67698689 67698691 67698693 67698689 67698691 67698693;} 
               @list l0:level1 	{mso-level-number-format:bullet; mso-level-text:\F06E;
               mso-level-tab-stop:.5in; mso-level-number-position:left; text-indent:-.25in;
               mso-ansi-font-size:16.0pt; mso-bidi-font-size:16.0pt; font-family:Wingdings;}
               ol{margin-bottom:0in;} ul{margin-bottom:0in;} -->
      </style>
    </head>
    <body lang='EN-US' style='tab-interval:.5in'>
      <div class='Section1'>
        <table border='1' cellpadding='1' cellspacing='1'>
          <tr>
            <td>DriverName</td>
            <td>CSCNo</td>
          </tr>
          <xsl:for-each select='//QualPerfDetailByDriver/Data'>
            <tr>
              <td><xsl:value-of select="DriverName"/></td>
              <td>
                <xsl:value-of select="CSCNo"/>
              </td>
            </tr>
            
          </xsl:for-each>
        </table>
      </div>
    </body>
  </html>
      </xsl:template>

</xsl:stylesheet> 

