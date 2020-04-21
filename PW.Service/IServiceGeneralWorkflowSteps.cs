
  using PW.DBCommon.Model;
  using System.Collections.Generic;
  using System.ServiceModel;

  namespace PW.Service
  {
    [ServiceContract]
    public interface IServiceGeneralWorkflowSteps
    {
        /// <summary>
        /// 查询
        /// </summary>
        [OperationContract]
        List<general_workflow_steps> query(general_workflow_steps record);

        /// <summary>
        /// 分页查询
        /// </summary>
        [OperationContract]
        List<general_workflow_steps> queryPage(general_workflow_steps record);

        /// <summary>
        /// 删除
        /// </summary>
        [OperationContract]
        int deleteById(int id);

        /// <summary>
        /// 添加
        /// </summary>
        [OperationContract]
        int add(general_workflow_steps record);

        /// <summary>
        /// 更新
        /// </summary>
        [OperationContract]
        int updateById(general_workflow_steps record);

        /// <summary>
        /// getById
        /// </summary>
        [OperationContract]
        general_workflow_steps getById(int id);
    }
  }
