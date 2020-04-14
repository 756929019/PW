using Prism.Modularity;
using Prism.Mef.Modularity;
using Prism.Regions;
using System.ComponentModel.Composition;
using PW.Infrastructure;
using System;
using System.Utility.Helper;
using System.Collections.ObjectModel;
using PW.Common;

namespace PW.Tools
{
    [ModuleExport( typeof(ToolsModule), InitializationMode = InitializationMode.OnDemand)]
    public class ToolsModule : IModule
    {
        private readonly IModuleTracker moduleTracker;
        private readonly IRegionManager regionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapModule"/> class.
        /// </summary>
        /// <param name="moduleTracker">The module tracker.</param>
        [ImportingConstructor]
        public ToolsModule(IModuleTracker moduleTracker, IRegionManager regionManager)
        {
            if (moduleTracker == null)
            {
                throw new ArgumentNullException("moduleTracker");
            }

            this.moduleTracker = moduleTracker;
            this.moduleTracker.RecordModuleConstructed(ModuleNames.Tools);
            this.regionManager = regionManager;
            CommandEvent cmdEvent = GlobalData.EventAggregator.GetEvent<CommandEvent>();
            cmdEvent.Subscribe(OnCommandEvent);
        }
        private void OnCommandEvent(CommandEventArgs e)
        {
            Log.info("ToolsModule OnCommandEvent");
            if (e.Type == CommandType.registerDefViewWithRegion)
            {
                regionManager.RegisterViewWithRegion(RegionNames.Main, typeof(ToolsModule_MainView));
            }
        }


        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            ObservableCollection<ItemTreeData> chi = new ObservableCollection<ItemTreeData>();
            chi.Add(new ItemTreeData() { itemId = 1, itemName = "代码生成", itemIcon = "\xe633", itemRegion = RegionNames.Tools, itemView = "CodeGenerator" });
            chi.Add(new ItemTreeData() { itemId = 2, itemName = "代码编辑", itemIcon = "\xe633", itemRegion = RegionNames.Tools, itemView = "CodeEdit" });
            MenuViewModel vm = new MenuViewModel();
            vm.ItemTreeDataList.Add(new ItemTreeData() { itemId = 1, itemName = "代码工具", itemIcon = "\xe604", Children = chi });
            vm.ItemTreeDataList.Add(new ItemTreeData() { itemId = 1, itemName = "地图显示", itemIcon = "\xe63c" });
            vm.ItemTreeDataList.Add(new ItemTreeData() { itemId = 1, itemName = "应用中心", itemIcon = "\xe62f" });
            vm.ItemTreeDataList.Add(new ItemTreeData() { itemId = 1, itemName = "列表", itemIcon = "\xe643" });
            vm.ItemTreeDataList.Add(new ItemTreeData() { itemId = 1, itemName = "列表", itemIcon = "\xe643" });
            vm.ItemTreeDataList.Add(new ItemTreeData() { itemId = 1, itemName = "列表", itemIcon = "\xe643" });
            vm.ItemTreeDataList.Add(new ItemTreeData() { itemId = 1, itemName = "列表", itemIcon = "\xe643" });

            GlobalData.NavModules.Add(new NavModuleInfo() {
                region = RegionNames.Main,
                module = ModuleNames.Tools,
                title = ModuleTitle.Tools,
                icon = "\xe604",
                img = Images.CreateImageSourceFromImage(Properties.Resources.icon),
                menuVm = vm
            });
            this.moduleTracker.RecordModuleInitialized(ModuleNames.Tools);
            // regionManager.RegisterViewWithRegion(RegionNames.Main, typeof(ToolsModule_MainView));
        }
    }
}
