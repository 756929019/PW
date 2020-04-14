using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Infrastructure
{
    public class CommandMenuEventArgs : EventArgs
    {
        public MenuViewModel menuVm
        {
            get;
            set;
        }
    }
}
