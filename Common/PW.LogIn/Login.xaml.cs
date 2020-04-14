using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using PW.Common;
using PW.Infrastructure;
using PW.ServiceCenter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PW.LogIn
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(Login))]
    public partial class Login : UserControl
    {
        private LoginUser user = new LoginUser();

        [Import]
        public IRegionManager regionManager;
        [Import]
        public IModuleManager moduleManager;
        [Import]
        public IModuleTracker moduleTracker;
        [Import]
        public IRegionViewRegistry regionViewRegistry;

        [ImportingConstructor]
        public Login(IRegionManager regionManager, IEventAggregator eventAggregator, IModuleManager moduleManager)
        {
            InitializeComponent();
            this.regionManager = regionManager;
            this.Loaded += new RoutedEventHandler(LogInView_Loaded);
            Init();
            //注册帧动画
            _timer = new System.Windows.Threading.DispatcherTimer();
            _timer.Tick += new EventHandler(PolyAnimation);
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 24);//一秒钟刷新24次
            _timer.Start();
        }

        /// <summary>
        /// 随机数
        /// </summary>
        private Random _random = new Random();

        private static int xsize = 9;
        private static int ysize = 5;
        private static int xywidth = 70;
        //布局宽490 高210 显示宽430 高180
        //阵距4行8列 点之间的距离 X轴Y轴都是70
        /// <summary>
        /// 点信息阵距
        /// </summary>
        private PointInfo[,] _points = new PointInfo[xsize, ysize];

        /// <summary>
        /// 计时器
        /// </summary>
        private DispatcherTimer _timer;

        /// <summary>
        /// 初始化阵距
        /// </summary>
        private void Init()
        {
            //生成阵距的点
            for (int i = 0; i < xsize; i++)
            {
                for (int j = 0; j < ysize; j++)
                {
                    double x = _random.Next(-11, 11);
                    double y = _random.Next(-6, 6);
                    _points[i, j] = new PointInfo()
                    {
                        X = i * xywidth,
                        Y = j * xywidth,
                        SpeedX = x / 24,
                        SpeedY = y / 24,
                        DistanceX = _random.Next(35, 106),
                        DistanceY = _random.Next(20, 40),
                        MovedX = 0,
                        MovedY = 0,
                        PolygonInfoList = new List<PolygonInfo>()
                    };
                }
            }
            byte a = (byte)_random.Next(10, 70);
            byte r = (byte)_random.Next(0, 11);
            byte g = (byte)_random.Next(100, 201);
            int intb = g + _random.Next(50, 101);
            if (intb > 255)
                intb = 255;
            byte b = (byte)intb;

            //上一行取2个点 下一行取1个点
            for (int i = 0; i < xsize - 1; i++)
            {
                for (int j = 0; j < ysize - 1; j++)
                {
                    Polygon poly = new Polygon();
                    poly.Points.Add(new Point(_points[i, j].X, _points[i, j].Y));
                    _points[i, j].PolygonInfoList.Add(new PolygonInfo() { PolygonRef = poly, PointIndex = 0 });
                    poly.Points.Add(new Point(_points[i + 1, j].X, _points[i + 1, j].Y));
                    _points[i + 1, j].PolygonInfoList.Add(new PolygonInfo() { PolygonRef = poly, PointIndex = 1 });
                    poly.Points.Add(new Point(_points[i + 1, j + 1].X, _points[i + 1, j + 1].Y));
                    _points[i + 1, j + 1].PolygonInfoList.Add(new PolygonInfo() { PolygonRef = poly, PointIndex = 2 });
                    poly.Fill = new SolidColorBrush(Color.FromArgb(a, r, g, (byte)b));
                    SetColorAnimation(poly);
                    layout.Children.Add(poly);
                }
            }

            //上一行取1个点 下一行取2个点
            for (int i = 0; i < xsize - 1; i++)
            {
                for (int j = 0; j < ysize - 1; j++)
                {
                    Polygon poly = new Polygon();
                    poly.Points.Add(new Point(_points[i, j].X, _points[i, j].Y));
                    _points[i, j].PolygonInfoList.Add(new PolygonInfo() { PolygonRef = poly, PointIndex = 0 });
                    poly.Points.Add(new Point(_points[i, j + 1].X, _points[i, j + 1].Y));
                    _points[i, j + 1].PolygonInfoList.Add(new PolygonInfo() { PolygonRef = poly, PointIndex = 1 });
                    poly.Points.Add(new Point(_points[i + 1, j + 1].X, _points[i + 1, j + 1].Y));
                    _points[i + 1, j + 1].PolygonInfoList.Add(new PolygonInfo() { PolygonRef = poly, PointIndex = 2 });
                    poly.Fill = new SolidColorBrush(Color.FromArgb(a, r, g, (byte)b));
                    SetColorAnimation(poly);
                    layout.Children.Add(poly);
                }
            }

            // 初始化之后改变矩阵形态--避免初始化就是四方四正的
            for (int i = 0; i < 68; i++)
            {
                PolyAnimation(null, null);
            }
        }

        /// <summary>
        /// 设置颜色动画
        /// </summary>
        /// <param name="polygon">多边形</param>
        private void SetColorAnimation(UIElement polygon)
        {
            //颜色动画的时间 1-4秒随机
            Duration dur = new Duration(new TimeSpan(0, 0, _random.Next(1, 5)));
            //故事版
            Storyboard sb = new Storyboard()
            {
                Duration = dur
            };
            sb.Completed += (S, E) => //动画执行完成事件
            {
                //颜色动画完成之后 重新set一个颜色动画
                SetColorAnimation(polygon);
            };
            //颜色动画
            //颜色的RGB
            byte a = (byte)_random.Next(10, 70);
            byte r = (byte)_random.Next(0, 11);
            byte g = (byte)_random.Next(100, 201);
            int intb = g + _random.Next(50, 101);
            if (intb > 255)
                intb = 255;
            byte b = (byte)intb;
            ColorAnimation ca = new ColorAnimation()
            {
                To = Color.FromArgb(a, r, g, b),
                Duration = dur
            };
            Storyboard.SetTarget(ca, polygon);
            Storyboard.SetTargetProperty(ca, new PropertyPath("Fill.Color"));
            sb.Children.Add(ca);
            sb.Begin(this);
        }

        /// <summary>
        /// 多边形变化动画
        /// </summary>
        void PolyAnimation(object sender, EventArgs e)
        {
            //不改变阵距最外边一层的点
            for (int i = 1; i < xsize - 1; i++)
            {
                for (int j = 1; j < ysize - 1; j++)
                {
                    PointInfo pointInfo = _points[i, j];
                    pointInfo.X += pointInfo.SpeedX;
                    pointInfo.Y += pointInfo.SpeedY;
                    pointInfo.MovedX += pointInfo.SpeedX;
                    pointInfo.MovedY += pointInfo.SpeedY;
                    if (pointInfo.MovedX >= pointInfo.DistanceX || pointInfo.MovedX <= -pointInfo.DistanceX)
                    {
                        pointInfo.SpeedX = -pointInfo.SpeedX;
                        pointInfo.MovedX = 0;
                    }
                    if (pointInfo.MovedY >= pointInfo.DistanceY || pointInfo.MovedY <= -pointInfo.DistanceY)
                    {
                        pointInfo.SpeedY = -pointInfo.SpeedY;
                        pointInfo.MovedY = 0;
                    }
                    //改变多边形的点
                    foreach (PolygonInfo pInfo in _points[i, j].PolygonInfoList)
                    {
                        pInfo.PolygonRef.Points[pInfo.PointIndex] = new Point(pointInfo.X, pointInfo.Y);
                    }
                }
            }
        }

        void LogInView_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsolatedStorageFile.IsEnabled == true)
            {
                IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForAssembly();
                if (isoFile.FileExists("login.txt"))
                {
                    IsolatedStorageFileStream isoFileStream = isoFile.OpenFile("login.txt", System.IO.FileMode.Open);
                    StreamReader sr = new StreamReader(isoFileStream, Encoding.UTF8);
                    String loginStr = sr.ReadToEnd();
                    user.Name = loginStr;
                    sr.Close();
                    isoFileStream.Close();
                    isoFile.Dispose();
                }
            }
            this.gcLogin.DataContext = user;
        }

        void ModuleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            Log.info("加载模块完成：" + e.ModuleInfo.ModuleName);
            this.moduleTracker.RecordModuleLoaded(e.ModuleInfo.ModuleName);
            GlobalData.LoadModule.Add(e.ModuleInfo.ModuleName);
            //throw new NotImplementedException();

            if (GlobalData.LoadModule.Count == GlobalData.ModuleCatalog.Modules.Count())
            {
                Log.info("加载模块完成打开主菜单");
                regionViewRegistry.RegisterViewWithRegion(RegionNames.Main, typeof(SysSwitchView));
                regionManager.RequestNavigate(RegionNames.Main, "SysSwitchView");

                // 加载各模块主页面
                CommandEventArgs ces = new CommandEventArgs();
                ces.Type = CommandType.registerDefViewWithRegion;
                GlobalData.EventAggregator.GetEvent<CommandEvent>().Publish(ces);
            }
        }

        public void LoadAllModules()
        {
            GlobalData.ModuleManager.LoadModuleCompleted += ModuleManager_LoadModuleCompleted;
            foreach (var item in GlobalData.ModuleCatalog.Modules)
            {
                if (item.ModuleName != ModuleNames.Login)
                {
                    Log.info("加载模块开始：" + item.ModuleName);
                    GlobalData.ModuleManager.LoadModule(item.ModuleName);
                }
            }
            Log.info("运行模块开始");
            GlobalData.ModuleManager.Run();
        }
        //Thread loadThread;
        //delegate void updateUIHandler(string name);
        //private void updateWork()
        //{
        //    GlobalData.ModuleManager.LoadModuleCompleted += ModuleManager_LoadModuleCompleted;
        //    foreach (var item in GlobalData.ModuleCatalog.Modules)
        //    {
        //        if (item.ModuleName != ModuleNames.Login)
        //        {
        //            Log.info("加载模块开始：" + item.ModuleName);
        //            GlobalData.ModuleManager.LoadModule(item.ModuleName);
        //        }
        //    }
        //    Log.info("运行模块开始");
        //    //GlobalData.ModuleManager.Run();
        //}
        //public void LoadAllModules()
        //{
        //    loadThread = new Thread(new ThreadStart(updateWork));
        //    loadThread.SetApartmentState(ApartmentState.STA);
        //    loadThread.IsBackground = true;
        //    loadThread.Start();
        //}
        //void ModuleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        //{
        //    Log.info("加载模块完成：" + e.ModuleInfo.ModuleName);
        //    updateUIHandler mothed = new updateUIHandler(updateUI);
        //    this.Dispatcher.Invoke(mothed, e.ModuleInfo.ModuleName);
        //}
        //private void updateUI(string name) {
        //    this.moduleTracker.RecordModuleLoaded(name);
        //    GlobalData.LoadModule.Add(name);
        //    //throw new NotImplementedException();

        //    if (GlobalData.LoadModule.Count == GlobalData.ModuleCatalog.Modules.Count())
        //    {
        //        Log.info("加载模块完成打开主菜单");
        //        regionViewRegistry.RegisterViewWithRegion(RegionNames.Main, typeof(SysSwitchView));
        //        regionManager.RequestNavigate(RegionNames.Main, "SysSwitchView");

        //        // 加载各模块主页面
        //        CommandEventArgs ces = new CommandEventArgs();
        //        ces.Type = CommandType.registerDefViewWithRegion;
        //        GlobalData.EventAggregator.GetEvent<CommandEvent>().Publish(ces);
        //    }
        //}
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            this.btnLogin.IsEnabled = false;
            if ((!String.IsNullOrEmpty(this.txtPassword.Password.Trim()) && !String.IsNullOrEmpty(this.txtName.Text.Trim())))
            {
                
                this.gcLogin.Visibility = Visibility.Collapsed;
                this.loadingInfo.Visibility = Visibility.Visible;
                ServiceComm sc = new ServiceComm();
                sc.LoginCompleted += (serice, eve) =>
                {
                    if (eve.Succesed && eve.Result)
                    {
                        Log.info("登录成功");
                        GlobalData.EventAggregator.GetEvent<BaseDataLoadedEvent>().Publish(1);
                        Log.info("加载模块");
                        LoadAllModules();
                        Log.info("存储登录信息");
                        #region 独立存储区操作
                        try
                        {
                            if (IsolatedStorageFile.IsEnabled == true)
                            {
                                IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForAssembly();
                                IsolatedStorageFileStream isoFileStream = isoFile.OpenFile("login.txt", System.IO.FileMode.Create);
                                String loginStr = "";
                                if (this.cbxRemPassword.IsChecked == true)
                                {
                                    loginStr = String.Format("{0}", this.txtName.Text.Trim());
                                }
                                Byte[] bts = Encoding.UTF8.GetBytes(loginStr);
                                isoFileStream.Write(bts, 0, bts.Length);
                                isoFileStream.Close();
                                isoFile.Dispose();
                            }
                        }
                        catch (Exception)
                        {

                        }
                        #endregion
                        GlobalData.UserName = this.txtName.Text.Trim();
                        Log.info("建立回话连接");
                        SocketClient socketClient = null;
                        socketClient = SocketClient.GetInstance();
                        //socketClient.ReceivedMsg = new SocketClient.ReceivedMsgHandler(socketClient_ReceivedMsg);
                        socketClient.BeginConnect();
                    }
                    else
                    {
                        this.btnLogin.IsEnabled = true;
                        this.gcLogin.Visibility = Visibility.Visible;
                        this.loadingInfo.Visibility = Visibility.Collapsed;
                        MessageBox.Show("登陆失败！");
                    }
                };
                Log.info("开始登录");
                sc.Login(this.txtName.Text.Trim(), this.txtPassword.Password.Trim());
            }
            else
            {
                this.btnLogin.IsEnabled = true;
                MessageBox.Show("请输入用户名或密码！");
            }
                
        }

        //void socketClient_ReceivedMsg(string msg)
        //{
        //    if (msgList.InvokeRequired)
        //    {
        //        this.Invoke(socketClient.ReceivedMsg, msg);
        //    }
        //    else
        //    {
        //        int cnt = msgList.Controls.Count;
        //        int msgWidth = 252;
        //        if (cnt >= 8)
        //        {
        //            msgWidth = 235;

        //            //重置前八个宽度
        //            for (int i = 0; i < 8; i++)
        //            {
        //                msgList.Controls[i].Width = msgWidth;
        //            }
        //        }
        //        SocketMsg smsg = new SocketMsg();
        //        smsg.setAllParametersJsonStr(msg);
        //        Hashtable param = smsg.getAllParameters();
        //        addMsgItem(param["title"].ToString(), param["content"].ToString(), param["menu"].ToString(), false, msgWidth);
        //    }
        //}
        private void txtPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                user.Password = this.txtPassword.Password.Trim();
                this.btnLogin_Click(this.btnLogin, null);
            }
        }
    }
    public class LoginUser : INotifyPropertyChanged
    {
        private String _name;

        public String Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private String _password;

        public String Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyPropertyChanged("Password");
            }
        }

        public LoginUser()
        {
#if DEBUG
            _name = "root";
            _password = "root";
#endif
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(String propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    /// <summary>
    /// 阵距点信息
    /// </summary>
    public class PointInfo
    {
        /// <summary>
        /// X坐标
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// X轴速度 wpf距离单位/二十四分之一秒
        /// </summary>
        public double SpeedX { get; set; }

        /// <summary>
        /// Y轴速度 wpf距离单位/二十四分之一秒
        /// </summary>
        public double SpeedY { get; set; }

        /// <summary>
        /// X轴需要移动的距离
        /// </summary>
        public double DistanceX { get; set; }

        /// <summary>
        /// Y轴需要移动的距离
        /// </summary>
        public double DistanceY { get; set; }

        /// <summary>
        /// X轴已经移动的距离
        /// </summary>
        public double MovedX { get; set; }

        /// <summary>
        /// Y轴已经移动的距离
        /// </summary>
        public double MovedY { get; set; }

        /// <summary>
        /// 多边形信息列表
        /// </summary>
        public List<PolygonInfo> PolygonInfoList { get; set; }
    }

    /// <summary>
    /// 多边形信息
    /// </summary>
    public class PolygonInfo
    {
        /// <summary>
        /// 对多边形的引用
        /// </summary>
        public Polygon PolygonRef { get; set; }

        /// <summary>
        /// 需要改变的点的索引
        /// </summary>
        public int PointIndex { get; set; }
    }
}
