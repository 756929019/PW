using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;

namespace PW.Controls
{
    /// <summary>
    /// CommonPanel.xaml 的交互逻辑
    /// </summary>
    public partial class CommonPanel : VirtualControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(CommonPanel), null);
       
        /// <summary>
        /// 标题
        /// </summary>
        [Description("标题"), Category("文本"), DefaultValue("Title")]
        public string Title
        {
            get
            {
                return (string)base.GetValue(CommonPanel.TitleProperty);
            }
            set
            {
                base.SetValue(CommonPanel.TitleProperty, value);
            }
        }
        public CommonPanel()
        {
            base.DefaultStyleKey=(typeof(CommonPanel));
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        public static readonly DependencyProperty HeaderVisibilityProperty = DependencyProperty.Register("HeaderVisibility", typeof(Visibility), typeof(CommonPanel), null);

        /// <summary>
        /// 标题显示隐藏
        /// </summary>
        [Description("标题显示隐藏"), Category("Visibility"), DefaultValue(Visibility.Visible)]
        public Visibility HeaderVisibility
        {
            
            get
            {
                return (Visibility)base.GetValue(CommonPanel.HeaderVisibilityProperty);
            }
            set
            {
                base.SetValue(CommonPanel.TitleProperty, value);
            }
        }
    }
}
