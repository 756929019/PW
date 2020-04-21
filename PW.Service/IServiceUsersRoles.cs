
  using PW.DBCommon.Model;
  using System.Collections.Generic;
  using System.ServiceModel;

  namespace PW.Service
  {
    [ServiceContract]
    public interface IServiceUsersRoles
    {
        /// <summary>
        /// 查询
        /// </summary>
        [OperationContract]
        List<users_roles> query(users_roles record);

        /// <summary>
        /// 分页查询
        /// </summary>
        [OperationContract]
        List<users_roles> queryPage(users_roles record);

        /// <summary>
        /// 删除
        /// </summary>
        [OperationContract]
        int deleteById(int id,int roleid);

        /// <summary>
        /// 添加
        /// </summary>
        [OperationContract]
        int add(users_roles record);

        /// <summary>
        /// 更新
        /// </summary>
        [OperationContract]
        int updateById(users_roles record);

        /// <summary>
        /// getById
        /// </summary>
        [OperationContract]
        users_roles getById(int id, int roleid);
    }
  }
