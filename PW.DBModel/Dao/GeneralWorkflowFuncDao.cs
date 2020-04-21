
  using PW.DBCommon.Model;
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Linq;
  using System.Linq.Expressions;

  namespace PW.DBCommon.Dao
  {
  
  public class GeneralWorkflowFuncDao : BaseDao
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<general_workflow_func> query(general_workflow_func record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                IQueryable<general_workflow_func> db = myDb.general_workflow_func;
                
                if (record.ORIGREC != null)
                    
                {
                    db = db.Where<general_workflow_func>(p => p.ORIGREC.Equals(record.ORIGREC));
                }
                
                if (!String.IsNullOrEmpty(record.ORIGSTS))
                    
                {
                    db = db.Where<general_workflow_func>(p => p.ORIGSTS.Equals(record.ORIGSTS));
                }
                
                if (!String.IsNullOrEmpty(record.WORKFLOWCODE))
                    
                {
                    db = db.Where<general_workflow_func>(p => p.WORKFLOWCODE.Equals(record.WORKFLOWCODE));
                }
                
                if (!String.IsNullOrEmpty(record.STEPCODE))
                    
                {
                    db = db.Where<general_workflow_func>(p => p.STEPCODE.Equals(record.STEPCODE));
                }
                
                if (!String.IsNullOrEmpty(record.CALCDESC))
                    
                {
                    db = db.Where<general_workflow_func>(p => p.CALCDESC.Equals(record.CALCDESC));
                }
                
                if (!String.IsNullOrEmpty(record.CALCNAME))
                    
                {
                    db = db.Where<general_workflow_func>(p => p.CALCNAME.Equals(record.CALCNAME));
                }
                
                if (!String.IsNullOrEmpty(record.CALCUL))
                    
                {
                    db = db.Where<general_workflow_func>(p => p.CALCUL.Equals(record.CALCUL));
                }
                
                if (!String.IsNullOrEmpty(record.CONTROLID))
                    
                {
                    db = db.Where<general_workflow_func>(p => p.CONTROLID.Equals(record.CONTROLID));
                }
                
                if (!String.IsNullOrEmpty(record.ACTIONTYPE))
                    
                {
                    db = db.Where<general_workflow_func>(p => p.ACTIONTYPE.Equals(record.ACTIONTYPE));
                }
                
                if (!String.IsNullOrEmpty(record.REPLACE))
                    
                {
                    db = db.Where<general_workflow_func>(p => p.REPLACE.Equals(record.REPLACE));
                }
                
                if (!String.IsNullOrEmpty(record.CONTROLTYPE))
                    
                {
                    db = db.Where<general_workflow_func>(p => p.CONTROLTYPE.Equals(record.CONTROLTYPE));
                }
                
                return db.ToList();
             }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public List<general_workflow_func> queryPage(general_workflow_func record)
        {
            int _total = 0;
            Expression<Func<general_workflow_func, bool>> whereLambda = PredicateExtensions.True<general_workflow_func>();
            
            if (record.ORIGREC != null)
                
            {
                whereLambda.And(p => p.ORIGREC.Equals(record.ORIGREC));
            }
            
            if (!String.IsNullOrEmpty(record.ORIGSTS))
                
            {
                whereLambda.And(p => p.ORIGSTS.Equals(record.ORIGSTS));
            }
            
            if (!String.IsNullOrEmpty(record.WORKFLOWCODE))
                
            {
                whereLambda.And(p => p.WORKFLOWCODE.Equals(record.WORKFLOWCODE));
            }
            
            if (!String.IsNullOrEmpty(record.STEPCODE))
                
            {
                whereLambda.And(p => p.STEPCODE.Equals(record.STEPCODE));
            }
            
            if (!String.IsNullOrEmpty(record.CALCDESC))
                
            {
                whereLambda.And(p => p.CALCDESC.Equals(record.CALCDESC));
            }
            
            if (!String.IsNullOrEmpty(record.CALCNAME))
                
            {
                whereLambda.And(p => p.CALCNAME.Equals(record.CALCNAME));
            }
            
            if (!String.IsNullOrEmpty(record.CALCUL))
                
            {
                whereLambda.And(p => p.CALCUL.Equals(record.CALCUL));
            }
            
            if (!String.IsNullOrEmpty(record.CONTROLID))
                
            {
                whereLambda.And(p => p.CONTROLID.Equals(record.CONTROLID));
            }
            
            if (!String.IsNullOrEmpty(record.ACTIONTYPE))
                
            {
                whereLambda.And(p => p.ACTIONTYPE.Equals(record.ACTIONTYPE));
            }
            
            if (!String.IsNullOrEmpty(record.REPLACE))
                
            {
                whereLambda.And(p => p.REPLACE.Equals(record.REPLACE));
            }
            
            if (!String.IsNullOrEmpty(record.CONTROLTYPE))
                
            {
                whereLambda.And(p => p.CONTROLTYPE.Equals(record.CONTROLTYPE));
            }
            
            return LoadPageItems(5, 2, out _total, whereLambda, p => p.ORIGREC, true);
                
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
                general_workflow_func record = new general_workflow_func() { ORIGREC = id };
                    
                myDb.general_workflow_func.Attach(record);
                myDb.Entry(record).State = EntityState.Deleted;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(general_workflow_func record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.general_workflow_func.Add(record);
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(general_workflow_func record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.general_workflow_func.Attach(record);
                myDb.Entry(record).State = EntityState.Modified;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// getById
        /// </summary>
        public general_workflow_func getById(int id)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                 // TODO 生成代码后需要检查一下是否找到正确的主键，这里做一个错误代码，避免直接使用
              
                 return myDb.Set<general_workflow_func>().Where<general_workflow_func>(p => p.ORIGREC == id).FirstOrDefault<general_workflow_func>();
                  
            }
        }
    }
  }
