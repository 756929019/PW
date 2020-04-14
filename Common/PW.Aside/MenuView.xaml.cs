using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using PW.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PW.Aside
{
    /// <summary>
    /// MenuView.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(MenuView))]
    public partial class MenuView : UserControl, INavigationAware
    {
        [Import]
        public IRegionManager regionManager;
        [Import]
        public IModuleManager moduleManager;
        [Import]
        public IEventAggregator eventAggregator;

        MenuViewModel menuVm;

        [ImportingConstructor]
        public MenuView(IRegionManager regionManager, IEventAggregator eventAggregator, IModuleManager moduleManager)
        {
            this.eventAggregator = eventAggregator;
            //ChangeModuleToMenuEvent cmtmEvent = this.eventAggregator.GetEvent<ChangeModuleToMenuEvent>();
            ////这里订阅一个改变模块的事件，模块改变时修改menu
            //cmtmEvent.Subscribe(OnChangeModuleEvent);

            NavigateToScreenEvent ntsEvent = GlobalData.EventAggregator.GetEvent<NavigateToScreenEvent>();
            ntsEvent.Subscribe(OnLinkageNavigateEvent);
            HNavigateToScreenEvent hntsEvent = GlobalData.EventAggregator.GetEvent<HNavigateToScreenEvent>();
            hntsEvent.Subscribe(OnLinkageHNavigateEvent);
        }

        public void OnLinkageNavigateEvent(CommandRegionEventArgs e)
        {
            if (_contentLoaded)
            {
                LayoutRoot.DataContext = e.menuVm;
            }
            else {
                menuVm = e.menuVm;
            }
        }

        public void OnLinkageHNavigateEvent(CommandRegionEventArgs e)
        {
            if (_contentLoaded)
            {
                LayoutRoot.DataContext = e.menuVm;
            }
            else
            {
                menuVm = e.menuVm;
            }
        }

        //private void OnChangeModuleEvent(CommandMenuEventArgs obj)
        //{
        //    //这里拿到obj.menuVm
        //    LayoutRoot.DataContext = obj.menuVm;
        //}

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //throw new System.NotImplementedException();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            InitializeComponent();
            LayoutRoot.DataContext = menuVm;
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ItemTreeData treeData = (ItemTreeData)treeView.SelectedItem;
            if (treeData!=null && !string.IsNullOrEmpty(treeData.itemRegion) && !string.IsNullOrEmpty(treeData.itemView))
            {
                regionManager.RequestNavigate(treeData.itemRegion, treeData.itemView);
            }
        }

        private void menuBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton).IsChecked.Value)
            {
                Storyboard sbHide = this.Resources["sb_HideRightPart"] as Storyboard;
                if (sbHide != null)
                    sbHide.Begin();
            }
            else
            {
                Storyboard sbHide = this.Resources["sb_ShowRightPart"] as Storyboard;
                if (sbHide != null)
                    sbHide.Begin();
            }
        }
    }
}
