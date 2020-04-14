using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Infrastructure
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ItemTreeData> itemTreeDataList = new ObservableCollection<ItemTreeData>();
        public ObservableCollection<ItemTreeData> ItemTreeDataList
        {
            get { return itemTreeDataList; }
            set
            {
                itemTreeDataList = value;
                NotifyPropertyChanged("ItemTreeDataList");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ItemTreeData // 自定义Item的树形结构
    {
        public int itemId { get; set; }      // ID
        public string itemName { get; set; } // 名称
        public string itemView { get; set; }
        public string itemRegion { get; set; }
        public string itemIcon { get; set; } // 

        private ObservableCollection<ItemTreeData> _children = new ObservableCollection<ItemTreeData>();
        public ObservableCollection<ItemTreeData> Children
        {  // 树形结构的下一级列表
            get
            {
                return _children;
            }
            set
            {
                _children = value;
            }
        }

        public bool IsExpanded { get; set; } // 节点是否展开
        public bool IsSelected { get; set; } // 节点是否选中
    }
}
