
  using PW.DBCommon.Model;
  using System.Collections.Generic;
  using System.ServiceModel;

  namespace PW.Service
  {
    [ServiceContract]
    public interface IServiceRole
    {
        /// <summary>
        /// 查询
        /// </summary>
        [OperationContract]
        List<role> query(role record);

        /// <summary>
        /// 分页查询
        /// </summary>
        [OperationContract]
        List<role> queryPage(role record);

        /// <summary>
        /// 删除
        /// </summary>
        [OperationContract]
        int deleteById(int id);

        /// <summary>
        /// 添加
        /// </summary>
        [OperationContract]
        int add(role record);

        /// <summary>
        /// 更新
        /// </summary>
        [OperationContract]
        int updateById(role record);

        /// <summary>
        /// getById
        /// </summary>
        [OperationContract]
        role getById(int id);
    }
  }
