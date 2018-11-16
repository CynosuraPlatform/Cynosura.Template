<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:nu="http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd" version="1.0">
   <xsl:output method="xml" encoding="utf-8" indent="no" />
   <xsl:template match="/nu:package">
      <coreProperties xmlns="http://schemas.openxmlformats.org/package/2006/metadata/core-properties" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:dcterms="http://purl.org/dc/terms/" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
         <dc:creator>
            <xsl:value-of select="nu:metadata/nu:authors" />
         </dc:creator>
         <dc:description>
            <xsl:value-of select="nu:metadata/nu:description" />
         </dc:description>
         <dc:identifier>
            <xsl:value-of select="nu:metadata/nu:id" />
         </dc:identifier>
         <version>
            <xsl:value-of select="nu:metadata/nu:version" />
         </version>
         <keywords />
         <lastModifiedBy>NuGet, Version=4.7.1.5, Culture=neutral, PublicKeyToken=31bf3856ad364e35;Microsoft Windows NT 6.2.9200.0;.NET Framework 4.6</lastModifiedBy>
      </coreProperties>
   </xsl:template>
</xsl:stylesheet>

