using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using QRCoder;

namespace Avalonia.QRCode
{
    public class QRCode : Control
    {

        public static readonly StyledProperty<int> PixelsPerModuleProperty = AvaloniaProperty.Register<QRCode, int>(nameof(PixelsPerModule), 20);

        public static readonly StyledProperty<IBrush> ColorProperty = AvaloniaProperty.Register<QRCode, IBrush>(nameof(Color), Brushes.Black);

        public static readonly StyledProperty<IBrush> SpaceBrushProperty = AvaloniaProperty.Register<QRCode, IBrush>(nameof(SpaceBrush), Brushes.White);

        public static readonly StyledProperty<bool> DrawQuietZonesProperty = AvaloniaProperty.Register<QRCode, bool>(nameof(DrawQuietZones), true);

        public static readonly StyledProperty<string> DataProperty = AvaloniaProperty.Register<QRCode, string>(nameof(Data), string.Empty);



        public static readonly StyledProperty<Bitmap> IconProperty = AvaloniaProperty.Register<QRCode, Bitmap>(nameof(Icon));

        public static readonly StyledProperty<int> IconScaleProperty = AvaloniaProperty.Register<QRCode, int>(nameof(IconScale), 15);

        public static readonly StyledProperty<int> IconBorderWidthProperty = AvaloniaProperty.Register<QRCode, int>(nameof(IconBorderWidth), 6);


        /// <summary>
        /// Width of the border which is drawn around the icon. Minimum: 1
        /// </summary>
        public int IconBorderWidth
        {
            get { return GetValue(IconBorderWidthProperty); }
            set
            {
                if (value < 1)
                    value = 1;

                SetValue(IconBorderWidthProperty, value);
            }
        }

        /// <summary>
        /// Value from 1-99. Sets how much % of the QR Code will be covered by the icon
        /// </summary>
        public int IconScale
        {
            get { return GetValue(IconScaleProperty); }
            set
            {
                if(value < 1)
                    value = 1;
                if(value > 99)
                    value = 99;

                SetValue(IconScaleProperty, value);
            }
        }

        /// <summary>
        /// If null, then ignored. If set, the Bitmap is drawn in the middle of the QR Code
        /// </summary>
        public Bitmap Icon
        {
            get { return GetValue(IconProperty); }
            set 
            { 
                SetValue(IconProperty, value);
            }
        }


        public string Data
        {
            get { return GetValue(DataProperty); }
            set 
            { 
                SetValue(DataProperty, value);
            }
        }

        /// <summary>
        /// If true a white border is drawn around the whole QR Code
        /// </summary>
        public bool DrawQuietZones
        {
            get { return GetValue(DrawQuietZonesProperty); }
            set 
            { 
                SetValue(DrawQuietZonesProperty, value);
            }
        }

        /// <summary>
        /// The color of the light/white modules
        /// </summary>
        public IBrush SpaceBrush
        {
            get { return GetValue(SpaceBrushProperty); }
            set 
            {
                SetValue(SpaceBrushProperty, value);
            }
        }

        /// <summary>
        /// The color of the dark/black modules
        /// </summary>
        public IBrush Color
        {
            get { return GetValue(ColorProperty); }
            set 
            { 
                SetValue(ColorProperty, value);
            }
        }

        /// <summary>
        /// The pixel size each b/w module is drawn
        /// </summary>
        public int PixelsPerModule
        {
            get { return GetValue(PixelsPerModuleProperty); }
            set 
            { 
                SetValue(PixelsPerModuleProperty, value);
            }
        }

        public QRCode()
        {
            AffectsRender<QRCode>(DataProperty, PixelsPerModuleProperty, DrawQuietZonesProperty, ColorProperty, SpaceBrushProperty, IconProperty, IconScaleProperty, IconBorderWidthProperty);

        }


        public override void Render(DrawingContext context)
        {
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(Data, QRCodeGenerator.ECCLevel.Q);
            QRCoder.QRCode qrCode = new QRCoder.QRCode(qrCodeData);
            System.Drawing.Bitmap systemBitmap = null;

            if (Icon == null)
                systemBitmap = qrCode.GetGraphic(PixelsPerModule, Color.ToHex(), SpaceBrush.ToHex(), DrawQuietZones);
            else
                systemBitmap = qrCode.GetGraphic(PixelsPerModule, Color.FromNative(), SpaceBrush.FromNative(), Icon.FromNative(), IconScale, IconBorderWidth, DrawQuietZones);

            var source = systemBitmap.ToNative();


            if (source != null && Bounds.Width > 0 && Bounds.Height > 0)
            {
                Rect viewPort = new Rect(Bounds.Size);
                Size sourceSize = source.Size;

                Vector scale = Stretch.Uniform.CalculateScaling(Bounds.Size, sourceSize, StretchDirection.Both);
                Size scaledSize = sourceSize * scale;
                Rect destRect = viewPort
                    .CenterRect(new Rect(scaledSize))
                    .Intersect(viewPort);
                Rect sourceRect = new Rect(sourceSize)
                    .CenterRect(new Rect(destRect.Size / scale));

                context.DrawImage(source, sourceRect, destRect);
            }
        }


        Image image;
        QRCodeGenerator qrGenerator = new QRCodeGenerator();

    }
}
