
  using PW.DBCommon.Dao;
  using PW.DBCommon.Model;
  using System.Collections.Generic;

  namespace PW.Service
  {
    public class ServiceUser : IServiceUser
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<user> query(user record)
        {
            return new UserDao().query(record);
        }
 
        /// <summary>
        /// 分页查询
        /// </summary>
        public PageInfo<user> queryPage(PageInfo<user> page)
        {
            return new UserDao().queryPage(page);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int deleteById(int id)
        {
            return new UserDao().deleteById(id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(user record)
        {
            return new UserDao().add(record);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(user record)
        {
            return new UserDao().updateById(record);
        }

        /// <summary>
        /// getById
        /// </summary>
        public user getById(int id)
        {
            return new UserDao().getById(id);
        }
    }
  }
