using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using PW.Infrastructure;
using PW.Map.Views;
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

namespace PW.Map
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(MapModule_MainView))]
    public partial class MapModule_MainView : UserControl
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
        public MapModule_MainView(IRegionManager regionManager, IEventAggregator eventAggregator, IRegionViewRegistry regionViewRegistry)
        {
            InitializeComponent();
            this.regionManager = regionManager;
            this.regionViewRegistry = regionViewRegistry;
            this.eventAggregator = eventAggregator;
            //RegionManager.SetRegionManager(mapContentControl, this.regionManager);
            //RegionManager.SetRegionName(mapContentControl, RegionNames.Map);
            regionViewRegistry.RegisterViewWithRegion(RegionNames.Map, typeof(ViewDef));
            regionViewRegistry.RegisterViewWithRegion(RegionNames.Map, typeof(View1));
            regionViewRegistry.RegisterViewWithRegion(RegionNames.Map, typeof(View2));
            regionManager.RequestNavigate(RegionNames.Map, "ViewDef");
        }
    }
}
