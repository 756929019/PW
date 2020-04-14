using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Infrastructure
{
    public class CommandRegionEventArgs : EventArgs
    {
        public string Region
        {
            get;
            set;
        }
        public string Module
        {
            get;
            set;
        }
        public MenuViewModel menuVm
        {
            get;
            set;
        }
    }
}
