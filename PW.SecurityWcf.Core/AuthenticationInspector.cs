using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Dispatcher;

namespace PW.SecurityWcf.Core
{
    public class AuthenticationInspector : IDispatchMessageInspector  
    {
        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            //注意引用 System.Runtime.Serialization

           // string UserId = request.Headers.GetHeader<string>("UserId", "www.test.com");
           // string Password = request.Headers.GetHeader<string>("Password", "www.test.com");
            string Token = request.Headers.GetHeader<string>("Token", "www.test.com");

            if (Token != "ABC")
            {
                throw new Exception("未经授权的访问！");
            }

            //可从后台数据库中进行验证
            /*
            if (UserId != "admin" && Password != "123456")
            {
                throw new Exception("用户名和密码不正确！");
            }
             */

            return null;  
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
        }
    }
}