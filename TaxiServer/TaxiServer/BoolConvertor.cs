using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TaxiServer
{
    public class BoolConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var booleanVal = (bool)value;
            if(booleanVal == true)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
