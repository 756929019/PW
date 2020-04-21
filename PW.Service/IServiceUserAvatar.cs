
  using PW.DBCommon.Model;
  using System.Collections.Generic;
  using System.ServiceModel;

  namespace PW.Service
  {
    [ServiceContract]
    public interface IServiceUserAvatar
    {
        /// <summary>
        /// 查询
        /// </summary>
        [OperationContract]
        List<user_avatar> query(user_avatar record);

        /// <summary>
        /// 分页查询
        /// </summary>
        [OperationContract]
        List<user_avatar> queryPage(user_avatar record);

        /// <summary>
        /// 删除
        /// </summary>
        [OperationContract]
        int deleteById(int id);

        /// <summary>
        /// 添加
        /// </summary>
        [OperationContract]
        int add(user_avatar record);

        /// <summary>
        /// 更新
        /// </summary>
        [OperationContract]
        int updateById(user_avatar record);

        /// <summary>
        /// getById
        /// </summary>
        [OperationContract]
        user_avatar getById(int id);
    }
  }
