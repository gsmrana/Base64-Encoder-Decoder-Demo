using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Base64_Encoder_Decoder
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _encodedText;
        public string EncodedText
        {
            get => _encodedText;
            set => SetProperty(ref _encodedText, value);
        }

        private string _encodedTextColor = "Blue";
        public string EncodedTextColor
        {
            get => _encodedTextColor;
            set => SetProperty(ref _encodedTextColor, value);
        }

        private string _decodedText;
        public string DecodedText
        {
            get => _decodedText;
            set => SetProperty(ref _decodedText, value);
        }

        private string _decodedTextColor = "Black";
        public string DecodedTextColor
        {
            get => _decodedTextColor;
            set => SetProperty(ref _decodedTextColor, value);
        }

        protected void SetProperty<T>(ref T field, T newValue, [CallerMemberName]string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
