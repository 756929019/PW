using PW.Infrastructure;
using PW.ServiceCenter.ServiceUser;
using System.Collections.Generic;

namespace PW.ServiceCenter
{
    public class CServiceUser
    {
        #region 查询
        public event System.EventHandler<ServicesEventArgs<user[]>> queryCompleted;
        public void query(user record)
        {
            ServiceUserClient client = new ServiceUserClient();
            client.queryCompleted += (sender, e) =>
            {
                ServicesEventArgs<user[]> arg = new ServicesEventArgs<user[]>();

                if (e.Error == null)
                {
                    arg.Result = e.Result;
                    arg.Succesed = true;
                }
                else
                {
                    arg.Succesed = false;
                    arg.Error = e.Error;
                }
                if (queryCompleted != null)
                {
                    queryCompleted.Invoke(this, arg);
                }
            };
            client.queryAsync(record);
        }
        #endregion
    }
}