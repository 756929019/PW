
  using PW.DBCommon.Model;
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Linq;
  using System.Linq.Expressions;

  namespace PW.DBCommon.Dao
  {
  
  public class GeneralWorkflowstepdetDao : BaseDao
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<general_workflowstepdet> query(general_workflowstepdet record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                IQueryable<general_workflowstepdet> db = myDb.general_workflowstepdet;
                
                if (record.ORIGREC != null)
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.ORIGREC.Equals(record.ORIGREC));
                }
                
                if (!String.IsNullOrEmpty(record.ORIGSTS))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.ORIGSTS.Equals(record.ORIGSTS));
                }
                
                if (!String.IsNullOrEmpty(record.WORKFLOWCODE))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.WORKFLOWCODE.Equals(record.WORKFLOWCODE));
                }
                
                if (!String.IsNullOrEmpty(record.STEPCODE))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.STEPCODE.Equals(record.STEPCODE));
                }
                
                if (!String.IsNullOrEmpty(record.DISPOSITIONNAME))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.DISPOSITIONNAME.Equals(record.DISPOSITIONNAME));
                }
                
                if (!String.IsNullOrEmpty(record.DISPOSITIONCODE))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.DISPOSITIONCODE.Equals(record.DISPOSITIONCODE));
                }
                
                if (!String.IsNullOrEmpty(record.TOSTEPCODE))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.TOSTEPCODE.Equals(record.TOSTEPCODE));
                }
                
                if (record.INVESTIGATIONCODE != null)
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.INVESTIGATIONCODE.Equals(record.INVESTIGATIONCODE));
                }
                
                if (!String.IsNullOrEmpty(record.TOSTEPNAME))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.TOSTEPNAME.Equals(record.TOSTEPNAME));
                }
                
                if (!String.IsNullOrEmpty(record.EXECUTEACTION))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.EXECUTEACTION.Equals(record.EXECUTEACTION));
                }
                
                if (record.SORTER != null)
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.SORTER.Equals(record.SORTER));
                }
                
                if (!String.IsNullOrEmpty(record.VERIFYACTION))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.VERIFYACTION.Equals(record.VERIFYACTION));
                }
                
                if (!String.IsNullOrEmpty(record.REASONS))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.REASONS.Equals(record.REASONS));
                }
                
                if (!String.IsNullOrEmpty(record.ISREJECT))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.ISREJECT.Equals(record.ISREJECT));
                }
                
                if (!String.IsNullOrEmpty(record.VISIBLE))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.VISIBLE.Equals(record.VISIBLE));
                }
                
                if (!String.IsNullOrEmpty(record.AUDITTYPE))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.AUDITTYPE.Equals(record.AUDITTYPE));
                }
                
                if (!String.IsNullOrEmpty(record.NOTNULLCOMMENT))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.NOTNULLCOMMENT.Equals(record.NOTNULLCOMMENT));
                }
                
                if (!String.IsNullOrEmpty(record.DISPFLAG))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.DISPFLAG.Equals(record.DISPFLAG));
                }
                
                if (!String.IsNullOrEmpty(record.STEPDETCAT))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.STEPDETCAT.Equals(record.STEPDETCAT));
                }
                
                if (!String.IsNullOrEmpty(record.REQUIREREASON))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.REQUIREREASON.Equals(record.REQUIREREASON));
                }
                
                if (!String.IsNullOrEmpty(record.ISMULTIAUDIT))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.ISMULTIAUDIT.Equals(record.ISMULTIAUDIT));
                }
                
                if (!String.IsNullOrEmpty(record.ISAUDITUSER))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.ISAUDITUSER.Equals(record.ISAUDITUSER));
                }
                
                if (!String.IsNullOrEmpty(record.CALCUL))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.CALCUL.Equals(record.CALCUL));
                }
                
                if (!String.IsNullOrEmpty(record.BUSINESSTYPE))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.BUSINESSTYPE.Equals(record.BUSINESSTYPE));
                }
                
                if (!String.IsNullOrEmpty(record.MESSAGETEXT))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.MESSAGETEXT.Equals(record.MESSAGETEXT));
                }
                
                if (!String.IsNullOrEmpty(record.MESSAGEUSER))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.MESSAGEUSER.Equals(record.MESSAGEUSER));
                }
                
                if (!String.IsNullOrEmpty(record.ISSENDMES))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.ISSENDMES.Equals(record.ISSENDMES));
                }
                
                if (!String.IsNullOrEmpty(record.ISMULTIPLE))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.ISMULTIPLE.Equals(record.ISMULTIPLE));
                }
                
                if (!String.IsNullOrEmpty(record.VERIFYCALCNO))
                    
                {
                    db = db.Where<general_workflowstepdet>(p => p.VERIFYCALCNO.Equals(record.VERIFYCALCNO));
                }
                
                return db.ToList();
             }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        public List<general_workflowstepdet> queryPage(general_workflowstepdet record)
        {
            int _total = 0;
            Expression<Func<general_workflowstepdet, bool>> whereLambda = PredicateExtensions.True<general_workflowstepdet>();
            
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
            
            if (!String.IsNullOrEmpty(record.DISPOSITIONNAME))
                
            {
                whereLambda.And(p => p.DISPOSITIONNAME.Equals(record.DISPOSITIONNAME));
            }
            
            if (!String.IsNullOrEmpty(record.DISPOSITIONCODE))
                
            {
                whereLambda.And(p => p.DISPOSITIONCODE.Equals(record.DISPOSITIONCODE));
            }
            
            if (!String.IsNullOrEmpty(record.TOSTEPCODE))
                
            {
                whereLambda.And(p => p.TOSTEPCODE.Equals(record.TOSTEPCODE));
            }
            
            if (record.INVESTIGATIONCODE != null)
                
            {
                whereLambda.And(p => p.INVESTIGATIONCODE.Equals(record.INVESTIGATIONCODE));
            }
            
            if (!String.IsNullOrEmpty(record.TOSTEPNAME))
                
            {
                whereLambda.And(p => p.TOSTEPNAME.Equals(record.TOSTEPNAME));
            }
            
            if (!String.IsNullOrEmpty(record.EXECUTEACTION))
                
            {
                whereLambda.And(p => p.EXECUTEACTION.Equals(record.EXECUTEACTION));
            }
            
            if (record.SORTER != null)
                
            {
                whereLambda.And(p => p.SORTER.Equals(record.SORTER));
            }
            
            if (!String.IsNullOrEmpty(record.VERIFYACTION))
                
            {
                whereLambda.And(p => p.VERIFYACTION.Equals(record.VERIFYACTION));
            }
            
            if (!String.IsNullOrEmpty(record.REASONS))
                
            {
                whereLambda.And(p => p.REASONS.Equals(record.REASONS));
            }
            
            if (!String.IsNullOrEmpty(record.ISREJECT))
                
            {
                whereLambda.And(p => p.ISREJECT.Equals(record.ISREJECT));
            }
            
            if (!String.IsNullOrEmpty(record.VISIBLE))
                
            {
                whereLambda.And(p => p.VISIBLE.Equals(record.VISIBLE));
            }
            
            if (!String.IsNullOrEmpty(record.AUDITTYPE))
                
            {
                whereLambda.And(p => p.AUDITTYPE.Equals(record.AUDITTYPE));
            }
            
            if (!String.IsNullOrEmpty(record.NOTNULLCOMMENT))
                
            {
                whereLambda.And(p => p.NOTNULLCOMMENT.Equals(record.NOTNULLCOMMENT));
            }
            
            if (!String.IsNullOrEmpty(record.DISPFLAG))
                
            {
                whereLambda.And(p => p.DISPFLAG.Equals(record.DISPFLAG));
            }
            
            if (!String.IsNullOrEmpty(record.STEPDETCAT))
                
            {
                whereLambda.And(p => p.STEPDETCAT.Equals(record.STEPDETCAT));
            }
            
            if (!String.IsNullOrEmpty(record.REQUIREREASON))
                
            {
                whereLambda.And(p => p.REQUIREREASON.Equals(record.REQUIREREASON));
            }
            
            if (!String.IsNullOrEmpty(record.ISMULTIAUDIT))
                
            {
                whereLambda.And(p => p.ISMULTIAUDIT.Equals(record.ISMULTIAUDIT));
            }
            
            if (!String.IsNullOrEmpty(record.ISAUDITUSER))
                
            {
                whereLambda.And(p => p.ISAUDITUSER.Equals(record.ISAUDITUSER));
            }
            
            if (!String.IsNullOrEmpty(record.CALCUL))
                
            {
                whereLambda.And(p => p.CALCUL.Equals(record.CALCUL));
            }
            
            if (!String.IsNullOrEmpty(record.BUSINESSTYPE))
                
            {
                whereLambda.And(p => p.BUSINESSTYPE.Equals(record.BUSINESSTYPE));
            }
            
            if (!String.IsNullOrEmpty(record.MESSAGETEXT))
                
            {
                whereLambda.And(p => p.MESSAGETEXT.Equals(record.MESSAGETEXT));
            }
            
            if (!String.IsNullOrEmpty(record.MESSAGEUSER))
                
            {
                whereLambda.And(p => p.MESSAGEUSER.Equals(record.MESSAGEUSER));
            }
            
            if (!String.IsNullOrEmpty(record.ISSENDMES))
                
            {
                whereLambda.And(p => p.ISSENDMES.Equals(record.ISSENDMES));
            }
            
            if (!String.IsNullOrEmpty(record.ISMULTIPLE))
                
            {
                whereLambda.And(p => p.ISMULTIPLE.Equals(record.ISMULTIPLE));
            }
            
            if (!String.IsNullOrEmpty(record.VERIFYCALCNO))
                
            {
                whereLambda.And(p => p.VERIFYCALCNO.Equals(record.VERIFYCALCNO));
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
                general_workflowstepdet record = new general_workflowstepdet() { ORIGREC = id };
                    
                myDb.general_workflowstepdet.Attach(record);
                myDb.Entry(record).State = EntityState.Deleted;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(general_workflowstepdet record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.general_workflowstepdet.Add(record);
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(general_workflowstepdet record)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                myDb.general_workflowstepdet.Attach(record);
                myDb.Entry(record).State = EntityState.Modified;
                return myDb.SaveChanges();
            }
        }

        /// <summary>
        /// getById
        /// </summary>
        public general_workflowstepdet getById(int id)
        {
            using (qdbEntities myDb = new qdbEntities())
            {
                 // TODO 生成代码后需要检查一下是否找到正确的主键，这里做一个错误代码，避免直接使用
              
                 return myDb.Set<general_workflowstepdet>().Where<general_workflowstepdet>(p => p.ORIGREC == id).FirstOrDefault<general_workflowstepdet>();
                  
            }
        }
    }
  }
