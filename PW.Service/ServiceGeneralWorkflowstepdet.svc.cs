
  using PW.DBCommon.Dao;
  using PW.DBCommon.Model;
  using System.Collections.Generic;

  namespace PW.Service
  {
    public class ServiceGeneralWorkflowstepdet : IServiceGeneralWorkflowstepdet
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<general_workflowstepdet> query(general_workflowstepdet record)
        {
            return new GeneralWorkflowstepdetDao().query(record);
        }
 
        /// <summary>
        /// 分页查询
        /// </summary>
        public List<general_workflowstepdet> queryPage(general_workflowstepdet record)
        {
            return new GeneralWorkflowstepdetDao().queryPage(record);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int deleteById(int id)
        {
            return new GeneralWorkflowstepdetDao().deleteById(id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(general_workflowstepdet record)
        {
            return new GeneralWorkflowstepdetDao().add(record);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(general_workflowstepdet record)
        {
            return new GeneralWorkflowstepdetDao().updateById(record);
        }

        /// <summary>
        /// getById
        /// </summary>
        public general_workflowstepdet getById(int id)
        {
            return new GeneralWorkflowstepdetDao().getById(id);
        }
    }
  }
