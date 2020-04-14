using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace PW.DBCommon.DBUtility
{
    public class MYSQLDBClass:DBClass
    {
        /**
         * 
         *使用前测试mysql ExecuteSqlTran方法中ExecuteNonQuery执行insert,update,delete返回影响条数，执行select或者发生回滚返回是否为-1
         *如果是，修改为
         * if (SQLStringList[n].Trim().Substring(0, 6).ToUpperInvariant().Contains("SELECT"))
                        {
                            //如果是select,正常统计执行成功条数,count加1， 
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SQLStringList[n]);
                            count += 1;
                        }
                        else
                        {
                            count += SqlHelper.ExecuteNonQuery(trans, CommandType.Text, SQLStringList[n]);  
                        }
         * 
         * for 石书伟
         * 
         * */
        #region
        public static string connStr = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        #endregion

        protected MySqlConnection Connection;
        public MYSQLDBClass()
        {
            Connection = new MySqlConnection(connStr);
        }

        public MYSQLDBClass(string constr)
        {
            connStr = constr;
            Connection = new MySqlConnection(connStr);
        }

        /// <summary>
        /// 判断是否能连接数据库
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public bool DatabaseConnectBool()
        {
            #region 判断是否能连接数据库
            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (MySql.Data.MySqlClient.MySqlException e)
                {
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
            #endregion
        }

        /// <summary>
        /// 判断是否能连接数据库
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public bool DatabaseConnectBool(string strConnection)
        {
            #region 判断是否能连接数据库
            using (MySqlConnection connection = new MySqlConnection(strConnection))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
            #endregion
        }

        #region 查询
        ///<summary>
        ///执行查询语句，返回DataSet
        ///</summary>
        ///<param name="SQLString">查询语句</param>
        ///<returns>DataSet</returns>
        public DataSet Query(string SQLString)
        {
            return MySqlHelper.ExecuteDataset(connStr, SQLString);
        }

        /// <summary>
        /// 根据表名，查询条件查询指定的列项
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fields">列项</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public DataSet GetDataList(string tableName, string fields, string condition, string orderby)
        {
            string sql = string.Format("SELECT {0} FROM {1} WHERE {2} ORDER BY {3}", fields, tableName, condition, orderby);
            return MySqlHelper.ExecuteDataset(connStr, sql, null);
        }
        public DataSet GetDataList(string tableName, string fields, string condition)
        {
            string sql = string.Format("SELECT {0} FROM {1} WHERE {2}", fields, tableName, condition);
            return MySqlHelper.ExecuteDataset(connStr, sql, null);
        }
        public DataSet GetDataList(string tableName, string condition)
        {
            string sql = string.Format("SELECT * FROM {0} WHERE {1}", tableName, condition);
            return MySqlHelper.ExecuteDataset(connStr, sql, null);
        }
        #endregion

        ///<summary>
        ///执行修改语句，返回影响条数
        ///</summary>
        ///<param name="SQLString">查询语句（insert,update,delete）</param>
        ///<returns>DataSet</returns>
        public int ExecuteSqlModify(string SQLString)
        {
            return MySqlHelper.ExecuteNonQuery(Connection, SQLString);
        }

        #region 修改
        /// <summary>
        /// 修改某个表的数据
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strFields">字段要修改的值</param>
        /// <param name="strCondtion">修改条件</param>
        /// <returns></returns>
        public int UpdateField(string strTableName, string strFields, string strCondtion)
        {
            string sql = string.Format("update {0} set {1} where {2}", strTableName, strFields, strCondtion);
            return MySqlHelper.ExecuteNonQuery(Connection, sql);
        }
        #endregion

        #region 增加
        /// <summary>
        /// 增加某个表的数据
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strFields">字段</param>
        /// <param name="values">值</param>
        /// <returns></returns>
        public int InsertField(string strTableName, string strFields, string values)
        {
            string sql = string.Format("insert into {0}({1}) values ({2}) ", strTableName, strFields, values);
            return MySqlHelper.ExecuteNonQuery(Connection, sql);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int DeleteRecord(string tableName, string condition)
        {
            string sql = string.Format("delete from {0} where {1}", tableName, condition);
            return MySqlHelper.ExecuteNonQuery(Connection, sql);
        }
        #endregion
        	
        public bool ExecuteSqlTran(List<String> SQLStringList)
        {
            #region 执行多条SQL语句，实现数据库事务
            Connection.Open();
            using (MySqlTransaction trans = Connection.BeginTransaction())
            {
                try
                {
                    int count = 0;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        count += MySqlHelper.ExecuteNonQuery(Connection, SQLStringList[n]);  
                    }
                    if (count == SQLStringList.Count)
                    {
                        trans.Commit();
                        return true;
                    }
                    else
                    {
                        trans.Rollback();
                        return false;
                    }
                }
                catch
                {

                    trans.Rollback();
                    return false;
                }
                finally
                {
                    Connection.Close();
                }
            }
            #endregion
        }
    }
}
