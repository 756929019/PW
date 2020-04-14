using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PW.Tools.Views
{
    public class TableModel
    {
        public string TableName { get; set; }
        public string Comment { get; set; }
        public string NameSpace { get; set; }
        public string ModelName { get; set; }
        private List<FieldModel> _Fields;
        public List<FieldModel> Fields {
            get
            {
                if (_Fields == null) {
                    _Fields = new List<FieldModel>();
                   
                }
                return _Fields;
            }
            set { _Fields = value; }
        }
    }
}
