using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace PW.Controls.Controls
{
    /// <summary>
    /// LeftMenu.xaml 的交互逻辑
    /// </summary>
    public partial class LeftMenu : UserControl
    {
        public LeftMenu()
        {
            InitializeComponent();
        }
        //根目录的名字
        public static readonly DependencyProperty RootNameProperty = DependencyProperty.Register("RootName", typeof(string), typeof(LeftMenu), new PropertyMetadata(default(string)));
        public string RootName
        {
            get { return (string)GetValue(RootNameProperty); }
            set { SetValue(RootNameProperty, value); }
        }
        //定义ListBox的数据源
        public static readonly DependencyProperty IResourceProperty = DependencyProperty.Register("IResource", typeof(IList<NavigateDetail>), typeof(LeftMenu), new PropertyMetadata(default(IList<NavigateDetail>)));
        public IList<NavigateDetail> IResource
        {
            get { return (IList<NavigateDetail>)GetValue(IResourceProperty); }
            set { SetValue(IResourceProperty, value); }
        }

        //获取所有的集合数据
        public static readonly DependencyProperty AllResourceProperty = DependencyProperty.Register("AllResource", typeof(IList<NavigateRoot>), typeof(LeftMenu), new PropertyMetadata(default(IList<NavigateRoot>)));
        public IList<NavigateRoot> AllResource
        {
            get { return (IList<NavigateRoot>)GetValue(AllResourceProperty); }
            set { SetValue(AllResourceProperty, value); }
        }

        //部分集合数据
        public static readonly DependencyProperty PartResourceProperty = DependencyProperty.Register("PartResource", typeof(IList<NavigateRoot>), typeof(LeftMenu), new PropertyMetadata(default(IList<NavigateRoot>)));
        public IList<NavigateRoot> PartResource
        {
            get { return (IList<NavigateRoot>)GetValue(PartResourceProperty); }
            set { SetValue(PartResourceProperty, value); }
        }
        //定义Grid的高
        public static readonly DependencyProperty ItemActualHeightProperty = DependencyProperty.Register("ItemActualHeight", typeof(double), typeof(LeftMenu), new PropertyMetadata(default(double)));
        public double ItemActualHeight
        {
            get { return (double)GetValue(ItemActualHeightProperty); }
            set { SetValue(ItemActualHeightProperty, value * AllResource[0].Details.Count + 5); }
        }
        //根目录的样式
        public static readonly DependencyProperty RootStyleProperty = DependencyProperty.Register("RootStyle", typeof(Style), typeof(LeftMenu), new PropertyMetadata(default(Style)));
        public Style RootStyle
        {
            get { return (Style)GetValue(RootStyleProperty); }
            set { SetValue(RootStyleProperty, value); }
        }
        //整个面板的背景样式
        public static readonly DependencyProperty LeftMenuBgProperty = DependencyProperty.Register("LeftMenuBg", typeof(Style), typeof(LeftMenu), new PropertyMetadata(default(Style)));
        public Style LeftMenuBg
        {
            get { return (Style)GetValue(LeftMenuBgProperty); }
            set { SetValue(LeftMenuBgProperty, value); }
        }
        //根目录上面的字体样式
        public static readonly DependencyProperty LeftMenuTextProperty = DependencyProperty.Register("LeftMenuText", typeof(Style), typeof(LeftMenu), new PropertyMetadata(default(Style)));
        public Style LeftMenuText
        {
            get { return (Style)GetValue(LeftMenuTextProperty); }
            set { SetValue(LeftMenuTextProperty, value); }
        }

        #region 左右收缩的动画
        //向左边收缩的动画
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            (this.Resources["SlideIn"] as Storyboard).Begin();
            (this.Resources["SlideIn"] as Storyboard).Completed += new EventHandler((s, a) => { this.LeftMenuLeft.Width = 0; });
            e.Handled = true;
        }
        //向右边收缩的动画
        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.LeftMenuLeft.Width = 200;
            (this.Resources["SlideOut"] as Storyboard).Begin();
        }
        #endregion

        //上下动画
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;
            this.ListBox.SelectedIndex = -1;
            if (grid != null)
            {
                if (grid.Tag.ToString() == "0")
                {
                    (this.Resources["ListBoxOut"] as Storyboard).Begin();
                    grid.Tag = "1";
                }
                else
                {
                    (this.Resources["ListBoxIn"] as Storyboard).Begin();
                    grid.Tag = "0";
                }
            }
        }
        //等第一个根目录的东西加载完成之后，就去添加里面需要的东西
        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            this.IResource = this.AllResource[0].Details;
            this.RootName = this.AllResource[0].Name;
            int count = this.AllResource[0].Details.Count;
            this.AllResource.RemoveAt(0);
            this.PartResource = this.AllResource;
            for (int i = 0; i < this.PartResource.Count; i++)
            {
                TabDetail td = new TabDetail();
                td.TTabDetailText = this.LeftMenuText;
                td.TRootName = this.PartResource[i].Name;
                td.TIResource = this.PartResource[i].Details;
                td.TItemActualHeight = (this.ItemActualHeight - 5) / count;
                Binding BRootStyle = new Binding("RootStyle");
                BRootStyle.Mode = BindingMode.OneWay;
                BRootStyle.ElementName = "LeftMenuUC";
                td.SetBinding(TabDetail.TRootStyleProperty, BRootStyle);
                this.LeftMenuLeft.Children.Add(td);
            }
        }


    }
}
