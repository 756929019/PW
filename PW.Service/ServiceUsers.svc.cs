using PW.DBCommon.Dao;
using PW.DBCommon.Model;
using System.Collections.Generic;

namespace PW.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 Service1.svc 或 Service1.svc.cs，然后开始调试。
    public class ServiceUsers : IServiceUsers
    {
        public List<users> query(users user)
        {
            return new UsersDao().query(user);
        }

        public List<users> queryPage(users user)
        {
            return new UsersDao().queryPage(user);
        }

        public int deleteById(int id)
        {
            return new UsersDao().deleteById(id);
        }

        public int add(users user)
        {
            return new UsersDao().add(user);
        }

        public int updateById(users user)
        {
            return new UsersDao().updateById(user);
        }

        public users getById(int id) {
            return new UsersDao().getById(id);
        }
    }
}
