using PW.DBCommon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PW.DBCommon.Dao
{
    public class BaseDao
    {

        /// <summary>
        /// 分页查询 + 条件查询 + 排序
        /// </summary>
        /// <typeparam name="Tkey">泛型</typeparam>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="total">总数量</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>IQueryable 泛型集合</returns>
        public List<T> LoadPageItems<T, TKey>(int pageSize, int pageIndex, out int total, Expression<Func<T, bool>> whereLambda, Func<T, TKey> orderbyLambda, bool isAsc) where T : class
        {
            using (qdbEntities qdb = new qdbEntities())
            {
                total = qdb.Set<T>().Where(whereLambda).Count();
                if (isAsc)
                {
                    var temp = qdb.Set<T>().Where(whereLambda)
                                 .OrderBy<T, TKey>(orderbyLambda)
                                 .Skip(pageSize * (pageIndex - 1))
                                 .Take(pageSize);
                    return temp.ToList();
                }
                else
                {
                    var temp = qdb.Set<T>().Where(whereLambda)
                               .OrderByDescending<T, TKey>(orderbyLambda)
                               .Skip(pageSize * (pageIndex - 1))
                               .Take(pageSize);
                    return temp.ToList();
                }
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        /// <param name="tableName">可以是表格（对应泛型T），也可以是sql</param>
        /// <returns></returns>
        public PageInfo<T> DBQueryPage<T>(PageInfo<T> page, string tableName, string where)
        {
            if (page == null)
            {
                return null;
            }
            using (qdbEntities qdb = new qdbEntities())
            {
                string totalSql = "select count(*) as recordCount from " + tableName + " T" + (String.IsNullOrEmpty(where) ? "" : " where " + where);
                int totalCount = qdb.Database.SqlQuery<int>(totalSql).FirstOrDefault();
                page.totalCount = totalCount;
                page.totalPage = (totalCount + page.pageSize - 1) / page.pageSize;
                string querySql = "select * from " + tableName + " T " + (String.IsNullOrEmpty(where) ? "" : " where " + where) + " " + (String.IsNullOrEmpty(page.orderName) ? "" : " order by " + page.orderName) + " limit " + ((page.pageIndex - 1) * page.pageSize) + "," + page.pageSize + "";
                List<T> list = qdb.Database.SqlQuery<T>(querySql).ToList();
                page.list = list;
                return page;
            }
        }
    }
}
