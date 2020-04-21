
  using PW.DBCommon.Model;
  using System.Collections.Generic;
  using System.ServiceModel;

  namespace PW.Service
  {
    [ServiceContract]
    public interface IServiceUser
    {
        /// <summary>
        /// 查询
        /// </summary>
        [OperationContract]
        List<user> query(user record);

        /// <summary>
        /// 分页查询
        /// </summary>
        [OperationContract]
        List<user> queryPage(user record);

        /// <summary>
        /// 删除
        /// </summary>
        [OperationContract]
        int deleteById(int id);

        /// <summary>
        /// 添加
        /// </summary>
        [OperationContract]
        int add(user record);

        /// <summary>
        /// 更新
        /// </summary>
        [OperationContract]
        int updateById(user record);

        /// <summary>
        /// getById
        /// </summary>
        [OperationContract]
        user getById(int id);
    }
  }
