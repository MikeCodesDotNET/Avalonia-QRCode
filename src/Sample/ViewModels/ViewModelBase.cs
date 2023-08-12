using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Sample.ViewModels
{

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void RaisePropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);

        protected void RaiseAndSetIfChanged<T>(ref T field, T value, [CallerMemberName] string propertyname = "")
        {
            RaiseAndSetIfChanged(ref field, value, new PropertyChangedEventArgs(propertyname));
        }

        protected void RaiseAndSetIfChanged<T>(ref T field, T value, PropertyChangedEventArgs e)
        {
            if (!Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, e);
            }
        }
    }
}
