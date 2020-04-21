
  using PW.DBCommon.Model;
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Linq;
  using System.Linq.Expressions;

  namespace PW.DBCommon.Dao
  {
  
  public class DictDetailDao : BaseDao
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<dict_detail> query(dict_detail record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                IQueryable<dict_detail> db = myDb.dict_detail;
                
                if (record.ID != null)
                    
                {
                    db = db.Where<dict_detail>(p => p.ID.Equals(record.ID));
                }
                
                if (!String.IsNullOrEmpty(record.LABEL))
                    
                {
                    db = db.Where<dict_detail>(p => p.LABEL.Equals(record.LABEL));
                }
                
                if (!String.IsNullOrEmpty(record.VALUE))
                    
                {
                    db = db.Where<dict_detail>(p => p.VALUE.Equals(record.VALUE));
                }
                
                if (!String.IsNullOrEmpty(record.SORT))
                    
                {
                    db = db.Where<dict_detail>(p => p.SORT.Equals(record.SORT));
                }
                
                if (record.DICT_ID != null)
                    
                {
                    db = db.Where<dict_detail>(p => p.DICT_ID.Equals(record.DICT_ID));
                }
                
                if (record.CREATE_TIME != null)
                    
                {
                    db = db.Where<dict_detail>(p => p.CREATE_TIME.Equals(record.CREATE_TIME));
                }
                
                return db.ToList();
             }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public List<dict_detail> queryPage(dict_detail record)
        {
            int _total = 0;
            Expression<Func<dict_detail, bool>> whereLambda = PredicateExtensions.True<dict_detail>();
            
            if (record.ID != null)
                
            {
                whereLambda.And(p => p.ID.Equals(record.ID));
            }
            
            if (!String.IsNullOrEmpty(record.LABEL))
                
            {
                whereLambda.And(p => p.LABEL.Equals(record.LABEL));
            }
            
            if (!String.IsNullOrEmpty(record.VALUE))
                
            {
                whereLambda.And(p => p.VALUE.Equals(record.VALUE));
            }
            
            if (!String.IsNullOrEmpty(record.SORT))
                
            {
                whereLambda.And(p => p.SORT.Equals(record.SORT));
            }
            
            if (record.DICT_ID != null)
                
            {
                whereLambda.And(p => p.DICT_ID.Equals(record.DICT_ID));
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
                dict_detail record = new dict_detail() { ID = id };
                    
                myDb.dict_detail.Attach(record);
                myDb.Entry(record).State = EntityState.Deleted;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(dict_detail record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.dict_detail.Add(record);
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(dict_detail record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.dict_detail.Attach(record);
                myDb.Entry(record).State = EntityState.Modified;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// getById
        /// </summary>
        public dict_detail getById(int id)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                 // TODO 生成代码后需要检查一下是否找到正确的主键，这里做一个错误代码，避免直接使用
              
                 return myDb.Set<dict_detail>().Where<dict_detail>(p => p.ID == id).FirstOrDefault<dict_detail>();
                  
            }
        }
    }
  }
