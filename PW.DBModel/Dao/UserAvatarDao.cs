
  using PW.DBCommon.Model;
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Linq;
  using System.Linq.Expressions;

  namespace PW.DBCommon.Dao
  {
  
  public class UserAvatarDao : BaseDao
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<user_avatar> query(user_avatar record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                IQueryable<user_avatar> db = myDb.user_avatar;
                
                if (record.ID != null)
                    
                {
                    db = db.Where<user_avatar>(p => p.ID.Equals(record.ID));
                }
                
                if (!String.IsNullOrEmpty(record.REAL_NAME))
                    
                {
                    db = db.Where<user_avatar>(p => p.REAL_NAME.Equals(record.REAL_NAME));
                }
                
                if (!String.IsNullOrEmpty(record.PATH))
                    
                {
                    db = db.Where<user_avatar>(p => p.PATH.Equals(record.PATH));
                }
                
                if (!String.IsNullOrEmpty(record.SIZE))
                    
                {
                    db = db.Where<user_avatar>(p => p.SIZE.Equals(record.SIZE));
                }
                
                if (record.CREATE_TIME != null)
                    
                {
                    db = db.Where<user_avatar>(p => p.CREATE_TIME.Equals(record.CREATE_TIME));
                }
                
                return db.ToList();
             }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public List<user_avatar> queryPage(user_avatar record)
        {
            int _total = 0;
            Expression<Func<user_avatar, bool>> whereLambda = PredicateExtensions.True<user_avatar>();
            
            if (record.ID != null)
                
            {
                whereLambda.And(p => p.ID.Equals(record.ID));
            }
            
            if (!String.IsNullOrEmpty(record.REAL_NAME))
                
            {
                whereLambda.And(p => p.REAL_NAME.Equals(record.REAL_NAME));
            }
            
            if (!String.IsNullOrEmpty(record.PATH))
                
            {
                whereLambda.And(p => p.PATH.Equals(record.PATH));
            }
            
            if (!String.IsNullOrEmpty(record.SIZE))
                
            {
                whereLambda.And(p => p.SIZE.Equals(record.SIZE));
            }
            
            if (record.CREATE_TIME != null)
                
            {
                whereLambda.And(p => p.CREATE_TIME.Equals(record.CREATE_TIME));
            }
            
            return LoadPageItems(5, 2, out _total, whereLambda, p => p.ID, true);
                
            // return LoadPageItems(5, 2, out _total, whereLambda, p => p.id, true);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int deleteById(int id)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                // TODO 生成代码后需要检查一下是否找到正确的主键，这里做一个错误代码，避免直接使用
                user_avatar record = new user_avatar() { ID = id };
                    
                myDb.user_avatar.Attach(record);
                myDb.Entry(record).State = EntityState.Deleted;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(user_avatar record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.user_avatar.Add(record);
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(user_avatar record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.user_avatar.Attach(record);
                myDb.Entry(record).State = EntityState.Modified;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// getById
        /// </summary>
        public user_avatar getById(int id)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                 // TODO 生成代码后需要检查一下是否找到正确的主键，这里做一个错误代码，避免直接使用
              
                 return myDb.Set<user_avatar>().Where<user_avatar>(p => p.ID == id).FirstOrDefault<user_avatar>();
                  
            }
        }
    }
  }
