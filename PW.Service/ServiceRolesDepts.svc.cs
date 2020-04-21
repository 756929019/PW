
  using PW.DBCommon.Dao;
  using PW.DBCommon.Model;
  using System.Collections.Generic;

  namespace PW.Service
  {
    public class ServiceRolesDepts : IServiceRolesDepts
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<roles_depts> query(roles_depts record)
        {
            return new RolesDeptsDao().query(record);
        }
 
        /// <summary>
        /// 分页查询
        /// </summary>
        public List<roles_depts> queryPage(roles_depts record)
        {
            return new RolesDeptsDao().queryPage(record);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int deleteById(int id, int deptid)
        {
            return new RolesDeptsDao().deleteById(id, deptid);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(roles_depts record)
        {
            return new RolesDeptsDao().add(record);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(roles_depts record)
        {
            return new RolesDeptsDao().updateById(record);
        }

        /// <summary>
        /// getById
        /// </summary>
        public roles_depts getById(int id, int deptid)
        {
            return new RolesDeptsDao().getById(id,deptid);
        }
    }
  }
