using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        IAssetLoader assets = AvaloniaLocator.Current.GetService<IAssetLoader>();


       
        public bool HasIcon
        {
            get => hasIcon;
            set
            {
                this.RaiseAndSetIfChanged(ref hasIcon, value);
                if (value)
                    IconSource = new Bitmap(assets.Open(new Uri("avares://Sample/Assets/bit.png")));
                else
                    IconSource = null;
            }
        }

        public string Data
        {
            get => data;
            set => this.RaiseAndSetIfChanged(ref data, value);
        }

        public int PixelsPerModule
        {
            get => pixelsPerModule;
            set => this.RaiseAndSetIfChanged(ref pixelsPerModule, value);
        }

        public bool QuitZones
        {
            get => quitZones;
            set => this.RaiseAndSetIfChanged(ref quitZones, value);
        }

        public string ColorHex
        {
            get => colorHex;
            set
            {
                if(value.Length >= 6)
                    this.RaiseAndSetIfChanged(ref colorHex, value); 
            }
        }

        public string SpaceColorHex
        {
            get => spaceColorHex;
            set
            {
                if (value.Length >= 6)
                    this.RaiseAndSetIfChanged(ref spaceColorHex, value);
            }
        }

        public IBitmap IconSource
        {
            get => iconSource;
            set => this.RaiseAndSetIfChanged(ref iconSource, value);
        }

        public int IconScale
        {
            get => iconScale;
            set => this.RaiseAndSetIfChanged(ref iconScale, value);
        }

        public int IconBorder
        {
            get => iconBorder;
            set => this.RaiseAndSetIfChanged(ref iconBorder, value);
        }

        public MainWindowViewModel()
        {
            this.Data = "https://github.com/MikeCodesDotNET/Avalonia-QRCode";
            this.PixelsPerModule = 20;
            IconScale = 15;
            IconBorder = 6;
            HasIcon = false;
        }


        private string data;
        private int pixelsPerModule;
        private bool quitZones;
        private string colorHex;
        private string spaceColorHex;

        private bool hasIcon;
        private IBitmap iconSource;
        private int iconScale;
        private int iconBorder;

        

    }
}
