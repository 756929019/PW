
  using PW.DBCommon.Model;
  using System.Collections.Generic;
  using System.ServiceModel;

  namespace PW.Service
  {
    [ServiceContract]
    public interface IServiceDict
    {
        /// <summary>
        /// 查询
        /// </summary>
        [OperationContract]
        List<dict> query(dict record);

        /// <summary>
        /// 分页查询
        /// </summary>
        [OperationContract]
        List<dict> queryPage(dict record);

        /// <summary>
        /// 删除
        /// </summary>
        [OperationContract]
        int deleteById(int id);

        /// <summary>
        /// 添加
        /// </summary>
        [OperationContract]
        int add(dict record);

        /// <summary>
        /// 更新
        /// </summary>
        [OperationContract]
        int updateById(dict record);

        /// <summary>
        /// getById
        /// </summary>
        [OperationContract]
        dict getById(int id);
    }
  }
