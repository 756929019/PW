
  using PW.DBCommon.Dao;
  using PW.DBCommon.Model;
  using System.Collections.Generic;

  namespace PW.Service
  {
    public class ServiceUserAvatar : IServiceUserAvatar
    {
        /// <summary>
        /// 查询
        /// </summary>
        public List<user_avatar> query(user_avatar record)
        {
            return new UserAvatarDao().query(record);
        }
 
        /// <summary>
        /// 分页查询
        /// </summary>
        public List<user_avatar> queryPage(user_avatar record)
        {
            return new UserAvatarDao().queryPage(record);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public int deleteById(int id)
        {
            return new UserAvatarDao().deleteById(id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public int add(user_avatar record)
        {
            return new UserAvatarDao().add(record);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int updateById(user_avatar record)
        {
            return new UserAvatarDao().updateById(record);
        }

        /// <summary>
        /// getById
        /// </summary>
        public user_avatar getById(int id)
        {
            return new UserAvatarDao().getById(id);
        }
    }
  }
