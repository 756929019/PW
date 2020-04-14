using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Tools.Views
{
    /// <summary>
    /// MySQL数据库操作
    /// author:hhm
    /// date:2012-2-22
    /// </summary>
    public class MySqlDbHelper
    {
        #region 私有变量
        private const string defaultConfigKeyName = "DbHelper";//连接字符串 默认Key
        private string connectionString;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数(DbHelper)
        /// </summary>
        public MySqlDbHelper()
        {
            this.connectionString = "server=localhost;User Id=root;password=123456;Database=qdb";
        }

        /// <summary>
        /// DbHelper构造函数
        /// </summary>
        /// <param name="keyName">连接字符串名</param>
        public MySqlDbHelper(string keyName)
        {
        }

        #endregion

        public int ExecuteNonQuery(string sql, params MySqlParameter[] parameters)
        {
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            foreach (MySqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
            int res = 0;
            try
            {
                res = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                res = -1;
            }
            cmd.Dispose();
            con.Close();
            return res;
        }

        public object ExecuteScalar(string sql, params MySqlParameter[] parameters)
        {
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            foreach (MySqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
            object res = cmd.ExecuteScalar();
            cmd.Dispose();
            con.Close();
            return res;
        }

        public DataTable ExecuteDataTable(string sql, params MySqlParameter[] parameters)
        {
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            if (parameters != null && parameters.Length > 0) {
                foreach (MySqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            DataSet dataset = new DataSet();//dataset放执行后的数据集合
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dataset);
            cmd.Dispose();
            con.Close();
            return dataset.Tables[0];
        }
    }
}
