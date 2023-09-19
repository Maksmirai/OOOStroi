using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Shared.PlatformSupport;
using DemoStroi.Context;
using DemoStroi.Models;

namespace DemoStroi;

public class Convertor : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        AssetLoader assetLoader = new AssetLoader();
        Bitmap bit;
        if (value.ToString() == null || value.ToString() == "" || value.ToString() == " ")
        {
            Uri uri = new Uri($"avares://DemoStroi/Image/picture.png", UriKind.RelativeOrAbsolute);
            bit = new Bitmap(assetLoader.Open(uri));
        }
        else
        {
            var s = Path.Combine(Directory.GetCurrentDirectory(), $"Image\\{value.ToString()}").Replace("bin\\Debug\\net6.0\\", "");
            using (var str = new FileStream( s , FileMode.Open) )
            {
                bit = new Bitmap(str);
                str.Close();
            }
        }
        return bit;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}