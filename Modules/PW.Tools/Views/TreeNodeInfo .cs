using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PW.Tools.Views
{
    public class TreeNodeInfo : INotifyPropertyChanged
    {
        public string ID { get; set; }
        public string name = "";
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }
        public string mark = "";
        public string Mark
        {
            get { return mark; }
            set
            {
                mark = value;
                RaisePropertyChanged("Mark");
            }
        }
        public bool? _IsChecked = false;
        public bool? IsChecked
        {
            get { return _IsChecked; }
            set
            {
                _IsChecked = value;
                RaisePropertyChanged("IsChecked");
            }
        }
        public bool _IsExpanded = false;
        public bool IsExpanded
        {
            get { return _IsExpanded; }
            set
            {
                _IsExpanded = value;
                RaisePropertyChanged("IsExpanded");
            }
        }
        
        public string ParentID { get; set; }


        private ObservableCollection<TreeNodeInfo> nodes = new ObservableCollection<TreeNodeInfo>();
        public ObservableCollection<TreeNodeInfo> Nodes
        {
            get
            {
                return nodes;
            }
        }


        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

   
    }
}
