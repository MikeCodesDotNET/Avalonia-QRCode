using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

using System;
using System.Drawing;
using System.Globalization;
using Avalonia.QRCode;
using Avalonia.Media;
using Brushes = Avalonia.Media.Brushes;

namespace Sample.Converters
{
    public class HexToColorConverter : IValueConverter
    {
        public static HexToColorConverter Instance = new HexToColorConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is string str)
            {
                var colorcode = str.TrimStart('#');

                Avalonia.Media.Color col;

                if (colorcode.Length == 6)
                {
                    col = Avalonia.Media.Color.FromArgb(255, // hardcoded opaque
                                byte.Parse(colorcode.Substring(0, 2), NumberStyles.HexNumber),
                                byte.Parse(colorcode.Substring(2, 2), NumberStyles.HexNumber),
                                byte.Parse(colorcode.Substring(4, 2), NumberStyles.HexNumber));
                }
                else 
                    col = Avalonia.Media.Color.FromArgb(
                                byte.Parse(colorcode.Substring(0, 2), NumberStyles.HexNumber),
                                byte.Parse(colorcode.Substring(2, 2), NumberStyles.HexNumber),
                                byte.Parse(colorcode.Substring(4, 2), NumberStyles.HexNumber),
                                byte.Parse(colorcode.Substring(6, 2), NumberStyles.HexNumber));


                return new SolidColorBrush(col);

            }

            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Avalonia.Media.Color c)
            {
                return c.ToHex();
            }

            if (value is Avalonia.Media.IBrush)
            {
                var b = value as SolidColorBrush;
                return b.Color.ToHex();
            }

            throw new NotSupportedException();
        }
    }
}
