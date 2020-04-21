
  using PW.DBCommon.Model;
  using System.Collections.Generic;
  using System.ServiceModel;

  namespace PW.Service
  {
    [ServiceContract]
    public interface IServiceGeneralWorkflows
    {
        /// <summary>
        /// 查询
        /// </summary>
        [OperationContract]
        List<general_workflows> query(general_workflows record);

        /// <summary>
        /// 分页查询
        /// </summary>
        [OperationContract]
        List<general_workflows> queryPage(general_workflows record);

        /// <summary>
        /// 删除
        /// </summary>
        [OperationContract]
        int deleteById(int id);

        /// <summary>
        /// 添加
        /// </summary>
        [OperationContract]
        int add(general_workflows record);

        /// <summary>
        /// 更新
        /// </summary>
        [OperationContract]
        int updateById(general_workflows record);

        /// <summary>
        /// getById
        /// </summary>
        [OperationContract]
        general_workflows getById(int id);
    }
  }
