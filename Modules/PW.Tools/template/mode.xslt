<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:template match="/" name="outputtemplate">
  using System;
  using System.Collections.Generic;

  namespace PW.Common.ViewModel
  {
  public class <xsl:value-of select="TableModel/ModelName"/>
    {
        <xsl:for-each select="TableModel/Fields/FieldModel">
        <xsl:choose>
        <xsl:when test="DefaultValueVar=''">
        private <xsl:value-of select="VarType"/> _<xsl:value-of select="VarNameLocal"/>;
        </xsl:when>
        <xsl:otherwise>
        private <xsl:value-of select="VarType"/> _<xsl:value-of select="VarNameLocal"/> = <xsl:value-of select="DefaultValueVar"/>;
        </xsl:otherwise>
        </xsl:choose>
        /// <summary>
        /// <xsl:value-of select="Mark"/> <xsl:number value="position()" />
        /// </summary>
        public <xsl:value-of select="VarType"/><xsl:text> </xsl:text><xsl:value-of select="VarName"/>
        {
            get
            {
                return _<xsl:value-of select="VarNameLocal"/>;
            }
            set
            {
                _<xsl:value-of select="VarNameLocal"/> = value;
            }
        }
        </xsl:for-each>
    }
}
</xsl:template>
</xsl:stylesheet>