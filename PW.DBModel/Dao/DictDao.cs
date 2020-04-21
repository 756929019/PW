
  using PW.DBCommon.Model;
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Linq;
  using System.Linq.Expressions;

  namespace PW.DBCommon.Dao
  {
  
  public class DictDao : BaseDao
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<dict> query(dict record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                IQueryable<dict> db = myDb.dict;
                
                if (record.ID != null)
                    
                {
                    db = db.Where<dict>(p => p.ID.Equals(record.ID));
                }
                
                if (!String.IsNullOrEmpty(record.NAME))
                    
                {
                    db = db.Where<dict>(p => p.NAME.Equals(record.NAME));
                }
                
                if (!String.IsNullOrEmpty(record.REMARK))
                    
                {
                    db = db.Where<dict>(p => p.REMARK.Equals(record.REMARK));
                }
                
                if (record.CREATE_TIME != null)
                    
                {
                    db = db.Where<dict>(p => p.CREATE_TIME.Equals(record.CREATE_TIME));
                }
                
                return db.ToList();
             }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public List<dict> queryPage(dict record)
        {
            int _total = 0;
            Expression<Func<dict, bool>> whereLambda = PredicateExtensions.True<dict>();
            
            if (record.ID != null)
                
            {
                whereLambda.And(p => p.ID.Equals(record.ID));
            }
            
            if (!String.IsNullOrEmpty(record.NAME))
                
            {
                whereLambda.And(p => p.NAME.Equals(record.NAME));
            }
            
            if (!String.IsNullOrEmpty(record.REMARK))
                
            {
                whereLambda.And(p => p.REMARK.Equals(record.REMARK));
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
                dict record = new dict() { ID = id };
                    
                myDb.dict.Attach(record);
                myDb.Entry(record).State = EntityState.Deleted;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(dict record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.dict.Add(record);
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(dict record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.dict.Attach(record);
                myDb.Entry(record).State = EntityState.Modified;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// getById
        /// </summary>
        public dict getById(int id)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                 // TODO 生成代码后需要检查一下是否找到正确的主键，这里做一个错误代码，避免直接使用
              
                 return myDb.Set<dict>().Where<dict>(p => p.ID == id).FirstOrDefault<dict>();
                  
            }
        }
    }
  }
