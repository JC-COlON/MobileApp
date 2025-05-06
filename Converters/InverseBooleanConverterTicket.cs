﻿
using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace DigesettAPP.Converters
{
    public class InverseBooleanConverterTicket : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool boolValue ? !boolValue : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool boolValue ? !boolValue : value;
        }
    }
}
