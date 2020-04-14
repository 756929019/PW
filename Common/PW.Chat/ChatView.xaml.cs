using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using PW.Chat.ViewModel;
using PW.Controls;
using PW.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PW.Chat
{
    /// <summary>
    /// ChatView.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(ChatView))]
    public partial class ChatView : UserControl, INavigationAware
    {
        [Import]
        public IRegionManager regionManager;
        [Import]
        public IModuleManager moduleManager;
        [Import]
        public IEventAggregator eventAggregator;

        [ImportingConstructor]
        public ChatView(IRegionManager regionManager, IEventAggregator eventAggregator, IModuleManager moduleManager)
        {
            this.eventAggregator = eventAggregator;
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        private void btnChat_Click(object sender, RoutedEventArgs e)
        {
            if (gridMsgMain.Visibility == Visibility.Visible)
            {
                gridMsgMain.Visibility = Visibility.Collapsed;
            }
            else
            {
                gridMsgMain.Visibility = Visibility.Visible;
            }
        }

        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {
            gridMsgMain.Visibility = Visibility.Collapsed;
        }

        Point pos = new Point();
        bool maxState = false;
        double minWidth = 852;
        double minHeight = 612;
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                toMaxOrMin();
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Canvas.SetLeft(myWin, 0);
            Canvas.SetTop(myWin, 0);
            gridMsgMain.Visibility = Visibility.Collapsed;
        }

        private void minBtn_Click(object sender, RoutedEventArgs e)
        {
            gridMsgMain.Visibility = Visibility.Collapsed;
        }

        private void maxBtn_Click(object sender, RoutedEventArgs e)
        {
            toMaxOrMin();
        }

        void toMaxOrMin()
        {
            if (maxState)
            {
                myWin.Width = minWidth;
                myWin.Height = minHeight;
                gridMsgMain.Width = minWidth;
                gridMsgMain.Height = minHeight;
                Canvas.SetLeft(myWin, 0);
                Canvas.SetTop(myWin, 0);
                maxState = false;
            }
            else
            {
                Window parentWindow = Window.GetWindow(this);
                myWin.Width = parentWindow.ActualWidth;
                myWin.Height = parentWindow.ActualHeight - 35;
                gridMsgMain.Width = parentWindow.ActualWidth;
                gridMsgMain.Height = parentWindow.ActualHeight - 35;
                Canvas.SetLeft(myWin, 0);
                Canvas.SetTop(myWin, 0);
                maxState = true;
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Grid tmp = (Grid)sender;
            pos = e.GetPosition(null);
            tmp.CaptureMouse();
            tmp.Cursor = Cursors.Hand;
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (!maxState)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Grid tmp = (Grid)sender;
                    double dx = e.GetPosition(null).X - pos.X + Canvas.GetLeft(myWin);
                    double dy = e.GetPosition(null).Y - pos.Y + Canvas.GetTop(myWin);
                    Canvas.SetLeft(myWin, dx);
                    Canvas.SetTop(myWin, dy);
                    pos = e.GetPosition(null);
                }
            }
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Grid tmp = (Grid)sender;
            tmp.ReleaseMouseCapture();
        }

        /// <summary>
        /// 弹窗关闭，并给RichTextbox添加Emoji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmojiTabControlUC_Close(object sender, EventArgs e)
        {
            //将Emoji放入以后，把光标移动到刚插入的Emoji的结尾部分
            var container = new InlineUIContainer(new Image { Source = EmojiTabControlUC.SelectEmoji.Value, Height = 20, Width = 20 }, rtb.CaretPosition);
            rtb.CaretPosition = container.ElementEnd;

            rtb.Focus();

            pop.IsOpen = false;
        }

        private void RB_Cut_Click(object sender, RoutedEventArgs e)
        {
            Window win = Window.GetWindow(this);
            win.Hide();
            Thread.Sleep(200);
            System.Drawing.Bitmap newBitmap = CopyHelper.CopyFromScreen();
            System.Windows.Media.ImageSource ISource = CopyHelper.BitMapToImageSource(newBitmap);

            ShowWinCopy(ISource, win);
        }

        private void ShowWinCopy(System.Windows.Media.ImageSource ISource, Window win)
        {
            try { 
            WindowCopy winCopy = new WindowCopy(ISource, win, null);
            winCopy.winMain = win;
            winCopy.Closed += WinCopy_Closed;
            winCopy.Show();
            }
            catch (Exception ex)
            { }
        }

        private void WinCopy_Closed(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Media.Imaging.BitmapSource bs = Clipboard.GetImage();
                Image img = new Image();
                img.Width = bs.Width;
                img.Height = bs.Height;
                img.Source = bs;
                new InlineUIContainer(img, rtb.Selection.End); //插入图片到选定位置
            }
            catch (Exception ex)
            { }
        }
    }
}
