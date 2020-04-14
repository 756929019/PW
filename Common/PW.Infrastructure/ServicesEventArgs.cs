using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Infrastructure
{
    public class ServicesEventArgs<ResultType> : EventArgs
    {
        public bool Succesed
        {
            get;
            set;
        }
        public ResultType Result
        {
            get;
            set;
        }
        public Exception Error
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public object UserState
        {
            get;
            set;
        }
        public ServicesEventArgs()
        {
            this.Succesed = false;
            this.Error = null;
            this.Name = string.Empty;
            this.UserState = null;
        }
    }
}
