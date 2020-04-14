using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Infrastructure
{
    public class CommandEventArgs : EventArgs
    {
        public CommandType Type
        {
            get;
            set;
        }
        public string ModuleName
        {
            get;
            set;
        }
        public string Msg
        {
            get;
            set;
        }
    }
}
