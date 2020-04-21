
  using PW.DBCommon.Dao;
  using PW.DBCommon.Model;
  using System.Collections.Generic;

  namespace PW.Service
  {
    public class ServiceDept : IServiceDept
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<dept> query(dept record)
        {
            return new DeptDao().query(record);
        }
 
        /// <summary>
        /// 分页查询
        /// </summary>
        public List<dept> queryPage(dept record)
        {
            return new DeptDao().queryPage(record);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int deleteById(int id)
        {
            return new DeptDao().deleteById(id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(dept record)
        {
            return new DeptDao().add(record);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(dept record)
        {
            return new DeptDao().updateById(record);
        }

        /// <summary>
        /// getById
        /// </summary>
        public dept getById(int id)
        {
            return new DeptDao().getById(id);
        }
    }
  }
