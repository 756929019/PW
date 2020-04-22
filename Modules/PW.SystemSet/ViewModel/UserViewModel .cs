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
            QueryCommand = new RelayCommand(QueryCommandFunc);
            NextPageSearchCommand = new RelayCommand(NextPageSearchCommandFunc);
            GetData();
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
        private void GetData() {
            var pageIndex = Convert.ToInt32(CurrentPage);
            CServiceUser client = new CServiceUser();
            client.queryPageCompleted += (serice, eve) =>
            {
                if (eve.Succesed)
                {
                    list.Clear();
                    ServiceCenter.ServiceUser.PageInfoOfuserCLUigIiY result = eve.Result;
                    this.TotalPage = result.totalPage + "";
                    this.TotalCount = result.totalCount;
                    foreach (ServiceCenter.ServiceUser.user item in result.list)
                    {
                        list.Add(JSONCom.ConvertObject<user>(item));
                    }
                }
                else
                {
                }
            };
            PageInfo<user> page = new PageInfo<user>()
            {
                pageIndex = pageIndex,
                pageSize = PageSize,
                orderName = "ID ASC",
                queryParams = new user
                {
                    USERNAME = Username
                }
            };
            client.queryPage(JSONCom.ConvertObject<ServiceCenter.ServiceUser.PageInfoOfuserCLUigIiY>(page));
        }

        private string _Username;
        /// <summary>
        /// Username
        /// </summary>
        public string Username
        {
            get { return _Username; }
            set
            {
                _Username = value;
                RaisePropertyChanged("Username");
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

        public RelayCommand QueryCommand { get; set; }

        private void QueryCommandFunc()
        {
            GetData();
        }

        #region 分页相关属性

        /// <summary>
        /// 分页查询命令
        /// </summary>
        private void NextPageSearchCommandFunc()
        {
            GetData();
        }
        private string _totalPage = string.Empty;
        /// <summary>
        /// 总页数
        /// </summary>
        public string TotalPage
        {
            get { return _totalPage; }
            set
            {
                _totalPage = value;
                RaisePropertyChanged("TotalPage");
            }
        }

        private string _currentPage = "1";
        /// <summary>
        /// 当前页
        /// </summary>
        public string CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                RaisePropertyChanged("CurrentPage");
            }
        }

        private int _pageSize = 10;
        /// <summary>
        /// 每页显示的记录数
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value;
                RaisePropertyChanged("PageSize");
            }
        }
        private int _pageIndex = 1;
        private int _totalCount;
        public int PageIndex
        {
            get { return _pageIndex; }
            set
            {
                _pageIndex = value;

                RaisePropertyChanged("PageIndex");
            }
        }

        public int TotalCount
        {
            get { return _totalCount; }
            set
            {
                _totalCount = value;
                RaisePropertyChanged("TotalCount");
            }
        }
        /// <summary>
        /// 分页管理
        /// </summary>
        public RelayCommand NextPageSearchCommand { get; set; }
        #endregion
    }
}
