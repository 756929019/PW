
  using PW.DBCommon.Model;
  using System.Collections.Generic;
  using System.ServiceModel;

  namespace PW.Service
  {
    [ServiceContract]
    public interface IServiceDept
    {
        /// <summary>
        /// 查询
        /// </summary>
        [OperationContract]
        List<dept> query(dept record);

        /// <summary>
        /// 分页查询
        /// </summary>
        [OperationContract]
        List<dept> queryPage(dept record);

        /// <summary>
        /// 删除
        /// </summary>
        [OperationContract]
        int deleteById(int id);

        /// <summary>
        /// 添加
        /// </summary>
        [OperationContract]
        int add(dept record);

        /// <summary>
        /// 更新
        /// </summary>
        [OperationContract]
        int updateById(dept record);

        /// <summary>
        /// getById
        /// </summary>
        [OperationContract]
        dept getById(int id);
    }
  }
