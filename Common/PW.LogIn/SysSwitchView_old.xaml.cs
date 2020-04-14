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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PW.LogIn
{
    /// <summary>
    /// SysSwitchView.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(SysSwitchView_old))]
    public partial class SysSwitchView_old : UserControl, INavigationAware
    {
        [Import]
        public IRegionManager regionManager;
        [Import]
        public IModuleManager moduleManager;

        [ImportingConstructor]
        public SysSwitchView_old(IRegionManager regionManager, IEventAggregator eventAggregator, IModuleManager moduleManager)
        {
            
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
            if (list != null )
            {
                initBtn(navBtn1, navImg1, navTxt1, 0, list);
                initBtn(navBtn2, navImg2, navTxt2, 1, list);
                initBtn(navBtn3, navImg3, navTxt3, 2, list);
                initBtn(navBtn4, navImg4, navTxt4, 3, list);
                initBtn(navBtn5, navImg5, navTxt5, 4, list);
                initBtn(navBtn6, navImg6, navTxt6, 5, list);
                initBtn(navBtn7, navImg7, navTxt7, 6, list);
            }
        }

        private void btnFlow()
        {

        }

        private void initBtn(Button navBtn, ImageBrush navImg, TextBlock navTxt, int index, List<NavModuleInfo> list)
        {
            if (list.Count > index)
            {
                navBtn.Tag = list[index];
                navImg.ImageSource = list[index].img;
                navTxt.Text = list[index].title;
            }
            else
            {
                navBtn.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CommandRegionEventArgs cms = new CommandRegionEventArgs();
            cms.Region = RegionNames.Main;
            cms.Module = ((sender as Button).Tag as NavModuleInfo).module;
            cms.menuVm = ((sender as Button).Tag as NavModuleInfo).menuVm;
            GlobalData.EventAggregator.GetEvent<NavigateToScreenEvent>().Publish(cms);
        }

       
    }
   
}
