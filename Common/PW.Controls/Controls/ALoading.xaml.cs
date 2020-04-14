using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PW.Controls.Controls
{
    /// <summary>
    /// ALoading.xaml 的交互逻辑
    /// </summary>
    public partial class ALoading : UserControl
    {
        public ALoading()
        {
            InitializeComponent();
        }
        static ALoading()
        {

            //重载默认样式
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ALoading), new FrameworkPropertyMetadata(typeof(ALoading)));
            ////DependencyProperty注册 FillColor
            //Loading.FillColorProperty = DependencyProperty.Register("FillColor", typeof(Color), typeof(Loading),
            //new UIPropertyMetadata(Colors.DarkBlue,
            //new PropertyChangedCallback(OnUriChanged))
            //);
        }
        #region 自定义Fields
        public static readonly DependencyProperty FillColorProperty = DependencyProperty.Register("FillColor", typeof(Color), typeof(ALoading), null);
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(ALoading), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsActiveChanged));

        #endregion

        //VS设计器属性支持
        /// <summary>
        /// 标题
        /// </summary>
        [Description("背景色"), Category("个性配置"), DefaultValue("#FF668899")]
        public Color FillColor
        {
            get
            {
                return (Color)base.GetValue(ALoading.FillColorProperty);
            }
            set
            {
                base.SetValue(ALoading.FillColorProperty, value);
            }
        }

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        private static void IsActiveChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ring = dependencyObject as ALoading;
            if (ring == null)
                return;

            ring.UpdateActiveState();
        }

        private void UpdateActiveState()
        {
            if (IsActive)
            {
            }
            else
            {
            }
        }

        //public override void OnApplyTemplate()
        //{
        //    UpdateActiveState();
        //}
    }
}
