
  using PW.DBCommon.Model;
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Linq;
  using System.Linq.Expressions;

  namespace PW.DBCommon.Dao
  {
  
  public class RolesMenusDao : BaseDao
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<roles_menus> query(roles_menus record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                IQueryable<roles_menus> db = myDb.roles_menus;
                
                if (record.MENU_ID != null)
                    
                {
                    db = db.Where<roles_menus>(p => p.MENU_ID.Equals(record.MENU_ID));
                }
                
                if (record.ROLE_ID != null)
                    
                {
                    db = db.Where<roles_menus>(p => p.ROLE_ID.Equals(record.ROLE_ID));
                }
                
                return db.ToList();
             }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public List<roles_menus> queryPage(roles_menus record)
        {
            int _total = 0;
            Expression<Func<roles_menus, bool>> whereLambda = PredicateExtensions.True<roles_menus>();
            
            if (record.MENU_ID != null)
                
            {
                whereLambda.And(p => p.MENU_ID.Equals(record.MENU_ID));
            }
            
            if (record.ROLE_ID != null)
                
            {
                whereLambda.And(p => p.ROLE_ID.Equals(record.ROLE_ID));
            }
            
            return LoadPageItems(5, 2, out _total, whereLambda, p => p.MENU_ID, true);
                
            // return LoadPageItems(5, 2, out _total, whereLambda, p => p.id, true);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int deleteById(int id, int menuid)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                // TODO 生成代码后需要检查一下是否找到正确的主键，这里做一个错误代码，避免直接使用
                roles_menus record = new roles_menus() { MENU_ID = menuid, ROLE_ID = id };
                    
                myDb.roles_menus.Attach(record);
                myDb.Entry(record).State = EntityState.Deleted;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(roles_menus record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.roles_menus.Add(record);
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(roles_menus record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.roles_menus.Attach(record);
                myDb.Entry(record).State = EntityState.Modified;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// getById
        /// </summary>
        public roles_menus getById(int id, int menuid)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                 // TODO 生成代码后需要检查一下是否找到正确的主键，这里做一个错误代码，避免直接使用
              
                 return myDb.Set<roles_menus>().Where<roles_menus>(p => p.MENU_ID == id && p.ROLE_ID == menuid).FirstOrDefault<roles_menus>();
            }
        }
    }
  }
