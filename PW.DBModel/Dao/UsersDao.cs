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
            using (qdbEntities myDb = new qdbEntities())
            {
                IQueryable<users> db = myDb.users; // var db = from s in qdb.Set<users>() select s;
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
            using (qdbEntities myDb = new qdbEntities())
            {
                users user = new users() { id = id };
                myDb.users.Attach(user);
                myDb.Entry(user).State = EntityState.Deleted;
                return myDb.SaveChanges();
            }
        }

        public int add(users user)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.users.Add(user);
                return myDb.SaveChanges();
            }
        }

        public int updateById(users user)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.users.Attach(user);
                myDb.Entry(user).State = EntityState.Modified;
                return myDb.SaveChanges();
            }
        }

        public users getById(int id)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                return myDb.Set<users>().Where<users>(p => p.id == id).FirstOrDefault<users>();
            }
        }

    }
}
