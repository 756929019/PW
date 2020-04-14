using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PW.Controls
{
    [ValueConversion(typeof(double), typeof(String))]
    public class DoubleToIntegerStringConverter : IValueConverter
    {
        public Object Convert(
            Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            double doubleValue = (double)value;
            int intValue = (int)doubleValue;

            return intValue.ToString();
        }
        public Object ConvertBack(
            Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            String stringValue = (String)value;
            double doubleValue = 0;
            if (!Double.TryParse(stringValue, out doubleValue))
                doubleValue = 0;

            return doubleValue;
        }
    }

}
