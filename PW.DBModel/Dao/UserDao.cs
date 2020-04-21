
  using PW.DBCommon.Model;
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Linq;
  using System.Linq.Expressions;

  namespace PW.DBCommon.Dao
  {
  
  public class UserDao : BaseDao
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<user> query(user record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                IQueryable<user> db = myDb.user;
                if (record != null) {
                    if (record.ID != null)

                    {
                        db = db.Where<user>(p => p.ID.Equals(record.ID));
                    }

                    if (record.AVATAR_ID != null)

                    {
                        db = db.Where<user>(p => p.AVATAR_ID.Equals(record.AVATAR_ID));
                    }

                    if (!String.IsNullOrEmpty(record.EMAIL))

                    {
                        db = db.Where<user>(p => p.EMAIL.Equals(record.EMAIL));
                    }

                    if (record.ENABLED != null)

                    {
                        db = db.Where<user>(p => p.ENABLED.Equals(record.ENABLED));
                    }

                    if (!String.IsNullOrEmpty(record.PASSWORD))

                    {
                        db = db.Where<user>(p => p.PASSWORD.Equals(record.PASSWORD));
                    }

                    if (!String.IsNullOrEmpty(record.USERNAME))

                    {
                        db = db.Where<user>(p => p.USERNAME.Equals(record.USERNAME));
                    }

                    if (record.DEPT_ID != null)

                    {
                        db = db.Where<user>(p => p.DEPT_ID.Equals(record.DEPT_ID));
                    }

                    if (!String.IsNullOrEmpty(record.PHONE))

                    {
                        db = db.Where<user>(p => p.PHONE.Equals(record.PHONE));
                    }

                    if (record.JOB_ID != null)

                    {
                        db = db.Where<user>(p => p.JOB_ID.Equals(record.JOB_ID));
                    }

                    if (record.CREATE_TIME != null)

                    {
                        db = db.Where<user>(p => p.CREATE_TIME.Equals(record.CREATE_TIME));
                    }

                    if (record.LAST_PASSWORD_RESET_TIME != null)

                    {
                        db = db.Where<user>(p => p.LAST_PASSWORD_RESET_TIME.Equals(record.LAST_PASSWORD_RESET_TIME));
                    }

                    if (!String.IsNullOrEmpty(record.NICK_NAME))

                    {
                        db = db.Where<user>(p => p.NICK_NAME.Equals(record.NICK_NAME));
                    }

                    if (!String.IsNullOrEmpty(record.SEX))

                    {
                        db = db.Where<user>(p => p.SEX.Equals(record.SEX));
                    }
                }
                return db.ToList();
             }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public List<user> queryPage(user record)
        {
            int _total = 0;
            Expression<Func<user, bool>> whereLambda = PredicateExtensions.True<user>();
            if (record != null)
            {
                if (record.ID != null)

                {
                    whereLambda.And(p => p.ID.Equals(record.ID));
                }

                if (record.AVATAR_ID != null)

                {
                    whereLambda.And(p => p.AVATAR_ID.Equals(record.AVATAR_ID));
                }

                if (!String.IsNullOrEmpty(record.EMAIL))

                {
                    whereLambda.And(p => p.EMAIL.Equals(record.EMAIL));
                }

                if (record.ENABLED != null)

                {
                    whereLambda.And(p => p.ENABLED.Equals(record.ENABLED));
                }

                if (!String.IsNullOrEmpty(record.PASSWORD))

                {
                    whereLambda.And(p => p.PASSWORD.Equals(record.PASSWORD));
                }

                if (!String.IsNullOrEmpty(record.USERNAME))

                {
                    whereLambda.And(p => p.USERNAME.Equals(record.USERNAME));
                }

                if (record.DEPT_ID != null)

                {
                    whereLambda.And(p => p.DEPT_ID.Equals(record.DEPT_ID));
                }

                if (!String.IsNullOrEmpty(record.PHONE))

                {
                    whereLambda.And(p => p.PHONE.Equals(record.PHONE));
                }

                if (record.JOB_ID != null)

                {
                    whereLambda.And(p => p.JOB_ID.Equals(record.JOB_ID));
                }

                if (record.CREATE_TIME != null)

                {
                    whereLambda.And(p => p.CREATE_TIME.Equals(record.CREATE_TIME));
                }

                if (record.LAST_PASSWORD_RESET_TIME != null)

                {
                    whereLambda.And(p => p.LAST_PASSWORD_RESET_TIME.Equals(record.LAST_PASSWORD_RESET_TIME));
                }

                if (!String.IsNullOrEmpty(record.NICK_NAME))

                {
                    whereLambda.And(p => p.NICK_NAME.Equals(record.NICK_NAME));
                }

                if (!String.IsNullOrEmpty(record.SEX))

                {
                    whereLambda.And(p => p.SEX.Equals(record.SEX));
                }
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
                user record = new user() { ID = id };
                    
                myDb.user.Attach(record);
                myDb.Entry(record).State = EntityState.Deleted;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(user record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.user.Add(record);
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(user record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.user.Attach(record);
                myDb.Entry(record).State = EntityState.Modified;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// getById
        /// </summary>
        public user getById(int id)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                 // TODO 生成代码后需要检查一下是否找到正确的主键，这里做一个错误代码，避免直接使用
              
                 return myDb.Set<user>().Where<user>(p => p.ID == id).FirstOrDefault<user>();
                  
            }
        }
    }
  }
