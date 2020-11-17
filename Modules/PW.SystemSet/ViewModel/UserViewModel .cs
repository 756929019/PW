using PW.Common;
using PW.ServiceCenter;
using PW.ServiceCenter.ServiceUser;
using PW.SystemSet.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace PW.SystemSet.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        //构造函数
        public UserViewModel()
        {
            QueryCommand = new RelayCommand(QueryCommandFunc);
            NextPageSearchCommand = new RelayCommand(NextPageSearchCommandFunc);
            SelectAllCommand = new RelayCommand(SelectAllCommandFunc);
            UnSelectAllCommand = new RelayCommand(UnSelectAllCommandFunc);
            AddCommand = new RelayCommand(AddCommandFunc);
            DeleteCommand = new RelayCommand(DeleteCommandFunc);
            ModifyCommand = new RelayCommand(ModifyCommandFunc);
            InfoCommand = new RelayCommand(InfoCommandFunc);
            GetData();
        }

        #region 查询相关属性
        private user _queryObj = new user();
        /// <summary>
        /// Username
        /// </summary>
        public user QueryObj
        {
            get { return _queryObj; }
            set
            {
                _queryObj = value;
                RaisePropertyChanged("QueryObj");
            }
        }

        public RelayCommand QueryCommand { get; set; }

        private void QueryCommandFunc()
        {
            CurrentPage = "1";
            GetData();
        }

        //数据源
        ObservableCollection<DataGridModel<user>> _list = new ObservableCollection<DataGridModel<user>>();
        public ObservableCollection<DataGridModel<user>> list
        {

            get { return _list; }
            set
            {
                _list = value;
                RaisePropertyChanged("list");
            }
        }
        private void GetData()
        {
            var pageIndex = Convert.ToInt32(CurrentPage);
            CServiceUser client = new CServiceUser();
            client.queryPageCompleted += (serice, eve) =>
            {
                if (eve.Succesed)
                {
                    list.Clear();
                    PageInfoOfuserCLUigIiY result = eve.Result;
                    this.TotalPage = result.totalPage + "";
                    this.TotalCount = result.totalCount;
                    foreach (user item in result.list)
                    {
                        list.Add(new DataGridModel<user>() { IsChecked = false, ObjData = item });
                    }
                }
                else
                {
                }
            };
            PageInfoOfuserCLUigIiY page = new PageInfoOfuserCLUigIiY()
            {
                pageIndex = pageIndex,
                pageSize = PageSize,
                orderName = "ID ASC",
                queryParams = _queryObj
            };
            client.queryPage(JSONCom.ConvertObject<ServiceCenter.ServiceUser.PageInfoOfuserCLUigIiY>(page));
        }
        #endregion

        #region 分页相关属性
        /// <summary>
        /// 分页管理
        /// </summary>
        public RelayCommand NextPageSearchCommand { get; set; }
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
        //private int _pageIndex = 1;
        //public int PageIndex
        //{
        //    get { return _pageIndex; }
        //    set
        //    {
        //        _pageIndex = value;

        //        RaisePropertyChanged("PageIndex");
        //    }
        //}
        private int _totalCount;
        public int TotalCount
        {
            get { return _totalCount; }
            set
            {
                _totalCount = value;
                RaisePropertyChanged("TotalCount");
            }
        }
        #endregion

        #region datagrid全选
        private bool? _IsCheckedAll = false;
        /// <summary>
        /// 
        /// </summary>
        public bool? IsCheckedAll
        {
            get { return _IsCheckedAll; }
            set
            {
                _IsCheckedAll = value;
                RaisePropertyChanged("IsCheckedAll");
            }
        }

        public RelayCommand SelectAllCommand { get; set; }
        private void SelectAllCommandFunc()
        {
            foreach (DataGridModel<user> item in list)
            {
                item.IsChecked = true;
            }
        }
        public RelayCommand UnSelectAllCommand { get; set; }
        private void UnSelectAllCommandFunc()
        {
            foreach (DataGridModel<user> item in list)
            {
                item.IsChecked = false;
            }
        }
        #endregion

        #region 增删查改命令
        public RelayCommand AddCommand { get; set; }
        private void AddCommandFunc()
        {
            UserEdit edit = new UserEdit();
            edit.ShowDialog();
        }
        public RelayCommand DeleteCommand { get; set; }
        private void DeleteCommandFunc()
        {
            // 支持多选
            var res = MessageBoxX.Question("确认删除所选数据？");
            MessageBoxX.Info(res.ToString());
        }
        public RelayCommand ModifyCommand { get; set; }
        private void ModifyCommandFunc()
        {
            UserEdit edit = new UserEdit();
            edit.ShowDialog();
        }
        public RelayCommand InfoCommand { get; set; }
        private void InfoCommandFunc()
        {
            UserEdit edit = new UserEdit();
            edit.ShowDialog();
        }
        #endregion

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
