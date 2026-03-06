<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/">
    <html>
		<head>
			<title>AIS Survey Detail - Nelson Westerberg</title>
			<style>
				<xsl:text>
				.aisname{font-family:tahoma,arial; font-size:10pt;font-weight:bold;color:#8E3C3E;}
				.aistitle{font-family:tahoma,arial; font-size:14pt;font-weight:bold;}
				.aisdescription{font-family:tahoma,arial; font-size:10px;}
				.aisquestions{font-family:tahoma,arial; font-size:10px;font-weight:bold;}
				.aisanswers{font-family:tahoma,arial; font-size:10px;}
				</xsl:text>
			</style>
		</head>
    <body>
		<table border="0" cellpadding="0" cellspacing="0" width="600">
			<tr>
				<td>
					<img id="imgLogo" src="images/asi_logo_sm.jpg" style="border-width:0px;" />
				</td>
				<td class="aisname">
					<xsl:text>Audit &amp;</xsl:text><br />
					<xsl:text>Information</xsl:text><br />
					<xsl:text>Services,</xsl:text><br />
					<xsl:text>Inc.</xsl:text>
				</td>
				<td align="center" class="aistitle">POST-MOVE EVALUATION SURVEY</td>
			</tr>
		</table>
		<table border="0" cellpadding="5" cellspacing="5" width="550">
			<tr>
				<td>
					<i class="aisdescription">
						<xsl:text>
						In an effort to measure the quality of service provided during your recent relocation with
						Nelson Westerberg, Inc., please take a few minutes to complete this evaluation. Your opinion is highly valued, and your comments are greatly appreciated.
						</xsl:text>
					</i>
				</td>
			</tr>
		</table>
		<table border="0" cellpadding="2" cellspacing="2" width="550">
			<tr>
				<td class="aisquestions">Name (Last, First):</td>
				<td>
					<span id="lblTransferee" class="aisanswers">GARDENHIRE, TERRY</span>
				</td>
				<td class="aisquestions">Origin (City, State):</td>
				<td>
					<span id="lblOriginAgent" class="aisanswers">1511</span>
				</td>
			</tr>
			<tr>
				<td class="aisquestions">Account Name:</td>
				<td>
					<span id="lblAccountName" class="aisanswers">213029-01</span>
				</td>
				<td class="aisquestions">Destination (City, State):</td>
				<td>
					<span id="lblDestinationAgent" class="aisanswers">1873</span>
				</td>
			</tr>
			<tr>
				<td class="aisquestions">CSC#:</td>
				<td>
					<span id="lblCSCNo" class="aisanswers">219379</span>
				</td>
				<td class="aisquestions">Current Phone #:</td>
				<td>
					<span id="Label5" class="aisanswers"></span>
				</td>
			</tr>
			<tr>
				<td class="aisquestions">Reg #:</td>
				<td>
					<span id="Label6" class="aisanswers"></span>
				</td>
				<td></td>
				<td>
					<span id="Label7" class="aisanswers"></span>
				</td>
			</tr>
		</table>
    </body>
    </html>
</xsl:template>

</xsl:stylesheet> 

