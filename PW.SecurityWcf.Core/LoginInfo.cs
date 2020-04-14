using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PW.SecurityWcf.Core
{
    public static class LoginInfo
    {
        public static string UserId { get; set; }

        public static string Password { get; set; }

        public static string Token { get; set; }

        public static bool IsRemember { get; set; }
    }
}
