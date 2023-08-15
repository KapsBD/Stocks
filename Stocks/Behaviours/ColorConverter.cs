using System;
using System.Globalization;
using System.Windows.Data;

namespace Stocks.Behaviours
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                if (doubleValue < 0)
                    return "Negative";
                else if (doubleValue > 0)
                    return "Positive";
            }
            return "Zero";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}
