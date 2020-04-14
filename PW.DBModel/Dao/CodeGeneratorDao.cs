using MySql.Data.MySqlClient;
using PW.DBCommon.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace PW.DBCommon.Dao
{
    public class CodeGeneratorDao : BaseDao
    {
        public List<table> queryTables()
        {
            using (qdbEntities qdb = new qdbEntities())
            {
                MySqlParameter[] param = { new MySqlParameter("@schema", "qdb") };
                return qdb.Database.SqlQuery<table>("select table_name,table_comment from information_schema.tables where table_schema=@schema", param).ToList();
            }
        }

        public List<table> queryViews()
        {
            using (qdbEntities qdb = new qdbEntities())
            {
                MySqlParameter[] param = { new MySqlParameter("@schema", "qdb") };
                return qdb.Database.SqlQuery<table>("select table_name, '' as table_comment from information_schema.views where table_schema = @schema", param).ToList();
            }
        }

        public List<column> queryColumns(string tableName)
        {
            using (qdbEntities qdb = new qdbEntities())
            {
                MySqlParameter[] param = { new MySqlParameter("@schema", "qdb"), new MySqlParameter("@tableName", tableName) };
                return qdb.Database.SqlQuery<column>("select COLUMN_NAME,IS_NULLABLE,ORDINAL_POSITION,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH,COLUMN_KEY,COLUMN_COMMENT from information_schema.columns where table_schema = @schema and table_name = @tableName", param).ToList();
            }
        }
    }
}
