
  using PW.DBCommon.Dao;
  using PW.DBCommon.Model;
  using System.Collections.Generic;

  namespace PW.Service
  {
    public class ServiceDict : IServiceDict
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<dict> query(dict record)
        {
            return new DictDao().query(record);
        }
 
        /// <summary>
        /// 分页查询
        /// </summary>
        public List<dict> queryPage(dict record)
        {
            return new DictDao().queryPage(record);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int deleteById(int id)
        {
            return new DictDao().deleteById(id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(dict record)
        {
            return new DictDao().add(record);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(dict record)
        {
            return new DictDao().updateById(record);
        }

        /// <summary>
        /// getById
        /// </summary>
        public dict getById(int id)
        {
            return new DictDao().getById(id);
        }
    }
  }
