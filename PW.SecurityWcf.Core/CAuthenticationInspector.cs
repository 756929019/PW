using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;

namespace PW.SecurityWcf.Core
{
    public class CAuthenticationInspector : IClientMessageInspector
    {
        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        { 
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
           // MessageHeader hdUserId = MessageHeader.CreateHeader("UserId", "www.test.com", LoginInfo.UserId);
           // MessageHeader hdPassword = MessageHeader.CreateHeader("Password", "www.test.com", LoginInfo.Password);
            MessageHeader hdToken = MessageHeader.CreateHeader("Token", "www.test.com", LoginInfo.Token);

            //request.Headers.Add(hdUserId);
            //request.Headers.Add(hdPassword); //为了安全性可以不传密码
            request.Headers.Add(hdToken);

            return null;
        }
    }
}
