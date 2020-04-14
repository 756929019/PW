using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PW.SocketServer
{
    public class ClientUser
    {
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public string LoginTime { get; set; }
        public string CliIp { get; set; }
        public string CliPort { get; set; }
        public Socket ClientSocket { get; set; }
    }
}
