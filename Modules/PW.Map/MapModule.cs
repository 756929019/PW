using Prism.Modularity;
using Prism.Mef.Modularity;
using Prism.Regions;
using System.ComponentModel.Composition;
using PW.Infrastructure;
using System;
using System.Utility.Helper;
using System.Collections.ObjectModel;
using PW.Common;

namespace PW.Map
{
    [ModuleExport(typeof(MapModule), InitializationMode = InitializationMode.OnDemand)]
    public class MapModule : IModule
    {
        private readonly IModuleTracker moduleTracker;
        private readonly IRegionManager regionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapModule"/> class.
        /// </summary>
        /// <param name="moduleTracker">The module tracker.</param>
        [ImportingConstructor]
        public MapModule(IModuleTracker moduleTracker, IRegionManager regionManager)
        {
            if (moduleTracker == null)
            {
                throw new ArgumentNullException("moduleTracker");
            }

            this.moduleTracker = moduleTracker;
            this.moduleTracker.RecordModuleConstructed(ModuleNames.Map);
            this.regionManager = regionManager;
            CommandEvent cmdEvent = GlobalData.EventAggregator.GetEvent<CommandEvent>();
            cmdEvent.Subscribe(OnCommandEvent);
        }
        private void OnCommandEvent(CommandEventArgs e)
        {
            Log.info("MapModule OnCommandEvent");
            if (e.Type == CommandType.registerDefViewWithRegion)
            {
                regionManager.RegisterViewWithRegion(RegionNames.Main, typeof(MapModule_MainView));
            }
        }


        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            ObservableCollection<ItemTreeData> chi = new ObservableCollection<ItemTreeData>();
            chi.Add(new ItemTreeData() { itemId = 1, itemName = "MapDef", itemIcon = "\xe63c", itemRegion = RegionNames.Map, itemView = "ViewDef" });
            chi.Add(new ItemTreeData() { itemId = 1, itemName = "Map1", itemIcon = "\xe63c", itemRegion = RegionNames.Map, itemView = "View1" });
            chi.Add(new ItemTreeData() { itemId = 1, itemName = "Map2", itemIcon = "\xe63c", itemRegion = RegionNames.Map, itemView = "View2" });
            chi.Add(new ItemTreeData() { itemId = 1, itemName = "Map3", itemIcon = "\xe63c" });
            chi.Add(new ItemTreeData() { itemId = 1, itemName = "Map4", itemIcon = "\xe63c" });
            chi.Add(new ItemTreeData() { itemId = 1, itemName = "Map5", itemIcon = "\xe63c" });
            MenuViewModel vm = new MenuViewModel();
            vm.ItemTreeDataList.Add(new ItemTreeData() { itemId = 1, itemName = "Map", itemIcon = "\xe63c", Children = chi });
            vm.ItemTreeDataList.Add(new ItemTreeData() { itemId = 1, itemName = "地图显示", itemIcon = "\xe63c" });
            vm.ItemTreeDataList.Add(new ItemTreeData() { itemId = 1, itemName = "Map", itemIcon = "\xe62f" });
            vm.ItemTreeDataList.Add(new ItemTreeData() { itemId = 1, itemName = "Map", itemIcon = "\xe643" });
            vm.ItemTreeDataList.Add(new ItemTreeData() { itemId = 1, itemName = "Map", itemIcon = "\xe643" });
            vm.ItemTreeDataList.Add(new ItemTreeData() { itemId = 1, itemName = "Map", itemIcon = "\xe643" });
            vm.ItemTreeDataList.Add(new ItemTreeData() { itemId = 1, itemName = "Map", itemIcon = "\xe643" });

            GlobalData.NavModules.Add(new NavModuleInfo() {
                region = RegionNames.Main,
                module = ModuleNames.Map,
                title = ModuleTitle.Map,
                icon = "\xe63c",
                img = Images.CreateImageSourceFromImage(Properties.Resources.icon),
                menuVm = vm
            });
            this.moduleTracker.RecordModuleInitialized(ModuleNames.Map);
            // regionManager.RegisterViewWithRegion(RegionNames.Main, typeof(MapModule_MainView));
        }
    }
}
