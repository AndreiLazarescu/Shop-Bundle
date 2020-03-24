using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Store.Client.Converter
{
    public class ICollectionToStringComverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<object> collection)
            {
                var builder = new StringBuilder();
                var index = 0;
                foreach (var item in collection)
                {
                    index++;
                    builder.AppendLine($"{index}. {item.ToString()}");
                }

                return builder.ToString();
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
