using System;
using Avalonia.Data.Converters;
using System.Globalization;

namespace SauceBucket.Converters
{
    /// <summary>
    /// Converts bool to bool for IsVisible binding (passthrough for consistency with XAML).
    /// In Avalonia, use IsVisible property instead of Visibility enum.
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // Return true for visible, false for collapsed
            if (value is bool b) return b;
            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool b) return b;
            return false;
        }
    }
}
