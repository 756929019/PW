using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.DBCommon.Model
{
    public class column
    {
        public string column_name { get; set; }
        public string is_nullable { get; set; }
        public long? ordinal_position { get; set; }
        public string data_type { get; set; }
        public long? character_maximum_length { get; set; }
        public string column_key { get; set; }
        public string column_comment { get; set; }
    }
}
