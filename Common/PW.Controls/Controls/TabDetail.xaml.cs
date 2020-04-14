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
    /// TabDetail.xaml 的交互逻辑
    /// </summary>
    public partial class TabDetail : UserControl
    {
        public TabDetail()
        {
            InitializeComponent();
        }

        //根目录的名字
        public static readonly DependencyProperty TRootNameProperty = DependencyProperty.Register("TRootName", typeof(string), typeof(TabDetail), new PropertyMetadata(default(string)));
        public string TRootName
        {
            get { return (string)GetValue(TRootNameProperty); }
            set { SetValue(TRootNameProperty, value); }
        }
        //定义ListBox的数据源
        public static readonly DependencyProperty TIResourceProperty = DependencyProperty.Register("TIResource", typeof(IList<NavigateDetail>), typeof(TabDetail), new PropertyMetadata(default(IList<NavigateDetail>)));
        public IList<NavigateDetail> TIResource
        {
            get { return (IList<NavigateDetail>)GetValue(TIResourceProperty); }
            set { SetValue(TIResourceProperty, value); }
        }

        //定义Grid的高
        public static readonly DependencyProperty TItemActualHeightProperty = DependencyProperty.Register("TItemActualHeight", typeof(double), typeof(TabDetail), new PropertyMetadata(default(double)));
        public double TItemActualHeight
        {
            get { return (double)GetValue(TItemActualHeightProperty); }
            set { SetValue(TItemActualHeightProperty, value * TIResource.Count + 5); }
        }
        //根目录的样式
        public static readonly DependencyProperty TRootStyleProperty = DependencyProperty.Register("TRootStyle", typeof(Style), typeof(TabDetail), new PropertyMetadata(default(Style)));
        public Style TRootStyle
        {
            get { return (Style)GetValue(TRootStyleProperty); }
            set { SetValue(TRootStyleProperty, value); }
        }
        //根目录上面的字体样式
        public static readonly DependencyProperty TTabDetailTextProperty = DependencyProperty.Register("TTabDetailText", typeof(Style), typeof(TabDetail), new PropertyMetadata(default(Style)));
        public Style TTabDetailText
        {
            get { return (Style)GetValue(TTabDetailTextProperty); }
            set { SetValue(TTabDetailTextProperty, value); }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;
            this.TListBox.SelectedIndex = -1;
            if (grid != null)
            {
                if (grid.Tag.ToString() == "0")
                {
                    (this.Resources["TListBoxOut"] as Storyboard).Begin();
                    grid.Tag = "1";
                }
                else
                {
                    (this.Resources["TListBoxIn"] as Storyboard).Begin();
                    grid.Tag = "0";
                }
            }
        }

    }
}
