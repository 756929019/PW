using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Common
{
    public class DataGridModel<T> : INotifyPropertyChanged
    {
        
        private bool _IsChecked = false;
        /// <summary>
        /// 
        /// </summary>
        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                _IsChecked = value;
                RaisePropertyChanged("IsChecked");
            }
        }

        private T _objData;
        /// <summary>
        /// 
        /// </summary>
        public T ObjData
        {
            get { return _objData; }
            set
            {
                _objData = value;
                RaisePropertyChanged("ObjData");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
