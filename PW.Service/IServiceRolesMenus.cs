
  using PW.DBCommon.Model;
  using System.Collections.Generic;
  using System.ServiceModel;

  namespace PW.Service
  {
    [ServiceContract]
    public interface IServiceRolesMenus
    {
        /// <summary>
        /// 查询
        /// </summary>
        [OperationContract]
        List<roles_menus> query(roles_menus record);

        /// <summary>
        /// 分页查询
        /// </summary>
        [OperationContract]
        List<roles_menus> queryPage(roles_menus record);

        /// <summary>
        /// 删除
        /// </summary>
        [OperationContract]
        int deleteById(int id, int menuid);

        /// <summary>
        /// 添加
        /// </summary>
        [OperationContract]
        int add(roles_menus record);

        /// <summary>
        /// 更新
        /// </summary>
        [OperationContract]
        int updateById(roles_menus record);

        /// <summary>
        /// getById
        /// </summary>
        [OperationContract]
        roles_menus getById(int id, int menuid);
    }
  }
