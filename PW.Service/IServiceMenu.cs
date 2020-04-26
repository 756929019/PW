
  using PW.DBCommon.Model;
  using System.Collections.Generic;
  using System.ServiceModel;

  namespace PW.Service
  {
    [ServiceContract]
    public interface IServiceMenu
    {
        /// <summary>
        /// 查询
        /// </summary>
        [OperationContract]
        List<menu> query(menu record);

        /// <summary>
        /// 分页查询
        /// </summary>
        [OperationContract]
        PageInfo<menu> queryPage(PageInfo<menu> page);

        /// <summary>
        /// 删除
        /// </summary>
        [OperationContract]
        int deleteById(int id);

        /// <summary>
        /// 添加
        /// </summary>
        [OperationContract]
        int add(menu record);

        /// <summary>
        /// 更新
        /// </summary>
        [OperationContract]
        int updateById(menu record);

        /// <summary>
        /// getById
        /// </summary>
        [OperationContract]
        menu getById(int id);
    }
  }
