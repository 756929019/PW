using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using PW.Common;
using PW.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Chat
{
    [ModuleExport(typeof(ChatModule), InitializationMode = InitializationMode.OnDemand)]
    public class ChatModule : IModule
    {
        private readonly IModuleTracker moduleTracker;
        private readonly IRegionManager regionManager;
        private readonly IRegionViewRegistry regionViewRegistry;
        [ImportingConstructor]
        public ChatModule(IModuleTracker moduleTracker, IRegionManager regionManager, IRegionViewRegistry registry)
        {
            if (moduleTracker == null)
            {
                throw new ArgumentNullException("moduleTracker");
            }

            this.moduleTracker = moduleTracker;
            this.moduleTracker.RecordModuleConstructed(ModuleNames.Chat);
            this.regionManager = regionManager;
            this.regionViewRegistry = registry;
            CommandEvent cmdEvent = GlobalData.EventAggregator.GetEvent<CommandEvent>();
            cmdEvent.Subscribe(OnCommandEvent);
        }
        private void OnCommandEvent(CommandEventArgs e)
        {
            Log.info("ChatModule OnCommandEvent");
        }

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            this.moduleTracker.RecordModuleInitialized(ModuleNames.Chat);
            regionManager.RegisterViewWithRegion(RegionNames.Chat, typeof(ChatView));
        }
    }
}
