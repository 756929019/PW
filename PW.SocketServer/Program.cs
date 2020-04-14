using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace PW.SocketServer
{
    static class Program
    {
        static MyServer myServer = null;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            myServer = MyServer.GetInstance();
            //myServer.ReceivedMsg = new MyServer.ReceivedMsgHandler(socketClient_ReceivedMsg);
           
            //Application.Run(new FormMain());

           
            FormMain main = null;

            new Thread((ThreadStart)delegate
            {
                main = new FormMain();
                Application.Run(main);
            }).Start();

            myServer.BeginServer();
            Console.ReadLine();
        }
    }
}
