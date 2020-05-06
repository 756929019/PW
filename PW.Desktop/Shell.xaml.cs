// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Windows;
using Prism.Logging;
using Prism.Modularity;
using Prism.Regions;
using System.Windows.Controls;
using Prism.Events;
using PW.Infrastructure;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Runtime.InteropServices;
using DNBSoft.WPF;

namespace PW.Desktop
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    [Export]
    public partial class Shell : Window, IPartImportsSatisfiedNotification
    {

#pragma warning disable 0649 // Imported by MEF
        // The shell imports IModuleTracker once to record updates as modules are downloaded.        
        [Import(AllowRecomposition = false)] private IModuleTracker moduleTracker;

        // The shell imports IModuleManager once to load modules on-demand.
        [Import(AllowRecomposition = false)] private IModuleManager moduleManager;

        // The shell imports the logger once to output logs to the UI.
        [Import(AllowRecomposition = false)] private CallbackLogger logger;

        [Import(AllowRecomposition = false)]
        private IEventAggregator eventAggregator;

        [Import(AllowRecomposition = false)]
        private IRegionManager regionManager;

        [Import(AllowRecomposition = false)]
        private IModuleCatalog moduleCatalog;

        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();

#pragma warning restore 0649

        string mainRegionCurrentModel = "";

        BgPopModel bgPop = new BgPopModel();
        /// <summary>
        /// Initializes a new instance of the <see cref="Shell"/> class.
        /// </summary>
        public Shell()
        {
#if DEBUG
            AllocConsole();
#endif
            this.InitializeComponent();

            InitializeResizeHandle();
        }

        /// <summary>
        /// 加载Resize委托
        /// </summary>
        public void InitializeResizeHandle()
        {
            WindowResizer wr = new WindowResizer(this);
            wr.addResizerRight(right);
            wr.addResizerLeft(left);
            wr.addResizerDown(bottom);
            wr.addResizerLeftDown(leftbottom);
            wr.addResizerRightDown(rightbottom);
            //wr.addResizerUp(topSizeGrip);
            //wr.addResizerLeftUp(topLeftSizeGrip);
            //wr.addResizerRightUp(topRightSizeGrip);
        }
        /// <summary>
        /// Logs the specified message.  Called by the CallbackLogger.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="priority">The priority.</param>
        public void Log(string message, Category category, Priority priority)
        {
            System.Diagnostics.Debug.WriteLine(String.Format(
                                                        CultureInfo.CurrentUICulture, 
                                                        "[{0}][{1}] {2}\r\n", 
                                                        category,
                                                        priority, 
                                                        message));
        }

        /// <summary>
        /// Called when a part's imports have been satisfied and it is safe to use.
        /// </summary>
        public void OnImportsSatisfied()
        {
            // IPartImportsSatisfiedNotification is useful when you want to coordinate doing some work
            // with imported parts independent of when the UI is visible.

            // I use the IModuleTracker as the data-context for data-binding.
            // This quickstart only demonstrates modularity for Prism and does not use data-binding patterns such as MVVM.
            gridFooter.DataContext = this.moduleTracker;

            // I set this shell's Log method as the callback for receiving log messages.
            this.logger.Callback = this.Log;
            this.logger.ReplaySavedLogs();
         
            //this.regionManager.RequestNavigate(RegionNames.Main, "Login");
            //ItemsControl ic = new ItemsControl();
            //RegionManager.SetRegionName(ic, RegionNames.Login);
            //mainContentControl.Content = ic;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SecurityWcf.Core.LoginInfo.UserId = "admin";
            SecurityWcf.Core.LoginInfo.Password = "123456";

            //服务授权码
            SecurityWcf.Core.LoginInfo.Token = "ABC";

            //注入类
            GlobalData.EventAggregator = this.eventAggregator;
            GlobalData.RegionManager = this.regionManager;
            GlobalData.ModuleManager = this.moduleManager;
            GlobalData.ModuleCatalog = this.moduleCatalog;
            NavigateToScreenEvent ntsEvent = GlobalData.EventAggregator.GetEvent<NavigateToScreenEvent>();
            ntsEvent.Subscribe(OnLinkageNavigateEvent);
            HNavigateToScreenEvent hntsEvent = GlobalData.EventAggregator.GetEvent<HNavigateToScreenEvent>();
            hntsEvent.Subscribe(OnLinkageHNavigateEvent);
            // I subscribe to events to help track module loading/loaded progress.
            // The ModuleManager uses the Async Events Pattern.
            //this.moduleManager.LoadModuleCompleted += this.ModuleManager_LoadModuleCompleted;
            this.moduleManager.ModuleDownloadProgressChanged += this.ModuleManager_ModuleDownloadProgressChanged;
            mainRegionCurrentModel = ModuleNames.Login;

            pop.DataContext = bgPop;

            foreach(var item in bgBtnPanel.Children)
            {
                if (item is Button)
                {
                    Button btn = item as Button;
                    btn.Click += Btn_Click;
                }
            }

            ctrlsProgressRing.IsActive = false;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Image img = btn.Content as Image;
            bgImg.ImageSource = img.Source;
        }

        /// <summary>
        /// Handles the RequestModuleLoad event of the ModuleB control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AsideRegion_RequestModuleLoad(object sender, EventArgs e)
        {
            // The ModuleManager uses the Async Events Pattern.
            this.moduleManager.LoadModule(ModuleNames.Aside);
            ItemsControl ic = new ItemsControl();
            RegionManager.SetRegionName(ic, RegionNames.Aside);
            mainContentControl.Content = ic;
        }

        /// <summary>
        /// Handles the RequestModuleLoad event of the ModuleC control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void HeaderRegion_RequestModuleLoad(object sender, EventArgs e)
        {
            // The ModuleManager uses the Async Events Pattern.
            this.moduleManager.LoadModule(ModuleNames.Header);
            ItemsControl ic = new ItemsControl();
            RegionManager.SetRegionName(ic, RegionNames.Header);
            mainContentControl.Content = ic;
        }

        /// <summary>
        /// Handles the RequestModuleLoad event of the ModuleE control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FooterRegion_RequestModuleLoad(object sender, EventArgs e)
        {
            // The ModuleManager uses the Async Events Pattern.
            this.moduleManager.LoadModule(ModuleNames.Footer);
            ItemsControl ic = new ItemsControl();
            RegionManager.SetRegionName(ic, RegionNames.Footer);
            mainContentControl.Content = ic;
        }

        /// <summary>
        /// Handles the LoadModuleProgressChanged event of the ModuleManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Microsoft.Practices.Composite.Modularity.ModuleDownloadProgressChangedEventArgs"/> instance containing the event data.</param>
        private void ModuleManager_ModuleDownloadProgressChanged(object sender, ModuleDownloadProgressChangedEventArgs e)
        {
            this.moduleTracker.RecordModuleDownloading(e.ModuleInfo.ModuleName, e.BytesReceived, e.TotalBytesToReceive);
        }

        /// <summary>
        /// Handles the LoadModuleCompleted event of the ModuleManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Microsoft.Practices.Composite.Modularity.LoadModuleCompletedEventArgs"/> instance containing the event data.</param>
        private void ModuleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            this.moduleTracker.RecordModuleLoaded(e.ModuleInfo.ModuleName);
        }

       
        public void OnLinkageNavigateEvent(CommandRegionEventArgs e)
        {
            //ItemsControl ic = new ItemsControl();
            //ic.Margin = new Thickness(0);
            //ic.HorizontalAlignment = HorizontalAlignment.Stretch;
            //ic.VerticalAlignment = VerticalAlignment.Stretch;
            //RegionManager.SetRegionName(ic, e.Region);
            //RegionManager.SetRegionContext(mainContentControl, null);
            //RegionManager.SetRegionName(mainContentControl, e.Region);
            //mainContentControl.Content = ic;
            if (mainRegionCurrentModel != e.Module)
            {
                //清理mainRegion
                //this.regionManager.Regions[e.Region].RemoveAll();
                mainRegionCurrentModel = e.Module;

                //CommandEventArgs ces = new CommandEventArgs();
                //ces.Type = CommandType.registerDefViewWithRegion;
                //ces.ModuleName = e.Module;
                //GlobalData.EventAggregator.GetEvent<CommandEvent>().Publish(ces);

                if (!GlobalData.IsLoadModule(e.Module))
                {
                    this.moduleManager.LoadModule(e.Module);
                }
            }
            //SystemSetModule_MainView
            this.regionManager.RequestNavigate(e.Region, e.Module+"_MainView");
            //点击导航后加载HeaderView，MenuView
            this.regionManager.RequestNavigate(RegionNames.Header, "HeaderView");
            this.regionManager.RequestNavigate(RegionNames.Aside, "MenuView");
        }

        public void OnLinkageHNavigateEvent(CommandRegionEventArgs e)
        {
            //SystemSetModule_MainView
            this.regionManager.RequestNavigate(e.Region, e.Module + "_MainView");
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void maxBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.WindowState = this.WindowState == WindowState.Maximized? WindowState.Normal: WindowState.Maximized;
            this.maxMinIcon.Text = this.WindowState == WindowState.Maximized ? "\xe62b" : "\xe65b";
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //进行拖放移动
            this.DragMove();
        }

        private void bgBtn_Click(object sender, RoutedEventArgs e)
        {
            bgPop.Bg_Popup = true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            FreeConsole();
        }
    }

    public class BgPopModel : INotifyPropertyChanged
    {
        private bool bg_Popup = false;
        /// <summary>
        /// Popup是否弹出
        /// </summary>
        public bool Bg_Popup
        {
            get
            {
                return bg_Popup;
            }

            set
            {
                bg_Popup = value;
                NotifyPropertyChanged("Bg_Popup");
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(String propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
