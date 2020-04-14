using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PW.Controls
{
    public class HueToColorConverter : IValueConverter
    {
        public Object Convert(
            Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            double doubleValue = (double)value;

            return ColorUtils.ConvertHsvToRgb(doubleValue, 1, 1);
        }
        public Object ConvertBack(
            Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
