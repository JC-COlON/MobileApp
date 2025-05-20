using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.Maui.Controls;


namespace DigesettAPP.Converters
{
    public class BoolToArrowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isExpanded = (bool)value;
            return isExpanded ? "flechaarriba.png" : "flechaabajo.png"; // Cambia los nombres de los íconos según corresponda
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value; // No necesitamos este método
        }
    }
}
