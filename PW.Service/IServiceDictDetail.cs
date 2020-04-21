
  using PW.DBCommon.Model;
  using System.Collections.Generic;
  using System.ServiceModel;

  namespace PW.Service
  {
    [ServiceContract]
    public interface IServiceDictDetail
    {
        /// <summary>
        /// 查询
        /// </summary>
        [OperationContract]
        List<dict_detail> query(dict_detail record);

        /// <summary>
        /// 分页查询
        /// </summary>
        [OperationContract]
        List<dict_detail> queryPage(dict_detail record);

        /// <summary>
        /// 删除
        /// </summary>
        [OperationContract]
        int deleteById(int id);

        /// <summary>
        /// 添加
        /// </summary>
        [OperationContract]
        int add(dict_detail record);

        /// <summary>
        /// 更新
        /// </summary>
        [OperationContract]
        int updateById(dict_detail record);

        /// <summary>
        /// getById
        /// </summary>
        [OperationContract]
        dict_detail getById(int id);
    }
  }
