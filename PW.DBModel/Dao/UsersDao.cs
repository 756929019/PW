using PW.DBCommon.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PW.DBCommon.Dao
{
    public class UsersDao : BaseDao
    {
        public List<users> query(users user)
        {
            using (qdbEntities qdb = new qdbEntities())
            {
                IQueryable<users> db = qdb.users; // var db = from s in qdb.Set<users>() select s;
                if (!String.IsNullOrEmpty(user.username))
                {
                    db = db.Where<users>(p => p.username.Contains(user.username));
                }
                if (!String.IsNullOrEmpty(user.userno))
                {
                    db = db.Where<users>(p => p.userno.Contains(user.userno));
                }
                return db.ToList();
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<users> queryPage(users user)
        {
            //TODO:这里传递的参数不能是users ,需要封装pageinfo信息（pagesize,pageindex,order desc,where） 返回数据类型也要修改,需要包含总条数
            int _total = 0;
            Expression<Func<users, bool>> whereLambda = PredicateExtensions.True<users>();
            if (!String.IsNullOrEmpty(user.username))
            {
                whereLambda.And(p => p.username.Contains(user.username));
            }
            if (!String.IsNullOrEmpty(user.userno))
            {
                whereLambda.And(p => p.userno.Contains(user.userno));
            }

            return LoadPageItems(5, 2, out _total, whereLambda, p => p.id, true);
        }

        public int deleteById(int id)
        {
            using (qdbEntities qdb = new qdbEntities())
            {
                users user = new users() { id = id };
                qdb.users.Attach(user);
                qdb.Entry(user).State = EntityState.Deleted;
                return qdb.SaveChanges();
            }
        }

        public int add(users user)
        {
            using (qdbEntities qdb = new qdbEntities())
            {
                qdb.users.Add(user);
                return qdb.SaveChanges();
            }
        }

        public int updateById(users user)
        {
            using (qdbEntities qdb = new qdbEntities())
            {
                qdb.users.Attach(user);
                qdb.Entry(user).State = EntityState.Modified;
                return qdb.SaveChanges();
            }
        }

        public users getById(int id)
        {
            using (qdbEntities qdb = new qdbEntities())
            {
                return qdb.Set<users>().Where<users>(p => p.id == id).FirstOrDefault<users>();
            }
        }

    }
}
