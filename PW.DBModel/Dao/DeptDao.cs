
  using PW.DBCommon.Model;
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Linq;
  using System.Linq.Expressions;

  namespace PW.DBCommon.Dao
  {
  
  public class DeptDao : BaseDao
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<dept> query(dept record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                IQueryable<dept> db = myDb.dept;
                
                if (record.ID != null)
                    
                {
                    db = db.Where<dept>(p => p.ID.Equals(record.ID));
                }
                
                if (!String.IsNullOrEmpty(record.NAME))
                    
                {
                    db = db.Where<dept>(p => p.NAME.Equals(record.NAME));
                }
                
                if (record.PID != null)
                    
                {
                    db = db.Where<dept>(p => p.PID.Equals(record.PID));
                }
                
                if (record.ENABLED != null)
                    
                {
                    db = db.Where<dept>(p => p.ENABLED.Equals(record.ENABLED));
                }
                
                if (record.CREATE_TIME != null)
                    
                {
                    db = db.Where<dept>(p => p.CREATE_TIME.Equals(record.CREATE_TIME));
                }
                
                return db.ToList();
             }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public List<dept> queryPage(dept record)
        {
            int _total = 0;
            Expression<Func<dept, bool>> whereLambda = PredicateExtensions.True<dept>();
            
            if (record.ID != null)
                
            {
                whereLambda.And(p => p.ID.Equals(record.ID));
            }
            
            if (!String.IsNullOrEmpty(record.NAME))
                
            {
                whereLambda.And(p => p.NAME.Equals(record.NAME));
            }
            
            if (record.PID != null)
                
            {
                whereLambda.And(p => p.PID.Equals(record.PID));
            }
            
            if (record.ENABLED != null)
                
            {
                whereLambda.And(p => p.ENABLED.Equals(record.ENABLED));
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
                dept record = new dept() { ID = id };
                    
                myDb.dept.Attach(record);
                myDb.Entry(record).State = EntityState.Deleted;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(dept record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.dept.Add(record);
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(dept record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.dept.Attach(record);
                myDb.Entry(record).State = EntityState.Modified;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// getById
        /// </summary>
        public dept getById(int id)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                 // TODO 生成代码后需要检查一下是否找到正确的主键，这里做一个错误代码，避免直接使用
              
                 return myDb.Set<dept>().Where<dept>(p => p.ID == id).FirstOrDefault<dept>();
                  
            }
        }
    }
  }
