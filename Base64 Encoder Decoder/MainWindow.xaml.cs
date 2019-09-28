using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Base64_Encoder_Decoder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly MainWindowViewModel _viewModel = new MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
            _viewModel.EncodedText = "VGhpcyBpcyBhIFRlc3Qh";
            _viewModel.DecodedText = "This is a Test!";
        }

        void PopupException(string message, string caption = "Exception")
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ButtonEncode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var inputstr = _viewModel.DecodedText;
                var inputbytes = new byte[0];
                if (inputstr.StartsWith("0x")) //from hex string
                {
                    inputstr = inputstr.Substring(2);
                    inputstr = inputstr.Replace("-", "");
                    inputbytes = Enumerable.Range(0, inputstr.Length)
                                           .Where(x => x % 2 == 0)
                                           .Select(x => Convert.ToByte(inputstr.Substring(x, 2), 16))
                                           .ToArray();
                }
                else  //from ascii string
                {
                    inputbytes = Encoding.ASCII.GetBytes(inputstr);
                }
                var outputstr = Convert.ToBase64String(inputbytes);
                _viewModel.EncodedText = outputstr;
                _viewModel.EncodedTextColor = Colors.Blue.ToString();
                _viewModel.DecodedTextColor = Colors.Black.ToString();
            }
            catch (Exception ex)
            {
                PopupException(ex.Message);
            }
        }

        private void ButtonDecode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var inputstr = _viewModel.EncodedText;
                var inputbytes = Convert.FromBase64String(inputstr);
                var outputstr = "";
                if (inputbytes.Any(p => p < 32 || p > 126)) //to hex string
                {
                    outputstr = "0x" + BitConverter.ToString(inputbytes);
                }
                else  //to ascii string
                {
                    outputstr = Encoding.ASCII.GetString(inputbytes);
                }
                _viewModel.DecodedText = outputstr;
                _viewModel.EncodedTextColor = Colors.Black.ToString();
                _viewModel.DecodedTextColor = Colors.Blue.ToString();
            }
            catch (Exception ex)
            {
                PopupException(ex.Message);
            }
        }
    }
}
