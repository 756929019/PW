using Prism.Modularity;
using Prism.Mef.Modularity;
using Prism.Regions;
using System.ComponentModel.Composition;
using System;
using PW.Infrastructure;
using PW.Common;

namespace PW.Aside
{
    [ModuleExport(typeof(AsideModule), InitializationMode = InitializationMode.OnDemand)]
    public class AsideModule : IModule
    {
        private readonly IModuleTracker moduleTracker;
        private readonly IRegionManager regionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleB"/> class.
        /// </summary>
        /// <param name="moduleTracker">The module tracker.</param>
        [ImportingConstructor]
        public AsideModule(IModuleTracker moduleTracker, IRegionManager regionManager)
        {
            if (moduleTracker == null)
            {
                throw new ArgumentNullException("moduleTracker");
            }

            this.moduleTracker = moduleTracker;
            this.moduleTracker.RecordModuleConstructed(ModuleNames.Aside);
            this.regionManager = regionManager;
            CommandEvent cmdEvent = GlobalData.EventAggregator.GetEvent<CommandEvent>();
            cmdEvent.Subscribe(OnCommandEvent);
        }
        private void OnCommandEvent(CommandEventArgs e) {
            Log.info("AsideModule OnCommandEvent");
        }

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            this.moduleTracker.RecordModuleInitialized(ModuleNames.Aside);
            regionManager.RegisterViewWithRegion(RegionNames.Aside, typeof(MenuView));
        }
    }
}
