
  using PW.DBCommon.Model;
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Linq;
  using System.Linq.Expressions;

  namespace PW.DBCommon.Dao
  {
  
  public class GeneralWorkflowsDao : BaseDao
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<general_workflows> query(general_workflows record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                IQueryable<general_workflows> db = myDb.general_workflows;
                
                if (record.ORIGREC != null)
                    
                {
                    db = db.Where<general_workflows>(p => p.ORIGREC.Equals(record.ORIGREC));
                }
                
                if (!String.IsNullOrEmpty(record.WORKFLOWCODE))
                    
                {
                    db = db.Where<general_workflows>(p => p.WORKFLOWCODE.Equals(record.WORKFLOWCODE));
                }
                
                if (!String.IsNullOrEmpty(record.WORKFLOWDESC))
                    
                {
                    db = db.Where<general_workflows>(p => p.WORKFLOWDESC.Equals(record.WORKFLOWDESC));
                }
                
                if (!String.IsNullOrEmpty(record.WORKFLOWNAME))
                    
                {
                    db = db.Where<general_workflows>(p => p.WORKFLOWNAME.Equals(record.WORKFLOWNAME));
                }
                
                if (record.STARTDATE != null)
                    
                {
                    db = db.Where<general_workflows>(p => p.STARTDATE.Equals(record.STARTDATE));
                }
                
                if (record.VERSION != null)
                    
                {
                    db = db.Where<general_workflows>(p => p.VERSION.Equals(record.VERSION));
                }
                
                if (!String.IsNullOrEmpty(record.APPLICATIONREF))
                    
                {
                    db = db.Where<general_workflows>(p => p.APPLICATIONREF.Equals(record.APPLICATIONREF));
                }
                
                if (record.ENDDATE != null)
                    
                {
                    db = db.Where<general_workflows>(p => p.ENDDATE.Equals(record.ENDDATE));
                }
                
                if (!String.IsNullOrEmpty(record.ORIGSTS))
                    
                {
                    db = db.Where<general_workflows>(p => p.ORIGSTS.Equals(record.ORIGSTS));
                }
                
                if (!String.IsNullOrEmpty(record.TABLERELATED))
                    
                {
                    db = db.Where<general_workflows>(p => p.TABLERELATED.Equals(record.TABLERELATED));
                }
                
                if (!String.IsNullOrEmpty(record.STATUSFIELD))
                    
                {
                    db = db.Where<general_workflows>(p => p.STATUSFIELD.Equals(record.STATUSFIELD));
                }
                
                if (!String.IsNullOrEmpty(record.DISSTATUSFIELD))
                    
                {
                    db = db.Where<general_workflows>(p => p.DISSTATUSFIELD.Equals(record.DISSTATUSFIELD));
                }
                
                if (!String.IsNullOrEmpty(record.NEEDESIGN))
                    
                {
                    db = db.Where<general_workflows>(p => p.NEEDESIGN.Equals(record.NEEDESIGN));
                }
                
                if (!String.IsNullOrEmpty(record.REJECTFIELD))
                    
                {
                    db = db.Where<general_workflows>(p => p.REJECTFIELD.Equals(record.REJECTFIELD));
                }
                
                if (!String.IsNullOrEmpty(record.RETIRED))
                    
                {
                    db = db.Where<general_workflows>(p => p.RETIRED.Equals(record.RETIRED));
                }
                
                return db.ToList();
             }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public List<general_workflows> queryPage(general_workflows record)
        {
            int _total = 0;
            Expression<Func<general_workflows, bool>> whereLambda = PredicateExtensions.True<general_workflows>();
            
            if (record.ORIGREC != null)
                
            {
                whereLambda.And(p => p.ORIGREC.Equals(record.ORIGREC));
            }
            
            if (!String.IsNullOrEmpty(record.WORKFLOWCODE))
                
            {
                whereLambda.And(p => p.WORKFLOWCODE.Equals(record.WORKFLOWCODE));
            }
            
            if (!String.IsNullOrEmpty(record.WORKFLOWDESC))
                
            {
                whereLambda.And(p => p.WORKFLOWDESC.Equals(record.WORKFLOWDESC));
            }
            
            if (!String.IsNullOrEmpty(record.WORKFLOWNAME))
                
            {
                whereLambda.And(p => p.WORKFLOWNAME.Equals(record.WORKFLOWNAME));
            }
            
            if (record.STARTDATE != null)
                
            {
                whereLambda.And(p => p.STARTDATE.Equals(record.STARTDATE));
            }
            
            if (record.VERSION != null)
                
            {
                whereLambda.And(p => p.VERSION.Equals(record.VERSION));
            }
            
            if (!String.IsNullOrEmpty(record.APPLICATIONREF))
                
            {
                whereLambda.And(p => p.APPLICATIONREF.Equals(record.APPLICATIONREF));
            }
            
            if (record.ENDDATE != null)
                
            {
                whereLambda.And(p => p.ENDDATE.Equals(record.ENDDATE));
            }
            
            if (!String.IsNullOrEmpty(record.ORIGSTS))
                
            {
                whereLambda.And(p => p.ORIGSTS.Equals(record.ORIGSTS));
            }
            
            if (!String.IsNullOrEmpty(record.TABLERELATED))
                
            {
                whereLambda.And(p => p.TABLERELATED.Equals(record.TABLERELATED));
            }
            
            if (!String.IsNullOrEmpty(record.STATUSFIELD))
                
            {
                whereLambda.And(p => p.STATUSFIELD.Equals(record.STATUSFIELD));
            }
            
            if (!String.IsNullOrEmpty(record.DISSTATUSFIELD))
                
            {
                whereLambda.And(p => p.DISSTATUSFIELD.Equals(record.DISSTATUSFIELD));
            }
            
            if (!String.IsNullOrEmpty(record.NEEDESIGN))
                
            {
                whereLambda.And(p => p.NEEDESIGN.Equals(record.NEEDESIGN));
            }
            
            if (!String.IsNullOrEmpty(record.REJECTFIELD))
                
            {
                whereLambda.And(p => p.REJECTFIELD.Equals(record.REJECTFIELD));
            }
            
            if (!String.IsNullOrEmpty(record.RETIRED))
                
            {
                whereLambda.And(p => p.RETIRED.Equals(record.RETIRED));
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
                general_workflows record = new general_workflows() { ORIGREC = id };
                    
                myDb.general_workflows.Attach(record);
                myDb.Entry(record).State = EntityState.Deleted;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(general_workflows record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.general_workflows.Add(record);
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(general_workflows record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.general_workflows.Attach(record);
                myDb.Entry(record).State = EntityState.Modified;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// getById
        /// </summary>
        public general_workflows getById(int id)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                 // TODO 生成代码后需要检查一下是否找到正确的主键，这里做一个错误代码，避免直接使用
              
                 return myDb.Set<general_workflows>().Where<general_workflows>(p => p.ORIGREC == id).FirstOrDefault<general_workflows>();
                  
            }
        }
    }
  }
