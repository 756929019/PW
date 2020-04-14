using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Common
{
    public class UserInfo
    {
        public string userid
        {
            get;
            set;
        }

        public string username
        {
            get;
            set;
        }

        public string fullname
        {
            get;
            set;
        }

        public string role
        {
            get;
            set;
        }

        public static implicit operator bool(UserInfo v)
        {
            throw new NotImplementedException();
        }
    }
}
