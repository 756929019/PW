
  using PW.DBCommon.Model;
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Linq;
  using System.Linq.Expressions;

  namespace PW.DBCommon.Dao
  {
  
  public class RoleDao : BaseDao
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<role> query(role record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                IQueryable<role> db = myDb.role;
                
                if (record.ID != null)
                    
                {
                    db = db.Where<role>(p => p.ID.Equals(record.ID));
                }
                
                if (!String.IsNullOrEmpty(record.NAME))
                    
                {
                    db = db.Where<role>(p => p.NAME.Equals(record.NAME));
                }
                
                if (!String.IsNullOrEmpty(record.REMARK))
                    
                {
                    db = db.Where<role>(p => p.REMARK.Equals(record.REMARK));
                }
                
                if (!String.IsNullOrEmpty(record.DATA_SCOPE))
                    
                {
                    db = db.Where<role>(p => p.DATA_SCOPE.Equals(record.DATA_SCOPE));
                }
                
                if (record.LEVEL != null)
                    
                {
                    db = db.Where<role>(p => p.LEVEL.Equals(record.LEVEL));
                }
                
                if (record.CREATE_TIME != null)
                    
                {
                    db = db.Where<role>(p => p.CREATE_TIME.Equals(record.CREATE_TIME));
                }
                
                if (!String.IsNullOrEmpty(record.PERMISSION))
                    
                {
                    db = db.Where<role>(p => p.PERMISSION.Equals(record.PERMISSION));
                }
                
                return db.ToList();
             }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public List<role> queryPage(role record)
        {
            int _total = 0;
            Expression<Func<role, bool>> whereLambda = PredicateExtensions.True<role>();
            
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
            
            if (!String.IsNullOrEmpty(record.DATA_SCOPE))
                
            {
                whereLambda.And(p => p.DATA_SCOPE.Equals(record.DATA_SCOPE));
            }
            
            if (record.LEVEL != null)
                
            {
                whereLambda.And(p => p.LEVEL.Equals(record.LEVEL));
            }
            
            if (record.CREATE_TIME != null)
                
            {
                whereLambda.And(p => p.CREATE_TIME.Equals(record.CREATE_TIME));
            }
            
            if (!String.IsNullOrEmpty(record.PERMISSION))
                
            {
                whereLambda.And(p => p.PERMISSION.Equals(record.PERMISSION));
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
                role record = new role() { ID = id };
                    
                myDb.role.Attach(record);
                myDb.Entry(record).State = EntityState.Deleted;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(role record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.role.Add(record);
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(role record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.role.Attach(record);
                myDb.Entry(record).State = EntityState.Modified;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// getById
        /// </summary>
        public role getById(int id)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                 // TODO 生成代码后需要检查一下是否找到正确的主键，这里做一个错误代码，避免直接使用
              
                 return myDb.Set<role>().Where<role>(p => p.ID == id).FirstOrDefault<role>();
                  
            }
        }
    }
  }
