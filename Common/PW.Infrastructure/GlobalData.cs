using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PW.Infrastructure
{
    public class GlobalData
    {
        public static string SKIN;
        public static string UserName;
        public static string NickName;
        public static string Password;
        public static string SocketIP;
        public static int SocketPort;
        public static int SocketMsgSize;
        public static IEventAggregator EventAggregator;
        public static IRegionManager RegionManager;
        public static IModuleCatalog ModuleCatalog;
        public static List<string> LoadModule;
        public static List<NavModuleInfo> NavModules;
        public static IModuleManager ModuleManager
        {
            get;
            set;
        }
        static GlobalData()
        {
            GlobalData.SKIN = "SC";
            GlobalData.UserName = "root";
            GlobalData.NickName = "张三";
            GlobalData.Password = "root";
            GlobalData.SocketIP = "192.168.5.149";
            GlobalData.SocketPort = 8081;
            GlobalData.SocketMsgSize = 1048576;
            LoadModule = new List<string>();
            NavModules = new List<NavModuleInfo>();
        }
        public static bool IsLoadModule(string moduleName)
        {
            return LoadModule.Contains(moduleName);
        }
        
    }

    public class NavModuleInfo {
        public string region { get; set; }
        public string module { get; set; }
        public ImageSource img { get; set; }
        public string icon { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public MenuViewModel menuVm { get; set; }
    }
}
