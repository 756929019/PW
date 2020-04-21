
  using PW.DBCommon.Dao;
  using PW.DBCommon.Model;
  using System.Collections.Generic;

  namespace PW.Service
  {
    public class ServiceGeneralWorkflowFunc : IServiceGeneralWorkflowFunc
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<general_workflow_func> query(general_workflow_func record)
        {
            return new GeneralWorkflowFuncDao().query(record);
        }
 
        /// <summary>
        /// 分页查询
        /// </summary>
        public List<general_workflow_func> queryPage(general_workflow_func record)
        {
            return new GeneralWorkflowFuncDao().queryPage(record);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int deleteById(int id)
        {
            return new GeneralWorkflowFuncDao().deleteById(id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(general_workflow_func record)
        {
            return new GeneralWorkflowFuncDao().add(record);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(general_workflow_func record)
        {
            return new GeneralWorkflowFuncDao().updateById(record);
        }

        /// <summary>
        /// getById
        /// </summary>
        public general_workflow_func getById(int id)
        {
            return new GeneralWorkflowFuncDao().getById(id);
        }
    }
  }
