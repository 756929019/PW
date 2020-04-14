using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PW.Controls
{
    public class ObjectConvertColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType().ToString().IndexOf("ChatUser") > -1)
            {
                return (Brush)new BrushConverter().ConvertFromString("#FFEAEAEA");
            }
            return (Brush)new BrushConverter().ConvertFromString("#FFE0E0E0");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
