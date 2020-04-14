using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.DBCommon.DBUtility
{
    public class DBBLL
    {
        static string connSqlStr = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        /// <summary>
        /// 年龄分布
        /// </summary>
        /// <returns></returns>
        public static DataTable AgeCnts()
        {
            try
            {
                DBClass db = new MYSQLDBClass(connSqlStr);
                String sql = "";
                sql += "SELECT SUBSTRING(CtfId,7,4) fyear,2018 - CONVERT(INT,SUBSTRING(CtfId,7,4)) age";
                sql += " ,SUM(CASE WHEN Gender = 'M' THEN 1 ELSE 0 END) cntm, SUM(CASE WHEN Gender = 'M' THEN 0 ELSE 1 END) cntf";
                sql += " FROM cdsgus";
                sql += " WHERE LEN(CtfId) = 18";
                sql += " AND SUBSTRING(CtfId,7,4)>'1950' AND SUBSTRING(CtfId,7,4) < '2015'";
                sql += " AND PATINDEX('%[^0-9]%',SUBSTRING(CtfId,7,4))=0";
                sql += " GROUP BY SUBSTRING(CtfId,7,4) ";
                sql += "";
                DataTable dt = db.Query(sql).Tables[0];
                return dt;
            }
            catch (Exception e) {
                return null;
            }
            
        }
        /// <summary>
        /// Addr分布
        /// </summary>
        /// <returns></returns>
        public static DataTable AddrCnts()
        {
            try { 
                DBClass db = new MYSQLDBClass(connSqlStr);
                String sql = "";
                sql += "SELECT * FROM addr_code T1 LEFT JOIN (";
                sql += " SELECT SUBSTRING(CtfId,1,2)  addr";
                sql += " ,SUM(CASE WHEN Gender = 'M' THEN 1 ELSE 0 END) cntm, SUM(CASE WHEN Gender = 'M' THEN 0 ELSE 1 END) cntf ";
                sql += "  FROM cdsgus WHERE LEN(CtfId) = 18 GROUP BY SUBSTRING(CtfId,1,2) ";
                sql += " ) T2 ON SUBSTRING(T1.code,1,2) = T2.addr";
                DataTable dt = db.Query(sql).Tables[0];
                return dt;
             }
            catch (Exception e) {
                return null;
            }
            
        }
        /// <summary>
        /// date分布
        /// </summary>
        /// <returns></returns>
        public static DataTable DateCnts()
        {
            try {
                DBClass db = new MYSQLDBClass(connSqlStr);
                String sql = "";
                sql += "SELECT datename(month,CONVERT(datetime,T1.version)) month,datename(weekday,CONVERT(datetime,T1.version)) weekday";
                sql += " ,SUM(CASE WHEN Gender = 'M' THEN 1 ELSE 0 END) cntm,SUM(CASE WHEN Gender = 'M' THEN 0 ELSE 1 END) cntf ";
                sql += " FROM [dbo].[cdsgus] T1 WHERE LEN(T1.version)>5";
                sql += " GROUP BY datename(month,CONVERT(datetime,T1.version)),datename(weekday,CONVERT(datetime,T1.version))";
                sql += " ORDER BY datename(month,CONVERT(datetime,T1.version)),datename(weekday,CONVERT(datetime,T1.version))";
                DataTable dt = db.Query(sql).Tables[0];
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static DataTable NameTags()
        {
            try {
                DBClass db = new MYSQLDBClass(connSqlStr);
                String sql = "";
                sql += "SELECT * FROM (SELECT TOP 50 * FROM (SELECT Name,COUNT(id) cnt FROM dbo.cdsgus WHERE LEN(Name)>1 GROUP BY Name) t ORDER BY cnt DESC) t";
                sql += " union all ";
                sql += "SELECT * FROM (SELECT TOP 30 * FROM (SELECT Name,COUNT(id) cnt FROM dbo.cdsgus WHERE LEN(Name)>2 GROUP BY Name) t ORDER BY cnt desc) t";
                DataTable dt = db.Query(sql).Tables[0];
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}
