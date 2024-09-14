using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DemoApp.WPF.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isVisible = (bool)value;
            Visibility visibility;
            if (isVisible)
            {
                visibility = Visibility.Visible;
            }
            else
            {
                visibility = Visibility.Collapsed;
            }
            return visibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
