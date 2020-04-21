
  using PW.DBCommon.Model;
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Linq;
  using System.Linq.Expressions;

  namespace PW.DBCommon.Dao
  {
  
  public class MenuDao : BaseDao
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<menu> query(menu record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                IQueryable<menu> db = myDb.menu;
                
                if (record.ID != null)
                    
                {
                    db = db.Where<menu>(p => p.ID.Equals(record.ID));
                }
                
                if (record.I_FRAME != null)
                    
                {
                    db = db.Where<menu>(p => p.I_FRAME.Equals(record.I_FRAME));
                }
                
                if (!String.IsNullOrEmpty(record.NAME))
                    
                {
                    db = db.Where<menu>(p => p.NAME.Equals(record.NAME));
                }
                
                if (!String.IsNullOrEmpty(record.COMPONENT))
                    
                {
                    db = db.Where<menu>(p => p.COMPONENT.Equals(record.COMPONENT));
                }
                
                if (record.PID != null)
                    
                {
                    db = db.Where<menu>(p => p.PID.Equals(record.PID));
                }
                
                if (record.SORT != null)
                    
                {
                    db = db.Where<menu>(p => p.SORT.Equals(record.SORT));
                }
                
                if (!String.IsNullOrEmpty(record.ICON))
                    
                {
                    db = db.Where<menu>(p => p.ICON.Equals(record.ICON));
                }
                
                if (!String.IsNullOrEmpty(record.PATH))
                    
                {
                    db = db.Where<menu>(p => p.PATH.Equals(record.PATH));
                }
                
                if (record.CACHE != null)
                    
                {
                    db = db.Where<menu>(p => p.CACHE.Equals(record.CACHE));
                }
                
                if (record.HIDDEN != null)
                    
                {
                    db = db.Where<menu>(p => p.HIDDEN.Equals(record.HIDDEN));
                }
                
                if (!String.IsNullOrEmpty(record.COMPONENT_NAME))
                    
                {
                    db = db.Where<menu>(p => p.COMPONENT_NAME.Equals(record.COMPONENT_NAME));
                }
                
                if (record.CREATE_TIME != null)
                    
                {
                    db = db.Where<menu>(p => p.CREATE_TIME.Equals(record.CREATE_TIME));
                }
                
                if (!String.IsNullOrEmpty(record.PERMISSION))
                    
                {
                    db = db.Where<menu>(p => p.PERMISSION.Equals(record.PERMISSION));
                }
                
                if (record.TYPE != null)
                    
                {
                    db = db.Where<menu>(p => p.TYPE.Equals(record.TYPE));
                }
                
                if (!String.IsNullOrEmpty(record.MODULES))
                    
                {
                    db = db.Where<menu>(p => p.MODULES.Equals(record.MODULES));
                }
                
                return db.ToList();
             }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public List<menu> queryPage(menu record)
        {
            int _total = 0;
            Expression<Func<menu, bool>> whereLambda = PredicateExtensions.True<menu>();
            
            if (record.ID != null)
                
            {
                whereLambda.And(p => p.ID.Equals(record.ID));
            }
            
            if (record.I_FRAME != null)
                
            {
                whereLambda.And(p => p.I_FRAME.Equals(record.I_FRAME));
            }
            
            if (!String.IsNullOrEmpty(record.NAME))
                
            {
                whereLambda.And(p => p.NAME.Equals(record.NAME));
            }
            
            if (!String.IsNullOrEmpty(record.COMPONENT))
                
            {
                whereLambda.And(p => p.COMPONENT.Equals(record.COMPONENT));
            }
            
            if (record.PID != null)
                
            {
                whereLambda.And(p => p.PID.Equals(record.PID));
            }
            
            if (record.SORT != null)
                
            {
                whereLambda.And(p => p.SORT.Equals(record.SORT));
            }
            
            if (!String.IsNullOrEmpty(record.ICON))
                
            {
                whereLambda.And(p => p.ICON.Equals(record.ICON));
            }
            
            if (!String.IsNullOrEmpty(record.PATH))
                
            {
                whereLambda.And(p => p.PATH.Equals(record.PATH));
            }
            
            if (record.CACHE != null)
                
            {
                whereLambda.And(p => p.CACHE.Equals(record.CACHE));
            }
            
            if (record.HIDDEN != null)
                
            {
                whereLambda.And(p => p.HIDDEN.Equals(record.HIDDEN));
            }
            
            if (!String.IsNullOrEmpty(record.COMPONENT_NAME))
                
            {
                whereLambda.And(p => p.COMPONENT_NAME.Equals(record.COMPONENT_NAME));
            }
            
            if (record.CREATE_TIME != null)
                
            {
                whereLambda.And(p => p.CREATE_TIME.Equals(record.CREATE_TIME));
            }
            
            if (!String.IsNullOrEmpty(record.PERMISSION))
                
            {
                whereLambda.And(p => p.PERMISSION.Equals(record.PERMISSION));
            }
            
            if (record.TYPE != null)
                
            {
                whereLambda.And(p => p.TYPE.Equals(record.TYPE));
            }
            
            if (!String.IsNullOrEmpty(record.MODULES))
                
            {
                whereLambda.And(p => p.MODULES.Equals(record.MODULES));
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
                menu record = new menu() { ID = id };
                    
                myDb.menu.Attach(record);
                myDb.Entry(record).State = EntityState.Deleted;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(menu record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.menu.Add(record);
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(menu record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.menu.Attach(record);
                myDb.Entry(record).State = EntityState.Modified;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// getById
        /// </summary>
        public menu getById(int id)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                 // TODO 生成代码后需要检查一下是否找到正确的主键，这里做一个错误代码，避免直接使用
              
                 return myDb.Set<menu>().Where<menu>(p => p.ID == id).FirstOrDefault<menu>();
                  
            }
        }
    }
  }
