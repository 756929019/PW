
  using PW.DBCommon.Model;
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Linq;
  using System.Linq.Expressions;

  namespace PW.DBCommon.Dao
  {
  
  public class GeneralWorkflowStepsDao : BaseDao
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<general_workflow_steps> query(general_workflow_steps record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                IQueryable<general_workflow_steps> db = myDb.general_workflow_steps;
                
                if (record.ORIGREC != null)
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.ORIGREC.Equals(record.ORIGREC));
                }
                
                if (!String.IsNullOrEmpty(record.WORKFLOWCODE))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.WORKFLOWCODE.Equals(record.WORKFLOWCODE));
                }
                
                if (!String.IsNullOrEmpty(record.USRNAM))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.USRNAM.Equals(record.USRNAM));
                }
                
                if (record.NUMBEROOFDAYS != null)
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.NUMBEROOFDAYS.Equals(record.NUMBEROOFDAYS));
                }
                
                if (!String.IsNullOrEmpty(record.STEPCODE))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.STEPCODE.Equals(record.STEPCODE));
                }
                
                if (record.SORTER != null)
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.SORTER.Equals(record.SORTER));
                }
                
                if (!String.IsNullOrEmpty(record.JOBDESCRIPTION))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.JOBDESCRIPTION.Equals(record.JOBDESCRIPTION));
                }
                
                if (!String.IsNullOrEmpty(record.GOTOSTEPS))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.GOTOSTEPS.Equals(record.GOTOSTEPS));
                }
                
                if (!String.IsNullOrEmpty(record.SIGNATURETYPE))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.SIGNATURETYPE.Equals(record.SIGNATURETYPE));
                }
                
                if (!String.IsNullOrEmpty(record.ORIGSTS))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.ORIGSTS.Equals(record.ORIGSTS));
                }
                
                if (!String.IsNullOrEmpty(record.TREEAUTH))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.TREEAUTH.Equals(record.TREEAUTH));
                }
                
                if (!String.IsNullOrEmpty(record.STEPSTATUS))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.STEPSTATUS.Equals(record.STEPSTATUS));
                }
                
                if (!String.IsNullOrEmpty(record.STEPNAME))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.STEPNAME.Equals(record.STEPNAME));
                }
                
                if (!String.IsNullOrEmpty(record.EXECUTEACTION))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.EXECUTEACTION.Equals(record.EXECUTEACTION));
                }
                
                if (!String.IsNullOrEmpty(record.EXECUTEACTION1))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.EXECUTEACTION1.Equals(record.EXECUTEACTION1));
                }
                
                if (!String.IsNullOrEmpty(record.EXECUTEACTION2))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.EXECUTEACTION2.Equals(record.EXECUTEACTION2));
                }
                
                if (!String.IsNullOrEmpty(record.EXECUTEACTION3))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.EXECUTEACTION3.Equals(record.EXECUTEACTION3));
                }
                
                if (!String.IsNullOrEmpty(record.CONSOLEITEM))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.CONSOLEITEM.Equals(record.CONSOLEITEM));
                }
                
                if (!String.IsNullOrEmpty(record.DESTINATIONTYPE))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.DESTINATIONTYPE.Equals(record.DESTINATIONTYPE));
                }
                
                if (!String.IsNullOrEmpty(record.GROUPNAME))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.GROUPNAME.Equals(record.GROUPNAME));
                }
                
                if (!String.IsNullOrEmpty(record.STEPDISPSTATUS))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.STEPDISPSTATUS.Equals(record.STEPDISPSTATUS));
                }
                
                if (!String.IsNullOrEmpty(record.GROUPTABLE))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.GROUPTABLE.Equals(record.GROUPTABLE));
                }
                
                if (!String.IsNullOrEmpty(record.GROUPTABLEFIELD1))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.GROUPTABLEFIELD1.Equals(record.GROUPTABLEFIELD1));
                }
                
                if (!String.IsNullOrEmpty(record.GROUPTABLEFIELD2))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.GROUPTABLEFIELD2.Equals(record.GROUPTABLEFIELD2));
                }
                
                if (!String.IsNullOrEmpty(record.GROUPTABLEFIELD3))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.GROUPTABLEFIELD3.Equals(record.GROUPTABLEFIELD3));
                }
                
                if (!String.IsNullOrEmpty(record.MYLIMSSTS))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.MYLIMSSTS.Equals(record.MYLIMSSTS));
                }
                
                if (!String.IsNullOrEmpty(record.COMMENTNAME))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.COMMENTNAME.Equals(record.COMMENTNAME));
                }
                
                if (!String.IsNullOrEmpty(record.FUNCCODE))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.FUNCCODE.Equals(record.FUNCCODE));
                }
                
                if (!String.IsNullOrEmpty(record.BYSERVGRP))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.BYSERVGRP.Equals(record.BYSERVGRP));
                }
                
                if (!String.IsNullOrEmpty(record.ROLES))
                    
                {
                    db = db.Where<general_workflow_steps>(p => p.ROLES.Equals(record.ROLES));
                }
                
                return db.ToList();
             }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public List<general_workflow_steps> queryPage(general_workflow_steps record)
        {
            int _total = 0;
            Expression<Func<general_workflow_steps, bool>> whereLambda = PredicateExtensions.True<general_workflow_steps>();
            
            if (record.ORIGREC != null)
                
            {
                whereLambda.And(p => p.ORIGREC.Equals(record.ORIGREC));
            }
            
            if (!String.IsNullOrEmpty(record.WORKFLOWCODE))
                
            {
                whereLambda.And(p => p.WORKFLOWCODE.Equals(record.WORKFLOWCODE));
            }
            
            if (!String.IsNullOrEmpty(record.USRNAM))
                
            {
                whereLambda.And(p => p.USRNAM.Equals(record.USRNAM));
            }
            
            if (record.NUMBEROOFDAYS != null)
                
            {
                whereLambda.And(p => p.NUMBEROOFDAYS.Equals(record.NUMBEROOFDAYS));
            }
            
            if (!String.IsNullOrEmpty(record.STEPCODE))
                
            {
                whereLambda.And(p => p.STEPCODE.Equals(record.STEPCODE));
            }
            
            if (record.SORTER != null)
                
            {
                whereLambda.And(p => p.SORTER.Equals(record.SORTER));
            }
            
            if (!String.IsNullOrEmpty(record.JOBDESCRIPTION))
                
            {
                whereLambda.And(p => p.JOBDESCRIPTION.Equals(record.JOBDESCRIPTION));
            }
            
            if (!String.IsNullOrEmpty(record.GOTOSTEPS))
                
            {
                whereLambda.And(p => p.GOTOSTEPS.Equals(record.GOTOSTEPS));
            }
            
            if (!String.IsNullOrEmpty(record.SIGNATURETYPE))
                
            {
                whereLambda.And(p => p.SIGNATURETYPE.Equals(record.SIGNATURETYPE));
            }
            
            if (!String.IsNullOrEmpty(record.ORIGSTS))
                
            {
                whereLambda.And(p => p.ORIGSTS.Equals(record.ORIGSTS));
            }
            
            if (!String.IsNullOrEmpty(record.TREEAUTH))
                
            {
                whereLambda.And(p => p.TREEAUTH.Equals(record.TREEAUTH));
            }
            
            if (!String.IsNullOrEmpty(record.STEPSTATUS))
                
            {
                whereLambda.And(p => p.STEPSTATUS.Equals(record.STEPSTATUS));
            }
            
            if (!String.IsNullOrEmpty(record.STEPNAME))
                
            {
                whereLambda.And(p => p.STEPNAME.Equals(record.STEPNAME));
            }
            
            if (!String.IsNullOrEmpty(record.EXECUTEACTION))
                
            {
                whereLambda.And(p => p.EXECUTEACTION.Equals(record.EXECUTEACTION));
            }
            
            if (!String.IsNullOrEmpty(record.EXECUTEACTION1))
                
            {
                whereLambda.And(p => p.EXECUTEACTION1.Equals(record.EXECUTEACTION1));
            }
            
            if (!String.IsNullOrEmpty(record.EXECUTEACTION2))
                
            {
                whereLambda.And(p => p.EXECUTEACTION2.Equals(record.EXECUTEACTION2));
            }
            
            if (!String.IsNullOrEmpty(record.EXECUTEACTION3))
                
            {
                whereLambda.And(p => p.EXECUTEACTION3.Equals(record.EXECUTEACTION3));
            }
            
            if (!String.IsNullOrEmpty(record.CONSOLEITEM))
                
            {
                whereLambda.And(p => p.CONSOLEITEM.Equals(record.CONSOLEITEM));
            }
            
            if (!String.IsNullOrEmpty(record.DESTINATIONTYPE))
                
            {
                whereLambda.And(p => p.DESTINATIONTYPE.Equals(record.DESTINATIONTYPE));
            }
            
            if (!String.IsNullOrEmpty(record.GROUPNAME))
                
            {
                whereLambda.And(p => p.GROUPNAME.Equals(record.GROUPNAME));
            }
            
            if (!String.IsNullOrEmpty(record.STEPDISPSTATUS))
                
            {
                whereLambda.And(p => p.STEPDISPSTATUS.Equals(record.STEPDISPSTATUS));
            }
            
            if (!String.IsNullOrEmpty(record.GROUPTABLE))
                
            {
                whereLambda.And(p => p.GROUPTABLE.Equals(record.GROUPTABLE));
            }
            
            if (!String.IsNullOrEmpty(record.GROUPTABLEFIELD1))
                
            {
                whereLambda.And(p => p.GROUPTABLEFIELD1.Equals(record.GROUPTABLEFIELD1));
            }
            
            if (!String.IsNullOrEmpty(record.GROUPTABLEFIELD2))
                
            {
                whereLambda.And(p => p.GROUPTABLEFIELD2.Equals(record.GROUPTABLEFIELD2));
            }
            
            if (!String.IsNullOrEmpty(record.GROUPTABLEFIELD3))
                
            {
                whereLambda.And(p => p.GROUPTABLEFIELD3.Equals(record.GROUPTABLEFIELD3));
            }
            
            if (!String.IsNullOrEmpty(record.MYLIMSSTS))
                
            {
                whereLambda.And(p => p.MYLIMSSTS.Equals(record.MYLIMSSTS));
            }
            
            if (!String.IsNullOrEmpty(record.COMMENTNAME))
                
            {
                whereLambda.And(p => p.COMMENTNAME.Equals(record.COMMENTNAME));
            }
            
            if (!String.IsNullOrEmpty(record.FUNCCODE))
                
            {
                whereLambda.And(p => p.FUNCCODE.Equals(record.FUNCCODE));
            }
            
            if (!String.IsNullOrEmpty(record.BYSERVGRP))
                
            {
                whereLambda.And(p => p.BYSERVGRP.Equals(record.BYSERVGRP));
            }
            
            if (!String.IsNullOrEmpty(record.ROLES))
                
            {
                whereLambda.And(p => p.ROLES.Equals(record.ROLES));
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
                general_workflow_steps record = new general_workflow_steps() { ORIGREC = id };
                    
                myDb.general_workflow_steps.Attach(record);
                myDb.Entry(record).State = EntityState.Deleted;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(general_workflow_steps record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.general_workflow_steps.Add(record);
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(general_workflow_steps record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.general_workflow_steps.Attach(record);
                myDb.Entry(record).State = EntityState.Modified;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// getById
        /// </summary>
        public general_workflow_steps getById(int id)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                 // TODO 生成代码后需要检查一下是否找到正确的主键，这里做一个错误代码，避免直接使用
              
                 return myDb.Set<general_workflow_steps>().Where<general_workflow_steps>(p => p.ORIGREC == id).FirstOrDefault<general_workflow_steps>();
                  
            }
        }
    }
  }
