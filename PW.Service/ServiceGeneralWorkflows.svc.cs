
  using PW.DBCommon.Dao;
  using PW.DBCommon.Model;
  using System.Collections.Generic;

  namespace PW.Service
  {
    public class ServiceGeneralWorkflows : IServiceGeneralWorkflows
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<general_workflows> query(general_workflows record)
        {
            return new GeneralWorkflowsDao().query(record);
        }
 
        /// <summary>
        /// 分页查询
        /// </summary>
        public List<general_workflows> queryPage(general_workflows record)
        {
            return new GeneralWorkflowsDao().queryPage(record);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int deleteById(int id)
        {
            return new GeneralWorkflowsDao().deleteById(id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(general_workflows record)
        {
            return new GeneralWorkflowsDao().add(record);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(general_workflows record)
        {
            return new GeneralWorkflowsDao().updateById(record);
        }

        /// <summary>
        /// getById
        /// </summary>
        public general_workflows getById(int id)
        {
            return new GeneralWorkflowsDao().getById(id);
        }
    }
  }
