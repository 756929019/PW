
  using PW.DBCommon.Model;
  using System.Collections.Generic;
  using System.ServiceModel;

  namespace PW.Service
  {
    [ServiceContract]
    public interface IServiceGeneralWorkflowstepdet
    {
        /// <summary>
        /// 查询
        /// </summary>
        [OperationContract]
        List<general_workflowstepdet> query(general_workflowstepdet record);

        /// <summary>
        /// 分页查询
        /// </summary>
        [OperationContract]
        List<general_workflowstepdet> queryPage(general_workflowstepdet record);

        /// <summary>
        /// 删除
        /// </summary>
        [OperationContract]
        int deleteById(int id);

        /// <summary>
        /// 添加
        /// </summary>
        [OperationContract]
        int add(general_workflowstepdet record);

        /// <summary>
        /// 更新
        /// </summary>
        [OperationContract]
        int updateById(general_workflowstepdet record);

        /// <summary>
        /// getById
        /// </summary>
        [OperationContract]
        general_workflowstepdet getById(int id);
    }
  }
