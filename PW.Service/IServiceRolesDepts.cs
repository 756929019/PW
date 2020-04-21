
  using PW.DBCommon.Model;
  using System.Collections.Generic;
  using System.ServiceModel;

  namespace PW.Service
  {
    [ServiceContract]
    public interface IServiceRolesDepts
    {
        /// <summary>
        /// 查询
        /// </summary>
        [OperationContract]
        List<roles_depts> query(roles_depts record);

        /// <summary>
        /// 分页查询
        /// </summary>
        [OperationContract]
        List<roles_depts> queryPage(roles_depts record);

        /// <summary>
        /// 删除
        /// </summary>
        [OperationContract]
        int deleteById(int id,int deptid);

        /// <summary>
        /// 添加
        /// </summary>
        [OperationContract]
        int add(roles_depts record);

        /// <summary>
        /// 更新
        /// </summary>
        [OperationContract]
        int updateById(roles_depts record);

        /// <summary>
        /// getById
        /// </summary>
        [OperationContract]
        roles_depts getById(int id, int deptid);
    }
  }
