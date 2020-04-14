using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Common
{
    public class Log
    {
        public static void error(Exception ex) {
            StringBuilder msg = new StringBuilder();
            msg.Append("*************************************** \n");
            msg.AppendFormat(" 异常发生时间： {0} \n", DateTime.Now);
            msg.AppendFormat(" 异常类型： {0} \n", ex.HResult);
            msg.AppendFormat(" 导致当前异常的 Exception 实例： {0} \n", ex.InnerException);
            msg.AppendFormat(" 导致异常的应用程序或对象的名称： {0} \n", ex.Source);
            msg.AppendFormat(" 引发异常的方法： {0} \n", ex.TargetSite);
            msg.AppendFormat(" 异常堆栈信息： {0} \n", ex.StackTrace);
            msg.AppendFormat(" 异常消息： {0} \n", ex.Message);
            msg.Append("***************************************");

            error(msg.ToString());
        }

        public static void error(String str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(str);
        }

        public static void info(String str)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("INFO:[" + DateTime.Now.ToString() + "] " + str);
        }
    }
}
