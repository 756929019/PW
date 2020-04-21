
  using PW.DBCommon.Dao;
  using PW.DBCommon.Model;
  using System.Collections.Generic;

  namespace PW.Service
  {
    public class ServiceDictDetail : IServiceDictDetail
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<dict_detail> query(dict_detail record)
        {
            return new DictDetailDao().query(record);
        }
 
        /// <summary>
        /// 分页查询
        /// </summary>
        public List<dict_detail> queryPage(dict_detail record)
        {
            return new DictDetailDao().queryPage(record);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int deleteById(int id)
        {
            return new DictDetailDao().deleteById(id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(dict_detail record)
        {
            return new DictDetailDao().add(record);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(dict_detail record)
        {
            return new DictDetailDao().updateById(record);
        }

        /// <summary>
        /// getById
        /// </summary>
        public dict_detail getById(int id)
        {
            return new DictDetailDao().getById(id);
        }
    }
  }
