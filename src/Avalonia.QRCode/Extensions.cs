using Avalonia.Media;
using Avalonia.Media.Imaging;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing.Imaging;

namespace Avalonia.QRCode
{
    public static class Extensions
    {

        public static string ToHex(this IBrush brush)
        {            
            var scb = brush as ISolidColorBrush;            
            var c = scb.Color;
            return "#" + c.A.ToString("X2")+ c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");      
        }

        public static string ToHex(this Media.Color c)
        {
            return "#" + c.A.ToString("X2") + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }




        public static System.Drawing.Color FromNative(this IBrush brush)
        {
            var scb = brush as ISolidColorBrush;
            return scb.Color.FromNative();
        }

        public static System.Drawing.Color FromNative(this Avalonia.Media.Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static Avalonia.Media.Color ToNative(this System.Drawing.Color color)
        {
            return Avalonia.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }


        /// <summary>
        /// Converts a System.Drawing.Bitmap to Avalonia.Media.Imaging.Bitmap
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static Avalonia.Media.Imaging.Bitmap ToNative(this System.Drawing.Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                return new Avalonia.Media.Imaging.Bitmap(memory);
            }
        }

        /// <summary>
        /// Converts a Avalonia.Media.Imaging.Bitmap to System.Drawing.Bitmap
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static System.Drawing.Bitmap FromNative(this Avalonia.Media.Imaging.Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory);
                memory.Position = 0;
                return new System.Drawing.Bitmap(memory);
            }
        }



    }
}
