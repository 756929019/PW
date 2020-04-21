
  using PW.DBCommon.Dao;
  using PW.DBCommon.Model;
  using System.Collections.Generic;

  namespace PW.Service
  {
    public class ServiceRolesMenus : IServiceRolesMenus
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<roles_menus> query(roles_menus record)
        {
            return new RolesMenusDao().query(record);
        }
 
        /// <summary>
        /// 分页查询
        /// </summary>
        public List<roles_menus> queryPage(roles_menus record)
        {
            return new RolesMenusDao().queryPage(record);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int deleteById(int id, int menuid)
        {
            return new RolesMenusDao().deleteById(id, menuid);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(roles_menus record)
        {
            return new RolesMenusDao().add(record);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(roles_menus record)
        {
            return new RolesMenusDao().updateById(record);
        }

        /// <summary>
        /// getById
        /// </summary>
        public roles_menus getById(int id,int menuid)
        {
            return new RolesMenusDao().getById(id, menuid);
        }
    }
  }
