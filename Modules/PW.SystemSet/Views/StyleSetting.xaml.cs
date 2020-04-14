using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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

namespace PW.SystemSet.Views
{
    /// <summary>
    /// StyleSetting.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(StyleSetting))]
    public partial class StyleSetting : UserControl
    {
        public StyleSetting()
        {
            InitializeComponent();

            List<int[]> list = new List<int[]>();
            list.Add(new int[] { 1, 2, 3, 4, 5 });
            list.Add(new int[] { 2, 3, 4, 5, 6 });
            list.Add(new int[] { 3, 4, 5, 6, 7 });

            int _col = list[0].Length;
            int _row = list.Count;
            for (int i = 0; i < _col; i++)
            {
                dataGrid.Columns.Add(new DataGridTextColumn
                {
                    Width = 60,
                    Header = (char)(65 + i),
                    Binding = new Binding("{"+i.ToString()+"}")
                });
            }
            dataGrid.ItemsSource = list;
        }
    }
}
