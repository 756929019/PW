using Prism.Modularity;
using Prism.Mef.Modularity;
using Prism.Regions;
using System.ComponentModel.Composition;
using PW.Infrastructure;
using System;
using System.Utility.Helper;
using System.Collections.ObjectModel;
using PW.Common;

namespace PW.SystemSet
{
    [ModuleExport( typeof(SystemSetModule), InitializationMode = InitializationMode.OnDemand)]
    public class SystemSetModule : IModule
    {
        private readonly IModuleTracker moduleTracker;
        private readonly IRegionManager regionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapModule"/> class.
        /// </summary>
        /// <param name="moduleTracker">The module tracker.</param>
        [ImportingConstructor]
        public SystemSetModule(IModuleTracker moduleTracker, IRegionManager regionManager)
        {
            if (moduleTracker == null)
            {
                throw new ArgumentNullException("moduleTracker");
            }

            this.moduleTracker = moduleTracker;
            this.moduleTracker.RecordModuleConstructed(ModuleNames.SystemSet);
            this.regionManager = regionManager;
            CommandEvent cmdEvent = GlobalData.EventAggregator.GetEvent<CommandEvent>();
            cmdEvent.Subscribe(OnCommandEvent);
        }
        private void OnCommandEvent(CommandEventArgs e)
        {
            Log.info("SystemSetModule OnCommandEvent");
            if (e.Type == CommandType.registerDefViewWithRegion)
            {
                regionManager.RegisterViewWithRegion(RegionNames.Main, typeof(SystemSetModule_MainView));
            }
        }


        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            ObservableCollection<ItemTreeData> chi = new ObservableCollection<ItemTreeData>();
            chi.Add(new ItemTreeData() { itemId = 1, itemName = "系统管理", itemIcon = "\xe633", itemRegion = RegionNames.SystemSet, itemView = "StyleSetting" });
            chi.Add(new ItemTreeData() { itemId = 1, itemName = "系统管理", itemIcon = "\xe633" });
            chi.Add(new ItemTreeData() { itemId = 1, itemName = "系统管理", itemIcon = "\xe633" });
            chi.Add(new ItemTreeData() { itemId = 1, itemName = "系统管理", itemIcon = "\xe633" });
            chi.Add(new ItemTreeData() { itemId = 1, itemName = "系统管理", itemIcon = "\xe633" });
            MenuViewModel vm = new MenuViewModel();
            vm.ItemTreeDataList.Add(new ItemTreeData() { itemId = 1, itemName = "系统设置", itemIcon = "\xe604", Children = chi });
            vm.ItemTreeDataList.Add(new ItemTreeData() { itemId = 1, itemName = "地图显示", itemIcon = "\xe63c" });
            vm.ItemTreeDataList.Add(new ItemTreeData() { itemId = 1, itemName = "应用中心", itemIcon = "\xe62f" });
            vm.ItemTreeDataList.Add(new ItemTreeData() { itemId = 1, itemName = "列表", itemIcon = "\xe643" });
            vm.ItemTreeDataList.Add(new ItemTreeData() { itemId = 1, itemName = "列表", itemIcon = "\xe643" });
            vm.ItemTreeDataList.Add(new ItemTreeData() { itemId = 1, itemName = "列表", itemIcon = "\xe643" });
            vm.ItemTreeDataList.Add(new ItemTreeData() { itemId = 1, itemName = "列表", itemIcon = "\xe643" });

            GlobalData.NavModules.Add(new NavModuleInfo() {
                region = RegionNames.Main,
                module = ModuleNames.SystemSet,
                title = ModuleTitle.SystemSet,
                icon = "\xe604",
                img = Images.CreateImageSourceFromImage(Properties.Resources.icon),
                menuVm = vm
            });
            this.moduleTracker.RecordModuleInitialized(ModuleNames.SystemSet);
            // regionManager.RegisterViewWithRegion(RegionNames.Main, typeof(SystemSetModule_MainView));
        }
    }
}
