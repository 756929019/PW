using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PW.DBCommon.DBUtility
{
    public interface DBClass
    {
        /// <summary>
        /// 判断是否能连接数据库
        /// </summary>
        /// <returns></returns>
        bool DatabaseConnectBool();

        /// <summary>
        /// 判断是否能连接数据库
        /// </summary>
        /// <param name="strConnection"></param>
        /// <returns></returns>
        bool DatabaseConnectBool(string strConnection);

        ///<summary>
        ///执行查询语句，返回DataSet
        ///</summary>
        ///<param name="SQLString">查询语句</param>
        ///<returns>DataSet</returns>
        DataSet Query(string SQLString);

        /// <summary>
        /// 根据表名，查询条件查询指定的列项
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fields">列项</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        DataSet GetDataList(string tableName, string fields, string condition, string orderby);

        /// <summary>
        /// 根据表名，查询条件查询指定的列项
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fields">列项</param>
        /// <returns></returns>
        DataSet GetDataList(string tableName, string fields, string condition);

        /// <summary>
        /// 根据表名，查询条件查询指定的列项
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        DataSet GetDataList(string tableName, string condition);

        ///<summary>
        ///执行修改语句，返回影响条数
        ///</summary>
        ///<param name="SQLString">查询语句（insert,update,delete）</param>
        ///<returns>DataSet</returns>
        int ExecuteSqlModify(string SQLString);

        /// <summary>
        /// 修改某个表的数据
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strFields">字段要修改的值</param>
        /// <param name="strCondtion">修改条件</param>
        /// <returns></returns>
        int UpdateField(string strTableName, string strFields, string strCondtion);
        
        /// <summary>
        /// 增加某个表的数据
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strFields">字段</param>
        /// <param name="values">值</param>
        /// <returns></returns>
        int InsertField(string strTableName, string strFields, string values);

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int DeleteRecord(string tableName, string condition);

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>	
        bool ExecuteSqlTran(List<String> SQLStringList);
    }
}
