
  using PW.DBCommon.Dao;
  using PW.DBCommon.Model;
  using System.Collections.Generic;

  namespace PW.Service
  {
    public class ServiceRole : IServiceRole
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<role> query(role record)
        {
            return new RoleDao().query(record);
        }
 
        /// <summary>
        /// 分页查询
        /// </summary>
        public List<role> queryPage(role record)
        {
            return new RoleDao().queryPage(record);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int deleteById(int id)
        {
            return new RoleDao().deleteById(id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(role record)
        {
            return new RoleDao().add(record);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(role record)
        {
            return new RoleDao().updateById(record);
        }

        /// <summary>
        /// getById
        /// </summary>
        public role getById(int id)
        {
            return new RoleDao().getById(id);
        }
    }
  }
