//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PW.DBCommon.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class menu
    {
        public long ID { get; set; }
        public Nullable<bool> I_FRAME { get; set; }
        public string NAME { get; set; }
        public string COMPONENT { get; set; }
        public Nullable<long> PID { get; set; }
        public Nullable<long> SORT { get; set; }
        public string ICON { get; set; }
        public string PATH { get; set; }
        public Nullable<bool> CACHE { get; set; }
        public Nullable<bool> HIDDEN { get; set; }
        public string COMPONENT_NAME { get; set; }
        public Nullable<System.DateTime> CREATE_TIME { get; set; }
        public string PERMISSION { get; set; }
        public Nullable<int> TYPE { get; set; }
        public string MODULES { get; set; }
    }
}