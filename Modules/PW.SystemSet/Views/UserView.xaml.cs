using PW.SystemSet.ViewModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace PW.SystemSet.Views
{
    /// <summary>
    /// StyleSetting.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(UserView))]
    public partial class UserView : UserControl
    {
        public UserView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new UserViewModel();
        }
    }
}
