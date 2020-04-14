using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;

namespace PW.SecurityWcf.Core
{
    public class AuthenticationBehaviorExtension : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(AuthenticationBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new AuthenticationBehavior();
        }
    }
}
