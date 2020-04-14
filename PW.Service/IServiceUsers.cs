using PW.DBCommon.Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace PW.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IServiceUsers
    {

        [OperationContract]
        List<users> query(users user);

        [OperationContract]
        List<users> queryPage(users user);

        [OperationContract]
        int deleteById(int id);

        [OperationContract]
        int add(users user);

        [OperationContract]
        int updateById(users user);

        [OperationContract]
        users getById(int id);

    }
}
