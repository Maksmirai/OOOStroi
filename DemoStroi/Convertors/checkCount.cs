using System;
using System.Globalization;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using DemoStroi.Context;

namespace DemoStroi;


public class checkCount : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        string s = "";

        if (int.Parse(value.ToString()) > 0)
            s = "Eсть в наличии";
        else
        {
            s = "Нет в наличии";
        }
            

        return s;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}