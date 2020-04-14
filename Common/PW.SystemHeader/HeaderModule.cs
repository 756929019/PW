using Prism.Modularity;
using Prism.Mef.Modularity;
using Prism.Regions;
using System.ComponentModel.Composition;
using System;
using PW.Infrastructure;
using PW.Common;

namespace PW.SystemHeader
{
    [ModuleExport(typeof(HeaderModule), InitializationMode = InitializationMode.OnDemand)]
    public class HeaderModule : IModule
    {
        private readonly IModuleTracker moduleTracker;
        private readonly IRegionManager regionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderModule"/> class.
        /// </summary>
        /// <param name="moduleTracker">The module tracker.</param>
        [ImportingConstructor]
        public HeaderModule(IModuleTracker moduleTracker, IRegionManager regionManager)
        {
            if (moduleTracker == null)
            {
                throw new ArgumentNullException("moduleTracker");
            }

            this.moduleTracker = moduleTracker;
            this.moduleTracker.RecordModuleConstructed(ModuleNames.Header);
            this.regionManager = regionManager;
            CommandEvent cmdEvent = GlobalData.EventAggregator.GetEvent<CommandEvent>();
            cmdEvent.Subscribe(OnCommandEvent);
        }
        private void OnCommandEvent(CommandEventArgs e)
        {
            Log.info("HeaderModule OnCommandEvent");
        }


        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            this.moduleTracker.RecordModuleInitialized(ModuleNames.Header);
            regionManager.RegisterViewWithRegion(RegionNames.Header, typeof(HeaderView));
        }
    }
}
