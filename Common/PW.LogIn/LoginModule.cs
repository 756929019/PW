using Prism.Modularity;
using Prism.Mef.Modularity;
using Prism.Regions;
using System.ComponentModel.Composition;
using Prism.Logging;
using System;
using PW.Infrastructure;

namespace PW.LogIn
{
    [ModuleExport(typeof(LoginModule), InitializationMode = InitializationMode.WhenAvailable)]
    public class LoginModule : IModule
    {
        private readonly ILoggerFacade logger;
        private readonly IModuleTracker moduleTracker;
        private readonly IRegionViewRegistry regionViewRegistry;
        [ImportingConstructor]
        public LoginModule(ILoggerFacade logger, IModuleTracker moduleTracker, IRegionViewRegistry registry)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            if (moduleTracker == null)
            {
                throw new ArgumentNullException("moduleTracker");
            }

            this.logger = logger;
            this.moduleTracker = moduleTracker;
            this.moduleTracker.RecordModuleConstructed(ModuleNames.Login);
            this.regionViewRegistry = registry;
        }
        //public LoginRegion(ILoggerFacade logger, IModuleTracker moduleTracker)
        //{
        //    if (logger == null)
        //    {
        //        throw new ArgumentNullException("logger");
        //    }

        //    if (moduleTracker == null)
        //    {
        //        throw new ArgumentNullException("moduleTracker");
        //    }

        //    this.logger = logger;
        //    this.moduleTracker = moduleTracker;
        //    this.moduleTracker.RecordModuleConstructed(ModuleNames.LoginRegion);
        //}
        public void Initialize()
        {
            this.logger.Log("LoginRegion demonstrates logging during Initialize().", Category.Info, Priority.Medium);
            this.moduleTracker.RecordModuleInitialized(ModuleNames.Login);
            GlobalData.LoadModule.Add(ModuleNames.Login);
            regionViewRegistry.RegisterViewWithRegion(RegionNames.Main, typeof(Login));
            //regionViewRegistry.RegisterViewWithRegion(RegionNames.Login, typeof(SysSwitchView));
        }
    }
}
