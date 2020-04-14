using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PW.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IServiceLogin
    {

        [OperationContract]
        bool Login(string username, string pwd);

        [OperationContract]
        UserInfo GetUserInfo(string userid);

        // TODO: 在此添加您的服务操作
    }


    // 使用下面示例中说明的数据约定将复合类型添加到服务操作。
    [DataContract]
    public class UserInfo
    {
        string _userid;
        string _username;
        string _fullname;
        string _role;

        [DataMember]
        public string userid
        {
            get { return _userid; }
            set { _userid = value; }
        }

        [DataMember]
        public string username
        {
            get { return _username; }
            set { _username = value; }
        }

        [DataMember]
        public string fullname
        {
            get { return _fullname; }
            set { _fullname = value; }
        }

        [DataMember]
        public string role
        {
            get { return _role; }
            set { _role = value; }
        }
    }
}
