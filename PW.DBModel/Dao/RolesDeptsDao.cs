
  using PW.DBCommon.Model;
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Linq;
  using System.Linq.Expressions;

  namespace PW.DBCommon.Dao
  {
  
  public class RolesDeptsDao : BaseDao
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<roles_depts> query(roles_depts record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                IQueryable<roles_depts> db = myDb.roles_depts;
                
                if (record.ROLE_ID != null)
                    
                {
                    db = db.Where<roles_depts>(p => p.ROLE_ID.Equals(record.ROLE_ID));
                }
                
                if (record.DEPT_ID != null)
                    
                {
                    db = db.Where<roles_depts>(p => p.DEPT_ID.Equals(record.DEPT_ID));
                }
                
                return db.ToList();
             }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public List<roles_depts> queryPage(roles_depts record)
        {
            int _total = 0;
            Expression<Func<roles_depts, bool>> whereLambda = PredicateExtensions.True<roles_depts>();
            
            if (record.ROLE_ID != null)
                
            {
                whereLambda.And(p => p.ROLE_ID.Equals(record.ROLE_ID));
            }
            
            if (record.DEPT_ID != null)
                
            {
                whereLambda.And(p => p.DEPT_ID.Equals(record.DEPT_ID));
            }
            
            return LoadPageItems(5, 2, out _total, whereLambda, p => p.ROLE_ID, true);
                
            // return LoadPageItems(5, 2, out _total, whereLambda, p => p.DEPT_ID, true);
                
            // return LoadPageItems(5, 2, out _total, whereLambda, p => p.id, true);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int deleteById(int id, int deptid)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                // TODO 生成代码后需要检查一下是否找到正确的主键，这里做一个错误代码，避免直接使用
                roles_depts record = new roles_depts() { ROLE_ID = id, DEPT_ID = deptid };
                myDb.roles_depts.Attach(record);
                myDb.Entry(record).State = EntityState.Deleted;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(roles_depts record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.roles_depts.Add(record);
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(roles_depts record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.roles_depts.Attach(record);
                myDb.Entry(record).State = EntityState.Modified;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// getById
        /// </summary>
        public roles_depts getById(int id, int deptid)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                 // TODO 生成代码后需要检查一下是否找到正确的主键，这里做一个错误代码，避免直接使用
              
                 return myDb.Set<roles_depts>().Where<roles_depts>(p => p.ROLE_ID == id && p.DEPT_ID == deptid).FirstOrDefault<roles_depts>();
                  
            }
        }
    }
  }
