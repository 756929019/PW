<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:template match="/" name="outputtemplate">
  using PW.DBCommon.Dao;
  using PW.DBCommon.Model;
  using System.Collections.Generic;

  namespace PW.Service
  {
    public class Service<xsl:value-of select="TableModel/ModelName"/> : IService<xsl:value-of select="TableModel/ModelName"/>
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<xsl:text disable-output-escaping="yes">&lt;</xsl:text><xsl:value-of select="TableModel/TableName"/><xsl:text disable-output-escaping="yes">&gt;</xsl:text> query(<xsl:value-of select="TableModel/TableName"/>&#160;record)
        {
            return new <xsl:value-of select="TableModel/ModelName"/>Dao().query(record);
        }
 
        /// <summary>
        /// 分页查询
        /// </summary>
        public List<xsl:text disable-output-escaping="yes">&lt;</xsl:text><xsl:value-of select="TableModel/TableName"/><xsl:text disable-output-escaping="yes">&gt;</xsl:text> queryPage(<xsl:value-of select="TableModel/TableName"/>&#160;record)
        {
            return new <xsl:value-of select="TableModel/ModelName"/>Dao().queryPage(record);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int deleteById(int id)
        {
            return new <xsl:value-of select="TableModel/ModelName"/>Dao().deleteById(id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public <xsl:value-of select="TableModel/TableName"/> add(<xsl:value-of select="TableModel/TableName"/>&#160;record)
        {
            return new <xsl:value-of select="TableModel/ModelName"/>Dao().add(record);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(<xsl:value-of select="TableModel/TableName"/>&#160;record)
        {
            return new <xsl:value-of select="TableModel/ModelName"/>Dao().updateById(record);
        }

        /// <summary>
        /// getById
        /// </summary>
        public <xsl:value-of select="TableModel/TableName"/> getById(int id)
        {
            return new <xsl:value-of select="TableModel/ModelName"/>Dao().getById(id);
        }
    }
  }
</xsl:template>
</xsl:stylesheet>