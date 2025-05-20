using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace DigesettAPP.Converters
{
    public class BoolToHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isExpanded = (bool)value;
            return isExpanded ? 150 : 60; // Cambia las alturas según tus necesidades (por ejemplo, 150 para expandido y 60 para contraído)
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value; // No necesitamos este método
        }
    }
}
