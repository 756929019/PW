using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.DBCommon.Model
{
    public class PageInfo<T>
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int totalCount { get; set; }
        public int totalPage { get; set; }
        public string orderName { get; set; }
        public T queryParams { get; set; }
        public List<T> list  { get; set; }
    }
}
