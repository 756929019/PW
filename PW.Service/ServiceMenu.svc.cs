
  using PW.DBCommon.Dao;
  using PW.DBCommon.Model;
  using System.Collections.Generic;

  namespace PW.Service
  {
    public class ServiceMenu : IServiceMenu
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<menu> query(menu record)
        {
            return new MenuDao().query(record);
        }
 
        /// <summary>
        /// 分页查询
        /// </summary>
        public List<menu> queryPage(menu record)
        {
            return new MenuDao().queryPage(record);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int deleteById(int id)
        {
            return new MenuDao().deleteById(id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(menu record)
        {
            return new MenuDao().add(record);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(menu record)
        {
            return new MenuDao().updateById(record);
        }

        /// <summary>
        /// getById
        /// </summary>
        public menu getById(int id)
        {
            return new MenuDao().getById(id);
        }
    }
  }
