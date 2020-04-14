<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:template match="/" name="outputtemplate">
  using PW.DBCommon.Model;
  using System.Collections.Generic;
  using System.ServiceModel;

  namespace PW.Service
  {
    [ServiceContract]
    public interface IService<xsl:value-of select="TableModel/ModelName"/>
    {
        /// <summary>
        /// 查询
        /// </summary>
        [OperationContract]
        List<xsl:text disable-output-escaping="yes">&lt;</xsl:text><xsl:value-of select="TableModel/TableName"/><xsl:text disable-output-escaping="yes">&gt;</xsl:text> query(<xsl:value-of select="TableModel/TableName"/>&#160;record);

        /// <summary>
        /// 分页查询
        /// </summary>
        [OperationContract]
        List<xsl:text disable-output-escaping="yes">&lt;</xsl:text><xsl:value-of select="TableModel/TableName"/><xsl:text disable-output-escaping="yes">&gt;</xsl:text> queryPage(<xsl:value-of select="TableModel/TableName"/>&#160;record);

        /// <summary>
        /// 删除
        /// </summary>
        [OperationContract]
        <xsl:value-of select="TableModel/TableName"/> deleteById(int id);

        /// <summary>
        /// 添加
        /// </summary>
        [OperationContract]
        <xsl:value-of select="TableModel/TableName"/> add(<xsl:value-of select="TableModel/TableName"/>&#160;record);

        /// <summary>
        /// 更新
        /// </summary>
        [OperationContract]
        int updateById(<xsl:value-of select="TableModel/TableName"/>&#160;record);

        /// <summary>
        /// getById
        /// </summary>
        [OperationContract]
        <xsl:value-of select="TableModel/TableName"/> getById(int id);
    }
  }
</xsl:template>
</xsl:stylesheet>