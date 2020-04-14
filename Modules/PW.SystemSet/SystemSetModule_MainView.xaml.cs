using Prism.Events;
using Prism.Regions;
using PW.Infrastructure;
using PW.SystemSet.Views;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PW.SystemSet
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(SystemSetModule_MainView))]
    public partial class SystemSetModule_MainView : UserControl
    {
        [Import]
        public IRegionManager regionManager;
        [Import]
        public IEventAggregator eventAggregator;
        [Import]
        public IModuleTracker moduleTracker;
        [Import]
        public IRegionViewRegistry regionViewRegistry;

        [ImportingConstructor]
        public SystemSetModule_MainView(IRegionManager regionManager, IEventAggregator eventAggregator, IRegionViewRegistry regionViewRegistry)
        {
            InitializeComponent();
            this.regionManager = regionManager;
            this.regionViewRegistry = regionViewRegistry;
            this.eventAggregator = eventAggregator;
            regionViewRegistry.RegisterViewWithRegion(RegionNames.SystemSet, typeof(StyleSetting));
            //regionManager.RequestNavigate(RegionNames.SystemSet, "StyleSetting");
        }
    }
}
