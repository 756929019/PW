using PW.Common;
using PW.DBCommon.Model;
using PW.ServiceCenter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.SystemSet.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        //构造函数
        public UserViewModel()
        {
            CServiceUser client = new CServiceUser();
            client.queryCompleted += (serice, eve) =>
            {
                if (eve.Succesed)
                {
                    list.Clear();
                    foreach(ServiceCenter.ServiceUser.user item in eve.Result) {
                        list.Add(JSONCom.ConvertObject<user>(item));
                    }
                }
                else
                {
                }
            };
            client.query(null);
        }

        //数据源
        ObservableCollection<user> _list = new ObservableCollection<user>();
        public ObservableCollection<user> list
        {

            get { return _list; }
            set
            {
                _list = value;
                RaisePropertyChanged("list");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (propertyName != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
