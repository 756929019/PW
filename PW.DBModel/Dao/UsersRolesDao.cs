
  using PW.DBCommon.Model;
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Linq;
  using System.Linq.Expressions;

  namespace PW.DBCommon.Dao
  {
  
  public class UsersRolesDao : BaseDao
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<users_roles> query(users_roles record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                IQueryable<users_roles> db = myDb.users_roles;
                
                if (record.USER_ID != null)
                    
                {
                    db = db.Where<users_roles>(p => p.USER_ID.Equals(record.USER_ID));
                }
                
                if (record.ROLE_ID != null)
                    
                {
                    db = db.Where<users_roles>(p => p.ROLE_ID.Equals(record.ROLE_ID));
                }
                
                return db.ToList();
             }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public List<users_roles> queryPage(users_roles record)
        {
            int _total = 0;
            Expression<Func<users_roles, bool>> whereLambda = PredicateExtensions.True<users_roles>();
            
            if (record.USER_ID != null)
                
            {
                whereLambda.And(p => p.USER_ID.Equals(record.USER_ID));
            }
            
            if (record.ROLE_ID != null)
                
            {
                whereLambda.And(p => p.ROLE_ID.Equals(record.ROLE_ID));
            }
            
            return LoadPageItems(5, 2, out _total, whereLambda, p => p.USER_ID, true);
                
            // return LoadPageItems(5, 2, out _total, whereLambda, p => p.ROLE_ID, true);
                
            // return LoadPageItems(5, 2, out _total, whereLambda, p => p.id, true);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int deleteById(int id, int roleid)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                // TODO 生成代码后需要检查一下是否找到正确的主键，这里做一个错误代码，避免直接使用
                users_roles record = new users_roles() { USER_ID = id, ROLE_ID = roleid };
                    
                myDb.users_roles.Attach(record);
                myDb.Entry(record).State = EntityState.Deleted;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(users_roles record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.users_roles.Add(record);
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(users_roles record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.users_roles.Attach(record);
                myDb.Entry(record).State = EntityState.Modified;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// getById
        /// </summary>
        public users_roles getById(int id, int roleid)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                 // TODO 生成代码后需要检查一下是否找到正确的主键，这里做一个错误代码，避免直接使用
              
                 return myDb.Set<users_roles>().Where<users_roles>(p => p.USER_ID == id && p.ROLE_ID == roleid).FirstOrDefault<users_roles>();
                  
                 // return myDb.Set<users_roles>().Where<users_roles>(p => p.ROLE_ID == id).FirstOrDefault<users_roles>();
                  
            }
        }
    }
  }
