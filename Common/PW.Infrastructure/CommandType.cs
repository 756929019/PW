using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Infrastructure
{
    public enum CommandType
    {
        registerDefViewWithRegion = 1, // 注册模块的默认页面
        msg = 2,
        showLoading = 3,
        hideLoading = 4
    }
}
