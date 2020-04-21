using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using PW.Infrastructure;
using System;
using System.Collections.Generic;
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

namespace PW.SystemHeader
{
    /// <summary>
    /// HeaderView.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(HeaderView))]
    public partial class HeaderView : UserControl, INavigationAware
    {
        [Import]
        public IRegionManager regionManager;
        [Import]
        public IModuleManager moduleManager;

        string initCheckModule = "";

        [ImportingConstructor]
        public HeaderView(IRegionManager regionManager, IEventAggregator eventAggregator, IModuleManager moduleManager)
        {
            HNavigateToScreenEvent ntsEvent = GlobalData.EventAggregator.GetEvent<HNavigateToScreenEvent>();
            ntsEvent.Subscribe(OnLinkageNavigateEvent);
        }
        public void OnLinkageNavigateEvent(CommandRegionEventArgs e)
        {
            initCheckModule = e.Module;
        }

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

            List<NavModuleInfo> list = GlobalData.NavModules;
            if (list != null)
            {
                initBtn(navBtn1, navIco1, navTxt1, 0, list);
                initBtn(navBtn2, navIco2, navTxt2, 1, list);
                initBtn(navBtn3, navIco3, navTxt3, 2, list);
                initBtn(navBtn4, navIco4, navTxt4, 3, list);
                initBtn(navBtn5, navIco5, navTxt5, 4, list);
                initBtn(navBtn6, navIco6, navTxt6, 5, list);
                initBtn(navBtn7, navIco7, navTxt7, 6, list);
            }
        }

        private void initBtn(ToggleButton navBtn, TextBlock navIco, TextBlock navTxt, int index, List<NavModuleInfo> list)
        {
            if (list.Count > index)
            {
                navBtn.Tag = list[index];
                navIco.Text = list[index].icon;
                navTxt.Text = list[index].title;
                if (initCheckModule == list[index].module)
                {
                    navBtn.IsChecked = true;
                }
            }
            else
            {
                navBtn.Visibility = Visibility.Collapsed;
            }
        }
        private void SystemButton_Click(object sender, RoutedEventArgs e)
        {
            UncheckedButtons();
            (sender as ToggleButton).IsChecked = true;
            CommandRegionEventArgs cms = new CommandRegionEventArgs();
            cms.Region = RegionNames.Main;
            cms.Module = ((sender as ToggleButton).Tag as NavModuleInfo).module;
            cms.menuVm = ((sender as ToggleButton).Tag as NavModuleInfo).menuVm;
            GlobalData.EventAggregator.GetEvent<HNavigateToScreenEvent>().Publish(cms);
        }
        private void UncheckedButtons()
        {
            foreach (var v in btnStackPanel.Children)
            {
                if (v is ToggleButton)
                {
                    ToggleButton tb = v as ToggleButton;
                    tb.IsChecked = false;
                }
            }
        }

        private void headerBtn_Click(object sender, RoutedEventArgs e)
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
