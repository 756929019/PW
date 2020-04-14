using PW.DBCommon.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PW.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“ServiceStatistics”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 ServiceStatistics.svc 或 ServiceStatistics.svc.cs，然后开始调试。
    public class ServiceStatistics : IServiceStatistics
    {
        public DataTable AgeCnts()
        {
            return DBBLL.AgeCnts();
        }

        public DataTable AddrCnts()
        {
            return DBBLL.AddrCnts();
        }

        public DataTable DateCnts()
        {
            return DBBLL.DateCnts();
        }
        public DataTable NameTags()
        {
            return DBBLL.NameTags();
        }
    }
}
