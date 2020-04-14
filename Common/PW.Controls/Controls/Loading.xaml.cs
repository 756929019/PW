using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace PW.Controls.Controls
{
    public class Loading : System.Windows.Controls.Control
    {

        static Loading()
        {

            //重载默认样式
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Loading), new FrameworkPropertyMetadata(typeof(Loading)));
            ////DependencyProperty注册 FillColor
            //Loading.FillColorProperty = DependencyProperty.Register("FillColor", typeof(Color), typeof(Loading),
            //new UIPropertyMetadata(Colors.DarkBlue,
            //new PropertyChangedCallback(OnUriChanged))
            //);
        }
        //属性变更回调函数
        private static void OnUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Border b = (Border)d;
        }

        #region 自定义Fields
        public static readonly DependencyProperty FillColorProperty = DependencyProperty.Register("FillColor", typeof(Color), typeof(Loading), null);
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(Loading), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsActiveChanged));

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
                return (Color)base.GetValue(Loading.FillColorProperty);
            }
            set
            {
                base.SetValue(Loading.FillColorProperty, value);
            }
        }

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        private static void IsActiveChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ring = dependencyObject as Loading;
            if (ring == null)
                return;

            ring.UpdateActiveState();
        }

        private void UpdateActiveState()
        {
            if (IsActive)
            {
                this.Visibility = Visibility.Visible;
            }
            else
            {
                this.Visibility = Visibility.Collapsed;
            }
        }
    }
}
