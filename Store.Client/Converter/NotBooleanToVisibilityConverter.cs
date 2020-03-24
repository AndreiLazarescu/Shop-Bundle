﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Store.Client.Converter
{
    public class NotBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue && !boolValue)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility && visibility == Visibility.Collapsed)
            {
                return true;
            }

            return false;
        }
    }
}
