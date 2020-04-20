<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:variable name="tableName" select="TableModel/TableName/node()" />
<xsl:template match="/" name="outputtemplate">
  using PW.DBCommon.Model;
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Linq;
  using System.Linq.Expressions;

  namespace PW.DBCommon.Dao
  {
  
  public class <xsl:value-of select="TableModel/ModelName"/>Dao : BaseDao
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<xsl:text disable-output-escaping="yes">&lt;</xsl:text><xsl:value-of select="TableModel/TableName"/><xsl:text disable-output-escaping="yes">&gt;</xsl:text> query(<xsl:value-of select="TableModel/TableName"/>&#160;record)
        {
            using (<xsl:value-of select="TableModel/DbContextName"/> myDb = new <xsl:value-of select="TableModel/DbContextName"/>())
            {
                IQueryable<xsl:text disable-output-escaping="yes">&lt;</xsl:text><xsl:value-of select="TableModel/TableName"/><xsl:text disable-output-escaping="yes">&gt;</xsl:text> db = myDb.<xsl:value-of select="TableModel/TableName"/>;
                <xsl:for-each select="TableModel/Fields/FieldModel">
                  <xsl:choose>
                    <xsl:when test="VarType='string'">
                if (!String.IsNullOrEmpty(record.<xsl:value-of select="FieldName"/>))
                    </xsl:when>
                    <xsl:otherwise>
                if (record.<xsl:value-of select="FieldName"/> != null)
                    </xsl:otherwise>
                  </xsl:choose>
                {
                    db = db.Where<xsl:text disable-output-escaping="yes">&lt;</xsl:text><xsl:copy-of select="$tableName" /><xsl:text disable-output-escaping="yes">&gt;</xsl:text>(p =<xsl:text disable-output-escaping="yes">&gt;</xsl:text> p.<xsl:value-of select="FieldName"/>.Equals(record.<xsl:value-of select="FieldName"/>));
                }
                </xsl:for-each>
                return db.ToList();
             }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public List<xsl:text disable-output-escaping="yes">&lt;</xsl:text><xsl:value-of select="TableModel/TableName"/><xsl:text disable-output-escaping="yes">&gt;</xsl:text> queryPage(<xsl:value-of select="TableModel/TableName"/>&#160;record)
        {
            int _total = 0;
            Expression<xsl:text disable-output-escaping="yes">&lt;Func&lt;</xsl:text><xsl:value-of select="TableModel/TableName"/>, bool<xsl:text disable-output-escaping="yes">&gt;&gt;</xsl:text> whereLambda = PredicateExtensions.True<xsl:text disable-output-escaping="yes">&lt;</xsl:text><xsl:value-of select="TableModel/TableName"/><xsl:text disable-output-escaping="yes">&gt;</xsl:text>();
            <xsl:for-each select="TableModel/Fields/FieldModel">
              <xsl:choose>
                <xsl:when test="VarType='string'">
            if (!String.IsNullOrEmpty(record.<xsl:value-of select="FieldName"/>))
                </xsl:when>
                <xsl:otherwise>
            if (record.<xsl:value-of select="FieldName"/> != null)
                </xsl:otherwise>
              </xsl:choose>
            {
                whereLambda.And(p =<xsl:text disable-output-escaping="yes">&gt;</xsl:text> p.<xsl:value-of select="FieldName"/>.Equals(record.<xsl:value-of select="FieldName"/>));
            }
            </xsl:for-each>
            return LoadPageItems(5, 2, out _total, whereLambda, p =<xsl:text disable-output-escaping="yes">&gt;</xsl:text> p.id, true);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int deleteById(int id)
        {
            using (<xsl:value-of select="TableModel/DbContextName"/> myDb = new <xsl:value-of select="TableModel/DbContextName"/>())
            {
                // TODO 生成代码后需要检查一下是否找到正确的主键，这里做一个错误代码，避免直接使用
                你好啊，确认sql条件后删除我
                <xsl:for-each select="TableModel/Fields/FieldModel">
                  <xsl:choose>
                    <xsl:when test="ColumnKey='PRI'">
                <xsl:copy-of select="$tableName" /> record = new <xsl:value-of select="TableModel/TableName"/>() { <xsl:value-of select="FieldName"/> = id };
                    </xsl:when>
                    <xsl:when test="FieldName='ID'">
                <xsl:copy-of select="$tableName" /> record = new <xsl:value-of select="TableModel/TableName"/>() { <xsl:value-of select="FieldName"/> = id };
                    </xsl:when>
                    <xsl:otherwise>
                    </xsl:otherwise>
                  </xsl:choose>
                </xsl:for-each>
                myDb.<xsl:value-of select="TableModel/TableName"/>.Attach(record);
                myDb.Entry(record).State = EntityState.Deleted;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        public <xsl:value-of select="TableModel/TableName"/> add(<xsl:value-of select="TableModel/TableName"/>&#160;record)
        {
            using (<xsl:value-of select="TableModel/DbContextName"/> myDb = new <xsl:value-of select="TableModel/DbContextName"/>())
            {
                myDb.<xsl:value-of select="TableModel/TableName"/>.Add(record);
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(<xsl:value-of select="TableModel/TableName"/>&#160;record)
        {
            using (<xsl:value-of select="TableModel/DbContextName"/> myDb = new <xsl:value-of select="TableModel/DbContextName"/>())
            {
                myDb.<xsl:value-of select="TableModel/TableName"/>.Attach(record);
                myDb.Entry(record).State = EntityState.Modified;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// getById
        /// </summary>
        public <xsl:value-of select="TableModel/TableName"/> getById(int id)
        {
            using (<xsl:value-of select="TableModel/DbContextName"/> myDb = new <xsl:value-of select="TableModel/DbContextName"/>())
            {
                 // TODO 生成代码后需要检查一下是否找到正确的主键，这里做一个错误代码，避免直接使用
                 你好啊，确认sql条件后删除我
              <xsl:for-each select="TableModel/Fields/FieldModel">
                <xsl:choose>
                  <xsl:when test="ColumnKey='PRI'">
                 return myDb.Set<xsl:text disable-output-escaping="yes">&lt;</xsl:text><xsl:copy-of select="$tableName" /><xsl:text disable-output-escaping="yes">&gt;</xsl:text>().Where<xsl:text disable-output-escaping="yes">&lt;</xsl:text><xsl:value-of select="TableModel/TableName"/><xsl:text disable-output-escaping="yes">&gt;</xsl:text>(p =<xsl:text disable-output-escaping="yes">&gt;</xsl:text> p.<xsl:value-of select="FieldName"/> == id).FirstOrDefault<xsl:text disable-output-escaping="yes">&lt;</xsl:text><xsl:value-of select="TableModel/TableName"/><xsl:text disable-output-escaping="yes">&gt;</xsl:text>();
                  </xsl:when>
                  <xsl:when test="FieldName='ID'">
                 return myDb.Set<xsl:text disable-output-escaping="yes">&lt;</xsl:text><xsl:copy-of select="$tableName" /><xsl:text disable-output-escaping="yes">&gt;</xsl:text>().Where<xsl:text disable-output-escaping="yes">&lt;</xsl:text><xsl:value-of select="TableModel/TableName"/><xsl:text disable-output-escaping="yes">&gt;</xsl:text>(p =<xsl:text disable-output-escaping="yes">&gt;</xsl:text> p.<xsl:value-of select="FieldName"/> == id).FirstOrDefault<xsl:text disable-output-escaping="yes">&lt;</xsl:text><xsl:value-of select="TableModel/TableName"/><xsl:text disable-output-escaping="yes">&gt;</xsl:text>();
                  </xsl:when>
                  <xsl:otherwise>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:for-each>
            }
        }
    }
  }
</xsl:template>
</xsl:stylesheet>