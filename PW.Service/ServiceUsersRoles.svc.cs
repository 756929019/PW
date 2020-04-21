
  using PW.DBCommon.Dao;
  using PW.DBCommon.Model;
  using System.Collections.Generic;

  namespace PW.Service
  {
    public class ServiceUsersRoles : IServiceUsersRoles
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<users_roles> query(users_roles record)
        {
            return new UsersRolesDao().query(record);
        }
 
        /// <summary>
        /// 分页查询
        /// </summary>
        public List<users_roles> queryPage(users_roles record)
        {
            return new UsersRolesDao().queryPage(record);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int deleteById(int id,int roleid)
        {
            return new UsersRolesDao().deleteById(id, roleid);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(users_roles record)
        {
            return new UsersRolesDao().add(record);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(users_roles record)
        {
            return new UsersRolesDao().updateById(record);
        }

        /// <summary>
        /// getById
        /// </summary>
        public users_roles getById(int id, int roleid)
        {
            return new UsersRolesDao().getById(id,roleid);
        }
    }
  }
