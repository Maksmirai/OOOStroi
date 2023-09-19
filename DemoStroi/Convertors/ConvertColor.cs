using System;
using System.Drawing;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Color = Avalonia.Media.Color;

namespace DemoStroi;

public class ConvertColor : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        int quantity = (int)value;
        if (quantity > 0)
            return new SolidColorBrush(Color.FromRgb(178, 232, 167));
        else
        {
            return new SolidColorBrush(Colors.LightGray);
        }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}