
  using PW.DBCommon.Dao;
  using PW.DBCommon.Model;
  using System.Collections.Generic;

  namespace PW.Service
  {
    public class ServiceGeneralWorkflowSteps : IServiceGeneralWorkflowSteps
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<general_workflow_steps> query(general_workflow_steps record)
        {
            return new GeneralWorkflowStepsDao().query(record);
        }
 
        /// <summary>
        /// 分页查询
        /// </summary>
        public List<general_workflow_steps> queryPage(general_workflow_steps record)
        {
            return new GeneralWorkflowStepsDao().queryPage(record);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int deleteById(int id)
        {
            return new GeneralWorkflowStepsDao().deleteById(id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(general_workflow_steps record)
        {
            return new GeneralWorkflowStepsDao().add(record);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(general_workflow_steps record)
        {
            return new GeneralWorkflowStepsDao().updateById(record);
        }

        /// <summary>
        /// getById
        /// </summary>
        public general_workflow_steps getById(int id)
        {
            return new GeneralWorkflowStepsDao().getById(id);
        }
    }
  }
